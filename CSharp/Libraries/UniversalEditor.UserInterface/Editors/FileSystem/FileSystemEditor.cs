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

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;

using UniversalWidgetToolkit;
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
		
		protected override void OnCreated(EventArgs e)
		{
			this.tv.RegisterDragSource(new DragDropTarget[]
			{
				new DragDropTarget("STRING", DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x0)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);

			this.tv.DragDropDataRequest += tv_DragDropDataRequest;
		}
		private void tv_DragDropDataRequest(object sender, DragDropDataRequestEventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				sb.AppendLine("file:///tmp/test/" + tv.SelectedRows[i].RowColumns[0].Value.ToString());
			}
			e.Data = sb.ToString();
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

				FileSystemSelection sel = new FileSystemSelection(row.GetExtraData<IFileSystemObject>("item"));
				Selections.Add(sel);
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
	}
}
