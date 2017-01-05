using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Blaze.Propositions
{
    [DebuggerDisplay(@"\{{ToString()}: {TruthValue}\}")]
    public abstract class Formula
    {
        internal static readonly Formula[] EmptyFormulas = new Formula[0];

        public static Formula True { get; } = new ConstantFormula(true);
        public static Formula False { get; } = new ConstantFormula(false);

        public static VariableFormula<TStatement> Variable<TStatement>(TStatement statement) => new VariableFormula<TStatement>(statement);
        public static VariableFormula<TStatement> Variable<TStatement>(TStatement statement, bool? value)
        {
            var v = new VariableFormula<TStatement>(statement);
            v.Value = value;
            return v;
        }

        public static Formula Negate(Formula v) => new NegationFormula(v);
        public static Formula And(Formula v1, Formula v2) => new AndFormula(v1, v2);
        public static Formula Or(Formula v1, Formula v2) => new OrFormula(v1, v2);
        public static Formula Imply(Formula v1, Formula v2) => new ImplicationFormula(v1, v2);
        public static Formula Equivalent(Formula v1, Formula v2) => new EquivalenceFormula(v1, v2);
        public static Formula Xor(Formula v1, Formula v2) => new XorFormula(v1, v2);

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
        public IEnumerable<VariableFormula> GetVariables() => GetDescendants().OfType<VariableFormula>().Distinct();

        // 指定された主張を表す変数を取得します。なければ作成します。
        public VariableFormula<TStatement> GetOrCreateVariable<TStatement>(TStatement statement) =>
            GetDescendants()
                .OfType<VariableFormula<TStatement>>()
                .Distinct()
                .FirstOrDefault(v => Equals(v.Statement, statement)) ??
            Variable(statement);

        public VariableFormula<TStatement> GetOrCreateVariable<TStatement>(VariableFormula<TStatement> variable) => GetOrCreateVariable(variable.Statement);

        static readonly bool[] bools = new[] { true, false };
        static readonly IEnumerable<bool> initialCombinations = new[] { false };

        public bool IsTautology()
        {
            var variables = GetVariables()
                .Where(v => v.Value == null)
                .ToArray();

            try
            {
                // 変数の値のすべての組合せを列挙します。
                return variables
                    .Aggregate(initialCombinations, (q, v) => q.SelectMany(_ => bools.Select(b => { v.Value = b; return _; })))
                    .All(_ => TruthValue.Value);
            }
            finally
            {
                foreach (var v in variables)
                    v.Value = null;
            }
        }

        public bool IsContradiction() => (!this).IsTautology();

        public void Determine(VariableFormula variable)
        {
            if (Imply(this, variable).IsTautology())
                variable.Value = true;
            else if (Imply(this, !variable).IsTautology())
                variable.Value = false;
        }
    }
}
