using System;
using System.IO;

namespace _34_Belikova_project.NeurNet
{
    class InputLayer
    {
        //поля
        private Random random = new Random();

        private double[,] trainset = new double[100, 16]; //100 изображений, 15 пикселей + желаемый отклик
        private double[,] testset = new double[10, 16];

        //свойства
        public double[,] Trainset { get => trainset; }

        public double[,] Testset { get => testset; }

        //конструктор
        public InputLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpStr;
            string[] tmpArrStr;
            double[] tmpArr;

            switch (nm)
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.text");
                    for (int i = 0; i < tmpArrStr.Length; i++)
                    {
                        tmpStr = tmpArrStr[i].Split();
                        tmpArr = new double[tmpStr.Length];
                        for (int j = 0; j < tmpArrStr.Length; j++)
                        {
                            tmpArr[j] = double.Parse(tmpStr[j], System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
                    //метод перетасовки Фишера-Йетса
                    Shuffling_Array_Rows(trainset);
                    break;
                case NetworkMode.Test:
                    tmpArrStr = File.ReadAllLines(path + "test.text");
                    //ДОПИСАТЬ СЧИТЫВАНИЕ ПО АНАЛОГИИ С TRAIN (ВЫШЕ)
                    break;
                case NetworkMode.Recogn:
                    break;

            } 
        }
    
        public void Shuffling_Array_Rows(double[,] arr)
        {
            int j;
            Random random = new Random();
            double[] temp = new double[arr.GetLength(1)];

            for(int n = arr.GetLength(0) - 1; n >= 1; n--)
            {
                j = random.Next(n + 1);

                for(int i = 0; i < arr.GetLength(1); i++)
                {
                    temp[i] = arr[n, i];
                }

                for(int i = 0; i < arr.GetLength(1); i++)
                {
                    arr[n, i] = arr[j, i];
                    arr[j, i] = temp[i];
                }
            }
        }
    
    }
}
