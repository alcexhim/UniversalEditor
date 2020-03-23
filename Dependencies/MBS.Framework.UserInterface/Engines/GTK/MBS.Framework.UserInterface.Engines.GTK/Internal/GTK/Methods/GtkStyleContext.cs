//
//  GtkStyleContext.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK.Methods
{
	internal class GtkStyleContext
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkStyleContext*/ gtk_style_context_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_add_provider(IntPtr hStyleContext, IntPtr /*GtkStyleProvider*/ provider, Constants.GtkStyleProviderPriority priority);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_add_class(IntPtr /*GtkStyleContext*/ context, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_get_property(IntPtr /*GtkStyleContext*/ context, string name, Internal.GTK.Constants.GtkStateFlags state, ref Internal.GLib.Structures.Value value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_get_background_color(IntPtr /*GtkStyleContext*/ context, Internal.GTK.Constants.GtkStateFlags state, ref Internal.GDK.Structures.GdkRGBA value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_get_color(IntPtr /*GtkStyleContext*/ context, Internal.GTK.Constants.GtkStateFlags state, ref Internal.GDK.Structures.GdkRGBA value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_set_path(IntPtr /*GtkStyleContext*/ context, IntPtr /*GtkWidgetPath*/ path);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_style_context_set_state(IntPtr /*GtkStyleContext*/ context, Constants.GtkStateFlags state);
	}
}

