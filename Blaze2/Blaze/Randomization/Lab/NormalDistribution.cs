using System;

namespace Blaze.Randomization.Lab
{
    public static class NormalDistribution
    {
        static readonly Random random = new Random();

        public static double NextDouble()
        {
            var x = random.NextDouble();
            var y = random.NextDouble();

            return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(2 * Math.PI * y);
        }
    }
}
