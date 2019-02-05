using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class NormalDistributionTest
    {
        [TestMethod]
        public void NextDoubles_Rare()
        {
            var count = 10000;
            var values = NormalDistribution.NextDoubles()
                .Take(count)
                .Where(x => Math.Abs(x) > 3)
                .ToArray();

            Console.WriteLine($"{(double)values.Length * 100 / count} %");
            foreach (var x in values.OrderBy(Math.Abs))
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDouble()
        {
            for (var i = 0; i < 100; i++)
                Console.WriteLine(NormalDistribution.NextDouble());
        }

        [TestMethod]
        public void NextDoubleInSigma()
        {
            for (var i = 0; i < 10000; i++)
            {
                var x = NormalDistribution.NextDoubleInSigma();
                Assert.IsTrue(Math.Abs(x) < NormalDistribution.DefaultMaxSigma);
            }

            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDoubleInSigma())
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDoubleInSigma_Max()
        {
            var max = 1.5;

            for (var i = 0; i < 10000; i++)
            {
                var x = NormalDistribution.NextDoubleInSigma(max);
                Assert.IsTrue(Math.Abs(x) < max);
            }

            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDoubleInSigma(max))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDouble_1()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDouble(1))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDouble_1_2()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDouble(1, 2))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDouble_Score()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => 50 + NormalDistribution.NextDouble(25))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextInt32_5()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextInt32(5))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextInt32_Many()
        {
            var values = Enumerable.Repeat(false, 10000)
                .Select(_ => NormalDistribution.NextInt32(10));
            WriteSummary(values);
        }

        [TestMethod]
        public void MapTest()
        {
            var M = Math.Atan(double.PositiveInfinity) * 2 / Math.PI; //  1.0
            var m = Math.Atan(double.NegativeInfinity) * 2 / Math.PI; // -1.0

            // 有限区間に射影する実験です。
            var count = 10000;
            var maxAbsValue = 10.5;
            var maxSigma = 3.5;

            var normals = NormalDistribution.NextDoubles()
                .Take(count)
                .ToArray();

            Console.WriteLine("Using range:");
            var ranges = normals
                .Select(x => Math.Abs(x) >= maxSigma ? NormalDistribution.NextDoubleInSigma(maxSigma) : x)
                .Select(x => x * maxAbsValue / maxSigma);
            WriteSummary(ranges);

            Console.WriteLine("Using arctan:");
            WriteSummary(normals.Select(x => Math.Atan(0.5 * x) * maxAbsValue * 2 / Math.PI));

            Console.WriteLine("Using exponential:");
            WriteSummary(normals.Select(x => Math.Sign(x) * (1 - Math.Exp(-0.3 * Math.Abs(x))) * maxAbsValue));

            Console.WriteLine("Using inverse:");
            WriteSummary(normals.Select(x => Math.Sign(x) * (1 - 1 / (1 + 0.3 * Math.Abs(x))) * maxAbsValue));
        }

        static void WriteSummary(IEnumerable<double> values) =>
            WriteSummary(values.Select(x => (int)Math.Round(x, MidpointRounding.AwayFromZero)));

        static void WriteSummary(IEnumerable<int> values)
        {
            var query = values
                .GroupBy(x => x)
                .Select(g => new { x = g.Key, count = g.Count() })
                .OrderBy(_ => _.x);
            foreach (var _ in query)
                Console.WriteLine($"{_.x}: {_.count}");
        }
    }
}
