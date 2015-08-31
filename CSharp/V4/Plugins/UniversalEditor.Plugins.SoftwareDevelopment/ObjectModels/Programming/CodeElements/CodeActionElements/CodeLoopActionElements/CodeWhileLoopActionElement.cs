using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements.CodeActionElements.CodeLoopActionElements
{
    public class CodeWhileLoopActionElement : CodeActionElement
    {
        private IConditionalStatement mvarCondition = null;
        public IConditionalStatement Condition
        {
            get { return mvarCondition; }
        }
    }
}
