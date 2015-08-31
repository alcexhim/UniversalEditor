using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	public class CodeVariableDeclarationElement : CodeElement
	{
		public CodeVariableDeclarationElement()
		{
		}
		public CodeVariableDeclarationElement(CodeVariableElement variable)
		{
			mvarVariable = variable;
		}

		private CodeVariableElement mvarVariable = null;
		public CodeVariableElement Variable { get { return mvarVariable; } set { mvarVariable = value; } }
	}
}
