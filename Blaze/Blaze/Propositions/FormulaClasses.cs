using System;
using System.Diagnostics;

namespace Blaze.Propositions
{
    [DebuggerDisplay(@"\{{Statement}: {Value}\}")]
    public class VariableFormula<TStatement> : Formula
    {
        public override Formula[] Children => EmptyFormulas;
        public override bool? IsTrue => Value;

        public bool? Value { get; set; }
        public TStatement Statement { get; }

        public VariableFormula(TStatement statement)
        {
            Statement = statement;
        }

        public override string ToString() => $"{Statement}";
    }

    public class ConstantFormula : Formula
    {
        public override Formula[] Children => EmptyFormulas;
        public override bool? IsTrue => Value;

        public bool Value { get; }

        public ConstantFormula(bool value)
        {
            Value = value;
        }

        public override string ToString() => Value ? "t" : "f";
    }

    public abstract class UnaryFormula : Formula
    {
        public override Formula[] Children { get; }

        public Formula Operand { get; }

        protected UnaryFormula(Formula operand)
        {
            Operand = operand;
            Children = new[] { Operand };
        }
    }

    public abstract class BinaryFormula : Formula
    {
        public override Formula[] Children { get; }

        public Formula Operand1 { get; }
        public Formula Operand2 { get; }

        protected BinaryFormula(Formula operand1, Formula operand2)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Children = new[] { Operand1, Operand2 };
        }
    }

    public class NegationFormula : UnaryFormula
    {
        public override bool? IsTrue => !Operand.IsTrue;

        public NegationFormula(Formula operand) : base(operand) { }

        public override string ToString() => $"～({Operand})";
    }

    public class AndFormula : BinaryFormula
    {
        public override bool? IsTrue => Operand1.IsTrue & Operand2.IsTrue;

        public AndFormula(Formula operand1, Formula operand2) : base(operand1, operand2) { }

        public override string ToString() => $"({Operand1})∧({Operand2})";
    }

    public class OrFormula : BinaryFormula
    {
        public override bool? IsTrue => Operand1.IsTrue | Operand2.IsTrue;

        public OrFormula(Formula operand1, Formula operand2) : base(operand1, operand2) { }

        public override string ToString() => $"({Operand1})∨({Operand2})";
    }

    public class ImplicationFormula : BinaryFormula
    {
        public override bool? IsTrue => !Operand1.IsTrue | Operand2.IsTrue;

        public ImplicationFormula(Formula operand1, Formula operand2) : base(operand1, operand2) { }

        public override string ToString() => $"({Operand1})⇒({Operand2})";
    }

    public class EquivalenceFormula : BinaryFormula
    {
        public override bool? IsTrue => !(Operand1.IsTrue ^ Operand2.IsTrue);

        public EquivalenceFormula(Formula operand1, Formula operand2) : base(operand1, operand2) { }

        public override string ToString() => $"({Operand1})≡({Operand2})";
    }

    public class XorFormula : BinaryFormula
    {
        public override bool? IsTrue => Operand1.IsTrue ^ Operand2.IsTrue;

        public XorFormula(Formula operand1, Formula operand2) : base(operand1, operand2) { }

        public override string ToString() => $"({Operand1})≢({Operand2})";
    }
}
