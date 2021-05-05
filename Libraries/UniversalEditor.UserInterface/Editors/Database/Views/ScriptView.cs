//
//  ScriptView.cs
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
using System.Collections.Generic;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.UserInterface;

namespace UniversalEditor.UserInterface.Editors.Database.Views
{
	[ContainerLayout("~/Editors/Database/Views/ScriptView.glade")]
	public class ScriptView : View
	{
		private SyntaxTextBox txtQuery;
		private ListViewControl lvResults;

		private string ScriptTable(DatabaseTable dt, ScriptTableMode mode)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			switch (mode)
			{
				case ScriptTableMode.Create:
				{
					sb.Append("CREATE");
					break;
				}
				case ScriptTableMode.Delete:
				{
					sb.Append("DELETE ");
					break;
				}
				case ScriptTableMode.Alter:
				{
					sb.Append("ALTER");
					break;
				}
				case ScriptTableMode.Drop:
				{
					sb.Append("DROP");
					break;
				}
				case ScriptTableMode.DropCreate:
				{
					sb.Append("DROP");
					break;
				}
				case ScriptTableMode.Select:
				{
					sb.Append("SELECT");
					break;
				}
				case ScriptTableMode.Insert:
				{
					sb.Append("INSERT INTO");
					break;
				}
			}

