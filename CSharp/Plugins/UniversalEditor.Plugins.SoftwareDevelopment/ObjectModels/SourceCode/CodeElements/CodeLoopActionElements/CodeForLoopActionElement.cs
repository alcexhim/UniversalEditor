using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements
{
    public class CodeForLoopActionElement : CodeLoopElement
    {

        private CodeElementReference mvarInitialization = null;
        public CodeElementReference Initialization { get { return mvarInitialization; } set { mvarInitialization = value; } }
        private CodeElementReference mvarCondition = null;
        public CodeElementReference Condition { get { return mvarCondition; } set { mvarCondition = value; } }
        private CodeElementReference mvarIncrement = null;
        public CodeElementReference Increment { get { return mvarIncrement; } set { mvarIncrement = value; } }
    }
}
