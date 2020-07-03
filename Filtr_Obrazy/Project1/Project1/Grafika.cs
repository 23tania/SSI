using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

//PROGRAM

    // Należy dodać odwołanie do System.Drawing

namespace Project1
{
    class Grafika
    {
        double[][] macierz;
        private Bitmap btm;
        private Bitmap bit_map;

        private void Macierz(string sciezka)
        {
            btm = new Bitmap(sciezka);
            bit_map = new Bitmap(btm.Width, btm.Height);
        }

        private void Filtr_Sharp(string sciezkaZapisu)
        {
            macierz = new double[3][];

            macierz[0] = new double[] { 0, -2, 0 };
            macierz[1] = new double[] { -2, 11, -2 };
            macierz[2] = new double[] { 0, -2, 0 };

            for (int i = 0; i < btm.Width - macierz.Length; i++)
            {
                for (int j = 0; j < btm.Height - macierz[0].Length; j++)
                {
                    double R = 0, G = 0, B = 0;

                    for (int k = macierz.Length - 1; k > -1; k--)
                    {
                        for (int l = macierz[0].Length - 1; l > -1; l--)
                        {
                            Color pxl = btm.GetPixel(i + macierz.Length - 1 - k, j + macierz[0].Length - 1 - l);
                            R += macierz[k][l] * pxl.R;
                            G += macierz[k][l] * pxl.G;
                            B += macierz[k][l] * pxl.B;
                        }

                    }
                    R = R < 0 ? 0 : R;
                    G = G < 0 ? 0 : G;
                    B = B < 0 ? 0 : B;

                    bit_map.SetPixel(i, j, Color.FromArgb((int)(R > 255 ? 255 : R), (int)(G > 255 ? 255 : G), (int)(B > 255 ? 255 : B)));
                }
            }
            bit_map.Save($@"{sciezkaZapisu}\image_Filtr_Sharp.jpg");
        }

        private void Filtr_Gauss(string sciezkaZapisu)
        {
            macierz = new double[3][];

            macierz[0] = new double[] { 1, 2, 1 };
            macierz[1] = new double[] { 2, 4, 2 };
            macierz[2] = new double[] { 1, 2, 1 };

            for (int i = 0; i < btm.Width - macierz.Length; i++)
            {
                for (int j = 0; j < btm.Height - macierz[0].Length; j++)
                {
                    double R = 0, G = 0, B = 0;

                    for (int k = macierz.Length - 1; k > -1; k--)
                    {
                        for (int l = macierz[0].Length - 1; l > -1; l--)
                        {
                            Color pxl = btm.GetPixel(i + macierz.Length - 1 - k, j + macierz[0].Length - 1 - l);
                            R += macierz[k][l] * pxl.R;
                            G += macierz[k][l] * pxl.G;
                            B += macierz[k][l] * pxl.B;
                        }

                    }
                    R = R < 0 ? 0 : R;
                    G = G < 0 ? 0 : G;
                    B = B < 0 ? 0 : B;

                    bit_map.SetPixel(i, j, Color.FromArgb((int)(R > 255 ? 255 : R), (int)(G > 255 ? 255 : G), (int)(B > 255 ? 255 : B)));
                }
            }
            bit_map.Save($@"{sciezkaZapisu}\image_Filtr_Gauss.jpg");
        }

        private void Filtr_Blur(string sciezkaZapisu)
        {
            macierz = new double[3][];

            macierz[0] = new double[] { 1, 1, 1 };
            macierz[1] = new double[] { 1, 1, 1 };
            macierz[2] = new double[] { 1, 1, 1 };

            for (int i = 0; i < btm.Width - macierz.Length; i++)
            {
                for (int j = 0; j < btm.Height - macierz[0].Length; j++)
                {
                    double R = 0, G = 0, B = 0;

                    for (int k = macierz.Length - 1; k > -1; k--)
                    {
                        for (int l = macierz[0].Length - 1; l > -1; l--)
                        {
                            Color pxl = btm.GetPixel(i + macierz.Length - 1 - k, j + macierz[0].Length - 1 - l);
                            R += macierz[k][l] * pxl.R * 1 / 9;
                            G += macierz[k][l] * pxl.G * 1 / 9;
                            B += macierz[k][l] * pxl.B * 1 / 9;
                        }

                    }
                    R = R < 0 ? 0 : R;
                    G = G < 0 ? 0 : G;
                    B = B < 0 ? 0 : B;

                    bit_map.SetPixel(i, j, Color.FromArgb((int)(R > 255 ? 255 : R), (int)(G > 255 ? 255 : G), (int)(B > 255 ? 255 : B)));
                }
            }
            bit_map.Save($@"{sciezkaZapisu}\image_Filtr_Blur.jpg");
        }

        private void Filtr_KeyPoints(string sciezkaZapisu)
        {
            macierz = new double[3][];

            macierz[0] = new double[] { -1, -1, -1 };
            macierz[1] = new double[] { -1, 8, -1 };
            macierz[2] = new double[] { -1, -1, -1 };

            for (int i = 0; i < btm.Width - macierz.Length; i++)
            {
                for (int j = 0; j < btm.Height - macierz[0].Length; j++)
                {
                    double R = 0, G = 0, B = 0;

                    for (int k = macierz.Length - 1; k > -1; k--)
                    {
                        for (int l = macierz[0].Length - 1; l > -1; l--)
                        {
                            Color pxl = btm.GetPixel(i + macierz.Length - 1 - k, j + macierz[0].Length - 1 - l);
                            R += macierz[k][l] * pxl.R * 1 / 9;
                            G += macierz[k][l] * pxl.G * 1 / 9;
                            B += macierz[k][l] * pxl.B * 1 / 9;
                        }

                    }
                    R = R < 0 ? 0 : R;
                    G = G < 0 ? 0 : G;
                    B = B < 0 ? 0 : B;

                    bit_map.SetPixel(i, j, Color.FromArgb((int)(R > 255 ? 255 : R), (int)(G > 255 ? 255 : G), (int)(B > 255 ? 255 : B)));
                }
            }
            bit_map.Save($@"{sciezkaZapisu}\image_Filtr_KeyPoints.jpg");
        }

        static void Main(string[] args)
        {
            //Grafika graf = new Grafika();
            //graf.Macierz("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\lab2ssi\\Dog.jpg");
            //graf.Filtr_Sharp("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\lab2ssi");
            //graf.Filtr_Blur("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\lab2ssi");
            //graf.Filtr_Gauss("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\lab2ssi");
            //graf.Filtr_KeyPoints("C:\\Users\\zosta\\source\\repos\\sem4\\ssi\\lab2ssi");

            Console.WriteLine(Math.Round(3.2, 1, MidpointRounding.AwayFromZero));
            Console.ReadLine();
        }

    }
}
