//
//  ContactEditor.cs - a UWT editor for ContactObjectModel
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using UniversalEditor.UserInterface;

using UniversalEditor.ObjectModels.Contact;
using UniversalWidgetToolkit.Layouts;
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using MBS.Framework.Drawing;
using System.Text;

namespace UniversalEditor.Editors.Contact
{
	public class ContactEditor : Editor
	{
		public ContactEditor ()
		{
			InitializeComponent ();
		}

		private DropDownButton btnName;
		private Button btnPhoto;

		private TextBox txtGivenName;
		private TextBox txtMiddleName;
		private TextBox txtFamilyName;

		private DefaultTreeModel tmAddresses;

		private ListView lvPhysicalAddresses;
		private Container ct;
		private Container ctName;

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout (Orientation.Vertical);
			this.Padding = new Padding (26);

			StackSidebar sidebar = new StackSidebar ();

			#region Summary Panel
			{
				ct = new Container ();
				ct.Name = "ctSummary";
				ct.Text = "Summary";
				ct.Layout = new GridLayout ();

				btnPhoto = new Button ();
				btnPhoto.Size = new Dimension2D (128, 128);
				ct.Controls.Add (btnPhoto, new GridLayout.Constraints (0, 0, 1, 1, ExpandMode.None));

				ctName = new Container ();
				ctName.Layout = new GridLayout ();

				Label lblGivenName = new Label ();
				lblGivenName.Text = "_Given";
				lblGivenName.Attributes.Add ("scale", 0.8);
				txtGivenName = new TextBox ();
				txtGivenName.SetExtraData<string> ("PropertyObject", "Name");
				txtGivenName.SetExtraData<string> ("PropertyName", "GivenName");
				txtGivenName.Changed += TextBox_Changed;
				txtGivenName.LostFocus += TextBox_LostFocus;
				ctName.Controls.Add (txtGivenName, new GridLayout.Constraints (0, 0, 1, 1, ExpandMode.Both));
				ctName.Controls.Add (lblGivenName, new GridLayout.Constraints (1, 0, 1, 1));

				Label lblMiddleName = new Label ();
				lblMiddleName.Text = "_Middle";
				lblMiddleName.Attributes.Add ("scale", 0.8);
				txtMiddleName = new TextBox ();
				txtMiddleName.SetExtraData<string> ("PropertyObject", "Name");
				txtMiddleName.SetExtraData<string> ("PropertyName", "MiddleName");
				txtMiddleName.Changed += TextBox_Changed;
				txtMiddleName.LostFocus += TextBox_LostFocus;
				ctName.Controls.Add (txtMiddleName, new GridLayout.Constraints (0, 1, 1, 1, ExpandMode.Both));
				ctName.Controls.Add (lblMiddleName, new GridLayout.Constraints (1, 1, 1, 1));

				Label lblFamilyName = new Label ();
				lblFamilyName.Text = "_Family";
				lblFamilyName.Attributes.Add ("scale", 0.8);
				txtFamilyName = new TextBox ();
				txtFamilyName.Changed += TextBox_Changed;
				txtFamilyName.SetExtraData<string> ("PropertyObject", "Name");
				txtFamilyName.SetExtraData<string> ("PropertyName", "FamilyName");
				txtFamilyName.LostFocus += TextBox_LostFocus;
				ctName.Controls.Add (txtFamilyName, new GridLayout.Constraints (0, 2, 1, 1, ExpandMode.Both));
				ctName.Controls.Add (lblFamilyName, new GridLayout.Constraints (1, 2, 1, 1));

				TextBox txtJobTitle = new TextBox ();
				txtJobTitle.Changed += TextBox_Changed;
				txtJobTitle.SetExtraData<string> ("PropertyObject", "Job");
				txtJobTitle.SetExtraData<string> ("PropertyName", "JobTitle");
				txtJobTitle.LostFocus += TextBox_LostFocus;
				Label lblJobTitle = new Label ();
				lblJobTitle.Text = "_Job title";
				lblJobTitle.Attributes.Add ("scale", 0.8);
				ctName.Controls.Add (txtJobTitle, new GridLayout.Constraints (2, 0, 1, 2, ExpandMode.Both));
				ctName.Controls.Add (lblJobTitle, new GridLayout.Constraints (3, 0, 1, 2, ExpandMode.Both));

				TextBox txtCompany = new TextBox ();
				txtCompany.Changed += TextBox_Changed;
				txtCompany.SetExtraData<string> ("PropertyObject", "Job");
				txtCompany.SetExtraData<string> ("PropertyName", "Company");
				txtCompany.LostFocus += TextBox_LostFocus;
				Label lblCompany = new Label ();
				lblCompany.Text = "_Company";
				lblCompany.Attributes.Add ("scale", 0.8);
				ctName.Controls.Add (txtCompany, new GridLayout.Constraints (2, 2, 1, 1, ExpandMode.Both));
				ctName.Controls.Add (lblCompany, new GridLayout.Constraints (3, 2, 1, 1, ExpandMode.Both));

				ct.Controls.Add (ctName, new GridLayout.Constraints (0, 1, 2, 1, ExpandMode.None));

				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			#endregion
			#region Addresses
			{
				Container ct = new Container ();
				ct.Name = "ctAddresses";
				ct.Text = "Addresses";
				ct.Layout = new BoxLayout (Orientation.Vertical);

				tmAddresses = new DefaultTreeModel (new Type [] { typeof (string) });

				lvPhysicalAddresses = new ListView ();
				lvPhysicalAddresses.Columns.Add (new ListViewColumnText (tmAddresses.Columns [0], "Address"));
				lvPhysicalAddresses.Model = tmAddresses;
				ct.Controls.Add (lvPhysicalAddresses, new BoxLayout.Constraints (true, true));

				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			#endregion
			#region Family
			{
				Container ct = new Container ();
				ct.Name = "ctFamily";
				ct.Text = "Family and Relationships";
				ct.Layout = new BoxLayout (Orientation.Vertical);

				tmAddresses = new DefaultTreeModel (new Type [] { typeof (string) });

				ListView lvAddresses = new ListView ();
				lvAddresses.Columns.Add (new ListViewColumnText (tmAddresses.Columns [0], "Address"));
				ct.Controls.Add (lvAddresses, new BoxLayout.Constraints (true, true));

				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			#endregion
			#region Digital IDs
			{
				Container ct = new Container ();
				ct.Name = "ctDigitalIDs";
				ct.Text = "Digital IDs";
				ct.Layout = new BoxLayout (Orientation.Vertical);

				tmAddresses = new DefaultTreeModel (new Type [] { typeof (string) });

				ListView lvAddresses = new ListView ();
				lvAddresses.Columns.Add (new ListViewColumnText (tmAddresses.Columns [0], "Address"));
				ct.Controls.Add (lvAddresses, new BoxLayout.Constraints (true, true));

				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			#endregion
			#region Notes
			{
				Container ct = new Container ();
				ct.Name = "ctNotes";
				ct.Text = "Notes";
				ct.Layout = new BoxLayout (Orientation.Vertical);

				TextBox txtNotes = new TextBox ();
				txtNotes.Multiline = true;
				ct.Controls.Add (txtNotes, new BoxLayout.Constraints (true, true));

				StackSidebarPanel panel = new StackSidebarPanel ();
				panel.Control = ct;
				sidebar.Items.Add (panel);
			}
			#endregion

			this.Controls.Add (sidebar, new BoxLayout.Constraints(true, true));
		}

		private void AddDetailRow (int iRow, string detailType, string value)
		{
			ComboBox cboDetail = new ComboBox ();
			cboDetail.Text = detailType;

			TextBox txtDetail = new TextBox ();
			Button btnDelete = new Button (ButtonStockType.Delete);
			ct.Controls.Add (cboDetail, new GridLayout.Constraints (iRow + 1, 0, 1, 1, ExpandMode.None));
			ct.Controls.Add (txtDetail, new GridLayout.Constraints (iRow + 1, 1, 1, 1, ExpandMode.Horizontal));
			ct.Controls.Add (btnDelete, new GridLayout.Constraints (iRow + 1, 2, 1, 1, ExpandMode.None));
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference ()
		{
			if (_er == null) {
				_er = base.MakeReference ();
			}
			_er.SupportedObjectModels.Add (typeof(ContactObjectModel));
			return _er;
		}

		protected override void OnObjectModelChanged (EventArgs e)
		{
			txtGivenName.Text = String.Empty;
			txtMiddleName.Text = String.Empty;
			txtFamilyName.Text = String.Empty;

			tmAddresses.Rows.Clear ();

			base.OnObjectModelChanged (e);

			ContactObjectModel contact = (ObjectModel as ContactObjectModel);
			if (contact.Names.Count > 0) {
				txtGivenName.Text = contact.Names [0].GivenName;
				txtMiddleName.Text = contact.Names [0].MiddleName;
				txtFamilyName.Text = contact.Names [0].FamilyName;
			}

			foreach (ContactPhysicalAddress address in contact.PhysicalAddresses) {
				if (address.IsEmpty) continue;

				string addr = address.ToString ();
				tmAddresses.Rows.Add (new TreeModelRow (new TreeModelRowColumn [] { new TreeModelRowColumn (tmAddresses.Columns [0], addr) }));
			}
			lvPhysicalAddresses.Model = tmAddresses;

			foreach (ContactPhysicalAddress addr in contact.PhysicalAddresses)
			{
				AddDetailRow(0, "Address", addr.ToString());
			}
		}

		/// <summary>
		/// Raised when any of the text box fields on this Editor has changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void TextBox_Changed(object sender, EventArgs e)
		{
		}

		private void TextBox_LostFocus(object sender, EventArgs e)
		{
			// eeek! I dont' rememver how  this works...... !!!a
			TextBox txt = (sender as TextBox);
			if (ProcessingUndoRedo)
				return;

			string propertyObject = txt.GetExtraData<string> ("PropertyObject");
			string propertyName = txt.GetExtraData<string> ("PropertyName");

			ContactObjectModel contact = (ObjectModel as ContactObjectModel);

			switch (propertyObject)
			{
				case "Name":
				{
					if (contact.Names.Count == 0) {
						contact.Names.Add (new ContactName ());
					}
					SetProperty<string> (propertyName, txt.Text, contact.Names[0], txt);
					break;
				}
				case "Job":
				{
					break;
				}
			}
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
	}
}

