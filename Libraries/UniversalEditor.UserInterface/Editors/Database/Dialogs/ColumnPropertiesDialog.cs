//
//  ColumnPropertiesDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using UniversalEditor.ObjectModels.Database;

namespace UniversalEditor.UserInterface.Editors.Database.Dialogs
{
	[ContainerLayout(typeof(ColumnPropertiesDialog), "UniversalEditor.UserInterface.Editors.Database.Dialogs.ColumnPropertiesDialog.glade")]
	public class ColumnPropertiesDialog : CustomDialog
	{
		private Button cmdOK;
		private Button cmdCancel;
		private TextBox txtColumnName;
		private ComboBox cboDataType;
		private TextBox txtDescription;
		private GroupBox fraDataTypeSpecificProperties;

		public void UpdateProperties(DatabaseField field)
		{
			field.Name = txtColumnName.Text;
			// field.DataType = cboDataType.SelectedItem
			// field.Description = txtDescription.Text;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			DefaultButton = cmdOK;
		}
	}
}
