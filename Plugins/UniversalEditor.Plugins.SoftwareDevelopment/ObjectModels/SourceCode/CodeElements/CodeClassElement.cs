//
//  CodeClassElement.cs - represents a CodeElement that defines a class
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
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	/// <summary>
	/// Represents a <see cref="CodeElement" /> that defines a class.
	/// </summary>
	public class CodeClassElement : CodeElementContainerElement, INamedCodeElement, IAccessModifiableCodeElement
	{
		public CodeClassElement()
		{
		}
		public CodeClassElement(string name)
		{
			mvarName = name;
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public string GetFullName(string separator = ".")
		{
			CodeElementContainerElement parent = base.Parent;
			StringBuilder sb = new StringBuilder();
			while (parent != null)
			{
				if (parent is INamedCodeElement)
				{
					sb.Append((parent as INamedCodeElement).Name);
					sb.Append(separator);
				}
				parent = parent.Parent;
			}
			sb.Append(mvarName);
			return sb.ToString();
		}

		private CodeAccessModifiers mvarAccessModifiers = CodeAccessModifiers.None;
		public CodeAccessModifiers AccessModifiers { get { return mvarAccessModifiers; } set { mvarAccessModifiers = value; } }

		private bool mvarIsStatic = false;
		public bool IsStatic { get { return mvarIsStatic; } set { mvarIsStatic = value; } }

		private bool mvarIsSealed = false;
		public bool IsSealed { get { return mvarIsSealed; } set { mvarIsSealed = value; } }

		private bool mvarIsAbstract = false;
		public bool IsAbstract { get { return mvarIsAbstract; } set { mvarIsAbstract = value; } }

		private bool mvarIsPartial = false;
		public bool IsPartial { get { return mvarIsPartial; } set { mvarIsPartial = value; } }

		private CodeVariableElement.CodeVariableElementCollection mvarGenericParameters = new CodeVariableElement.CodeVariableElementCollection();
		public CodeVariableElement.CodeVariableElementCollection GenericParameters
		{
			get { return mvarGenericParameters; }
		}

		public override string ToString()
		{
			return "Class: " + mvarName + " (" + mvarAccessModifiers.ToString() + (mvarIsStatic ? ", Static" : String.Empty) + ")";
		}

		private string[] mvarBaseClassName = new string[0];
		public string[] BaseClassName { get { return mvarBaseClassName; } set { mvarBaseClassName = value; } }
	}
}
