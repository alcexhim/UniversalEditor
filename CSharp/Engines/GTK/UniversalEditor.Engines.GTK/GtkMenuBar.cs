using System;

namespace UniversalEditor.Engines.GTK
{
	public class GtkMenuBar : GtkMenuShell
	{
		protected override IntPtr CreateInternal ()
		{
			IntPtr handle = Internal.GTK.Methods.gtk_menu_bar_new ();
			return handle;
		}

	}
}

