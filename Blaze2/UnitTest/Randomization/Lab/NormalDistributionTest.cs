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
            var count = 10000;
            var values = Enumerable.Repeat(false, count)
                .Select(_ => NormalDistribution.NextInt32_2(5));
            WriteSummary(values, count);
            Console.WriteLine();
            WriteTheoreticalBinomial(10);
        }

        static void WriteSummary(IEnumerable<double> values, int count) =>
            WriteSummary(values.Select(x => (int)Math.Round(x, MidpointRounding.AwayFromZero)), count);

        static void WriteSummary(IEnumerable<int> values, int count)
        {
            var query = values
                .GroupBy(x => x)
                .Select(g => new { x = g.Key, count = g.Count() })
                .OrderBy(_ => _.x);
            foreach (var _ in query)
                Console.WriteLine($"{_.x}: {(double)_.count / count:F4}");
        }

        static void WriteTheoreticalBinomial(int n)
        {
            var all = Math.Pow(2, n);
            var query = Enumerable.Range(0, n + 1)
                .Select(k => new { k, p = Combination(n, k) / all });
            foreach (var _ in query)
                Console.WriteLine($"{_.k}: {_.p:F4}");
        }

        public static long Permutation(int n, int k)
        {
            if (k == 0) return 1;
            return Enumerable.Range(0, k).Select(i => n - i).Aggregate(1L, (x0, x) => x0 * x);
        }

        public static long Combination(int n, int k)
        {
            if (k > n / 2) k = n - k;
            if (k == 0) return 1;
            return Permutation(n, k) / Enumerable.Range(1, k).Aggregate(1L, (x0, x) => x0 * x);
        }
    }
}
