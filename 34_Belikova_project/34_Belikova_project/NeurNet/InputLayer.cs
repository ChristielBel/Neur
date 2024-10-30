using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Belikova_project.NeurNet
{
    class InputLayer : Layer
    {
        public InputLayer(int numNeurons, string nameLayer) : base(numNeurons, 0, NeuronType.Hidden, nameLayer) { }
    }
}
