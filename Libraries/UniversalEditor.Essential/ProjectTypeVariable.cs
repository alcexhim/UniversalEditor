using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public enum ProjectTypeVariableType
	{
		Text,
		Choice,
		FileOpen,
		FileSave
	}
	public class ProjectTypeVariable
	{

		public class ProjectTypeVariableCollection
			: System.Collections.ObjectModel.Collection<ProjectTypeVariable>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private ProjectTypeVariableType mvarType = ProjectTypeVariableType.Text;
		public ProjectTypeVariableType Type { get { return mvarType; } set { mvarType = value; } }

		private object mvarDefaultValue = null;
		public object DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		private Dictionary<string, object> mvarValidValues = new Dictionary<string, object>();
		public Dictionary<string, object> ValidValues { get { return mvarValidValues; } }

	}
}
