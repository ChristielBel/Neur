using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace _34_Belikova_project.NeurNet
{
    abstract class Layer
    {
        protected string name_Layer;
        string pathDirWeights;
        string pathFileWeights;
        protected int numofneurons;
        protected int numofprevneurouns;
        protected const double learningrate = 0.05d;
        protected const double momentum = 0.2d;
        protected double[,] lastdeltaweights;
        protected Neuron[] neurons;

        //Свойства
        public Neuron[] Neurons { get => neurons; set => neurons = value; }

        public double[] Data
        {
            set
            {
                for(int i = 0; i < Neurons.Length; i++)
                {
                    neurons[i].Inputs = value;
                    neurons[i].Activator(Neurons[i].Inputs, Neurons[i].Weights);
                }
            }
        }

        //Конструктор
        protected Layer(int non, int nopn, NeuronType nt, string nm_Layer)//non - кол-во нейронов, nopn - кол-во нейронов предыдущего слоя, nt - тип нейрона, nm_Layer - наименование слоя 
        {
            numofneurons = non;
            numofprevneurouns = nopn;
            neurons = new Neuron[non];
            name_Layer = nm_Layer;
            pathDirWeights = AppDomain.CurrentDomain.BaseDirectory + "memory\\";
            pathFileWeights = pathDirWeights + name_Layer + "_memory.csv";

            double[,] Weights;

            if (File.Exists(pathFileWeights))
                Weights = WeightInitialize(MemoryMode.GET, pathFileWeights);
            else
            {
                Directory.CreateDirectory(pathDirWeights);
                File.Create(pathFileWeights).Close();
                Weights = WeightInitialize(MemoryMode.INIT, pathFileWeights);
            }

            lastdeltaweights = new double[non, nopn + 1];

            for(int i = 0; i < non; i++)
            {
                double[] tmp_weights = new double[nopn + 1];
                for(int j = 0; j < nopn; j++)
                {
                    tmp_weights[j] = Weights[i, j];
                }
                neurons[i] = new Neuron(tmp_weights, nt);
            }
        }

        //метод для работы с массивом синаптических весов слоя
        public double[,] WeightInitialize(MemoryMode mm, string path)
        {
            char[] delim = new char[] { ';', ' ' };
            string tmpStr;
            string[] tmpStrWeights;
            double[,] weigths = new double[numofneurons, numofprevneurouns + 1];

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
                            weigths[i, j] = double.Parse(memory_elemet[j].Replace(',', '.'),System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    break;

                case MemoryMode.SET:
                    tmpStrWeights = new string[numofneurons];

                    for (int i = 0; i < numofneurons; i++)
                    {
                        tmpStr = Neurons[i].Weights[0].ToString();
                        for (int j = 1; j < numofprevneurouns + 1; j++)
                        {
                            tmpStr += delim[0] + Neurons[i].Weights[j].ToString();
                        }
                        tmpStrWeights[i] = tmpStr;
                    }
                    File.WriteAllLines(pathFileWeights, tmpStrWeights);
                    break;

                case MemoryMode.INIT:
                    Random rand = new Random();

                    tmpStrWeights = new string[numofneurons];

                    // инициализация весов:
                    // 1. веса инициализируются случайными величинами
                    // 2. мат ожидание всех весов нейрона должно равняться 0
                    // 3. среднее квадратическое значение должно равняться 1

                    // Инициализация весов для логистической функции (сигмоиды) с Xavier инициализацией
                    // Формула: w_i ~ N(0, 2 / (n_in + n_out)), где n_in — количество входных нейронов, n_out — количество выходных нейронов

                    double variance = 2.0 / (numofprevneurouns + numofneurons);  // Вычисляем дисперсию
                    double scale = Math.Sqrt(variance);

                    for (int i = 0; i < numofneurons; i++)
                    {
                        double sum = 0;
                        for (int j = 0; j < numofprevneurouns + 1; j++) 
                        {
                            weigths[i, j] = (rand.NextDouble() * 2 - 1) * scale;
                            sum += weigths[i, j];
                        }

                        // Нормализуем веса, если нужно
                        double mean = sum / (numofprevneurouns + 1);
                        sum = 0;

                        // Вычисляем среднее квадратическое отклонение
                        for (int j = 0; j < numofprevneurouns + 1; j++)
                            sum += Math.Pow(weigths[i, j] - mean, 2);

                        double std = Math.Sqrt(sum / (numofprevneurouns + 1));

                        // Нормализуем веса для поддержания нужной дисперсии
                        for (int j = 0; j < numofprevneurouns + 1; j++)
                            weigths[i, j] = (weigths[i, j] - mean) / std;

                        tmpStr = weigths[i, 0].ToString();
                        for (int j = 1; j < numofprevneurouns + 1; j++)
                        {
                            tmpStr += delim[0] + weigths[i, j].ToString();
                        }
                        tmpStrWeights[i] = tmpStr;

                    }
                    File.WriteAllLines(pathFileWeights, tmpStrWeights);
                    break;
            }
            return weigths;
        }

        //для прямых проходов
        abstract public void Recognize(NetWork net, Layer nextLayer);
        //для обратных
        abstract public double[] BackwardPass(double[] stuff);

    }
}
