//
//  FileSystemEditor.cs - cross-platform (UWT) file system editor for Universal Editor
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
using System.Collections.Generic;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.UserInterface;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace UniversalEditor.Editors.FileSystem
{
	[ContainerLayout("~/Editors/FileSystem/FileSystemEditor.glade")]
	public class FileSystemEditor : Editor
	{
		private ListViewControl tv = null;
		private DefaultTreeModel tm = null;

		internal void ClearSelectionContent(FileSystemSelection sel)
		{
			while (tv.SelectedRows.Count > 0)
			{
				if (tv.SelectedRows[0].GetExtraData<IFileSystemObject>("item") == sel.Items[0])
				{
					tm.Rows.Remove(tv.SelectedRows[0]);
					break;
				}
			}
		}

		private void tv_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			FileSystemContextMenu_Open_Click(sender, e);
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			tv.SelectionMode = SelectionMode.Multiple;
			tv.BeforeContextMenu += tv_BeforeContextMenu;
			tv.RowActivated += tv_RowActivated;
			tv.SortContainerRowsFirst = true;

			Context.AttachCommandEventHandler("FileSystemContextMenu_Open", FileSystemContextMenu_Open_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_Add_ExistingItem", FileAddExistingItem_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_Add_ExistingFolder", FileAddExistingFolder_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_Add_FilesFromFolder", FileAddItemsFromFolder_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_New_Folder", FileNewFolder_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_Add_NewFolder", FileNewFolder_Click);
			// Application.AttachCommandEventHandler("EditDelete", ContextMenuDelete_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_Rename", ContextMenuRename_Click);
			Context.AttachCommandEventHandler("FileSystemContextMenu_CopyTo", ContextMenuCopyTo_Click);
			// Application.AttachCommandEventHandler("FileProperties", ContextMenuProperties_Click);

			// FIXME: this is GTK-specific...
			this.tv.RegisterDragSource(new DragDropTarget[]
			{
				new DragDropTarget(DragDropTargetTypes.FileList, DragDropTargetFlags.SameApplication | DragDropTargetFlags.OtherApplication, 0x0)
			}, DragDropEffect.Copy, MouseButtons.Primary | MouseButtons.Secondary, KeyboardModifierKey.None);

			this.tv.DragDropDataRequest += tv_DragDropDataRequest;

			OnObjectModelChanged(EventArgs.Empty);
		}

		private void tv_DragDropDataRequest(object sender, DragDropDataRequestEventArgs e)
		{
			if (tv.SelectedRows.Count == 0) return;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			List<string> list = new List<string>();
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				IFileSystemObject fso = tv.SelectedRows[i].GetExtraData<IFileSystemObject>("item");
				if (fso is File)
				{
					string wTmpFile = TemporaryFileManager.CreateTemporaryFile(tv.SelectedRows[i].RowColumns[0].Value.ToString(), (fso as File).GetData());
					list.Add(String.Format("file://{0}", wTmpFile));
				}
			}
			e.Data = list.ToArray();
		}

		/// <summary>
		/// Prompts the user to select an existing file to add to the currently-loaded <see cref="FileSystemObjectModel"/>.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void FileAddExistingItem_Click(object sender, EventArgs e)
		{
			FileSystemObjectModel fsom = ObjectModel as FileSystemObjectModel;
			if (fsom == null)
				return;

			FileDialog fd = new FileDialog();
			fd.Mode = FileDialogMode.Open;
			fd.MultiSelect = true;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();

				foreach (string fileName in fd.SelectedFileNames)
				{
					string fileTitle = System.IO.Path.GetFileName(fileName);
					byte[] data = System.IO.File.ReadAllBytes(fileName);

					UIAddExistingFile(fsom, fileTitle, data);
				}

				EndEdit();
			}
		}

		private Folder UIAddEmptyFolder(IFileSystemContainer fsom, string fileTitle)
		{
			Folder f = new Folder();
			f.Name = fileTitle;
			DateTime now = DateTime.Now;
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], f.Name),
				new TreeModelRowColumn(tm.Columns[1], String.Format("{0} items", (f.Files.Count + f.Folders.Count))),
				new TreeModelRowColumn(tm.Columns[2], "Folder"),
				new TreeModelRowColumn(tm.Columns[3], now.ToString())
			});
			row.RowColumns[1].RawValue = (f.Folders.Count + f.Files.Count);
			row.RowColumns[3].RawValue = now.ToBinary();
			row.SetExtraData<IFileSystemObject>("item", f);

			if (tv.SelectedRows.Count > 0 && tv.LastHitTest.Row != null)
			{
				for (int i = 0; i < tv.SelectedRows.Count; i++)
				{
					IFileSystemObject item = tv.SelectedRows[i].GetExtraData<IFileSystemObject>("item");
					AddFolderToItem(f, item);
					tv.SelectedRows[i].Rows.Add(row);
				}
			}
			else
			{
				AddFolderToItem(f, null);
				tm.Rows.Add(row);
			}
			return f;
		}
		private File UIAddExistingFile(IFileSystemContainer fsom, string fileTitle, byte[] data)
		{
			File f = fsom.AddFile(fileTitle, data);
			TreeModelRow row = UIGetTreeModelRowForFileSystemObject(f);
			row.SetExtraData<IFileSystemObject>("item", f);
			tm.Rows.Add(row);
			return f;
		}

		private void FileSystemContextMenu_Open_Click(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count < 1)
				return;

			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				IFileSystemObject fso = tv.SelectedRows[i].GetExtraData<IFileSystemObject>("item");
				if (fso is File)
				{
					File f = (fso as File);

					EmbeddedFileAccessor ma = new EmbeddedFileAccessor(f);
					Document doc = new Document(ma);
					HostApplication.CurrentWindow.OpenFile(doc);
				}
			}
		}

		/// <summary>
		/// Creates a new folder in the current directory of the currently-loaded <see cref="FileSystemObjectModel"/>.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void FileNewFolder_Click(object sender, EventArgs e)
		{
			FileSystemObjectModel fsom = ObjectModel as FileSystemObjectModel;
			if (fsom == null)
				return;

			int iNewFolderCt = 0;
			foreach (Folder ef in fsom.Folders)
			{
				if (ef.Name.Equals("New folder") || ef.Name.StartsWith("New folder "))
				{
					iNewFolderCt++;
				}
			}

			UIAddEmptyFolder(fsom, String.Format("New folder{0}", ((iNewFolderCt > 0) ? " (" + (iNewFolderCt + 1).ToString() + ")" : String.Empty)));
		}

		private void FileAddItemsFromFolder_Click(object sender, EventArgs e)
		{
			FileDialog fd = new FileDialog();
			fd.Mode = FileDialogMode.SelectFolder;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				Folder f = FolderFromPath(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1]);
				IFileSystemObject[] files = f.GetContents();

				BeginEdit();
				foreach (IFileSystemObject fso in files)
				{
					if (fso is File)
					{
						RecursiveAddFile(fso as File);
						if (CurrentFolder != null)
						{
							CurrentFolder.Files.Add(fso as File);
						}
						else
						{
							(ObjectModel as FileSystemObjectModel).Files.Add(fso as File);
						}
					}
					else if (fso is Folder)
					{
						RecursiveAddFolder(fso as Folder);
						if (CurrentFolder != null)
						{
							CurrentFolder.Folders.Add(fso as Folder);
						}
						else
						{
							(ObjectModel as FileSystemObjectModel).Folders.Add(fso as Folder);
						}
					}
				}
				EndEdit();
			}
		}

		private void AddFolderToItem(Folder f, IFileSystemObject item)
		{
			FileSystemObjectModel fsom = ObjectModel as FileSystemObjectModel;
			if (fsom == null)
				return;

			BeginEdit();
			if (item is Folder)
			{
				(item as Folder).Folders.Add(f);
			}
			else if (item == null)
			{
				fsom.Folders.Add(f);
			}
			EndEdit();
		}

		private void FileAddExistingFolder_Click(object sender, EventArgs e)
		{
			FileSystemObjectModel fsom = ObjectModel as FileSystemObjectModel;
			if (fsom == null)
				return;

			FileDialog fd = new FileDialog();
			fd.Mode = FileDialogMode.SelectFolder;
			if (fd.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();

				Folder f = FolderFromPath(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1]);
				RecursiveAddFolder(f);
				AddFolderToItem(f, null);

				EndEdit();
			}
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return null;

			if (content is string)
			{
				string str = (string)content;

				// FIXME: assuming nautilus, for now, PLEASE FIX THIS
				string[] parts = str.Split(new char[] { '\n' });
				if (parts[0] == "x-special/nautilus-clipboard")
				{
					if (parts[1] == "cut" || parts[1] == "copy")
					{
						List<IFileSystemObject> fileList = new List<IFileSystemObject>();
						for (int ip = 2; ip < parts.Length - 1; ip++)
						{
							string url = parts[ip];
							Uri uri = new Uri(url);

							string filepath = uri.LocalPath;

							IFileSystemObject fso = FileSystemObjectFromPath(filepath);
							if (fso == null)
								continue;

							TreeModelRow row = UIGetTreeModelRowForFileSystemObject(fso);
							tm.Rows.Add(row);

							fileList.Add(fso);
						}
						return new FileSystemSelection(this, fileList.ToArray());
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Creates and returns a new <see cref="TreeModelRow" /> containing details about the specified <see cref="IFileSystemObject" />.
		/// </summary>
		/// <returns>The et tree model row for file system object.</returns>
		/// <param name="fso">Fso.</param>
		private TreeModelRow UIGetTreeModelRowForFileSystemObject(IFileSystemObject fso, bool recurse = true)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			TreeModelRow r = null;
			if (fso is Folder)
			{
				Folder f = (fso as Folder);
				r = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], f.Name),
					new TreeModelRowColumn(tm.Columns[1], (f.Folders.Count + f.Files.Count).ToString() + " items"),
					new TreeModelRowColumn(tm.Columns[2], "Folder"),
					new TreeModelRowColumn(tm.Columns[3], "")
				});
				r.RowColumns[1].RawValue = (long)(f.Folders.Count + f.Files.Count);
				r.RowColumns[3].RawValue = (long)0;

				if (recurse)
				{
					foreach (Folder folder in f.Folders)
					{
						TreeModelRow r2 = UIGetTreeModelRowForFileSystemObject(folder);
						r.Rows.Add(r2);
					}
					foreach (File file in f.Files)
					{
						TreeModelRow r2 = UIGetTreeModelRowForFileSystemObject(file);
						r.Rows.Add(r2);
					}
				}
			}
			else if (fso is File)
			{
				File f = (fso as File);
				r = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tm.Columns[0], f.Name),
					new TreeModelRowColumn(tm.Columns[1], UserInterface.Common.FileInfo.FormatSize(f.Size)),
					new TreeModelRowColumn(tm.Columns[2], "File"),
					new TreeModelRowColumn(tm.Columns[3], f.ModificationTimestamp.ToString())
				});
				r.RowColumns[1].RawValue = f.Size;
				r.RowColumns[3].RawValue = f.ModificationTimestamp.ToBinary();

				for (int i = 0; i < fsom.AdditionalDetails.Count; i++)
				{
					r.RowColumns.Add(new TreeModelRowColumn(tm.Columns[4 + i], f.GetAdditionalDetail(fsom.AdditionalDetails[i].Name)));
				}
			}
			r.SetExtraData<IFileSystemObject>("item", fso);
			return r;
		}

		/// <summary>
		/// Creates a <see cref="Folder" /> or a <see cref="File" /> from the specified path to a folder or a file.
		/// </summary>
		/// <returns>The created <see cref="IFileSystemObject" />.</returns>
		/// <param name="filepath">The path in the actual file system that contains the object to load.</param>
		private IFileSystemObject FileSystemObjectFromPath(string filepath)
		{
			if (System.IO.Directory.Exists(filepath))
			{
				return FolderFromPath(filepath);
			}
			else if (System.IO.File.Exists(filepath))
			{
				return FileFromPath(filepath);
			}
			return null;
		}

		private File FileFromPath(string filepath)
		{
			File file = new File();
			file.Name = System.IO.Path.GetFileName(filepath);
			file.SetData(System.IO.File.ReadAllBytes(filepath));
			return file;
		}
		private Folder FolderFromPath(string filepath)
		{
			Folder f = new Folder();
			f.Name = System.IO.Path.GetFileName(filepath);

			string[] folders = System.IO.Directory.GetDirectories(filepath);
			foreach (string folder in folders)
			{
				f.Folders.Add(FolderFromPath(folder));
			}
			string[] files = System.IO.Directory.GetFiles(filepath);
			foreach (string file in files)
			{
				f.Files.Add(FileFromPath(file));
			}
			return f;
		}

		public override void UpdateSelections()
		{
			Selections.Clear();
			for (int i = 0; i < tv.SelectedRows.Count; i++)
			{
				TreeModelRow row = tv.SelectedRows[i];
				if (row == null) continue;

				Selections.Add(new FileSystemSelection(this, row.GetExtraData<IFileSystemObject>("item")));
			}
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.KeyBindings.Add(new KeyBinding("FileSystemContextMenu_CopyTo", KeyboardKey.F5, KeyboardModifierKey.None));
				_er.KeyBindings.Add(new KeyBinding("FileSystemContextMenu_Rename", KeyboardKey.F6, KeyboardModifierKey.None));
				_er.KeyBindings.Add(new KeyBinding("FileSystemContextMenu_New_Folder", KeyboardKey.F7, KeyboardModifierKey.None));
				_er.KeyBindings.Add(new KeyBinding("EditDelete", KeyboardKey.F8, KeyboardModifierKey.None));

				_er.SupportedObjectModels.Add(typeof(FileSystemObjectModel));
			}
			return _er;
		}

		private Folder _CurrentFolder = null;
		public Folder CurrentFolder
		{
			get { return _CurrentFolder; }
			set
			{
				bool changed = (_CurrentFolder != value);
				_CurrentFolder = value;

				if (!changed) return;

				UpdateList();
			}
		}

		private void UpdateList()
		{
			tm.Rows.Clear();

			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			for (int i = 4; i < tv.Columns.Count; i++)
			{
				tv.Columns.Remove(tv.Columns[i]);
			}
			for (int i = 0; i < fsom.AdditionalDetails.Count; i++)
			{
				tm.Columns.Add(new TreeModelColumn(typeof(string)));
				tv.Columns.Add(new ListViewColumnText(tm.Columns[tm.Columns.Count - 1], fsom.AdditionalDetails[i].Title));
			}
			tv.Model = tm;

			if (_CurrentFolder == null)
			{
				foreach (Folder f in fsom.Folders)
				{
					RecursiveAddFolder(f, null);
				}
				foreach (File f in fsom.Files)
				{
					RecursiveAddFile(f, null);
				}
			}
			else
			{
				foreach (Folder f in _CurrentFolder.Folders)
				{
					RecursiveAddFolder(f, null);
				}
				foreach (File f in _CurrentFolder.Files)
				{
					RecursiveAddFile(f, null);
				}
			}
		}

		private void RecursiveAddFolderDExplore(Folder f, EditorDocumentExplorerNode parent)
		{
			EditorDocumentExplorerNode dexpnode1 = new EditorDocumentExplorerNode(f.Name);
			dexpnode1.SetExtraData("item", f);
			foreach (Folder f1 in f.Folders)
			{
				RecursiveAddFolderDExplore(f1, dexpnode1);
			}
			if (parent == null)
			{
				DocumentExplorer.Nodes.Add(dexpnode1);
			}
			else
			{
				parent.Nodes.Add(dexpnode1);
			}
		}

		protected internal override void OnDocumentExplorerSelectionChanged(EditorDocumentExplorerSelectionChangedEventArgs e)
		{
			base.OnDocumentExplorerSelectionChanged(e);

			if (e.Node == null)
			{
				CurrentFolder = null;
				return;
			}

			Folder item = e.Node.GetExtraData<Folder>("item");
			CurrentFolder = item;
		}

		private void RecursiveAddFolder(Folder f, TreeModelRow parent = null)
		{
			TreeModelRow r = UIGetTreeModelRowForFileSystemObject(f, false);

			foreach (Folder f2 in f.Folders)
			{
				RecursiveAddFolder(f2, r);
			}
			foreach (File f2 in f.Files)
			{
				RecursiveAddFile(f2, r);
			}

			if (parent == null)
			{
				tm.Rows.Add(r);
			}
			else
			{
				parent.Rows.Add(r);
			}
		}
		private void RecursiveAddFile(File f, TreeModelRow parent = null)
		{
			TreeModelRow r = UIGetTreeModelRowForFileSystemObject(f);
			r.SetExtraData<IFileSystemObject>("item", f);

			if (parent == null)
			{
				tm.Rows.Add(r);
			}
			else
			{
				parent.Rows.Add(r);
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			DocumentExplorer.Nodes.Clear();

			if (!IsCreated) return;

			for (int i = 4; i < tm.Columns.Count; i++)
			{
				tm.Columns.RemoveAt(i);
			}

			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			// add folders to Document Explorer window ONCE PER DOCUMENT
			foreach (Folder f in fsom.Folders)
			{
				RecursiveAddFolderDExplore(f, null);
			}

			UpdateList();
		}

		/// <summary>
		/// Prompts the user to choose a filename, and then extracts the selected file in the current <see cref="FileSystemObjectModel" /> to a file on disk with the chosen filename.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void ContextMenuCopyTo_Click(object sender, EventArgs e)
		{
			// extract files
			if (tv.SelectedRows.Count == 1)
			{
				UIExtractFileSystemObject(tv.SelectedRows[0].GetExtraData<IFileSystemObject>("item"));
			}
			else if (tv.SelectedRows.Count > 1)
			{
				FileDialog fd = new FileDialog();
				fd.Mode = FileDialogMode.SelectFolder;
				fd.MultiSelect = false;

				if (fd.ShowDialog() == DialogResult.OK)
				{
					string fileName = fd.SelectedFileNames[0];

					if (!System.IO.Directory.Exists(fileName))
						System.IO.Directory.CreateDirectory(fileName);

					foreach (TreeModelRow row in tv.SelectedRows)
					{
						IFileSystemObject fso = row.GetExtraData<IFileSystemObject>("item");
						ExtractFileSystemObject(fso, fileName);
					}
				}
			}
		}

		private void ContextMenuRename_Click(object sender, EventArgs e)
		{
			// gtk_tree_view_column_focus_cell (GtkTreeViewColumn *tree_column, GtkCellRenderer* cell);
			// tv.SelectedRows[0].RowColumns[0].BeginEdit();
		}

		private void ExtractFileSystemObject(IFileSystemObject fso, string fileName)
		{
			if (String.IsNullOrEmpty(fso.Name))
			{
				Console.Error.WriteLine("ERROR! FileSystemEditor::ExtractFileSystemObject - we have to work around some weirdo bug in ZIP");
				return;
			}

			if (fso is File)
			{
				File f = (fso as File);

				string filePath = System.IO.Path.Combine(new string[] { fileName, System.IO.Path.GetFileName(f.Name) });
				System.IO.File.WriteAllBytes(filePath, f.GetData());
			}
			else if (fso is Folder)
			{
				Folder f = (fso as Folder);

				string filePath = System.IO.Path.Combine(new string[] { fileName, f.Name });
				if (!System.IO.Directory.Exists(filePath))
					System.IO.Directory.CreateDirectory(filePath);

				foreach (File file in f.Files)
				{
					ExtractFileSystemObject(file, filePath);
				}
				foreach (Folder file in f.Folders)
				{
					ExtractFileSystemObject(file, filePath);
				}
			}
		}

		private void UIExtractFileSystemObject(IFileSystemObject fso)
		{
			FileDialog fd = new FileDialog();
			if (fso is File)
			{
				File f = (fso as File);
				/*
				if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + f.Name))
				{
					fd.SelectedFileNames.Add(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar.ToString() + f.Name);
				}
				else
				{
				*/
				fd.SelectedFileNames.Add(f.Name);
				//}
				fd.Mode = FileDialogMode.Save;
				fd.MultiSelect = false;
				if (fd.ShowDialog() == DialogResult.OK)
				{
					System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1], f.GetData());
				}
			}
			else if (fso is Folder)
			{
				Folder f = (fso as Folder);
				fd.SelectedFileNames.Add(f.Name);
				fd.Mode = FileDialogMode.CreateFolder;
				fd.MultiSelect = false;
				if (fd.ShowDialog() == DialogResult.OK)
				{
					ExtractFileSystemObject(f, fd.SelectedFileNames[fd.SelectedFileNames.Count - 1]);
				}
			}
		}

		void tv_BeforeContextMenu(object sender, EventArgs e)
		{
			TreeModelRow row = null;
			if (e is MouseEventArgs)
			{
				MouseEventArgs ee = (e as MouseEventArgs);
				ListViewHitTestInfo info = tv.HitTest(ee.X, ee.Y);
				if (info != null)
					row = info.Row;
			}

			if (row != null)
			{
				if (row.GetExtraData<IFileSystemObject>("item") is Folder)
				{
					tv.ContextMenuCommandID = "FileSystemContextMenu_Selected_Folder";
				}
				else
				{
					tv.ContextMenuCommandID = "FileSystemContextMenu_Selected_File";
				}
			}
			else
			{
				tv.ContextMenuCommandID = "FileSystemContextMenu_Unselected";
			}

			Application.Commands["EditPaste"].Enabled = Clipboard.Default.ContainsFileList;
			// (tv.ContextMenu.Items["FileSystemContextMenu_PasteShortcut"] as CommandMenuItem).Enabled = Clipboard.Default.ContainsFileList;
		}

		protected override SettingsProvider[] GetDocumentPropertiesSettingsProviders()
		{
			List<SettingsProvider> list = new List<SettingsProvider>();

			FileSystemEditorDocumentPropertiesSettingsProvider docprops = new FileSystemEditorDocumentPropertiesSettingsProvider();

			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			docprops.ObjectModel = fsom;

			if (tv.Focused)
			{
				if (tv.LastHitTest != null)
				{
					if (tv.LastHitTest.Row != null)
					{
						docprops.FileSystemObject = tv.LastHitTest.Row.GetExtraData<IFileSystemObject>("item");
					}
				}
			}
			list.Add(docprops);

			return list.ToArray();
		}
	}
}
