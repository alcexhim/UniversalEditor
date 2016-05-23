using System;

namespace UniversalEditor.Engines.GTK
{
	public class GtkMenu : GtkMenuShell
	{
		protected override IntPtr CreateInternal ()
		{
			IntPtr handle = Internal.GTK.Methods.gtk_menu_new ();
			return handle;
		}

		public string Text
		{
			get { return Internal.GTK.Methods.gtk_menu_get_title(this.Handle); }
			set { Internal.GTK.Methods.gtk_menu_set_title (this.Handle, value); }
		}
	}
}

