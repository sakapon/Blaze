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
    }
}
