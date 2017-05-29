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
            tb_Critetions.Text = "(X1-9)*(X1-9)-2"+ Environment.NewLine + "6-(1/2)*X1";
            tb_Bounders.Text = "0 16";
            tb_num_of_blocks.Text = "5";
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
            int blocksCount = Int32.Parse(tb_num_of_blocks.Text);

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

            List<IFunction> functions = new List<IFunction>();
            foreach (string str in function_strings)
                functions.Add(new Function(str, N));

            var grid = new Grid<PointStorage>(limitations, blocksCount);
            Algorthm Alg = new Algorthm(functions, grid.GetStorage().Get());
            var points = Alg.Run();

            resultsTextBox.Clear();
            foreach (IPoint point in points)
            {
                resultsTextBox.Text += "Point: (";
                for (int i = 0; i < N; i++)
                {
                    resultsTextBox.Text += point.GetPointOnAxis(i);
                    if (i + 1 < N) resultsTextBox.Text += ";";
                }
                resultsTextBox.Text += ")" + Environment.NewLine;
            }
        }
    }
}
