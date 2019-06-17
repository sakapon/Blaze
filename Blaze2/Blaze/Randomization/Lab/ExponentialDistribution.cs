using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    /// <summary>
    /// Provides a set of methods for exponential distribution.
    /// </summary>
    public static class ExponentialDistribution
    {
        static readonly Random random = new Random();

        // 0 <= x < ∞
        public static double Next(double mean) =>
            -mean * Math.Log(1 - random.NextDouble());
    }
}
