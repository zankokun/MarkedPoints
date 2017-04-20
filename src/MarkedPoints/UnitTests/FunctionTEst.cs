using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using Z.Expressions;

namespace UnitTests
{
    /// <summary>
    /// Summary description for FunctionTest
    /// </summary>
    [TestClass]
    public class FunctionTest
    {
        public FunctionTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void FunctionTest1()
        {
            int N = 3;
            List<string> functions = new List<string>();
            IPoint point = new Point(new List<double>{ 1, 1, 1 });
            functions.Add("X1+X2+X3");
            functions.Add("X1+X2+X3-(X1+X2+X3)");
            functions.Add("(X1+X2+X3)*(X1+X2+X3)");

            IFunction func = new Function(functions[0], N);
            Assert.AreEqual(3, func.GetValue(point));

            func = new Function(functions[1], N);
            Assert.AreEqual(0, func.GetValue(point));

            func = new Function(functions[2], N);
            Assert.AreEqual(9, func.GetValue(point));
            //
            // TODO: Add test logic here
            //
        }
    }
}