			switch (mode)
			{
				case ScriptTableMode.Create:
				case ScriptTableMode.Alter:
				case ScriptTableMode.Drop:
				case ScriptTableMode.DropCreate:
				{
					sb.Append(" TABLE [");
					sb.Append(dt.Name);
					sb.Append("]");
					break;
				}
				case ScriptTableMode.Select:
				case ScriptTableMode.Delete:
				{
					if (mode == ScriptTableMode.Select)
					{
						sb.Append("* ");
					}
					sb.Append("FROM '");
					sb.Append(dt.Name);
					sb.Append("'");
					break;
				}
				case ScriptTableMode.Insert:
				{
					sb.Append(' ');
					sb.Append(dt.Name);
					sb.Append(" (");
					for (int i = 0; i < dt.Fields.Count; i++)
					{
						sb.Append(dt.Fields[i].Name);
						if (i < dt.Fields.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(") VALUES (");
					for (int i = 0; i < dt.Fields.Count; i++)
					{
						if (dt.Fields[i].Value == null)
						{
							sb.Append("NULL");
						}
						else
						{
							if (dt.Fields[i].Value is string)
							{
								sb.Append("'");
							}
							sb.Append(dt.Fields[i].Value);
							if (dt.Fields[i].Value is string)
							{
								sb.Append("'");
							}
						}
						if (i < dt.Fields.Count - 1)
						{
							sb.Append(", ");
						}
					}
					sb.Append(')');
					break;
				}
			}
			switch (mode)
			{
				case ScriptTableMode.Create:
				{
					sb.Append("\n(\n");
					for (int i = 0; i < dt.Fields.Count; i++)
					{
						sb.Append('\t');
						sb.Append('[');
						sb.Append(dt.Fields[i].Name);
						sb.Append(']');
						sb.Append(' ');
						sb.Append('[');
						sb.Append(TypeToSQL(dt.Fields[i].DataType));
						sb.Append(']');
						sb.Append(' ');
						sb.Append("DEFAULT ");
						if (dt.Fields[i].Value != null)
						{
							if (dt.Fields[i].Value is string) sb.Append('\'');
							sb.Append(dt.Fields[i].Value.ToString());
							if (dt.Fields[i].Value is string) sb.Append('\'');
						}
						else
						{
							sb.Append("NULL");
						}
						if (i < dt.Fields.Count - 1)
							sb.AppendLine(", ");
					}
					sb.Append("\n)");
					break;
				}
			}
			sb.Append(';');
			return sb.ToString();
		}

		private void ScriptTable(DatabaseTable dt, ScriptTableMode mode, ScriptTableTo where)
		{
			string tableText = ScriptTable(dt, mode);
			if (mode == ScriptTableMode.DropCreate)
			{
				tableText = ScriptTable(dt, ScriptTableMode.Drop) + "\r\n\r\n" + ScriptTable(dt, ScriptTableMode.Create);
			}
			switch (where)
			{
				case ScriptTableTo.Window:
				{
					ObjectModels.Text.Plain.PlainTextObjectModel text = new ObjectModels.Text.Plain.PlainTextObjectModel();
					text.Text = tableText;

					Document d = new Document(text, new DataFormats.Text.Plain.PlainTextDataFormat(), null);
					d.IsSaved = false;
					d.IsChanged = true;
					d.Title = "New Query";
					((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).OpenFile(new Document[]
					{
						d
					});
					break;
				}
				case ScriptTableTo.File:
				{
					FileDialog dlg = new FileDialog();
					dlg.Mode = FileDialogMode.Save;
					dlg.FileNameFilters.Add("SQL script", "*.sql");
					dlg.Text = "Script Table to File";
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						System.IO.File.WriteAllText(dlg.SelectedFileName, tableText);
					}
					break;
				}
				case ScriptTableTo.Clipboard:
				{
					Clipboard.Default.SetText(tableText);
					break;
				}
				case ScriptTableTo.Job:
				{
					Clipboard.Default.SetText(tableText);
					break;
				}
			}
		}

		private void DatabaseEditor_ContextMenu_Table_Script(object sender, EventArgs ee)
		{
			CommandEventArgs cea = (ee as CommandEventArgs);
			string wpcmdid = cea.Command.ID.Substring("DatabaseEditor_ContextMenu_Table_Script_".Length);
			string wpcmd_type = wpcmdid.Substring(0, wpcmdid.IndexOf('_'));
			string wpcmd_dest = wpcmdid.Substring(wpcmdid.IndexOf('_') + 1);

			ScriptTableMode mode = ScriptTableMode.Create;
			ScriptTableTo dest = ScriptTableTo.Window;
			switch (wpcmd_type.ToLower())
			{
				case "alter": mode = ScriptTableMode.Alter; break;
				case "create": mode = ScriptTableMode.Create; break;
				case "delete": mode = ScriptTableMode.Delete; break;
				case "drop": mode = ScriptTableMode.Drop; break;
				case "dropcreate": mode = ScriptTableMode.DropCreate; break;
				case "execute": mode = ScriptTableMode.Execute; break;
				case "insert": mode = ScriptTableMode.Insert; break;
				case "select": mode = ScriptTableMode.Select; break;
				case "update": mode = ScriptTableMode.Update; break;
			}
			switch (wpcmd_dest.ToLower())
			{
				case "newwindow": dest = ScriptTableTo.Window; break;
				case "clipboard": dest = ScriptTableTo.Clipboard; break;
				case "file": dest = ScriptTableTo.File; break;
				case "job": dest = ScriptTableTo.Job; break;
			}
			DatabaseTable dt = Editor.DocumentExplorer.SelectedNode?.GetExtraData<DatabaseTable>("table");
			if (dt != null)
			{
				ScriptTable(dt, mode, dest);
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			txtQuery.ContextMenuCommandID = "DatabaseEditor_ContextMenu_Query";

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Create_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Create_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Create_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Create_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Alter_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Alter_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Alter_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Alter_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Drop_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Drop_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Drop_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Drop_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_DropCreate_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_DropCreate_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_DropCreate_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_DropCreate_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Select_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Select_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Select_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Select_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Insert_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Insert_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Insert_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Insert_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Update_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Update_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Update_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Update_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Delete_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Delete_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Delete_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Delete_Job", DatabaseEditor_ContextMenu_Table_Script);

			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Execute_NewWindow", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Execute_File", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Execute_Clipboard", DatabaseEditor_ContextMenu_Table_Script);
			Editor.Context.AttachCommandEventHandler("DatabaseEditor_ContextMenu_Table_Script_Execute_Job", DatabaseEditor_ContextMenu_Table_Script);

			OnObjectModelChanged(EventArgs.Empty);
		}

		private static string TypeToSQL(Type dataType)
		{
			if (dataType == typeof(string))
			{
				return "TEXT";
			}
			else if (dataType == typeof(short))
			{
				return "SMALLINT";
			}
			else if (dataType == typeof(int))
			{
				return "INT";
			}
			return "ANY";
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			DatabaseObjectModel db = (ObjectModel as DatabaseObjectModel);
			if (db == null)
				return;

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
					for (int c = 0; c < rec.Fields.Count; c++)
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
