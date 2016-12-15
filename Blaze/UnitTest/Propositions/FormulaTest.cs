using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Blaze.Propositions.Formula;

namespace UnitTest.Propositions
{
    [TestClass]
    public class FormulaTest
    {
        [TestMethod]
        public void IsTrue_And()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = p & q;

            Assert.AreEqual(null, p_q.IsTrue);
            q.Value = false;
            Assert.AreEqual(false, p_q.IsTrue);
            q.Value = true;
            Assert.AreEqual(null, p_q.IsTrue);

            p.Value = false;
            q.Value = false;
            Assert.AreEqual(false, p_q.IsTrue);
            q.Value = true;
            Assert.AreEqual(false, p_q.IsTrue);

            p.Value = true;
            Assert.AreEqual(true, p_q.IsTrue);
        }

        [TestMethod]
        public void IsTrue_Or()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = p | q;

            Assert.AreEqual(null, p_q.IsTrue);
            q.Value = false;
            Assert.AreEqual(null, p_q.IsTrue);
            q.Value = true;
            Assert.AreEqual(true, p_q.IsTrue);

            p.Value = false;
            q.Value = false;
            Assert.AreEqual(false, p_q.IsTrue);
            q.Value = true;
            Assert.AreEqual(true, p_q.IsTrue);

            p.Value = true;
            Assert.AreEqual(true, p_q.IsTrue);
        }
    }
}
