using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class ExponentialDistributionTest
    {
        [TestMethod]
        public void Next()
        {
            var mean = 2.0;
            var count = 100000;

            var values = Enumerable.Range(0, count)
                .Select(i => ExponentialDistribution.Next(mean))
                .OrderBy(x => x)
                .ToArray();
            Console.WriteLine($"Mean of Values: {values.Average()}");

            var largeValues = values.SkipWhile(x => x < mean).ToArray();
            // 1/e
            Console.WriteLine($"Large Count Ratio: {(double)largeValues.Length / count}");
            // 2/e
            Console.WriteLine($"Large Sum Ratio: {largeValues.Sum() / (mean * count)}");

            Console.WriteLine();
            foreach (var g in values.GroupBy(x => (int)x))
                Console.WriteLine($"{g.Key}: {g.Count()}");
            Console.WriteLine();
            foreach (var g in values.GroupBy(x => Math.Floor(x * 10) / 10))
                Console.WriteLine($"{g.Key:F1}: {g.Count()}");
        }
    }
}
