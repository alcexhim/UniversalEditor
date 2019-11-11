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

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;

namespace UniversalEditor.UserInterface.Panels
{
	public class SolutionExplorerPanel : Panel
	{
		private DefaultTreeModel tmSolutionExplorer = null;
		private ListView tvSolutionExplorer = new ListView();

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
			tvSolutionExplorer.Columns.Add(new ListViewColumnText(tmSolutionExplorer.Columns[0], "File Name"));

			this.Controls.Add(tvSolutionExplorer, new BoxLayout.Constraints(true, true));

			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Project_Add_ExistingFiles", mnuContextProjectAddExistingFiles_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Project_Add_NewFolder", mnuContextProjectAddNewFolder_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_ExistingFiles", mnuContextSolutionAddExistingProject_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_ExistingProject", mnuContextSolutionAddExistingProject_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Solution_Add_NewProject", mnuContextSolutionAddNewProject_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_File_Open", mnuContextFileOpen_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_NewFile", mnuContextFolderAddNewFile_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_ExistingFiles", mnuContextFolderAddExistingFile_Click);
			Application.AttachCommandEventHandler("SolutionExplorer_ContextMenu_Folder_Add_NewFolder", mnuContextFolderAddNewFolder_Click);
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
			//		- if it is a ProjectFile we want to open the respective file in a new document tab
			// 		- if it is a ProjectFolder we just want to expand/collapse tree (default action)
			//		- if it is a special folder named "Properties", or the project item itself, it should display the special project properties dialog
			//		- if it is a special folder named "Properties/Resources", the resource editor shall be shown
			ProjectObjectModel project = e.Row.GetExtraData<ProjectObjectModel>("project");
			ProjectFile file = e.Row.GetExtraData<ProjectFile>("file");
			if (project != null)
			{
				MessageDialog.ShowDialog(String.Format("Opening project properties for {0}", e.Row.RowColumns[0].Value), "Info", MessageDialogButtons.OK);
			}
			else if (file != null)
			{
				Engine.CurrentEngine.LastWindow.OpenFile(file.SourceFileName);
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
			MainWindow mw = (Engine.CurrentEngine.LastWindow as MainWindow);
			if (mw == null) return;
			mw.NewProject(true);
		}
		private void mnuContextSolutionAddExistingProject_Click(object sender, EventArgs e)
		{
			MainWindow mw = (Engine.CurrentEngine.LastWindow as MainWindow);
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

	}
}
