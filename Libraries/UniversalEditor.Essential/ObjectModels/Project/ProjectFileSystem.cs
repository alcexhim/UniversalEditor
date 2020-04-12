//
//  ProjectFileSystem.cs - represents a file system containing ProjectFolders and ProjectFiles
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

namespace UniversalEditor.ObjectModels.Project
{
	/// <summary>
	/// Represents a file system containing <see cref="ProjectFolder" />s and <see cref="ProjectFile" />s.
	/// </summary>
	public class ProjectFileSystem : IProjectItemContainer
	{
		/// <summary>
		/// The collection of folders in this project.
		/// </summary>
		public ProjectFolder.ProjectFolderCollection Folders { get; private set; } = null;

		/// <summary>
		/// The collection of files in this project.
		/// </summary>
		public ProjectFile.ProjectFileCollection Files { get; private set; } = null;

		public ProjectFileSystem()
		{
			Files = new ProjectFile.ProjectFileCollection(this);
			Folders = new ProjectFolder.ProjectFolderCollection(this);
		}

		public void Clear()
		{
			Folders.Clear();
			Files.Clear();
		}
		public void CopyTo(ProjectFileSystem clone)
		{
			foreach (ProjectFile file in Files)
			{
				clone.Files.Add(file.Clone() as ProjectFile);
			}
			foreach (ProjectFolder folder in Folders)
			{
				clone.Folders.Add(folder.Clone() as ProjectFolder);
			}
		}

		public void AddFile(string sourceFileName, string destinationFileName, char pathSeparator)
		{
			string[] paths = destinationFileName.Split(pathSeparator);
			ProjectFolder parentFolder = null;
			for (int i = 0; i < paths.Length - 1; i++)
			{
				if (parentFolder == null)
				{
					if (Folders[paths[i]] != null)
					{
						parentFolder = Folders[paths[i]];
					}
					else
					{
						parentFolder = Folders.Add(paths[i]);
					}
				}
				else
				{
					if (parentFolder.Folders[paths[i]] != null)
					{
						parentFolder = parentFolder.Folders[paths[i]];
					}
					else
					{
						parentFolder = parentFolder.Folders.Add(paths[i]);
					}
				}
			}

			ProjectFile pf = new ProjectFile();
			pf.SourceFileName = sourceFileName;
			pf.DestinationFileName = paths[paths.Length - 1];

			if (parentFolder == null)
			{
				Files.Add(pf);
			}
			else
			{
				parentFolder.Files.Add(pf);
			}
		}

		public ProjectFile FindFile(string filePath)
		{
			string[] pathParts = filePath.Split(new char[] { '\\', '/' });
			ProjectFolder pfParent = null;
			for (int i = 0; i < pathParts.Length - 1; i++)
			{
				if (pfParent == null)
				{
					pfParent = Folders[pathParts[i]];
				}
				else
				{
					pfParent = pfParent.Folders[pathParts[i]];
				}
			}
			if (pfParent == null)
			{
				return Files[pathParts[pathParts.Length - 1]];
			}
			else
			{
				return pfParent.Files[pathParts[pathParts.Length - 1]];
			}
		}
	}
}
