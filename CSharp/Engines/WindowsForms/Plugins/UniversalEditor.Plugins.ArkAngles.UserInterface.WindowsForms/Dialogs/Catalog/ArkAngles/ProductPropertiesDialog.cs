using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Catalog.ArkAngles;

namespace UniversalEditor.Dialogs.Catalog.ArkAngles
{
	internal partial class ProductPropertiesDialogImpl : Form
	{
		public ProductPropertiesDialogImpl()
		{
			InitializeComponent();
			
			Font = SystemFonts.MenuFont;
			
			RefreshButtons();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtProductTitle.Text))
			{
				MessageBox.Show("Please enter a product title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			List<string> warnings = new List<string>();
			if (cboCategory.SelectedIndex == -1)
			{
				warnings.Add("You did not specify a category.");
			}
			if (cboPlatform.SelectedIndex == -1)
			{
				warnings.Add("You did not specify a platform.");
			}
			if (cboListing.SelectedIndex == -1)
			{
				warnings.Add("You did not specify a listing.");
			}

			if (warnings.Count > 0)
			{
				StringBuilder issues = new StringBuilder();
				foreach (string warning in warnings)
				{
					issues.Append("•");
					issues.Append(" ");
					issues.Append(warning);
					if (warnings.IndexOf(warning) < warnings.Count - 1) issues.AppendLine();
				}
				if (MessageBox.Show("The following issues were detected that you may want to resolve before continuing:\r\n\r\n" + issues.ToString() + "\r\n\r\nContinue anyway?", "Unresolved Issues", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No) return;
			}


			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdKeywordAdd_Click(object sender, EventArgs e)
		{
			KeywordPropertiesDialog dlg = new KeywordPropertiesDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = dlg.Item;
				lvKeywords.Items.Add(lvi);
			}
		}

		private void cmdKeywordModify_Click(object sender, EventArgs e)
		{
			if (lvKeywords.SelectedItems.Count < 1) return;

			KeywordPropertiesDialog dlg = new KeywordPropertiesDialog();
			dlg.Item = lvKeywords.SelectedItems[0].Text;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				lvKeywords.SelectedItems[0].Text = dlg.Item;
			}
		}

		private void cmdKeywordRemove_Click(object sender, EventArgs e)
		{
			if (lvKeywords.SelectedItems.Count < 1) return;

			if (MessageBox.Show("Are you sure you want to remove the selected keywords from the list?", "Remove Keywords", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

			while (lvKeywords.SelectedItems.Count > 0)
			{
				lvKeywords.SelectedItems[0].Remove();
			}
		}

		private void cmdKeywordClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all keywords from the list?", "Remove Keywords", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
			lvKeywords.Items.Clear();
		}

		private void lvKeywords_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void lvKeywords_ItemActivate(object sender, EventArgs e)
		{
			cmdKeywordModify_Click(sender, e);
		}

		private void RefreshButtons()
		{
			cmdKeywordModify.Enabled = (lvKeywords.SelectedItems.Count == 1);
			cmdKeywordRemove.Enabled = (lvKeywords.SelectedItems.Count >= 1);
			cmdKeywordClear.Enabled = (lvKeywords.Items.Count > 0);

			cmdFileModify.Enabled = (lvFiles.SelectedItems.Count == 1);
			cmdFileRemove.Enabled = (lvFiles.SelectedItems.Count >= 1);
			cmdFileClear.Enabled = (lvFiles.Items.Count > 0);
		}

		private void cmdFileAdd_Click(object sender, EventArgs e)
		{
			AttachmentPropertiesDialog dlg = new AttachmentPropertiesDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = dlg.Item;
				lvFiles.Items.Add(lvi);
			}
		}

		private void cmdFileModify_Click(object sender, EventArgs e)
		{
			if (lvFiles.SelectedItems.Count < 1) return;

			AttachmentPropertiesDialog dlg = new AttachmentPropertiesDialog();
			dlg.Item = lvFiles.SelectedItems[0].Text;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				lvFiles.SelectedItems[0].Text = dlg.Item;
			}
		}

		private void cmdFileRemove_Click(object sender, EventArgs e)
		{
			if (lvFiles.SelectedItems.Count < 1) return;

			if (MessageBox.Show("Are you sure you want to remove the selected files from the list?", "Remove Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

			while (lvFiles.SelectedItems.Count > 0)
			{
				lvFiles.SelectedItems[0].Remove();
			}
		}

		private void cmdFileClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to remove all files from the list?", "Remove Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
			lvFiles.Items.Clear();
		}

		private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void lvFiles_ItemActivate(object sender, EventArgs e)
		{
			cmdFileModify_Click(sender, e);
		}
	}
	public class ProductPropertiesDialog
	{
		private CatalogObjectModel mvarCatalog = null;
		public CatalogObjectModel Catalog { get { return mvarCatalog; } set { mvarCatalog = value; } }

		private Product mvarItem = null;
		public Product Item { get { return mvarItem; } set { mvarItem = value; } }

		public DialogResult ShowDialog()
		{
			if (mvarCatalog == null) throw new ArgumentNullException("Please specify a catalog");
			
			ProductPropertiesDialogImpl dlg = new ProductPropertiesDialogImpl();

			dlg.cboCategory.Items.Clear();
			foreach (Category item in mvarCatalog.Categories)
			{
				dlg.cboCategory.Items.Add(item);
			}

			dlg.cboListing.Items.Clear();
			foreach (Listing item in mvarCatalog.Listings)
			{
				dlg.cboListing.Items.Add(item);
			}

			dlg.cboPlatform.Items.Clear();
			foreach (Platform item in mvarCatalog.Platforms)
			{
				dlg.cboPlatform.Items.Add(item);
			}

			if (mvarItem == null) mvarItem = new Product();

			if (mvarItem.Category == null)
			{
				dlg.cboCategory.SelectedItem = null;
			}
			else
			{
				dlg.cboCategory.SelectedItem = mvarItem.Category;
			}

			if (mvarItem.Listing == null)
			{
				dlg.cboListing.SelectedItem = null;
			}
			else
			{
				dlg.cboListing.SelectedItem = mvarItem.Listing;
			}

			if (mvarItem.Platform == null)
			{
				dlg.cboPlatform.SelectedItem = null;
			}
			else
			{
				dlg.cboPlatform.SelectedItem = mvarItem.Platform;
			}

			dlg.txtProductTitle.Text = mvarItem.Title;

			foreach (string keyword in mvarItem.Keywords)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = keyword;
				dlg.lvKeywords.Items.Add(lvi);
			}

			foreach (string fileName in mvarItem.AssociatedFiles)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = fileName;
				dlg.lvFiles.Items.Add(lvi);
			}

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarItem.Category = (dlg.cboCategory.SelectedItem as Category);
				mvarItem.Listing = (dlg.cboListing.SelectedItem as Listing);
				mvarItem.Platform = (dlg.cboPlatform.SelectedItem as Platform);
				mvarItem.Title = dlg.txtProductTitle.Text;

				mvarItem.Keywords.Clear();
				foreach (ListViewItem lvi in dlg.lvKeywords.Items)
				{
					mvarItem.Keywords.Add(lvi.Text);
				}

				mvarItem.AssociatedFiles.Clear();
				foreach (ListViewItem lvi in dlg.lvFiles.Items)
				{
					mvarItem.AssociatedFiles.Add(lvi.Text);
				}
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
