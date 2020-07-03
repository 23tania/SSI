using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kolokwium_SSI
{
    class Synapse
    {
        Neuron fromNeuron;
        Neuron toNeuron;

        static double learningRate = 0.6;
        public static Random random = new Random();

        public double Weight { get; set; }
        public double Output { get; set; }

        public static string path = "wczytane_wagi.txt";
        string[] lines = File.ReadAllLines(path);
        static int index = -1;
        int newIndex;

        //Konstruktory
        //Tworzenie synapsy łącącej neuron fromNeuron z toNeuron o zainicjowanej wadze
        public Synapse(Neuron fromNeuron, Neuron toNeuron)
        {
            this.fromNeuron = fromNeuron;
            this.toNeuron = toNeuron;

            //Z PLIKU
            Weight = AssignWeight();

            //ZAINICJOWANE
            //Weight = InitalizeWeight();
        }


        //Synapsa w warstwie input
        public Synapse(Neuron neuron, double output)
        {
            toNeuron = neuron;
            Output = output;

            //Z PLIKU
            Weight = AssignWeight();

            //ZAINICJOWANE
            //Weight = 1;

        }

        //Zainicjowanie wag poszczególnych synaps
        double InitalizeWeight()
        {
            return random.NextDouble() - 0.5;
        }

        //Zaktualizowanie wagi synapsy
        public void UpdateWeight(double WeightChange)
        {
            Weight += WeightChange * learningRate;
        }

        //Zwraca iloczyn wagi połączenia i wartości output
        public double GetOutputValue()
        {
            if (fromNeuron == null)
            {
                return Output;
            }
            return fromNeuron.OutputValue * Weight;
        }

        //Funkcja pobierająca wagi z pliku
        public double AssignWeight()
        {
            newIndex = ++index;
            double weight = Convert.ToDouble(lines[newIndex]);
            return weight;
        }
    }
}
