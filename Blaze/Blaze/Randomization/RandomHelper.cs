using System;
using System.Collections.Generic;
using System.Linq;

namespace Blaze.Randomization
{
    /// <summary>
    /// Provides a set of methods for random numbers.
    /// </summary>
    public static class RandomHelper
    {
        static readonly Random _random = new Random();

        public static double NextDouble(double maxValue)
        {
            if (maxValue < 0) throw new ArgumentOutOfRangeException("maxValue", "maxValue is less than 0.");

            return NextDouble(0, maxValue);
        }

        public static double NextDouble(double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * _random.NextDouble();
        }

        public static IEnumerable<int> ShuffleRange(int start, int count)
        {
            return Enumerable.Range(start, count).Shuffle();
        }

        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            var l = source.ToList();

            while (l.Count > 0)
            {
                var index = _random.Next(l.Count);
                yield return l[index];
                l.RemoveAt(index);
            }
        }

        public static T GetRandomElement<T>(this IList<T> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (source.Count == 0) throw new ArgumentException("The source must not be empty.", "source");

            return source[_random.Next(source.Count)];
        }

        public static IEnumerable<T> GetRandomPiece<T>(this IList<T> source, int count)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (count < 0) throw new ArgumentOutOfRangeException("count", count, "The value must be non-negative.");

            if (count == 0) return Enumerable.Empty<T>();
            if (count >= source.Count) return source;

            return Enumerable.Range(_random.Next(source.Count - count + 1), count)
                .Select(i => source[i]);
        }

        // The sum of values must be 1.
        public static int GetRandomIndex(this IList<double> probabilities)
        {
            if (probabilities == null) throw new ArgumentNullException(nameof(probabilities));
            if (probabilities.Count == 0) throw new ArgumentException("The source must not be empty.", nameof(probabilities));

            var v = _random.NextDouble();

            var sum = 0.0;
            for (var i = 0; i < probabilities.Count; i++)
            {
                sum += probabilities[i];
                if (v < sum) return i;
            }

            return probabilities.Count - 1;
        }

        // The sum of values must be 1.
        public static T GetRandomElement<T>(this Dictionary<T, double> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) throw new ArgumentException("The source must not be empty.", nameof(source));

            var v = _random.NextDouble();

            var sum = 0.0;
            foreach (var p in source)
            {
                sum += p.Value;
                if (v < sum) return p.Key;
            }

            return source.Last().Key;
        }
    }
}
