namespace QuadroTest1
{
    partial class rohdaten
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rohdaten));
            this.dgvRecData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbFilter1 = new System.Windows.Forms.TextBox();
            this.pnlCont1 = new System.Windows.Forms.Panel();
            this.btnRefresh1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlCont1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRecData
            // 
            this.dgvRecData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecData.Location = new System.Drawing.Point(3, 3);
            this.dgvRecData.Name = "dgvRecData";
            this.dgvRecData.Size = new System.Drawing.Size(699, 319);
            this.dgvRecData.TabIndex = 0;
            this.dgvRecData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecData_CellContentClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.dgvRecData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlCont1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 362);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // txbFilter1
            // 
            this.txbFilter1.Location = new System.Drawing.Point(3, 3);
            this.txbFilter1.Name = "txbFilter1";
            this.txbFilter1.Size = new System.Drawing.Size(100, 20);
            this.txbFilter1.TabIndex = 1;
            this.txbFilter1.TextChanged += new System.EventHandler(this.txbFilter1_TextChanged);
            // 
            // pnlCont1
            // 
            this.pnlCont1.Controls.Add(this.btnRefresh1);
            this.pnlCont1.Controls.Add(this.txbFilter1);
            this.pnlCont1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCont1.Location = new System.Drawing.Point(3, 328);
            this.pnlCont1.Name = "pnlCont1";
            this.pnlCont1.Size = new System.Drawing.Size(699, 31);
            this.pnlCont1.TabIndex = 2;
            // 
            // btnRefresh1
            // 
            this.btnRefresh1.Location = new System.Drawing.Point(109, 5);
            this.btnRefresh1.Name = "btnRefresh1";
            this.btnRefresh1.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh1.TabIndex = 2;
            this.btnRefresh1.Text = "Neue Werte";
            this.btnRefresh1.UseVisualStyleBackColor = true;
            this.btnRefresh1.Click += new System.EventHandler(this.btnRefresh1_Click);
            // 
            // rohdaten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 362);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "rohdaten";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Rohdaten";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlCont1.ResumeLayout(false);
            this.pnlCont1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRecData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txbFilter1;
        private System.Windows.Forms.Panel pnlCont1;
        private System.Windows.Forms.Button btnRefresh1;
    }
}