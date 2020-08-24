//
//  DatabaseEditor.cs - provides a UWT-based Editor for manipulating database files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Collections.Generic;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Database
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for manipulating database files.
	/// </summary>
	public class DatabaseEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Views.Add("Design");
				_er.Views.Add("Script");
				_er.SupportedObjectModels.Add(typeof(DatabaseObjectModel));
			}
			return _er;
		}

		public DatabaseEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			DesignView = new Views.DesignView();
			Controls.Add(DesignView, new BoxLayout.Constraints(true, true));

			ScriptView = new Views.ScriptView();
			Controls.Add(ScriptView, new BoxLayout.Constraints(true, true));

			DesignView.Editor = this;
			ScriptView.Editor = this;

			ScriptView.Visible = false;
		}

		protected override void OnViewChanged(EditorViewChangedEventArgs e)
		{
			base.OnViewChanged(e);

			switch (e.NewView.Title)
			{
				case "Design":
				{
					DesignView.Visible = true;
					ScriptView.Visible = false;
					break;
				}
				case "Script":
				{
					DesignView.Visible = false;
					ScriptView.Visible = true;
					break;
				}
			}
		}

		private Views.DesignView DesignView = null;
		private Views.ScriptView ScriptView = null;

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private string ToString(object value)
		{
			if (value == null) return "NULL";
			if (value is string)
			{
				return String.Format("'{0}'", value);
			}
			return value.ToString();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			// eww
			DesignView.ObjectModel = ObjectModel;
			ScriptView.ObjectModel = ObjectModel;

			DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
			if (db == null) return;

			EditorDocumentExplorerNode nodeTables = new EditorDocumentExplorerNode("Tables");

			foreach (DatabaseTable table in db.Tables)
			{
				EditorDocumentExplorerNode nodeTable = new EditorDocumentExplorerNode(table.Name);
				nodeTable.SetExtraData<DatabaseTable>("table", table);

				EditorDocumentExplorerNode nodeColumns = new EditorDocumentExplorerNode("Columns");
				foreach (DatabaseField field in table.Fields)
				{
					EditorDocumentExplorerNode nodeColumn = new EditorDocumentExplorerNode(String.Format("{0} ({1}, default {2})", field.Name, field.Value == null ? (field.DataType != null ? field.DataType.Name : String.Empty) : field.Value.GetType().Name, ToString(field.Value)));

					nodeColumn.SetExtraData<DatabaseField>("column", field);
					nodeColumns.Nodes.Add(nodeColumn);
				}

				nodeTable.Nodes.Add(nodeColumns);
				nodeTables.Nodes.Add(nodeTable);
			}

			DocumentExplorer.Nodes.Add(nodeTables);
		}

		protected internal override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			if (e.Node == null) return;

			DatabaseTable table = e.Node.GetExtraData<DatabaseTable>("table");
			if (table != null)
			{
				DesignView.Table = table;
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			DocumentExplorer.BeforeContextMenu += DocumentExplorer_BeforeContextMenu;
			OnObjectModelChanged(EventArgs.Empty);

			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_NewTable", delegate (object sender, EventArgs ee)
			{
				DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
				if (db != null)
				{
					int newTableCount = 0;
					for (int i = 0; i < db.Tables.Count; i++)
					{
						if (db.Tables[i].Name.StartsWith("New Table ") && Int32.TryParse(db.Tables[i].Name.Substring("New Table ".Length), out int dummy))
						{
							newTableCount++;
						}
					}
					newTableCount++;

					BeginEdit();

					DatabaseTable dt = new DatabaseTable();
					dt.Name = "New Table " + newTableCount.ToString();
					db.Tables.Add(dt);

					EndEdit();

					EditorDocumentExplorerNode node = new EditorDocumentExplorerNode(dt.Name);
					node.SetExtraData<DatabaseTable>("table", dt);
					DocumentExplorer.Nodes[0].Nodes.Add(node);
				}
			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Design", delegate (object sender, EventArgs ee)
			{
				ScriptView.Visible = false;
				DesignView.Visible = true;
			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Select", delegate (object sender, EventArgs ee)
			{
				DesignView.Visible = false;
				ScriptView.Visible = true;
			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Edit", delegate (object sender, EventArgs ee)
			{

			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Encrypt", delegate (object sender, EventArgs ee)
			{

			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Rename", delegate (object sender, EventArgs ee)
			{

			});
			Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Delete", delegate (object sender, EventArgs ee)
			{

			});
		}

		void DocumentExplorer_BeforeContextMenu(object sender, EditorDocumentExplorerBeforeContextMenuEventArgs e)
		{
			e.ContextMenuCommandID = "DatabaseEditor_ContextMenu_Table";
		}
	}
}
