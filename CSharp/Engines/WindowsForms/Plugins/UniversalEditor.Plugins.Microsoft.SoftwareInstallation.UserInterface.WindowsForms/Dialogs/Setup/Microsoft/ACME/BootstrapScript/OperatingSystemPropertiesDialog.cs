using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Setup.Microsoft.ACME.BootstrapScript
{
	internal partial class OperatingSystemPropertiesDialogImpl : Form
	{
		public OperatingSystemPropertiesDialogImpl()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtName.Text))
			{
				MessageBox.Show("Please enter a name for this operating system.", "Enter Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	public class OperatingSystemPropertiesDialog
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		public DialogResult ShowDialog()
		{
			OperatingSystemPropertiesDialogImpl dlg = new OperatingSystemPropertiesDialogImpl();
			dlg.txtName.Text = mvarName;
			dlg.chkEnabled.Checked = mvarEnabled;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarName = dlg.txtName.Text;
				mvarEnabled = dlg.chkEnabled.Checked;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
