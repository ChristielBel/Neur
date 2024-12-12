using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace _34_Belikova_project
{
    public partial class Form1 : Form
    {
        private double[] inputPixels;

        private NeurNet.NetWork net;

   
        public Form1()
        {
            InitializeComponent();

            inputPixels = new double[15];
            net = new NeurNet.NetWork();
        }


        private void ChangeState(Button b, int index)
        {
            if (b.BackColor == Color.White)
            {
                b.BackColor = Color.Black;
                inputPixels[index] = 1;
            }
            else if (b.BackColor == Color.Black)
            {
                b.BackColor = Color.White;
                inputPixels[index] = 0;
            }
        }


        private void SaveTrain(decimal vale, double[] input)
        {
            string pathDir; // каталог с обучающими данными
            string nameFileTrain; // имя файла обучающей выборки

            pathDir = AppDomain.CurrentDomain.BaseDirectory;
            nameFileTrain = pathDir + "train.txt";

            string[] tmpStr = new string[1];
            tmpStr[0] = vale.ToString();
            for (int i = 0; i < 15; i++)
            {
                tmpStr[0] += input[i].ToString();
            }

            if (File.Exists(nameFileTrain))
            {
                File.AppendAllLines(nameFileTrain, tmpStr);
            }

        }

        private void SaveTest(decimal vale, double[] input)
        {
            string pathDir;
            string nameFileTest;

            pathDir = AppDomain.CurrentDomain.BaseDirectory;
            nameFileTest = pathDir + "test.txt";

            string[] tmpStr = new string[1];
            tmpStr[0] = vale.ToString();
            for (int i = 0; i < 15; i++)
            {
                tmpStr[0] += input[i].ToString();
            }

            if (File.Exists(nameFileTest))
            {
                File.AppendAllLines(nameFileTest, tmpStr);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeState(button1, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeState(button2, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeState(button3, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeState(button4, 3);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ChangeState(button5, 4);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ChangeState(button6, 5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeState(button7, 6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeState(button8, 7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeState(button9, 8);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ChangeState(button10, 9);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            ChangeState(button11, 10);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChangeState(button12, 11);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ChangeState(button13, 12);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ChangeState(button14, 13);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            ChangeState(button15, 14);
        }

        //распознать
        private void button16_Click(object sender, EventArgs e)
        {
            net.ForwardPass(net, inputPixels);
            labelAnswer.Text = net.fact.ToList().IndexOf(net.fact.Max()).ToString();
            labelProbability.Text = (100 * net.fact.Max()).ToString("0.00") + "%";
        }

        private void buttonSaveTrainSample_Click(object sender, EventArgs e)
        {
            SaveTrain(numericUpDownTrue.Value, inputPixels);
        }

        private void buttonSaveTestSample_Click(object sender, EventArgs e)
        {
            SaveTest(numericUpDownTrue.Value, inputPixels);
        }

        // обучить
        private void buttonTrain_Click(object sender, EventArgs e)
        {
            net.Train(net);
            for(int i = 0; i < net.E_error_avr.Length; i++)
            {
                chartEavr.Series[0].Points.AddY(net.E_error_avr[i]);
            }
            MessageBox.Show("Обучение успешно завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //тестировать
        private void buttonTest_Click(object sender, EventArgs e)
        {
            net.Test(net);
            for(int i = 0; i < net.E_error_avr.Length; i++)
            {
                chartEavr.Series[0].Points.AddY(net.E_error_avr[i]);
            }
            MessageBox.Show("Тестирование успешно завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}