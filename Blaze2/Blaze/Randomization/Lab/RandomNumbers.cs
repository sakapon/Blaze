using System;

namespace Blaze.Randomization.Lab
{
    public static class RandomNumbers
    {
        public static Random CreateRandomGenerator()
        {
            return new Random((int)DateTime.UtcNow.Ticks);
        }

        public static int GenerateInt32(int minValue, int maxValueEx)
        {
            return minValue + GenerateInt32(maxValueEx - minValue);
        }

        public static int GenerateInt32(int maxValueEx)
        {
            var e = GenerateDouble_From0To1();
            return (int)(maxValueEx * e);
        }

        public static byte GenerateByte()
        {
            var bytes = RandomData.GenerateBytes(1);
            return bytes[0];
        }

        public static int GenerateInt32()
        {
            var bytes = RandomData.GenerateBytes(4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static long GenerateInt64()
        {
            var bytes = RandomData.GenerateBytes(8);
            return BitConverter.ToInt64(bytes, 0);
        }

        public static double GenerateDouble()
        {
            var bytes = RandomData.GenerateBytes(8);
            return BitConverter.ToDouble(bytes, 0);
        }

        // 2 ^ 48
        const double Power_2_48 = 0x1000000000000;

        // 0 <= x < 1
        public static double GenerateDouble_From0To1()
        {
            // 48 bits
            var bytes = RandomData.GenerateBytes(8);
            bytes[6] = 0;
            bytes[7] = 0;

            var l = BitConverter.ToInt64(bytes, 0);
            return l / Power_2_48;
        }
    }
}
