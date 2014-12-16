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
    public class FSharpCodeDataFormat : CodeDataFormat
    {
        private string mvarNamespaceSeparator = ".";
        public string NamespaceSeparator { get { return mvarNamespaceSeparator; } set { mvarNamespaceSeparator = value; } }

		private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
            return _dfr;
        }

        protected override string MakeFriendlyDataType(string DataType)
        {
            switch (DataType)
            {
                case "System.Boolean":
                {
                    return "bool";
                }
                case "System.Byte":
                {
                    return "byte";
                }
                case "System.Char":
                {
                    return "char";
                }
                case "System.Decimal":
                {
                    return "decimal";
                }
                case "System.Double":
                {
                    return "double";
                }
                case "System.Int16":
                {
                    return "short";
                }
                case "System.Int32":
                {
                    return "int";
                }
                case "System.Int64":
                {
                    return "long";
                }
                case "System.SByte":
                {
                    return "sbyte";
                }
                case "System.Single":
                {
                    return "float";
                }
                case "System.String":
                {
                    return "string";
                }
                case "System.UInt16":
                {
                    return "ushort";
                }
                case "System.UInt32":
                {
                    return "uint";
                }
                case "System.UInt64":
                {
                    return "ulong";
                }
            }
            return base.MakeFriendlyDataType(DataType);
        }

        protected internal override string GenerateCode(object obj, int indentCount)
        {
            StringBuilder sb = new StringBuilder();
            string indent = GetIndentString(indentCount);
            if (obj is CodeAccessModifiers)
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
            else if (obj is CodeNamespaceElement)
            {
                CodeNamespaceElement namespaceEl = (obj as CodeNamespaceElement);
                sb.Append(indent);
                sb.Append("namespace ");

                string namespaceName = namespaceEl.GetFullName(mvarNamespaceSeparator);
                sb.AppendLine(namespaceName);
                sb.AppendLine(indent + "{");
                foreach (CodeElement el in namespaceEl.Elements)
                {
                    sb.Append(GenerateCode(el, indentCount + 1));
                    sb.AppendLine();
                }
                sb.Append(indent + "}");
            }
            else if (obj is CodeClassElement)
            {
                CodeClassElement classEl = (obj as CodeClassElement);
                sb.Append(indent);
                sb.Append("type ");
                sb.Append(GenerateCode(classEl.AccessModifiers));
                if (classEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
                
                sb.Append(classEl.Name);
                /*
                if (classEl.GenericParameters.Count > 0)
                {
                    sb.Append("<");
                    foreach (CodeVariableElement param in classEl.GenericParameters)
                    {
                        sb.Append(param.Name);
                        if (classEl.GenericParameters.IndexOf(param) < classEl.GenericParameters.Count - 1)
                        {
                            sb.Append(", ");
                        }
                    }
                    sb.Append(">");
                }
                 */
                sb.AppendLine();
                foreach (CodeElement el in classEl.Elements)
                {
                    sb.Append(GenerateCode(el, indentCount + 1));
                    sb.AppendLine();
                }
            }
            else if (obj is CodeMethodElement)
            {
                CodeMethodElement methodEl = (obj as CodeMethodElement);
                sb.Append(indent + "let ");
                sb.Append(GenerateCode(methodEl.AccessModifiers));
                if (methodEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
                sb.Append(methodEl.Name);
                if (methodEl.GenericParameters.Count > 0)
                {
                    sb.Append("<");
                    foreach (CodeVariableElement param in methodEl.GenericParameters)
                    {
                        sb.Append("'" + param.Name);
                        if (methodEl.GenericParameters.IndexOf(param) < methodEl.GenericParameters.Count - 1)
                        {
                            sb.Append(", ");
                        }
                    }
                    sb.Append(">");
                }
                sb.Append(" ");
                foreach (CodeVariableElement param in methodEl.Parameters)
                {
                    sb.Append(param.Name);
                    if (!String.IsNullOrEmpty(param.DataType))
                    {
                        sb.AppendLine(" : ");
                        if (param.PassByReference)
                        {
                            sb.Append("byref<");
                        }
                        sb.Append(param.DataType);
                        if (param.PassByReference)
                        {
                            sb.Append(">");
                        }
                    }
                    if (methodEl.Parameters.IndexOf(param) < methodEl.Parameters.Count - 1)
                    {
                        sb.Append(" ");
                    }
                }
                sb.Append("= ");
                sb.AppendLine();
                foreach (CodeElement el in methodEl.Elements)
                {
                    sb.Append(GenerateCode(el, indentCount + 1));
                }
            }
            else if (obj is CodePropertyElement)
            {
                sb.Append(indent);
                CodePropertyElement propertyEl = (obj as CodePropertyElement);
                sb.Append(GenerateCode(propertyEl.AccessModifiers));
                if (propertyEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
                if (propertyEl.IsAbstract)
                {
                    sb.Append("abstract ");
                }
                else if (propertyEl.IsVirtual)
                {
                    sb.Append("virtual ");
                }
                else if (propertyEl.IsOverriding)
                {
                    sb.Append("override ");
                }
                sb.Append(MakeFriendlyDataType(propertyEl.DataType) + " " + propertyEl.Name);
                if (propertyEl.Parameters.Count > 0)
                {
                    sb.Append("[");
                    foreach (CodeVariableElement varEl in propertyEl.Parameters)
                    {
                        sb.Append(MakeFriendlyDataType(varEl.DataType) + " " + varEl.Name);
                        if (propertyEl.Parameters.IndexOf(varEl) < propertyEl.Parameters.Count - 1)
                        {
                            sb.Append(", ");
                        }
                    }
                    sb.Append("]");
                }
            }
            else if (obj is CodeVariableElement)
            {
                sb.Append(indent + "let ");
                CodeVariableElement varEl = (obj as CodeVariableElement);
                sb.Append(GenerateCode(varEl.AccessModifiers));
                if (varEl.AccessModifiers != CodeAccessModifiers.None) sb.Append(" ");
                sb.Append(varEl.Name);
                if (varEl.Value != null)
                {
                    sb.Append(" = ");
                    sb.Append(GenerateCode(varEl.Value.Value, 0));
                }
                sb.Append(";");
            }
            return sb.ToString();
        }
    }
}
