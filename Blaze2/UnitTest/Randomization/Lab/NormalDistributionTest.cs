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
        public void Truncate()
        {
            var maxAbsValue = 5.4;
            var sigma = 3.6;

            for (var i = 0; i < 10000; i++)
            {
                var x = NormalDistribution.Truncate(maxAbsValue, sigma);
                Assert.IsTrue(Abs(x) < maxAbsValue);
            }

            var values = Enumerable.Repeat(false, 100)
                .Select(_ => NormalDistribution.Truncate(maxAbsValue, sigma))
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
        public void NextInt32()
        {
            var count = 10000;
            var values = Enumerable.Repeat(false, count)
                .Select(_ => NormalDistribution.NextInt32(5));
            WriteSummary(values, count);
            Console.WriteLine();
            WriteTheoreticalBinomial(10);
        }

        [TestMethod]
        public void NextInt32_Uniform()
        {
            var M = 3;
            var maxAbsValue = M + 0.5;
            var sigma = Sqrt(2 * M) / 2.0;

            var length = 512;
            var d = 1.0 / length;
            var x0 = 0.5 / length;
            var TwoPi = 2 * PI;

            var query =
                from x in Enumerable.Range(0, length)
                from y in Enumerable.Range(0, length)
                from v in NextDoubles(x0 + d * x, x0 + d * y)
                select v * sigma;
            var values = query.Where(v => Abs(v) < maxAbsValue).ToArray();
            WriteSummary(values, values.Length);
            Console.WriteLine();
            WriteTheoreticalBinomial(2 * M);

            IEnumerable<double> NextDoubles(double x, double y)
            {
                yield return Sqrt(-2 * Log(x)) * Sin(TwoPi * y);
                yield return Sqrt(-2 * Log(x)) * Cos(TwoPi * y);
            }
        }

        static void WriteSummary(IEnumerable<double> values, int count) =>
            WriteSummary(values.Select(x => (int)Round(x, MidpointRounding.AwayFromZero)), count);

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
            var all = Pow(2, n);
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
