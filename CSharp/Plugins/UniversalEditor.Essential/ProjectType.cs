using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class ProjectType
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarLargeIconImageFileName = null;
		public string LargeIconImageFileName { get { return mvarLargeIconImageFileName; } set { mvarLargeIconImageFileName = value; } }

		private string mvarSmallIconImageFileName = null;
		public string SmallIconImageFileName { get { return mvarSmallIconImageFileName; } set { mvarSmallIconImageFileName = value; } }

		private ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection mvarItemShortcuts = new ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection();
		public ProjectTypeItemShortcut.ProjectTypeItemShortcutCollection ItemShortcuts { get { return mvarItemShortcuts; } }
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
