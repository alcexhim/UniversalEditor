//
//  DesignView.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.UserInterface;

namespace UniversalEditor.UserInterface.Editors.Database.Views
{
	[ContainerLayout("~/Editors/Database/Views/DesignView.glade")]
	public class DesignView : View
	{
		private TextBox txtName;
		private TextBox txtDescription;
		private Toolbar tbColumns;
		private MBS.Framework.UserInterface.Controls.ListView.ListViewControl tvColumns;

		private DatabaseTable _Table = null;
		public DatabaseTable Table { get { return _Table; } set { _Table = value; Update(); } }

		private bool _InhibitEditing = false;

		[EventHandler(nameof(txtName), nameof(TextBox.Changed))]
		private void txtName_Changed(object sender, EventArgs e)
		{
			if (_InhibitEditing) return;
			if (Table == null) return;

			Editor.BeginEdit();

			Table.Name = txtName.Text;

			Editor.EndEdit();
		}

		[EventHandler(nameof(tvColumns), nameof(Control.BeforeContextMenu))]
		private void tvColumns_BeforeContextMenu(object sender, EventArgs e)
		{
			tvColumns.ContextMenuCommandID = "DatabaseEditor_ContextMenu_Columns";
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
			if (db == null) return;

			Update();
		}
		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			((ToolbarItemButton)tbColumns.Items["tsbColumnAdd"]).Click += tsbColumnAdd_Click;
			((ToolbarItemButton)tbColumns.Items["tsbColumnEdit"]).Click += tsbColumnEdit_Click;
			((ToolbarItemButton)tbColumns.Items["tsbColumnRemove"]).Click += tsbColumnRemove_Click;
			((ToolbarItemButton)tbColumns.Items["tsbColumnMoveUp"]).Click += tsbColumnMoveUp_Click;
			((ToolbarItemButton)tbColumns.Items["tsbColumnMoveDown"]).Click += tsbColumnMoveDown_Click;
		}

		private void tsbColumnAdd_Click(object sender, EventArgs e)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tvColumns.Model.Columns[0], ""), // Name
				new TreeModelRowColumn(tvColumns.Model.Columns[1], ""), // Data type
				new TreeModelRowColumn(tvColumns.Model.Columns[2], true), // Not null
				new TreeModelRowColumn(tvColumns.Model.Columns[3], false), // Identity
				new TreeModelRowColumn(tvColumns.Model.Columns[4], "") // Default value
			});

			tvColumns.Model.Rows.Add(row);
			tvColumns.Focus(row, tvColumns.Columns[0], tvColumns.Columns[0].Renderers[0], true);
		}
		private void tsbColumnEdit_Click(object sender, EventArgs e)
		{
		}
		private void tsbColumnRemove_Click(object sender, EventArgs e)
		{
		}
		private void tsbColumnMoveUp_Click(object sender, EventArgs e)
		{
		}
		private void tsbColumnMoveDown_Click(object sender, EventArgs e)
		{
		}

		private void Update()
		{
			if (Table != null)
			{
				_InhibitEditing = true;
				txtName.Text = Table.Name;
				_InhibitEditing = false;

				tvColumns.Model.Rows.Clear();

				for (int i = 0; i < Table.Fields.Count; i++)
				{
					TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tvColumns.Model.Columns[0], Table.Fields[i].Name),
						new TreeModelRowColumn(tvColumns.Model.Columns[1], Table.Fields[i].DataType),
						new TreeModelRowColumn(tvColumns.Model.Columns[2], Table.Fields[i].Value == null ? "NULL" : "NOT NULL"),
						new TreeModelRowColumn(tvColumns.Model.Columns[3], String.Empty),
						new TreeModelRowColumn(tvColumns.Model.Columns[4], Table.Fields[i].Value)
					});
					tvColumns.Model.Rows.Add(row);
				}
			}
		}
	}
}
