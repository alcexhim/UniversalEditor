//
//  Platform.cs - represents an available platform for a software product in an Ark Angles software catalog
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
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
	/// <summary>
	/// Represents an available platform for a software product in an Ark Angles software catalog.
	/// </summary>
	public class Platform : ICloneable
	{
		/// <summary>
		/// Represents a collection of available <see cref="Platform" />s for a software product in an Ark Angles software catalog.
		/// </summary>
		public class PlatformCollection
			: System.Collections.ObjectModel.Collection<Platform>
		{
			public Platform Add(string title)
			{
				Platform item = new Platform();
				item.Title = title;
				Add(item);
				return item;
			}

			private Dictionary<string, Platform> itemsByName = new Dictionary<string, Platform>();

			protected override void ClearItems()
			{
				base.ClearItems();
				itemsByName.Clear();
			}
			protected override void InsertItem(int index, Platform item)
			{
				if (index >= 0 && index < Count)
				{
					itemsByName.Remove(this[index].Title);
				}
				base.InsertItem(index, item);
				itemsByName.Add(item.Title, item);
			}
			protected override void RemoveItem(int index)
			{
				itemsByName.Remove(this[index].Title);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, Platform item)
			{
				if (index >= 0 && index < Count)
				{
					itemsByName.Remove(this[index].Title);
				}
				base.SetItem(index, item);
				itemsByName.Add(item.Title, item);
			}

			public Platform this[string title]
			{
				get
				{
					if (itemsByName.ContainsKey(title)) return itemsByName[title];
					return null;
				}
			}

			public bool Remove(string title)
			{
				Platform item = this[title];
				if (item == null) return false;
				Remove(item);
				return true;
			}
		}

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Platform" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public object Clone()
		{
			Platform clone = new Platform();
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
