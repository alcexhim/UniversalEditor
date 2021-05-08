//
//  CodePropertySetElement.cs - represents a CodeElement which assigns a value to a property
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
	/// Represents a <see cref="CodeElement" /> which assigns a value to a property.
	/// </summary>
	public class CodePropertySetElement : CodeElement
	{
		/// <summary>
		/// Gets or sets the name of the object containing the property to assign.
		/// </summary>
		/// <value>The name of the object containing the property to assign.</value>
		public string ObjectName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the name of the property to assign.
		/// </summary>
		/// <value>The name of the property to assign.</value>
		public string PropertyName { get; set; } = String.Empty;

		/// <summary>
		/// The value to which to set this property. Note that value can be a CodeElement such as a method call or another complex
		/// expression.
		/// </summary>
		public object Value { get; set; } = null;
	}
}
