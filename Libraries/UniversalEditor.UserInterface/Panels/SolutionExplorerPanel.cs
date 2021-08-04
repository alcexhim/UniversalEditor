//
//  SolutionExplorerPanel.cs
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
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;

namespace UniversalEditor.UserInterface.Panels
{
	public class SolutionExplorerPanel : Panel, IDocumentPropertiesProviderControl
	{
		private DefaultTreeModel tmSolutionExplorer = null;
		private ListViewControl tvSolutionExplorer = new ListViewControl();

		private SolutionObjectModel _Solution = null;
		public SolutionObjectModel Solution
		{
			get { return _Solution; }
			set
			{
				_Solution = value;
				UpdateSolutionExplorer();
			}
		}

		public ProjectObjectModel Project
		{
			get
			{
				if (tvSolutionExplorer.SelectedRows.Count > 0)
				{
					TreeModelRow row = tvSolutionExplorer.SelectedRows[0];
					while (row != null)
					{
						ProjectObjectModel proj = row.GetExtraData<ProjectObjectModel>("project");
						if (proj != null)
							return proj;

						row = row.ParentRow;
					}
				}
				else
				{
					if (Solution != null)
					{
						if (Solution.Projects.Count > 0)
						{
							return Solution.Projects[0];
						}
					}
				}
				return null;
			}
			set
			{

			}
		}

