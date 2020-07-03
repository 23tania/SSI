using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//KLASA ODPOWIADAJĄCA ZA PROGRAM
namespace Systemy_rozmyte
{
    class Program
    {
        //FUNKCJE PRZYNALEZNOSCI
        static double fTrojkatna(double a, double b, double c, double x)
        {
            if (x <= a) return 0;
            if ((a < x) && (x <= b)) return (x - a) / (b - a);
            if ((b < x) && (x <= c)) return (c - x) / (c - b);
            if (x > c) return 0;
            return 0;
        }

        static double fTrapezoidowa(double a, double b, double c, double d, double x)
        {
            if (x <= a) return 0;
            if (x > a && x <= b) return (x - a) / (b - a);
            if (x > b && x <= c) return 1;
            if (x > c && x <= d) return (d - x) / (d - c);
            if (x > d) return 0;
            return 0;
        }

        //IMPLEMENTACJA NORM
        static double min(double a, double b)
        {
            if (a < b) return a;
            else return b;
        }

        static double prod(double a, double b)
        {
            return a * b;
        }

        //LICZENIE JAKOŚCI ŻYCIA W MIEŚCIE
        public static double jakoscZycia(City miasto)
        {
            //IMPLEMENTACJA REGUŁ
            List<double> skazenieRule = new List<double>();
            skazenieRule.Add(fTrapezoidowa(0, 0, 0.2, 0.4, miasto.Skazenie)); //małe skażenie
            skazenieRule.Add(fTrojkatna(0.2, 0.5, 0.8, miasto.Skazenie)); //srednie skażenie
            skazenieRule.Add(fTrapezoidowa(0.6, 0.8, 1, 1, miasto.Skazenie)); //duże skażenie

            List<double> naslonecznienieRule = new List<double>();
            naslonecznienieRule.Add(fTrapezoidowa(0, 0, 0.2, 0.4, miasto.Naslonecznienie)); //małe nasłonecznienie
            naslonecznienieRule.Add(fTrojkatna(0.2, 0.5, 0.8, miasto.Naslonecznienie)); //srednie nasłonecznienie
            naslonecznienieRule.Add(fTrapezoidowa(0.6, 0.8, 1, 1, miasto.Naslonecznienie)); //duże nasłonecznienie

            //IMPLEMENTACJA WNIOSKOWANIA
            List<double> results = new List<double>();

            for (int i = 0; i < skazenieRule.Count; i++)
            {
                for (int j = 0; j < naslonecznienieRule.Count; j++)
                {
                    results.Add(min(skazenieRule[i], naslonecznienieRule[j]));
                }
            }

            List<double> resultOfRules = new List<double>();
            resultOfRules.Add(0.6); //małe skażenie i małe słońce 
            resultOfRules.Add(0.9); //małe skażenie i średnie słońce
            resultOfRules.Add(1.0); //małe skażenie i dużo słońca

            resultOfRules.Add(0.3); //średnie skażenie i mało słońca
            resultOfRules.Add(0.5); //średnie skażenie i średnie słońce
            resultOfRules.Add(0.7); //średnie skażenie i dużo słońca

            resultOfRules.Add(0.1); //duże skażenie i mało słońca
            resultOfRules.Add(0.2); //duże skażenie i średnie słońce
            resultOfRules.Add(0.4); //duże skażenie i duże słońce


            //IMPLEMENTACJA WYOSTRZENIA
            double decision = 0;
            for (int i = 0; i < resultOfRules.Count; i++)
            {
                decision += results[i] * resultOfRules[i];
            }

            double tmp = 0;
            for (int i = 0; i < results.Count; i++)
            {
                tmp += results[i];
            }

            double result = decision / tmp;
            return result;
        }

        public static void Main(string[] args)
        {
            List<City> cities = new List<City>();
            cities.Add(new City(0.6, 0.3, "Warszawa"));
            cities.Add(new City(1.0, 0.1, "Kraków"));
            cities.Add(new City(0.9, 0.9, "Gdańsk"));
            cities.Add(new City(0.8, 0.7, "Wrocław"));
            cities.Add(new City(0.3, 0.1, "Katowice"));
            cities.Add(new City(0.7, 0.6, "Poznań"));
            cities.Add(new City(0.3, 0.1, "Gliwice"));

            for (int i = 0; i < cities.Count; i++)
            {
                Console.WriteLine($"Dla miasta {cities[i].Nazwa} jakość życia wynosi: {jakoscZycia(cities[i])}");
            }

            Console.Read();
        }
    }
}
