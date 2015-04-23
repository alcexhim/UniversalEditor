using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
	public class Platform : ICloneable
	{
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
				if (!(index < Count && index >= 0)) return;

				itemsByName.Remove(this[index].Title);
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
				if (index > this.Count - 1 || index < 0) return;

				itemsByName.Remove(this[index].Title);
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
	}
}
