using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK.Methods
{
	internal static class GtkSpinButton
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_spin_button_new_with_range(double minimum, double maximum, double step);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_spin_button_get_range(IntPtr /*GtkWidget*/ spin_button, out double min, out double max);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_spin_button_set_range(IntPtr /*GtkWidget*/ spin_button, double min, double max);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_spin_button_get_increments(IntPtr /*GtkWidget*/ spin_button, out double step, out double page);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_spin_button_set_increments(IntPtr /*GtkWidget*/ spin_button, double step, double page);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern double gtk_spin_button_get_value(IntPtr /*GtkWidget*/ spin_button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_spin_button_set_value(IntPtr /*GtkWidget*/ spin_button, double value);
	}
}
