using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class NormalDistribution
    {
        const double TwoPi = 2 * Math.PI;
        static readonly Random random = new Random();
        static readonly IEnumerator<double> standardsEnumerator = Standards().GetEnumerator();

        // 0 < x < 1
        static double UniformExceptZero()
        {
            while (true)
            {
                var x = random.NextDouble();
                if (x != 0.0) return x;
            }
        }

        public static IEnumerable<double> Standards()
        {
            while (true)
            {
                var x = UniformExceptZero();
                var y = UniformExceptZero();

                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Sin(TwoPi * y);
                yield return Math.Sqrt(-2 * Math.Log(x)) * Math.Cos(TwoPi * y);
            }
        }

        /// <summary>
        /// 最も基本となる標準正規分布に従ったランダム値を取得します。値の範囲は実数全体です。
        /// </summary>
        /// <returns>標準正規分布に従ったランダム値。</returns>
        public static double Standard()
        {
            standardsEnumerator.MoveNext();
            return standardsEnumerator.Current;
        }

        public static double Next(double sigma = 1, double mean = 0) =>
            Standard() * sigma + mean;

        // -M < x < M
        // Ignores values out of the range.
        internal static double TruncateByMaxAbs(double maxAbsValue, double sigma = 1)
        {
            if (sigma <= 0) throw new ArgumentOutOfRangeException(nameof(sigma), sigma, "The value must be positive.");
            if (maxAbsValue < sigma) throw new ArgumentOutOfRangeException(nameof(maxAbsValue), maxAbsValue, "The value must be large enough.");

            while (true)
            {
                var x = Standard() * sigma;
                if (Math.Abs(x) < maxAbsValue) return x;
            }
        }

        // -M < x < M
        [Obsolete]
        public static double NextDouble_0(double maxAbsValue, double maxSigma = 3)
        {
            var x = TruncateByMaxAbs(maxSigma);
            return x * maxAbsValue / maxSigma;
        }

        // -M <= x <= M
        [Obsolete]
        public static int NextInt32_0(int maxAbsValue, double maxSigma = 3)
        {
            var x = NextDouble_0(maxAbsValue + 0.5, maxSigma);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        // -M <= x <= M
        public static int NextInt32(int maxAbsValue)
        {
            var sigma = Math.Sqrt(2 * maxAbsValue) / 2.0;

            var x = TruncateByMaxAbs(maxAbsValue + 0.5, sigma);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        // 0 <= x <= M
        public static int NextInt32ByBinomial(int maxValue)
        {
            var mean = maxValue / 2.0;
            var sigma = Math.Sqrt(maxValue) / 2.0;

            var x = TruncateByMaxAbs(mean + 0.5, sigma);
            return (int)Math.Round(x + mean, MidpointRounding.AwayFromZero);
        }
    }
}
