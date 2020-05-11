//
//  ManageBookmarksDialog.cs - provides a UWT ContainerLayout-based CustomDialog for managing bookmarks in Universal Editor
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
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Keyboard;

namespace UniversalEditor.UserInterface.Dialogs
{
	/// <summary>
	/// Provides a UWT ContainerLayout-based <see cref="CustomDialog" /> for managing bookmarks in Universal Editor.
	/// </summary>
	[ContainerLayout("~/Dialogs/ManageBookmarksDialog.glade")]
	public class ManageBookmarksDialog : CustomDialog
	{
		private ListView tv;
		private DefaultTreeModel tm;

		private System.Collections.Specialized.StringCollection _FileNames = new System.Collections.Specialized.StringCollection();

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			for (int i = 0; i < Engine.CurrentEngine.BookmarksManager.FileNames.Count; i++)
			{
				_FileNames.Add(Engine.CurrentEngine.BookmarksManager.FileNames[i]);

				string filepath = Engine.CurrentEngine.BookmarksManager.FileNames[i];
				string filetitle = System.IO.Path.GetFileName(filepath);

				tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], filetitle),
					new TreeModelRowColumn(tm.Columns[1], filepath)
				}));
				tm.Rows[tm.Rows.Count - 1].SetExtraData<int>("index", i);
			}

			DefaultButton = Buttons[0];
		}

		[EventHandler("cmdOK", "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			Engine.CurrentEngine.BookmarksManager.FileNames.Clear();
			for (int i = 0; i < _FileNames.Count; i++)
			{
				Engine.CurrentEngine.BookmarksManager.FileNames.Add(_FileNames[i]);
			}
			Close();
		}

		[EventHandler("tv", "KeyDown")]
		private void tv_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case KeyboardKey.Delete:
				{
					if (tv.SelectedRows.Count == 1)
					{
						int index = tv.Model.Rows.IndexOf(tv.SelectedRows[0]);
						string filetitle = System.IO.Path.GetFileName(_FileNames[index]);

						if (MessageDialog.ShowDialog(String.Format("Remove '{0}' from the list of bookmarks?", filetitle), "Remove Bookmark", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
							return;

						_FileNames.RemoveAt(index);
						tm.Rows.Remove(tv.SelectedRows[0]);
					}
					else if (tv.SelectedRows.Count > 1)
					{
						if (MessageDialog.ShowDialog(String.Format("Remove {0} items from the list of bookmarks?", tv.SelectedRows.Count), "Remove Bookmark", MessageDialogButtons.YesNo, MessageDialogIcon.Warning) == DialogResult.No)
							return;

						MessageDialog.ShowDialog("Not implemented", "error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					}
					break;
				}
			}
		}

		[EventHandler("tv", "RowActivated")]
		private void tv_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			int index = e.Row.GetExtraData<int>("index");

			Engine.CurrentEngine.LastWindow.OpenFile(Engine.CurrentEngine.BookmarksManager.FileNames[index]);
			Close();
		}

	}
}
