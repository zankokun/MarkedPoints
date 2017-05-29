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
                    Assert.IsFalse(points[i].NearTo(points[j]));
                }
            }
        }

        //PointData - декоратор к IPoint для получения блока, к которому относится точка
        public class PointData : IPoint
        {
            private IPoint point;
            private List<AxisRange> axisRanges;

            public PointData(IPoint point, List<AxisRange> axisRanges)
            {
                this.point = point;
                this.axisRanges = axisRanges;
            }

            public double GetPointOnAxis(int i)
            {
                return point.GetPointOnAxis(i);
            }

            public bool NearTo(IPoint p)
            {
                return point.NearTo(p);
            }

            public List<double> Results
            {
                get { return point.Results; }
            }
            public bool Mark
            {
                get { return point.Mark; }
                set { point.Mark = value; }
            }

            public bool CrossesBlock(PointData other)
            {
                int crosses = 0;
                for (int i = 0; i < axisRanges.Count; ++i)
                {
                    if ((axisRanges[i].First < other.axisRanges[i].Second &&
                        axisRanges[i].Second > other.axisRanges[i].First))
                        ++crosses;
                }
                return crosses == axisRanges.Count;
            }

            public bool CheckPointInBlock()
            {
                for (int j = 0; j < axisRanges.Count; ++j)
                {
                    if (axisRanges[j].First >= point.GetPointOnAxis(j) ||
                        axisRanges[j].Second <= point.GetPointOnAxis(j))
                        return false;
                }
                return true;
            }
        }

        public class PointDataStorage : IPointStorage
        {
            List<IPoint> pointData;

            public PointDataStorage()
            {
                pointData = new List<IPoint>();
            }
            public List<IPoint> Get()
            {
                return pointData;
            }

            public void Add(IPoint point, List<AxisRange> axisRange)
            {
                pointData.Add(new PointData(point, axisRange));
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
            var points = grid.GetStorage().Get();
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
