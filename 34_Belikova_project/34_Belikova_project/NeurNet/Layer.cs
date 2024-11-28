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
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        for (int i = 0; i < numofneurons; i++)
                        {
                            for (int j = 0; j < numofprevneurouns + 1; j++)
                            {
                                writer.Write(weights[i, j].ToString(System.Globalization.CultureInfo.InvariantCulture) + ";");
                            }
                            writer.WriteLine();
                        }
                    }
                    break;
                case MemoryMode.INIT:
                    double sum = 0;
                    double sumSquared = 0;
                    Random random = new Random();

                    // Шаг 1: Генерация весов от -1 до 1 и рассчет суммы и суммы квадратов для нормализации
                    for (int i = 0; i < numofneurons; i++)
                    {
                        for (int j = 0; j < numofprevneurouns + 1; j++)
                        {
                            weights[i, j] = 2 * random.NextDouble() - 1; // диапазон от -1 до 1
                            sum += weights[i, j];
                            sumSquared += weights[i, j] * weights[i, j];
                        }
                    }

                    // Шаг 2: Расчет среднего и стандартного отклонения
                    double mean = sum / (numofneurons * (numofprevneurouns + 1));
                    double variance = (sumSquared / (numofneurons * (numofprevneurouns + 1))) - (mean * mean);
                    double stdDev = Math.Sqrt(variance);

                    // Шаг 3: Нормализация весов для соблюдения условий
                    for (int i = 0; i < numofneurons; i++)
                    {
                        for (int j = 0; j < numofprevneurouns + 1; j++)
                        {
                            // Корректируем вес, чтобы среднее значение стало 0, а стандартное отклонение — 1
                            weights[i, j] = (weights[i, j] - mean) / stdDev;
                        }
                    }
                    using (StreamWriter writer = new StreamWriter("weights_output.txt"))
                    {
                        for (int i = 0; i < numofneurons; i++)
                        {
                            for (int j = 0; j < numofprevneurouns + 1; j++)
                            {
                                writer.Write(weights[i, j].ToString(System.Globalization.CultureInfo.InvariantCulture) + "\t");
                            }
                            writer.WriteLine(); // Перевод строки после каждого нейрона
                        }
                    }
                    break;
            }
            return weights;
        }

        //для прямых проходов
        abstract public void Recognize(NetWork net, Layer nextLayer);
        //для обратных
        abstract public double[] BackwardPass(double[] stuff);

    }
}
