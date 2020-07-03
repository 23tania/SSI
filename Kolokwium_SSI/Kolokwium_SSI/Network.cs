using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kolokwium_SSI
{
    class Network
    {
        List<Layer> layers;
        double[][] expectedResult;
        double[][] errors;
        static double maxError = 0.03;
        public static string path = "zapisane_wagi.txt";
        StreamWriter sw;

        #region Konstruktor
        public Network(int hiddenLayersCount, int inputNeuronsCount, int outputNeuronsCount,
            int hiddenNeuronsCount)
        {
            layers = new List<Layer>();

            //Dodawanie warstwy input
            CreateFirstLayer(inputNeuronsCount);

            //Dodawanie warstw ukrytych
            for (int i = 0; i < hiddenLayersCount; i++)
            {
                AddNewLayer(new Layer(hiddenNeuronsCount));
            }

            //Dodawanie warstwy output
            AddNewLayer(new Layer(outputNeuronsCount));

            errors = new double[layers.Count][];
            for (int i = 1; i < layers.Count; i++)
            {
                errors[i] = new double[layers[i].neurons.Count];
            }
        }
        #endregion

        #region Tworzenie warstw

        //Tworzenie pierwszej warstwy = inputowej
        public void CreateFirstLayer(int neuronsCount)
        {
            Layer firstLayer = new Layer(neuronsCount);

            foreach (Neuron neuron in firstLayer.neurons)
                neuron.AddInputConnection(0);

            layers.Add(firstLayer);
        }

        //Dodawanie kolejnych warstw
        public void AddNewLayer(Layer newLayer)
        {
            Layer lastLayer = layers[layers.Count - 1];
            lastLayer.ConnectLayers(newLayer);
            layers.Add(newLayer);
        }

        #endregion

        #region Określenie danych wejściowych sieci (input i oczekiwane rezultaty)

        //Określa wartości input sieci
        public void PushInputValues(double[] inputs)
        {
            //Sprawdzenie czy ilość podanych inputów jest równa ilości
            //neuronów inputowych w warstwie wejściowej
            if (inputs.Length != layers[0].neurons.Count)
                throw new Exception("Zła ilość neuronów wejściowych!");

            for (int i = 0; i < inputs.Length; i++)
            {
                layers[0].neurons[i].PushValueOnInput(inputs[i]);
            }
        }

        //Określa oczekiwane rezultaty trenowania sieci
        public void PushDesiredValues(double[][] expectedValues)
        {
            //Sprawdzenie czy ilość oczekiwanych rezultatów jest równa ilości
            //neuronów outputowych w warstwie wyjściowej
            if (expectedValues[0].Length != layers[layers.Count - 1].neurons.Count)
                throw new Exception("Zła ilość oczekiwanych rezultatów!");

            expectedResult = expectedValues;
        }
        #endregion

        #region Trenowanie sieci

        //Trenowanie sieci - obliczanie globalnego błędu
        public void Training(double[][] inputs)
        {
            double globalError = double.MaxValue;
            Console.WriteLine("Nauczanie sieci");

            while (globalError / inputs.Length > maxError)
            {
                globalError = 0;
                var outputs = new List<double>();
                for (int row = 0; row < inputs.Length; row++)
                {
                    PushInputValues(inputs[row]);
                    outputs = GetOutput();
                    UpdateWeights(row, outputs);

                    globalError += CalculateErrorInRow(outputs, row);

                }
            }

            Console.WriteLine("Błąd globalny wynosi: " + globalError / inputs.Length);
            Console.WriteLine();

            //Zapisanie wag do pliku
            SaveWeightsToFile(path);
        }

        //Liczenie błędu w poszczególnym rzędzie
        public double CalculateErrorInRow(List<double> outputs, int rowOfNeurons)
        {
            double errorInRow = 0;

            for (int i = 0; i < outputs.Count; i++)
            {
                errorInRow += Math.Pow(outputs[i] - expectedResult[rowOfNeurons][i], 2);
            }

            return errorInRow;
        }
        #endregion

        #region Wsteczna propagacja

        //Algorytm wstecznej propagacji
        public void BackPropagation(int rowOfNeurons, List<double> outputs)
        {
            //warstwa output
            BackPropOutput(layers.Count - 1, rowOfNeurons, outputs);

            //warstwy ukryte
            for (int k = layers.Count - 2; k > 0; k--)
                BackPropHidden(k);
        }

        //Wsteczna propagacja dla warstwy ukrytej
        public void BackPropHidden(int layerNumber)
        {
            for (int i = 0; i < layers[layerNumber].neurons.Count; i++)
            {
                errors[layerNumber][i] = 0;

                //Iterowanie po każdym neuronie w warstwie kolejnej, czyli output
                for (int j = 0; j < layers[layerNumber + 1].neurons.Count; j++)
                {
                    errors[layerNumber][i] += errors[layerNumber + 1][j] * layers[layerNumber + 1].neurons[j].inputs[i].Weight;
                }
                errors[layerNumber][i] *= Function_Activation.DerivativeBipolar(layers[layerNumber].neurons[i].InputValue);

            }
        }

        //Wsteczna propagacja dla warstwy output
        public void BackPropOutput(int layerNumber, int rowOfNeurons, List<double> outputs)
        {
            //Iterowanie po każdym neuronie w warstwie output
            for (int i = 0; i < layers[layerNumber].neurons.Count; i++)
            {
                //Obliczanie błędu dla każdego neurona
                errors[layerNumber][i] = (expectedResult[rowOfNeurons][i] - outputs[i]) *
                    Function_Activation.DerivativeBipolar(layers[layerNumber].neurons[i].InputValue);
            }
        }

        #endregion

        #region Output
        //Zwraca output sieci neuronowej
        public List<double> GetOutput()
        {
            var result = new List<double>();

            //Obliczanie output dla każdej z warstw
            foreach (Layer layer in layers)
            {
                layer.CalculateLayerOutput();
            }

            //Dodanie do wyniku outputów z warstwy wyjściowej
            foreach (Neuron neuron in layers[layers.Count - 1].neurons)
            {
                result.Add(neuron.OutputValue);
            }

            return result;

        }
        #endregion

        #region Aktualizacja wag
        public void UpdateWeights(int rowOfNeurons, List<double> outputs)
        {
            BackPropagation(rowOfNeurons, outputs);

            for (int k = layers.Count - 1; k > 0; k--)
            {
                for (int i = 0; i < layers[k].neurons.Count; i++)
                {
                    for (int j = 0; j < layers[k - 1].neurons.Count; j++)
                    {
                        layers[k].neurons[i].inputs[j].UpdateWeight(
                            2 * layers[k - 1].neurons[j].OutputValue * errors[k][i]);
                    }
                }
            }
        }
        #endregion

        //Zapisywanie wag do pliku
        private void SaveWeightsToFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            //Dodanie wag z warstwy input
            for (int i=0; i < layers[0].neurons.Count; i++)
            {
                sw = File.AppendText(path);
                sw.WriteLine(layers[0].neurons[i].inputs[0].Weight.ToString());
                sw.Close();
            }

            //Dodanie wag z pozostałych warstw
            for (int k = 1; k < layers.Count; k++)
            {
                for (int i = 0; i < layers[k].neurons.Count; i++)
                {
                    for (int j = 0; j < layers[k - 1].neurons.Count; j++)
                    {
                        sw = File.AppendText(path);
                        sw.WriteLine(layers[k].neurons[i].inputs[j].Weight.ToString());
                        sw.Close();
                    }
                }
            }
        }
    }
}
