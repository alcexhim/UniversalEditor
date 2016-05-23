using System;

namespace UniversalEditor.Engines.GTK
{
	public enum GtkBoxOrientation
	{
		Horizontal,
		Vertical
	}
	public class GtkBox : GtkContainer
	{
		private int mvarItemCount = 0;

		private GtkBoxOrientation mvarOrientation = GtkBoxOrientation.Vertical;

		public GtkBox(GtkBoxOrientation orientation, int itemCount)
		{
			mvarOrientation = orientation;
			mvarItemCount = itemCount;
		}

		protected override IntPtr CreateInternal ()
		{
			Internal.GTK.Constants.GtkBoxOrientation orientation = Internal.GTK.Constants.GtkBoxOrientation.Vertical;
			switch (mvarOrientation) 
			{
				case GtkBoxOrientation.Horizontal:
				{
					orientation = Internal.GTK.Constants.GtkBoxOrientation.Horizontal;
					break;
				}
				case GtkBoxOrientation.Vertical:
				{
					orientation = Internal.GTK.Constants.GtkBoxOrientation.Vertical;
					break;
				}
			}
			IntPtr handle = Internal.GTK.Methods.gtk_box_new (orientation, mvarItemCount);
			return handle;
		}

		public void Pack(PackDirection direction, GtkWidget child, bool expand, bool fill, int padding)
		{
			switch (direction)
			{
				case PackDirection.Start:
				{
					Internal.GTK.Methods.gtk_box_pack_start (this.Handle, child.Handle, expand, fill, padding);
					break;
				}
				case PackDirection.End:
				{
					Internal.GTK.Methods.gtk_box_pack_end (this.Handle, child.Handle, expand, fill, padding);
					break;
				}
			}
		}
	}
}

