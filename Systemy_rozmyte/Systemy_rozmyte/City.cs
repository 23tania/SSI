using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//KLASA ODPOWIADAJĄCA ZA POSZCZEGÓLNE MIASTA
namespace Systemy_rozmyte
{
    class City
    {
        double naslonecznienie;
        double skazenie;
        string nazwa;

        public double Naslonecznienie
        {
            get { return naslonecznienie; }
            set { naslonecznienie = value; }
        }

        public double Skazenie
        {
            get { return skazenie; }
            set { skazenie = value; }
        }

        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; }
        }

        public City(double naslonecznienie, double skazenie, string nazwa)
        {
            this.nazwa = nazwa;
            this.naslonecznienie = naslonecznienie;
            this.skazenie = skazenie;
        }
    }
}
