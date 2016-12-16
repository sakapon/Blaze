using System;
using System.Linq;
using Blaze.Propositions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Blaze.Propositions.Formula;

namespace UnitTest.Propositions
{
    [TestClass]
    public class FormulaTest
    {
        [TestMethod]
        public void GetDescendants_1()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = Imply(p & q, p | q);

            var descendants = p_q.GetDescendants().ToArray();
            Assert.AreEqual(7, descendants.Length);
            var ands = p_q.GetDescendants<AndFormula>().ToArray();
            Assert.AreEqual(1, ands.Length);
        }

        [TestMethod]
        public void Variable_1()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var p_q = Imply(p & q, p | q);

            var actual = p_q.GetVariables();
            var expected = new[] { p, q };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Tautology_1()
        {
            Assert.AreEqual(true, True.IsTautology());
            Assert.AreEqual(false, (!True).IsTautology());
            Assert.AreEqual(false, False.IsTautology());
            Assert.AreEqual(true, (!False).IsTautology());
        }

        [TestMethod]
        public void Tautology_2()
        {
            var p = Variable("P");

            Assert.AreEqual(false, p.IsTautology());
            Assert.AreEqual(false, (p & !p).IsTautology());
            Assert.AreEqual(true, (p | !p).IsTautology());
            Assert.AreEqual(true, Imply(p, p).IsTautology());
            Assert.AreEqual(true, Equivalent(p, p).IsTautology());
        }

        [TestMethod]
        public void Tautology_3()
        {
            var p = Variable("P");
            var q = Variable("Q");
            var r = Variable("R");

            var target1 = Equivalent(!(p & q), !p | !q);
            Assert.AreEqual(true, target1.IsTautology());

            var target2 = Equivalent(!(p | q), !p & !q);
            Assert.AreEqual(true, target2.IsTautology());

            // p & (p => q) => q
            var target3 = Imply(p & Imply(p, q), q);
            Assert.AreEqual(true, target3.IsTautology());

            // 三段論法
            // (p => q) & (q => r) => (p => r)
            var syllogism = Imply(Imply(p, q) & Imply(q, r), Imply(p, r));
            Assert.AreEqual(true, syllogism.IsTautology());

            // 対偶
            var contraposition = Equivalent(Imply(p, q), Imply(!q, !p));
            Assert.AreEqual(true, contraposition.IsTautology());
        }

        [TestMethod]
        public void Contradiction_1()
        {
            Assert.AreEqual(false, True.IsContradiction());
            Assert.AreEqual(true, (!True).IsContradiction());
            Assert.AreEqual(true, False.IsContradiction());
            Assert.AreEqual(false, (!False).IsContradiction());
        }

        [TestMethod]
        public void Contradiction_2()
        {
            var p = Variable("P");

            Assert.AreEqual(false, p.IsContradiction());
            Assert.AreEqual(true, (p & !p).IsContradiction());
            Assert.AreEqual(false, (p | !p).IsContradiction());
            Assert.AreEqual(false, Imply(p, !p).IsContradiction());
            Assert.AreEqual(true, Equivalent(p, !p).IsContradiction());
        }

        [TestMethod]
        public void Contradiction_3()
        {
            var p = Variable("P");
            var q = Variable("Q");

            var target1 = Imply(p, q) & Imply(p, !q);
            Assert.AreEqual(false, target1.IsContradiction());

            var target2 = p & Imply(p, q) & Imply(p, !q);
            Assert.AreEqual(true, target2.IsContradiction());
        }
    }
}
