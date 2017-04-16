using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class Point : IPoint
    {
        List<double> points_on_axes;
        private Point() { }
        public Point(List<double> axis)
        {
            this.points_on_axes = axis;
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
    }
}
