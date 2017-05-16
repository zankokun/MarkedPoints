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
            results_of_func = new List<double>();
        }
        public double GetPointOnAxis(int i)
        {
            return points_on_axes[i];
        }

        public bool NearTo(IPoint p)
        {
            for (int i = 0; i < points_on_axes.Count; ++i)
                if (points_on_axes[i] == p.GetPointOnAxis(i)) return true;
            return false;
        }
        public List<double> Results
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
