//
//  CodeVariableElement.cs - represents a CodeElement that declares a variable
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
	/// Represents a <see cref="CodeElement" /> that declares a variable.
	/// </summary>
	public class CodeVariableElement : CodeElement, INamedCodeElement, IAccessModifiableCodeElement
	{
		public class CodeVariableElementCollection
			: System.Collections.ObjectModel.Collection<CodeVariableElement>
		{
			public CodeVariableElement Add(string VariableName, CodeDataType VariableDataType)
			{
				return Add(VariableName, VariableDataType, null);
			}
			public CodeVariableElement Add(string VariableName, CodeDataType VariableDataType, CodeElementReference VariableValue)
			{
				CodeVariableElement cve = new CodeVariableElement();
				cve.Name = VariableName;
				cve.DataType = VariableDataType;
				cve.Value = VariableValue;
				base.Add(cve);
				return cve;
			}
		}

		public CodeVariableElement()
		{
		}
		public CodeVariableElement(string name) : this(name, null)
		{
		}
		public CodeVariableElement(string name, string[] datatype)
		{
			Name = name;
			DataType = datatype;
			Value = null;
		}
		public CodeVariableElement(string name, string[] datatype, CodeElementReference value)
		{
			Name = name;
			DataType = datatype;
			Value = value;
		}
		public CodeVariableElement(string name, CodeDataType datatype, CodeElementReference value)
		{
			Name = name;
			DataType = datatype;
			Value = value;
		}

		/// <summary>
		/// Gets or sets the name of the variable to declare.
		/// </summary>
		/// <value>The name of the variable to declare.</value>
		public string Name { get; set; } = String.Empty;

		public string GetFullName(string separator = ".")
		{
			return CodeElement.GetFullName(this, separator);
		}

		/// <summary>
		/// Gets or sets the default value of the declared variable.
		/// </summary>
		/// <value>The default value of the declared variable.</value>
		public CodeElementReference Value { get; set; } = null;

		/// <summary>
		/// Gets or sets the data type of the variable.
		/// </summary>
		/// <value>The data type of the variable.</value>
		public CodeDataType DataType { get; set; } = CodeDataType.Empty;

		/// <summary>
		/// Gets or sets the access modifiers for the variable.
		/// </summary>
		/// <value>The access modifiers for the variable.</value>
		public CodeAccessModifiers AccessModifiers { get; set; } = CodeAccessModifiers.None;

		/// <summary>
		/// Gets or sets a value indicating whether the variable declared by this
		/// <see cref="T:UniversalEditor.ObjectModels.SourceCode.CodeElements.CodeVariableElement"/> should be passed by reference.
		/// </summary>
		/// <value><c>true</c> if this variable should be passed by reference; otherwise, <c>false</c>.</value>
		public bool PassByReference { get; set; } = false;
	}
}
