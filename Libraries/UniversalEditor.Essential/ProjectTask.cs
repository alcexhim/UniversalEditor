//
//  ProjectTask.cs - represents a build task for a project
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor
{
	/// <summary>
	/// Represents a build task for a project.
	/// </summary>
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
}
