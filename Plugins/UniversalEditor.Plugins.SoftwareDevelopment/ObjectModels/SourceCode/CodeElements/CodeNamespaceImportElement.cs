//
//  CodeNamespaceImportElement.cs - represents a CodeElement that imports a namespace reference into an existing namespace
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
	/// Represents a <see cref="CodeElement" /> that imports a namespace reference into an existing namespace.
	/// </summary>
	public class CodeNamespaceImportElement : CodeElement
	{
		public CodeNamespaceImportElement()
		{
		}
		public CodeNamespaceImportElement(string[] namespaceName, string objectName = null)
		{
			NamespaceName = namespaceName;
			ObjectName = objectName;
		}

		/// <summary>
		/// Gets or sets the fully-qualified name of the namespace to import.
		/// </summary>
		/// <value>The fully-qualified name of the namespace to import.</value>
		public string[] NamespaceName { get; set; } = new string[0];
		/// <summary>
		/// Gets or sets the name of the object to import, or <see langword="null"/> if no object should be imported.
		/// </summary>
		/// <value>The name of the object to import, or <see langword="null"/> if no object should be imported.</value>
		public string ObjectName { get; set; } = null;
	}
}
