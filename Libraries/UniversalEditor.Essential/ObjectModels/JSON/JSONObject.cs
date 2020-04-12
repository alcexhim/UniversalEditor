//
//  JSONObject.cs - represents a JSON object which can contain other JSONObjects and JSONFields
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

namespace UniversalEditor.ObjectModels.JSON
{
	/// <summary>
	/// Represents a JSON object which can contain other <see cref="JSONObject" />s and <see cref="JSONField" />s.
	/// </summary>
	public class JSONObject : ICloneable
	{
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private JSONField.JSONFieldCollection mvarFields = new JSONField.JSONFieldCollection();
		public JSONField.JSONFieldCollection Fields { get { return mvarFields; } }
		
		public class JSONObjectCollection
			: System.Collections.ObjectModel.Collection<JSONObject>
		{
			public JSONObject Add()
			{
				return Add("");
			}
			public JSONObject Add(string Name)
			{
				JSONObject o = new JSONObject();
				o.Name = Name;
				base.Add(o);
				return o;
			}
			
			public JSONObject this[string Name]
			{
				get
				{
					foreach(JSONObject o in this)
					{
						if (o.Name == Name)
						{
							return o;
						}
					}
					return null;
				}
			}
			public bool Contains(string Name)
			{
				return (this[Name] != null);
			}
			public bool Remove(string Name)
			{
				JSONObject o = this[Name];
				if (o != null)
				{
					base.Remove(o);
					return true;
				}
				return false;
			}
		}

		public object Clone()
		{
			JSONObject clone = new JSONObject();
			foreach (JSONField field in Fields)
			{
				clone.Fields.Add(field.Clone() as JSONField);
			}
			clone.Name = (Name.Clone() as string);
			return clone;
		}
	}
}
