using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Expressions;
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
            var values = new Dictionary<string, object>();
            for (int i=0;i<dimentions;++i)
            {
                values.Add("X" + (i+1).ToString(), p.GetPointOnAxis(i));
            }
            return Eval.Execute<double>(function_string, values);
        }
    }
}
