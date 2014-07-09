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
			
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Name", new Gtk.CellRendererText(), 1));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Size", new Gtk.CellRendererText(), 2));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Type", new Gtk.CellRendererText()));
			tvFiles.AppendColumn(new Gtk.TreeViewColumn("Date Modified", new Gtk.CellRendererText()));
		}
		
		private Folder mvarCurrentFolder = null;
		private void RefreshListView()
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
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
		
		protected override void OnObjectModelChanged (EventArgs e)
		{
			base.OnObjectModelChanged(e);
			
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;
			
			RefreshListView();
		}
	}
}

