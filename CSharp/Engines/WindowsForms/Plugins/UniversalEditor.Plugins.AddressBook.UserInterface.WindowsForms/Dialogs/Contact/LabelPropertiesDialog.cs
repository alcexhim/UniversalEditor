using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Contact
{
	internal partial class LabelPropertiesDialogImpl : Form
	{
		public LabelPropertiesDialogImpl()
		{
			InitializeComponent();
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
	public class LabelPropertiesDialog
	{
		private Guid mvarElementID = Guid.Empty;
		public Guid ElementID { get { return mvarElementID; } set { mvarElementID = value; } }

		private string mvarLabel = String.Empty;
		public string Label { get { return mvarLabel; } set { mvarLabel = value; } }

		private DateTime? mvarModificationDate = null;
		public DateTime? ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }

		private bool mvarIsEmpty = false;
		public bool IsEmpty { get { return mvarIsEmpty; } set { mvarIsEmpty = value; } }

		public DialogResult ShowDialog()
		{
			LabelPropertiesDialogImpl dlg = new LabelPropertiesDialogImpl();
			dlg.txtElementID.Value = mvarElementID;
			dlg.txtLabel.Text = mvarLabel;
			if (mvarModificationDate != null)
			{
				dlg.txtModificationDate.Value = mvarModificationDate.Value;
				dlg.txtModificationDate.Checked = true;
			}
			else
			{
				dlg.txtModificationDate.Checked = false;
			}
			dlg.chkIsEmpty.Checked = mvarIsEmpty;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarElementID = dlg.txtElementID.Value;
				mvarLabel = dlg.txtLabel.Text;
				if (dlg.txtModificationDate.Checked)
				{
					mvarModificationDate = dlg.txtModificationDate.Value;
				}
				else
				{
					mvarModificationDate = null;
				}
				mvarIsEmpty = dlg.chkIsEmpty.Checked;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
