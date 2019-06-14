using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class ExponentialDistribution
    {
        static readonly Random random = new Random();

        public static double Next(double mean) =>
            -mean * Math.Log(1 - random.NextDouble());
    }
}
