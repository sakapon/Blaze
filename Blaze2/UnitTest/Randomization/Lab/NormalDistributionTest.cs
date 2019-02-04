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
        public void NextDouble_5()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDouble(5))
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
                .Select(_ => NormalDistribution.NextInt32(10))
                .GroupBy(x => x)
                .Select(g => new { x = g.Key, count = g.Count() })
                .OrderBy(_ => _.x);
            foreach (var _ in values)
                Console.WriteLine($"{_.x}: {_.count}");
        }
    }
}
