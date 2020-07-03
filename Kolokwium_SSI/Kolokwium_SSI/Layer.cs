using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolokwium_SSI
{
    class Layer
    {
        public List<Neuron> neurons;

        //Konstruktory
        public Layer(int neuronsCount)
        {
            neurons = new List<Neuron>();
            for (int i = 0; i < neuronsCount; i++)
            {
                neurons.Add(new Neuron());
            }
        }

        public Layer()
        {
            neurons = new List<Neuron>();
        }

        //Funkcja łącząca warstwy
        public void ConnectLayers(Layer outputLayer)
        {
            foreach (var inputNeuron in neurons)
            {
                foreach (var outputNeuron in outputLayer.neurons)
                {
                    inputNeuron.AddOutputNeuron(outputNeuron);
                }
            }
        }

        //Funkcja licząca output na poszczególnej warstwie
        public void CalculateLayerOutput()
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.GetOutputValue();
            }
        }
    }
}
