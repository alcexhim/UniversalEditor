using System;

namespace UniversalEditor.Engines.GTK
{
	public static class GtkApplication
	{
		private static IntPtr mvarHandle = IntPtr.Zero;

		public static bool Initialize(ref int argc, ref string[] argv)
		{
			bool check = Internal.GTK.Methods.gtk_init_check (ref argc, ref argv);
			if (!check)
				return check;

			mvarHandle = Internal.GTK.Methods.gtk_application_new ("net.alcetech.UniversalEditor", Internal.GTK.Constants.GApplicationFlags.None);


			return check;
		}
		public static void Run()
		{
			Internal.GTK.Methods.gtk_main ();
		}
		public static void Quit()
		{
			Internal.GTK.Methods.gtk_main_quit ();
		}
	}
}

