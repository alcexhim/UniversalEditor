using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	public class CodeLiteralElement : CodeElement
	{
		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		public CodeLiteralElement()
		{
			mvarValue = null;
		}
		public CodeLiteralElement(object value)
		{
			mvarValue = value;
		}
	}
}
