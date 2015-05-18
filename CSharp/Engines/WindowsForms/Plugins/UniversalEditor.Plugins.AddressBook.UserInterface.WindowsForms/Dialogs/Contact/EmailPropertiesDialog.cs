using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Contact;

namespace UniversalEditor.Dialogs.Contact
{
	public partial class EmailPropertiesDialogImpl : Form
	{
		public EmailPropertiesDialogImpl()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdCompose_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:" + txtEmailAddress.Text);
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

		private void lvLabels_RequestItemProperties(object sender, AwesomeControls.CollectionListView.ItemPropertiesEventArgs e)
		{
			LabelPropertiesDialog dlg = new LabelPropertiesDialog();
			
			ContactLabel label = (e.Item.Data as ContactLabel);
			if (label == null) label = new ContactLabel();
			dlg.ElementID = label.ElementID;
			dlg.IsEmpty = label.IsEmpty;
			dlg.Label = label.Value;
			dlg.ModificationDate = label.ModificationDate;

			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				label.ElementID = dlg.ElementID;
				label.IsEmpty = dlg.IsEmpty;
				label.Value = dlg.Label;
				label.ModificationDate = dlg.ModificationDate;

				e.Item.Text = label.Value;
				e.Item.Data = label;
			}
		}
	}
	public class EmailPropertiesDialog
	{
		private string mvarEmailAddress = String.Empty;
		public string EmailAddress { get { return mvarEmailAddress; } set { mvarEmailAddress = value ; } }

		private Guid mvarElementID = Guid.Empty;
		public Guid ElementID { get { return mvarElementID; } set { mvarElementID = value; } }

		private DateTime? mvarModificationDate = null;
		public DateTime? ModificationDate { get { return mvarModificationDate; } set { mvarModificationDate = value; } }

		private bool mvarIsEmpty = false;
		public bool IsEmpty { get { return mvarIsEmpty; } set { mvarIsEmpty = value; } }

		private ContactLabel.ContactLabelCollection mvarLabels = new ContactLabel.ContactLabelCollection();
		public ContactLabel.ContactLabelCollection Labels { get { return mvarLabels; } }

		public DialogResult ShowDialog()
		{
			EmailPropertiesDialogImpl dlg = new EmailPropertiesDialogImpl();
			dlg.txtEmailAddress.Text = mvarEmailAddress;
			dlg.copc.ElementID = mvarElementID;
			dlg.copc.IsEmpty = mvarIsEmpty;
			dlg.copc.ModificationDate = mvarModificationDate;
			foreach (ContactLabel item in mvarLabels)
			{
				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.Text = item.Value;
				lvi.Data = item;
				dlg.lvLabels.Items.Add(lvi);
			}

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarEmailAddress = dlg.txtEmailAddress.Text;
				mvarElementID = dlg.copc.ElementID;
				mvarIsEmpty = dlg.copc.IsEmpty;
				mvarModificationDate = dlg.copc.ModificationDate;

				mvarLabels.Clear();
				foreach (AwesomeControls.ListView.ListViewItem lvi in dlg.lvLabels.Items)
				{
					mvarLabels.Add(lvi.Data as ContactLabel);
				}
				
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
