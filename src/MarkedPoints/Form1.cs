using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathCore;

namespace MarkedPoints
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] function_strings = tb_Critetions.Lines;
            string[] bounders = tb_Bounders.Lines;

            //размерность 
            int N = 0; //пока не проверим все ограничения, не знаем скольки размерная задача
            //количество блоков разбиения
            int blocks = Int32.Parse(tb_num_of_blocks.Text);

            //создаem сетку
            var limitations = new List<AxisRange>();
            string[] temp;
            foreach (string str in bounders)
            {
                if (str == "") continue; // если есть пустые строки ограничений, не берем их в расчёт
                temp = str.Split(' ');
                limitations.Add(new AxisRange(double.Parse(temp.First<string>()), double.Parse(temp.Last<string>())));
                ++N;
            }
            IGrid grid = new Grid(limitations, blocks);


            //создали функции
            List<IFunction> functions = new List<IFunction>();
            foreach (string str in function_strings)
                functions.Add(new Function(str, N));
            //здесь - инициализация, запуск и обработка результатов алгоритма алгоритма
            //points - лист точек
            Algorthm Alg = new Algorthm(functions, grid.GetPoints());
            var points = Alg.DoAlgorithm();

            resultsTextBox.Clear();
            foreach (var point in points)
            {
                resultsTextBox.Text += "Point: (";
                foreach (var res in point.Results)
                {
                    resultsTextBox.Text += " " + res.ToString();
                }
                resultsTextBox.Text += " ) \n\n";
            }
        }
    }
}
