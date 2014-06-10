using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Project
{
	public class ProjectFileSystem
	{
		private ProjectFolder.ProjectFolderCollection mvarFolders = new ProjectFolder.ProjectFolderCollection();
		/// <summary>
		/// The collection of folders in this project.
		/// </summary>
		public ProjectFolder.ProjectFolderCollection Folders { get { return mvarFolders; } }

		private ProjectFile.ProjectFileCollection mvarFiles = new ProjectFile.ProjectFileCollection();
		/// <summary>
		/// The collection of files in this project.
		/// </summary>
		public ProjectFile.ProjectFileCollection Files { get { return mvarFiles; } }

		public void Clear()
		{
			mvarFolders.Clear();
			mvarFiles.Clear();
		}
		public void CopyTo(ProjectFileSystem clone)
		{
			foreach (ProjectFile file in mvarFiles)
			{
				clone.Files.Add(file.Clone() as ProjectFile);
			}
			foreach (ProjectFolder folder in mvarFolders)
			{
				clone.Folders.Add(folder.Clone() as ProjectFolder);
			}
		}
	}
}
