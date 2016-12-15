using System;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class RandomNumbersTest
    {
        [TestMethod]
        public void GenerateDouble_From0To1_1()
        {
            for (var i = 0; i < 1000; i++)
            {
                var x = RandomNumbers.GenerateDouble_From0To1();
                Console.WriteLine(x);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x < 1);
            }
        }
    }
}
