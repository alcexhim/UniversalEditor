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

namespace UniversalEditor.Editors.FileSystem
{
	public partial class FileSystemEditor : Editor
	{
		public FileSystemEditor()
		{
			this.InitializeComponent();
		}

		public override void Copy()
		{
			throw new NotImplementedException();
		}
		public override void Paste()
		{
			throw new NotImplementedException();
		}
		public override void Delete()
		{
			throw new NotImplementedException();
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
