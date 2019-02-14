using System;
using static System.Math;
using static Blaze.Randomization.Lab.NormalDistribution;

namespace Blaze.Randomization.Lab
{
    public static class BinomialDistribution
    {
        // -M <= x <= M
        // 正規分布で近似します。
        public static int NextInt32(int maxAbsValue)
        {
            var sigma = Sqrt(2 * maxAbsValue) / 2.0;

            var x = Truncate(maxAbsValue + 0.5, sigma);
            return (int)Round(x, MidpointRounding.AwayFromZero);
        }

        // m <= x <= M
        public static int NextInt32ByMinMax(int minValue, int maxValue)
        {
            var mean = (maxValue + minValue) / 2.0;
            var sigma = Sqrt(maxValue - minValue) / 2.0;
            var maxAbsValue = (maxValue - minValue) / 2.0 + 0.5;

            var x = Truncate(maxAbsValue, sigma);
            return (int)Round(x + mean, MidpointRounding.AwayFromZero);
        }
    }
}
