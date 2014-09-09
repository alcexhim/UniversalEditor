using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeNamespaceElement : CodeElementContainerElement, IMultipleNamedCodeElement
    {
		public CodeNamespaceElement()
		{
		}
		public CodeNamespaceElement(params string[] Name)
		{
            mvarName = Name;
		}

        private string[] mvarName = new string[0];
        public string[] Name { get { return mvarName; } set { mvarName = value; } }

        public string GetFullName(string separator = ".")
        {
			return CodeElement.GetFullName(this, separator);
        }

		public override string ToString()
		{
			return "Namespace: " + GetFullName();
		}
    }
}
