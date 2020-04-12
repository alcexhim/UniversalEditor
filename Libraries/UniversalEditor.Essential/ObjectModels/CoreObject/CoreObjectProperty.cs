//
//  CoreObjectProperty.cs - represents a property in a CoreObjectObjectModel
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

namespace UniversalEditor.ObjectModels.CoreObject
{
	/// <summary>
	/// Represents a property in a <see cref="CoreObjectObjectModel" />.
	/// </summary>
	public class CoreObjectProperty : ICloneable
	{
		public class CoreObjectPropertyCollection
			: System.Collections.ObjectModel.Collection<CoreObjectProperty>
		{
			private CoreObjectGroup _parent = null;
			public CoreObjectPropertyCollection(CoreObjectGroup parent = null)
			{
				_parent = parent;
			}

			public CoreObjectProperty Add(string name, string[] values = null, CoreObjectAttribute[] attributes = null)
			{
				CoreObjectProperty item = new CoreObjectProperty();
				item.Name = name;
				if (values != null)
				{
					foreach (string value in values)
					{
						item.Values.Add(value);
					}
				}
				if (attributes != null)
				{
					foreach (CoreObjectAttribute att in attributes)
					{
						item.Attributes.Add(att);
					}
				}
				Add(item);
				return item;
			}

			public CoreObjectProperty this[string name]
			{
				get
				{
					foreach (CoreObjectProperty item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}

			protected override void InsertItem(int index, CoreObjectProperty item)
			{
				base.InsertItem(index, item);
				item.ParentGroup = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].ParentGroup = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, CoreObjectProperty item)
			{
				if (index > -1 && index < this.Count - 1)
				{
					this[index].ParentGroup = null;
				}
				base.SetItem(index, item);
				item.ParentGroup = _parent;
			}
			protected override void ClearItems()
			{
				foreach (CoreObjectProperty item in this)
				{
					item.ParentGroup = null;
				}
				base.ClearItems();
			}

		}

		public string Name { get; set; } = String.Empty;
		public CoreObjectGroup ParentGroup { get; internal set; } = null;

		public CoreObjectAttribute.CoreObjectAttributeCollection Attributes { get; } = new CoreObjectAttribute.CoreObjectAttributeCollection();
		public System.Collections.Specialized.StringCollection Values { get; } = new System.Collections.Specialized.StringCollection();

		public object Clone()
		{
			CoreObjectProperty clone = new CoreObjectProperty();
			clone.Name = (Name.Clone() as string);
			foreach (CoreObjectAttribute item in Attributes)
			{
				clone.Attributes.Add(item.Clone() as CoreObjectAttribute);
			}
			foreach (string item in Values)
			{
				clone.Values.Add(item);
			}
			return clone;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			if (Attributes.Count > 0)
			{
				sb.Append(";");
				for (int i = 0; i < Attributes.Count; i++)
				{
					CoreObjectAttribute att = Attributes[i];
					sb.Append(att.Name);
					if (att.Values.Count > 0)
					{
						sb.Append("=");
						for (int j = 0; j < att.Values.Count; j++)
						{
							sb.Append(att.Values[j]);
							if (j < att.Values.Count - 1) sb.Append(',');
						}
					}
					if (i < Attributes.Count - 1) sb.Append(";");
				}
				if (Values.Count > 0)
				{
					sb.Append(":");
					for (int i = 0; i < Values.Count; i++)
					{
						sb.Append(Values[i]);
						if (i < Values.Count - 1) sb.Append(";");
					}
				}
			}
			return sb.ToString();
		}
	}
}
