using MathCore;
using System.Collections.Generic;

namespace UnitTests
{
    public partial class GridTests
    {
        public class PointDataStorage : IPointStorage
        {
            List<IPoint> pointData;

            public PointDataStorage()
            {
                pointData = new List<IPoint>();
            }
            public List<IPoint> Get()
            {
                return pointData;
            }

            public void Add(List<AxisRange> axisRange, IPoint point)
            {
                pointData.Add(new PointData(point, axisRange));
            }
        }
    }
}
