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

		public void Execute(ProgressEventHandler progressEventHandler = null)
		{
			for (int i = 0; i < mvarActions.Count; i++)
			{
				mvarActions[i].Execute();
				if (progressEventHandler != null)
				{
					progressEventHandler(this, new ProgressEventArgs(i, mvarActions.Count, mvarActions[i].Title));
				}
			}
		}
	}
	public abstract class ProjectTaskAction : References<ProjectTaskActionReference>
	{
		public abstract string Title { get; }
		protected abstract void ExecuteInternal();

		public void Execute()
		{
			ExecuteInternal();
		}

		public class ProjectTaskActionCollection
			: System.Collections.ObjectModel.Collection<ProjectTaskAction>
		{

		}

		public virtual ProjectTaskActionReference MakeReference()
		{
			return new ProjectTaskActionReference(GetType());
		}
	}
	public class ProjectTaskActionReference : ReferencedBy<ProjectTaskAction>
	{
		private Guid mvarProjectTaskActionTypeID = Guid.Empty;
		public Guid ProjectTaskActionTypeID { get { return mvarProjectTaskActionTypeID; } set { mvarProjectTaskActionTypeID = value; } }

		private string mvarProjectTaskActionTypeName = String.Empty;
		public string ProjectTaskActionTypeName { get { return mvarProjectTaskActionTypeName; } set { mvarProjectTaskActionTypeName = value; } }

		private Type mvarProjectTaskActionType = null;
		public ProjectTaskActionReference(Type type)
		{
			mvarProjectTaskActionType = type;
			mvarProjectTaskActionTypeName = type.FullName;
		}
		public ProjectTaskActionReference(Guid id)
		{
			mvarProjectTaskActionTypeID = id;
		}
		public ProjectTaskActionReference(string typeName)
		{
			mvarProjectTaskActionTypeName = typeName;
		}
		public ProjectTaskAction Create()
		{
			if (mvarProjectTaskActionType != null)
			{
				return (ProjectTaskAction)mvarProjectTaskActionType.Assembly.CreateInstance(mvarProjectTaskActionType.FullName);
			}
			return null;
		}

		public string[] GetDetails()
		{
			throw new NotImplementedException();
		}

		public bool ShouldFilterObject(string filter)
		{
			throw new NotImplementedException();
		}

		private static Dictionary<Guid, ProjectTaskActionReference> _dict = null;

		public static ProjectTaskActionReference GetByTypeID(Guid id)
		{
			if (_dict == null)
			{
				_dict = new Dictionary<Guid, ProjectTaskActionReference>();
				Type[] types = Common.Reflection.GetAvailableTypes();
				foreach (Type type in types)
				{
					if (!type.IsAbstract && type.IsSubclassOf(typeof(ProjectTaskAction)))
					{
						ProjectTaskAction action = (ProjectTaskAction)type.Assembly.CreateInstance(type.FullName);
						ProjectTaskActionReference actionref = action.MakeReference();
						_dict[actionref.ProjectTaskActionTypeID] = actionref;
					}
				}
			}
			return _dict[id];
		}
	}
	public class ProjectTaskActionExecute : ProjectTaskAction
	{
		public override string Title
		{
			get { return "Execute: " + mvarCommandLine.ToString(); }
		}

		private static ProjectTaskActionReference _ptar = null;
		public override ProjectTaskActionReference MakeReference()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReference();
				_ptar.ProjectTaskActionTypeID = new Guid("{EE505E05-F125-4718-BA0A-879C72B5125A}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionExecute";
			}
			return _ptar;
		}

		private ExpandedString mvarCommandLine = ExpandedString.Empty;
		public ExpandedString CommandLine
		{
			get { return mvarCommandLine; }
			set
			{
				if (value == null)
				{
					mvarCommandLine = ExpandedString.Empty;
					return;
				}
				mvarCommandLine = value;
			}
		}

		protected override void ExecuteInternal()
		{
			string fileNameWithArguments = mvarCommandLine.ToString();
			if (String.IsNullOrEmpty(fileNameWithArguments)) return;

			string[] fileNameArgumentsSplit = fileNameWithArguments.Split(new char[] { ' ' }, "\"", StringSplitOptions.None, 2);
			string fileName = fileNameArgumentsSplit[0];
			string arguments = fileNameArgumentsSplit[1];

			if (!System.IO.File.Exists(fileName)) throw new System.IO.FileNotFoundException(fileName);

			System.Diagnostics.Process p = new System.Diagnostics.Process();

			StringBuilder sbArguments = new StringBuilder();
			// TODO: complete loading arguments

			p.StartInfo = new System.Diagnostics.ProcessStartInfo(fileName, arguments);
			p.Start();
		}
	}
}
