using System;
using System.Collections.Generic;
using System.Linq;
using Blaze.Randomization.Lab;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Randomization.Lab
{
    [TestClass]
    public class NormalDistributionTest
    {
        [TestMethod]
        public void NextDouble()
        {
            for (var i = 0; i < 100; i++)
                Console.WriteLine(NormalDistribution.NextDouble());
        }

        [TestMethod]
        public void NextDouble_Rare()
        {
            for (var i = 0; i < 10000; i++)
            {
                var x = NormalDistribution.NextDouble();
                if (Math.Abs(x) > 3)
                    Console.WriteLine(x);
            }
        }
    }
}
