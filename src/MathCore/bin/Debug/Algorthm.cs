using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCore;

namespace MathCore
{
    public class Algorthm
    {
        List<IFunction> functions;
        List<IPoint> points;
        public Algorthm(List<IFunction> pfunc, List<IPoint> ppoints)
        {
            functions = pfunc;
            points = ppoints;
        }
        public List<IPoint> DoAlgorithm()
        {
            foreach (IPoint point in points)
                foreach (IFunction func in functions)
                    point.Results.Add(func.GetValue(point));
            //сам метод
            IPoint current_point = points[0];
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
            return points;
        }
    }
}
