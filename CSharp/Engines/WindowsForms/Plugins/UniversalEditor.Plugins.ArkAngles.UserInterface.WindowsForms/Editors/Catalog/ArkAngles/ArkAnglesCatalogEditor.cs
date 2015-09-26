using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.Dialogs.Catalog.ArkAngles;
using UniversalEditor.ObjectModels.Catalog.ArkAngles;

namespace UniversalEditor.Editors.Catalog.ArkAngles
{
	public partial class ArkAnglesCatalogEditor : Editor
	{
		public ArkAnglesCatalogEditor()
		{
			InitializeComponent();
			RefreshButtons();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Catalog Editor";
				_er.SupportedObjectModels.Add(typeof(CatalogObjectModel));
			}
			return _er;
		}

		private void SwitchTo(string name)
		{
			foreach (Control ctl in scMain.Panel2.Controls)
			{
				if (name != null && (ctl.Name.Substring(3) == name))
				{
					ctl.Enabled = true;
					ctl.Visible = true;
					continue;
				}

				ctl.Visible = false;
				ctl.Enabled = false;
			}
			RefreshButtons();
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				SwitchTo(e.Node.Name.Substring(4));
			}
			else
			{
				SwitchTo(null);
			}
		}

		private void cmdProductAdd_Click(object sender, EventArgs e)
		{
			ProductPropertiesDialog dlg = new ProductPropertiesDialog();
			dlg.Catalog = (ObjectModel as CatalogObjectModel);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				(ObjectModel as CatalogObjectModel).Products.Add(dlg.Item);

				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, dlg.Item);
				lvProducts.Items.Add(lvi);
			}
		}

		private void UpdateListViewItem(ref ListViewItem lvi, Product product)
		{
			lvi.Tag = product;
			lvi.SubItems.Clear();

			lvi.Text = product.Title;
			lvi.SubItems.Add(product.Category == null ? String.Empty : product.Category.Title);
			lvi.SubItems.Add(product.Platform == null ? String.Empty : product.Platform.Title);
			lvi.SubItems.Add(product.Listing == null ? String.Empty : product.Listing.Title);
		}
		private void UpdateListViewItem(ref ListViewItem lvi, Listing listing)
		{
			lvi.Tag = listing;
			lvi.Text = listing.Title;
		}
		private void UpdateListViewItem(ref ListViewItem lvi, Platform platform)
		{
			lvi.Tag = platform;
			lvi.Text = platform.Title;
		}
		private void UpdateListViewItem(ref ListViewItem lvi, Category category)
		{
			lvi.Tag = category;
			lvi.Text = category.Title;
		}

		private void cmdProductModify_Click(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProducts.SelectedItems[0];
			Product product = (lvi.Tag as Product);
			if (product == null) return;

			ProductPropertiesDialog dlg = new ProductPropertiesDialog();
			dlg.Catalog = (ObjectModel as CatalogObjectModel);
			dlg.Item = product;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UpdateListViewItem(ref lvi, dlg.Item);
			}
		}

		private void cmdProductRemove_Click(object sender, EventArgs e)
		{
			if (lvProducts.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove the selected product(s) from the list?", "Remove Products", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				while (lvProducts.SelectedItems.Count > 0)
				{
					(ObjectModel as CatalogObjectModel).Products.Remove(lvProducts.SelectedItems[0].Tag as Product);
					lvProducts.SelectedItems[0].Remove();
				}
				RefreshButtons();
			}
		}

		private void cmdProductClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all product(s) from the list?", "Remove Products", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			(ObjectModel as CatalogObjectModel).Products.Clear();
			lvProducts.Items.Clear();
			RefreshButtons();
		}

		private void RefreshButtons()
		{
			cmdListingModify.Enabled = (lvListings.SelectedItems.Count == 1);
			cmdListingRemove.Enabled = (lvListings.SelectedItems.Count > 0);
			cmdListingClear.Enabled = (lvListings.Items.Count > 0);

			cmdCategoryModify.Enabled = (lvCategories.SelectedItems.Count == 1);
			cmdCategoryRemove.Enabled = (lvCategories.SelectedItems.Count > 0);
			cmdCategoryClear.Enabled = (lvCategories.Items.Count > 0);

			cmdPlatformModify.Enabled = (lvPlatforms.SelectedItems.Count == 1);
			cmdPlatformRemove.Enabled = (lvPlatforms.SelectedItems.Count > 0);
			cmdPlatformClear.Enabled = (lvPlatforms.Items.Count > 0);

			cmdProductModify.Enabled = (lvProducts.SelectedItems.Count == 1);
			cmdProductRemove.Enabled = (lvProducts.SelectedItems.Count > 0);
			cmdProductClear.Enabled = (lvProducts.Items.Count > 0);
		}

		private void lvProducts_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void lvProducts_ItemActivate(object sender, EventArgs e)
		{
			cmdProductModify_Click(sender, e);
		}

		private void cmdListingAdd_Click(object sender, EventArgs e)
		{
			ListingPropertiesDialog dlg = new ListingPropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				(ObjectModel as CatalogObjectModel).Listings.Add(dlg.Item);

				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, dlg.Item);
				lvListings.Items.Add(lvi);
			}
		}


		private void cmdListingModify_Click(object sender, EventArgs e)
		{
			ListViewItem lvi = lvProducts.SelectedItems[0];
			Listing item = (lvi.Tag as Listing);
			if (item == null) return;

			ListingPropertiesDialog dlg = new ListingPropertiesDialog();
			dlg.Item = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UpdateListViewItem(ref lvi, dlg.Item);
			}
		}

		private void cmdListingRemove_Click(object sender, EventArgs e)
		{
			if (lvListings.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove the selected listing(s) from the list?", "Remove Listings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				while (lvListings.SelectedItems.Count > 0)
				{
					(ObjectModel as CatalogObjectModel).Listings.Remove(lvListings.SelectedItems[0].Tag as Listing);
					lvListings.SelectedItems[0].Remove();
				}
				RefreshButtons();
			}
		}

		private void cmdListingClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all listing(s) from the list?", "Remove Listings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			(ObjectModel as CatalogObjectModel).Listings.Clear();
			lvListings.Items.Clear();
			RefreshButtons();
		}

		#region Platform Management
		private void cmdPlatformAdd_Click(object sender, EventArgs e)
		{
			PlatformPropertiesDialog dlg = new PlatformPropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				(ObjectModel as CatalogObjectModel).Platforms.Add(dlg.Item);

				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, dlg.Item);
				lvPlatforms.Items.Add(lvi);
			}
		}


		private void cmdPlatformModify_Click(object sender, EventArgs e)
		{
			if (lvProducts.SelectedItems.Count <= 0) return;

			ListViewItem lvi = lvProducts.SelectedItems[0];
			Platform item = (lvi.Tag as Platform);
			if (item == null) return;

			PlatformPropertiesDialog dlg = new PlatformPropertiesDialog();
			dlg.Item = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UpdateListViewItem(ref lvi, dlg.Item);
			}
		}

		private void cmdPlatformRemove_Click(object sender, EventArgs e)
		{
			if (lvPlatforms.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove the selected platform(s) from the list?", "Remove Platforms", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				while (lvPlatforms.SelectedItems.Count > 0)
				{
					(ObjectModel as CatalogObjectModel).Platforms.Remove(lvPlatforms.SelectedItems[0].Tag as Platform);
					lvPlatforms.SelectedItems[0].Remove();
				}
				RefreshButtons();
			}
		}

		private void cmdPlatformClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all platform(s) from the list?", "Remove Platforms", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			(ObjectModel as CatalogObjectModel).Platforms.Clear();
			lvPlatforms.Items.Clear();
			RefreshButtons();
		}

		private void lvPlatforms_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void lvPlatforms_ItemActivate(object sender, EventArgs e)
		{
			cmdPlatformModify_Click(sender, e);
		}
		#endregion

		#region Category Management

		private void lvCategories_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void cmdCategoryAdd_Click(object sender, EventArgs e)
		{
			CategoryPropertiesDialog dlg = new CategoryPropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				(ObjectModel as CatalogObjectModel).Categories.Add(dlg.Item);

				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, dlg.Item);
				lvCategories.Items.Add(lvi);
			}
		}

		private void cmdCategoryModify_Click(object sender, EventArgs e)
		{
			if (lvProducts.SelectedItems.Count <= 0) return;

			ListViewItem lvi = lvProducts.SelectedItems[0];
			Category item = (lvi.Tag as Category);
			if (item == null) return;

			CategoryPropertiesDialog dlg = new CategoryPropertiesDialog();
			dlg.Item = item;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				UpdateListViewItem(ref lvi, dlg.Item);
			}
		}

		private void cmdCategoryRemove_Click(object sender, EventArgs e)
		{
			if (lvCategories.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove the selected categories from the list?", "Remove Categories", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
				while (lvPlatforms.SelectedItems.Count > 0)
				{
					(ObjectModel as CatalogObjectModel).Categories.Remove(lvCategories.SelectedItems[0].Tag as Category);
					lvCategories.SelectedItems[0].Remove();
				}
				RefreshButtons();
			}
		}

		private void cmdCategoryClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all categories from the list?", "Remove Categories", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
			(ObjectModel as CatalogObjectModel).Categories.Clear();
			lvCategories.Items.Clear();
			RefreshButtons();
		}

		#endregion

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			lvCategories.Items.Clear();
			lvListings.Items.Clear();
			lvPlatforms.Items.Clear();
			lvProducts.Items.Clear();

			CatalogObjectModel catalog = (ObjectModel as CatalogObjectModel);
			if (catalog == null) return;

			foreach (Category item in catalog.Categories)
			{
				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, item);
				lvCategories.Items.Add(lvi);
			}
			foreach (Listing item in catalog.Listings)
			{
				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, item);
				lvListings.Items.Add(lvi);
			}
			foreach (Platform item in catalog.Platforms)
			{
				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, item);
				lvPlatforms.Items.Add(lvi);
			}
			foreach (Product item in catalog.Products)
			{
				ListViewItem lvi = new ListViewItem();
				UpdateListViewItem(ref lvi, item);
				lvProducts.Items.Add(lvi);
			}

		}
	}
}
