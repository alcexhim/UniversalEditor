//
//  JSONField.cs - the abstract base class from which all fields in a JSONObjectModel derive
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

using UniversalEditor.ObjectModels.JSON.Fields;

namespace UniversalEditor.ObjectModels.JSON
{
	/// <summary>
	/// The abstract base class from which all fields in a <see cref="JSONObjectModel" /> derive.
	/// </summary>
	public abstract class JSONField : ICloneable
	{
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		public class JSONFieldCollection
			: System.Collections.ObjectModel.Collection<JSONField>
		{
			public JSONNumberField Add(string Name, int Value)
			{
				JSONNumberField f = new JSONNumberField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONStringField Add(string Name, string Value)
			{
				JSONStringField f = new JSONStringField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONBooleanField Add(string Name, bool Value)
			{
				JSONBooleanField f = new JSONBooleanField();
				f.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
			public JSONArrayField Add(string Name, params string[] Values)
			{
				JSONArrayField f = new JSONArrayField();
				f.Name = Name;
				foreach(string s in Values)
				{
					f.Values.Add(s);
				}
				base.Add(f);
				return f;
			}
			public JSONObjectField Add(string Name, JSONObject Value)
			{
				JSONObjectField f = new JSONObjectField();
				f.Name = Name;
				Value.Name = Name;
				f.Value = Value;
				base.Add(f);
				return f;
			}
		}

		public abstract object Clone();
	}
}
