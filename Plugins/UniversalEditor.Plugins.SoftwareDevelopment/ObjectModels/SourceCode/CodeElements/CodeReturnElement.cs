//
//  CodeReturnElement.cs - represents a CodeElement that returns a value from a function
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

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	/// <summary>
	/// Represents a <see cref="CodeElement" /> that returns a value from a function. Returns the specified expression, e.g.
	/// "return _xx;" (C#) or "Return _xx" (VB).
	/// </summary>
	public class CodeReturnElement : CodeElement
	{
		/// <summary>
		/// Gets or sets the expression to return.
		/// </summary>
		/// <value>The expression to return.</value>
		public CodeElementReference Expression { get; set; } = null;

		public CodeReturnElement(CodeElementReference expression)
		{
			Expression = expression;
		}

		public override object Clone()
		{
			CodeReturnElement clone = new CodeReturnElement(Expression);
			return clone;
		}
	}
}
