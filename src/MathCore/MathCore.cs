using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public interface IPoint
    {
        double GetPointOnAxis(int i);
        bool NearTo(IPoint p);
        List<double> Results { get; }
        bool Mark { get; set; }
    }

    public interface IGrid
    {
        List<IPoint> GetPoints();
    }

    public interface IFunction
    {
        double GetValue(IPoint p);
    }
}