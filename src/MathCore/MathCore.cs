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
        bool Equals(IPoint p);
        void AddResult(double result);
        double GetResult(int index);
        bool Mark { get; set; }
    }

    public interface IFunction
    {
        double GetValue(IPoint p);
    }
}