using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.ObjectModels.SourceCode
{
    public class CodeElementReference
    {
		public CodeElementReference()
		{
		}
		public CodeElementReference(CodeElement value)
		{
			mvarValue = value;
		}

        private CodeElement mvarValue = null;
        public CodeElement Value
        {
            get { return mvarValue; }
            set { mvarValue = value; }
        }

		public Type DataType
		{
			get
			{
				if (mvarValue is CodeLiteralElement)
				{
					CodeLiteralElement lit = (mvarValue as CodeLiteralElement);
					return lit.Value.GetType();
				}
				// otherwise is Boolean expression, maybe?
				return null;
			}
		}

		public object GetValue()
		{
			if (mvarValue is CodeLiteralElement)
			{
				CodeLiteralElement lit = (mvarValue as CodeLiteralElement);
				return lit.Value;
			}
			return null;
		}
	}
}
