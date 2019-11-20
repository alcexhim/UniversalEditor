//
//  DatabaseEditor.cs
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
using System.Collections.Generic;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Database
{
	public partial class DatabaseEditor
	{
		public DatabaseEditor()
		{
			InitializeComponent();
		}

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

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tmDatabase.Rows.Clear();

			DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
			if (db == null)
				return;

			TreeModelRow rowTables = new TreeModelRow();
			rowTables.RowColumns.Add(new TreeModelRowColumn(tmDatabase.Columns[0], String.Format("Tables ({0})", db.Tables.Count)));

			foreach (DatabaseTable table in db.Tables)
			{
				TreeModelRow rowTable = new TreeModelRow();
				rowTable.RowColumns.Add(new TreeModelRowColumn(tmDatabase.Columns[0], table.Name));

				TreeModelRow rowColumns = new TreeModelRow();
				rowColumns.RowColumns.Add(new TreeModelRowColumn(tmDatabase.Columns[0], "Columns"));
				foreach (DatabaseField field in table.Fields)
				{
					TreeModelRow rowColumn = new TreeModelRow();
					rowColumn.RowColumns.Add(new TreeModelRowColumn(tmDatabase.Columns[0], String.Format("{0} ({1}, default {2})", field.Name, field.Value == null ? (field.DataType != null ? field.DataType.Name : String.Empty) : field.Value.GetType().Name, field.Value == null ? "NULL" : field.Value.ToString())));
					rowColumns.Rows.Add(rowColumn);
				}
				rowTable.Rows.Add(rowColumns);

				rowTables.Rows.Add(rowTable);
			}

			if (db.Tables.Count > 0)
			{
				List<Type> list = new List<Type>();
				for (int i = 0; i < db.Tables[0].Fields.Count; i++)
				{
					list.Add(typeof(string));
				}
				this.tmResults = new DefaultTreeModel(list.ToArray());
				for (int i = 0; i < db.Tables[0].Fields.Count; i++)
				{
					lvResults.Columns.Add(new ListViewColumnText(this.tmResults.Columns[i], db.Tables[0].Fields[i].Name));
				}
				foreach (DatabaseRecord rec in db.Tables[0].Records)
				{
					TreeModelRow row = new TreeModelRow();
					for (int c = 0;  c < rec.Fields.Count;  c++)
					{
						row.RowColumns.Add(new TreeModelRowColumn(tmResults.Columns[c], rec.Fields[c].Value == null ? "NULL" : rec.Fields[c].Value.ToString()));
					}
					tmResults.Rows.Add(row);
				}
				lvResults.Model = tmResults;
				this.txtQuery.Text = "SELECT * FROM '" + db.Tables[0].Name + "'";
			}

			tmDatabase.Rows.Add(rowTables);
		}
	}
}
