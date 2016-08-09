using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuadroTest1
{
    static class SaveExcel
    {
        class DatenPunkt
        {
            public int xWert { get; set; }
            public double yWert { get; set; }
        }

        class ZeitPunkt
        {
            public int zeit { get; set; }
            public double[] yWerte { get; set; }
        }

        static public int saveInExcel(SeriesCollection pDatenSerien)
        {
            List<List<DatenPunkt>> listOfListOfDatenPunkte = new List<List<DatenPunkt>>();

            List<string> listOfColNames = new List<string>();
            foreach (Series curSerie in pDatenSerien)
            {
                listOfColNames.Add(curSerie.Name);
            }
            int colCount = listOfColNames.Count;
            Dictionary<int, double[]> zeitPunkte = new Dictionary<int, double[]>();
            for (int i = 0; i < colCount; i++)
            {
                foreach (DataPoint curPoint in pDatenSerien[i].Points)
                {
                    if (!zeitPunkte.ContainsKey((int)curPoint.XValue))
                    {
                        zeitPunkte.Add((int)curPoint.XValue, new double[colCount]);
                    }
                    zeitPunkte[(int)curPoint.XValue][i] = curPoint.YValues.ElementAtOrDefault(0);
                }

            }
            zeitPunkte.OrderBy(x => x.Key);
            //List<int> listOfZeiten = listOfListOfDatenPunkte.SelectMany(x => x.Select(z => (int)z.xWert)).Distinct().ToList();
            //List<ZeitPunkt> listOfZeitPunkte = listOfZeiten.Select(x => new ZeitPunkt { zeit = x, yWerte = new double[colCount] }).ToList();
            //for (int i = 0; i < listOfZeitPunkte.Count; i++)
            //{
            //    for (int j = 0; j < colCount; j++)
            //    {
            //        int curZeit = listOfZeitPunkte[i].zeit;
            //        if(curZeit == listOfListOfDatenPunkte[j][j].xWert)
            //            listOfZeitPunkte[i].yWerte[j]
            //    }
            //}
            //int mostDatenPunkte = listOfListOfDatenPunkte.Max(x => x.Count);
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Output.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Data will be exported and you will be notified when it is ready.");
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                    }
                }
                string columnNames = "Zeit;";
                string[] output = new string[zeitPunkte.Count + 1];
                for (int i = 0; i < colCount; i++)
                {
                    columnNames += pDatenSerien[i].Name.ToString() + ";";
                }
                output[0] += columnNames;
                for (int i = 1; (i - 1) < zeitPunkte.Count; i++)
                {
                    int zeit = zeitPunkte.Keys.ElementAt(i-1);
                    output[i] = zeit.ToString() + ";";
                    for (int j = 0; j < colCount; j++)
                    {
                        output[i] += zeitPunkte[zeit][j].ToString() + ";";
                    }
                }
                System.IO.File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);
                MessageBox.Show("Your file was generated and its ready for use.");
            }
            return -1;
        }
    }
}
