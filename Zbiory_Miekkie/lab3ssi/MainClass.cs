using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3ssi
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Spodnie noweSpodnie = new Spodnie();

            string[] klientA = new string[] { "jeans", "modern", "na zamek" };
            string[] klientB = new string[] { "jeans", "klasyczne", "granatowe", "na guziki" };

            noweSpodnie.wyborSpodni(klientA);
            //noweSpodnie.wyborSpodni(klientB);

            Warzywa noweWarzywo = new Warzywa();

            string[] klient2A = new string[] { "świeże", "ostre", "czerwone" };
            string[] klient2B = new string[] { "mrożone", "słodkie", "zielone", "liściaste" };
            string[] klient2C = new string[] { "świeże", "zielone", "czerwone", "słodkie" };

            //noweWarzywo.wyborWarzywa(klient2A);
            noweWarzywo.wyborWarzywa(klient2B);
            //noweWarzywo.wyborWarzywa(klient2C);


            Console.Read();
        }
    }
}
