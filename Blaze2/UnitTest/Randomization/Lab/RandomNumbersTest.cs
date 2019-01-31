using System;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class RandomNumbersTest
    {
        [TestMethod]
        public void GenerateByte_1()
        {
            for (var i = 0; i < 100; i++)
            {
                var x = RandomNumbers.GenerateByte();
                Console.WriteLine(x);
            }
        }

        [TestMethod]
        public void GenerateDouble_From0To1_1()
        {
            for (var i = 0; i < 100; i++)
            {
                var x = RandomNumbers.GenerateDouble_From0To1();
                Console.WriteLine($"{x:F3}");
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 1);
            }
        }
    }
}
