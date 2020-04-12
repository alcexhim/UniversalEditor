//
//  CodeMethodCallElement.cs - represents a CodeElement specifying a call to an existing method
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
	/// Represents a <see cref="CodeElement" /> specifying a call to an existing method.
	/// </summary>
	public class CodeMethodCallElement : CodeElementContainerElement
    {
		public CodeMethodCallElement()
		{
		}
		public CodeMethodCallElement(string objectName, string methodName, params CodeVariableElement[] parameters) : this(objectName.Split(new char[] { '.' }), methodName, parameters) { }
		public CodeMethodCallElement(string methodName, params CodeVariableElement[] parameters) : this((string[])null, methodName, parameters) { }
		public CodeMethodCallElement(string[] objectName, string methodName, params CodeVariableElement[] parameters)
		{
			ObjectName = objectName;
			MethodName = methodName;
			foreach (CodeVariableElement cve in parameters)
			{
				Parameters.Add(cve);
			}
		}

		/// <summary>
		/// Gets or sets the name of the object containing the method to invoke, or <see langword="null"/> if the method is a simple function.
		/// </summary>
		/// <value>The name of the object containing the method to invoke, or <see langword="null"/> if the method is a simple function.</value>
		public string[] ObjectName { get; set; } = null;

		/// <summary>
		/// Gets or sets the name of the method to invoke.
		/// </summary>
		/// <value>The name of the method to invoke.</value>
		public string MethodName { get; set; } = String.Empty;

		/// <summary>
		/// Gets a collection of <see cref="CodeVariableElement" /> instances representing the parameters to pass into the method call.
		/// </summary>
		/// <value>The parameters to pass into the method call.</value>
		public CodeVariableElement.CodeVariableElementCollection Parameters { get; } = new CodeVariableElement.CodeVariableElementCollection();
    }
}
