using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements;

namespace UniversalEditor.DataFormats.SourceCode
{
    public class AdaCodeDataFormat : CodeDataFormat
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
            StringBuilder sb = new StringBuilder();
            string indent = GetIndentString(indentCount);
            string indent2 = GetIndentString(indentCount + 1);

            if (obj is CodeAccessModifiers)
            {
                switch ((CodeAccessModifiers)obj)
                {
                    case CodeAccessModifiers.Assembly:
                    case CodeAccessModifiers.Family:
                    case CodeAccessModifiers.FamilyANDAssembly:
                    case CodeAccessModifiers.FamilyORAssembly:
                        sb.Append("limited");
                        break;
                    case CodeAccessModifiers.Private:
                        sb.Append("private");
                        break;
                    case CodeAccessModifiers.Public:
                        sb.Append("public");
                        break;
                }
            }
			else if (obj is CodeNamespaceElement)
			{
				CodeNamespaceElement ns = (obj as CodeNamespaceElement);
				sb.Append (indent);
				sb.AppendLine ("package " + ns.GetFullName () + " is");
				foreach (CodeElement el in ns.Elements) {
					sb.Append (GenerateCode (el, indentCount + 1));
				}
				sb.AppendLine ("end " + ns.GetFullName () + ";");
			}
            else if (obj is CodeClassElement)
            {
                CodeClassElement classEl = (obj as CodeClassElement);
                sb.AppendLine(indent + "type " + classEl.Name + " is");
                sb.AppendLine(indent2 + "record");
                foreach (CodeElement el in classEl.Elements)
                {
                    sb.AppendLine(GenerateCode(el, indentCount + 2));
                    sb.AppendLine();
                }
                sb.AppendLine(indent2 + "end record;");
            }
            else if (obj is CodeEnumerationElement)
            {
                CodeEnumerationElement enumEl = (obj as CodeEnumerationElement);
                sb.Append(indent + "type " + enumEl.Name + " is (");
                int i = 0;
                foreach (CodeEnumerationValue value in enumEl.Values)
                {
                    sb.Append(value.Name);
                    if (i < enumEl.Values.Count - 1)
                    {
                        sb.Append(", ");
                    }
                    i++;
                }
                sb.AppendLine(");");
            }
            else if (obj is CodeVariableElement)
            {
                CodeVariableElement varEl = (obj as CodeVariableElement);
                sb.Append(indent + varEl.Name + " : " + varEl.DataType + ";");
            }
            #region Action Elements
            else if (obj is CodeWhileLoopActionElement)
            {
                CodeWhileLoopActionElement loop = (obj as CodeWhileLoopActionElement);
                sb.AppendLine("while " + GenerateCode(loop.Condition) + " loop");
            }
            #endregion
            #region Conditional Statements
            else if (obj is Condition)
            {
                Condition cond = (obj as Condition);
                sb.Append(cond.PropertyName);
                sb.Append(" ");
                switch (cond.Comparison)
                {
                    case ConditionComparison.Equal:
                        sb.Append("=");
                        break;
                    case ConditionComparison.GreaterThan:
                        sb.Append(">");
                        break;
                    case ConditionComparison.LessThan:
                        sb.Append("<");
                        break;
                }
                sb.Append(" ");
                sb.Append(GenerateCode(cond.Value));
            }
            #endregion
			else if (obj is CodeCommentElement)
			{
				CodeCommentElement comment = (obj as CodeCommentElement);
				string[] lines = comment.Content.Split (new string[] { Environment.NewLine });
				for (int i = 0; i < lines.Length; i++) {
					sb.Append (indent);
					sb.Append ("-- ");
					sb.Append (lines [i]);
					if (i < lines.Length - 1)
						sb.AppendLine ();
				}
			}

            return sb.ToString();
        }
    }
}
