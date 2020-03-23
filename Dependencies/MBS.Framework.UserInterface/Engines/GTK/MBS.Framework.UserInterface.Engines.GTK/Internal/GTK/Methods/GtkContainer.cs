//
//  GtkContainer.cs
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
	internal class GtkContainer
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_container_add(IntPtr container, IntPtr widget);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_container_set_border_width(IntPtr container, int width);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_container_get_focus_child(IntPtr container);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ gtk_container_get_children(IntPtr /*GtkContainer*/ container);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_container_set_focus_child(IntPtr container, IntPtr widget);
	}
}

