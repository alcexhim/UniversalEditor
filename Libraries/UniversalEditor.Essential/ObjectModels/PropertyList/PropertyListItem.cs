//
//  PropertyListItem.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using System.Collections.Generic;
using System.Linq;

namespace UniversalEditor.ObjectModels.PropertyList
{
	public abstract class PropertyListItem : ICloneable
	{
		public class PropertyListItemCollection
			: System.Collections.ObjectModel.Collection<PropertyListItem>
		{
			private Group _parent = null;
			public PropertyListItemCollection(Group parent)
			{
				this._parent = parent;
			}

			public PropertyListItem this[string name]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].Name == name)
							return this[i];
					}
					return null;
				}
			}

			public T OfType<T>(string name) where T : PropertyListItem
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i].Name == name && this[i] is T)
						return (T)this[i];
				}
				return null;
			}

			public Property AddProperty(string name, object value = null)
			{
				Property property = new Property(name, value);
				Add(property);
				return property;
			}
			public Group AddGroup(string name, PropertyListItem[] items = null)
			{
				Group group = new Group(name, items);
				Add(group);
				return group;
			}

			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
					this[i].Parent = null;
				base.ClearItems();
			}
			protected override void InsertItem(int index, PropertyListItem item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, PropertyListItem item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = _parent;
			}

			public bool Contains<T>(string name) where T : PropertyListItem
			{
				return this.OfType<T>(name) != null;
			}
			public bool Contains(string name)
			{
				return this[name] != null;
			}

			public void AddRange(PropertyListItem[] items)
			{
				for (int i = 0; i < items.Length; i++)
					Add(items[i]);
			}
		}

		public abstract void Combine(PropertyListItem item);
		public abstract object Clone();

		/// <summary>
		/// The name of this <see cref="PropertyListItem"/>.
		/// </summary>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// The <see cref="Group" /> that contains this <see cref="PropertyListItem" /> as a child.
		/// </summary>
		public Group Parent { get; protected set; } = null;
	}
}
