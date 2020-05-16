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

using UniversalEditor.ObjectModels.Database;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Database
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for manipulating database files.
	/// </summary>
	[ContainerLayout("~/Editors/Database/DatabaseEditor.glade")]
	public class DatabaseEditor : Editor
	{
		private SyntaxTextBox txtQuery;
		private ListView lvResults;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(DatabaseObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			OnObjectModelChanged(EventArgs.Empty);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
			if (db == null)
				return;

			EditorDocumentExplorerNode nodeTables = new EditorDocumentExplorerNode("Tables");

			foreach (DatabaseTable table in db.Tables)
			{
				EditorDocumentExplorerNode nodeTable = new EditorDocumentExplorerNode(table.Name);
				nodeTable.SetExtraData<DatabaseTable>("table", table);

				EditorDocumentExplorerNode nodeColumns = new EditorDocumentExplorerNode("Columns");
				foreach (DatabaseField field in table.Fields)
				{
					EditorDocumentExplorerNode nodeColumn = new EditorDocumentExplorerNode(String.Format("{0} ({1}, default {2})", field.Name, field.Value == null ? (field.DataType != null ? field.DataType.Name : String.Empty) : field.Value.GetType().Name, field.Value == null ? "NULL" : field.Value.ToString()));

					nodeColumn.SetExtraData<DatabaseField>("column", field);
					nodeColumns.Nodes.Add(nodeColumn);
				}

				nodeTable.Nodes.Add(nodeColumns);
				nodeTables.Nodes.Add(nodeTable);
			}

			DocumentExplorer.Nodes.Add(nodeTables);

			if (db.Tables.Count > 0)
			{
				List<Type> list = new List<Type>();
				for (int i = 0; i < db.Tables[0].Fields.Count; i++)
				{
					list.Add(db.Tables[0].Fields[i].DataType == null ? typeof(string) : db.Tables[0].Fields[i].DataType);
				}
				DefaultTreeModel tmResults = new DefaultTreeModel(list.ToArray());
				for (int i = 0; i < db.Tables[0].Fields.Count; i++)
				{
					lvResults.Columns.Add(new ListViewColumnText(tmResults.Columns[i], db.Tables[0].Fields[i].Name));
				}
				foreach (DatabaseRecord rec in db.Tables[0].Records)
				{
					TreeModelRow row = new TreeModelRow();
					for (int c = 0;  c < rec.Fields.Count;  c++)
					{
						row.RowColumns.Add(new TreeModelRowColumn(tmResults.Columns[c], rec.Fields[c].Value == null ? "NULL" : rec.Fields[c].Value));
					}
					tmResults.Rows.Add(row);
				}
				lvResults.Model = tmResults;
				this.txtQuery.Text = "SELECT * FROM '" + db.Tables[0].Name + "'";
			}
		}
	}
}
