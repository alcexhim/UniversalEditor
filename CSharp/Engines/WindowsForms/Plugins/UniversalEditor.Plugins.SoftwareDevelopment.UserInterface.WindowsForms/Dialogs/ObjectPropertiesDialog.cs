using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs
{
	internal partial class ObjectPropertiesDialogBase : Form
	{
		public ObjectPropertiesDialogBase()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
	public class ObjectPropertiesDialog
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public DialogResult ShowDialog()
		{
			ObjectPropertiesDialogBase dlg = new ObjectPropertiesDialogBase();
			dlg.txtName.Text = mvarName;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarName = dlg.txtName.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
