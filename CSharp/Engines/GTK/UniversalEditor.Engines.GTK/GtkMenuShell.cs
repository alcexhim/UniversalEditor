using System;

namespace UniversalEditor.Engines.GTK
{
	public abstract class GtkMenuShell : GtkWidget
	{
		private GtkMenuItem.GtkMenuItemCollection mvarItems = null;
		public GtkMenuItem.GtkMenuItemCollection Items { get { return mvarItems; } }

		public GtkMenuShell()
		{
			mvarItems = new GtkMenuItem.GtkMenuItemCollection (this);
		}
	}
}

