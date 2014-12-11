using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.Programming
{
    public class GoCodeDataFormat : CodeDataFormat
    {
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("Go code file", new string[] { "*.go" });
            return dfr;
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
