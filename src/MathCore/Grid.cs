using System;
using System.Collections.Generic;

namespace MathCore
{
    public class AxisRange
    {
        public double First { get; private set; }
        public double Second { get; private set; }

        public AxisRange() { }
        public AxisRange(double first, double second) { First = first; Second = second; }
    }

    public class Grid : IGrid
    {
        List<IPoint> points;
      
        public Grid(List<AxisRange> limitations, int blocksCount)
        {
            double pointsCount = Math.Pow(blocksCount, limitations.Count);

            points = new List<IPoint>();
         
            for (int i = 0; i < pointsCount; ++i)
            {
                List<double> pointOnAxis = new List<double>();
                for (int j = 0; j < limitations.Count; ++j)
                {
                    double step = Math.Abs(limitations[j].First - limitations[j].Second) / pointsCount;

                    var firstVerge = limitations[j].First + step * i;
                    var lastVerge = i == pointsCount - 1 ?
                        limitations[j].Second :
                        limitations[j].First + step * (i + 1);

                    pointOnAxis.Add((firstVerge + lastVerge) / 2);
                }
                points.Add(new Point(pointOnAxis));
            }
        }

        public List<IPoint> GetPoints()
        {
            return points;
        }
    }
}
