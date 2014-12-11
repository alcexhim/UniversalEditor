 using System;
using System.Collections.Generic;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor
{
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

		public ProjectTaskActionReference MakeReference()
		{
			return MakeReferenceInternal();
		}
		protected virtual ProjectTaskActionReference MakeReferenceInternal()
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
}
