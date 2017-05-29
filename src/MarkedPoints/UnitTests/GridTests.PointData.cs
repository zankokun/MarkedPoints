using MathCore;
using System.Collections.Generic;

namespace UnitTests
{
    public partial class GridTests
    {
        //PointData - декоратор к IPoint для получения блока, к которому относится точка
        public class PointData : IPoint
        {
            private IPoint point;
            private List<AxisRange> axisRanges;

            public PointData(IPoint point, List<AxisRange> axisRanges)
            {
                this.point = point;
                this.axisRanges = axisRanges;
            }

            public double GetPointOnAxis(int i)
            {
                return point.GetPointOnAxis(i);
            }

            public bool Equals(IPoint p)
            {
                return point.Equals(p);
            }

            public void AddResult(double result)
            {
                point.AddResult(result);
            }
            public double GetResult(int index)
            {
                return point.GetResult(index);
            }

            public bool Mark
            {
                get { return point.Mark; }
                set { point.Mark = value; }
            }

            public bool CrossesBlock(PointData other)
            {
                int crosses = 0;
                for (int i = 0; i < axisRanges.Count; ++i)
                {
                    if ((axisRanges[i].First < other.axisRanges[i].Second &&
                        axisRanges[i].Second > other.axisRanges[i].First))
                        ++crosses;
                }
                return crosses == axisRanges.Count;
            }

            public bool CheckPointInBlock()
            {
                for (int j = 0; j < axisRanges.Count; ++j)
                {
                    if (axisRanges[j].First >= point.GetPointOnAxis(j) ||
                        axisRanges[j].Second <= point.GetPointOnAxis(j))
                        return false;
                }
                return true;
            }
        }
    }
}
