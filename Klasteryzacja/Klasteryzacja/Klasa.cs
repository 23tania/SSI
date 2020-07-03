using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Klasteryzacja
{
    class Klasa
    {
        private double[][] POBIERZ(string sciezka)
        {
            string[] lines = File.ReadAllLines(sciezka);
            double[][] data = new double[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tmp = lines[i].Split(',');
                data[i] = new double[tmp.Length];

                for (int j = 0; j < tmp.Length - 1; j++)
                {
                    data[i][j] = Convert.ToDouble(tmp[j].Replace('.', ','));
                }

                //PRZYPISANIE DO KLAS
                if (tmp[4] == "Iris-setosa")
                {
                    data[i][4] = 0;
                }
                else if (tmp[4] == "Iris-versicolor")
                {
                    data[i][4] = 1;
                }
                else if (tmp[4] == "Iris-virginica")
                {
                    data[i][4] = 2;
                }
            }
            return data;
        }

        private double[] metrykaEuklidesa(double[][] data, double[] obiekt)
        {
            double value = 0.0;
            double[] odl = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                value = Math.Sqrt(((obiekt[0] - data[i][0]) * (obiekt[0] - data[i][0])) +
                    ((obiekt[1] - data[i][1]) * (obiekt[1] - data[i][1])) +
                    ((obiekt[2] - data[i][2]) * (obiekt[2] - data[i][2])) +
                    ((obiekt[3] - data[i][3]) * (obiekt[3] - data[i][3])));

                odl[i] = value;
            }

            return odl;
        }

        private Dictionary<int, double> klasteryzacja(int k, double[] odl)
        {
            Dictionary<int, double> sasiedzi = new Dictionary<int, double> { };

            for (int j = 0; j < k; j++)
            {
                sasiedzi.Add(j, odl[j]);
            }

            for (int i = k; i < odl.Length; i++)
            {
                double max = sasiedzi.Max(val => val.Value);

                if (max > odl[i])
                {
                    var value = sasiedzi.First(val => val.Value == max);
                    sasiedzi.Remove(value.Key);
                    sasiedzi.Add(i, odl[i]);
                }
            }

            return sasiedzi;

        }

        private string vote(Dictionary<int, double> sasiedzi)
        {
            int setosa = 0, versicolor = 0, virginica = 0;
            string wybor = "";

            foreach (var neigh in sasiedzi)
            {
                if (znajdzKlase(neigh.Key) == 0) setosa++;
                else if (znajdzKlase(neigh.Key) == 1) versicolor++;
                else if (znajdzKlase(neigh.Key) == 2) virginica++;
            }

            int max = setosa;
            if (versicolor > setosa && versicolor > virginica)
                wybor = "Iris-versicolor";
            else if (virginica > versicolor && virginica > setosa)
                wybor = "Iris-virginica";
            else
                wybor = "Iris-setosa";

            return wybor;
        }

        private int znajdzKlase(int indeks)
        {
            if (indeks >= 0 && indeks <= 49)
            {
                return 0;
            }
            else if (indeks > 49 && indeks <= 99)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static void Main(string[] args)
        {
            Klasa klast = new Klasa();
            double[][] data = klast.POBIERZ("C:\\Users\\zosta\\source\\repos\\Klasteryzacja\\iris.txt");
            double[] X = new double[] { 2.3, 3.3, 1.7, 5.3 };
            int k = 10;
            Dictionary<int, double> sasiedzi = klast.klasteryzacja(k, klast.metrykaEuklidesa(data, X));

            double[] tablicaEuklidesa = klast.metrykaEuklidesa(data, X);

            Console.Write("Wartości przypisywanego obiektu do klasy: ");
            for (int i = 0; i < X.Length; i++)
            {
                Console.Write(X[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Szukamy k=" + k + " najbliższych sąsiadów tego obiektu.");
            Console.WriteLine("Po analizie bazy Iris o to sąsiadujące wartości obiektu:");
            Console.WriteLine();

            for (int i = 0; i < data.Length; i++)
            {
                foreach (var sasiad in sasiedzi)
                {
                    if (sasiad.Key == i)
                    {
                        foreach (var element in data[i])
                        {
                            Console.Write(element + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("gdzie ostatnia cyfra oznacza klasę:" +
                "\n0 = Iris-setosa, \n1 = Iris-versicolor, \n2 = Iris-virginica.");
            Console.WriteLine();

            Console.WriteLine("A o to ich odległości od obiektu oraz klasy, do których należą:");
            Console.WriteLine();
            foreach (var wart in sasiedzi)
            {
                Console.WriteLine(wart.Value + " " + klast.znajdzKlase(wart.Key));

            }
            Console.WriteLine();
            Console.WriteLine("Wybrana klasa dla obiektu to: " + klast.vote(klast.klasteryzacja(k, klast.metrykaEuklidesa(data, X))));
            Console.Read();
        }
    }
}
