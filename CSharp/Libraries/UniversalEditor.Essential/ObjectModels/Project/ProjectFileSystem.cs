﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Project
{
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
