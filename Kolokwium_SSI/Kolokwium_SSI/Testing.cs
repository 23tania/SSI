using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kolokwium_SSI
{
    class Testing
    {
        public static int numberOfGoodValues = 0;

        //Klasyfikowanie danych do wieku
        static public void RingClassification(Network network, double[] inputs, double expectedAge)
        {
            //Wypisanie danych z oryginalną il pierścieni
            network.PushInputValues(inputs);
            Console.Write("Dane: ");

            for (int i = 0; i < inputs.Length; i++)
            {
                Console.Write(Math.Round(inputs[i], 4).ToString() + " ");
            }

            Console.Write(expectedAge);
            Console.WriteLine();
            Console.Write("Ilość pierścieni: ");

            double sumOfNeurons = 0.0;
            List<double> outputs = network.GetOutput();

            foreach (var i in outputs)
            {
                //Suma neuronów w outputach
                sumOfNeurons += Math.Abs(i);
            }

            //Odwrotna normalizacja
            int max = 1;
            int min = 0;
            int rings = 0;

            rings = Convert.ToInt32(Math.Floor(((sumOfNeurons - min) *
                (Data.maxRings - Data.minRings)) / (max - min) + Data.minRings));

            //Wypisanie otrzymanych ilości pierścieni
            Console.Write(rings);
            Console.WriteLine();

            //Sprawdzanie zgadzających się wyników
            if (rings == expectedAge)
            {
                numberOfGoodValues++;
            }
            Console.WriteLine();

        }

        static void Main(string[] args)
        {
            //ZADANIE 2
            Console.WriteLine("------------------ZADANIE 2------------------");

            double[][] firstData = Data.POBIERZ("abalone.data");
            double[] X = new double[] { 1, 0, 0, 0.425, 0.3, 0.078, 0.3515,
                0.1475, 0.0775, 0.12 };
            int k = 18; //ilość najbliższych sąsiadów

            Console.Write("Wartości obiektu: ");
            for (int i = 0; i < X.Length; i++)
            {
                Console.Write(X[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Szukamy k=" + k + " najbliższych sąsiadów tego obiektu.");
            Console.WriteLine();

            //Liczenie ze wzoru na metrykę
            double[] result = Manhattan.MetrykaManhattan(firstData, X);

            //Stworzenie słownika sąsiadów
            Dictionary<int, double> neighbours = Manhattan.Sasiedzi(k, result);

            //Wypisanie sąsiadów
            for (int i = 0; i < firstData.Length; i++)
            {
                foreach (var sasiad in neighbours)
                {
                    if (sasiad.Key == i)
                    {
                        foreach (var element in firstData[i])
                        {
                            Console.Write(element + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            //Przypisanie ilości pierścieni obiektowi
            Manhattan.Vote(neighbours, firstData);
            Console.WriteLine();

            //ZADANIE 3
            //TWORZENIE SIECI I TESTOWANIE PROGRAMU

            double[][] data = Data.POBIERZ("abalone.data");

            //Tablica z numerami indeksów danych
            double[] rowNumbers = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                rowNumbers[i] = i;
            }

            //Tasowanie indeksów
            Data.TASUJ(rowNumbers);

            //Stworzenie przetasowanych danych
            double[][] shuffledData = new double[data.Length][];

            for (int i = 0; i < rowNumbers.Length; i++)
            {
                shuffledData[i] = data[(int)rowNumbers[i]];
            }

            //Stworzenie tablicy z ilością pierścieni
            double[] rings = new double[data.Length];
            
            for (int i=0; i < rowNumbers.Length; i++)
            {
                rings[i] = shuffledData[i][10];
            }

            //Normalizacja
            Data.NORMALIZUJ(shuffledData);

            //Podział danych na trenowane i do testowania
            int lenTrainingData = (int)(0.7 * shuffledData.Length);
            int lenTestingData = shuffledData.Length - lenTrainingData;

            double[][] expectedValues = new double[lenTrainingData][];
            double[][] trainingData = new double[lenTrainingData][];

            double[] expectedData = new double[lenTestingData];
            double[][] testingData = new double[lenTestingData][];

            //Trenowanie
            for (int i = 0; i < lenTrainingData; i++)
            {
                expectedValues[i] = new double[1];
                trainingData[i] = new double[10];

                //Dodawanie ilości pierścieni do expectedValues
                expectedValues[i][0] = shuffledData[i][10];

                //Dodawanie danych, które posłużą jako input sieci
                for (int j = 0; j < 10; j++)
                {
                    trainingData[i][j] = shuffledData[i][j];
                }
            }

            //Testowanie
            for (int i = 0; i < lenTestingData; i++)
            {
                testingData[i] = new double[10];

                //Dodanie do expectedData ilości pierścieni o indeksach
                //danych do testowania
                expectedData[i] = rings[i + lenTrainingData];

                for (int j = 0; j < 10; j++)
                {
                    testingData[i][j] = shuffledData[i + lenTrainingData][j];
                }
            }

            //Tworzenie nowej sieci neuronowej
            int hiddenLayersCount = 5;
            int hiddenNeuronsCount = 22;
            int inputNeuronsCount = 10;
            int outputNeuronsCount = 1;
            Network network = new Network(hiddenLayersCount, inputNeuronsCount, outputNeuronsCount, hiddenNeuronsCount);

            Console.WriteLine("------------------ZADANIE 3------------------");
            Console.WriteLine("Tworzenie sieci neuronowej o:");
            Console.WriteLine("- ilości neuronów wejściowych: " + inputNeuronsCount);
            Console.WriteLine("- ilości neuronów wyjściowych: " + outputNeuronsCount);
            Console.WriteLine("- ilości warstw ukrytych: " + hiddenLayersCount);
            Console.WriteLine("- ilości neuronów w warstwach ukrytych: " + hiddenNeuronsCount);
            Console.WriteLine();
            Console.WriteLine("Wagi sczytano z pliku: " + Synapse.path);
            Console.WriteLine();

            network.PushDesiredValues(expectedValues);
            network.Training(trainingData);

            //Klasyfikacja do ilości pierścieni
            for (int i = 0; i < testingData.Length; i++)
                RingClassification(network, testingData[i], expectedData[i]);

            Console.WriteLine("Ilość poprawnie sklasyfikowanych próbek: " + numberOfGoodValues);
            Console.WriteLine($"Skuteczność klasyfikatora: " +
                $"{numberOfGoodValues * 100 / lenTestingData} %");

            Console.WriteLine();
            Console.WriteLine("Wagi zapisano do pliku: " + Network.path);
            Console.WriteLine();


            //ZADANIE 1s
            //Console.WriteLine("------------------ZADANIE 1------------------");
            //Console.WriteLine("Dane zostały przetasowane i znormalizowane.");
            //Console.WriteLine("Wypisanie z płcią: 001 = M, 010 = F, 100 = I");
            //for (int i = 0; i < shuffledData.Length; i++)
            //{
            //    for (int j = 0; j < shuffledData[i].Length; j++)
            //    {
            //        Console.Write(" " + shuffledData[i][j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            Console.ReadLine();

        }
    }
}
