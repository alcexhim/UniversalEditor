using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.SourceCode
{
    public class GoCodeDataFormat : CodeDataFormat
    {
		private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
            return _dfr;
        }

        protected internal override string GenerateCode(object obj, int indentCount)
        {
            string indent = GetIndentString(indentCount);
            StringBuilder sb = new StringBuilder();
            if (obj is CodeMethodElement)
            {
                CodeMethodElement method = (obj as CodeMethodElement);
                sb.Append(indent + "func " + method.Name + "(");
                foreach (CodeVariableElement varEl in method.Parameters)
                {

                }
                sb.AppendLine(") {");
                foreach (CodeElement action in method.Elements)
                {
                    sb.AppendLine(GenerateCode(action, indentCount + 1));
                }
                sb.Append(indent + "}");
            }
            return sb.ToString();
        }
    }
}
