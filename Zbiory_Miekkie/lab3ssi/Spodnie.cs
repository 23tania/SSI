using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3ssi
{
    class Spodnie
    {
        public void wyborSpodni(string[] wybranyProdukt)
        {
            Console.Write("Klient szuka następujących spodni: ");

            for (int i=0; i<wybranyProdukt.Length; i++)
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
                new string[] { "drogie", "jeans", "klasyczne", "na zamek" },
                new string[] { "tanie", "dresowe", "modern", "na zamek" },
                new string[] { "jeans", "granatowe", "na guziki", "fit" },
                new string[] { "drogie", "jeans", "klasyczne", "na guziki" }
            };

            Console.WriteLine("Dostępne pary w sklepie to:");

            int count = 1;
            foreach (string[] produkty in dostepneProdukty)
            {
                Console.Write("Para numer " + count + ": ");
                for (int i=0; i<produkty.Length; i++)
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

            for (int i=0; i<iloscZgodnych.Length; i++)
            {
                if (iloscZgodnych[i] == max)
                {
                    najlepszeWybory.Add(i);
                }
            }

            Console.WriteLine("\nNajbardziej odpowiada mu para/pary: ");
            for (int i=0; i<dostepneProdukty.Count; i++)
            {
                for (int j=0; j<najlepszeWybory.Count; j++)
                {
                    if (i == najlepszeWybory[j])
                    {
                        Console.WriteLine("Para numer " + (i + 1) );                        
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
