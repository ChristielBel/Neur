using static System.Math;

namespace _34_Belikova_project.NeurNet
{
    class Neuron
    {
        private NeuronType _type; //тип нейрона
        private double[] _weights; //его веса
        private double[] _inputs; //его входы
        private double _output; //его выходы
        private double _derivative; //производная функции активации
        //константы для функции активации
        private double a = 0.01; //параметр функции активации (лякириллу)

        public double[] Weights { get => _weights; set => _weights = value; }

        public double[] Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }

        public double Output { get => _output; }

        public double Derivative { get => _derivative; }

        //конструктор
        public Neuron(double[] inputs, double[] weights, NeuronType type)
        {
            _type = type;
            _weights = weights;
            _inputs = inputs;
        }

        public void Activator(double[] i, double[] v)
        {

        }
    }
}
