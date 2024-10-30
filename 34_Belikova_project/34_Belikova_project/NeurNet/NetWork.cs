using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Belikova_project.NeurNet
{
    class NetWork
    {
        private Layer[] _layers;

        public NetWork(int[] layerStructure)
        {
            _layers = new Layer[layerStructure.Length];
            InitializeNetwork(layerStructure);
        }

        private void InitializeNetwork(int[] layerStructure)
        {
            for (int i = 0; i < layerStructure.Length; i++)
            {
                int numNeurons = layerStructure[i];
                int numPrevNeurons = (i == 0) ? 0 : layerStructure[i - 1];
                string layerName = $"Layer_{i + 1}";

                if (i == 0)
                    _layers[i] = new InputLayer(numNeurons, layerName);
                else if (i == layerStructure.Length - 1)
                    _layers[i] = new OutputLayer(numNeurons, numPrevNeurons, NeuronType.Output, layerName);
                else
                    _layers[i] = new HiddenLayer(numNeurons, numPrevNeurons, NeuronType.Hidden, layerName);
            }
        }

        //передает входные данные в InputLayer
        public void SetInputData(double[] inputData)
        {
            if (_layers.Length > 0 && _layers[0] is InputLayer inputLayer)
                inputLayer.Data = inputData;
        }
    }
}


/*int[] layerStructure = { 15, 70, 34, 10 };
NetWork neuralNetwork = new NetWork(layerStructure);*/