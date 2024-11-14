using System;

namespace _34_Belikova_project.NeurNet
{
    class NetWork
    {
        private InputLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(70, 15,NeuronType.Hidden,nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(34, 70, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 34, NeuronType.Output, nameof(output_layer));

        public double[] fact = new double[10];

        //среднее значение энергии ошибки
        private double e_error_avr;

        public double E_error_avr
        {
            get => e_error_avr;
            set => e_error_avr = value;
        }

        public NetWork()
        {

        }

        public void Train(NetWork net)
        {
            int epoches = 70; // кол-во эпох обучения(кол-во прогонов программы)
            net.input_layer = new InputLayer(NetworkMode.Train); //инициализация входного слоя
            double tmpSumError;// временная переменная суммы ошибок
            double[] errors;//вектор сигнала ошибки
            double[] temp_gsums1;//вектор градиента 1 скрытого слоя
            double[] temp_gsums2;//вектор градиента 2 скрытого слоя


        }

        //прямой проход сети
        public void ForwardPass(NetWork net, double[] netInput)
        {
            net.hidden_layer1.Data = netInput;
            net.hidden_layer1.Recognize(null, net.hidden_layer2);
            net.hidden_layer2.Recognize(null, net.output_layer);
            net.output_layer.Recognize(net, null);
        }
    }
}