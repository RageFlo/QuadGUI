namespace QuadroTest1
{
    partial class formMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.mainTabLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanConInfo = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.CheckBox();
            this.btnRohdaten = new System.Windows.Forms.Button();
            this.lblMessagesRec = new System.Windows.Forms.Label();
            this.lblMessagesSent = new System.Windows.Forms.Label();
            this.lblPingCounter = new System.Windows.Forms.Label();
            this.lblConState = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblBaudrate = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.chrDaten = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlDatenAuswahl = new System.Windows.Forms.Panel();
            this.txbmScale3 = new System.Windows.Forms.MaskedTextBox();
            this.txbmScale2 = new System.Windows.Forms.MaskedTextBox();
            this.lblDatenScale = new System.Windows.Forms.Label();
            this.txbmScale1 = new System.Windows.Forms.MaskedTextBox();
            this.lblDatenName = new System.Windows.Forms.Label();
            this.lblDatenCode = new System.Windows.Forms.Label();
            this.btnDatenremove = new System.Windows.Forms.Button();
            this.btnDatenAdd = new System.Windows.Forms.Button();
            this.txbmName = new System.Windows.Forms.MaskedTextBox();
            this.txbmCode = new System.Windows.Forms.MaskedTextBox();
            this.lsbDatenAuswahl = new System.Windows.Forms.CheckedListBox();
            this.pnlToSet = new System.Windows.Forms.Panel();
            this.txtbAllData = new System.Windows.Forms.TextBox();
            this.txbLoadScript = new System.Windows.Forms.TextBox();
            this.btnloadScript = new System.Windows.Forms.Button();
            this.mainTabLayout.SuspendLayout();
            this.mainPanConInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrDaten)).BeginInit();
            this.pnlDatenAuswahl.SuspendLayout();
            this.pnlToSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabLayout
            // 
            this.mainTabLayout.AutoSize = true;
            this.mainTabLayout.ColumnCount = 2;
            this.mainTabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTabLayout.Controls.Add(this.mainPanConInfo, 0, 2);
            this.mainTabLayout.Controls.Add(this.chrDaten, 0, 1);
            this.mainTabLayout.Controls.Add(this.pnlDatenAuswahl, 1, 0);
            this.mainTabLayout.Controls.Add(this.pnlToSet, 1, 1);
            this.mainTabLayout.Controls.Add(this.txtbAllData, 0, 0);
            this.mainTabLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTabLayout.Name = "mainTabLayout";
            this.mainTabLayout.RowCount = 3;
            this.mainTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTabLayout.Size = new System.Drawing.Size(984, 611);
            this.mainTabLayout.TabIndex = 0;
            // 
            // mainPanConInfo
            // 
            this.mainPanConInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainTabLayout.SetColumnSpan(this.mainPanConInfo, 2);
            this.mainPanConInfo.Controls.Add(this.btnSave);
            this.mainPanConInfo.Controls.Add(this.btnPause);
            this.mainPanConInfo.Controls.Add(this.btnRohdaten);
            this.mainPanConInfo.Controls.Add(this.lblMessagesRec);
            this.mainPanConInfo.Controls.Add(this.lblMessagesSent);
            this.mainPanConInfo.Controls.Add(this.lblPingCounter);
            this.mainPanConInfo.Controls.Add(this.lblConState);
            this.mainPanConInfo.Controls.Add(this.btnConnect);
            this.mainPanConInfo.Controls.Add(this.lblBaudrate);
            this.mainPanConInfo.Controls.Add(this.txtBaudRate);
            this.mainPanConInfo.Controls.Add(this.lblComPort);
            this.mainPanConInfo.Controls.Add(this.cmbComPorts);
            this.mainPanConInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainPanConInfo.Location = new System.Drawing.Point(3, 584);
            this.mainPanConInfo.Name = "mainPanConInfo";
            this.mainPanConInfo.Size = new System.Drawing.Size(986, 24);
            this.mainPanConInfo.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(604, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 21);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPause
            // 
            this.btnPause.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPause.Location = new System.Drawing.Point(528, 0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(70, 21);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "Pause";
            this.btnPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.CheckedChanged += new System.EventHandler(this.btnPause_CheckedChanged);
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRohdaten
            // 
            this.btnRohdaten.Location = new System.Drawing.Point(452, 0);
            this.btnRohdaten.Name = "btnRohdaten";
            this.btnRohdaten.Size = new System.Drawing.Size(70, 21);
            this.btnRohdaten.TabIndex = 9;
            this.btnRohdaten.Text = "Rohdaten";
            this.btnRohdaten.UseVisualStyleBackColor = true;
            this.btnRohdaten.Click += new System.EventHandler(this.btnRohdaten_Click);
            // 
            // lblMessagesRec
            // 
            this.lblMessagesRec.AutoSize = true;
            this.lblMessagesRec.Location = new System.Drawing.Point(687, 4);
            this.lblMessagesRec.Name = "lblMessagesRec";
            this.lblMessagesRec.Size = new System.Drawing.Size(58, 13);
            this.lblMessagesRec.TabIndex = 8;
            this.lblMessagesRec.Text = "Erhalten: 0";
            // 
            // lblMessagesSent
            // 
            this.lblMessagesSent.AutoSize = true;
            this.lblMessagesSent.Location = new System.Drawing.Point(787, 4);
            this.lblMessagesSent.Name = "lblMessagesSent";
            this.lblMessagesSent.Size = new System.Drawing.Size(65, 13);
            this.lblMessagesSent.TabIndex = 7;
            this.lblMessagesSent.Text = "Gesendet: 0";
            // 
            // lblPingCounter
            // 
            this.lblPingCounter.AutoSize = true;
            this.lblPingCounter.Location = new System.Drawing.Point(897, 4);
            this.lblPingCounter.Name = "lblPingCounter";
            this.lblPingCounter.Size = new System.Drawing.Size(45, 13);
            this.lblPingCounter.TabIndex = 6;
            this.lblPingCounter.Text = "Pings: 0";
            // 
            // lblConState
            // 
            this.lblConState.AutoSize = true;
            this.lblConState.BackColor = System.Drawing.Color.Gold;
            this.lblConState.Location = new System.Drawing.Point(377, 4);
            this.lblConState.Name = "lblConState";
            this.lblConState.Size = new System.Drawing.Size(48, 13);
            this.lblConState.TabIndex = 5;
            this.lblConState.Text = "Getrennt";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(300, 0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(70, 21);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Verbinden";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.AutoSize = true;
            this.lblBaudrate.Location = new System.Drawing.Point(247, 4);
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(50, 13);
            this.lblBaudrate.TabIndex = 3;
            this.lblBaudrate.Text = "Baudrate";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(160, 1);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(80, 20);
            this.txtBaudRate.TabIndex = 2;
            this.txtBaudRate.Text = "57600";
            this.txtBaudRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(107, 4);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(47, 13);
            this.lblComPort.TabIndex = 1;
            this.lblComPort.Text = "ComPort";
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(0, 0);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(100, 21);
            this.cmbComPorts.TabIndex = 0;
            // 
            // chrDaten
            // 
            chartArea1.Name = "ChartArea1";
            this.chrDaten.ChartAreas.Add(chartArea1);
            this.chrDaten.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chrDaten.Legends.Add(legend1);
            this.chrDaten.Location = new System.Drawing.Point(3, 164);
            this.chrDaten.Name = "chrDaten";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrDaten.Series.Add(series1);
            this.chrDaten.Size = new System.Drawing.Size(682, 414);
            this.chrDaten.TabIndex = 1;
            this.chrDaten.Text = "Daten";
            // 
            // pnlDatenAuswahl
            // 
            this.pnlDatenAuswahl.Controls.Add(this.txbmScale3);
            this.pnlDatenAuswahl.Controls.Add(this.txbmScale2);
            this.pnlDatenAuswahl.Controls.Add(this.lblDatenScale);
            this.pnlDatenAuswahl.Controls.Add(this.txbmScale1);
            this.pnlDatenAuswahl.Controls.Add(this.lblDatenName);
            this.pnlDatenAuswahl.Controls.Add(this.lblDatenCode);
            this.pnlDatenAuswahl.Controls.Add(this.btnDatenremove);
            this.pnlDatenAuswahl.Controls.Add(this.btnDatenAdd);
            this.pnlDatenAuswahl.Controls.Add(this.txbmName);
            this.pnlDatenAuswahl.Controls.Add(this.txbmCode);
            this.pnlDatenAuswahl.Controls.Add(this.lsbDatenAuswahl);
            this.pnlDatenAuswahl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatenAuswahl.Location = new System.Drawing.Point(691, 3);
            this.pnlDatenAuswahl.Name = "pnlDatenAuswahl";
            this.pnlDatenAuswahl.Size = new System.Drawing.Size(298, 155);
            this.pnlDatenAuswahl.TabIndex = 3;
            // 
            // txbmScale3
            // 
            this.txbmScale3.Location = new System.Drawing.Point(172, 130);
            this.txbmScale3.Name = "txbmScale3";
            this.txbmScale3.Size = new System.Drawing.Size(28, 20);
            this.txbmScale3.TabIndex = 7;
            this.txbmScale3.Text = "0";
            // 
            // txbmScale2
            // 
            this.txbmScale2.Location = new System.Drawing.Point(138, 130);
            this.txbmScale2.Name = "txbmScale2";
            this.txbmScale2.Size = new System.Drawing.Size(28, 20);
            this.txbmScale2.TabIndex = 6;
            this.txbmScale2.Text = "1";
            // 
            // lblDatenScale
            // 
            this.lblDatenScale.AutoSize = true;
            this.lblDatenScale.Location = new System.Drawing.Point(105, 114);
            this.lblDatenScale.Name = "lblDatenScale";
            this.lblDatenScale.Size = new System.Drawing.Size(129, 13);
            this.lblDatenScale.TabIndex = 10;
            this.lblDatenScale.Text = "Scale1 / Scale2 e Scale3";
            // 
            // txbmScale1
            // 
            this.txbmScale1.Location = new System.Drawing.Point(108, 130);
            this.txbmScale1.Name = "txbmScale1";
            this.txbmScale1.Size = new System.Drawing.Size(28, 20);
            this.txbmScale1.TabIndex = 5;
            this.txbmScale1.Text = "1";
            // 
            // lblDatenName
            // 
            this.lblDatenName.AutoSize = true;
            this.lblDatenName.Location = new System.Drawing.Point(36, 114);
            this.lblDatenName.Name = "lblDatenName";
            this.lblDatenName.Size = new System.Drawing.Size(35, 13);
            this.lblDatenName.TabIndex = 8;
            this.lblDatenName.Text = "Name";
            // 
            // lblDatenCode
            // 
            this.lblDatenCode.AutoSize = true;
            this.lblDatenCode.Location = new System.Drawing.Point(3, 114);
            this.lblDatenCode.Name = "lblDatenCode";
            this.lblDatenCode.Size = new System.Drawing.Size(32, 13);
            this.lblDatenCode.TabIndex = 7;
            this.lblDatenCode.Text = "Code";
            // 
            // btnDatenremove
            // 
            this.btnDatenremove.Location = new System.Drawing.Point(248, 127);
            this.btnDatenremove.Name = "btnDatenremove";
            this.btnDatenremove.Size = new System.Drawing.Size(39, 23);
            this.btnDatenremove.TabIndex = 9;
            this.btnDatenremove.TabStop = false;
            this.btnDatenremove.Text = "Entfernen";
            this.btnDatenremove.UseVisualStyleBackColor = true;
            this.btnDatenremove.Click += new System.EventHandler(this.btnDatenremove_Click);
            // 
            // btnDatenAdd
            // 
            this.btnDatenAdd.Location = new System.Drawing.Point(203, 127);
            this.btnDatenAdd.Name = "btnDatenAdd";
            this.btnDatenAdd.Size = new System.Drawing.Size(45, 23);
            this.btnDatenAdd.TabIndex = 8;
            this.btnDatenAdd.Text = "Hinzufügen";
            this.btnDatenAdd.UseVisualStyleBackColor = true;
            this.btnDatenAdd.Click += new System.EventHandler(this.btnDatenAdd_Click);
            // 
            // txbmName
            // 
            this.txbmName.Location = new System.Drawing.Point(39, 130);
            this.txbmName.Name = "txbmName";
            this.txbmName.Size = new System.Drawing.Size(63, 20);
            this.txbmName.TabIndex = 4;
            // 
            // txbmCode
            // 
            this.txbmCode.Location = new System.Drawing.Point(4, 130);
            this.txbmCode.Mask = "000";
            this.txbmCode.Name = "txbmCode";
            this.txbmCode.Size = new System.Drawing.Size(28, 20);
            this.txbmCode.TabIndex = 3;
            // 
            // lsbDatenAuswahl
            // 
            this.lsbDatenAuswahl.FormattingEnabled = true;
            this.lsbDatenAuswahl.Location = new System.Drawing.Point(3, 3);
            this.lsbDatenAuswahl.Name = "lsbDatenAuswahl";
            this.lsbDatenAuswahl.Size = new System.Drawing.Size(284, 109);
            this.lsbDatenAuswahl.TabIndex = 2;
            this.lsbDatenAuswahl.SelectedIndexChanged += new System.EventHandler(this.lsbDatenAuswahl_SelectedIndexChanged);
            // 
            // pnlToSet
            // 
            this.pnlToSet.Controls.Add(this.btnloadScript);
            this.pnlToSet.Controls.Add(this.txbLoadScript);
            this.pnlToSet.Location = new System.Drawing.Point(691, 164);
            this.pnlToSet.Name = "pnlToSet";
            this.pnlToSet.Size = new System.Drawing.Size(287, 414);
            this.pnlToSet.TabIndex = 4;
            // 
            // txtbAllData
            // 
            this.txtbAllData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbAllData.Location = new System.Drawing.Point(3, 3);
            this.txtbAllData.MaxLength = 1000;
            this.txtbAllData.Multiline = true;
            this.txtbAllData.Name = "txtbAllData";
            this.txtbAllData.ReadOnly = true;
            this.txtbAllData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbAllData.Size = new System.Drawing.Size(682, 155);
            this.txtbAllData.TabIndex = 5;
            // 
            // txbLoadScript
            // 
            this.txbLoadScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txbLoadScript.Location = new System.Drawing.Point(0, 127);
            this.txbLoadScript.MaxLength = 1000;
            this.txbLoadScript.Multiline = true;
            this.txbLoadScript.Name = "txbLoadScript";
            this.txbLoadScript.ReadOnly = true;
            this.txbLoadScript.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbLoadScript.Size = new System.Drawing.Size(287, 287);
            this.txbLoadScript.TabIndex = 6;
            // 
            // btnloadScript
            // 
            this.btnloadScript.Location = new System.Drawing.Point(6, 98);
            this.btnloadScript.Name = "btnloadScript";
            this.btnloadScript.Size = new System.Drawing.Size(98, 23);
            this.btnloadScript.TabIndex = 7;
            this.btnloadScript.Text = "Lade Skript";
            this.btnloadScript.UseVisualStyleBackColor = true;
            this.btnloadScript.Click += new System.EventHandler(this.btnloadScript_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.mainTabLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 650);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "formMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Quadro Steuerung und Info";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainTabLayout.ResumeLayout(false);
            this.mainTabLayout.PerformLayout();
            this.mainPanConInfo.ResumeLayout(false);
            this.mainPanConInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chrDaten)).EndInit();
            this.pnlDatenAuswahl.ResumeLayout(false);
            this.pnlDatenAuswahl.PerformLayout();
            this.pnlToSet.ResumeLayout(false);
            this.pnlToSet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTabLayout;
        private System.Windows.Forms.Panel mainPanConInfo;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblBaudrate;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Label lblPingCounter;
        private System.Windows.Forms.Label lblConState;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblMessagesSent;
        private System.Windows.Forms.Label lblMessagesRec;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrDaten;
        private System.Windows.Forms.Panel pnlDatenAuswahl;
        private System.Windows.Forms.Label lblDatenCode;
        private System.Windows.Forms.Button btnDatenremove;
        private System.Windows.Forms.Button btnDatenAdd;
        private System.Windows.Forms.MaskedTextBox txbmName;
        private System.Windows.Forms.MaskedTextBox txbmCode;
        private System.Windows.Forms.CheckedListBox lsbDatenAuswahl;
        private System.Windows.Forms.Label lblDatenName;
        private System.Windows.Forms.Label lblDatenScale;
        private System.Windows.Forms.MaskedTextBox txbmScale1;
        private System.Windows.Forms.CheckBox btnPause;
        private System.Windows.Forms.Button btnRohdaten;
        private System.Windows.Forms.Panel pnlToSet;
        private System.Windows.Forms.TextBox txtbAllData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MaskedTextBox txbmScale3;
        private System.Windows.Forms.MaskedTextBox txbmScale2;
        private System.Windows.Forms.TextBox txbLoadScript;
        private System.Windows.Forms.Button btnloadScript;
    }
}

