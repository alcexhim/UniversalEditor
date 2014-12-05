using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ProjectTask
	{
		public class ProjectTaskCollection
			: System.Collections.ObjectModel.Collection<ProjectTask>
		{

		}

		private ProjectTaskAction.ProjectTaskActionCollection mvarActions = new ProjectTaskAction.ProjectTaskActionCollection();
		public ProjectTaskAction.ProjectTaskActionCollection Actions { get { return mvarActions; } }
		
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
	public abstract class ProjectTaskAction
	{
		public abstract Guid ID { get; }

		public class ProjectTaskActionCollection
			: System.Collections.ObjectModel.Collection<ProjectTaskAction>
		{

		}
	}
}
