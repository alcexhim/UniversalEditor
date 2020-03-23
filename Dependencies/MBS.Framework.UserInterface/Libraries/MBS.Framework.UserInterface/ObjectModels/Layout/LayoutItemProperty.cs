//
//  LayoutItemProperty.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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

namespace MBS.Framework.UserInterface.ObjectModels.Layout
{
	public class LayoutItemProperty
	{
		public class LayoutItemPropertyCollection
			: System.Collections.ObjectModel.Collection<LayoutItemProperty>
		{
			private Dictionary<string, LayoutItemProperty> _itemsByName = new Dictionary<string, LayoutItemProperty>();
			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByName.Clear();
			}
			protected override void InsertItem(int index, LayoutItemProperty item)
			{
				base.InsertItem(index, item);
				if (!String.IsNullOrEmpty(item.Name)) _itemsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (!String.IsNullOrEmpty(this[index].Name))
				{
					if (_itemsByName.ContainsKey(this[index].Name))
					{
						_itemsByName.Remove(this[index].Name);
					}
				}
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, LayoutItemProperty item)
			{
				if (!String.IsNullOrEmpty(this[index].Name)) _itemsByName[this[index].Name] = null;
				base.SetItem(index, item);
				if (!String.IsNullOrEmpty(item.Name)) _itemsByName[item.Name] = item;
			}

			public LayoutItemProperty this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}

			public LayoutItemProperty Add(string name, string value = null)
			{
				LayoutItemProperty p = new LayoutItemProperty();
				p.Name = name;
				p.Value = value;
				Add(p);
				return p;
			}
		}

		public string Name { get; set; }
		public string Value { get; set; }

		public object Clone()
		{
			LayoutItemProperty clone = new LayoutItemProperty();
			clone.Name = (Name.Clone() as string);
			clone.Value = (Value.Clone() as string);
			return clone;
		}
	}
}
