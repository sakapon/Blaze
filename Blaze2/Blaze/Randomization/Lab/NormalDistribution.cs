using System;
using System.Collections.Generic;
using static System.Math;

namespace Blaze.Randomization.Lab
{
    /// <summary>
    /// Provides a set of methods for normal distribution.
    /// </summary>
    public static class NormalDistribution
    {
        internal const double DefaultConfidenceInSigma = 3.0;
        const double TwoPi = 2 * PI;
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

                yield return Sqrt(-2 * Log(x)) * Sin(TwoPi * y);
                yield return Sqrt(-2 * Log(x)) * Cos(TwoPi * y);
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
        internal static double Truncate(double maxAbsValue, double sigma = 1)
        {
            if (sigma <= 0) throw new ArgumentOutOfRangeException(nameof(sigma), sigma, "The value must be positive.");
            if (maxAbsValue < sigma) throw new ArgumentOutOfRangeException(nameof(maxAbsValue), maxAbsValue, "The value must be large enough.");

            while (true)
            {
                var x = Standard() * sigma;
                if (Abs(x) < maxAbsValue) return x;
            }
        }

        // -M < x < M
        public static double NextDouble(double maxAbsValue, double confidenceInSigma = DefaultConfidenceInSigma)
        {
            var sigma = maxAbsValue / confidenceInSigma;
            return Truncate(maxAbsValue, sigma);
        }

        // m < x < M
        public static double NextDoubleByMinMax(double minValue, double maxValue, double confidenceInSigma = DefaultConfidenceInSigma)
        {
            var x = NextDouble((maxValue - minValue) / 2, confidenceInSigma);
            return x + (maxValue + minValue) / 2;
        }

        // -M <= x <= M
        [Obsolete]
        public static int NextInt32_0(int maxAbsValue, double confidenceInSigma = DefaultConfidenceInSigma)
        {
            var x = NextDouble(maxAbsValue + 0.5, confidenceInSigma);
            return (int)Round(x, MidpointRounding.AwayFromZero);
        }
    }
}
