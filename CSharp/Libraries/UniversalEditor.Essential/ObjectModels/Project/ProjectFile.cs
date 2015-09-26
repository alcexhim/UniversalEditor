using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Project
{
	public class ProjectFile : ICloneable
	{
		public class ProjectFileCollection
			: System.Collections.ObjectModel.Collection<ProjectFile>
		{
			private ProjectFolder _parent = null;
			public ProjectFileCollection(ProjectFolder parent = null)
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

			protected override void InsertItem(int index, ProjectFile item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				foreach (ProjectFile file in this)
				{
					file.Parent = null;
				}
				base.ClearItems();
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

		private ProjectFolder mvarParent = null;
		public ProjectFolder Parent { get { return mvarParent; } private set { mvarParent = value; } }

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
	}
}
