using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements;
using UniversalEditor.ObjectModels.SourceCode.CodeElementReferences;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.SourceCode.Java
{
    public class JavaCodeDataFormat : CodeDataFormat
    {
        private char[] lineEndings = new char[] { '\r', '\n' };

        private string mvarNamespaceSeparator = ".";
        public string NamespaceSeparator { get { return mvarNamespaceSeparator; } set { mvarNamespaceSeparator = value; } }

        private string mvarArrayBeginSignal = "[";
        public string ArrayBeginSignal { get { return mvarArrayBeginSignal; } set { mvarArrayBeginSignal = value; } }

        private string mvarArrayEndSignal = "]";
        public string ArrayEndSignal { get { return mvarArrayEndSignal; } set { mvarArrayEndSignal = value; } }

        protected internal override string GenerateCode(object obj, int indentCount)
        {
            string indent = GetIndentString(indentCount);
            StringBuilder sb = new StringBuilder();
            if (obj is CodeCommentElement)
            {
                CodeCommentElement comment = (obj as CodeCommentElement);
                sb.Append(indent);
                if (comment.Multiline)
                {
                    if (comment.IsDocumentationComment)
                    {
                        sb.AppendLine("/**");
                    }
                    else
                    {
                        sb.AppendLine("/*");
                    }
                }

                string[] commentLines = comment.Content.Split(lineEndings);
                foreach (string commentLine in commentLines)
                {
                    if (comment.Multiline)
                    {
                        if (comment.IsDocumentationComment)
                        {
                            sb.Append("  *");
                        }
                        else
                        {
                            sb.Append(" *");
                        }
                    }
                    else
                    {
                        if (comment.IsDocumentationComment)
                        {
                            sb.Append("/// ");
                        }
                        else
                        {
                            sb.Append("// ");
                        }
                    }
                    sb.AppendLine(commentLine);

                    if (Array.IndexOf<string>(commentLines, commentLine) < commentLines.Length - 1)
                    {
                        sb.Append(indent);
                    }
                }
                if (comment.Multiline || comment.IsDocumentationComment)
                {
                    sb.Append("*/");
                }
            }
            else if (obj is CodeNamespaceElement)
            {
                CodeNamespaceElement ns = (obj as CodeNamespaceElement);
                string name = ns.GetFullName(mvarNamespaceSeparator);
                sb.Append("package ");
                sb.Append(name);
                sb.Append(";");
            }
            else if (obj is CodeNamespaceImportElement)
            {
                CodeNamespaceImportElement nsi = (obj as CodeNamespaceImportElement);
                string name = String.Join(mvarNamespaceSeparator, nsi.NamespaceName);
                if (nsi.ObjectName == null)
                {
                    name += mvarNamespaceSeparator + "*";
                }
                else
                {
                    if (nsi.ObjectName != nsi.NamespaceName[nsi.NamespaceName.Length - 1])
                    {
                        name += mvarNamespaceSeparator + nsi.ObjectName;
                    }
                }
                sb.Append("import ");
                sb.Append(name);
                sb.Append(";");
            }
            else if (obj is CodeClassElement)
            {
                CodeClassElement clss = (obj as CodeClassElement);
                sb.Append(indent);
                if (clss.AccessModifiers != ObjectModels.SourceCode.CodeAccessModifiers.None)
                {
                    sb.Append(GenerateCode(clss.AccessModifiers, indentCount));
                    sb.Append(" ");
                }
                sb.Append("class ");
                sb.Append(clss.Name);
                if (clss.BaseClassName.Length > 0)
                {
                    sb.Append(" extends ");
                    sb.Append(String.Join(mvarNamespaceSeparator, clss.BaseClassName));
                }
                sb.AppendLine();
                sb.Append(indent);
                sb.AppendLine("{");
                foreach (CodeElement ce in clss.Elements)
                {
                    string code = GenerateCode(ce, indentCount + 1);
                    sb.Append(code);
                }
                sb.AppendLine();
                sb.Append(indent);
                sb.Append("}");
            }
            else if (obj is CodeAccessModifiers)
            {
                switch ((CodeAccessModifiers)obj)
                {
                    case CodeAccessModifiers.Assembly:
                    {
                        sb.Append("internal");
                        break;
                    }
                    case CodeAccessModifiers.Family:
                    {
                        sb.Append("protected");
                        break;
                    }
                    case CodeAccessModifiers.FamilyANDAssembly:
                    case CodeAccessModifiers.FamilyORAssembly:
                    {
                        sb.Append("protected internal");
                        break;
                    }
                    case CodeAccessModifiers.Private:
                    {
                        sb.Append("private");
                        break;
                    }
                    case CodeAccessModifiers.Public:
                    {
                        sb.Append("public");
                        break;
                    }
                }
            }
            else if (obj is CodeMethodElement)
            {
                CodeMethodElement meth = (obj as CodeMethodElement);
                sb.Append(indent);
                if (meth.AccessModifiers != CodeAccessModifiers.None)
                {
                    sb.Append(GenerateCode(meth.AccessModifiers));
                    sb.Append(" ");
                }
                if (!String.IsNullOrEmpty(meth.DataType))
                {
                    sb.Append(MakeFriendlyDataType(meth.DataType));
                }
                else
                {
                    sb.Append("void");
                }
                sb.Append(" ");
                sb.Append(meth.Name);
                sb.Append("(");
                foreach (CodeVariableElement var in meth.Parameters)
                {
                    sb.Append(GenerateCode(var));
                    if (meth.Parameters.IndexOf(var) < meth.Parameters.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.AppendLine(")");

                sb.Append(indent);
                sb.AppendLine("{");

                foreach (CodeElement action in meth.Elements)
                {
                    sb.Append(GenerateCode(action, indentCount + 1));
                }
                sb.AppendLine();
                sb.Append(indent);
                sb.Append("}");
            }
            else if (obj is CodeDataType)
            {
                CodeDataType cdt = (CodeDataType)obj;
                string DataTypeName = cdt.GetFullName(mvarNamespaceSeparator);
                sb.Append(MakeFriendlyDataType(DataTypeName));
                if (cdt.IsArray)
                {
                    sb.Append(mvarArrayBeginSignal);
                    if (cdt.IsArrayLengthDefined)
                    {
                        sb.Append(cdt.ArrayLength);
                    }
                    sb.Append(mvarArrayEndSignal);
                }
            }
            else if (obj is CodeVariableElement)
            {
                CodeVariableElement var = (obj as CodeVariableElement);
                sb.Append(indent);
                if (var.AccessModifiers != CodeAccessModifiers.None)
                {
                    sb.Append(GenerateCode(var.AccessModifiers));
                    sb.Append(" ");
                }

                sb.Append(GenerateCode(var.DataType));
                sb.Append(" ");
                sb.Append(var.Name);

                if (var.Value != null)
                {
                    sb.Append(" = ");
                    sb.Append(GenerateCode(var.Value));
                }
            }
            else if (obj is CodeMethodCallElement)
            {
                CodeMethodCallElement mc = (obj as CodeMethodCallElement);
                sb.Append(indent);
                if (mc.ObjectName.Length > 0)
                {
                    sb.Append(String.Join(mvarNamespaceSeparator, mc.ObjectName));
                    sb.Append(mvarNamespaceSeparator);
                }
                sb.Append(mc.MethodName);
                sb.Append("(");
                foreach (CodeVariableElement var in mc.Parameters)
                {
                    sb.Append(GenerateCode(var.Value));
                    if (mc.Parameters.IndexOf(var) < mc.Parameters.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(");");
            }
            else if (obj is CodeVariableDeclarationElement)
            {
                CodeVariableDeclarationElement vardec = (obj as CodeVariableDeclarationElement);
                sb.Append(GenerateCode(vardec.Variable, indentCount));
                sb.AppendLine(";");
            }
            else if (obj is CodeConditionalStatementActionElement)
            {
                CodeConditionalStatementActionElement action = (obj as CodeConditionalStatementActionElement);
                sb.Append(indent);
                sb.Append("if (");
                sb.Append(GenerateCode(action.Expression));
                sb.AppendLine(")");

                sb.AppendLine("{");
                foreach (CodeElement action1 in action.Elements)
                {
                    sb.Append(GenerateCode(action1, indentCount + 1));
                }
                sb.AppendLine(indent);
                sb.Append("}");

            }
            else if (obj is CodeForLoopActionElement)
            {
                CodeForLoopActionElement forr = (obj as CodeForLoopActionElement);
                sb.Append("for (");
                sb.Append(GenerateCode(forr.Initialization));
                sb.Append("; ");
                sb.Append(GenerateCode(forr.Condition));
                sb.Append("; ");
                sb.Append(GenerateCode(forr.Increment));
                sb.Append(")");
            }
            else if (obj is CodeElementReference)
            {
                CodeElementReference cer = (obj as CodeElementReference);
                if (cer is CodeElementDynamicReference)
                {
                    CodeElementDynamicReference dynref = (cer as CodeElementDynamicReference);
                    sb.Append(dynref.Name);
                }
                else if (cer.Value is CodeLiteralElement)
                {
                    CodeLiteralElement lit = (cer.Value as CodeLiteralElement);
                    sb.Append(ExpressionToString(lit.Value));
                }
            }
            return sb.ToString();
        }

        protected override string ExpressionToString(object expr)
        {
            StringBuilder sb = new StringBuilder();
            if (expr is CodeElementReference)
            {
                CodeElementReference cer = (expr as CodeElementReference);
                if (cer.Value is CodeLiteralElement)
                {
                    CodeLiteralElement lit = (cer.Value as CodeLiteralElement);
                    object value = lit.Value;

                    if (value.GetType() == typeof(String))
                    {
                        sb.Append("\"");
                        sb.Append(value.ToString());
                        sb.Append("\"");
                    }
                    else if (value.GetType() == typeof(Char))
                    {
                        sb.Append("'");
                        sb.Append(value.ToString());
                        sb.Append("'");
                    }
                    else if (
                        value.GetType() == typeof(Int16) ||
                        value.GetType() == typeof(Int32) ||
                        value.GetType() == typeof(Int64) ||
                        value.GetType() == typeof(UInt16) ||
                        value.GetType() == typeof(UInt32) ||
                        value.GetType() == typeof(UInt64) ||
                        value.GetType() == typeof(Single) ||
                        value.GetType() == typeof(Double)
                        )
                    {
                        sb.Append(value.ToString());
                        if (value.GetType() == typeof(Single))
                        {
                            sb.Append("f");
                        }
                    }
                }
            }
            return sb.ToString();
        }
        protected override void ProcessToken(string token, Reader tr)
        {
            base.ProcessToken(token, tr);
        }
        protected override string MakeFriendlyDataTypeInternal(string DataType)
        {
            switch (DataType)
            {
                case "System.Boolean": return "boolean";
                case "System.Byte": return "byte";
                case "System.Char": return "char";
                case "System.Double": return "double";
                case "System.Int16": return "short";
                case "System.Int32": return "int";
                case "System.Int64": return "long";
                case "System.Single": return "float";
				case "System.String": return "string";
				case "System.Void": return "void";
            }
            return base.MakeFriendlyDataType(DataType);
        }
        protected override string MakeKnownDataType(string DataType)
        {
            switch (DataType)
            {
                case "boolean": return "System.Boolean";
                case "byte": return "System.Byte";
                case "char": return "System.Char";
                case "double": return "System.Double";
                case "short": return "System.Int16";
                case "int": return "System.Int32";
                case "long": return "System.Int64";
                case "float": return "System.Single";
                case "string": return "System.String";
            }
            return base.MakeKnownDataType(DataType);
        }
    }
}
