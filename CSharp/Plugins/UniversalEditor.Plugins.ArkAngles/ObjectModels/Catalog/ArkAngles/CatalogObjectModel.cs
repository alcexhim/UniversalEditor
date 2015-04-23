using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Catalog.ArkAngles
{
	/// <summary>
	/// Stores information about software products.
	/// </summary>
	public class CatalogObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Catalog";
				_omr.Path = new string[] { "Catalog", "Ark Angles" };
			}
			return _omr;
		}

		private Category.CategoryCollection mvarCategories = new Category.CategoryCollection();
		/// <summary>
		/// The categories contained in this catalog.
		/// </summary>
		public Category.CategoryCollection Categories { get { return mvarCategories; } }

		private string mvarFooterText = String.Empty;
		/// <summary>
		/// The text shown at the bottom of the catalog window.
		/// </summary>
		public string FooterText { get { return mvarFooterText; } set { mvarFooterText = value; } }

		private string mvarHeaderText = String.Empty;
		/// <summary>
		/// The text shown at the top of the catalog window.
		/// </summary>
		public string HeaderText { get { return mvarHeaderText; } set { mvarHeaderText = value; } }

		private string mvarHelpFileName = String.Empty;
		/// <summary>
		/// The file name of the help file to open when the Help button is clicked. If this value is empty, the Help button is not displayed.
		/// </summary>
		public string HelpFileName { get { return mvarHelpFileName; } set { mvarHelpFileName = value; } }

		private Platform.PlatformCollection mvarPlatforms = new Platform.PlatformCollection();
		/// <summary>
		/// The platforms contained in this catalog.
		/// </summary>
		public Platform.PlatformCollection Platforms { get { return mvarPlatforms; } }

		private Listing.ListingCollection mvarListings = new Listing.ListingCollection();
		/// <summary>
		/// The listings contained in this catalog.
		/// </summary>
		public Listing.ListingCollection Listings { get { return mvarListings; } }

		private Product.ProductCollection mvarProducts = new Product.ProductCollection();
		/// <summary>
		/// The products listed within this catalog.
		/// </summary>
		public Product.ProductCollection Products { get { return mvarProducts; } }

		public override void Clear()
		{
			mvarCategories.Clear();
			mvarFooterText = String.Empty;
			mvarHeaderText = String.Empty;
			mvarListings.Clear();
			mvarPlatforms.Clear();
			mvarProducts.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			CatalogObjectModel clone = (where as CatalogObjectModel);
			foreach (Category item in mvarCategories)
			{
				clone.Categories.Add(item.Clone() as Category);
			}
			clone.FooterText = (mvarFooterText.Clone() as string);
			clone.HeaderText = (mvarHeaderText.Clone() as string);
			foreach (Listing item in mvarListings)
			{
				clone.Listings.Add(item.Clone() as Listing);
			}
			foreach (Platform item in mvarPlatforms)
			{
				clone.Platforms.Add(item.Clone() as Platform);
			}
			foreach (Product item in mvarProducts)
			{
				clone.Products.Add(item.Clone() as Product);
			}
		}
	}
}
