using System;
using System.IO;
using System.Windows.Forms;

namespace _34_Belikova_project.NeurNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        string pathFileWeights;
        protected int numofneurons;
        protected int numofprevneurouns;
        protected const double learningrate = 0.005d;
        protected const double momentum = 0.05d;
        protected double[,] lastdeltaweights;
        Neuron[] _neurons;

        //Свойства
        public Neuron[] Neurons { get => _neurons; set => _neurons = value; }

        public double[] Data
        {
            set
            {
                for(int i = 0; i < Neurons.Length; i++)
                {
                    Neurons[i].Inputs = value;
                    Neurons[i].Activator(Neurons[i].Inputs, Neurons[i].Weights);
                }
            }
        }

        //Конструктор
        protected Layer(int non, int nopn, NeuronType nt, string nm_Layer)//non - кол-во нейронов, nopn - кол-во нейронов предыдущего слоя, nt - тип нейрона, nm_Layer - наименование слоя 
        {
            numofneurons = non;
            numofprevneurouns = nopn;
            Neurons = new Neuron[non];
            name_Layer = nm_Layer;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            double[,] Weights;

            if (File.Exists(pathFileWeights))
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights);
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }
        }

        //метод для работы с массивом синаптических весов слоя
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weights = new double[numofneurons, numofprevneurouns + 1];

            switch (mm)
            {
                case MemoryMode.GET:
                    tmpStrWeights = File.ReadAllLines(path);
                    string[] memory_elemet;
                    for(int i = 0; i < numofneurons; i++)
                    {
                        memory_elemet = tmpStrWeights[i].Split(delim);
                        for(int j = 0; j < numofprevneurouns + 1; j++)
                        {
                            weights[i, j] = double.Parse(memory_elemet[j].Replace(',', '.'),System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;
                case MemoryMode.SET:
                    break;
                case MemoryMode.INIT:
                    break;
            }
            return weights;
        }
    }
}
