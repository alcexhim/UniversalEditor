//
//  FileDialogImplementation.cs
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
using System.Collections.Generic;
using System.Text;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(FileDialog))]
	public class FileDialogImplementation : GTKDialogImplementation //, IFileDialogImplementation
	{
		public FileDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}
		
		#region File Dialog
		private static Dictionary<FileDialogFileNameFilter, IntPtr> _FileNameFilterHandles = new Dictionary<FileDialogFileNameFilter, IntPtr>();
		private static Dictionary<IntPtr, FileDialogFileNameFilter> _HandleFileNameFilters = new Dictionary<IntPtr, FileDialogFileNameFilter>();
		internal static IntPtr CreateGTKFileChooserFilter(FileDialogFileNameFilter filter)
		{
			IntPtr hFileFilter = Internal.GTK.Methods.GtkFileFilter.gtk_file_filter_new();
			Internal.GTK.Methods.GtkFileFilter.gtk_file_filter_set_name(hFileFilter, filter.Title);
			string[] patterns = filter.Filter.Split(new char[] { ';' });
			foreach (string pattern in patterns)
			{
				string pattern2 = pattern.Trim();

				StringBuilder sbp = new StringBuilder();
				if (pattern2 != "*.*" && (pattern2.StartsWith("*.") || pattern2.StartsWith(".")))
				{
					if (pattern[0] == '*')
						pattern2 = pattern2.Substring(1);

					sbp.Append("*.");
					// now we have .xyz
					// convert it to .[xX][yY][zZ]
					if (!filter.CaseSensitive)
					{
						for (int i = 1; i < pattern2.Length; i++)
						{
							sbp.Append('[');
							sbp.Append(Char.ToLower(pattern2[i]));
							sbp.Append(Char.ToUpper(pattern2[i]));
							sbp.Append(']');
						}
					}
				}
				else
				{
					sbp.Append(pattern);
				}
				Internal.GTK.Methods.GtkFileFilter.gtk_file_filter_add_pattern(hFileFilter, sbp.ToString());
			}

			_FileNameFilterHandles[filter] = hFileFilter;
			_HandleFileNameFilters[hFileFilter] = filter;
			return hFileFilter;
		}
		internal static FileDialogFileNameFilter GetGTKFileChooserFilter(IntPtr handle)
		{
			if (_HandleFileNameFilters.ContainsKey(handle))
				return _HandleFileNameFilters[handle];
			return null;
		}
		internal static IntPtr GetHandleForGTKFileChooserFilter(FileDialogFileNameFilter handle)
		{
			if (_FileNameFilterHandles.ContainsKey(handle))
				return _FileNameFilterHandles[handle];
			return IntPtr.Zero;
		}
		internal static bool UnregisterGTKFileChooserFilter(FileDialogFileNameFilter handle)
		{
			if (_FileNameFilterHandles.ContainsKey(handle))
			{
				_HandleFileNameFilters.Remove(_FileNameFilterHandles[handle]);
				_FileNameFilterHandles.Remove(handle);
				return true;
			}
			return false;
		}
		#endregion

		protected override bool AcceptInternal()
		{
			IntPtr handle = (Handle as GTKNativeControl).Handle;
			FileDialog dlg = (Control as FileDialog);
			string[] fileNames = Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_get_filenames_managed(handle);
			foreach (string fileName in fileNames)
			{
				dlg.SelectedFileNames.Add(fileName);
			}
			return true;
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			FileDialog dlg = (dialog as FileDialog);
			string title = dlg.Text;

			Internal.GTK.Constants.GtkFileChooserAction fca = Internal.GTK.Constants.GtkFileChooserAction.Open;
			switch (dlg.Mode)
			{
				case FileDialogMode.CreateFolder:
				{
					fca = Internal.GTK.Constants.GtkFileChooserAction.CreateFolder;
					if (title == null)
						title = "Create Folder";
					break;
				}
				case FileDialogMode.Open:
				{
					fca = Internal.GTK.Constants.GtkFileChooserAction.Open;
					if (title == null)
						title = "Open";
					break;
				}
				case FileDialogMode.Save:
				{
					fca = Internal.GTK.Constants.GtkFileChooserAction.Save;
					if (title == null)
						title = "Save";
					break;
				}
				case FileDialogMode.SelectFolder:
				{
					fca = Internal.GTK.Constants.GtkFileChooserAction.SelectFolder;
					if (title == null)
						title = "Select Folder";
					break;
				}
			}

			IntPtr handle = Internal.GTK.Methods.GtkFileChooserDialog.gtk_file_chooser_dialog_new(title, (Engine as GTKEngine).CommonDialog_GetParentHandle(dlg), fca);

			// set up the file filters
			foreach (FileDialogFileNameFilter filter in dlg.FileNameFilters)
			{
				Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_add_filter(handle, CreateGTKFileChooserFilter(filter));
			}

			string accept_button = "gtk-save";
			string cancel_button = "gtk-cancel";

			switch (dlg.Mode)
			{
				case FileDialogMode.CreateFolder:
				case FileDialogMode.Save:
				{
					accept_button = (Engine as GTKEngine).StockTypeToString(StockType.Save);
					break;
				}
				case FileDialogMode.SelectFolder:
				case FileDialogMode.Open:
				{
					accept_button = (Engine as GTKEngine).StockTypeToString(StockType.Open);
					break;
				}
			}

			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					// buttons go cancel, then accept
					// gnome3 : no longer display explicitcancel button in UI
					Internal.GTK.Methods.GtkDialog.gtk_dialog_add_button(handle, cancel_button, Internal.GTK.Constants.GtkResponseType.Cancel);
					Internal.GTK.Methods.GtkDialog.gtk_dialog_add_button(handle, accept_button, Internal.GTK.Constants.GtkResponseType.Accept);
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					// buttons go accept, then cancel
					Internal.GTK.Methods.GtkDialog.gtk_dialog_add_button(handle, accept_button, Internal.GTK.Constants.GtkResponseType.Accept);
					Internal.GTK.Methods.GtkDialog.gtk_dialog_add_button(handle, cancel_button, Internal.GTK.Constants.GtkResponseType.Cancel);
					break;
				}
			}

			Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_set_select_multiple(handle, dlg.MultiSelect);
			Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_set_do_overwrite_confirmation(handle, dlg.ConfirmOverwrite);
			if (dlg.SelectedFileNames.Count > 0)
			{
				if (System.IO.File.Exists(dlg.SelectedFileNames[0]) && dlg.HighlightExistingFile)
				{
					Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_set_filename(handle, dlg.SelectedFileNames[0]);
				}
				else
				{
					Internal.GTK.Methods.GtkFileChooser.gtk_file_chooser_set_current_name(handle, dlg.SelectedFileNames[0]);
				}
			}
			return new GTKNativeControl(handle);
		}
	}
}
