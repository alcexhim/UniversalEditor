//
//  CodeVariableDeclarationElement.cs - represents a CodeElement that declares a variable
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
	/// Represents a <see cref="CodeElement" /> that declares a variable.
	/// </summary>
	public class CodeVariableDeclarationElement : CodeElement
	{
		public CodeVariableDeclarationElement()
		{
		}
		public CodeVariableDeclarationElement(CodeVariableElement variable)
		{
			Variable = variable;
		}

		/// <summary>
		/// Gets or sets the variable to declare.
		/// </summary>
		/// <value>The variable to declare.</value>
		public CodeVariableElement Variable { get; set; } = null;
	}
}
