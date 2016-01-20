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
    public partial class Rohdaten : Form
    {
        private List<Kommunikator.armRecVal> recDataCopy;
        private Queue<Kommunikator.armRecVal> recDataPointer;
        private BindingSource bs = new BindingSource();
        public Rohdaten(Queue<Kommunikator.armRecVal> precDataCopy)
        {
            InitializeComponent();
            recDataPointer = precDataCopy;
            recDataCopy = new List<Kommunikator.armRecVal>(precDataCopy.Select(x => x.Clone()).Cast<Kommunikator.armRecVal>());
            recDataCopy.Sort();
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
            recDataCopy = new List<Kommunikator.armRecVal>(recDataPointer.Select(x => x.Clone()).Cast<Kommunikator.armRecVal>());
            bs.DataSource = recDataCopy;
            dgvRecData.DataSource = bs;
        }
    }
}
