using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class NormalDistributionTest
    {
        [TestMethod]
        public void Standards()
        {
            foreach (var x in NormalDistribution.Standards().Take(100))
                Console.WriteLine(x);
        }

        [TestMethod]
        public void Standard_Rare()
        {
            var count = 10000;
            var values = Enumerable.Repeat(false, count)
                .Select(_ => NormalDistribution.Standard())
                .Where(x => Abs(x) > 3)
                .OrderBy(Abs)
                .ToArray();

            Console.WriteLine($"{(double)values.Length * 100 / count} %");
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void Next()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.Next(10, 50))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void Truncate()
        {
            var M = 5.4;
            var sigma = 3.6;

            var values = Enumerable.Repeat(false, 10000)
                .Select(_ => NormalDistribution.Truncate(M, sigma));
            foreach (var x in values)
                Assert.IsTrue(Abs(x) < M);
        }

        [TestMethod]
        public void NextDouble()
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
        public void NextDoubleByMinMax()
        {
            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.NextDoubleByMinMax(25, 75))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextDouble_Uniform()
        {
            var n = 6;
            var confidence = 3.0;
            var mean = n / 2.0;
            var maxAbsValue = mean + 0.5;
            var sigma = maxAbsValue / confidence;
            var sidePoints = 1024;

            var values = BinomialDistributionTest.GetValuesFromUniform(sidePoints)
                .Select(x => x * sigma)
                .Where(x => Abs(x) < maxAbsValue)
                .Select(x => (int)Round(x + mean, MidpointRounding.AwayFromZero))
                .ToArray();
            BinomialDistributionTest.WriteSummary(values, n);
        }
    }
}
