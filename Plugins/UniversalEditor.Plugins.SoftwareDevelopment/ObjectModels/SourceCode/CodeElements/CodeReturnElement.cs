using System;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	/// <summary>
	/// Returns the specified expression, e.g. "return _xx;" (C#) or "Return _xx" (VB).
	/// </summary>
	public class CodeReturnElement : CodeElement
	{
		private CodeElementReference mvarExpression = null;
		public CodeElementReference Expression { get { return mvarExpression; } set { mvarExpression = value; } }

		public CodeReturnElement(CodeElementReference expression) {
			mvarExpression = expression;
		}

		public override object Clone ()
		{
			CodeReturnElement clone = new CodeReturnElement (mvarExpression);
			return clone;
		}
	}
}

