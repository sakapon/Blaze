using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class NormalDistribution
    {
        static readonly Random random = new Random();

        // 0 < x < 1
        static double NextDoubleExceptZero()
        {
            while (true)
            {
                var x = random.NextDouble();
                if (x != 0.0) return x;
            }
        }

        public static double NextDouble()
        {
            var x = NextDoubleExceptZero();
            var y = NextDoubleExceptZero();

            return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(2 * Math.PI * y);
        }

        public static IEnumerable<double> NextDoubles()
        {
            while (true)
            {
                var x = NextDoubleExceptZero();
                var y = NextDoubleExceptZero();

                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(2 * Math.PI * y);
                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Cos(2 * Math.PI * y);
            }
        }
    }
}
