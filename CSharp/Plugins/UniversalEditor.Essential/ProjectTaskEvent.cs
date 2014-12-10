using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public delegate void ProjectTaskEventHandler(object sender, ProjectTaskEventArgs e);
	public class ProjectTaskEventArgs : EventArgs
	{
		private string mvarMessage = String.Empty;
		public string Message { get { return mvarMessage; } set { mvarMessage = value; } }

		public ProjectTaskEventArgs(string message)
		{
			mvarMessage = message;
		}
	}
}
