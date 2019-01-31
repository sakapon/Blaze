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
        VariableFormula p;
        VariableFormula q;
        VariableFormula r;

        [TestInitialize]
        public void Initialize()
        {
            p = Variable("P");
            q = Variable("Q");
            r = Variable("R");
        }

        [TestMethod]
        public void GetDescendants_1()
        {
            var p_q = Imply(p & q, p | q);

            var descendants = p_q.GetDescendants().ToArray();
            Assert.AreEqual(7, descendants.Length);
            var ands = p_q.GetDescendants().OfType<AndFormula>().ToArray();
            Assert.AreEqual(1, ands.Length);
        }

        [TestMethod]
        public void GetVariables_1()
        {
            var p_q = Imply(p & q, p | q);

            var actual = p_q.GetVariables().ToArray();
            var expected = new[] { p, q };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOrCreateVariable_1()
        {
            var p_q = Imply(p & q, p | q);

            Assert.AreEqual(p, p_q.GetOrCreateVariable("P"));
            var s = p_q.GetOrCreateVariable("S");
            Assert.AreEqual(false, p_q.GetVariables().Contains(s));
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
            Assert.AreEqual(false, p.IsTautology());
            Assert.AreEqual(false, (p & !p).IsTautology());
            Assert.AreEqual(true, (p | !p).IsTautology());
            Assert.AreEqual(true, Imply(p, p).IsTautology());
            Assert.AreEqual(true, Equivalent(p, p).IsTautology());
        }

        [TestMethod]
        public void Tautology_3()
        {
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

            // 背理法
            var absurd = Imply(Imply(p, q) & Imply(p, !q), !p);
            Assert.AreEqual(true, absurd.IsTautology());

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
            Assert.AreEqual(false, p.IsContradiction());
            Assert.AreEqual(true, (p & !p).IsContradiction());
            Assert.AreEqual(false, (p | !p).IsContradiction());
            Assert.AreEqual(false, Imply(p, !p).IsContradiction());
            Assert.AreEqual(true, Equivalent(p, !p).IsContradiction());
        }

        [TestMethod]
        public void Contradiction_3()
        {
            var target1 = Imply(p, q) & Imply(p, !q);
            Assert.AreEqual(false, target1.IsContradiction());

            var target2 = p & Imply(p, q) & Imply(p, !q);
            Assert.AreEqual(true, target2.IsContradiction());
        }

        [TestMethod]
        public void Determine_1()
        {
            // p & (p => q) => q
            (p & Imply(p, q)).Determine(q);
            Assert.AreEqual(true, q.Value);
        }

        [TestMethod]
        public void Determine_1_1()
        {
            // p & (p => q) => q
            p.Value = true;
            Imply(p, q).Determine(q);
            Assert.AreEqual(true, q.Value);
        }

        [TestMethod]
        public void Determine_2()
        {
            // 背理法
            (Imply(p, q) & Imply(p, !q)).Determine(p);
            Assert.AreEqual(false, p.Value);
        }

        [TestMethod]
        public void Determine_3()
        {
            // 場合分け
            ((p | q) & Imply(p, r) & Imply(q, r)).Determine(r);
            Assert.AreEqual(true, r.Value);
        }

        [TestMethod]
        public void Determine_4()
        {
            Imply(p, q).Determine(q);
            Assert.AreEqual(null, q.Value);
        }

        [TestMethod]
        public void Knights_1_3()
        {
            var k1 = Variable("K1");
            var k2 = Variable("K2");

            // Q 1.3
            var kb = Equivalent(k1, !k1 & !k2);

            kb.Determine(k1);
            Assert.AreEqual(false, k1.Value);
            kb.Determine(k2);
            Assert.AreEqual(true, k2.Value);
        }

        [TestMethod]
        public void Knights_1_4()
        {
            var k1 = Variable("K1");
            var k2 = Variable("K2");

            // Q 1.4
            var kb = Equivalent(k1, !k1 | !k2);

            kb.Determine(k1);
            Assert.AreEqual(true, k1.Value);
            kb.Determine(k2);
            Assert.AreEqual(false, k2.Value);
        }

        [TestMethod]
        public void Knights_1_5()
        {
            var k1 = Variable("K1");
            var k2 = Variable("K2");

            // Q 1.5
            var kb = Equivalent(k1, Equivalent(k1, k2));

            kb.Determine(k1);
            Assert.AreEqual(null, k1.Value);
            kb.Determine(k2);
            Assert.AreEqual(true, k2.Value);
        }

        [TestMethod]
        public void Knights_1_20()
        {
            var k1 = Variable("K1");
            var k2 = Variable("K2");

            // Q 1.20
            var kb = Equivalent(k1, k1 & k2);

            var kb_t = kb & Equivalent(k2, k1);
            kb_t.Determine(k1);
            Assert.AreEqual(null, k1.Value);
            kb_t.Determine(k2);
            Assert.AreEqual(null, k2.Value);

            var kb_f = kb & Equivalent(k2, !k1);
            kb_f.Determine(k1);
            Assert.AreEqual(false, k1.Value);
            kb_f.Determine(k2);
            Assert.AreEqual(true, k2.Value);
        }
    }
}
