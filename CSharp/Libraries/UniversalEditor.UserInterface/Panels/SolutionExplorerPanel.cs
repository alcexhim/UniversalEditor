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

		private Menu mnuContextProject = null;
		private Menu mnuContextSolution = null;
		private Menu mnuContextFolder = null;
		private Menu mnuContextFile = null;

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

			mnuContextProject = new Menu();
			mnuContextProject.Items.AddRange(new MBS.Framework.UserInterface.MenuItem[]
			{
				new MBS.Framework.UserInterface.CommandMenuItem("B_uild Project"),
				new MBS.Framework.UserInterface.CommandMenuItem("R_ebuild Project"),
				new MBS.Framework.UserInterface.CommandMenuItem("C_lean Project"),
				new MBS.Framework.UserInterface.CommandMenuItem("Unload"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Run Pro_ject"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Debug Project"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("A_dd", new MBS.Framework.UserInterface.MenuItem[]
				{
					new MBS.Framework.UserInterface.CommandMenuItem("New _File..."),
					new MBS.Framework.UserInterface.CommandMenuItem("E_xisting File(s)...", null, mnuContextProjectAddExistingFiles_Click)
				}),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Cu_t"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Copy"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Paste"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Delete"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("P_roperties...")
			});
			mnuContextSolution = new Menu();
			mnuContextSolution.Items.AddRange(new MBS.Framework.UserInterface.MenuItem[]
			{
				new MBS.Framework.UserInterface.CommandMenuItem("B_uild Solution"),
				new MBS.Framework.UserInterface.CommandMenuItem("R_ebuild Solution"),
				new MBS.Framework.UserInterface.CommandMenuItem("C_lean Solution"),
				new MBS.Framework.UserInterface.CommandMenuItem("Unload"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Run Solution"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Debug Solution"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("A_dd", new MBS.Framework.UserInterface.MenuItem[]
				{
					new MBS.Framework.UserInterface.CommandMenuItem("New _Project...", null, mnuContextSolutionAddNewProject_Click),
					new MBS.Framework.UserInterface.CommandMenuItem("E_xisting Project...", null, mnuContextSolutionAddExistingProject_Click),
					new MBS.Framework.UserInterface.SeparatorMenuItem(),
					new MBS.Framework.UserInterface.CommandMenuItem("New Fol_der")
				}),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Cu_t"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Copy"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Paste"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Delete"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("P_roperties...")
			});

			mnuContextFile = new Menu();
			mnuContextFile.Items.AddRange(new MBS.Framework.UserInterface.MenuItem[]
			{
				new MBS.Framework.UserInterface.CommandMenuItem("Open", null, mnuContextFileOpen_Click),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Cu_t"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Copy"),
				// new MBS.Framework.UserInterface.CommandMenuItem("_Paste"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Delete"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Rena_me"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("P_roperties...")
			});
			mnuContextFolder = new Menu();
			mnuContextFolder.Items.AddRange(new MBS.Framework.UserInterface.MenuItem[]
			{
				new MBS.Framework.UserInterface.CommandMenuItem("A_dd", new MBS.Framework.UserInterface.MenuItem[]
				{
					new MBS.Framework.UserInterface.CommandMenuItem("New _File...", null, mnuContextFolderAddNewFile_Click),
					new MBS.Framework.UserInterface.CommandMenuItem("E_xisting File...", null, mnuContextFolderAddExistingFile_Click),
					new MBS.Framework.UserInterface.SeparatorMenuItem(),
					new MBS.Framework.UserInterface.CommandMenuItem("New Fol_der")
				}),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Cu_t"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Copy"),
				// new MBS.Framework.UserInterface.CommandMenuItem("_Paste"),
				new MBS.Framework.UserInterface.CommandMenuItem("_Delete"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("Rena_me"),
				new MBS.Framework.UserInterface.SeparatorMenuItem(),
				new MBS.Framework.UserInterface.CommandMenuItem("P_roperties...")
			});
		}

		private void mnuContextFileOpen_Click(object sender, EventArgs e)
		{
		}

		private void mnuContextFolderAddNewFile_Click(object sender, EventArgs e)
		{
		}

		private void mnuContextFolderAddExistingFile_Click(object sender, EventArgs e)
		{
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
					ObjectModels.Project.ProjectFile pf = new ObjectModels.Project.ProjectFile();
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
				ObjectModels.Project.ProjectObjectModel project = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ObjectModels.Project.ProjectObjectModel>("project");
				ObjectModels.Solution.SolutionObjectModel solution = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ObjectModels.Solution.SolutionObjectModel>("solution");
				ObjectModels.Project.ProjectFolder folder = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ObjectModels.Project.ProjectFolder>("folder");
				ObjectModels.Project.ProjectFile file = tvSolutionExplorer.LastHitTest.Row.GetExtraData<ObjectModels.Project.ProjectFile>("file");

				if (project != null)
				{
					tvSolutionExplorer.ContextMenu = mnuContextProject;
				}
				else if (solution != null)
				{
					tvSolutionExplorer.ContextMenu = mnuContextSolution;
				}
				else if (folder != null)
				{
					tvSolutionExplorer.ContextMenu = mnuContextFolder;
				}
				else if (file != null)
				{
					tvSolutionExplorer.ContextMenu = mnuContextFile;
				}
			}
		}

	}
}
