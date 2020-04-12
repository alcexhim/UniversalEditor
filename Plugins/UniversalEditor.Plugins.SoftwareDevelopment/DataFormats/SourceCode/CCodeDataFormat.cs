//
//  CCodeDataFormat.cs - provides a DataFormat for manipulating code files written in the C programming language
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.SourceCode
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating code files written in the C programming language.
	/// </summary>
	public class CCodeDataFormat : CodeDataFormat
	{
		private bool mvarDisplayVoidInEmptyParameterList = false;
		public bool DisplayVoidInEmptyParameterList { get { return mvarDisplayVoidInEmptyParameterList; } set { mvarDisplayVoidInEmptyParameterList = value; } }

		protected internal override string GenerateCode(object obj, int indentCount)
		{
			StringBuilder sb = new StringBuilder();
			string indent = new string(' ', 4 * indentCount);
			if (obj is CodeLiteralElement)
			{
				CodeLiteralElement lit = (obj as CodeLiteralElement);
				if (lit.Value == null)
				{
					sb.Append(indent);
					sb.Append("null");
				}
				else if (lit.Value is string)
				{
					sb.Append(indent);
					sb.Append("\"");
					sb.Append(lit.Value.ToString());
					sb.Append("\"");
				}
				else
				{
					sb.Append(indent);
					sb.Append(lit.Value.ToString());
				}
			}
			else if (obj is CodeIncludeFileElement)
			{
				CodeIncludeFileElement include = (obj as CodeIncludeFileElement);
				sb.Append(indent);
				sb.Append("#include ");
				if (include.IsRelativePath)
				{
					sb.Append("<");
				}
				else
				{
					sb.Append("\"");
				}
				sb.Append(include.FileName);
				if (include.IsRelativePath)
				{
					sb.Append(">");
				}
				else
				{
					sb.Append("\"");
				}
				sb.AppendLine();
			}
			else if (obj is CodeMethodElement)
			{
				CodeMethodElement method = (obj as CodeMethodElement);
				sb.Append(indent);
				if (method.DataType == null)
				{
					sb.Append("void");
				}
				else
				{
					sb.Append(method.DataType);
				}
				sb.Append(" ");
				sb.Append(method.Name);

				sb.Append("(");
				if (method.Parameters.Count == 0 && mvarDisplayVoidInEmptyParameterList)
				{
					sb.Append("void");
				}
				else
				{
					foreach (CodeVariableElement cve in method.Parameters)
					{
						sb.Append(cve.DataType);
						sb.Append(" ");
						sb.Append(cve.Name);
						if (method.Parameters.IndexOf(cve) < method.Parameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
				}
				sb.AppendLine(")");
				sb.Append(indent);
				sb.AppendLine("{");
				foreach (CodeElement action in method.Elements)
				{
					sb.AppendLine(GenerateCode(action, indentCount + 1));
				}
				sb.Append(indent);
				sb.Append("}");
			}
			#region Code Actions
			else if (obj is CodeMethodCallElement)
			{
				CodeMethodCallElement mc = (obj as CodeMethodCallElement);
				sb.Append(indent);
				if (mc.ObjectName != null && mc.ObjectName.Length > 0)
				{
					sb.Append(String.Join(".", mc.ObjectName));
					sb.Append(".");
				}
				sb.Append(mc.MethodName);
				sb.Append("(");
				if (mc.Parameters.Count == 0 && mvarDisplayVoidInEmptyParameterList)
				{
					sb.Append("void");
				}
				else
				{
					foreach (CodeVariableElement cve in mc.Parameters)
					{
						if (cve.PassByReference)
						{
							sb.Append("&");
						}
						sb.Append(GenerateCode(cve.Value));
						if (mc.Parameters.IndexOf(cve) < mc.Parameters.Count - 1)
						{
							sb.Append(", ");
						}
					}
				}
				sb.Append(");");
			}
			else if (obj is CodeVariableDeclarationElement)
			{
				CodeVariableDeclarationElement decl = (obj as CodeVariableDeclarationElement);
				if (decl.Variable == null) return String.Empty;

				sb.Append(indent);
				if (!String.IsNullOrEmpty(decl.Variable.DataType))
				{
					sb.Append(decl.Variable.DataType);
					sb.Append(" ");
				}
				else
				{
					sb.Append("void ");
				}

				sb.Append(decl.Variable.Name);
				if (decl.Variable.Value != null)
				{
					sb.Append(" = ");
					sb.Append(GenerateCode(decl.Variable.Value));
				}

				if (!sb.ToString().EndsWith(";"))
				{
					sb.Append(";");
				}
			}
			#endregion
			#region Code Element References
			else if (obj is CodeElementReference)
			{
				CodeElementReference cer = (obj as CodeElementReference);
				sb.Append(indent);
				sb.Append(GenerateCode(cer.Value));
			}
			else if (obj is CodeVariableElement)
			{
				CodeVariableElement varr = (obj as CodeVariableElement);
				sb.Append(varr.Name);
			}
			#endregion
			return sb.ToString();
		}
	}
}
