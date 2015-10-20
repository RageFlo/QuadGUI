using System;
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

        public class DatenAuswahlElement
        {
            public byte code { get; set; }
            public string name { get; set; }
            public Series serie { get; set; }
            public override string ToString()
            {
                return code + "  " + name;
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

            //datenAuswahlAdd(0, "Accel X");
            //datenAuswahlAdd(1, "Accel Y");
            //datenAuswahlAdd(2, "Accel Z");
            //datenAuswahlAdd(3, "Temp");
            datenAuswahlAdd(4, "Gyro X");
            datenAuswahlAdd(5, "Gyro Y");
            datenAuswahlAdd(6, "Gyro Z");
            datenAuswahlAdd(7, "angle X");
            datenAuswahlAdd(8, "angle y");
            datenAuswahlAdd(9, "angle z");
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
            foreach (DatenAuswahlElement datenEle in lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>())
            {
                datenEle.serie.Points.Clear();
                if (lsbDatenAuswahl.CheckedItems.Contains(datenEle))
                {
                    foreach (double y in mKommu.recData.Where(x => x.code == datenEle.code).Select<Kommunikator.armSetting, Int32>(x => x.value))
                        datenEle.serie.Points.AddY(y);
                }
            }

        }

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
            try { 
                newCode = Convert.ToByte(txbmCode.Text);
            }
            catch
            {
                return;
            }
            datenAuswahlAdd(newCode, txbmName.Text);
        }

        private void datenAuswahlAdd(byte newCode, string newName)
        {
            bool checkExists;
            checkExists = lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.code).Contains(newCode);
            checkExists |= lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.name).Contains(newName);
            if (lsbDatenAuswahl.Items.Count < 8 && !checkExists && newName.Length>0)
            {
                Series newSerie = new Series(newName);
                newSerie.AxisLabel = newName;
                newSerie.YValueType = ChartValueType.Int32;
                //newSerie.XAxisType = AxisType.Primary;
                //newSerie.XValueType = ChartValueType.Int32;
                //newSerie.IsXValueIndexed = true;
                newSerie.ChartType = SeriesChartType.Line;
                lsbDatenAuswahl.Items.Add(new DatenAuswahlElement { code = newCode, name = newName, serie = newSerie },true);
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


    }
}
