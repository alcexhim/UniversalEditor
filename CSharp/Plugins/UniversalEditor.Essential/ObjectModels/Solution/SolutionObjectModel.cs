using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Project;

namespace UniversalEditor.ObjectModels.Solution
{
	public class SolutionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Solution";
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarConfiguration.Clear();
			mvarProjects.Clear();
			mvarTitle = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			SolutionObjectModel solution = (where as SolutionObjectModel);
			solution.Title = (mvarTitle.Clone() as string);
			foreach (ProjectObjectModel project in mvarProjects)
			{
				solution.Projects.Add(project);
			}
			mvarConfiguration.CopyTo(solution.Configuration);
		}

		private ProjectObjectModel.ProjectObjectModelCollection mvarProjects = new ProjectObjectModel.ProjectObjectModelCollection();
		public ProjectObjectModel.ProjectObjectModelCollection Projects { get { return mvarProjects; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private PropertyList.PropertyListObjectModel mvarConfiguration = new PropertyList.PropertyListObjectModel();
		public PropertyList.PropertyListObjectModel Configuration { get { return mvarConfiguration; } }
	}
}
