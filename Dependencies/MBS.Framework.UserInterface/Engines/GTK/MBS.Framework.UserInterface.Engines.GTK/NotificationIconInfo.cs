using System;

namespace MBS.Framework.UserInterface.Engines.GTK
{
	public class NotificationIconInfo
	{
		/// <summary>
		/// The handle to the native object for this notification icon.
		/// </summary>
		public IntPtr hIndicator;

		/// <summary>
		/// The handle to the native object for the title menu item for this notification icon.
		/// </summary>
		public IntPtr hMenuItemTitle;
	}
}

