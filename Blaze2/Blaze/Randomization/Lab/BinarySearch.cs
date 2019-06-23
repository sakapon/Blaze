using System;
using System.Collections.Generic;

namespace Blaze.Randomization.Lab
{
    public static class BinarySearch
    {
        static readonly Random random = new Random();

        // 累積分布関数 (単調増加数列) であることを前提とします。
        // a[i] <= x < a[i+1] のとき、i を返します。
        public static int GetIndexByRange(this double[] array, double value)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length == 0) return -1;

            return Search(0, array.Length);

            int Search(int start, int count)
            {
                if (count == 1)
                    return array[start] <= value ? start : -1;

                var c1 = count >> 1;
                var s2 = start + c1;
                return value < array[s2] ? Search(start, c1) : Search(s2, count - c1);
            }
        }

        // 累積分布関数 (単調増加数列) であることを前提とします。
        // 最初の要素は 0。最後の要素は 1 未満でも 1 以上でも可。
        public static int GetRandomIndexByCumulation(this double[] cumulation)
        {
            var v = random.NextDouble();
            return GetIndexByRange(cumulation, v);
        }
    }
}
