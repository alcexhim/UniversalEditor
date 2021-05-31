//
//  FileSystemObjectModel.cs - provides an ObjectModel for manipulating files or block devices which contain other files, such as archives, file systems, and disk images
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
using MBS.Framework.Logic.Conditional;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating files which contain other files, such as archives, file systems, and disk images.
	/// </summary>
	public class FileSystemObjectModel : ObjectModel, IFileSystemContainer
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = new ObjectModelReference(GetType(), new Guid("{A23026E9-DFE1-4090-AF35-8B916D3F1FCD}"));
				_omr.Path = new string[] { "General", "File system/archive" };
			}
			return _omr;
		}
		public override void Clear()
		{
			Files.Clear();
			Folders.Clear();
			ID = Guid.Empty;
			Title = String.Empty;
			mvarPathSeparators = new string[] { System.IO.Path.DirectorySeparatorChar.ToString(), System.IO.Path.AltDirectorySeparatorChar.ToString() };
		}
		public override void CopyTo(ObjectModel where)
		{
			FileSystemObjectModel clone = (where as FileSystemObjectModel);
			clone.ID = ID;
			for (int i = 0; i < Files.Count; i++)
			{
				File file = Files[i];
				clone.Files.Add(file.Clone() as File);
			}
			for (int i = 0; i < Folders.Count; i++)
			{
				Folder folder = Folders[i];
				clone.Folders.Add(folder.Clone() as Folder);
			}
		}

		[NonSerializedProperty]
		public FileSystemObjectModel FileSystem { get { return this; } }
		[NonSerializedProperty]
		public IFileSystemContainer Parent { get { return null; } }

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
				if (UniversalEditor.Common.Reflection.GetAvailableObjectModel<FileSystemObjectModel>(accessor, out FileSystemObjectModel fsom1))
				{
					fsom1.CopyTo(fsom);
				}
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

		public FileSystemObjectModel()
		{
			Files = new File.FileCollection(this);
			Folders = new Folder.FolderCollection(this);
		}

		public File.FileCollection Files { get; private set; } = null;
		public Folder.FolderCollection Folders { get; private set; } = null;

		/// <summary>
		/// The unique ID associated with this file system. Not supported by all <see cref="DataFormat" />s.
		/// </summary>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// The title associated with this file system.  Not supported by all <see cref="DataFormat" />s.
		/// </summary>
		public string Title { get; set; } = String.Empty;
		public string Name { get; set; } = String.Empty;

		private string[] mvarPathSeparators = new string[] { "/", "\\" }; // System.IO.Path.DirectorySeparatorChar.ToString(), System.IO.Path.AltDirectorySeparatorChar.ToString() };
		public string[] PathSeparators { get { return mvarPathSeparators; } set { mvarPathSeparators = value; } }

		public bool ContainsFile(string path)
		{
			return (FindFile(path) != null);
		}

		public File FindFile(string path, MBS.Framework.IO.CaseSensitiveHandling caseSensitiveHandling = MBS.Framework.IO.CaseSensitiveHandling.System)
		{
			string[] pathParts = path.Split(PathSeparators);
			if (pathParts.Length == 1)
			{
				File file = Files[pathParts[0], caseSensitiveHandling];
				if (file != null) return file;
			}
			else
			{
				Folder parentFolder = Folders[pathParts[0], caseSensitiveHandling];
				if (parentFolder == null) return null;

				for (int i = 1; i < pathParts.Length; i++)
				{
					if (i < pathParts.Length - 1)
					{
						parentFolder = parentFolder.Folders[pathParts[i], caseSensitiveHandling];
						if (parentFolder == null) return null;
					}
					else
					{
						return parentFolder.Files[pathParts[i], caseSensitiveHandling];
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
					parent = Folders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
			}

			if (parent == null)
			{
				return Folders[path[path.Length - 1]];
			}
			else
			{
				return parent.Folders[path[path.Length - 1]];
			}
		}

		public IFileSystemObject FindObject(string name)
		{
			string[] path = name.Split(new char[] { '/', '\\' });
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					parent = Folders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
			}

			if (parent == null)
			{
				File file = Files[path[path.Length - 1]];
				Folder folder = Folders[path[path.Length - 1]];
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
					parent = Folders[path[i]];
				}
				else
				{
					parent = parent.Folders[path[i]];
				}
				if (parent == null) throw new System.IO.DirectoryNotFoundException();
			}
			if (parent == null)
			{
				return Folders.Add(path[path.Length - 1]);
			}
			return parent.Folders.Add(path[path.Length - 1]);
		}

		/// <summary>
		/// Adds a <see cref="File" /> to this <see cref="FileSystemObjectModel" />, building the parent directory hierarchy as appropriate.
		/// </summary>
		/// <param name="name">The full path of the <see cref="File" /> to create, including any parent directories.</param>
		/// <param name="fileData"></param>
		/// <returns></returns>
		public File AddFile(string name, byte[] fileData = null)
		{
			if (name == null) name = String.Empty;
			string[] path = name.Split(mvarPathSeparators, StringSplitOptions.None);
			Folder parent = null;
			for (int i = 0; i < path.Length - 1; i++)
			{
				if (parent == null)
				{
					if (Folders.Contains(path[i]))
					{
						parent = Folders[path[i]];
					}
					else
					{
						parent = Folders.Add(path[i]);
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
			if (fileData != null)
			{
				file.SetData(fileData);
			}
			if (parent == null)
			{
				Files.Add(file);
			}
			else
			{
				parent.Files.Add(file);
			}
			return file;
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
			for (int i = 0; i < Files.Count; i++)
			{
				File file = Files[i];
				if (searchPattern != null && !file.Name.Match(searchPattern)) continue;

				files.Add(file);
			}
			for (int i = 0; i < Folders.Count; i++)
			{
				Folder folder = Folders[i];
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
			for (int i = 0; i < Files.Count; i++)
			{
				File file = Files[i];
				files.Add(file);
			}
			for (int i = 0; i < Folders.Count; i++)
			{
				Folder folder = Folders[i];
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

		public IFileSystemObject[] GetAllObjects(string pathSeparator = null, System.IO.SearchOption option = System.IO.SearchOption.AllDirectories, IFileSystemObjectType objectTypes = IFileSystemObjectType.All)
		{
			if (pathSeparator == null) pathSeparator = "/";

			List<IFileSystemObject> files = new List<IFileSystemObject>();
			if ((objectTypes & IFileSystemObjectType.File) == IFileSystemObjectType.File)
			{
				for (int i = 0; i < Files.Count; i++)
				{
					File file = Files[i];
					files.Add(file);
				}
			}
			for (int i = 0; i < Folders.Count; i++)
			{
				Folder folder = Folders[i];
				if ((objectTypes & IFileSystemObjectType.Folder) == IFileSystemObjectType.Folder)
				{
					files.Add(folder);
				}
				if (option == System.IO.SearchOption.AllDirectories) GetAllObjectsRecursively(folder, ref files, folder.Name, pathSeparator, null, objectTypes);
			}
			return files.ToArray();
		}

		private void GetAllObjectsRecursively(Folder folder, ref List<IFileSystemObject> files, string parentPath, string pathSeparator, string searchPattern = null, IFileSystemObjectType objectTypes = IFileSystemObjectType.All)
		{
			if ((objectTypes & IFileSystemObjectType.File) == IFileSystemObjectType.File)
			{
				for (int i = 0; i < folder.Files.Count; i++)
				{
					File file = folder.Files[i];
					if (searchPattern != null && !file.Name.Match(searchPattern)) continue;

					File file2 = (file.Clone() as File);
					file2.Name = parentPath + pathSeparator + file.Name;
					files.Add(file2);
				}
			}
			for (int i = 0; i < folder.Folders.Count; i++)
			{
				Folder folder1 = folder.Folders[i];
				if ((objectTypes & IFileSystemObjectType.Folder) == IFileSystemObjectType.Folder)
				{
					files.Add(folder1);
				}
				GetAllObjectsRecursively(folder1, ref files, parentPath + pathSeparator + folder1.Name, pathSeparator, searchPattern);
			}
		}

		/// <summary>
		/// Gets the next available "New Folder" name for the given <see cref="IFileSystemContainer" />.
		/// </summary>
		/// <returns>A string "New Folder" if there are no other "New Folder"s in the given <see cref="IFileSystemContainer" />; otherwise, a string "New Folder (n)" where N is the number of "New Folder"s in the given <see cref="IFileSystemContainer" /> plus one.</returns>
		public static string GetNewFolderName(IFileSystemContainer container)
		{
			int count = 0;

			foreach (Folder f in container.Folders)
			{
				if (f.Name.StartsWith("New Folder (") && f.Name.EndsWith(")"))
				{
					string strIntPart = f.Name.Substring("New Folder (".Length, f.Name.Length - "New Folder (".Length - 1);
					int intPart = 0;
					if (Int32.TryParse(strIntPart, out intPart))
					{
						if (intPart > count) count = intPart;
					}
				}
				else if (f.Name == "New Folder")
				{
					count++;
				}
			}

			if (count == 0) return "New Folder";
			return "New Folder (" + (count + 1).ToString() + ")";
		}

		/// <summary>
		/// Gets the next available "New Folder" name for this <see cref="FileSystemObjectModel" />.
		/// </summary>
		/// <returns>A string "New Folder" if there are no other "New Folder"s in this <see cref="FileSystemObjectModel" />; otherwise, a string "New Folder (n)" where N is the number of "New Folder"s in this <see cref="FileSystemObjectModel" /> plus one.</returns>
		public string GetNewFolderName()
		{
			return FileSystemObjectModel.GetNewFolderName(this);
		}

		public FileAdditionalDetail.FileAdditionalDetailCollection AdditionalDetails { get; } = new FileAdditionalDetail.FileAdditionalDetailCollection();

		protected override CriteriaResult[] FindInternal(CriteriaQuery query)
		{
			List<CriteriaResult> list = new List<CriteriaResult>();
			File[] files = GetAllFiles();
			for (int i = 0; i < files.Length; i++)
			{
				if (MatchesCriteria(files[i], query))
				{
					list.Add(new CriteriaResult(files[i]));
				}
			}
			return list.ToArray();
		}

		private bool MatchesCriteria(File file, CriteriaQuery query)
		{
			bool ret = false;
			for (int i = 0; i < query.Criteria.Count; i++)
			{
				if (query.Criteria[i].Property == CriteriaProperty_File_Name)
				{
					switch (query.Criteria[i].Comparison)
					{
						case ConditionComparison.Equal:
						{
							ret |= (query.Criteria[i].Value?.ToString() == file.Name);
							break;
						}
						case ConditionComparison.StartsWith:
						{
							ret |= file.Name.StartsWith(query.Criteria[i].Value?.ToString());
							break;
						}
						case ConditionComparison.EndsWith:
						{
							ret |= file.Name.EndsWith(query.Criteria[i].Value?.ToString());
							break;
						}
						case ConditionComparison.Contains:
						{
							ret |= file.Name.Contains(query.Criteria[i].Value?.ToString());
							break;
						}
					}
				}
			}
			return ret;
		}

		private CriteriaProperty CriteriaProperty_File_Name = new CriteriaProperty("Name", typeof(string));

		private CriteriaObject[] _CriteriaObjects = null;
		protected override CriteriaObject[] GetCriteriaObjectsInternal()
		{
			if (_CriteriaObjects == null)
			{
				_CriteriaObjects = new CriteriaObject[]
				{
					new CriteriaObject("File", new CriteriaProperty[]
					{
						CriteriaProperty_File_Name,
						new CriteriaProperty("Type", typeof(string)),
						new CriteriaProperty("Size", typeof(long)),
						new CriteriaProperty("Date modified", typeof(string))
					})
				};
			}
			return _CriteriaObjects;
		}

		public static ObjectModel FromMarkup(MarkupTagElement tagContent)
		{
			FileSystemObjectModel fsom = new FileSystemObjectModel();
			InitFileSystemFromMarkup(fsom, tagContent);
			return fsom;
		}

		private static void InitFileSystemFromMarkup(IFileSystemContainer fsom, MarkupTagElement tagContent)
		{
			MarkupTagElement tagFolders = tagContent.Elements["Folders"] as MarkupTagElement;
			MarkupTagElement tagFiles = tagContent.Elements["Files"] as MarkupTagElement;
			if (tagFolders != null)
			{
				for (int i = 0; i < tagFolders.Elements.Count; i++)
				{
					MarkupTagElement tagFolder = (tagFolders.Elements[i] as MarkupTagElement);
					if (tagFolder == null) continue;
					if (tagFolder.FullName != "Folder") continue;

					MarkupAttribute attName = tagFolder.Attributes["Name"];

					Folder item = new Folder();
					if (attName != null)
					{
						item.Name = attName.Value;
					}
					InitFileSystemFromMarkup(item, tagFolder);
					fsom.Folders.Add(item);
				}
			}
			if (tagFiles != null)
			{
				for (int i = 0; i < tagFiles.Elements.Count; i++)
				{
					MarkupTagElement tagFile = (tagFiles.Elements[i] as MarkupTagElement);
					if (tagFile == null) continue;
					if (tagFile.FullName != "File") continue;

					MarkupAttribute attName = tagFile.Attributes["Name"];

					File item = new File();
					if (attName != null)
					{
						item.Name = attName.Value;
					}
					fsom.Files.Add(item);
				}
			}
		}

		public void Delete(IFileSystemObject fso)
		{
			if (fso is File)
			{
				fso.Parent.Files.Remove(fso as File);
			}
			else if (fso is Folder)
			{
				fso.Parent.Folders.Remove(fso as Folder);
			}
		}
	}
}
