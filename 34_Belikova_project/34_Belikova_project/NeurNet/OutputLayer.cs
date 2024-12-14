﻿using System;
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

        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numofprevneurouns + 1];

            for(int j = 0; j < numofprevneurouns + 1; j++)
            {
                double sum = 0;
                for(int k = 0; k < numofneurons; k++)
                {
                    sum += neurons[k].Weights[j] * errors[k];
                }
                gr_sum[j] = sum;
            }

            for (int i = 0; i < numofneurons; i++)
            {
                for (int n = 0; n < numofprevneurouns + 1; n++)
                {
                    double deltaw;
                    if (n == 0)
                        deltaw = momentum * lastdeltaweights[i, 0] + learningrate * errors[i];
                    else
                        deltaw = momentum * lastdeltaweights[i, n] + learningrate * Neurons[i].Inputs[n - 1] * errors[i];

                    lastdeltaweights[i, n] = deltaw;
                    Neurons[i].Weights[n] += deltaw;
                }
            }

            return gr_sum;
        }
    }
}
