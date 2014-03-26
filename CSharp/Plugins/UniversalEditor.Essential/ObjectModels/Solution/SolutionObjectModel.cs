using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Solution
{
	public class SolutionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "Solution";
			}
			return _omr;
		}

		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			SolutionObjectModel solution = (where as SolutionObjectModel);
			solution.Title = (mvarTitle.Clone() as string);
			foreach (Project project in mvarProjects)
			{
				solution.Projects.Add(project);
			}
			mvarConfiguration.CopyTo(solution.Configuration);
		}

		private Project.ProjectCollection mvarProjects = new Project.ProjectCollection();
		public Project.ProjectCollection Projects { get { return mvarProjects; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private PropertyList.PropertyListObjectModel mvarConfiguration = new PropertyList.PropertyListObjectModel();
		public PropertyList.PropertyListObjectModel Configuration { get { return mvarConfiguration; } }
	}
}
