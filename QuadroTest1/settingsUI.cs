using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuadroTest1
{
    class settingsUI : Panel
    {
        Label lblDatenValue;
        Label lblDatenCode;
        Button btnDatenSend;
        TextBox txbmValue;
        TextBox txbmCode;

        private Kommunikator mKommunikator;
        private bool mValOkay = false;
        private bool mCodeOkay = false;
        private byte mLastValidCode = 0;
        private int  mLastValidVal = 0;
        public settingsUI(Kommunikator pKommunikator)
        {
            this.lblDatenValue = new System.Windows.Forms.Label();
            this.lblDatenCode = new System.Windows.Forms.Label();
            this.btnDatenSend = new System.Windows.Forms.Button();
            this.txbmValue = new System.Windows.Forms.TextBox();
            this.txbmCode = new System.Windows.Forms.TextBox();

            this.lblDatenCode.AutoSize = true;
            this.lblDatenCode.Location = new System.Drawing.Point(0, 0);
            this.lblDatenCode.Name = "lblDatenCode";
            this.lblDatenCode.Size = new System.Drawing.Size(32, 15);
            this.lblDatenCode.TabIndex = 1;
            this.lblDatenCode.Text = "Code";

            this.lblDatenValue.AutoSize = true;
            this.lblDatenValue.Location = new System.Drawing.Point(100, 0);
            this.lblDatenValue.Name = "lblDatenValue";
            this.lblDatenValue.Size = new System.Drawing.Size(32, 15);
            this.lblDatenValue.TabIndex = 3;
            this.lblDatenValue.Text = "Value";

            this.txbmCode.Location = new System.Drawing.Point(40, 0);
            this.txbmCode.Name = "txbmCode";
            this.txbmCode.Size = new System.Drawing.Size(40, 20);
            this.txbmCode.TabIndex = 2;
            this.txbmCode.TextChanged += new EventHandler(txbmCode_TextChanged);

            this.txbmValue.Location = new System.Drawing.Point(140, 0);
            this.txbmValue.Name = "txbmValue";
            this.txbmValue.Size = new System.Drawing.Size(40, 20);
            this.txbmValue.TabIndex = 4;
            this.txbmValue.TextChanged += new EventHandler(txbmValue_TextChanged);

            this.btnDatenSend.Location = new System.Drawing.Point(190, 0);
            this.btnDatenSend.Name = "btnDatenSend";
            this.btnDatenSend.Size = new System.Drawing.Size(40, 23);
            this.btnDatenSend.TabIndex = 5;
            this.btnDatenSend.Text = "Senden";
            this.btnDatenSend.UseVisualStyleBackColor = true;
            this.btnDatenSend.Click += new System.EventHandler(this.btnDatenSend_Click);

            this.Dock = System.Windows.Forms.DockStyle.Top;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(250, 25);
            this.Name = "pnlToSet";
            this.TabIndex = 0;

            this.Controls.Add(this.txbmCode);
            this.Controls.Add(this.txbmValue);
            this.Controls.Add(this.lblDatenCode);
            this.Controls.Add(this.lblDatenValue);
            this.Controls.Add(this.btnDatenSend);

            mKommunikator = pKommunikator;
        }

        void txbmValue_TextChanged(object sender, EventArgs e)
        {
            short tempShort;
            if (mValOkay = short.TryParse(txbmValue.Text, out tempShort))
            {
                mLastValidVal = tempShort;
                txbmValue.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                txbmValue.BackColor = System.Drawing.Color.LightPink;
            }
        }

        void txbmCode_TextChanged(object sender, EventArgs e)
        {
            byte tempByte;
            if (mCodeOkay = byte.TryParse(txbmCode.Text, out tempByte))
            {
                mLastValidCode = tempByte;
                txbmCode.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                txbmCode.BackColor = System.Drawing.Color.LightPink;
            }

        }

        public void setKommunikator(Kommunikator pKommunikator)
        {
            mKommunikator = pKommunikator;
        }

        void btnDatenSend_Click(object sender, EventArgs e)
        {
            if (mCodeOkay && mValOkay)
            {
                mKommunikator.sendSetting(mLastValidCode, mLastValidVal);
            }
        }
    }
}
