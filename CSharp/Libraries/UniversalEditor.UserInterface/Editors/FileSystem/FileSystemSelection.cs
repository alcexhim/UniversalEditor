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
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.FileSystem
{
	internal class FileSystemSelection : EditorSelection
	{
		public IFileSystemObject Item { get; set; } = null;
		public override object Content
		{
			get => Item;
			set
			{
				if (value == null)
					_parent.ClearSelectionContent(this);

				Item = (value is IFileSystemObject ? (value as IFileSystemObject) : null);
			}
		}

		private FileSystemEditor _parent = null;
		internal FileSystemSelection(FileSystemEditor parent, IFileSystemObject item)
		{
			_parent = parent;
			Item = item;
		}
	}
}