using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class NormalDistribution
    {
        internal const double DefaultMaxSigma = 2.5; // ≒ 99 %
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

                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(2 * Math.PI * y);
                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Cos(2 * Math.PI * y);
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
        public static double NextDouble(double maxAbsValue, double maxSigma = DefaultMaxSigma)
        {
            var x = NextDoubleInSigma(maxSigma);
            return x * maxAbsValue / maxSigma;
        }

        // -M < x < M
        public static int NextInt32(int maxAbsValue, double maxSigma = DefaultMaxSigma)
        {
            var x = NextDouble(maxAbsValue + 0.5, maxSigma);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }
    }
}
