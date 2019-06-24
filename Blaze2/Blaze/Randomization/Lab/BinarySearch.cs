using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    /// <summary>
    /// Provides a set of methods for random numbers using binary search.
    /// </summary>
    public static class BinarySearch
    {
        static readonly Random random = new Random();

        // 累積分布関数 (単調増加数列) であることを前提とします。
        // a[i] <= x < a[i+1] のとき、i を返します。
        // https://github.com/sakapon/Harmonia/blob/master/Harmonia/Harmonia/Search/BinarySearch.cs
        public static int GetIndexByRange(this IList<double> values, double value)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Count == 0) return -1;

            return Search(0, values.Count);

            int Search(int start, int count)
            {
                if (count == 1)
                    return values[start] <= value ? start : -1;

                var c1 = count >> 1;
                var s2 = start + c1;
                return value < values[s2] ? Search(start, c1) : Search(s2, count - c1);
            }
        }

        // 累積分布関数 (単調増加数列) であることを前提とします。
        // 最初の要素は 0。最後の要素は 1 未満でも 1 以上でも可。
        // 入力が確率分布関数の場合、累積を求めることになるため、RandomHelper.GetRandomIndex メソッドを使うのが速いです。
        public static int GetRandomIndexByCumulation(this IList<double> cumulation)
        {
            if (cumulation == null) throw new ArgumentNullException(nameof(cumulation));
            if (cumulation.Count == 0) throw new ArgumentException("The source must not be empty.", nameof(cumulation));

            var v = random.NextDouble();
            return GetIndexByRange(cumulation, v);
        }
    }
}
