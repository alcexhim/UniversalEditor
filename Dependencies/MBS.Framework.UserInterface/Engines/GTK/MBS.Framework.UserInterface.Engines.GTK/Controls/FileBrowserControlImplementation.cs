//
//  FileBrowserControlImplementation.cs
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
using MBS.Framework.UserInterface.Controls.FileBrowser;
using MBS.Framework.UserInterface.Controls.FileBrowser.Native;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(FileBrowserControl))]
	public class FileBrowserControlImplementation : GTKNativeImplementation, IFileBrowserControlImplementation
	{
		public FileBrowserControlImplementation(Engine engine, Control control)
			: base (engine, control)
		{
			file_activated_handler = new Internal.GObject.Delegates.GCallback (file_activated);
		}

		private Internal.GObject.Delegates.GCallback file_activated_handler = null;
		private void file_activated (IntPtr /*GtkFileChooser*/ chooser, IntPtr user_data)
		{
			FileBrowserControl ctl = ((Engine as GTKEngine).GetControlByHandle (chooser) as FileBrowserControl);
			if (ctl == null) return;

			InvokeMethod (ctl, "OnItemActivated", EventArgs.Empty);
		}

		protected override NativeControl CreateControlInternal (Control control)
		{
			FileBrowserControl ctl = (control as FileBrowserControl);
			IntPtr handle = Internal.GTK.Methods.GtkFileChooserWidget.gtk_file_chooser_widget_new ((Engine as GTKEngine).FileBrowserModeToGtkFileChooserAction (ctl.Mode));

			// set up the file filters
			foreach (FileDialogFileNameFilter filter in ctl.FileNameFilters)
			{
				AddFileNameFilter(ctl, filter);
			}

			Internal.GObject.Methods.g_signal_connect (handle, "file_activated", file_activated_handler);

			return new GTKNativeControl (handle);
		}

		public void UpdateSelectedFileNames (System.Collections.Generic.List<string> coll)
		{
			IntPtr hFileBrowser = (Handle as GTKNativeControl).Handle;
			FileBrowserControl ctl = (Control as FileBrowserControl);

			string[] fileNames = Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_get_filenames_managed (hFileBrowser);
			foreach (string fileName in fileNames) {
				coll.Add (fileName);
			}
		}

		public void ClearFileNameFilters()
		{
			FileBrowserControl ctl = (Control as FileBrowserControl);
			IntPtr hFileBrowser = (Handle as GTKNativeControl).Handle;

			for (int i = 0; i < ctl.FileNameFilters.Count; i++)
			{
				IntPtr hFilter = Dialogs.FileDialogImplementation.GetHandleForGTKFileChooserFilter(ctl.FileNameFilters[i]);
				Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_remove_filter(hFileBrowser, hFilter);
				Dialogs.FileDialogImplementation.UnregisterGTKFileChooserFilter(ctl.FileNameFilters[i]);
			}
		}

		public void AddFileNameFilter(IFileBrowserControl control, FileDialogFileNameFilter filter)
		{
			IntPtr hFileBrowser = (Handle as GTKNativeControl).Handle;
			Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_add_filter(hFileBrowser, Dialogs.FileDialogImplementation.CreateGTKFileChooserFilter(filter));
		}

		public void RemoveFileNameFilter(IFileBrowserControl control, FileDialogFileNameFilter filter)
		{
			IntPtr hFileBrowser = (Handle as GTKNativeControl).Handle;
			IntPtr hFilter = Dialogs.FileDialogImplementation.GetHandleForGTKFileChooserFilter(filter);
			Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_remove_filter(hFileBrowser, hFilter);
			Dialogs.FileDialogImplementation.UnregisterGTKFileChooserFilter(filter);
		}
	}
}

