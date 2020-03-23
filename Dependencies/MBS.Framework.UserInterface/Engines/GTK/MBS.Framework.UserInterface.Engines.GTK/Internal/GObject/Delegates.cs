using System;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GObject
{
	internal static class Delegates
	{
		public delegate void SingleIntPtrFunc(IntPtr ptr);

		public delegate void GCallback(IntPtr handle, IntPtr data);
		public delegate T GCallback<T>(IntPtr handle, IntPtr data);
		
		public delegate void GCallbackV1I(IntPtr handle);
		public delegate void GCallbackV3I(IntPtr handle, IntPtr evt, IntPtr data);

		public delegate void ListViewRowActivatedFunc(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreePath*/ path, IntPtr /*GtkTreeViewColumn*/ column);

		public delegate void GDestroyNotify(IntPtr data);
		public delegate void GClosureNotify(IntPtr data, IntPtr closure);

		/// <summary>
		/// Handler for draw signal
		/// </summary>
		/// <param name="widget">the object which received the signal</param>
		/// <param name="cr">the cairo context to draw to</param>
		/// <param name="user_data">user data set when the signal handler was connected.</param>
		/// <returns>TRUE to stop other handlers from being invoked for the event. FALSE to propagate the event further.</returns>
		public delegate bool DrawFunc(IntPtr /*GtkWidget*/ widget, IntPtr /*CairoContext*/ cr, IntPtr user_data);

		public delegate int GApplicationCommandLineHandler(IntPtr handle, IntPtr commandLine, IntPtr user_data);

	}
}

