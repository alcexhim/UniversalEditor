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
	internal partial class AttachmentPropertiesDialogImpl : Form
	{
		public AttachmentPropertiesDialogImpl()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtTitle.Text))
			{
				MessageBox.Show("Please enter or choose a file name for this attachment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtTitle.Text = System.IO.Path.GetFileName(ofd.FileName);
			}
		}
	}

	[System.Diagnostics.DebuggerNonUserCode()]
	public class AttachmentPropertiesDialog
	{
		private string mvarItem = String.Empty;
		public string Item { get { return mvarItem; } set { mvarItem = value; } }
		public DialogResult ShowDialog()
		{
			AttachmentPropertiesDialogImpl dlg = new AttachmentPropertiesDialogImpl();

			dlg.txtTitle.Text = mvarItem;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarItem = dlg.txtTitle.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
