using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Blaze.Propositions.Formula;

namespace UnitTest.Propositions
{
    [TestClass]
    public class FormulaTest
    {
        [TestMethod]
        public void TruthValue_And()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = p & q;

            Assert.AreEqual(null, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);
            q.Value = true;
            Assert.AreEqual(null, p_q.TruthValue);

            p.Value = false;
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);
            q.Value = true;
            Assert.AreEqual(false, p_q.TruthValue);

            p.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
        }

        [TestMethod]
        public void TruthValue_Or()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = p | q;

            Assert.AreEqual(null, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(null, p_q.TruthValue);
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);

            p.Value = false;
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);

            p.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
        }
    }
}
