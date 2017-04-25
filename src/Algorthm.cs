using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method_of_mark_point
{
    class Algorthm
    {
        //F1() - критерий
        //kolvo_kr - количество критериев
        //left - левая граница отрезка
        //right - правая граница
        //N - кол-во точек рабиения
        public Algorthm()
        {

        }
        void DoAlgorithm()
        {
            List<Point> points = new List<Point>();
            int right = 0, left = 0, N = 1, current = left,kolvo_kr = 1;
            int h = (right - left) / N;    
            //инициализация точек
            while (current<right)
            {
                Point tmp = new Point(current);
                for (int i = 0; i < kolvo_kr; i++) tmp.Results.Add(F(tmp.X));
                points.Add(tmp);
                current += h;
            }
            //сам метод
            Point current_point = points[0];
            while (true)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    int kolvo_b = 0, kolvo_m = 0;
                    for (int j = 0; j < current_point.Results.Count; j++)
                    {
                        if (current_point.Results[j] < points[i].Results[j]) kolvo_m++;
                        if (current_point.Results[j] > points[i].Results[j]) kolvo_b++;
                    }
                    if (kolvo_b == 0)
                    {
                        points.RemoveAt(i);
                        i--;
                    }
                    if (kolvo_m == 0)
                    {
                        points.Remove(current_point);
                        current_point = points[i];
                        i--;
                    }
                }
                current_point.Mark = true;
                bool IsMark = false;
                foreach (Point p in points)
                    if (!p.Mark)
                    {
                        current_point = p;
                        IsMark = true;
                        break;
                    }
                if (!IsMark) break;
            }
        }
    }
    class Point
    {
        int x;
        List<int> results_of_func;
        bool mark;
        public Point(int p)
        {
            x = p;
            results_of_func = new List<int>();
            mark = false;
        }
        public int X
        {
            get
            {
                return x;
            }
        }
        public List<int> Results
        {
            get
            {
                return results_of_func;
            }
        }
        public bool Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }
    }
}
