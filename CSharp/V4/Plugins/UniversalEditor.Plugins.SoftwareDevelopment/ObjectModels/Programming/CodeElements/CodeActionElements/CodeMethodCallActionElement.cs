using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements.CodeActionElements
{
    public class CodeMethodCallActionElement : CodeActionElement
    {
        private string mvarObjectName = String.Empty;
        public string ObjectName
        {
            get { return mvarObjectName; }
            set { mvarObjectName = value; }
        }
        private string mvarMethodName = String.Empty;
        public string MethodName
        {
            get { return mvarMethodName; }
            set { mvarMethodName = value; }
        }
        private CodeVariableElement.CodeVariableElementCollection mvarVariables = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection Variables
        {
            get { return mvarVariables; }
        }
    }
}
