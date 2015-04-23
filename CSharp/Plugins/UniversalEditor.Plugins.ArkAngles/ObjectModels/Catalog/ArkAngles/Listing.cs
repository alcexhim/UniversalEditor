using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
	public class Listing : ICloneable
	{
		public class ListingCollection
			: System.Collections.ObjectModel.Collection<Listing>
		{
			public Listing Add(string title)
			{
				Listing item = new Listing();
				item.Title = title;
				Add(item);
				return item;
			}

			private Dictionary<string, Listing> itemsByName = new Dictionary<string, Listing>();

			protected override void ClearItems()
			{
				base.ClearItems();
				itemsByName.Clear();
			}
			protected override void InsertItem(int index, Listing item)
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
			protected override void SetItem(int index, Listing item)
			{
				if (index >= 0 && index < Count)
				{
					itemsByName.Remove(this[index].Title);
				}
				base.SetItem(index, item);
				itemsByName.Add(item.Title, item);
			}

			public Listing this[string title]
			{
				get
				{
					if (itemsByName.ContainsKey(title)) return itemsByName[title];
					return null;
				}
			}

			public bool Remove(string title)
			{
				Listing item = this[title];
				if (item == null) return false;
				Remove(item);
				return true;
			}
		}

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Listing" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public object Clone()
		{
			Listing clone = new Listing();
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
