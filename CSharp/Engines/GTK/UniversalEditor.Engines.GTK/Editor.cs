using System;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.GTK
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Editor : Gtk.Bin, IEditorImplementation
	{
		public Editor ()
		{
			this.Build ();
		}

		#region IEditorImplementation implementation
		public event ToolboxItemEventHandler ToolboxItemAdded;
		public event ToolboxItemEventHandler ToolboxItemSelected;

		public virtual void Copy ()
		{
		}
		public virtual void Paste ()
		{
		}
		public virtual void Delete ()
		{
		}

		public virtual void Undo ()
		{
		}
		public virtual void Redo ()
		{
		}

		public virtual bool SelectToolboxItem (ToolboxItem item)
		{
			return false;
		}

		public virtual string Title { get { return String.Empty; } }

		private ObjectModelReference.ObjectModelReferenceCollection mvarSupportedObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get { return mvarSupportedObjectModels; } }
		#endregion
	}
}

