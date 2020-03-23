using System;

namespace MBS.Framework.UserInterface.Dialogs
{
	public enum FileDialogMode
	{
		Open,
		Save,
		SelectFolder,
		CreateFolder
	}
	public class FileDialogFileNameFilter
	{
		public class FileDialogFileNameFilterCollection
			: System.Collections.ObjectModel.Collection<FileDialogFileNameFilter>
		{
			private Controls.FileBrowser.IFileBrowserControl _parent = null;
			public FileDialogFileNameFilterCollection(Controls.FileBrowser.IFileBrowserControl parent)
			{
				_parent = parent;
			}

			public FileDialogFileNameFilter Add(string title, string filter)
			{
				FileDialogFileNameFilter item = new FileDialogFileNameFilter();
				item.Title = title;
				item.Filter = filter;
				Add (item);
				return item;
			}

			protected override void ClearItems()
			{
				(_parent.ControlImplementation as Controls.FileBrowser.Native.IFileBrowserControlImplementation)?.ClearFileNameFilters();
				base.ClearItems();
			}
			protected override void InsertItem(int index, FileDialogFileNameFilter item)
			{
				base.InsertItem(index, item);
				(_parent.ControlImplementation as Controls.FileBrowser.Native.IFileBrowserControlImplementation)?.AddFileNameFilter(_parent, item);
			}
			protected override void RemoveItem(int index)
			{
				(_parent.ControlImplementation as Controls.FileBrowser.Native.IFileBrowserControlImplementation)?.RemoveFileNameFilter(_parent, this[index]);
				base.RemoveItem(index);
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarFilter = String.Empty;
		public string Filter { get { return mvarFilter; } set { mvarFilter = value; } }

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:MBS.Framework.UserInterface.Dialogs.FileDialogFileNameFilter"/> is case sensitive.
		/// </summary>
		/// <value><c>true</c> if case sensitive; otherwise, <c>false</c>.</value>
		public bool CaseSensitive { get; set; } = false;
	}
	public class FileDialog : CommonDialog, Controls.FileBrowser.IFileBrowserControl
	{
		public FileDialog()
		{
			mvarFileNameFilters = new FileDialogFileNameFilter.FileDialogFileNameFilterCollection(this);
		}

		private FileDialogFileNameFilter.FileDialogFileNameFilterCollection mvarFileNameFilters = null;
		public FileDialogFileNameFilter.FileDialogFileNameFilterCollection FileNameFilters { get { return mvarFileNameFilters; } }

		private FileDialogMode mvarMode = FileDialogMode.Open;
		public FileDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

		private bool mvarMultiSelect = false;
		public bool MultiSelect { get { return mvarMultiSelect; } set { mvarMultiSelect = value; } }

		private System.Collections.Generic.List<string> mvarSelectedFileNames = new System.Collections.Generic.List<string> ();
		public System.Collections.Generic.List<string> SelectedFileNames { get { return mvarSelectedFileNames; } }

		/// <summary>
		/// Determines whether existing filenames should automatically be selected in the file dialog, if the underlying platform supports this.
		/// </summary>
		/// <value><c>true</c> if highlight existing file; otherwise, <c>false</c>.</value>
		public bool HighlightExistingFile { get; set; } = true;

		public bool ConfirmOverwrite { get; set; } = true;
	}
}

