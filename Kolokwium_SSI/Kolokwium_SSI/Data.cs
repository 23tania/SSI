using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kolokwium_SSI
{
    class Data
    {
        static double[] rings;
        static public double maxRings;
        static public double minRings;

        //Funkcja pobierająca dane z pliku
        static public double[][] POBIERZ(string sciezka)
        {
            string[] lines = File.ReadAllLines(sciezka);
            double[][] data = new double[lines.Length - 1][];
            rings = new double[lines.Length - 1];

            for (int i = 0; i < lines.Length - 1; i++)
            {
                string[] tmp = lines[i + 1].Split(',');
                data[i] = new double[tmp.Length + 2];

                if (tmp[0] == "M")
                {
                    data[i][0] = 0;
                    data[i][1] = 0;
                    data[i][2] = 1;
                }
                else if (tmp[0] == "F")
                {
                    data[i][0] = 0;
                    data[i][1] = 1;
                    data[i][2] = 0;
                }
                else if (tmp[0] == "I")
                {
                    data[i][0] = 1;
                    data[i][1] = 0;
                    data[i][2] = 0;
                }

                for (int j = 1; j < tmp.Length; j++)
                {
                    data[i][j + 2] = Convert.ToDouble(tmp[j].Replace('.', ','));
                }

                rings[i] = Convert.ToDouble(tmp[tmp.Length - 1]);

            }
            return data;
        }

        //Funkcja tasująca dane
        static public void TASUJ(double[] data)
        {
            Random rand = new Random();
            int n = data.Length;

            for (int i = 0; i < n - 1; i++)
            {
                //Przenosi na randomowy indeks w danych
                int r = i + rand.Next(n - i);
                double tmp = data[r];
                data[r] = data[i];
                data[i] = tmp;

            }
        }

        //Funkcja normalizująca dane MINMAX
        public static double[][] NORMALIZUJ(double[][] data)
        {
            double nmin = 0;
            double nmax = 1;

            //Normalizacja dla danych oprócz ilości pierścieni
            for (int i = 3; i < data[0].Length - 1; i++)
            {
                double max = data[0][i];
                double min = data[0][i];

                for (int j = 0; j < data.Length; j++)
                {
                    if (max < data[j][i])
                        max = data[j][i];
                    else if (min > data[j][i])
                        min = data[j][i];
                }

                //Wzór
                for (int k = 0; k < data.Length; k++)
                {
                    data[k][i] = ((data[k][i] - min) * (nmax - nmin)) / (max - min) + nmin;
                }
            }

            //Normalizacja dla ilości pierścieni
            minRings = double.MaxValue;
            maxRings = double.MinValue;

            //Szukanie minimalnej i maksymalnej ilości pierścieni
            for (int i = 0; i < rings.Length; i++)
            {
                if (maxRings < rings[i])
                {
                    maxRings = rings[i];
                }
                else if (minRings > rings[i])
                {
                    minRings = rings[i];
                }
            }

            //Normalizacja
            for (int i = 0; i < data.Length; i++)
            {
                data[i][10] = ((data[i][10] - minRings) * (nmax - nmin)) / (maxRings - minRings) + nmin;
            }

            return data;
        }

    }
}
