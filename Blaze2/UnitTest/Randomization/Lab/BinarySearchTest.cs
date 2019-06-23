using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class BinarySearchTest
    {
        static readonly Random random = new Random();

        [TestMethod]
        public void GetRandomIndexByCumulation_1()
        {
            var count = 99999;
            var mean = count / 2.0;

            var probs = Enumerable.Range(0, count)
                .Select(i => random.NextDouble())
                .ToArray();
            var cumulation = new double[count + 1];
            for (var i = 0; i < count; i++)
                cumulation[i + 1] = cumulation[i] + probs[i] / mean;

            var result = Enumerable.Range(0, count)
                .Select(_ => cumulation.GetRandomIndexByCumulation())
                .GroupBy(i => i)
                .OrderByDescending(g => g.Count())
                .ToArray();
            var summary = result.Take(30)
                .Concat(result.Skip(result.Length - 30))
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (var p in summary)
                Console.WriteLine($"{p.Key}: {p.Value}, {(p.Key == count ? (1 - cumulation[p.Key]) * mean : probs[p.Key])}");
        }
    }
}
