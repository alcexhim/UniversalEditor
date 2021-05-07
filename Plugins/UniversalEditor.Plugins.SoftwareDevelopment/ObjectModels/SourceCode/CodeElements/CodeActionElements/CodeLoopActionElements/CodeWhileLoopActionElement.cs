using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeActionElements.CodeLoopActionElements
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
