using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.Accessors;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public class FileSystemObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = new ObjectModelReference(GetType(), new Guid("{A23026E9-DFE1-4090-AF35-8B916D3F1FCD}"));
				_omr.Title = "File system/archive";
				_omr.Path = new string[] { "General", "File system/archive" };
			}
			return _omr;
		}
		public override void Clear()
		{
			mvarFiles.Clear();
			mvarFolders.Clear();
			mvarID = Guid.Empty;
			mvarTitle = String.Empty;
			mvarPathSeparators = new string[] { System.IO.Path.DirectorySeparatorChar.ToString(), System.IO.Path.AltDirectorySeparatorChar.ToString() };
		}
		public override void CopyTo(ObjectModel where)
		{
			FileSystemObjectModel clone = (where as FileSystemObjectModel);
			clone.ID = mvarID;
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
		}

		public static FileSystemObjectModel FromFiles(string[] fileNames)
		{
			// TODO: This doesn't work because GetAvailableObjectModel returns an
			// ObjectModel but automatically closes the file after reading... deferred
			// FileSystemObjectModels associated files need to remain open in order to
			// read the file data. Consider using a Document-based approach which provides
			// more control over closing files as needed?

			FileSystemObjectModel fsom = new FileSystemObjectModel();

			foreach (string fileName in fileNames)
			{
				FileAccessor accessor = new FileAccessor(fileName);
				FileSystemObjectModel fsom1 = UniversalEditor.Common.Reflection.GetAvailableObjectModel<FileSystemObjectModel>(accessor);
				if (fsom1 == null) continue;

				fsom1.CopyTo(fsom);
			}
			return fsom;
		}
		public static FileSystemObjectModel FromDirectory(string path, string searchPattern = "*.*", System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly)
		{
			string[] folders = System.IO.Directory.GetDirectories(path);
			string[] files = System.IO.Directory.GetFiles(path);

			FileSystemObjectModel fsom = new FileSystemObjectModel();
			foreach (string folder in folders)
			{
				string title = System.IO.Path.GetFileName(folder);
				fsom.Folders.Add(title);
			}
			foreach (string fileName in files)
			{
				string title = System.IO.Path.GetFileName(fileName);
				fsom.Files.Add(title, fileName);
			}
			return fsom;

			// string[] files = System.IO.Directory.GetFiles(path, searchPattern, searchOption);
			// return FromFiles(files);
		}

		private File.FileCollection mvarFiles = new File.FileCollection();
		public File.FileCollection Files { get { return mvarFiles; } }
		private Folder.FolderCollection mvarFolders = new Folder.FolderCollection();
		public Folder.FolderCollection Folders { get { return mvarFolders; } }

		private Guid mvarID = Guid.Empty;
		/// <summary>
		/// The unique ID associated with this file system. Not supported by all data formats.
		/// </summary>
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title associated with this file system.  Not supported by all data formats.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string[] mvarPathSeparators = new string[] { System.IO.Path.DirectorySeparatorChar.ToString(), System.IO.Path.AltDirectorySeparatorChar.ToString() };
		public string[] PathSeparators { get { return mvarPathSeparators; } set { mvarPathSeparators = value; } }

		public bool ContainsFile(string path)
		{
			return (FindFile(path) != null);
		}

		public File FindFile(string path)
		{
			string[] pathParts = path.Split(new char[] { System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar });
			if (pathParts.Length == 1)
			{
				File file = mvarFiles[pathParts[0]];
				if (file != null) return file;
			}
			else
			{
				Folder parentFolder = mvarFolders[pathParts[0]];
				if (parentFolder == null) return null;

				for (int i = 1; i < pathParts.Length; i++)
				{
					if (i < pathParts.Length - 1)
					{
						parentFolder = parentFolder.Folders[pathParts[i]];
						if (parentFolder == null) return null;
					}
					else
					{
						return parentFolder.Files[pathParts[i]];
					}
				}
			}
			return null;
		}

		public Folder FindFolder(string name)
		{
			string[] path = name.Split(new char[] { '/' });
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					parent = mvarFolders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
			}

			if (parent == null)
			{
				return mvarFolders[path[path.Length - 1]];
			}
			else
			{
				return parent.Folders[path[path.Length - 1]];
			}
		}

		public object FindObject(string name)
		{
			string[] path = name.Split(new char[] { '/' });
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					parent = mvarFolders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
			}

			if (parent == null)
			{
				File file = mvarFiles[path[path.Length - 1]];
				Folder folder = mvarFolders[path[path.Length - 1]];
				if (folder == null) return file;
				return folder;
			}
			else
			{
				File file = parent.Files[path[path.Length - 1]];
				Folder folder = parent.Folders[path[path.Length - 1]];
				if (folder == null) return file;
				return folder;
			}
		}

		public Folder AddFolder(string name)
		{
			string[] path = name.Split(mvarPathSeparators, StringSplitOptions.None);
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					parent = mvarFolders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
				if (parent == null) throw new System.IO.DirectoryNotFoundException();
			}
			if (parent == null)
			{
				return mvarFolders.Add(path[path.Length - 1]);
			}
			return parent.Folders.Add(path[path.Length - 1]);
		}

		public File AddFile(string name)
		{
			string[] path = name.Split(mvarPathSeparators, StringSplitOptions.None);
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

			if (parent == null)
			{
				File file = new File();
				file.Name = path[path.Length - 1];
				mvarFiles.Add(file);
				return file;
			}
			else
			{
				File file = new File();
				file.Name = path[path.Length - 1];
				parent.Files.Add(file);
				return file;
			}
		}

		/// <summary>
		/// Gets all files in all folders of the <see cref="FileSystemObjectModel" /> with file names that
		/// match the <see cref="searchPattern"/>, and assigns the file names separated by the
		/// <see cref="pathSeparator"/>.
		/// </summary>
		/// <param name="searchPattern">The string by which to filter the retrieved file names.</param>
		/// <param name="pathSeparator">The string by which to separate directory and file names.</param>
		/// <returns></returns>
		public File[] GetFiles(string searchPattern = null, string pathSeparator = null)
		{
			if (pathSeparator == null) pathSeparator = "/";

			List<File> files = new List<File>();
			for (int i = 0; i < mvarFiles.Count; i++)
			{
				File file = mvarFiles[i];
				if (searchPattern != null && !file.Name.Match(searchPattern)) continue;

				files.Add(file);
			}
			for (int i = 0; i < mvarFolders.Count; i++)
			{
				Folder folder = mvarFolders[i];
				GetAllFilesRecursively(folder, ref files, folder.Name, pathSeparator, searchPattern);
			}
			return files.ToArray();
		}

		/// <summary>
		/// Gets all files in all folders of the <see cref="FileSystemObjectModel" />, and assigns the file names
		/// separated by the default path separator.
		/// </summary>
		/// <returns></returns>
		public File[] GetAllFiles(string pathSeparator = null)
		{
			if (pathSeparator == null) pathSeparator = "/";

			List<File> files = new List<File>();
			for (int i = 0; i < mvarFiles.Count; i++)
			{
				File file = mvarFiles[i];
				files.Add(file);
			}
			for (int i = 0; i < mvarFolders.Count; i++ )
			{
				Folder folder = mvarFolders[i];
				GetAllFilesRecursively(folder, ref files, folder.Name, pathSeparator);
			}
			return files.ToArray();
		}

		private void GetAllFilesRecursively(Folder folder, ref List<File> files, string parentPath, string pathSeparator, string searchPattern = null)
		{
			for (int i = 0; i < folder.Files.Count; i++)
			{
				File file = folder.Files[i];
				if (searchPattern != null && !file.Name.Match(searchPattern)) continue;

				File file2 = (file.Clone() as File);
				file2.Name = parentPath + pathSeparator + file.Name;
				files.Add(file2);
			}
			for (int i = 0; i < folder.Folders.Count; i++)
			{
				Folder folder1 = folder.Folders[i];
				GetAllFilesRecursively(folder1, ref files, parentPath + pathSeparator + folder1.Name, pathSeparator, searchPattern);
			}
		}
	}
}
