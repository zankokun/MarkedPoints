using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public partial class GridTests
    {
        [TestMethod]
        public void CreateGridWithUniquePoints()
        {
            // количество блоков сетки в одной строке
            int blocksCount = 8;
            // ограничения области D  в виде двойных неравенств a_i <= X_i <= b_i
            List<AxisRange> limitations = new List<AxisRange>()
            {
               new AxisRange(0, 1),
               new AxisRange(0, 1),
               new AxisRange(0, 1),
               new AxisRange(0, 1)
            };
            var grid = new Grid<PointStorage>(limitations, blocksCount);
            var pointsCount = Math.Pow(blocksCount, limitations.Count);
            var points = grid.GetStorage().Get();
            // Проверяем количество созданных точек и то, 
            // что каждая координата каждой точки отличаются друг от друга
            Assert.AreEqual(points.Count, pointsCount);
            for (int i = 0; i < pointsCount - 1; ++i)
            {
                for (int j = i + 1; j < pointsCount; ++j)
                {
                    Assert.IsFalse(points[i].Equals(points[j]));
                }
            }
        }

        [TestMethod]
        public void CreateGridWithProperlyBlocks()
        {
            // количество блоков сетки в одной строке
            int blocksCount = 2;
            // ограничения области D  в виде двойных неравенств a_i <= X_i <= b_i
            List<AxisRange> limitations = new List<AxisRange>()
            {
               new AxisRange(0, 1),
               new AxisRange(0, 1)
            };
            var grid = new Grid<PointDataStorage>(limitations, blocksCount);

            var pointsCount = Math.Pow(blocksCount, limitations.Count);

            List<PointData> data = new List<PointData>();
            var points = grid.GetPoints();
            foreach (var p in points)
                data.Add(p as PointData);

            // Проверяем количество созданных блоков
            Assert.AreEqual(data.Count, pointsCount);

            // Проверяем, что каждый блок уникален (не равен/пересекает другой блок)
            for (int i = 0; i < pointsCount - 1; ++i)
            {
                for (int j = i + 1; j < pointsCount; ++j)
                {
                    Assert.IsFalse(data[i].CrossesBlock(data[j]));
                }
            }

            // Проверяем, что точка принадлежит блоку
            for (int i = 0; i < pointsCount; ++i)
            {
                Assert.IsTrue(data[i].CheckPointInBlock());
            }
        }
    }
}
