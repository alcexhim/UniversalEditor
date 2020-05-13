//
//  Property.cs - represents a property in a PropertyListObjectModel which associates a name with a value
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.PropertyList
{
	/// <summary>
	/// Represents a property in a <see cref="PropertyListObjectModel" /> which associates a name with a value.
	/// </summary>
	public class Property : PropertyListItem, ICloneable
	{
		public class PropertyCollection : System.Collections.ObjectModel.Collection<Property>
		{
			private Group mvarParent = null;
			public Property this[string Name]
			{
				get
				{
					Property result;
					foreach (Property g in this)
					{
						if (g.Name == Name)
						{
							result = g;
							return result;
						}
					}
					result = null;
					return result;
				}
			}
			public PropertyCollection()
			{
				this.mvarParent = null;
			}
			public PropertyCollection(Group parent)
			{
				this.mvarParent = parent;
			}
			public Property Add(string name)
			{
				return this.Add(name, null);
			}
			public Property Add(string name, object value)
			{
				return Add(name, value, PropertyValueType.Unknown);
			}
			public Property Add(string name, object value, PropertyValueType type)
			{
				Property p = new Property();
				p.Name = name;
				p.Value = value;
				p.Parent = this.mvarParent;
				p.Type = type;
				base.Add(p);
				return p;
			}
			public bool Contains(string Name)
			{
				return this[Name] != null;
			}
			public bool Remove(string Name)
			{
				Property g = this[Name];
				bool result;
				if (g != null)
				{
					base.Remove(g);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}
			public void AddOrReplace(string Name, object Value)
			{
				if (this.Contains(Name))
				{
					this[Name].Value = Value;
				}
				else
				{
					this.Add(Name, Value);
				}
			}
		}

		public object Value { get; set; } = null;
		public override object Clone()
		{
			return new Property
			{
				Name = Name.Clone() as string,
				Value = (Value is ICloneable) ? (Value as ICloneable).Clone() : Value
			};
		}
		public override void Combine(PropertyListItem item)
		{
			if (!(item is Property))
			{
				Console.WriteLine("cannot combine a Property with a {0}", item?.GetType());
				return;
			}
			if (item.Name == Name) Value = (item as Property).Value;
		}
		public override string ToString()
		{
			string sValue = String.Empty;
			if (Value == null)
			{
				sValue = "null";
			}
			else if (Value is string)
			{
				sValue = "\"" + Value.ToString() + "\"";
			}
			else
			{
				sValue = Value.ToString();
			}
			return String.Format("{0} = {1}", Name, sValue);
		}

		public Property()
		{
		}
		public Property(string name, object value = null)
		{
			Name = name;
			Value = value;
		}

		public PropertyValueType Type { get; set; } = PropertyValueType.Unknown;
	}
}
