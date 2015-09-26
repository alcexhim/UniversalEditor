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
	internal partial class ListingPropertiesDialogImpl : Form
	{
		public ListingPropertiesDialogImpl()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtTitle.Text))
			{
				MessageBox.Show("Please provide a title for this listing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}

	[System.Diagnostics.DebuggerNonUserCode()]
	public class ListingPropertiesDialog
	{
		private Listing mvarItem = null;
		public Listing Item { get { return mvarItem; } set { mvarItem = value; } }
		public DialogResult ShowDialog()
		{
			ListingPropertiesDialogImpl dlg = new ListingPropertiesDialogImpl();

			if (mvarItem == null) mvarItem = new Listing();
			dlg.txtTitle.Text = mvarItem.Title;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarItem.Title = dlg.txtTitle.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
