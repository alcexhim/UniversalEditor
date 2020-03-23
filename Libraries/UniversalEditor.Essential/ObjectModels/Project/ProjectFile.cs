using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Project
{
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
