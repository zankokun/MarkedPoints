using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.Expression;

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
            var symbols = new Dictionary<string, FloatingPoint>
             {{ "X1", 2.0 },
             { "X2", 2.0 },
              { "X3", 2.0 }};

            Assert.AreEqual(0.125, Evaluate.Evaluate(symbols, Infix.ParseOrUndefined("1/(X1*X2*X3)")).RealValue);
            
        }
    }
}
