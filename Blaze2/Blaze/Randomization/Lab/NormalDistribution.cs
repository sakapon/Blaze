using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class NormalDistribution
    {
        internal const double DefaultMaxSigma = 3.0;
        const double TwoPi = 2 * Math.PI;
        static readonly Random random = new Random();
        static readonly IEnumerator<double> doublesEnumerator = NextDoubles().GetEnumerator();

        // 0 < x < 1
        static double NextDoubleExceptZero()
        {
            while (true)
            {
                var x = random.NextDouble();
                if (x != 0.0) return x;
            }
        }

        public static IEnumerable<double> NextDoubles()
        {
            while (true)
            {
                var x = NextDoubleExceptZero();
                var y = NextDoubleExceptZero();

                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(TwoPi * y);
                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Cos(TwoPi * y);
            }
        }

        public static double NextDouble()
        {
            doublesEnumerator.MoveNext();
            return doublesEnumerator.Current;
        }

        // -σ < x < σ
        public static double NextDoubleInSigma(double maxSigma = DefaultMaxSigma)
        {
            if (maxSigma < 1) throw new ArgumentOutOfRangeException(nameof(maxSigma), maxSigma, "The value must be large enough.");

            // 範囲外の値を無視します。
            while (true)
            {
                var x = NextDouble();
                if (Math.Abs(x) < maxSigma) return x;
            }
        }

        // -M < x < M
        // Ignores values out of the range.
        public static double NextDoubleInRange(double maxAbsValue, double sigma = 1)
        {
            if (maxAbsValue < sigma) throw new ArgumentOutOfRangeException(nameof(maxAbsValue), maxAbsValue, "The value must be large enough.");

            while (true)
            {
                var x = NextDouble() * sigma;
                if (Math.Abs(x) < maxAbsValue) return x;
            }
        }

        public static double NextDoubleWith(double sigma = 1, double mean = 0) =>
            NextDouble() * sigma + mean;

        // -M < x < M
        public static double NextDouble(double maxAbsValue, double maxSigma = DefaultMaxSigma)
        {
            var x = NextDoubleInSigma(maxSigma);
            return x * maxAbsValue / maxSigma;
        }

        // -M <= x <= M
        public static int NextInt32(int maxAbsValue, double maxSigma = DefaultMaxSigma)
        {
            var x = NextDouble(maxAbsValue + 0.5, maxSigma);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        // -M <= x <= M
        public static int NextInt32_2(int maxAbsValue)
        {
            var sigma = Math.Sqrt(2 * maxAbsValue) / 2.0;

            var x = NextDoubleInRange(maxAbsValue + 0.5, sigma);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        // 0 <= x <= M
        public static int NextInt32ByBinomial(int maxValue)
        {
            var mean = maxValue / 2.0;
            var sigma = Math.Sqrt(maxValue) / 2.0;

            var x = NextDoubleInRange(mean + 0.5, sigma);
            return (int)Math.Round(x + mean, MidpointRounding.AwayFromZero);
        }
    }
}
