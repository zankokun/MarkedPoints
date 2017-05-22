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
        public List<AxisRange> AxisRanges { get; set; }
        private IPoint point;

        public Block()
        {
            AxisRanges = new List<AxisRange>();
            point = null;
        }

        public bool TrySetPoint(IPoint point)
        {
            if (this.point != null) return false;

            for (int i = 0; i < AxisRanges.Count; i++)
            {
                if (point.GetPointOnAxis(i) > AxisRanges[i].Second &&
                    point.GetPointOnAxis(i) < AxisRanges[i].First)
                {
                    return false;
                }
            }
            this.point = point;
            return true;
        }

        public bool Empty()
        {
            return point == null;
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

        List<IPoint> points;
        int pointsCount;
        int blocksCount;

        bool fromDifferentBlocks;

        List<List<double>> coordinates;
        List<Block> blocks;

        public Grid(List<AxisRange> limitations, int blocksCount, bool fromDifferentBlocks = true)
        {
            this.limitations = limitations;
            this.blocksCount = blocksCount;
            pointsCount = (int)Math.Pow(blocksCount, limitations.Count);
            this.fromDifferentBlocks = fromDifferentBlocks;
            points = new List<IPoint>();
            FillPoints();
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
            blocks = new List<Block>();
            var FakeRange = new AxisRange(0d, 0d);
            for (int i = 0; i < pointsCount; ++i)
            {
                blocks.Add(new Block());
            }
            List<AxisRange> values = new List<AxisRange>();
            internalFillBlocks(0, 0, ref values);
        }

        private void internalFillBlocks(int axisIndex, int pointIndex, ref List<AxisRange> values)
        {
            if (axisIndex > limitations.Count - 1)
            {
                foreach (var val in values) blocks[pointIndex].AxisRanges.Add(val);
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
                internalFillBlocks(axisIndex + 1, pointIndex + i * factor, ref values);
                values.RemoveAt(values.Count - 1);
            }
        }

        private bool TrySetPointInBlock(IPoint point)
        {
            for (int i = 0; i < pointsCount; i++)
            {
                if (blocks[i].TrySetPoint(point)) return true;
            }
            return false;
        }

        private void FillPoints()
        {
            FillCoordinates();
            if (fromDifferentBlocks) FillBlocks();
            for (int i = 0; i < pointsCount; ++i)
            {
                points.Add(fromDifferentBlocks ? GetRandomPointFromBlock() : GetRandomPoint());
            }
        }

        private IPoint GetRandomPoint()
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
            return new Point(vector);
        }

        private IPoint GetRandomPointFromBlock()
        {
            var rand = new Random();

            List<double> vector = new List<double>();
            do
            {
                for (int i = 0; i < limitations.Count; ++i)
                {
                    var index = rand.Next(pointsCount);
                    while (Double.IsNaN(coordinates[i][index]))
                    {
                        index = rand.Next(pointsCount);
                    }
                    vector.Add(coordinates[i][index]);
                }
            }
            while (!TrySetPointInBlock(new Point(vector)));
            for (int i = 0; i < limitations.Count; ++i)
            {
                coordinates[i][coordinates[i].IndexOf(vector[i])] = Double.NaN;
            }
            return new Point(vector);
        }

        public List<IPoint> GetPoints()
        {
            return points;
        }

        public List<Block> GetBlocks()
        {
            return blocks;
        }
    }
}
