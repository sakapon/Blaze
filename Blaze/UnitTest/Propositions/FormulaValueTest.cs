using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Blaze.Propositions.Formula;

namespace UnitTest.Propositions
{
    [TestClass]
    public class FormulaValueTest
    {
        [TestMethod]
        public void TruthValue_Constant()
        {
            Assert.AreEqual(true, True.TruthValue);
            Assert.AreEqual(false, False.TruthValue);

            var p = Variable("P");
            Assert.AreEqual(null, (p & True).TruthValue);
            Assert.AreEqual(true, (p | True).TruthValue);
            Assert.AreEqual(false, (p & False).TruthValue);
            Assert.AreEqual(null, (p | False).TruthValue);
        }

        [TestMethod]
        public void TruthValue_Negation()
        {
            var p = Variable("P");
            var _p = !p;

            Assert.AreEqual(null, _p.TruthValue);
            p.Value = false;
            Assert.AreEqual(true, _p.TruthValue);
            p.Value = true;
            Assert.AreEqual(false, _p.TruthValue);
        }

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

        [TestMethod]
        public void TruthValue_Implication()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = Imply(p, q);

            p.Value = true;
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);

            p.Value = false;
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(true, p_q.TruthValue);
        }

        [TestMethod]
        public void TruthValue_Equivalence()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = Equivalent(p, q);

            p.Value = true;
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);

            p.Value = false;
            q.Value = true;
            Assert.AreEqual(false, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(true, p_q.TruthValue);
        }

        [TestMethod]
        public void TruthValue_Xor()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = p ^ q;

            p.Value = true;
            q.Value = true;
            Assert.AreEqual(false, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(true, p_q.TruthValue);

            p.Value = false;
            q.Value = true;
            Assert.AreEqual(true, p_q.TruthValue);
            q.Value = false;
            Assert.AreEqual(false, p_q.TruthValue);
        }
    }
}
