using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public class Folder : IFileSystemObject
	{
		public class FolderCollection
			: System.Collections.ObjectModel.Collection<Folder>
		{
			private Folder mvarParent = null;
			public FolderCollection(Folder parent = null)
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

			public Folder this[string Name]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].Name.Equals(Name)) return this[i];
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
		}

		private FolderCollection _parentCollection = null;

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private Folder mvarParent = null;
		public Folder Parent { get { return mvarParent; } private set { mvarParent = value; } }

		private FolderCollection mvarFolders = null;
		public FolderCollection Folders { get { return mvarFolders; } }

		private File.FileCollection mvarFiles = new File.FileCollection();
		public File.FileCollection Files { get { return mvarFiles; } }

		public object Clone()
		{
			Folder clone = new Folder();
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
			clone.Name = mvarName;
			return clone;
		}
	}
}
