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
		
		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel
		{
			get { return mvarObjectModel; }
			set
			{
				if (mvarObjectModel == value) return;
				
				ObjectModelChangingEventArgs omcea = new ObjectModelChangingEventArgs(mvarObjectModel, value);
				OnObjectModelChanging (omcea);
				if (omcea.Cancel) return;
				
				mvarObjectModel = value;
				OnObjectModelChanged(EventArgs.Empty);
			}
		}
		

		public virtual string Title { get { return String.Empty; } }

		private ObjectModelReference.ObjectModelReferenceCollection mvarSupportedObjectModels = new ObjectModelReference.ObjectModelReferenceCollection();
		public ObjectModelReference.ObjectModelReferenceCollection SupportedObjectModels { get { return mvarSupportedObjectModels; } }
		
		public event ObjectModelChangingEventHandler ObjectModelChanging;
		protected virtual void OnObjectModelChanging(ObjectModelChangingEventArgs e)
		{
			if (ObjectModelChanging != null) ObjectModelChanging(this, e);
		}
		public event EventHandler ObjectModelChanged;
		protected virtual void OnObjectModelChanged(EventArgs e)
		{
			if (ObjectModelChanged != null) ObjectModelChanged(this, e);
		}
		#endregion
	}
}

