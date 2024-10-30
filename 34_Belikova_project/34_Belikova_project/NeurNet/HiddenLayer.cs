using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Belikova_project.NeurNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int numNeurons, int numPrevNeurons, NeuronType neuronType, string nameLayer)
            : base(numNeurons, numPrevNeurons, neuronType, nameLayer) { }
    }
}
