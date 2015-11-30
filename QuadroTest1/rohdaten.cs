using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuadroTest1
{
    public partial class rohdaten : Form
    {
        private List<Kommunikator.armSetting> recDataCopy;
        private Queue<Kommunikator.armSetting> recDataPointer;
        private BindingSource bs = new BindingSource();
        public rohdaten(Queue<Kommunikator.armSetting> precDataCopy)
        {
            InitializeComponent();
            recDataPointer = precDataCopy;
            recDataCopy = new List<Kommunikator.armSetting>(precDataCopy.Select(x => x.Clone()).Cast<Kommunikator.armSetting>());
            bs.DataSource = recDataCopy;
            dgvRecData.DataSource = bs;
        }

        private void dgvRecData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txbFilter1_TextChanged(object sender, EventArgs e)
        {
            byte toFilter;
            if(byte.TryParse(txbFilter1.Text,out toFilter))
            {
                bs.DataSource = recDataCopy.Where(x => x.code == toFilter);
            }
        }

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            recDataCopy = new List<Kommunikator.armSetting>(recDataPointer.Select(x => x.Clone()).Cast<Kommunikator.armSetting>());
            bs.DataSource = recDataCopy;
            dgvRecData.DataSource = bs;
        }
    }
}
