using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public interface IPointStorage
    {
        void Add(IPoint point, List<AxisRange> axisRange);
        List<IPoint> Get();
    }

    public class PointStorage: IPointStorage
    {
        private List<IPoint> points;

        public PointStorage()
        {
            points = new List<IPoint>();
        }

        public List<IPoint> Get()
        {
            return points;
        }

        public void Add(IPoint point, List<AxisRange> axisRange)
        {
            points.Add(point);
        }
    }

}
