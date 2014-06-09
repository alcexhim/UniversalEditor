using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Solution
{
	public class Project
	{
		public class ProjectCollection
			: System.Collections.ObjectModel.Collection<Project>
		{
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
