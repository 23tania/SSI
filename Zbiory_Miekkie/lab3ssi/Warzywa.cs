using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3ssi
{
    class Warzywa
    {

        public void wyborWarzywa(string[] wybranyProdukt)
        {
            Console.Write("Klient szuka następujących warzyw: ");

            for (int i = 0; i < wybranyProdukt.Length; i++)
            {
                if (i == wybranyProdukt.Length - 1)
                {
                    Console.WriteLine(wybranyProdukt[i] + ".");
                }
                else
                {
                    Console.Write(wybranyProdukt[i] + ", ");
                }
            }

            List<string[]> dostepneProdukty = new List<string[]>()
            {
                new string[] { "mrożone", "słodkie", "zielone", "bulwowe" },
                new string[] { "świeże", "ostre", "zielone"},
                new string[] { "świeże", "czerwone", "lokalne", "liściaste" },
                new string[] { "mrożone", "czerwone", "liściaste", "ostre", "lokalne" }
            };

            Console.WriteLine("Dostępne warzywa w sklepie to:");

            int count = 1;
            foreach (string[] produkty in dostepneProdukty)
            {
                Console.Write("Warzywo numer " + count + ": ");
                for (int i = 0; i < produkty.Length; i++)
                {

                    if (i == produkty.Length - 1)
                    {
                        Console.Write(produkty[i] + ".");
                    }
                    else
                    {
                        Console.Write(produkty[i] + ", ");
                    }
                }
                count++;
                Console.WriteLine();
            }

            int[] iloscZgodnych = new int[] { 0, 0, 0, 0 };
            int n = 0;

            foreach (string[] produkty in dostepneProdukty)
            {
                for (int i = 0; i < produkty.Length; i++)
                {
                    for (int j = 0; j < wybranyProdukt.Length; j++)
                    {
                        if (produkty[i] == wybranyProdukt[j])
                        {
                            iloscZgodnych[n]++;
                        }
                    }
                }
                n++;
            }

            int max = iloscZgodnych[0];
            List<int> najlepszeWybory = new List<int>();

            foreach (int numer in iloscZgodnych)
            {
                if (numer > max)
                {
                    max = numer;
                }
            }

            for (int i = 0; i < iloscZgodnych.Length; i++)
            {
                if (iloscZgodnych[i] == max)
                {
                    najlepszeWybory.Add(i);
                }
            }

            Console.WriteLine("\nNajbardziej odpowiada mu warzywo/warzywa: ");
            for (int i = 0; i < dostepneProdukty.Count; i++)
            {
                for (int j = 0; j < najlepszeWybory.Count; j++)
                {
                    if (i == najlepszeWybory[j])
                    {
                        Console.WriteLine("Warzywo numer " + (i + 1));
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
