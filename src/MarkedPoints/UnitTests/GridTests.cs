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
            int blocksCount = 8;
            //ограничения области D  в виде двойных неравенств a_i <= X_i <= b_i 
            List<AxisRange> limitations = new List<AxisRange>()
            {
               new AxisRange(0, 1),
               new AxisRange(0, 1),
               new AxisRange(0, 1),
               new AxisRange(0, 1)
            };

            IGrid grid = new Grid(limitations, blocksCount);
            var pointsCount = Math.Pow(blocksCount, limitations.Count);
            //Проверяем количество созданных точек и то, что каждая координата каждой точки отличаются друг от друга
            Assert.AreEqual(grid.GetPoints().Count, pointsCount);
            for (int i = 0; i < blocksCount * blocksCount - 1; ++i)
                for (int j = i + 1; j < blocksCount * blocksCount; ++j)
                {
                    Assert.IsFalse(grid.GetPoints()[i].NearTo(grid.GetPoints()[j]));
                }
        }
    }
}
