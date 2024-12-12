﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private void SaveTrain(decimal value, double[] input)
        {
            string pathDir; // каталог с обучающими данными
            string nameFileTrain; // имя файла обучающей выборки

            pathDir = AppDomain.CurrentDomain.BaseDirectory;
            nameFileTrain = pathDir + "train.txt";

            // Формируем строку, которая будет записана в файл
            string tmpStr = value.ToString(); // Начинаем с значения
            for (int i = 0; i < 15; i++) // Добавляем все элементы из массива input
            {
                tmpStr += " " + input[i].ToString(); // Разделяем пробелом
            }

            // Проверяем существует ли файл и записываем данные
            if (File.Exists(nameFileTrain))
            {
                using (StreamWriter writer = File.AppendText(nameFileTrain))
                {
                    writer.WriteLine(tmpStr); // Записываем строку
                }
            }
            else
            {
                using (StreamWriter writer = File.CreateText(nameFileTrain))
                {
                    writer.WriteLine(tmpStr); // Записываем строку
                }
            }
        }

        private void SaveTest(decimal value, double[] input)
        {
            string pathDir; // путь к exe
            string nameFileTest; // имя файла

            pathDir = AppDomain.CurrentDomain.BaseDirectory;
            nameFileTest = pathDir + "test.txt"; // Измените имя файла на .txt

            // Формируем строку, которая будет записана в файл
            string tmpStr = value.ToString(); // Начинаем с значения
            for (int i = 0; i < 15; i++) // Добавляем все элементы из массива input
            {
                tmpStr += " " + input[i].ToString(); // Разделяем пробелом
            }

            // Проверяем существует ли файл и записываем данные
            if (File.Exists(nameFileTest))
            {
                using (StreamWriter writer = File.AppendText(nameFileTest))
                {
                    writer.WriteLine(tmpStr); // Записываем строку
                }
            }
            else
            {
                using (StreamWriter writer = File.CreateText(nameFileTest))
                {
                    writer.WriteLine(tmpStr); // Записываем строку
                }
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
            for (int i = 0; i < net.E_error_avr.Length; i++)
            {
                chartEavr.Series[0].Points.AddY(net.E_error_avr[i]);
            }
            MessageBox.Show("Тестирование успешно завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}