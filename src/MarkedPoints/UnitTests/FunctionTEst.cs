using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using MathNet.Symbolics;

namespace UnitTests
{
    /// <summary>
    /// Summary description for FunctionTest
    /// </summary>
    [TestClass]
    public class FunctionTest
    {
   

        [TestMethod]
        public void FunctionTest_PasreAndEval()
        {
            var symbols = new Dictionary<string,  FloatingPoint>
             {{ "X1", 2d },
             { "X2", 2d },
              { "X3", 2d }};
            string function = "1/(X1*X2*X3)";
            IFunction func = new MathCore.Function(function,3);
            //проеряем, что результат вычисления функции и реализации из библиотеки совпадают
            Assert.AreEqual(func.GetValue(new Point(new List<double> { 2d,2d,2d})), Evaluate.Evaluate(symbols, Infix.ParseOrUndefined(function)).RealValue);
            
        }
    }
}
