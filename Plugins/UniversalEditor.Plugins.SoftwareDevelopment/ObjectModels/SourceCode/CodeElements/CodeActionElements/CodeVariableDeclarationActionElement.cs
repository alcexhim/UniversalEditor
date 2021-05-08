using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeActionElements
{
	public class CodeVariableDeclarationActionElement : CodeActionElement
	{
		public CodeVariableDeclarationActionElement()
		{
		}
		public CodeVariableDeclarationActionElement(CodeVariableElement variable)
		{
			mvarVariable = variable;
		}

		private CodeVariableElement mvarVariable = null;
		public CodeVariableElement Variable { get { return mvarVariable; } set { mvarVariable = value; } }
	}
}
