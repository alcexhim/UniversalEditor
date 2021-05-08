//
//  CodeEnumerationElement.cs - represents a CodeElement that is a list of named values
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
	/// Represents a  <see cref="CodeElement" /> that is a list of named values.
	/// </summary>
	public class CodeEnumerationValue
	{
		public class CodeEnumerationValueCollection
			: System.Collections.ObjectModel.Collection<CodeEnumerationValue>
		{
			public CodeEnumerationValue Add(string name)
			{
				return Add(name, null);
			}
			public CodeEnumerationValue Add(string name, int? value)
			{
				CodeEnumerationValue cev = new CodeEnumerationValue();
				cev.parent = this;
				cev.Name = name;
				if (value != null)
				{
					cev.Value = value.Value;
					cev.IsValueDefined = true;
				}
				else
				{
					cev.IsValueDefined = false;
				}
				Add(cev);
				return cev;
			}
		}

		private CodeEnumerationValueCollection parent = null;

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private bool mvarIsValueDefined = false;
		public bool IsValueDefined { get { return mvarIsValueDefined; } set { mvarIsValueDefined = value; } }

		private int mvarValue = 0;
		public int Value
		{
			get
			{
				if (mvarIsValueDefined) return mvarValue;
				if (parent != null)
				{
					if (!parent.Contains(this)) throw new InvalidOperationException("not contained in a collection of enums and doesn't have a value set");

					int index = parent.IndexOf(this) - 1;
					if (index >= 0) return (parent[index].Value + 1);
				}
				return 0;
			}
			set { mvarValue = value; }
		}
	}
	/// <summary>
	/// Represents a <see cref="CodeElement" /> that is a list of named values.
	/// </summary>
	public class CodeEnumerationElement : CodeElement, INamedCodeElement, IAccessModifiableCodeElement
	{
		public CodeEnumerationElement(string name = null)
		{
			if (name == null)
				name = String.Empty;
			mvarName = name;
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public string GetFullName(string separator = ".")
		{
			return CodeElement.GetFullName(this, separator);
		}

		private CodeEnumerationValue.CodeEnumerationValueCollection mvarValues = new CodeEnumerationValue.CodeEnumerationValueCollection();
		public CodeEnumerationValue.CodeEnumerationValueCollection Values
		{
			get { return mvarValues; }
		}

		private CodeAccessModifiers mvarAccessModifiers = CodeAccessModifiers.None;
		public CodeAccessModifiers AccessModifiers { get { return mvarAccessModifiers; } set { mvarAccessModifiers = value; } }
	}
}
