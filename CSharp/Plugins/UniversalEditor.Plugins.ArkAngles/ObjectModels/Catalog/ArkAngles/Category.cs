using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
	public class Category : ICloneable
	{
		public class CategoryCollection
			: System.Collections.ObjectModel.Collection<Category>
		{
			public Category Add(string title)
			{
				Category item = new Category();
				item.Title = title;
				Add(item);
				return item;
			}

			private Dictionary<string, Category> itemsByName = new Dictionary<string, Category>();

			protected override void ClearItems()
			{
				base.ClearItems();
				itemsByName.Clear();
			}
			protected override void InsertItem(int index, Category item)
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
			protected override void SetItem(int index, Category item)
			{
				if (index >= 0 && index < Count)
				{
					itemsByName.Remove(this[index].Title);
				}
				base.SetItem(index, item);
				itemsByName.Add(item.Title, item);
			}

			public Category this[string title]
			{
				get
				{
					if (itemsByName.ContainsKey(title)) return itemsByName[title];
					return null;
				}
			}

			public bool Remove(string title)
			{
				Category item = this[title];
				if (item == null) return false;
				Remove(item);
				return true;
			}
		}

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Category" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public object Clone()
		{
			Category clone = new Category();
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}

		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
