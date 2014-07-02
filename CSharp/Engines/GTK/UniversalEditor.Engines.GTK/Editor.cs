using System;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.GTK
{
	[System.ComponentModel.ToolboxItem(true)]
	public abstract partial class Editor : Gtk.Bin, IEditorImplementation
	{
		public Editor ()
		{
			this.Build ();
		}

		#region IEditorImplementation implementation
		public event ToolboxItemEventHandler ToolboxItemAdded;
		public event ToolboxItemEventHandler ToolboxItemSelected;

		public abstract void Copy ();
		public abstract void Paste ();
		public abstract void Delete ();

		public abstract void Undo ();
		public abstract void Redo ();

		public abstract bool SelectToolboxItem (ToolboxItem item);

		public abstract string Title { get; }

		private ObjectModelReference.ObjectModelReferenceCollection mvarSupportedObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get { return mvarSupportedObjectModels; } }
		#endregion
	}
}

