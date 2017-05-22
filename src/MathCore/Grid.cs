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

    public class Block
    {
        public List<AxisRange> AxisRanges { get; private set; }
        public IPoint Point { get;  private set; }

        public Block(List<AxisRange> axisRanges, IPoint point)
        {
            AxisRanges = axisRanges;
            Point = point;
        }

        public bool Equals(Block other)
        {
            for (int i = 0; i < AxisRanges.Count; ++i)
            {
                if (AxisRanges[i].First != other.AxisRanges[i].First ||
                   AxisRanges[i].Second != other.AxisRanges[i].Second)
                    return false;
            }
            return true;
        }
    }

    public class Grid : IGrid
    {
        List<AxisRange> limitations;

        int pointsCount;
        int blocksCount;

        List<List<double>> coordinates;
        List<Block> blocks;
        List<IPoint> pointsFromBlocks;

        public Grid(List<AxisRange> limitations, int blocksCount)
        {
            this.limitations = limitations;
            this.blocksCount = blocksCount;
            pointsCount = (int)Math.Pow(blocksCount, limitations.Count);;
            FillBlocks();
        }

        private void FillCoordinates()
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

        private void FillBlocks()
        {
            FillCoordinates();
            blocks = new List<Block>();
            pointsFromBlocks = new List<IPoint>();
            List<AxisRange> values = new List<AxisRange>();
           InternalFillBlocks(0, 0, ref values);
        }

        private void InternalFillBlocks(int axisIndex, int pointIndex, ref List<AxisRange> values)
        {
            if (axisIndex > limitations.Count - 1)
            {
                var copyValues = new List<AxisRange>();
                foreach (var val in values) copyValues.Add(val);

                var vector = new List<double>();
                var rand = new Random();
                for(int i = 0; i < limitations.Count; i++)
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
                 blocks.Add(new Block(copyValues, newPoint));
                pointsFromBlocks.Add(newPoint);
                return;
            }

            int factor = (int)Math.Pow(blocksCount, axisIndex);
            for (int i = 0; i < blocksCount; ++i)
            {
                double step = Math.Abs(limitations[axisIndex].First - limitations[axisIndex].Second) /
                    blocksCount;

                var firstVerge = limitations[axisIndex].First + step * i;
                var lastVerge = i == blocksCount - 1 ?
                    limitations[axisIndex].Second :
                    limitations[axisIndex].First + step * (i + 1);

                values.Add(new AxisRange(firstVerge, lastVerge));
                InternalFillBlocks(axisIndex + 1, pointIndex + i * factor, ref values);
                values.RemoveAt(values.Count - 1);
            }
        }

        public List<IPoint> GetPoints()
        {
            return pointsFromBlocks;
        }

        public List<Block> GetBlocks()
        {
            return blocks;
        }
    }
}
