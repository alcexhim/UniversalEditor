//
//  ProjectType.cs - provides common functionality associated with all projects which have the same ProjectType
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
	/// Provides common functionality associated with all projects which have the same <see cref="ProjectType" />.
	/// </summary>
	public class ProjectType
	{
		public class ProjectTypeCollection
			: System.Collections.ObjectModel.Collection<ProjectType>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarLargeIconImageFileName = null;
		public string LargeIconImageFileName { get { return mvarLargeIconImageFileName; } set { mvarLargeIconImageFileName = value; } }

		private string mvarSmallIconImageFileName = null;
		public string SmallIconImageFileName { get { return mvarSmallIconImageFileName; } set { mvarSmallIconImageFileName = value; } }

		private ProjectTask.ProjectTaskCollection mvarTasks = new ProjectTask.ProjectTaskCollection();
		/// <summary>
		/// Gets the <see cref="ProjectTask" />s that are made available by this <see cref="ProjectType" />.
		/// </summary>
		public ProjectTask.ProjectTaskCollection Tasks { get { return mvarTasks; } }

		private ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection mvarItemShortcuts = new ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection();
		public ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection ItemShortcuts { get { return mvarItemShortcuts; } }

		private ProjectTypeVariable.ProjectTypeVariableCollection mvarVariables = new ProjectTypeVariable.ProjectTypeVariableCollection();
		public ProjectTypeVariable.ProjectTypeVariableCollection Variables { get { return mvarVariables; } }

		public string ProjectFileExtension { get; set; } = null;
	}
	/// <summary>
	/// A shortcut placed in the "Add New Item" menu when the project is selected. When
	/// activated, these shortcuts create a new Document with the ObjectModel specified in the
	/// ObjectModelReference and add the resulting Document to the selected project. The
	/// Document may optionally be preloaded with content from the DocumentTemplate, if
	/// specified.
	/// </summary>
	public class ProjectTypeItemShortcut
	{
		public class ProjectTypeItemShortcutCollection
			: System.Collections.ObjectModel.Collection<ProjectTypeItemShortcut>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private ObjectModelReference mvarObjectModelReference = null;
		public ObjectModelReference ObjectModelReference { get { return mvarObjectModelReference; } set { mvarObjectModelReference = value; } }

		private DocumentTemplate mvarDocumentTemplate = null;
		/// <summary>
		/// The <see cref="DocumentTemplate" /> from which to load the document content for the
		/// shortcut.
		/// </summary>
		public DocumentTemplate DocumentTemplate { get { return mvarDocumentTemplate; } set { mvarDocumentTemplate = value; } }
	}
}
