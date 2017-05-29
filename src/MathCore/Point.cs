using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class Point : IPoint
    {
        List<double> points_on_axes, results_of_func;
        bool mark;
        public Point(List<double> axis)
        {
            this.points_on_axes = axis;
            mark = false;
            results_of_func = new List<double>(points_on_axes.Count);
        }
        public double GetPointOnAxis(int i)
        {
            if (points_on_axes.Count < i) return -1;
            return points_on_axes[i];
        }

        public bool Equals(IPoint p)
        {
            for (int i = 0; i < points_on_axes.Count; ++i)
                if (Math.Abs(points_on_axes[i] - p.GetPointOnAxis(i)) > Double.Epsilon) return false;
            return true;
        }

        public void AddResult(double result)
        {
            results_of_func.Add(result);
        }
        public double GetResult(int index)
        {
            if (results_of_func.Count < index) return -1;
            return results_of_func[index];
        }

        public bool Mark
        {
            get
            {
                return mark;
            }
            set => mark = value;
        }
    }
}
