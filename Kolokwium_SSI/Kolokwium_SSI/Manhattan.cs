using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolokwium_SSI
{
    class Manhattan
    {
        //Obliczanie metryki Manhattan dla każdego elementu
        public static double[] MetrykaManhattan(double[][] data, double[] obiekt)
        {
            double sum = 0.0;
            double[] result = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                sum = Math.Abs(data[i][0] - obiekt[0]) + Math.Abs(data[i][1] - obiekt[1]) +
                    Math.Abs(data[i][2] - obiekt[2]) + Math.Abs(data[i][3] - obiekt[3]) +
                    Math.Abs(data[i][4] - obiekt[4]) + Math.Abs(data[i][5] - obiekt[5]) +
                    Math.Abs(data[i][6] - obiekt[6]) + Math.Abs(data[i][7] - obiekt[7]) +
                    Math.Abs(data[i][8] - obiekt[8]) + Math.Abs(data[i][9] - obiekt[9]);

                result[i] = sum;
            }

            return result;
        }

        //Tworzenie słownika z najbliższymi sąsiadami:
        //key = indeks sąsiada, value = metryka Manhattan
        public static Dictionary<int, double> Sasiedzi(int k, double[] result)
        {
            Dictionary<int, double> neighbours = new Dictionary<int, double> { };

            for (int j = 0; j < k; j++)
            {
                neighbours.Add(j, result[j]);
            }

            //Szukanie elementów o najmniejszej metryce Manhattan
            for (int i = k; i < result.Length; i++)
            {
                double max = neighbours.Max(val => val.Value);

                if (max > result[i])
                {
                    var value = neighbours.First(val => val.Value == max);
                    neighbours.Remove(value.Key);
                    neighbours.Add(i, result[i]);
                }
            }

            return neighbours;

        }

        //Znalezienie ilości pierścieni, które występuje najczęściej wśród sąsiadów
        public static void Vote(Dictionary<int, double> neighbours, double[][] data)
        {
            //Tworzymy słownik gdzie: klucz = ilość pierścieni, 
            //wartość = ilość sąsiadów, którzy mają tą ilość pierścieni
        
            Dictionary<int, int> results = new Dictionary<int, int>();

            foreach (var sasiad in neighbours)
            {
                //Sprawdzanie czy w wynikach znajduje się ten sąsiad
                if (!(results.ContainsKey((int)(data[sasiad.Key][10]))))
                {
                    //Pierwsze wystąpienie
                    results.Add((int)data[sasiad.Key][10], 1);
                }
                else
                {
                    //Kolejne wystąpienie
                    results[(int)data[sasiad.Key][10]] += 1;
                }
            }

            //Gdy występuje 2 albo więcej razy ta sama największa ilość pierścieni
            //to zwracany jest ostatni wynik
            int max = 0;
            int key = 0;

            foreach (var wynik in results)
            {
                if (wynik.Value > max)
                {
                    max = wynik.Value;
                    key = wynik.Key;
                }
            }

            Console.WriteLine("\nObiektowi została przypisana ilość pierścieni: " + key + "\nWystąpiła " 
                + max + " razy wśród sąsiadów.");
        }

        
    }
}
