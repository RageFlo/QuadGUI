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
        System.Windows.Forms.DataVisualization.Charting.Series serX = new Series("xAcc");
        System.Windows.Forms.DataVisualization.Charting.Series serY = new Series("yAcc");
        System.Windows.Forms.DataVisualization.Charting.Series serZ = new Series("zAcc");
        System.Windows.Forms.DataVisualization.Charting.Series serTemp = new Series("mpuTemp");
        System.Windows.Forms.DataVisualization.Charting.Series serGX = new Series("xGyro");
        System.Windows.Forms.DataVisualization.Charting.Series serGY = new Series("yGyro");
        System.Windows.Forms.DataVisualization.Charting.Series serGZ = new Series("zGyro");


        public formMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; //Größe nicht veränderbar
            zyklTimer.Interval = 200;
            zyklTimer.Tick += new EventHandler(zyklTimer_Tick);
            zyklTimer.Start();
            chrDaten.Series.Clear();
            
            serX.AxisLabel = "x Accel";
            serX.YValueType = ChartValueType.Int32;
            //serX.IsXValueIndexed = true;
            serX.ChartType = SeriesChartType.Line;
            serY.AxisLabel = "y Accel";
            serY.YValueType = ChartValueType.Int32;
            //serY.IsXValueIndexed = true;
            serY.ChartType = SeriesChartType.Line;
            serZ.AxisLabel = "z Accel";
            serZ.YValueType = ChartValueType.Int32;
            //serZ.IsXValueIndexed = true;
            serZ.ChartType = SeriesChartType.Line;

            serTemp.AxisLabel = "Temp Gyro";
            serTemp.YValueType = ChartValueType.Int32;
            //serZ.IsXValueIndexed = true;
            serTemp.ChartType = SeriesChartType.Line;

            serGX.AxisLabel = "x Gyro";
            serGX.YValueType = ChartValueType.Int32;
            //serX.IsXValueIndexed = true;
            serGX.ChartType = SeriesChartType.Line;
            serGY.AxisLabel = "y Gyro";
            serGY.YValueType = ChartValueType.Int32;
            //serY.IsXValueIndexed = true;
            serGY.ChartType = SeriesChartType.Line;
            serGZ.AxisLabel = "z Gyro";
            serGZ.YValueType = ChartValueType.Int32;
            //serZ.IsXValueIndexed = true;
            serGZ.ChartType = SeriesChartType.Line;

            chrDaten.Series.Add(serX);
            chrDaten.Series.Add(serY);
            chrDaten.Series.Add(serZ);
            chrDaten.Series.Add(serTemp);
            chrDaten.Series.Add(serGX);
            chrDaten.Series.Add(serGY);
            chrDaten.Series.Add(serGZ);
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
            for(int i = 4; i < 7 ; i++){
                chrDaten.Series[i].Points.Clear();
                foreach (double y in mKommu.recData.Where(x => x.code==i).Select<Kommunikator.armSetting,Int32>(x => x.value))
                    chrDaten.Series[i].Points.AddY(y);
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
            lsbDatenAuswahl.Items.Add(new DatenAuswahlElement { code = 0, name = "test" });
        }
        public class DatenAuswahlElement
        {
            public byte code { get; set; }
            public string name { get; set; }
            public override string ToString(){
                return code + "  " + name;
            }
        }
    }
}
