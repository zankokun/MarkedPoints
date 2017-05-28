using System;
using System.Collections.Generic;

namespace MathCore
{
    public class Grid : IGrid
    {
        List<AxisRange> limitations;

        int pointsCount;
        int blocksCount;

        List<List<double>> coordinates;
        List<Block> blocks;
        List<IPoint> blocksPoints;

        public Grid(List<AxisRange> limitations, int blocksCount)
        {
            this.limitations = limitations;
            this.blocksCount = blocksCount;
            pointsCount = (int)Math.Pow(blocksCount, limitations.Count);
            FillPoints();
        }

        private void FillPoints()
        {
            FillCoords();
            blocks = new List<Block>();
            blocksPoints = new List<IPoint>();
            FillBlocks(0, new List<AxisRange>());
            foreach (var block in blocks)
            {
                blocksPoints.Add(block.Point);
            }
        }

        private void FillCoords()
        {
            coordinates = new List<List<double>>();
            for (int i = 0; i < limitations.Count; ++i)
            {
                coordinates.Add(new List<double>());
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
        }

        private void FillBlocks(int axisIndex, List<AxisRange> values)
        {
            if (axisIndex > limitations.Count - 1)
            {
                var vector = new List<double>();
                var rand = new Random();
                for (int i = 0; i < limitations.Count; i++)
                {
                    int index = rand.Next(pointsCount);
                    while (Double.IsNaN(coordinates[i][index]) ||
                        values[i].First > coordinates[i][index] ||
                        values[i].Second < coordinates[i][index])
                    {
                        index = rand.Next(pointsCount);
                    }
                    vector.Add(coordinates[i][index]);
                    coordinates[i][index] = Double.NaN;
                }
                var newPoint = new Point(vector);

                var copyValues = new List<AxisRange>();
                foreach (var val in values) copyValues.Add(val);
                blocks.Add(new Block(copyValues, newPoint));
                return;
            }

            for (int i = 0; i < blocksCount; ++i)
            {
                double step = Math.Abs(limitations[axisIndex].First - limitations[axisIndex].Second) /
                    blocksCount;

                var firstVerge = limitations[axisIndex].First + step * i;
                var lastVerge = i == blocksCount - 1 ?
                    limitations[axisIndex].Second :
                    limitations[axisIndex].First + step * (i + 1);

                values.Add(new AxisRange(firstVerge, lastVerge));
                FillBlocks(axisIndex + 1, values);
                values.RemoveAt(values.Count - 1);
            }
        }

        public List<IPoint> GetPoints()
        {
            return blocksPoints;
        }

        public List<Block> GetBlocks()
        {
            return blocks;
        }
    }
}
