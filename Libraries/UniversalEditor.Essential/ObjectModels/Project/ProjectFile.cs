//
//  ProjectFile.cs - represents a reference to a file in a ProjectObjectModel
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

using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Project
{
	/// <summary>
	/// Represents a reference to a file in a <see cref="ProjectObjectModel" />.
	/// </summary>
	public class ProjectFile : IProjectFileContainer, ICloneable
	{
		public class ProjectFileCollection
			: System.Collections.ObjectModel.Collection<ProjectFile>
		{
			private IProjectFileContainer _parent = null;
			public ProjectFileCollection(IProjectFileContainer parent = null)
			{
				_parent = parent;
			}

			public ProjectFile Add(string DestinationFileName)
			{
				return Add(String.Empty, DestinationFileName);
			}
			public ProjectFile Add(string SourceFileName, string DestinationFileName)
			{
				ProjectFile file = new ProjectFile();
				file.SourceFileName = SourceFileName;
				file.DestinationFileName = DestinationFileName;
				Add(file);
				return file;
			}

			private Dictionary<string, ProjectFile> _itemsByName = new Dictionary<string, ProjectFile>();
			public ProjectFile this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}

			protected override void InsertItem(int index, ProjectFile item)
			{
				base.InsertItem(index, item);
				if (_parent != null)
					item.Parent = _parent;
				_itemsByName[item.DestinationFileName] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (_itemsByName.ContainsKey(this[index].DestinationFileName))
					_itemsByName.Remove(this[index].DestinationFileName);

				if (_parent != null)
					this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				if (_parent != null)
				{
					foreach (ProjectFile file in this)
					{
						file.Parent = null;
					}
				}
				base.ClearItems();
			}
			protected override void SetItem(int index, ProjectFile item)
			{
				if (_itemsByName.ContainsKey(this[index].DestinationFileName))
					_itemsByName.Remove(this[index].DestinationFileName);

				base.SetItem(index, item);

				_itemsByName[item.DestinationFileName] = item;
			}
		}

		private string mvarSourceFileName = String.Empty;
		/// <summary>
		/// The file name of the project file on disk.
		/// </summary>
		public string SourceFileName { get { return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		/// <summary>
		/// The name of the project file in the project itself, which can be different from the file name of the file on disk.
		/// </summary>
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		private PropertyListObjectModel mvarConfiguration = new PropertyListObjectModel();
		/// <summary>
		/// The per-file configuration for this project file.
		/// </summary>
		public PropertyListObjectModel Configuration { get { return mvarConfiguration; } set { mvarConfiguration = value; } }

		private IProjectFileContainer mvarParent = null;
		public IProjectFileContainer Parent { get { return mvarParent; } private set { mvarParent = value; } }

		public object Clone()
		{
			ProjectFile clone = new ProjectFile();
			clone.DestinationFileName = (mvarDestinationFileName.Clone() as string);
			clone.SourceFileName = (mvarSourceFileName.Clone() as string);
			clone.Configuration = (mvarConfiguration.Clone() as PropertyListObjectModel);
			return clone;
		}

		private byte[] mvarContent = new byte[0];
		public byte[] Content { get { return mvarContent; } set { mvarContent = value; } }

		public ProjectFileCollection Dependents { get; } = new ProjectFileCollection();
		public ProjectFileCollection Files { get; } = new ProjectFileCollection();
	}
}
