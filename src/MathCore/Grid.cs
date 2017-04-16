using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class Grid : IGrid
    {
        List<IPoint> points;
        private Grid(){}
        public Grid(List<Tuple<double,double>> limitations, int blocks)
        {
            points = new List<IPoint>();
            int t =0;
            for (int k = 0; k < blocks*blocks; ++k)
            {
                List<double> point_on_axis = new List<double>();
                for (int i = 0; i < limitations.Count; ++i)
                {
                    double step = Math.Abs(limitations[i].Item1 - limitations[i].Item2) / blocks;
                    point_on_axis.Add(step * i + k*step/blocks);
                }
                points.Add(new Point(point_on_axis));
            }
            
        }
        public List<IPoint> GetPoints()
        {
            return points;
        }

        public void Remove(IPoint p)
        {
            throw new NotImplementedException();
        }
    }
}
