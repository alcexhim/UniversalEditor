//
//  AdaCodeDataFormat.cs - provides a DataFormat for manipulating code files written in the Ada programming language
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
using MBS.Framework;
using MBS.Framework.Logic.Conditional;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;
using UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeLoopActionElements;

namespace UniversalEditor.DataFormats.SourceCode
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating code files written in the Ada programming language.
	/// </summary>
	public class AdaCodeDataFormat : CodeDataFormat
	{
		private static DataFormatReference _dfr;
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
					{
						sb.Append("limited");
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
				CodeNamespaceElement ns = (obj as CodeNamespaceElement);
				sb.Append(indent);
				sb.AppendLine("package " + ns.GetFullName() + " is");
				foreach (CodeElement el in ns.Elements)
				{
					sb.Append(GenerateCode(el, indentCount + 1));
				}
				sb.AppendLine("end " + ns.GetFullName() + ";");
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
					{
						sb.Append("=");
						break;
					}
					case ConditionComparison.GreaterThan:
					{
						sb.Append(">");
						break;
					}
					case ConditionComparison.LessThan:
					{
						sb.Append("<");
						break;
					}
				}
				sb.Append(" ");
				sb.Append(GenerateCode(cond.Value));
			}
			#endregion
			else if (obj is CodeCommentElement)
			{
				CodeCommentElement comment = (obj as CodeCommentElement);
				string[] lines = comment.Content.Split(new string[] { Environment.NewLine });
				for (int i = 0; i < lines.Length; i++)
				{
					sb.Append(indent);
					sb.Append("-- ");
					sb.Append(lines[i]);
					if (i < lines.Length - 1)
						sb.AppendLine();
				}
			}

			return sb.ToString();
		}
	}
}
