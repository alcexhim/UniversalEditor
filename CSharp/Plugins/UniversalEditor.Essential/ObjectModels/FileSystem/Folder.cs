using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
    public class Folder
    {
        public class FolderCollection
            : NetFX.Collections.ObjectModel.Collection<Folder>
        {
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
                        if (this[i].Name.SafeEquals(Name)) return this[i];
                    }
                    return null;
                }
            }

			public bool Contains(string Name)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name.SafeEquals(Name)) return true;
                }
                return false;
			}

            public bool Remove(string Name)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name.SafeEquals(Name))
                    {
                        Remove(this[i]);
                        return true;
                    }
                }
                return false;
            }
		}

		private FolderCollection _parentCollection = null;

        private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set
			{
				mvarName = value;
			}
		}

        private FolderCollection mvarFolders = new FolderCollection();
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
