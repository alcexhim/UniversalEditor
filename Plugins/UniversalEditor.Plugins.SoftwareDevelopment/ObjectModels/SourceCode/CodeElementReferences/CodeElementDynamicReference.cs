//
//  CodeElementDynamicReference.cs - represents a dynamic reference to a code element whose target is calculated by the scripting engine at runtime
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

namespace UniversalEditor.ObjectModels.SourceCode.CodeElementReferences
{
	/// <summary>
	/// Represents a dynamic reference to an Object, Property, Method, Field, or similar element whose
	/// target is calculated by the scripting engine at runtime.
	/// </summary>
	public class CodeElementDynamicReference : CodeElementReference
	{
		public CodeElementDynamicReference(string name)
			: this(name, null)
		{

		}
		public CodeElementDynamicReference(string name, string[] objectName)
		{
			Name = name;
			ObjectName = objectName;
		}

		/// <summary>
		/// The name of the object, property, method, field, or similar element whose target should be
		/// dynamically located.
		/// </summary>
		public string Name { get; set; }

		public string[] ObjectName { get; set; } = null;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (ObjectName != null)
			{
				if (ObjectName.Length > 0)
				{
					sb.Append(String.Join(".", ObjectName));
					sb.Append('.');
				}
			}
			sb.Append(Name);
			return sb.ToString();
		}

	}
}
