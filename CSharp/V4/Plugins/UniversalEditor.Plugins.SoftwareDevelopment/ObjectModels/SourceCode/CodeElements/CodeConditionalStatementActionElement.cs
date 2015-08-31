using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeConditionalStatementActionElement : CodeElementContainerElement
    {
        private bool mvarNegate = false;
        public bool Negate { get { return mvarNegate; } set { mvarNegate = value; } }

        private CodeElementReference mvarExpression = null;
        public CodeElementReference Expression { get { return mvarExpression; } set { mvarExpression = value; } }

    }
}
