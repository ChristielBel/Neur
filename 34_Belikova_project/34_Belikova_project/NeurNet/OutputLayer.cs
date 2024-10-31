using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _34_Belikova_project.NeurNet
{
    class OutputLayer : Layer
    {
        public OutputLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type)
        {

        }

        public override void Recognize(NetWork net, Layer nextLayer)
        {
            double e_sum = 0;

            for (int i = 0; i < Neurons.Length; i++)
                e_sum += Neurons[i].Output;

            for (int i = 0; i < Neurons.Length; i++)
                net.fact[i] = Neurons[i].Output / e_sum;
        }

        public override double[] BackwardPass(double[] stuff)
        {
            double[] gr_sum = new double[numofprevneurouns + 1];

            //код для обучения нейронной сети

            return gr_sum;
        }
    }
}
