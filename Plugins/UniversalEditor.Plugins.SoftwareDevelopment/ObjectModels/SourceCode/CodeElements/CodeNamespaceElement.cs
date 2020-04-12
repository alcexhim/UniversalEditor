//
//  CodeNamespaceElement.cs - represents a CodeElement that defines a namespace
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
	/// Represents a <see cref="CodeElement" /> that defines a namespace.
	/// </summary>
	public class CodeNamespaceElement : CodeElementContainerElement, IMultipleNamedCodeElement
	{
		public CodeNamespaceElement()
		{
		}
		public CodeNamespaceElement(params string[] name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets or sets the fully-qualified name of this namespace.
		/// </summary>
		/// <value>The fully-qualified name of this namespace.</value>
		public string[] Name { get; set; } = new string[0];

		public string GetFullName(string separator = ".")
		{
			return CodeElement.GetFullName(this, separator);
		}

		public override string ToString()
		{
			return "Namespace: " + GetFullName();
		}
	}
}
