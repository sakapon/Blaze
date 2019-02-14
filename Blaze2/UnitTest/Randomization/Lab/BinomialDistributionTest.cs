﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class BinomialDistributionTest
    {
        [TestMethod]
        public void NextInt32()
        {
            var M = 3;

            for (var i = 0; i < 10000; i++)
            {
                var x = BinomialDistribution.NextInt32(M);
                Assert.IsTrue(Abs(x) <= M);
            }

            var values = Enumerable.Repeat(false, 100)
                .Select(_ => BinomialDistribution.NextInt32(M))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextInt32_Distribution()
        {
            var M = 5;
            var count = 10000;

            var query = Enumerable.Repeat(false, count)
                .Select(_ => NormalDistribution.NextInt32(M))
                .GroupBy(x => x)
                .Select(g => new { x = g.Key, r = (double)g.Count() / count })
                .OrderBy(_ => _.x)
                .Zip(GetTheoretical(2 * M), (_, t) => new { _.x, _.r, t });
            foreach (var _ in query)
                Console.WriteLine($"{_.x}: {_.r:F4} : {_.t:F4}");
        }

        [TestMethod]
        public void NextInt32ByMinMax()
        {
            var m = 11;
            var M = 16;

            for (var i = 0; i < 10000; i++)
            {
                var x = BinomialDistribution.NextInt32ByMinMax(m, M);
                Assert.IsTrue(m <= x && x <= M);
            }

            var values = Enumerable.Repeat(false, 100)
                .Select(_ => BinomialDistribution.NextInt32ByMinMax(m, M))
                .OrderBy(x => x);
            foreach (var x in values)
                Console.WriteLine(x);
        }

        [TestMethod]
        public void NextInt32_Uniform()
        {
            var M = 5;
            var sigma = Sqrt(2 * M) / 2.0;
            var maxAbsValue = M + 0.5;
            var sidePoints = 512;

            var values = GetValuesFromUniform(sidePoints)
                .Select(x => x * sigma)
                .Where(x => Abs(x) < maxAbsValue)
                .Select(x => (int)Round(x, MidpointRounding.AwayFromZero))
                .ToArray();

            var query = values
                .GroupBy(x => x)
                .Select(g => new { x = g.Key, r = (double)g.Count() / values.Length })
                .OrderBy(_ => _.x)
                .Zip(GetTheoretical(2 * M), (_, t) => new { _.x, _.r, t });
            foreach (var _ in query)
                Console.WriteLine($"{_.x}: {_.r:F4} : {_.t:F4}");
        }

        static IEnumerable<double> GetValuesFromUniform(int sidePoints)
        {
            var TwoPi = 2 * PI;
            var d = 1.0 / sidePoints;
            var d0 = 0.5 / sidePoints;

            return
                from x in Enumerable.Range(0, sidePoints)
                from y in Enumerable.Range(0, sidePoints)
                from v in Standards(d0 + d * x, d0 + d * y)
                select v;

            IEnumerable<double> Standards(double x, double y)
            {
                yield return Sqrt(-2 * Log(x)) * Sin(TwoPi * y);
                yield return Sqrt(-2 * Log(x)) * Cos(TwoPi * y);
            }
        }

        static IEnumerable<double> GetTheoretical(int n)
        {
            var all = Pow(2, n);
            return Enumerable.Range(0, n + 1)
                .Select(k => Combination(n, k) / all);
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
