using System;
using UniversalEditor.UserInterface;
using UniversalEditor.Engines.GTK;

namespace UniversalEditor.Engines.GTK.Plugins.FileSystem
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class FileSystemEditor : Editor
	{
		
		public FileSystemEditor ()
		{
			this.Build ();
		}

		#region implemented abstract members of UniversalEditor.Engines.GTK.Editor
		public override void Copy ()
		{
			throw new System.NotImplementedException ();
		}

		public override void Paste ()
		{
			throw new System.NotImplementedException ();
		}

		public override void Delete ()
		{
			throw new System.NotImplementedException ();
		}

		public override void Undo ()
		{
			throw new System.NotImplementedException ();
		}

		public override void Redo ()
		{
			throw new System.NotImplementedException ();
		}

		public override bool SelectToolboxItem (ToolboxItem item)
		{
			throw new System.NotImplementedException ();
		}

		public override string Title { get { return "File System/Archive"; } }
		#endregion
	}
}

