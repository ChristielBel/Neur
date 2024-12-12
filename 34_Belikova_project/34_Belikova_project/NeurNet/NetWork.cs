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
        private double[] e_error_avr;

        public double[] E_error_avr
        {
            get => e_error_avr;
            set => e_error_avr = value;
        }

        public NetWork()
        {

        }

        public void Train(NetWork net)
        {
            int epoches = 100; // кол-во эпох обучения(кол-во прогонов программы)
            net.input_layer = new InputLayer(NetworkMode.Train); //инициализация входного слоя
            double tmpSumError;// временная переменная суммы ошибок
            double[] errors;//вектор сигнала ошибки
            double[] temp_gsums1;//вектор градиента 1 скрытого слоя
            double[] temp_gsums2;//вектор градиента 2 скрытого слоя

            e_error_avr = new double[epoches];

            for(int k = 0; k < epoches; k++)
            {
                e_error_avr[k] = 0;
                net.input_layer.Shuffling_Array_Rows(net.input_layer.Trainset);
                for(int i = 0; i < net.input_layer.Trainset.GetLength(0); i++)
                {
                    double[] tmpTrain = new double[15];
                    for (int j = 0; j < tmpTrain.Length; j++)
                        tmpTrain[j] = net.input_layer.Trainset[i, j + 1];

                    ForwardPass(net, tmpTrain);

                    tmpSumError = 0;
                    errors = new double[net.fact.Length];
                    for(int x = 0; x < errors.Length; x++)
                    {
                        if (x == net.input_layer.Trainset[i, 0])
                            errors[x] = 1.0 - net.fact[x];
                        else
                            errors[x] = -net.fact[x];

                        tmpSumError += errors[x] * errors[x] / 2;
                    }
                    e_error_avr[k] += tmpSumError / errors.Length;


                    temp_gsums2 = net.output_layer.BackwardPass(errors);
                    temp_gsums1 = net.hidden_layer2.BackwardPass(temp_gsums2);
                    net.hidden_layer1.BackwardPass(temp_gsums1);
                }
                e_error_avr[k] /= net.input_layer.Trainset.GetLength(0);
            }

            net.input_layer = null;

            net.hidden_layer1.WeightInitialize(MemoryMode.SET);
            net.hidden_layer2.WeightInitialize(MemoryMode.SET);
            net.output_layer.WeightInitialize(MemoryMode.SET);
        }

        public void Test(NetWork net)
        {
            int epoches = 3; // кол-во эпох обучения(кол-во прогонов программы)
            net.input_layer = new InputLayer(NetworkMode.Test); //инициализация входного слоя
            double tmpSumError;// временная переменная суммы ошибок
            double[] errors;//вектор сигнала ошибки

            e_error_avr = new double[epoches];

            for (int k = 0; k < epoches; k++)
            {
                e_error_avr[k] = 0;
                net.input_layer.Shuffling_Array_Rows(net.input_layer.Testset);
                for (int i = 0; i < net.input_layer.Testset.GetLength(0); i++)
                {
                    double[] tmpTest = new double[15];
                    for (int j = 0; j < tmpTest.Length; j++)
                        tmpTest[j] = net.input_layer.Testset[i, j + 1];

                    ForwardPass(net, tmpTest);

                    tmpSumError = 0;
                    errors = new double[net.fact.Length];
                    for (int x = 0; x < errors.Length; x++)
                    {
                        if (x == net.input_layer.Testset[i, 0])
                            errors[x] = 1.0 - net.fact[x];
                        else
                            errors[x] = -net.fact[x];

                        tmpSumError += errors[x] * errors[x] / 2;
                    }
                    e_error_avr[k] += tmpSumError / errors.Length;
                }
                e_error_avr[k] /= net.input_layer.Testset.GetLength(0);
            }
            net.input_layer = null;
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