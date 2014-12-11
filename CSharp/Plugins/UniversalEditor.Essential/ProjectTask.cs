using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor
{
	public class ProjectTask
	{
		public class ProjectTaskCollection
			: System.Collections.ObjectModel.Collection<ProjectTask>
		{

		}

		public event EventHandler TaskStarted;
		public event ProjectTaskEventHandler TaskCompleted;
		public event ProjectTaskEventHandler TaskFailed;
		public event ProgressEventHandler TaskProgress;

		protected virtual void OnTaskStarted(EventArgs e)
		{
			if (TaskStarted != null) TaskStarted(this, e);
		}
		protected virtual void OnTaskCompleted(ProjectTaskEventArgs e)
		{
			if (TaskCompleted != null) TaskCompleted(this, e);
		}
		protected virtual void OnTaskFailed(ProjectTaskEventArgs e)
		{
			if (TaskFailed != null) TaskFailed(this, e);
		}
		protected virtual void OnTaskProgress(ProgressEventArgs e)
		{
			if (TaskProgress != null) TaskProgress(this, e);
		}

		private ProjectTaskAction.ProjectTaskActionCollection mvarActions = new ProjectTaskAction.ProjectTaskActionCollection();
		public ProjectTaskAction.ProjectTaskActionCollection Actions { get { return mvarActions; } }
		
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public void Execute()
		{
			OnTaskStarted(EventArgs.Empty);

			ExpandedStringVariableStore variables = new ExpandedStringVariableStore();
			ExpandedStringVariable varr = new ExpandedStringVariable();
			varr.ID = "CompilerExecutablePath";
			varr.Value = @"C:\Applications\MinGW\bin\gcc.exe";
			variables.Variables[ExpandedStringSegmentVariableScope.Global, "CompilerExecutablePath"] = varr;

			varr = new ExpandedStringVariable();
			varr.ID = "OutputFileName";
			varr.Value = @"C:\Temp\ProjectOutput.exe";
			variables.Variables[ExpandedStringSegmentVariableScope.Project, "OutputFileName"] = varr;

			for (int i = 0; i < mvarActions.Count; i++)
			{
				try
				{
					mvarActions[i].Execute(variables);
				}
				catch (Exception ex)
				{
					OnTaskFailed(new ProjectTaskEventArgs(ex.Message));
					return;
				}

				OnTaskProgress(new ProgressEventArgs(i, mvarActions.Count, mvarActions[i].Title));
			}
		}
	}
	public abstract class ProjectTaskAction : References<ProjectTaskActionReference>
	{
		public abstract string Title { get; }
		protected abstract void ExecuteInternal(ExpandedStringVariableStore variables);

		public void Execute(ExpandedStringVariableStore variables)
		{
			ExecuteInternal(variables);
		}

		public class ProjectTaskActionCollection
			: System.Collections.ObjectModel.Collection<ProjectTaskAction>
		{

		}

		public virtual ProjectTaskActionReference MakeReference()
		{
			return new ProjectTaskActionReference(GetType());
		}

		public void LoadFromMarkup(MarkupTagElement tag)
		{
			if (tag == null) return;
			if (tag.FullName != "Action") return;
			LoadFromMarkupInternal(tag);
		}
		protected abstract void LoadFromMarkupInternal(MarkupTagElement tag);
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
	public class ProjectTaskActionPackage : ProjectTaskAction
	{
		private DataFormatReference mvarDataFormatReference = null;
		/// <summary>
		/// The <see cref="DataFormatReference" /> that specifies a <see cref="DataFormat" /> compatible with the
		/// <see cref="UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel" /> in which to package the
		/// project files.
		/// </summary>
		public DataFormatReference DataFormatReference { get { return mvarDataFormatReference; } }

		private static ProjectTaskActionReference _ptar = null;
		public override ProjectTaskActionReference MakeReference()
		{
			if (_ptar == null)
			{
				_ptar = base.MakeReference();
				_ptar.ProjectTaskActionTypeID = new Guid("{527B7B07-FB0E-46F2-9EA8-0E93E3B21A14}");
				_ptar.ProjectTaskActionTypeName = "UniversalEditor.ProjectTaskActionPackage";
			}
			return _ptar;
		}

		private ExpandedString mvarOutputFileName = null;
		public ExpandedString OutputFileName { get { return mvarOutputFileName; } set { mvarOutputFileName = value; } }

		public override string Title
		{
			get { return "Package: "; }
		}
		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
			DataFormat df = mvarDataFormatReference.Create();

			string outputFileName = String.Empty;
			if (mvarOutputFileName != null) outputFileName = mvarOutputFileName.ToString(variables);
		}
		protected override void LoadFromMarkupInternal(MarkupTagElement tag)
		{
			MarkupTagElement tagDataFormatReference = (tag.Elements["DataFormatReference"] as MarkupTagElement);
			if (tagDataFormatReference != null)
			{
				MarkupAttribute attTypeName = tagDataFormatReference.Attributes["TypeName"];
				MarkupAttribute attTypeID = tagDataFormatReference.Attributes["TypeID"];
				if (attTypeName == null && attTypeID == null) throw new ArgumentNullException("Must specify at least one of 'TypeName' or 'TypeID'");


			}

			MarkupTagElement tagOutputFileName = (tag.Elements["OutputFileName"] as MarkupTagElement);
			if (tagOutputFileName != null) mvarOutputFileName = ExpandedString.FromMarkup(tagOutputFileName);
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

		protected override void ExecuteInternal(ExpandedStringVariableStore variables)
		{
			string fileNameWithArguments = mvarCommandLine.ToString(variables);
			if (String.IsNullOrEmpty(fileNameWithArguments)) return;

			string[] fileNameArgumentsSplit = fileNameWithArguments.Split(new char[] { ' ' }, "\"", StringSplitOptions.None, 2);
			string fileName = fileNameArgumentsSplit[0];
			string arguments = fileNameArgumentsSplit[1];

			if (!System.IO.File.Exists(fileName)) throw new System.IO.FileNotFoundException(fileName);

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo = new System.Diagnostics.ProcessStartInfo(fileName, arguments);
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.Start();
			p.WaitForExit();

			string error = p.StandardError.ReadToEnd();
			string output = p.StandardOutput.ReadToEnd();

			if (!String.IsNullOrEmpty(error))
			{
				throw new Exception(error);
			}
		}

		protected override void LoadFromMarkupInternal(MarkupTagElement tag)
		{
			MarkupTagElement tagCommandLine = (tag.Elements["CommandLine"] as MarkupTagElement);
			if (tagCommandLine != null)
			{
				mvarCommandLine = ExpandedString.FromMarkup(tagCommandLine);
			}
		}
	}
}
