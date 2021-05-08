//
//  CodeMethodElement.cs - represents a CodeElement that defines a method
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
	/// Represents a <see cref="CodeElement" /> that defines a method.
	/// </summary>
	public class CodeMethodElement : CodeElementContainerElement, INamedCodeElement, IAccessModifiableCodeElement
	{
		/// <summary>
		/// Gets or sets the name of the method.
		/// </summary>
		/// <value>The name of the method.</value>
		public string Name { get; set; } = String.Empty;

		public string GetFullName(string separator = ".")
		{
			return CodeElement.GetFullName(this, separator);
		}

		public CodeAccessModifiers AccessModifiers { get; set; } = CodeAccessModifiers.None;

		/// <summary>
		/// Gets or sets the return type of the method, or NULL if the method does not return a value (e.g. Sub in Visual Basic or void in C#).
		/// </summary>
		/// <value>The return type of the method, or NULL if the method does not return a value.</value>
		public string DataType { get; set; } = null;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeMethodElement"/> represents an abstract (must be overridden) method.
		/// </summary>
		/// <value><c>true</c> if the method is abstract; otherwise, <c>false</c>.</value>
		public bool IsAbstract { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeMethodElement"/> represents a virtual (may be overridden) method.
		/// </summary>
		/// <value><c>true</c> if the method is virtual; otherwise, <c>false</c>.</value>
		public bool IsVirtual { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeMethodElement"/> represents a method that overrides a method in a base class.
		/// </summary>
		/// <value><c>true</c> if the method is overriding a method in a base class; otherwise, <c>false</c>.</value>
		public bool IsOverriding { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeMethodElement"/> represents a static (local to the type of the class itself,
		/// not an instance of the class) method.
		/// </summary>
		/// <value><c>true</c> if the method is static; otherwise, <c>false</c>.</value>
		public bool IsStatic { get; set; } = false;

		/// <summary>
		/// Gets a collection of <see cref="CodeVariableElement" /> instances representing the parameters accepted by the method.
		/// </summary>
		/// <value>The parameters accepted by the method..</value>
		public CodeVariableElement.CodeVariableElementCollection Parameters { get; } = new CodeVariableElement.CodeVariableElementCollection();

		/// <summary>
		/// Gets a collection of <see cref="CodeVariableElement" /> instances representing the generic type parameters accepted by the method.
		/// </summary>
		/// <value>The generic type parameters accepted by the method..</value>
		public CodeVariableElement.CodeVariableElementCollection GenericParameters { get; } = new CodeVariableElement.CodeVariableElementCollection();
	}
}
