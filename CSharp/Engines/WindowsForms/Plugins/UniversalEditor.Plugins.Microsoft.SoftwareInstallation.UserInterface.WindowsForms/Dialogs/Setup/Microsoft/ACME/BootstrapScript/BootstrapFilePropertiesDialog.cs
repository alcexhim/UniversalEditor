using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Setup.Microsoft.ACME.BootstrapScript
{
	public partial class BootstrapFilePropertiesDialogImpl : Form
	{
		public BootstrapFilePropertiesDialogImpl()
		{
			InitializeComponent();
		}

		private void cmdBrowseSourceFileName_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtSourceFileName.Text = ofd.FileName;
			}
		}

		private void cmdBrowseDestinationFileName_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				txtDestinationFileName.Text = sfd.FileName;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtSourceFileName.Text))
			{
				MessageBox.Show("Please provide a source file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (String.IsNullOrEmpty(txtDestinationFileName.Text))
			{
				if (MessageBox.Show("You have not specified a destination file name.  Would you like to use the same file name ('" + System.IO.Path.GetFileName(txtSourceFileName.Text) + "') as the source file?", "Destination File Name", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
				txtDestinationFileName.Text = System.IO.Path.GetFileName(txtSourceFileName.Text);
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
	public class BootstrapFilePropertiesDialog
	{
		private string mvarSourceFileName = String.Empty;
		public string SourceFileName { get { return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		public DialogResult ShowDialog()
		{
			BootstrapFilePropertiesDialogImpl dlg = new BootstrapFilePropertiesDialogImpl();
			dlg.txtSourceFileName.Text = mvarSourceFileName;
			dlg.txtDestinationFileName.Text = mvarDestinationFileName;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarSourceFileName = dlg.txtSourceFileName.Text;
				mvarDestinationFileName = dlg.txtDestinationFileName.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
