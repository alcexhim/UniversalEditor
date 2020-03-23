using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GIO
{
	internal class Methods
	{
		public const string LIBRARY_FILENAME = "gio-2.0";

		#region GApplication
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_application_new (string application_id, Constants.GApplicationFlags flags);
		[DllImport(LIBRARY_FILENAME)]
		public static extern bool g_application_register (IntPtr /*GApplication*/ application, IntPtr /*GCancellable*/ cancellable, IntPtr /*GError*/ error);
		[DllImport(LIBRARY_FILENAME)]
		public static extern bool g_application_get_is_registered (IntPtr /*GApplication*/ application);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_application_command_line_get_arguments(IntPtr /*GApplication*/ commandLine, ref int argc);
		[DllImport(LIBRARY_FILENAME)]
		public static extern int g_application_run (IntPtr /*GApplication*/ application, int argc, string[] argv);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_application_quit(IntPtr /*GApplication*/ application);
		#endregion

		#region GMenu
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_menu_new ();
		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_menu_append_item (IntPtr /*GMenu*/ menu, IntPtr /*GMenuItem*/ item);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GMenuItem*/ g_menu_item_new (string label, string detailed_action);
		#endregion

		#region GAction

		#region GSimpleAction
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GSimpleAction*/ g_simple_action_new (string name, IntPtr /*GVariantType*/ parameter_type);
		#endregion
		#endregion

		#region GActionGroup

		#region GSimpleActionGroup
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GSimpleActionGroup*/ g_simple_action_group_new ();
		#endregion
		#endregion

		#region GActionEntry
		#endregion

		#region GActionMap
		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_action_map_add_action (IntPtr /*GActionMap*/ action_map, IntPtr  /*GAction*/ action);
		#endregion
	}
}

