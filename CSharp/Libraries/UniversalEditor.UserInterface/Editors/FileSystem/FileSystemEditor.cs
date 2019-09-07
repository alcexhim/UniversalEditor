//
//  FileSystemEditor.cs - cross-platform (UWT) file system editor for Universal Editor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.DragDrop;
using UniversalWidgetToolkit.Input.Keyboard;
using UniversalWidgetToolkit.Input.Mouse;

namespace UniversalEditor.Editors.FileSystem
{
	public partial class FileSystemEditor : Editor
	{
		public FileSystemEditor()
		{
			this.InitializeComponent();
		}

		internal void ClearSelectionContent(FileSystemSelection sel)
		{
			while (tv.SelectedRows.Count > 0)
			{
				if (tv.SelectedRows[0].GetExtraData<IFileSystemObject>("item") == sel.Item)
				{
					tmTreeView.Rows.Remove(tv.SelectedRows[0]);
					break;
				}
			}
		}


		protected override void OnCreated(EventArgs e)
		{
			// FIXME: this is GTK-specific...
			this.tv.RegisterDragSource(new DragDropTarget[]
			{
				new DragDropTarget("text/uri-list", DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x0)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);

			this.tv.DragDropDataRequest += tv_DragDropDataRequest;
		}

		private void tv_DragDropDataRequest(object sender, DragDropDataRequestEventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			List<string> list = new List<string>();
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				IFileSystemObject fso = tv.SelectedRows[i].GetExtraData<IFileSystemObject>("item");
				if (fso is File)
				{
					string wTmpFile = TemporaryFileManager.CreateTemporaryFile(tv.SelectedRows[i].RowColumns[0].Value.ToString(), (fso as File).GetData());
					list.Add(String.Format("file://{0}", wTmpFile));
				}
			}
			e.Data = list.ToArray();
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			Selections.Clear();
			for (int i = 0;  i < tv.SelectedRows.Count; i++)
			{
				TreeModelRow row = tv.SelectedRows[i];
				if (row == null) continue;

				Selections.Add(new FileSystemSelection(this, row.GetExtraData<IFileSystemObject>("item")));
			}
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(FileSystemObjectModel));
			}
			return _er;
		}

		private void RecursiveAddFolder(Folder f, TreeModelRow parent = null)
		{
			TreeModelRow r = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmTreeView.Columns[0], f.Name),
				new TreeModelRowColumn(tmTreeView.Columns[1], (f.Folders.Count + f.Files.Count).ToString() + " items"),
				new TreeModelRowColumn(tmTreeView.Columns[2], "Folder"),
				new TreeModelRowColumn(tmTreeView.Columns[3], "")
			});
			r.SetExtraData<IFileSystemObject>("item", f);

			foreach (Folder f2 in f.Folders)
			{
				RecursiveAddFolder(f2, r);
			}
			foreach (File f2 in f.Files)
			{
				RecursiveAddFile(f2, r);
			}

			if (parent == null)
			{
				tmTreeView.Rows.Add(r);
			}
			else
			{
				parent.Rows.Add(r);
			}
		}
		private void RecursiveAddFile(File f, TreeModelRow parent = null)
		{
			TreeModelRow r = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmTreeView.Columns[0], f.Name),
				new TreeModelRowColumn(tmTreeView.Columns[1], UniversalEditor.UserInterface.Common.FileInfo.FormatSize(f.Size)),
				new TreeModelRowColumn(tmTreeView.Columns[2], ""),
				new TreeModelRowColumn(tmTreeView.Columns[3], "")
			});
			r.SetExtraData<IFileSystemObject>("item", f);

			if (parent == null)
			{
				tmTreeView.Rows.Add(r);
			}
			else
			{
				parent.Rows.Add(r);
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			foreach (Folder f in fsom.Folders)
			{
				RecursiveAddFolder(f, null);
			}
			foreach (File f in fsom.Files)
			{
				RecursiveAddFile(f, null);
			}
		}

		void ContextMenuCopyTo_Click(object sender, EventArgs e)
		{
			// extract files
			if (tv.SelectedRows.Count == 1)
			{
				UIExtractFileSystemObject(tv.SelectedRows[0].GetExtraData<IFileSystemObject>("item"));
			}
			else if (tv.SelectedRows.Count > 1)
			{
				FileDialog fd = new FileDialog();
				fd.Mode = FileDialogMode.SelectFolder;
				fd.MultiSelect = false;
				foreach (TreeModelRow row in tv.SelectedRows)
				{
				}
			}
		}

		private void UIExtractFileSystemObject(IFileSystemObject fso)
		{
			FileDialog fd = new FileDialog();
			if (fso is File)
			{
				File f = (fso as File);
				/*
				if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + f.Name))
				{
					fd.SelectedFileNames.Add(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + f.Name);
				}
				else
				{
				*/
				fd.SelectedFileNames.Add(f.Name);
				//}
				fd.Mode = FileDialogMode.Save;
				fd.MultiSelect = false;
				if (fd.ShowDialog() == DialogResult.OK)
				{
					System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1], f.GetData());
				}
			}
			else if (fso is Folder)
			{
				Folder f = (fso as Folder);
				fd.SelectedFileNames.Add(f.Name);
				fd.Mode = FileDialogMode.CreateFolder;
				fd.MultiSelect = false;
				if (fd.ShowDialog() == DialogResult.OK)
				{
					System.IO.Directory.CreateDirectory(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1]);
					// TODO: implement this
				}
			}
		}

		void tv_BeforeContextMenu(object sender, EventArgs e)
		{
			TreeModelRow row = null;
			if (e is MouseEventArgs)
			{
				MouseEventArgs ee = (e as MouseEventArgs);
				ListViewHitTestInfo info = tv.HitTest(ee.X, ee.Y);
				if (info != null)
					row = info.Row;
			}

			if (row != null)
			{
				tv.ContextMenu = contextMenuSelected;
			}
			else
			{
				tv.ContextMenu = contextMenuUnselected;
			}
		}
	}
}
