﻿using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuadroTest1
{
    public partial class formMain : Form
    {
        private Kommunikator mKommu = new Kommunikator();
        private Timer zyklTimer = new Timer();
        private uint tick200ms = 0;
        private bool startedConnect = false;
        private bool chrPaused = false;

        public class DatenAuswahlElement
        {
            private double _scale = 1;
            public byte code { get; set; }
            public string name { get; set; }
            public Series serie { get; set; }
            public double scale { get { return _scale; } set{ _scale = value;} }
            public override string ToString()
            {
                return String.Format("{0:000}  {1,-15} {2:e}",code,name,scale);
            }
        }


        public formMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; //Größe nicht veränderbar
            zyklTimer.Interval = 200;
            zyklTimer.Tick += new EventHandler(zyklTimer_Tick);
            zyklTimer.Start();
            chrDaten.Series.Clear();

            for (int i = 0; i < 3; i++)
            {
                pnlToSet.Controls.Add(new settingsUI(mKommu));
            }
            
            //datenAuswahlAdd(0, "Accel X",1);
            //datenAuswahlAdd(1, "Accel Y", 0.1);
            //datenAuswahlAdd(2, "Accel Z");
            //datenAuswahlAdd(3, "Temp");
            //datenAuswahlAdd(4, "Gyro X", 0.1 / 13.1);
            //datenAuswahlAdd(5, "Gyro Y");
            //datenAuswahlAdd(6, "Gyro Z");
            datenAuswahlAdd(7, "angle g X",0.0001/13.1);
            //datenAuswahlAdd(8, "angle g y");
            //datenAuswahlAdd(9, "angle g z");
            datenAuswahlAdd(10, "angle a X", 0.1);
            //datenAuswahlAdd(11, "angle a Y", 1);
            //datenAuswahlAdd(12, "angle a Z", 1);
            //datenAuswahlAdd(13, "angleCpmp X", 0.01 / 13.1);
            //datenAuswahlAdd(14, "angleCpmp Y", 0.01 / 13.1);
            //datenAuswahlAdd(15, "angleCpmp Z", 0.01);
            datenAuswahlAdd(17, "pidX", 1);
            datenAuswahlAdd(18, "kalmanX", 1 / 1000.0);
        }

        void zyklTimer_Tick(object sender, EventArgs e)
        {
            if (mKommu == null)
                return;
            tick200ms++;
            mKommu.zyklischMain();
            lblPingCounter.Text = "Pings: " + Convert.ToString(mKommu.cntPing);
            lblMessagesRec.Text = "Empfangen: " + Convert.ToString(mKommu.cntRec);
            lblMessagesSent.Text = "Gesendet: " + Convert.ToString(mKommu.cntSend);
            switch (mKommu.mKommunikatorState)
            {
                case Kommunikator.kommunikatorStateTyp.connected:
                    lblConState.Text = "Verbunden";
                    lblConState.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case Kommunikator.kommunikatorStateTyp.connecting:
                    lblConState.Text = "Verbinde";
                    lblConState.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    break;
                case Kommunikator.kommunikatorStateTyp.disconnected:
                    lblConState.Text = "Getrennt";
                    lblConState.BackColor = System.Drawing.Color.Gold;
                    break;
                case Kommunikator.kommunikatorStateTyp.error:
                    lblConState.Text = "Fehler";
                    lblConState.BackColor = System.Drawing.Color.LightPink;
                    break;
            }

            //Daten einlesen
            if (mKommu.mKommunikatorState == Kommunikator.kommunikatorStateTyp.connected && !chrPaused)
            {
                foreach (DatenAuswahlElement datenEle in lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>())
                {
                    datenEle.serie.Points.Clear();
                    if (lsbDatenAuswahl.CheckedItems.Contains(datenEle))
                    {
                        foreach (Kommunikator.armRecVal y in mKommu.recData.Where(x => x.code == datenEle.code))
                            datenEle.serie.Points.AddXY(y.time,y.value*datenEle.scale);
                    }
                }
            }
            if (mKommu.mKommunikatorState == Kommunikator.kommunikatorStateTyp.connected ||
                mKommu.mKommunikatorState == Kommunikator.kommunikatorStateTyp.connecting)
            {
                while (mKommu.allDataForTB.Count > 0)
                {
                    string[] lines = txtbAllData.Lines;
                    //txtbAllData.Text +=  "xxx\r\n";
                    if (lines.Length > 40)
                    {
                        lines = lines.Skip(lines.Length - 20).ToArray();
                    }
                    txtbAllData.Lines = lines;
                    if (!chrPaused)
                        txtbAllData.AppendText("\r\n" + mKommu.allDataForTB.Dequeue());
                    else
                        mKommu.allDataForTB.Dequeue();
                }
            }
        }
        //private string byteArrayToString(byte[] pBytes)
        //{
            
        //    string temp = string.Empty;
        //    for(int i = 0; i < pBytes.Length; i++){
        //        byte curByte = pBytes[i];
        //        if (i > 0) {
        //            temp += Convert.ToInt32(curByte) + " ";
        //        }
        //        else
        //        {
        //            temp += Convert.ToChar(curByte); ;
        //        }
               
        //    }
        //    return temp;
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();
            foreach (String port in ports)
            {
                cmbComPorts.Items.Add(port);
            }
            if (cmbComPorts.Items.Count > 0)
            {
                cmbComPorts.SelectedIndex = 0;
               
            }

            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            String comPort = String.Empty;
            int baudRate = 0;
            try
            {
                comPort = (String)cmbComPorts.SelectedItem;
                baudRate = Convert.ToInt32(txtBaudRate.Text);
            }
            catch
            {
                Debug.WriteLine("Couldnt convert Baudrate!");
                return;
            }
            if (comPort == null || comPort == String.Empty || baudRate == 0 || !comPort.Contains("COM"))
            {
                Debug.WriteLine("Empty Com or No Baud");
                return;
            }
            if (!startedConnect)
            {
                mKommu.open(comPort, baudRate);
                startedConnect = true;
                foreach (DatenAuswahlElement curToSend in lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>())
                {
                    mKommu.queRequestValue(curToSend.code, true, false);
                }
            }
            else
            {
                mKommu.close();
                startedConnect = false;
            }
                
        }

        private void btnDatenAdd_Click(object sender, EventArgs e)
        {
            byte newCode;
            double newScale;
            try { 
                newCode = Convert.ToByte(txbmCode.Text);
                newScale = Convert.ToDouble(txbmScale1.Text) / Convert.ToDouble(txbmScale2.Text) * Math.Pow(10,Convert.ToDouble(txbmScale3.Text));
            }
            catch
            {
                return;
            }
            datenAuswahlAdd(newCode, txbmName.Text, newScale);
        }

        private void datenAuswahlAdd(byte newCode, string newName, double newScale)
        {
            bool checkExists;
            checkExists = lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.code).Contains(newCode);
            checkExists |= lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.name).Contains(newName);
            if (lsbDatenAuswahl.Items.Count < 8 && !checkExists && newName.Length>0)
            {
                Series newSerie = new Series(newName);
                //newSerie.AxisLabel = newName;
                newSerie.YValueType = ChartValueType.Double;
                newSerie.XAxisType = AxisType.Primary;
                newSerie.XValueType = ChartValueType.Double;
                
                //newSerie.IsXValueIndexed = true;
                newSerie.ChartType = SeriesChartType.Line;
                lsbDatenAuswahl.Items.Add(new DatenAuswahlElement { code = newCode, name = newName, serie = newSerie, scale = newScale },true);
                txbmCode.Text = (newCode + 1).ToString();
                chrDaten.Series.Add(newSerie);
                mKommu.queRequestValue(newCode, true, false);
            }
        }

        private void btnDatenremove_Click(object sender, EventArgs e)
        {
            if (lsbDatenAuswahl.SelectedItem != null && lsbDatenAuswahl.SelectedItem.GetType().Name == "DatenAuswahlElement")
            {
                DatenAuswahlElement toDelete = (DatenAuswahlElement)lsbDatenAuswahl.SelectedItem;
                chrDaten.Series.Remove(toDelete.serie);
                lsbDatenAuswahl.Items.Remove(toDelete);
                mKommu.queRequestValue(toDelete.code, false, true);
            }
        }

        private void lsbDatenAuswahl_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

        private void btnRohdaten_Click(object sender, EventArgs e)
        {
           // mKommu.recData.Enqueue(new Kommunikator.armRecVal() { code = 1, value = 1, time=100-(int)tick200ms});
            Rohdaten currentroh = new Rohdaten(mKommu.recData);
            currentroh.Show();
        }

        private void btnPause_CheckedChanged(object sender, EventArgs e)
        {
            chrPaused = btnPause.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveExcel.saveInExcel(chrDaten.Series);
        }

        private void btnloadScript_Click(object sender, EventArgs e)
        {
            Scripting.runScript();
        }

    }
}
