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

        public void Activator(double[] i, double[] w)
        {
            double sum = w[0];
            for (int m = 0; m < i.Length; m++)
                sum += i[m] * w[m + 1];
            switch (_type)
            {
                case NeuronType.Hidden:
                    _output = LogisticAF(sum);
                    _derivative = LogisticAF_Derivativator(sum);
                    break;
                case NeuronType.Output:
                    _output = Exp(sum);
                    break;
            }

        }

        private double LogisticAF(double sum)
        {
            // Логистическая функция активации
            return 1.0 / (1.0 + Exp(-sum));
        }

        private double LogisticAF_Derivativator(double sum)
        {
            double logistic = LogisticAF(sum);
            return logistic * (1.0 - logistic);
        }

    }
}
