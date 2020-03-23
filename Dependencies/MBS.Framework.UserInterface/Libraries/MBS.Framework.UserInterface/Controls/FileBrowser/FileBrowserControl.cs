//
//  FileBrowserControl.cs
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

namespace MBS.Framework.UserInterface.Controls.FileBrowser
{
	namespace Native
	{
		public interface IFileBrowserControlImplementation
		{
			void UpdateSelectedFileNames (System.Collections.Generic.List<string> coll);

			void ClearFileNameFilters();
			void AddFileNameFilter(IFileBrowserControl control, Dialogs.FileDialogFileNameFilter filter);
			void RemoveFileNameFilter(IFileBrowserControl control, Dialogs.FileDialogFileNameFilter filter);
		}
	}
	public class FileBrowserControl : Control, IFileBrowserControl
	{
		public event EventHandler ItemActivated;
		protected virtual void OnItemActivated (EventArgs e)
		{
			ItemActivated?.Invoke (this, e);
		}

		public FileBrowserControl()
		{
			FileNameFilters = new Dialogs.FileDialogFileNameFilter.FileDialogFileNameFilterCollection(this);
		}

		public Dialogs.FileDialogFileNameFilter.FileDialogFileNameFilterCollection FileNameFilters { get; } = null;
		public FileBrowserMode Mode { get; set; }

		public System.Collections.ObjectModel.ReadOnlyCollection<string> SelectedFileNames {
			get {
				System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string> ();
				Native.IFileBrowserControlImplementation impl = (ControlImplementation as Native.IFileBrowserControlImplementation);
				if (impl != null) {
					impl.UpdateSelectedFileNames (list);
				}
				return new System.Collections.ObjectModel.ReadOnlyCollection<string>(list);
			}
		}
	}
}

