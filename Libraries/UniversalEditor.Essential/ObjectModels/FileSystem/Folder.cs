//
//  Folder.cs - represents an IFileSystemObject which contains other IFileSystemObjects such as Files and Folders
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Represents an <see cref="IFileSystemObject" /> which contains other <see cref="IFileSystemObject" />s such as <see cref="File" />s and <see cref="Folder" />s.
	/// </summary>
	public class Folder : IFileSystemObject, IFileSystemContainer
	{
		public class FolderCollection
			: System.Collections.ObjectModel.Collection<Folder>
		{
			private IFileSystemContainer mvarParent = null;
			public FolderCollection(IFileSystemContainer parent)
			{
				mvarParent = parent;
			}

			public Folder Add(string Name)
			{
				Folder folder = new Folder();
				folder.Name = Name;
				Add(folder);
				return folder;
			}

			public Folder this[string Name, MBS.Framework.IO.CaseSensitiveHandling caseSensitiveHandling = MBS.Framework.IO.CaseSensitiveHandling.System]
			{
				get
				{
					bool caseSensitive = (caseSensitiveHandling == MBS.Framework.IO.CaseSensitiveHandling.CaseSensitive || (caseSensitiveHandling == MBS.Framework.IO.CaseSensitiveHandling.System && System.Environment.OSVersion.Platform == PlatformID.Unix));
					for (int i = 0; i < Count; i++)
					{
						if ((caseSensitive && this[i].Name.Equals(Name)) || (!caseSensitive && this[i].Name.ToUpper() == Name.ToUpper()))
							return this[i];
					}
					return null;
				}
			}

			public bool Contains(string Name)
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i].Name.Equals(Name)) return true;
				}
				return false;
			}

			public bool Remove(string Name)
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i].Name.Equals(Name))
					{
						Remove(this[i]);
						return true;
					}
				}
				return false;
			}

			protected override void InsertItem(int index, Folder item)
			{
				base.InsertItem(index, item);
				item.Parent = mvarParent;
				item.FileSystem = mvarParent?.FileSystem;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, Folder item)
			{
				item.Parent = mvarParent;
				base.SetItem(index, item);
			}
		}

		public Folder()
		{
			mvarFolders = new FolderCollection(this);
			mvarFiles = new File.FileCollection(this);
		}

		public FileSystemObjectModel FileSystem { get; private set; } = null;

		private FolderCollection _parentCollection = null;

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private IFileSystemContainer mvarParent = null;
		public IFileSystemContainer Parent { get { return mvarParent; } private set { mvarParent = value; } }

		private FolderCollection mvarFolders = null;
		public FolderCollection Folders { get { return mvarFolders; } }

		private File.FileCollection mvarFiles = null;
		public File.FileCollection Files { get { return mvarFiles; } }

		/// <summary>
		/// Gets the next available "New Folder" name for this folder.
		/// </summary>
		/// <returns>A string "New Folder" if there are no other "New Folder"s in this folder; otherwise, a string "New Folder (n)" where N is the number of "New Folder"s in this folder plus one.</returns>
		public string GetNewFolderName()
		{
			return FileSystemObjectModel.GetNewFolderName(this);
		}

		public object Clone()
		{
			Folder clone = new Folder();
			clone.Name = (Name.Clone() as string);
			for (int i = 0; i < mvarFiles.Count; i++)
			{
				File file = mvarFiles[i];
				clone.Files.Add(file.Clone() as File);
			}
			for (int i = 0; i < mvarFolders.Count; i++)
			{
				Folder folder = mvarFolders[i];
				clone.Folders.Add(folder.Clone() as Folder);
			}
			return clone;
		}

		/// <summary>
		/// Recursively gets the size of this <see cref="Folder" /> and all the contained files.
		/// </summary>
		/// <returns></returns>
		public long GetSize()
		{
			long size = 0;
			foreach (File file in mvarFiles)
			{
				size += file.Size;
			}
			foreach (Folder folder in mvarFolders)
			{
				size += folder.GetSize();
			}
			return size;
		}

		public File AddFile(string fileName, byte[] fileData)
		{
			string[] path = fileName.Split(new string[] { System.IO.Path.DirectorySeparatorChar.ToString(), System.IO.Path.AltDirectorySeparatorChar.ToString() }, StringSplitOptions.None);
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					if (mvarFolders.Contains(path[i]))
					{
						parent = mvarFolders[path[i]];
					}
					else
					{
						parent = mvarFolders.Add(path[i]);
					}
				}
				else
				{
					if (parent.Folders.Contains(path[i]))
					{
						parent = parent.Folders[path[i]];
					}
					else
					{
						parent = parent.Folders.Add(path[i]);
					}
				}

				if (parent == null)
				{
					throw new System.IO.DirectoryNotFoundException();
				}
			}

			File file = new File();
			file.Name = path[path.Length - 1];
			if (fileData != null) file.SetData(fileData);
			if (parent == null)
			{
				mvarFiles.Add(file);
			}
			else
			{
				parent.Files.Add(file);
			}
			return file;
		}

		public IFileSystemObject[] GetContents()
		{
			List<IFileSystemObject> fsos = new List<IFileSystemObject>();
			foreach (Folder folder in Folders)
			{
				fsos.Add(folder);
			}
			foreach (File file in Files)
			{
				fsos.Add(file);
			}
			return fsos.ToArray();
		}
	}
}
