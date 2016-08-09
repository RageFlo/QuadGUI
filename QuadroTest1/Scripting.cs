using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuadroTest1
{
    static class Scripting
    {
        static public void runScript()
        {
            string filename = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "QUADROSCRIPTO (*.qds)|*.qds";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("It wasn't possible to read the data from the disk." + ex.Message);
                    }
                }
            }
        }

    }
}
