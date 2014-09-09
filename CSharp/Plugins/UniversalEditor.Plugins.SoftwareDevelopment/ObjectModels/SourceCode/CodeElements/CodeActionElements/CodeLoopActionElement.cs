using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeActionElements
{
    public abstract class CodeLoopActionElement : CodeActionElement
    {
        private CodeActionElementCollection mvarActions = new CodeActionElementCollection();
        public CodeActionElementCollection Actions
        {
            get { return mvarActions; }
        }
    }
}
