using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Dialogs.Contact;
using UniversalEditor.ObjectModels.Contact;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Contact
{
	public partial class ContactEditor : Editor
	{
		public ContactEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ContactObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();

			ContactObjectModel contact = (ObjectModel as ContactObjectModel);
			if (contact == null) return;


		}

		private void lvNames_RequestItemProperties(object sender, AwesomeControls.CollectionListView.ItemPropertiesEventArgs e)
		{
			NamePropertiesDialog dlg = new NamePropertiesDialog();

			ContactName name = (e.Item.Data as ContactName);
			if (name == null) name = new ContactName();

			dlg.GivenName = name.GivenName;
			dlg.MiddleName = name.MiddleName;
			dlg.FamilyName = name.FamilyName;
			dlg.DisplayName = name.FormattedName;
			dlg.PersonalTitle = name.Title;
			dlg.Nickname = name.Nickname;

			if (dlg.ShowDialog() == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}

			BeginEdit();

			name.GivenName = dlg.GivenName;
			name.MiddleName = dlg.MiddleName;
			name.FamilyName = dlg.FamilyName;
			name.FormattedName = dlg.DisplayName;
			name.Title = dlg.PersonalTitle;
			name.Nickname = dlg.Nickname;

			e.Item.Text = name.GivenName;
			e.Item.Details.Clear();

			e.Item.Data = name;

			EndEdit();
		}

		private void lvEmailAddresses_RequestItemProperties(object sender, AwesomeControls.CollectionListView.ItemPropertiesEventArgs e)
		{
			EmailPropertiesDialog dlg = new EmailPropertiesDialog();

			ContactEmailAddress email = (e.Item.Data as ContactEmailAddress);
			if (email == null) email = new ContactEmailAddress();

			dlg.EmailAddress = email.Address;
			dlg.ElementID = email.ElementID;
			dlg.IsEmpty = email.IsEmpty;
			foreach (ContactLabel item in email.Labels)
			{
				dlg.Labels.Add(item);
			}
			dlg.ModificationDate = email.ModificationDate;
			// dlg.Type = email.Type;

			if (dlg.ShowDialog() == DialogResult.Cancel)
			{
				e.Cancel = true;
				return;
			}

			BeginEdit();

			email.Address = dlg.EmailAddress;
			email.ElementID = dlg.ElementID;
			email.IsEmpty = dlg.IsEmpty;
			email.Labels.Clear();
			foreach (ContactLabel item in dlg.Labels)
			{
				email.Labels.Add(item);
			}
			email.ModificationDate = dlg.ModificationDate;
			// email.Type = dlg.Type;

			e.Item.Text = email.Address;
			e.Item.Details.Clear();

			e.Item.Data = email;

			EndEdit();
		}
	}
}
