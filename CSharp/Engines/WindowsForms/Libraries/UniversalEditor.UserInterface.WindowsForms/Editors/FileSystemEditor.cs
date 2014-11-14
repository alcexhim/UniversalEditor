using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.Dialogs.FileSystem;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms.Editors
{
	public partial class FileSystemEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.ID = new Guid("{1B5B1E8D-442A-4AC0-8EFD-03AADFF3CAD2}");
				_er.Title = "File system/archive";
				_er.SupportedObjectModels.Add(typeof(FileSystemObjectModel));
			}
			return _er;
		}

		public FileSystemEditor()
		{
			InitializeComponent();

			ImageList large = base.LargeImageList;
			ImageList small = base.SmallImageList;
			IconMethods.PopulateSystemIcons(ref large);
			IconMethods.PopulateSystemIcons(ref small);

			lv.LargeImageList = base.LargeImageList;
			lv.SmallImageList = base.SmallImageList;
			tv.ImageList = base.SmallImageList;

			mnuTreeViewContextExpand.Font = new Font(mnuTreeViewContextExpand.Font, FontStyle.Bold);

			lv.Columns.Add("Name", 300);
			lv.Columns.Add("Size", 100);
			lv.Columns.Add("Type", 100);
			lv.Columns.Add("Date Modified", 200);
			lv.Columns.Add("Comment", 200);

			ActionMenuItem mnuFileSystem = MenuBar.Items.Add("mnuFileSystem", "File&system", null, 4);
			mnuFileSystem.Items.Add("mnuFileSystemAddFile", "Add &File...", AddFile_Click);
			mnuFileSystem.Items.Add("mnuFileSystemAddFolder", "Add Fol&der...", AddFolder_Click);
			mnuFileSystem.Items.AddSeparator();
			mnuFileSystem.Items.Add("mnuFileSystemUndelete", "&Undelete");
			mnuFileSystem.Items.AddSeparator();
			mnuFileSystem.Items.Add("mnuFileSystemExtractAll", "E&xtract All...", tsbExtract_Click);
			mnuFileSystem.Items.AddSeparator();
			mnuFileSystem.Items.Add("mnuFileSystemComment", "Com&ment...", Comment_Click);

			Toolbar tbFileSystem = Toolbars.Add("tbFileSystem", "Filesystem");
			tbFileSystem.Items.Add(mnuFileSystem.Items["mnuFileSystemAddFile"]);
			tbFileSystem.Items.Add(mnuFileSystem.Items["mnuFileSystemAddFolder"]);
			tbFileSystem.Items.AddSeparator();
			tbFileSystem.Items.Add(mnuFileSystem.Items["mnuFileSystemUndelete"]);
			tbFileSystem.Items.AddSeparator();
			tbFileSystem.Items.Add(mnuFileSystem.Items["mnuFileSystemExtractAll"]);
			tbFileSystem.Items.AddSeparator();
			tbFileSystem.Items.Add(mnuFileSystem.Items["mnuFileSystemComment"]);
		}

		private void AddFolder_Click(object sender, EventArgs e)
		{
		}

		private void Comment_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count > 0)
			{
				CommentDialog dlg = new CommentDialog();
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string comment = dlg.txtComment.Text;
					foreach (AwesomeControls.ListView.ListViewItem lvi in lv.SelectedItems)
					{
						File file = (lvi.Data as File);
						if (file != null)
						{
							file.Description = comment;
							lvi.Details[3] = new AwesomeControls.ListView.ListViewDetailLabel(file.Description);
						}
					}
				}
			}
		}

		private string mvarTitle = "File system";
		public override string Title { get { return mvarTitle; } }

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			TemporaryFileManager.RegisterTemporaryDirectory("~u", 8);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			if (tIconLoader.IsAlive)
			{
				tIconLoader.Abort();
			}
			TemporaryFileManager.UnregisterTemporaryDirectory();
		}

		private System.Threading.Thread tIconLoader = null;
		private void tIconLoader_ThreadStart()
		{
			try
			{
				foreach (AwesomeControls.ListView.ListViewItem lvi in lv.Items)
				{
					File file = (lvi.Data as File);
					if (file == null) continue;

					byte[] data = null;
					try
					{
						data = file.GetDataAsByteArray();
						if (data == null) continue;

						ObjectModel picture = UniversalEditor.Common.Reflection.GetAvailableObjectModel(data, file.Name, "UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel");
						UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel pic = (picture as UniversalEditor.ObjectModels.Multimedia.Picture.PictureObjectModel);
						if (pic != null)
						{
							// System.Reflection.MethodInfo miToBitmap = picture.GetType().GetMethod("ToBitmap", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
							Bitmap bitmap = pic.ToBitmap(); // (miToBitmap.Invoke(picture, null) as Bitmap);
							if (bitmap != null) lvi.Image = bitmap;
							//lv.Invoke(new Action<AwesomeControls.ListView.ListViewItem>(InvalidateItem), lvi);
						}
					}
					catch
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void InvalidateItem(AwesomeControls.ListView.ListViewItem lvi)
		{
			lv.Invalidate(lv.GetItemBounds(lvi));
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (tIconLoader != null)
			{
				tIconLoader.Abort();
			}
			tIconLoader = new System.Threading.Thread(tIconLoader_ThreadStart);

			lv.Items.Clear();
			tv.Nodes.Clear();

			TreeNode tnRoot = new TreeNode();
			tnRoot.Text = "<ROOT>";
			tv.Nodes.Add(tnRoot);

			tv.SelectedNode = tnRoot;
			tnRoot.EnsureVisible();

			FileSystemObjectModel fsom = (base.ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			foreach (Folder folder in fsom.Folders)
			{
				RecursiveLoadFolder(folder, tnRoot);
			}

			if (ObjectModel.Accessor != null)
			{
				FileAccessor file = (ObjectModel.Accessor as FileAccessor);
				if (file != null) tv.Nodes[0].Text = System.IO.Path.GetFileName(file.FileName);
			}
			UpdateListView();

			tv.EndUpdate();
		}

		private void RecursiveLoadFile(File file, AwesomeControls.ListView.ListViewItem parent)
		{
			AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
			if (!String.IsNullOrEmpty(txtFilter.Text) && (file.Name.Contains(txtFilter.Text) || !file.Name.Match(txtFilter.Text))) return;

			lvi.Data = file;
			lvi.ImageKey = "generic-file";
			lvi.Text = file.Name;

			string unit = "bytes";
			double size = (double)file.Size;
			if (size >= 1024)
			{
				size /= 1024;
				size = Math.Round(size, 2);
				unit = "KB";

				if (size >= 1024)
				{
					size /= 1024;
					size = Math.Round(size, 2);
					unit = "MB";

					if (size >= 1024)
					{
						size /= 1024;
						size = Math.Round(size, 2);
						unit = "GB";

						if (size >= 1024)
						{
							size /= 1024;
							size = Math.Round(size, 2);
							unit = "TB";
						}
					}
				}
			}

			lvi.Details.Add(size.ToString() + " " + unit);
			lvi.Details.Add("File");
			lvi.Details.Add(file.ModificationTimestamp.ToString());

			if (parent != null)
			{
				parent.Items.Add(lvi);
			}
			else
			{
				lv.Items.Add(lvi);
			}
		}

		private void RecursiveLoadFolder(Folder folder, TreeNode parent)
		{
			TreeNode tn = new TreeNode();
			tn.Text = folder.Name;
			tn.ImageKey = "generic-folder-closed";
			tn.SelectedImageKey = "generic-folder-closed";
			tn.Tag = folder;
			foreach (Folder folder1 in folder.Folders)
			{
				RecursiveLoadFolder(folder1, tn);
			}
			if (parent == null)
			{
				tv.Nodes.Add(tn);
			}
			else
			{
				parent.Nodes.Add(tn);
			}
		}

		private void RecursiveLoadListViewFolder(Folder folder, AwesomeControls.ListView.ListViewItem parent)
		{
			if (!String.IsNullOrEmpty(txtFilter.Text) && (folder.Name.ToLower().Contains(txtFilter.Text.ToLower()) || !folder.Name.ToLower().Match(txtFilter.Text.ToLower()))) return;

			AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
			lvi.Text = folder.Name;
			lvi.ImageKey = "generic-folder-closed";
			lvi.Data = folder;
			foreach (Folder folder1 in folder.Folders)
			{
				RecursiveLoadListViewFolder(folder1, lvi);
			}
			foreach (File file1 in folder.Files)
			{
				RecursiveLoadFile(file1, lvi);
			}

			if (parent != null)
			{
				parent.Items.Add(lvi);
			}
			else
			{
				lv.Items.Add(lvi);
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			mvarCurrentFolder = (tv.SelectedNode.Tag as Folder);
			UpdateListView();
			lv.Refresh();
		}

		private Folder mvarCurrentFolder = null;

		private void UpdateListView()
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			lv.BeginUpdate();

			lv.Items.Clear();

			if (tIconLoader.ThreadState == System.Threading.ThreadState.Running)
			{
				tIconLoader.Abort();
				tIconLoader = null;
			}
			tIconLoader = new System.Threading.Thread(tIconLoader_ThreadStart);

			if (mvarCurrentFolder != null)
			{
				Folder folder = mvarCurrentFolder;
				foreach (Folder folder1 in folder.Folders)
				{
					RecursiveLoadListViewFolder(folder1, null);
				}
				foreach (File file in folder.Files)
				{
					RecursiveLoadFile(file, null);
				}
			}
			else
			{
				foreach (Folder folder1 in fsom.Folders)
				{
					RecursiveLoadListViewFolder(folder1, null);
				}
				foreach (File file in fsom.Files)
				{
					RecursiveLoadFile(file, null);
				}
			}
			lv.EndUpdate();

			RecursiveUpdateTreeView();

			if (tIconLoader != null)
			{
				tIconLoader.Abort();
				tIconLoader = new System.Threading.Thread(tIconLoader_ThreadStart);
			}
			tIconLoader.Start();
		}

		private void RecursiveUpdateTreeView(TreeNode parent = null)
		{
			if (parent == null)
			{
				foreach (TreeNode tn in tv.Nodes)
				{
					RecursiveUpdateTreeView(tn);
				}
			}
			else
			{
				if ((parent.Tag == null && mvarCurrentFolder == null) || (parent.Tag == mvarCurrentFolder))
				{
					tv.SelectedNode = parent;
				}

				foreach (TreeNode tn in parent.Nodes)
				{
					RecursiveUpdateTreeView(tn);
				}
			}
		}

		private void lv_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void mnuListViewContextView_Click(object sender, EventArgs e)
		{
			#region Reset all the other items
			{
				foreach (ToolStripMenuItem tsmi in mnuListViewContextView.DropDownItems)
				{
					if (tsmi != sender)
					{
						tsmi.Checked = false;
					}
				}
			}
			#endregion

			if (mnuListViewContextViewExtraLargeIcons.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.ExtraLargeIcons;
			}
			else if (mnuListViewContextViewLargeIcons.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.LargeIcons;
			}
			else if (mnuListViewContextViewMediumIcons.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.MediumIcons;
			}
			else if (mnuListViewContextViewSmallIcons.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.SmallIcons;
			}
			else if (mnuListViewContextViewList.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.List;
			}
			else if (mnuListViewContextViewDetails.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.Details;
			}
			else if (mnuListViewContextViewTiles.Checked)
			{
				lv.Mode = AwesomeControls.ListView.ListViewMode.Tiles;
			}
		}

		private void lv_ItemDrag(object sender, AwesomeControls.ListView.ListViewItemDragEventArgs e)
		{
			List<string> filePaths = new List<string>();
			foreach (AwesomeControls.ListView.ListViewItem lvi in lv.SelectedItems)
			{
				File file = (lvi.Data as File);
				if (file != null)
				{
					byte[] data = file.GetDataAsByteArray();

					if (String.IsNullOrEmpty(file.Name))
					{
						file.Name = "[]";
					}
					string filePath = TemporaryFileManager.CreateTemporaryFile(file.Name, data);
					filePaths.Add(filePath);

					file.Properties["tempfile"] = filePath;
				}
			}
			if (filePaths.Count > 0)
			{
				DataObject dobj = new DataObject("FileDrop", filePaths.ToArray());
				dobj.SetData(typeof(AwesomeControls.ListView.ListViewItem.ListViewItemCollection), lv.SelectedItems);

				e.DataObject = dobj;
				
				e.Effects = DragDropEffects.Copy | DragDropEffects.Move;
			}
		}

		private void lv_ItemDragComplete(object sender, AwesomeControls.ListView.ListViewItemDragEventArgs e)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);

			DataObject dobj = (e.DataObject as DataObject);
			AwesomeControls.ListView.ListViewItem.ListViewItemCollection items = (AwesomeControls.ListView.ListViewItem.ListViewItemCollection)e.DataObject.GetData(typeof(AwesomeControls.ListView.ListViewItem.ListViewItemCollection));
			foreach (AwesomeControls.ListView.ListViewItem lvi in items)
			{
				File file = (lvi.Data as File);
				if (!System.IO.File.Exists(file.Properties["tempfile"].ToString()))
				{
					// delete the file from the archive
					BeginEdit();

					fsom.Files.Remove(file);
					lv.Items.Remove(lvi);

					EndEdit();
				}
			}
		}

		private void AddFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Add Files to File System";
			ofd.Filter = "All files (*.*)|*.*";
			ofd.Multiselect = true;
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				foreach (string filename in ofd.FileNames)
				{
					AddFileToArchive(filename);
				}
			}
		}

		private void AddFolderToArchive(string filename = "", Folder parent = null)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			if (parent == null)
			{
				if (tv.SelectedNode != null)
				{
					if (tv.SelectedNode.Tag is Folder) parent = (tv.SelectedNode.Tag as Folder);
				}
			}

			Folder folder = new Folder();
			folder.Name = System.IO.Path.GetFileName(filename);

			if (parent != null)
			{
				parent.Folders.Add(folder);
			}
			else
			{
				fsom.Folders.Add(folder);
			}

			if (System.IO.Directory.Exists(filename) /* && copy */)
			{
				string[] files = System.IO.Directory.GetFiles(filename);
				foreach (string file in files)
				{
					AddFileToArchive(file, folder);
				}
				string[] dirs = System.IO.Directory.GetDirectories(filename);
				foreach (string dir in dirs)
				{
					AddFolderToArchive(dir, folder);
				}
			}
			
			if (tv.SelectedNode == null || (tv.SelectedNode == tv.Nodes[0] && parent == null) || (tv.SelectedNode.Tag is Folder && tv.SelectedNode.Tag == parent))
			{
				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.Text = folder.Name;
				lvi.ImageKey = "generic-folder-closed";
				lv.Items.Add(lvi);
				lv.Refresh();
			}

			TreeNode tn = new TreeNode();
			tn.Text = folder.Name;
			tn.ImageKey = "generic-folder-closed";
			tn.SelectedImageKey = "generic-folder-closed";
			tv.SelectedNode.Nodes.Add(tn);
			tn.Tag = folder;
			tn.EnsureVisible();

			if (lv.Focused)
			{
				// lv.BeginEdit();
			}
			else if (tv.Focused)
			{
				tn.BeginEdit();
			}
		}
		
		private class DirectoryComparer
			: IComparer<string>
		{
			public static readonly DirectoryComparer Instance = new DirectoryComparer();
			public int Compare(string a1, string a2)
			{
				if (a1.Contains(System.IO.Path.DirectorySeparatorChar.ToString()) || a1.Contains(System.IO.Path.AltDirectorySeparatorChar.ToString()))
				{
					return 1;
				}
				return a1.CompareTo(a2);
			}
		}

		private void AddFilesToArchive(params string[] filenames)
		{
			BeginEdit();
			
			List<string> lstFileNames = new List<string>(filenames);
			lstFileNames.Sort(DirectoryComparer.Instance);
			
			foreach (string filename in lstFileNames)
			{
				AddFileToArchive(filename);
				if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					System.IO.File.Delete(filename);
				}
			}
			EndEdit();
		}
		private void AddFileToArchive(string filename, Folder parent = null)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			if (System.IO.Directory.Exists(filename))
			{
				AddFolderToArchive(filename, parent);
				return;
			}

			BeginEdit();

			if (parent == null)
			{
				if (tv.SelectedNode != null)
				{
					if (tv.SelectedNode.Tag is Folder) parent = (tv.SelectedNode.Tag as Folder);
				}
			}

			byte[] data = System.IO.File.ReadAllBytes(filename);
			string FileTitle = System.IO.Path.GetFileName(filename);

			File file = new File();
			file.Name = FileTitle;
			file.SetDataAsByteArray(data);

			if (parent != null)
			{
				parent.Files.Add(file);
			}
			else
			{
				fsom.Files.Add(file);
			}

			if (tv.SelectedNode == null || (tv.SelectedNode == tv.Nodes[0] && parent == null) || (tv.SelectedNode.Tag is Folder && tv.SelectedNode.Tag == parent))
			{
				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.ImageKey = "generic-file";
				lvi.Text = FileTitle;
				lvi.Data = file;
				lv.Items.Add(lvi);

				if (tIconLoader != null)
				{
					if (tIconLoader.ThreadState != System.Threading.ThreadState.Unstarted)
					{
						tIconLoader = new System.Threading.Thread(tIconLoader_ThreadStart);
					}
					tIconLoader.Start();
				}

				lv.Refresh();
			}
			
			EndEdit();
		}

		private void lv_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(AwesomeControls.ListView.ListViewItem.ListViewItemCollection)) && ((Control.ModifierKeys & Keys.Control) != Keys.Control))
			{
				e.Effect = DragDropEffects.Move;
				return;
			}

			string[] formats = e.Data.GetFormats();
			System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
			sc.AddRange(formats);

			if (sc.Contains("FileDrop") || sc.Contains("FileNameW") || sc.Contains("FileName"))
			{
				if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					e.Effect = DragDropEffects.Move;
				}
				else
				{
					e.Effect = DragDropEffects.Copy;
				}
			}
		}

		private void lv_DragDrop(object sender, DragEventArgs e)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);

			string[] formats = e.Data.GetFormats();

			if (e.Data.GetDataPresent(typeof(AwesomeControls.ListView.ListViewItem.ListViewItemCollection)))
			{
				if (e.Effect == DragDropEffects.Copy)
				{
					AwesomeControls.ListView.ListViewItem.ListViewItemCollection lvis = (AwesomeControls.ListView.ListViewItem.ListViewItemCollection)e.Data.GetData(typeof(AwesomeControls.ListView.ListViewItem.ListViewItemCollection));
					foreach (AwesomeControls.ListView.ListViewItem lvi in lvis)
					{
						File file = (lvi.Data as File);
						if (file != null)
						{
							if (fsom.Files.Contains(file.Name))
							{
								fsom.Files.Add("Copy of " + file.Name, file.GetDataAsByteArray());
							}
						}
					}
				}
				return;
			}

			System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
			sc.AddRange(formats);

			if (sc.Contains("FileDrop"))
			{
				string[] FileNames = (string[])e.Data.GetData("FileDrop");
				AddFilesToArchive(FileNames);
			}
			else if (sc.Contains("FileNameW"))
			{
				string[] FileNames = (string[])e.Data.GetData("FileNameW");
				AddFilesToArchive(FileNames);
			}
			else if (sc.Contains("FileName"))
			{
				string[] FileNames = (string[])e.Data.GetData("FileName");
				AddFilesToArchive(FileNames);
			}
			else if (sc.Contains("Shell IDList Array"))
			{
				MessageBox.Show("This item cannot be added to archives at this time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		private void mnuListViewContextAddNewFolder_Click(object sender, EventArgs e)
		{
			AddFolderToArchive();
		}

		private void tv_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (tv.SelectedNode == tv.Nodes[0])
			{
				e.CancelEdit = true;
				return;
			}
		}

		private void tv_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			Folder folder = (e.Node.Tag as Folder);
			if (folder == null) return;
			if (e.Label == null) return;

			folder.Name = e.Label;
			e.Node.Text = e.Label;
		}

		private void tv_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TreeNode tn = tv.HitTest(e.Location).Node;
				if (tn != null)
				{
					tv.SelectedNode = tn;
				}
			}
		}

		private void mnuListViewContextAddNewItem_Click(object sender, EventArgs e)
		{

		}

		private void mnuListViewContextProperties_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count == 0)
			{

			}
			else if (lv.SelectedItems.Count == 1)
			{
				File file = (lv.SelectedItems[0].Data as File);
				if (file != null)
				{
					Dialogs.FileSystem.FilePropertiesDialog dlg = new Dialogs.FileSystem.FilePropertiesDialog();
					dlg.SelectedObjects.Add(file);

					if (dlg.ShowDialog() == DialogResult.OK)
					{
						lv.SelectedItems[0].Text = file.Name;
						lv.Refresh();
					}
				}
			}
		}

		private void tsbExtract_Click(object sender, EventArgs e)
		{
			FileSystemObjectModel fsom = (ObjectModel as FileSystemObjectModel);
			if (fsom == null) return;

			AwesomeControls.NativeDialogs.FolderBrowserDialog dlg = new AwesomeControls.NativeDialogs.FolderBrowserDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				File[] files = fsom.GetAllFiles();
				HostApplication.CurrentWindow.UpdateProgress(true);
				foreach (File file in files)
				{
					HostApplication.CurrentWindow.UpdateStatus("Extracting file " + file.Name + " (" + (Array.IndexOf(files, file) + 1).ToString() + " of " + files.Length + ")");
					HostApplication.CurrentWindow.UpdateProgress(0, files.Length, Array.IndexOf(files, file));
					Application.DoEvents();

					string FileName = dlg.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString() + file.Name;
					string ParentDirectoryName = System.IO.Path.GetDirectoryName(FileName);
					if (!System.IO.Directory.Exists(ParentDirectoryName))
					{
						System.IO.Directory.CreateDirectory(ParentDirectoryName);
					}
					file.Save(FileName);
				}
				HostApplication.CurrentWindow.UpdateProgress(false);
				HostApplication.CurrentWindow.UpdateStatus("Ready");

				if (MessageBox.Show("All files in the file system have been saved to:\r\n" + dlg.SelectedPath + "\r\n\r\nWould you like to open the folder?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					System.Diagnostics.Process.Start(dlg.SelectedPath);
				}
				return;
			}
		}

		private void txtFilter_TextChanged(object sender, EventArgs e)
		{
			UpdateListView();
		}

		private void lv_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Back)
			{
				if (mvarCurrentFolder != null)
				{
					if (mvarCurrentFolder.Parent != null)
					{
						mvarCurrentFolder = mvarCurrentFolder.Parent;
					}
					else
					{
						mvarCurrentFolder = null;
					}
				}
				else
				{
					System.Media.SystemSounds.Beep.Play();
				}
				UpdateListView();
			}
		}

		private void lv_ItemActivate(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count == 1)
			{
				if (lv.SelectedItems[0].Items.Count > 0)
				{
					if (lv.SelectedItems[0].Data is Folder)
					{
						mvarCurrentFolder = (lv.SelectedItems[0].Data as Folder);
						UpdateListView();
					}
				}
			}
		}
	}
}
