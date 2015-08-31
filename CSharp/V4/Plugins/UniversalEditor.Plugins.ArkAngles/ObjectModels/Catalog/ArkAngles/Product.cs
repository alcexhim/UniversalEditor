using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
    public class Product : ICloneable
    {
		public class ProductCollection
			: System.Collections.ObjectModel.Collection<Product>
		{

			private Dictionary<string, Product> itemsByName = new Dictionary<string, Product>();

			protected override void ClearItems()
			{
				base.ClearItems();
				itemsByName.Clear();
			}
			protected override void InsertItem(int index, Product item)
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
			protected override void SetItem(int index, Product item)
			{
				if (index >= 0 && index < Count)
				{
					itemsByName.Remove(this[index].Title);
				}
				base.SetItem(index, item);
				itemsByName.Add(item.Title, item);
			}

			public Product this[string title]
			{
				get
				{
					if (itemsByName.ContainsKey(title)) return itemsByName[title];
					return null;
				}
			}

			public bool Remove(string title)
			{
				Product item = this[title];
				if (item == null) return false;
				Remove(item);
				return true;
			}
		}

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of this <see cref="Product" />.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Category mvarCategory = null;
		/// <summary>
		/// The <see cref="Category" /> associated with this <see cref="Product" />.
		/// </summary>
		public Category Category { get { return mvarCategory; } set { mvarCategory = value; } }

		private Platform mvarPlatform = null;
		/// <summary>
		/// The <see cref="Platform" /> this <see cref="Product" /> is designed to run on.
		/// </summary>
		public Platform Platform { get { return mvarPlatform; } set { mvarPlatform = value; } }

		private Listing mvarListing = null;
		/// <summary>
		/// The <see cref="Listing" /> associated with this <see cref="Product" />.
		/// </summary>
		public Listing Listing { get { return mvarListing; } set { mvarListing = value; } }

		private List<string> mvarKeywords = new List<string>();
		/// <summary>
		/// Key words associated with this <see cref="Product" />.
		/// </summary>
		public List<String> Keywords { get { return mvarKeywords; } }

		private List<string> mvarAssociatedFiles = new List<string>();
		/// <summary>
		/// File names of associated files to be displayed along with this product (screen shot BMPs, readme TXTs, etc.)
		/// </summary>
		public List<string> AssociatedFiles { get { return mvarAssociatedFiles; } }

		public object Clone()
		{
			Product clone = new Product();
			foreach (string filename in mvarAssociatedFiles)
			{
				clone.AssociatedFiles.Add(filename.Clone() as string);
			}
			clone.Category = mvarCategory;
			foreach (string keyword in mvarKeywords)
			{
				clone.Keywords.Add(keyword.Clone() as string);
			}
			clone.Listing = mvarListing;
			clone.Platform = mvarPlatform;
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
    }
}
