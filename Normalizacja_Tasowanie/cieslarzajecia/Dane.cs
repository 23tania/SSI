using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

//PROGRAM

namespace cieslarzajecia
{
    class Dane
    {
        private double[][] POBIERZ(string sciezka)
        {
            string[] lines = File.ReadAllLines(sciezka);
            double[][] data = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                data[i] = new double[tmp.Length + 2];

                for (int j = 0; j < tmp.Length - 1; j++)
                {
                    data[i][j] = Convert.ToDouble(tmp[j].Replace('.', ','));
                }

                if (tmp[4] == "Iris-setosa")
                {
                    data[i][4] = 1;
                    data[i][5] = 0;
                    data[i][6] = 0;
                }
                else if (tmp[4] == "Iris-versicolor")
                {
                    data[i][4] = 0;
                    data[i][5] = 1;
                    data[i][6] = 0;
                }
                else if (tmp[4] == "Iris-virginica")
                {
                    data[i][4] = 0;
                    data[i][5] = 0;
                    data[i][6] = 1;
                }

            }
            return data;
        }

        private double[][] TASUJ(double[][] data)
        {
            Random rand = new Random();
            int n = 4;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < (n - 1); j++)
                {
                    int r = j + rand.Next(n - j);
                    double t = data[i][r];
                    data[i][r] = data[i][j];
                    data[i][j] = t;
                }
            }
            return data;
        }

        private double[][] NORMALIZUJ(double[][] data)
        {
            //minmax
            double nmin = 0;
            double nmax = 1;

            for (int i = 0; i < 4; i++)
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

                //minmax wzór
                for (int k = 0; k < data.Length; k++)
                {
                    data[k][i] = ((data[k][i] - min) * (nmax - nmin)) / (max - min) + nmin;
                }
            }
            return data;
        }

        private double[][] MEAN(double[][] data)
        {
            double avg = 0.0;
            int count = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    avg += data[j][i];
                    count++;
                }
            }
            avg = avg / count;

            for (int i = 0; i < 4; i++)
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
                for (int k = 0; k < data.Length; k++)
                    data[k][i] = (data[k][i] - avg) / (max - min);
            }
            return data;
        }

        private double[][] STANDARYZACJA(double[][] data)
        {
            double avg = 0.0;
            int count = 0;
            double odchylenie = 0.0;

            for (int i=0; i<4; i++)
            {
                for (int j=0; j<data.Length; j++)
                {
                    avg += data[j][i];
                    count++;
                }
            }
            avg = avg / count;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < data.Length; j++)
                    odchylenie += ((data[j][i] - avg) * (data[j][i] - avg));

            odchylenie = odchylenie / count;
            odchylenie = Math.Sqrt(odchylenie);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < data.Length; j++)
                    data[j][i] = (data[j][i] - avg) / odchylenie;

            return data;
        }

        public static void Main(string[] args)
        {
            Dane obiekt = new Dane();
            double[][] data = obiekt.POBIERZ("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\cieslarzajecia\\iris.txt");

            //for (int i = 0; i < data.Length; i++)
            //{
            //    for (int j = 0; j < data[i].Length; j++)
            //    {
            //        Console.Write(obiekt.TASUJ(data)[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    Console.Write(obiekt.NORMALIZUJ(data)[i][j] + " ");
                    //Console.Write(obiekt.STANDARYZACJA(data)[i][j] + " ");
                    //Console.Write(obiekt.MEAN(data)[i][j] + " ");
                }
                Console.WriteLine();
            }

            Console.Read();

        }
    }
}
