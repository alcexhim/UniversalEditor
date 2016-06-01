using System;

namespace UniversalEditor.Engines.GTK
{
	public class GtkSeparator : GtkWidget
	{
		private GtkBoxOrientation mvarOrientation = GtkBoxOrientation.Horizontal;
		public GtkBoxOrientation Orientation { get { return mvarOrientation; } }

		public GtkSeparator (GtkBoxOrientation orientation)
		{
			mvarOrientation = orientation;
		}

		protected override IntPtr CreateInternal ()
		{
			IntPtr handle = Internal.GTK.Methods.gtk_separator_new (mvarOrientation);
			return handle;
		}
	}
}

