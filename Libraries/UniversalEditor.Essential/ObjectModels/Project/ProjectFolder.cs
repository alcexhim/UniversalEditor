//
//  ProjectFolder.cs - represents a folder which can contain ProjectFiles and other ProjectFolders in a ProjectObjectModel
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

namespace UniversalEditor.ObjectModels.Project
{
	/// <summary>
	/// Represents a folder which can contain <see cref="ProjectFile" />s and other <see cref="ProjectFolder" />s in a <see cref="ProjectObjectModel" />.
	/// </summary>
	public class ProjectFolder : IProjectItemContainer, ICloneable
	{
		public class ProjectFolderCollection
			: System.Collections.ObjectModel.Collection<ProjectFolder>
		{
			private IProjectItemContainer _parent = null;
			public ProjectFolderCollection(IProjectItemContainer parent = null)
			{
				_parent = parent;
			}

			public ProjectFolder Add(string Name)
			{
				ProjectFolder folder = new ProjectFolder();
				folder.Name = Name;
				Add(folder);
				return folder;
			}

			private Dictionary<string, ProjectFolder> _itemsByName = new Dictionary<string, ProjectFolder>();
			public ProjectFolder this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}

			protected override void InsertItem(int index, ProjectFolder item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
				_itemsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (_itemsByName.ContainsKey(this[index].Name))
					_itemsByName.Remove(this[index].Name);

				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				foreach (ProjectFolder folder in this)
				{
					folder.Parent = null;
				}
				base.ClearItems();
			}
			protected override void SetItem(int index, ProjectFolder item)
			{
				if (_itemsByName.ContainsKey(this[index].Name))
					_itemsByName.Remove(this[index].Name);

				base.SetItem(index, item);

				_itemsByName[item.Name] = item;
			}
		}

		public ProjectFolder()
		{
			mvarFolders = new ProjectFolder.ProjectFolderCollection(this);
			mvarFiles = new ProjectFile.ProjectFileCollection(this);
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private IProjectItemContainer mvarParent = null;
		public IProjectItemContainer Parent { get { return mvarParent; } private set { mvarParent = value; } }

		private ProjectFolder.ProjectFolderCollection mvarFolders = null;
		public ProjectFolder.ProjectFolderCollection Folders { get { return mvarFolders; } }

		private ProjectFile.ProjectFileCollection mvarFiles = null;
		public ProjectFile.ProjectFileCollection Files { get { return mvarFiles; } }

		public object Clone()
		{
			ProjectFolder clone = new ProjectFolder();
			foreach (ProjectFile file in mvarFiles)
			{
				clone.Files.Add(file.Clone() as ProjectFile);
			}
			foreach (ProjectFolder folder in mvarFolders)
			{
				clone.Folders.Add(folder.Clone() as ProjectFolder);
			}
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}
	}
}
