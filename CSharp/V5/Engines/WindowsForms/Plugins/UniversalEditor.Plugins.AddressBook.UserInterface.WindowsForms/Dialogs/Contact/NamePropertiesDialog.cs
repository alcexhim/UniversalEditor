using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Contact
{
	internal partial class NamePropertiesDialogImpl : Form
	{
		public NamePropertiesDialogImpl()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}
	public class NamePropertiesDialog
	{
		private string mvarGivenName = String.Empty;
		public string GivenName { get { return mvarGivenName; } set { mvarGivenName = value; } }

		private string mvarMiddleName = String.Empty;
		public string MiddleName { get { return mvarMiddleName; } set { mvarMiddleName = value; } }

		private string mvarFamilyName = String.Empty;
		public string FamilyName { get { return mvarFamilyName; } set { mvarFamilyName = value; } }

		private string mvarDisplayName = String.Empty;
		public string DisplayName { get { return mvarDisplayName; } set { mvarDisplayName = value; } }

		private string mvarPersonalTitle = String.Empty;
		public string PersonalTitle { get { return mvarPersonalTitle; } set { mvarPersonalTitle = value; } }

		private string mvarNickname = String.Empty;
		public string Nickname { get { return mvarNickname; } set { mvarNickname = value; } }

		public DialogResult ShowDialog()
		{
			NamePropertiesDialogImpl dlg = new NamePropertiesDialogImpl();
			dlg.txtGivenName.Text = mvarGivenName;
			dlg.txtMiddleName.Text = mvarMiddleName;
			dlg.txtFamilyName.Text = mvarFamilyName;
			dlg.cboDisplayName.Text = mvarDisplayName;
			dlg.txtPersonalTitle.Text = mvarPersonalTitle;
			dlg.txtNickname.Text = mvarNickname;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarGivenName = dlg.txtGivenName.Text;
				mvarMiddleName = dlg.txtMiddleName.Text;
				mvarFamilyName = dlg.txtFamilyName.Text;
				mvarDisplayName = dlg.cboDisplayName.Text;
				mvarPersonalTitle = dlg.txtPersonalTitle.Text;
				mvarNickname = dlg.txtNickname.Text;

				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
