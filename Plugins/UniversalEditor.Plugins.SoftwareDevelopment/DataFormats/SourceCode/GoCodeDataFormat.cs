//
//  GoCodeDataFormat.cs - provides a DataFormat for manipulating code files written in the Go programming language
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

using System.Text;

using UniversalEditor.ObjectModels.SourceCode;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.DataFormats.SourceCode
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating code files written in the Go programming language.
	/// </summary>
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
