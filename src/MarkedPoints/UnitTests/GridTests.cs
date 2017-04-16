using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void CreateGridWithUniquePoints()
        {
            //количество блоков сетки в одной строке
            int blocks = 8;
            //ограничения области D  в виде двойных неравенств a_i <= X_i <= b_i 
            //TODO: возможно стоит сделать класс для них
            List<Tuple<double, double>> limitations = new List<Tuple<double, double>>();
            limitations.Add(new Tuple<double, double>(0, 1));
            limitations.Add(new Tuple<double, double>(0, 1));
            limitations.Add(new Tuple<double, double>(0, 1));
            limitations.Add(new Tuple<double, double>(0, 1));
            IGrid grid = new Grid(limitations, blocks);
            //Проверяем количество созданных точек и то, что каждая координата каждой точки отличаются друг от друга
            Assert.AreEqual(grid.GetPoints().Count, blocks * blocks);
            for (int i = 0; i < blocks * blocks - 1; ++i)
                for (int j = i+1; j < blocks * blocks; ++j)           
                    Assert.IsFalse(grid.GetPoints()[i].NearTo(grid.GetPoints()[j]));
        }
    }
}
