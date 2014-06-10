using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Project
{
	public class ProjectObjectModel : ObjectModel
	{
		public class ProjectObjectModelCollection
			: System.Collections.ObjectModel.Collection<ProjectObjectModel>
		{
		}

		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Project";
				_omr.Description = "Stores a set of related files and folders with an accompanying configuration.";
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarConfiguration.Clear();
			mvarFileSystem.Clear();
			mvarID = Guid.Empty;
			mvarProjectType = null;
			mvarReferences.Clear();
			mvarRelativeFileName = String.Empty;
			mvarTitle = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			ProjectObjectModel clone = (where as ProjectObjectModel);
			mvarConfiguration.CopyTo(clone.Configuration);
			mvarFileSystem.CopyTo(clone.FileSystem);
			clone.ID = mvarID;
			clone.ProjectType = mvarProjectType;
			foreach (Reference _ref in mvarReferences)
			{
				clone.References.Add(_ref);
			}
			clone.RelativeFileName = (mvarRelativeFileName.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private PropertyListObjectModel mvarConfiguration = new PropertyListObjectModel();
		public PropertyListObjectModel Configuration { get { return mvarConfiguration; } }

		private Reference.ReferenceCollection mvarReferences = new Reference.ReferenceCollection();
		public Reference.ReferenceCollection References { get { return mvarReferences; } }

		private ProjectFileSystem mvarFileSystem = new ProjectFileSystem();
		public ProjectFileSystem FileSystem { get { return mvarFileSystem; } }

		private ProjectType mvarProjectType = null;
		public ProjectType ProjectType { get { return mvarProjectType; } set { mvarProjectType = value; } }

		private string mvarRelativeFileName = String.Empty;
		public string RelativeFileName { get { return mvarRelativeFileName; } set { mvarRelativeFileName = value; } }
	}
}
