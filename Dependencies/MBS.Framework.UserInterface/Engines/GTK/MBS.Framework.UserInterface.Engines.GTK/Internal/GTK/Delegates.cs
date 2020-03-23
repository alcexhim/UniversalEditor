//
//  Delegates.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK
{
	internal static class Delegates
	{
		public delegate void GtkTreeViewRowActivatedFunc(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreePath*/ path, IntPtr /*GtkTreeViewColumn*/ column);
		public delegate void GtkWidgetRealize(IntPtr /*GtkWidget*/ widget);
		public delegate bool GtkWidgetEvent(IntPtr /*GtkWidget*/ widget, IntPtr hEventArgs, IntPtr user_data);
		public delegate bool GtkFileFilterFunc(ref Structures.GtkFileFilterInfo filter_info, IntPtr data);
		public delegate void GtkTreeViewFunc(IntPtr /*GtkTreeView*/ tree_view);
		public delegate bool GtkGlAreaRenderFunc(IntPtr /*GtkGLArea*/ area, IntPtr /*GdkGLContext*/ context);
		public delegate void GtkDragEvent(IntPtr /*GtkWidget*/ widget, IntPtr /*GdkDragContext*/ context, IntPtr user_data);
		public delegate void GtkDragDataGetEvent(IntPtr /*GtkWidget*/ widget, IntPtr /*GdkDragContext*/ context, IntPtr /*GtkSelectionData*/ data, uint info, uint time, IntPtr user_data);
		public delegate bool GtkTreeSelectionFunc(IntPtr /*GtkTreeSelection*/ selection, IntPtr /*GtkTreeModel*/ model, IntPtr /*GtkTreePath*/ path, bool path_currently_selected, IntPtr data);

		public delegate void GtkPrintJobCompleteFunc(IntPtr /*GtkPrintJob*/ print_job, IntPtr user_data, ref GLib.Structures.GError error);

		public delegate int GtkTreeIterCompareFunc(IntPtr /*GtkTreeModel*/ model, ref Structures.GtkTreeIter a, ref Structures.GtkTreeIter b, IntPtr user_data);
	}
}
