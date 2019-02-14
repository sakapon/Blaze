using System;
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
