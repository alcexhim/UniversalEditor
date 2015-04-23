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
		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(txtProductTitle.Text))
			{
				MessageBox.Show("Please enter a product title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cboCategory.SelectedIndex == -1)
			{
				MessageBox.Show("Please select a category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cboPlatform.SelectedIndex == -1)
			{
				MessageBox.Show("Please select a platform.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (cboListing.SelectedIndex == -1)
			{
				MessageBox.Show("Please select a listing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
	public class ProductPropertiesDialog
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Category mvarCategory = null;
		public Category Category { get { return mvarCategory; } set { mvarCategory = value; } }

		private Platform mvarPlatform = null;
		public Platform Platform { get { return mvarPlatform; } set { mvarPlatform = value; } }

		private Listing mvarListing = null;
		public Listing Listing { get { return mvarListing; } set { mvarListing = value; } }

		public DialogResult ShowDialog()
		{
			ProductPropertiesDialogImpl dlg = new ProductPropertiesDialogImpl();
			dlg.txtProductTitle.Text = mvarTitle;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarTitle = dlg.txtProductTitle.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
