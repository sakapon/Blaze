using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Blaze.Propositions
{
    [DebuggerDisplay(@"\{{ToString()}: {TruthValue}\}")]
    public abstract class Formula
    {
        internal static Formula[] EmptyFormulas { get; } = new Formula[0];

        public static Formula True { get; } = new ConstantFormula(true);
        public static Formula False { get; } = new ConstantFormula(false);

        public static VariableFormula<TStatement> Variable<TStatement>(TStatement statement) => new VariableFormula<TStatement>(statement);
        public static Formula Imply(Formula v1, Formula v2) => new ImplicationFormula(v1, v2);
        public static Formula Equivalent(Formula v1, Formula v2) => new EquivalenceFormula(v1, v2);

        public static Formula And(IEnumerable<Formula> formulas) => formulas.Aggregate((v1, v2) => v1 & v2);
        public static Formula Or(IEnumerable<Formula> formulas) => formulas.Aggregate((v1, v2) => v1 | v2);
        public static Formula Xor(IEnumerable<Formula> formulas) => formulas.Aggregate((v1, v2) => v1 ^ v2);

        public static Formula operator !(Formula v) => new NegationFormula(v);
        public static Formula operator &(Formula v1, Formula v2) => new AndFormula(v1, v2);
        public static Formula operator |(Formula v1, Formula v2) => new OrFormula(v1, v2);
        public static Formula operator ^(Formula v1, Formula v2) => new XorFormula(v1, v2);

        public abstract Formula[] Children { get; }
        public abstract bool? TruthValue { get; }

        // GetDescendants メソッドでは、要素が重複する可能性があります。
        public IEnumerable<Formula> GetDescendants() => new[] { this }.Concat(Children.SelectMany(f => f.GetDescendants()));
        public IEnumerable<TFormula> GetDescendants<TFormula>() where TFormula : Formula => GetDescendants().OfType<TFormula>();
        public VariableFormula[] GetVariables() => GetDescendants<VariableFormula>().Distinct().ToArray();
    }
}
