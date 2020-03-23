using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements
{
    public class CodeWhileLoopActionElement : CodeElementContainerElement
    {
        private IConditionalStatement mvarCondition = null;
        public IConditionalStatement Condition
        {
            get { return mvarCondition; }
        }
    }
}
