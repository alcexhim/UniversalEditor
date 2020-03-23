using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.Notify
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "notify";

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool notify_init (string appname);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr notify_notification_new(string summary, string body, string icon);

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool notify_notification_show(IntPtr notification, IntPtr error);
	}
}

