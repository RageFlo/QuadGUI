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
        private Kommunikator mKommu = null;
        private Timer zyklTimer = new Timer();
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
        }

        void zyklTimer_Tick(object sender, EventArgs e)
        {
            if (mKommu == null)
                return;
            mKommu.zyklischMain();
            lblPingCounter.Text = "Pings: " + Convert.ToString(mKommu.cntping);
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
            foreach(DatenAuswahlElement datenEle in lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>()){
                datenEle.serie.Points.Clear();
                foreach (double y in mKommu.recData.Where(x => x.code==datenEle.code).Select<Kommunikator.armSetting,Int32>(x => x.value))
                    datenEle.serie.Points.AddY(y);
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
            if(mKommu==null)
            {
                mKommu = new Kommunikator(comPort, baudRate);
                
            }
            else
            {
                //DO SOMETHING TO CLOSE!
                mKommu = new Kommunikator(comPort, baudRate);
            }    
        }

        private void btnDatenAdd_Click(object sender, EventArgs e)
        {
            byte newCode;
            string newName;
            bool checkExists;
            try { 
                newCode = Convert.ToByte(txbmCode.Text);
            }
            catch
            {
                return;
            }
            newName = txbmName.Text;
            checkExists = lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.code).Contains(newCode);
            checkExists |= lsbDatenAuswahl.Items.Cast<DatenAuswahlElement>().Select(x => x.name).Contains(newName);
            if (lsbDatenAuswahl.Items.Count < 8 && !checkExists && newName.Length>0)
            {
                Series newSerie = new Series(newName);
                newSerie.AxisLabel = newName;
                newSerie.YValueType = ChartValueType.Int32;
                //serY.IsXValueIndexed = true;
                newSerie.ChartType = SeriesChartType.Line;
                lsbDatenAuswahl.Items.Add(new DatenAuswahlElement { code = newCode, name = newName, serie = newSerie });
                txbmCode.Text = (newCode + 1).ToString();
                chrDaten.Series.Add(newSerie);
            }
        }

        private void btnDatenremove_Click(object sender, EventArgs e)
        {
            if (lsbDatenAuswahl.SelectedItem != null && lsbDatenAuswahl.SelectedItem.GetType().Name == "DatenAuswahlElement")
            {
                DatenAuswahlElement toDelete = (DatenAuswahlElement)lsbDatenAuswahl.SelectedItem;
                chrDaten.Series.Remove(toDelete.serie);
                lsbDatenAuswahl.Items.Remove(toDelete);
            }
        }


    }
}
