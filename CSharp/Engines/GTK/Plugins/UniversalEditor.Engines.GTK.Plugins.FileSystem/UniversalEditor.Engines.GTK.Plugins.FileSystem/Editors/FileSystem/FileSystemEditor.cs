using System;
using UniversalEditor.Engines.GTK;

using UniversalEditor;
using UniversalEditor.UserInterface;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Editors.FileSystem
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class FileSystemEditor : Editor
	{
		public FileSystemEditor ()
		{
			this.Build ();
			base.SupportedObjectModels.Add (typeof(FileSystemObjectModel));
			
			tvFolders.AppendColumn (new Gtk.TreeViewColumn("Name", new Gtk.CellRendererText(), "text", 1));
			tvFolders.HeadersVisible = false;
			
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Name", new Gtk.CellRendererText(), "text", 1));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Size", new Gtk.CellRendererText(), "text", 2));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Type", new Gtk.CellRendererText()));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Date Modified", new Gtk.CellRendererText()));
		}
		
		private Folder mvarCurrentFolder = null;
		private void RefreshListView()
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			Gtk.TreeStore tsFolders = new Gtk.TreeStore(typeof(Folder), typeof(string));
			foreach (Folder folder in fsom.Folders)
			{
				RecursiveAppendFolderToTreeStore(folder, ref tsFolders);
			}
			tvFolders.Model = tsFolders;
			
			File.FileCollection fileColl = null;
			if (mvarCurrentFolder == null)
			{
				fileColl = fsom.Files;
			}
			else
			{
				fileColl = mvarCurrentFolder.Files;
			}
			
			Gtk.TreeStore ts = new Gtk.TreeStore(typeof(File), typeof(string), typeof(long));
			foreach (File file in fileColl)
			{
				ts.AppendValues(file, file.Name, file.Size);
			}
			tvFiles.Model = ts;
		}

		private void RecursiveAppendFolderToTreeStore (Folder folder, ref Gtk.TreeStore ts)
		{
			Gtk.TreeIter ti = ts.AppendValues(folder, folder.Name);
			foreach (Folder folder1 in folder.Folders)
			{
				RecursiveAppendFolderToTreeStore(folder1, ref ts, ti);
			}
		}
		private void RecursiveAppendFolderToTreeStore (Folder folder, ref Gtk.TreeStore ts, Gtk.TreeIter tiParent)
		{
			Gtk.TreeIter ti = ts.AppendValues(tiParent, folder, folder.Name);
			foreach (Folder folder1 in folder.Folders)
			{
				RecursiveAppendFolderToTreeStore(folder1, ref ts, ti);
			}
		}		
		protected override void OnObjectModelChanged (EventArgs e)
		{
			base.OnObjectModelChanged(e);
			
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			RefreshListView();
		}
	}
}

