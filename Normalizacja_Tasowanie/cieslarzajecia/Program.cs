using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;


namespace cieslarzajecia
{
    class Irys
    {
        //tasowanie zbioru polega na losowym jego wymieszaniu
        public double[][] shuffle(double[][] data)
        {
            Random rand = new Random();
            int n = 4; //ilość danych w wierszu

            for (int i = 0; i < data.Length; i++) //iterowanie po kolejnych wierszach
            {
                for (int j = 0; j < (n - 1); j++) //iterowanie po 4 pierwszych el w wierszu
                {
                    int r = j + rand.Next(n - j);
                    //n-j to górne ograniczenie przedziału
                    double t = data[i][r];
                    data[i][r] = data[i][j];
                    data[i][j] = t;
                }
            }
            return data;
        }

        public double[][] normalizacja(double[][] data)
        {
            /*
            Przykład normalizacji.
            Przedział początkowy: <min,max> = <1,8> (np elementy {2,4,1,6,8})
            <nmin,nmax> = <0,1>
            x to wartosc z oryginalnego zbioru, x' to nowa wartosc z przedzialu <0,1>
            x = 1
            x' = 1-1/8-1 (1-0)+0 (wzor na normalizacje)

            Robi sie to po to aby liczby byly zapisane w malym przedziale bo wtedy nie tworza sie ogromne liczby.
            A wiec normalizacja to zamiana wartosci na wartosci w przedziale <nmin,nmax> (tutaj <0,1>).
            */

            double nmin = 0;
            double nmax = 1;

            for (int i = 0; i < 4; i++) //iterowanie po 4 pierwszych elementach w każdym rzędzie
            {
                double max = data[0][i];
                double min = data[0][i];

                //znalezenie el max i min w każdym rzędzie
                for (int j = 0; j < data.Length; j++)
                {
                    if (max < data[j][i]) max = data[j][i];
                    else if (min > data[j][i]) min = data[j][i];
                }

                //normalizacja wzór
                for (int j = 0; j < data.Length; j++)
                {
                    data[j][i] = ((data[j][i] - min) * (nmax - nmin)) / (max - min) + nmin;
                }
            }
            return data;
        }

        public void skalaSzarosci (string sciezka)
        {
            //Przetwarzanie obrazów
            //Obraz ma wysokość, szerokość i głębię - każdy z tych elementów to macierze definiujące obraz.
            //Np. w RGB te 3 elementy to R,G i B - każda z nich ma własną wysokość i szerokość, a głębia to 3 (el R,G i B).
            //Należy dodać: using System.Drawing
            //w odwolaniach dodajemy System.Drawing - prawy przycisk na Odwołania i dodaj odwolanie i tam znalezc System.Drawing

            Bitmap image = new Bitmap($@"{sciezka}");
            //Bitmap image = new Bitmap(@"C:\Users\zosta\source\repos\sem4\cieslarzajecia\zdjecie.jpg");
            for (int i=0; i<image.Width; i++)
            {
                for (int j=0; j<image.Height; j++)
                {
                    Color pxl = image.GetPixel(i, j);
                    //Console.WriteLine("(" + i + "," + j + "=" + pxl.R); //drukowanie czerwieni
                    int avg = (pxl.R + pxl.G + pxl.B) / 3; //utworzenie barwy czarno białej
                    Color a = Color.FromArgb(avg, avg, avg);
                    image.SetPixel(i, j, a); //wlozenie nowej barwy do piksela
                }
            }
            image.Save(@"C:\Users\zosta\source\repos\sem4\cieslarzajecia\nowe.jpg");
        }
    }

    class Program
    {
        //static void Main(string[] args)
        //{
        //    //jest w sumie 7 znakow w kazdym wierszu
        //    //4 liczby a potem string ktory trzeba przekonwertowac na wartosc 001,010,100 itd
        //    string[] lines = File.ReadAllLines(@"C:\Users\zosta\source\repos\sem4\ssi\cieslarzajecia\iris.txt");
        //    double[][] data = new double[lines.Length][];

        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        string[] tmp = lines[i].Split(',');
        //        data[i] = new double[tmp.Length + 2]; //aby bylo 001 itd

        //        for (int j = 0; j < tmp.Length - 1; j++) //-1 bo iterujemy bez ostatniego Stringa
        //        {
        //            data[i][j] = Convert.ToDouble(tmp[j].Replace('.', ',')); //w polskiej wersji konwersja kropki na przecinek
        //        }

        //        if (tmp[4] == "Iris-setosa")
        //        {
        //            data[i][4] = 1;
        //            data[i][5] = 0;
        //            data[i][6] = 0;
        //        }
        //        else if (tmp[4] == "Iris-versicolor")
        //        {
        //            data[i][4] = 0;
        //            data[i][5] = 1;
        //            data[i][6] = 0;
        //        }
        //        else if (tmp[4] == "Iris-virginica")
        //        {
        //            data[i][4] = 0;
        //            data[i][5] = 0;
        //            data[i][6] = 1;
        //        }

        //    }

        //    Irys irys1 = new Irys();
        //    //Console.WriteLine(irys1.shuffle(data));

        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        for (int j = 0; j < data[i].Length; j++)
        //        {
        //            Console.Write(irys1.normalizacja(data)[i][j] + " ");
        //        }
        //        Console.WriteLine();

        //    }

        //    //irys1.skalaSzarosci("C:\\Users\\zosta\\source\\repos\\sem4\\cieslarzajecia\\zdjecie.jpg");

        //    Console.ReadKey();
        //}
    }
}
