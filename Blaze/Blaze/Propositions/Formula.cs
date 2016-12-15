using System;
using System.Collections.Generic;
using System.Linq;

namespace Blaze.Propositions
{
    public abstract class Formula
    {
        internal static Formula[] EmptyFormulas { get; } = new Formula[0];

        public abstract Formula[] Children { get; }
        public abstract bool? IsTrue { get; }
    }
}
