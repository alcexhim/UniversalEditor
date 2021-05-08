//
//  CodePropertyElement.cs - represents a CodeElement that defines a property
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

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	/// <summary>
	/// Represents a <see cref="CodeElement" /> that defines a property.
	/// </summary>
	public class CodePropertyElement : CodeElement, INamedCodeElement, IAccessModifiableCodeElement
	{
		public string Name { get; set; } = String.Empty;

		public string GetFullName(string separator = ".")
		{
			return CodeElement.GetFullName(this, separator);
		}

		public CodeAccessModifiers AccessModifiers { get; set; } = CodeAccessModifiers.None;

		public CodeMethodElement GetMethod { get; set; } = null;
		public CodeMethodElement SetMethod { get; set; } = null;

		/// <summary>
		/// Gets or sets the data type of the property.
		/// </summary>
		/// <value>The data type of the property.</value>
		public string DataType { get; set; } = null;

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> represents an abstract property (one that must be
		/// overridden in a derived class).
		/// </summary>
		/// <value><c>true</c> if the property is abstract; otherwise, <c>false</c>.</value>
		public bool IsAbstract { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> represents a virtual property (one that may be overridden
		/// in a derived class).
		/// </summary>
		/// <value><c>true</c> if the property is virtual; otherwise, <c>false</c>.</value>
		public bool IsVirtual { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> represents a property that is overriding a property in a
		/// derived class.
		/// </summary>
		/// <value><c>true</c> if the property is overriding a property in a derived class; otherwise, <c>false</c>.</value>
		public bool IsOverriding { get; set; } = false;

		/// <summary>
		/// Gets a collection of <see cref="CodeVariableElement" /> instances representing the parameters accepted by the property indexer.
		/// </summary>
		/// <value>The parameters accepted by the property indexer.</value>
		public CodeVariableElement.CodeVariableElementCollection Parameters { get; } = new CodeVariableElement.CodeVariableElementCollection();

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> contains an auto-generated get method.
		/// </summary>
		/// <value><c>true</c> if the property contains an auto-generated get method; otherwise, <c>false</c>.</value>
		public bool AutoGenerateGetMethod { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> contains an auto-generated set method.
		/// </summary>
		/// <value><c>true</c> if the property contains an auto-generated set method; otherwise, <c>false</c>.</value>
		public bool AutoGenerateSetMethod { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodePropertyElement"/> represents a property that is static (l
		/// </summary>
		/// <value><c>true</c> if the property contains an auto-generated get method; otherwise, <c>false</c>.</value>
		public bool IsStatic { get; set; } = false;
	}
}
