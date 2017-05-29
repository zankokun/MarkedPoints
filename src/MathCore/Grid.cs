using System;
using System.Collections.Generic;

namespace MathCore
{
    public class Grid<T> where T: IPointStorage, new()
    {
        List<AxisRange> limitations;
        T storage;

        int pointCount;
        int blockCount;

        public Grid(List<AxisRange> limitations, int blockCount)
        {
            this.limitations = limitations;
            this.blockCount = blockCount;
            pointCount = (int)Math.Pow(blockCount, limitations.Count);

            storage = new T();

            var axisRanges = new List<AxisRange>();
            FillPoints(0, axisRanges, CreateCoords());
        }

        private List<List<double>> CreateCoords()
        {
            var coordinates = new List<List<double>>();
            for (int i = 0; i < limitations.Count; ++i)
            {
                coordinates.Add(new List<double>());
            }

            for (int i = 0; i < pointCount; ++i)
            {
                List<double> pointOnAxis = new List<double>();
                for (int j = 0; j < limitations.Count; ++j)
                {
                    double step = Math.Abs(limitations[j].First - limitations[j].Second)
                        / pointCount;

                    var firstVerge = limitations[j].First + step * i;
                    var lastVerge = i == pointCount - 1 ?
                        limitations[j].Second :
                        limitations[j].First + step * (i + 1);

                    coordinates[j].Add((firstVerge + lastVerge) / 2);
                }
            }
            return coordinates;
        }

        private void FillPoints(int axisIndex, List<AxisRange> values,
            List<List<double>> coordinates)
        {
            if (axisIndex > limitations.Count - 1)
            {
                var vector = new List<double>();
                var rand = new Random();
                for (int i = 0; i < limitations.Count; i++)
                {
                    int index = rand.Next(pointCount);
                    while (Double.IsNaN(coordinates[i][index]) ||
                        values[i].First > coordinates[i][index] ||
                        values[i].Second < coordinates[i][index])
                    {
                        index = rand.Next(pointCount);
                    }
                    vector.Add(coordinates[i][index]);
                    coordinates[i][index] = Double.NaN;
                }
                List<AxisRange> copyValues = new List<AxisRange>();
                foreach (var val in values) copyValues.Add(val);
                storage.Add(copyValues, new Point(vector));
                return;
            }

            for (int i = 0; i < blockCount; ++i)
            {
                double step = Math.Abs(limitations[axisIndex].First - limitations[axisIndex].Second)
                    / blockCount;

                var firstVerge = limitations[axisIndex].First + step * i;
                var lastVerge = i == blockCount - 1 ?
                    limitations[axisIndex].Second :
                    limitations[axisIndex].First + step * (i + 1);

                values.Add(new AxisRange(firstVerge, lastVerge));
                FillPoints(axisIndex + 1, values, coordinates);
                values.RemoveAt(values.Count - 1);
            }
        }

        public T GetStorage()
        {
            return storage;
        }
    }
}
