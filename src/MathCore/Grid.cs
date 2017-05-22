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
        List<AxisRange> limitations;

        List<IPoint> points;
        int pointsCount;

        List<List<double>> coordinates;


        public Grid(List<AxisRange> limitations, int blocksCount)
        {
            this.limitations = limitations;
            this.pointsCount = (int)Math.Pow(blocksCount, limitations.Count);

            points = new List<IPoint>();
            FillPoints();
        }

        private void FillPoints()
        {
            coordinates = new List<List<double>>();
            for (int i = 0; i < limitations.Count; ++i)
            {
                coordinates.Add(new List<double>((int)pointsCount));
            }

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

                    coordinates[j].Add((firstVerge + lastVerge) / 2);
                }
            }

          for(int i = 0; i < pointsCount; ++i)
            {
                points.Add(new Point(GetRandomVector()));
            }
        }

        private List<double> GetRandomVector()
        {
            var rand = new Random();

            List<double> vector = new List<double>();
            for (int i = 0; i < limitations.Count; ++i)
            {
                var index = rand.Next(pointsCount);
                while (Double.IsNaN(coordinates[i][index]))
                {
                    index = rand.Next(pointsCount);
                }
                vector.Add(coordinates[i][index]);
                coordinates[i][index] = Double.NaN;
            }
            return vector;
        }

        public List<IPoint> GetPoints()
        {
            return points;
        }
    }
}
