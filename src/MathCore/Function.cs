using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Symbolics;

namespace MathCore
{
    public class Function : IFunction
    {
        string function_string;
        int dimentions;
        public Function(string str, int dimentions)
        {
            this.function_string = str;
            this.dimentions = dimentions;
        }
        public double GetValue(IPoint p)
        {
           Dictionary<string, FloatingPoint> values = new Dictionary<string, FloatingPoint>();
            for (int i = 0; i < dimentions; ++i)
            {
                values.Add("X" + (i+1).ToString(), p.GetPointOnAxis(i));
            }
            return Evaluate.Evaluate(values, Infix.ParseOrUndefined(function_string)).RealValue;
        }
    }
}