		private TreeModelRow LoadSolutionExplorerFolder(ObjectModels.Project.ProjectFolder fold)
		{
			TreeModelRow rowFolder = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmSolutionExplorer.Columns[0], fold.Name)
			});
			rowFolder.SetExtraData<ObjectModels.Project.ProjectFolder>("folder", fold);

			foreach (ObjectModels.Project.ProjectFolder fold2 in fold.Folders)
			{
				TreeModelRow row = LoadSolutionExplorerFolder(fold2);
				rowFolder.Rows.Add(row);
			}
			foreach (ObjectModels.Project.ProjectFile file in fold.Files)
			{
				if (file.Dependents.Count > 0)
					continue;

				TreeModelRow rowFile = LoadSolutionExplorerFile(file);
				rowFolder.Rows.Add(rowFile);
			}
			return rowFolder;
		}

		private TreeModelRow LoadSolutionExplorerFile(ProjectFile file)
		{
			TreeModelRow rowFile = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmSolutionExplorer.Columns[0], file.DestinationFileName)
			});
			rowFile.SetExtraData<ObjectModels.Project.ProjectFile>("file", file);
			foreach (ProjectFile file2 in file.Files)
			{
				TreeModelRow rowFile2 = LoadSolutionExplorerFile(file2);
				rowFile2.SetExtraData<ObjectModels.Project.ProjectFile>("file", file2);
				rowFile.Rows.Add(rowFile2);
			}
			return rowFile;
		}

		private void UpdateSolutionExplorer()
		{
			tmSolutionExplorer.Rows.Clear();

			if (_Solution != null)
			{
				TreeModelRow rowSolution = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmSolutionExplorer.Columns[0], _Solution.Title)
				});
				rowSolution.SetExtraData<ObjectModels.Solution.SolutionObjectModel>("solution", _Solution);

				foreach (ObjectModels.Project.ProjectObjectModel proj in _Solution.Projects)
				{
					TreeModelRow rowProject = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmSolutionExplorer.Columns[0], proj.Title)
					});
					rowProject.SetExtraData<ObjectModels.Project.ProjectObjectModel>("project", proj);

					TreeModelRow rowReferences = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmSolutionExplorer.Columns[0], "References")
					});
					foreach (ObjectModels.Project.Reference reff in proj.References)
					{
						rowReferences.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
						{
							new TreeModelRowColumn(tmSolutionExplorer.Columns[0], reff.Title)
						}));
					}
					rowProject.Rows.Add(rowReferences);

					foreach (ObjectModels.Project.ProjectFolder fold in proj.FileSystem.Folders)
					{
						TreeModelRow row = LoadSolutionExplorerFolder(fold);
						rowProject.Rows.Add(row);
					}
					foreach (ObjectModels.Project.ProjectFile file in proj.FileSystem.Files)
					{
						TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
						{
							new TreeModelRowColumn(tmSolutionExplorer.Columns[0], file.DestinationFileName)
						});
						row.SetExtraData<ObjectModels.Project.ProjectFile>("file", file);
						rowProject.Rows.Add(row);
					}

					rowSolution.Rows.Add(rowProject);
				}
				tmSolutionExplorer.Rows.Add(rowSolution);
			}
		}

		public SolutionExplorerPanel()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			tmSolutionExplorer = new DefaultTreeModel(new Type[] { typeof(string) });
			tvSolutionExplorer.Model = tmSolutionExplorer;
			tvSolutionExplorer.BeforeContextMenu += tvSolutionExplorer_BeforeContextMenu;
			tvSolutionExplorer.RowActivated += tvSolutionExplorer_RowActivated;

			// (UniversalEditor.exe:24867): Gtk-CRITICAL **: 21:28:56.913: gtk_tree_store_set_value: assertion 'G_IS_VALUE (value)' failed
			tvSolutionExplorer.Columns.Add(new ListViewColumn("File name", new CellRenderer[] { new CellRendererText(tmSolutionExplorer.Columns[0]) }));

			this.Controls.Add(tvSolutionExplorer, new BoxLayout.Constraints(true, true));

			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_OpenContainingFolder", delegate(object sender, EventArgs e)
			{
				if (tvSolutionExplorer.SelectedRows.Count != 1) return;

				ProjectObjectModel project = tvSolutionExplorer.SelectedRows[0].GetExtraData<ProjectObjectModel>("project");
				ProjectFile file = tvSolutionExplorer.SelectedRows[0].GetExtraData<ProjectFile>("file");
				ProjectFolder folder = tvSolutionExplorer.SelectedRows[0].GetExtraData<ProjectFolder>("folder");

				TreeModelRow prow = tvSolutionExplorer.SelectedRows[0].ParentRow;
				while (project == null && prow != null)
				{
					project = prow.GetExtraData<ProjectObjectModel>("project");
					prow = prow.ParentRow;
				}

				if (project != null)
				{
					if (project.BasePath != null)
					{
						string fullpath = System.IO.Path.Combine(project.BasePath, tvSolutionExplorer.SelectedRows[0].RowColumns[0].Value.ToString());
						if (file == null && folder == null)
						{
							fullpath = fullpath + ".ueproj";
						}

						// not sure if this should be made into a UWT convenience function or not...
						try
						{
							if (Environment.OSVersion.Platform == PlatformID.Unix)
							{
								((UIApplication)Application.Instance).Launch("nautilus", String.Format("-s \"{0}\"", fullpath));
							}
							else if (Environment.OSVersion.Platform == PlatformID.Win32Windows)
							{
								((UIApplication)Application.Instance).Launch("explorer", String.Format("/select \"{0}\"", fullpath));
							}
							else
							{
								((UIApplication)Application.Instance).Launch(System.IO.Path.GetDirectoryName(fullpath));
							}
						}
						catch (Exception ex)
						{
							// not using nautilus, just launch the folder
							((UIApplication)Application.Instance).Launch(System.IO.Path.GetDirectoryName(fullpath));
						}
					}
				}
			});
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Project_Add_ExistingFiles", mnuContextProjectAddExistingFiles_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Project_Add_NewFolder", mnuContextProjectAddNewFolder_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_ExistingFiles", mnuContextSolutionAddExistingProject_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_ExistingProject", mnuContextSolutionAddExistingProject_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_NewProject", mnuContextSolutionAddNewProject_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_File_Open", mnuContextFileOpen_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_NewFile", mnuContextFolderAddNewFile_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_ExistingFiles", mnuContextFolderAddExistingFile_Click);
			Application.Instance.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_NewFolder", mnuContextFolderAddNewFolder_Click);
		}

		private void mnuContextFileOpen_Click(object sender, EventArgs e)
		{
		}

		private void mnuContextFolderAddNewFile_Click(object sender, EventArgs e)
		{
		}

		private void mnuContextFolderAddExistingFile_Click(object sender, EventArgs e)
		{
			TreeModelRow row = tvSolutionExplorer.LastHitTest.Row;
			if (row == null) return;

			ProjectFolder fldr = row.GetExtraData<ProjectFolder>("folder");
			if (fldr == null) return;

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			dlg.MultiSelect = true;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				foreach (string filename in dlg.SelectedFileNames)
				{
					ObjectModels.Project.ProjectFile pf = new ObjectModels.Project.ProjectFile();
					pf.SourceFileName = filename;
					pf.DestinationFileName = System.IO.Path.GetFileName(filename);
					fldr.Files.Add(pf);
				}

				UpdateSolutionExplorer();
			}
		}

		void tvSolutionExplorer_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			// TODO: implement what happens when we activate a row in the solution explorer list

			// typically:
			//		- if it is a ProjectFile we want to open the respective file in a new document tab - DONE
			// 		- if it is a ProjectFolder we just want to expand/collapse tree (default action) - DONE
			//		- if it is a special folder named "Properties", or the project item itself, it should display the special project properties dialog
			//		- if it is a special folder named "Properties/Resources", the resource editor shall be shown

			// these last two conditions can be simplified by simply having the Properties folder open the project file in an editor, and implementing
			// an editor for ProjectObjectModel which is essentially the "project properties" window (a la VS)
			ProjectObjectModel project = e.Row.GetExtraData<ProjectObjectModel>("project");
			ProjectFile file = e.Row.GetExtraData<ProjectFile>("file");
			ProjectFolder folder = e.Row.GetExtraData<ProjectFolder>("folder");
			if (project != null)
			{
				Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(new byte[0], String.Format("{0} Properties", project.Title));
				Document d = new Document(project, null, ma);
				d.Title = String.Format("{0} Properties", project.Title);
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).OpenFile(d);
			}
			else if (file != null)
			{
				if (file.SourceFileAccessor != null)
				{
					((EditorApplication)Application.Instance).LastWindow.OpenFile(new Document(file.SourceFileAccessor));
				}
				else
				{
					((EditorApplication)Application.Instance).LastWindow.OpenFile(file.SourceFileName);
				}
			}
			else if (folder != null)
			{
				return;
			}
			else
			{
				MessageDialog.ShowDialog(String.Format("Activated row {0}", e.Row.RowColumns[0].Value), "Info", MessageDialogButtons.OK);
			}
		}

		private void mnuContextProjectAddNewFolder_Click(object sender, EventArgs e)
		{
			TreeModelRow row = tvSolutionExplorer.LastHitTest.Row;
			if (row == null) return;

			ProjectObjectModel proj = row.GetExtraData<ProjectObjectModel>("project");
			if (proj == null) return;

			string folderName = "New folder";
			ProjectFolder folder = proj.FileSystem.Folders.Add(folderName);

			TreeModelRow rowFolder = new TreeModelRow();
			rowFolder.RowColumns.Add(new TreeModelRowColumn(tmSolutionExplorer.Columns[0], folderName));
			rowFolder.SetExtraData<ProjectFolder>("folder", folder);
			row.Rows.Add(rowFolder);

			UpdateSolutionExplorer();

			tvSolutionExplorer.Focus(rowFolder, null, null);
		}

		private void mnuContextFolderAddNewFolder_Click(object sender, EventArgs e)
		{
			TreeModelRow row = tvSolutionExplorer.LastHitTest.Row;
			if (row == null) return;

			ProjectFolder proj = row.GetExtraData<ProjectFolder>("folder");
			if (proj == null) return;

			string folderName = "New folder";
			ProjectFolder folder = proj.Folders.Add(folderName);

			TreeModelRow rowFolder = new TreeModelRow();
			rowFolder.RowColumns.Add(new TreeModelRowColumn(tmSolutionExplorer.Columns[0], folderName));
			rowFolder.SetExtraData<ProjectFolder>("folder", folder);
			row.Rows.Add(rowFolder);

			UpdateSolutionExplorer();
		}

		private void mnuContextProjectAddExistingFiles_Click(object sender, EventArgs e)
		{
			TreeModelRow row = tvSolutionExplorer.LastHitTest.Row;
			if (row == null) return;

			ObjectModels.Project.ProjectObjectModel proj = row.GetExtraData<ObjectModels.Project.ProjectObjectModel>("project");
			if (proj == null) return;

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			dlg.MultiSelect = true;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				foreach (string filename in dlg.SelectedFileNames)
				{
					ProjectFile pf = new ProjectFile();
					pf.SourceFileName = filename;
					pf.DestinationFileName = System.IO.Path.GetFileName(filename);
					proj.FileSystem.Files.Add(pf);
				}

				UpdateSolutionExplorer();
			}
		}

		private void mnuContextSolutionAddNewProject_Click(object sender, EventArgs e)
		{
			MainWindow mw = (((EditorApplication)Application.Instance).LastWindow as MainWindow);
			if (mw == null) return;
			mw.NewProject(true);
		}
		private void mnuContextSolutionAddExistingProject_Click(object sender, EventArgs e)
		{
			MainWindow mw = (((EditorApplication)Application.Instance).LastWindow as MainWindow);
			if (mw == null) return;

			ProjectObjectModel proj = mw.ShowOpenProjectDialog();
			if (proj == null) return;

			_Solution.Projects.Add(proj);
			Solution = _Solution; // update the UI
		}

		private void tvSolutionExplorer_BeforeContextMenu(object sender, EventArgs e)
		{
			if (tvSolutionExplorer.LastHitTest.Row != null)
			{
				ProjectObjectModel project = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ProjectObjectModel>("project");
				SolutionObjectModel solution = tvSolutionExplorer.LastHitTest.Row.GetExtraData<SolutionObjectModel>("solution");
				ProjectFolder folder = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ProjectFolder>("folder");
				ProjectFile file = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ProjectFile>("file");

				if (project != null)
				{
					tvSolutionExplorer.ContextMenuCommandID = "SolutionExplorer_ContextMenu_Project";
				}
				else if (solution != null)
				{
					tvSolutionExplorer.ContextMenuCommandID = "SolutionExplorer_ContextMenu_Solution";
				}
				else if (folder != null)
				{
					tvSolutionExplorer.ContextMenuCommandID = "SolutionExplorer_ContextMenu_Folder";
				}
				else if (file != null)
				{
					tvSolutionExplorer.ContextMenuCommandID = "SolutionExplorer_ContextMenu_File";
				}
			}
		}

		/// <summary>
		/// Deletes the selected item in the Solution Explorer panel.
		/// </summary>
		public void Delete()
		{
			if (tvSolutionExplorer.SelectedRows.Count > 0)
			{
				/*
				 // does not work - we need to fix this in UWT
				while (tvSolutionExplorer.SelectedRows.Count > 0)
				{
					tmSolutionExplorer.Rows.Remove(tvSolutionExplorer.SelectedRows[0]);
				}
				*/
				ProjectFolder folder = tvSolutionExplorer.SelectedRows[0].GetExtraData<ProjectFolder>("folder");
				ProjectFile file = tvSolutionExplorer.SelectedRows[0].GetExtraData<ProjectFile>("file");
				if (file != null)
				{
					file.Parent.Files.Remove(file);
				}
				if (folder != null)
				{
					folder.Parent.Folders.Remove(folder);
				}
				UpdateSolutionExplorer();
			}
		}

		void IDocumentPropertiesProvider.ShowDocumentPropertiesDialog()
		{
			if (tvSolutionExplorer.SelectedRows.Count == 1)
			{
				TreeModelRow row = tvSolutionExplorer.SelectedRows[0];
				ProjectObjectModel project = row.GetExtraData<ProjectObjectModel>("project");
				ProjectFile file = row.GetExtraData<ProjectFile>("file");
				ProjectFolder folder = row.GetExtraData<ProjectFolder>("folder");

				if (project != null)
				{
					SettingsDialog dlg = new SettingsDialog();
					dlg.SettingsProviders.Clear();
					dlg.Text = String.Format("{0} Properties", project.Title);

					if (dlg.ShowDialog() == DialogResult.OK)
					{

					}
					return;
				}
				if (file != null)
				{

				}
			}

		}
	}
}
