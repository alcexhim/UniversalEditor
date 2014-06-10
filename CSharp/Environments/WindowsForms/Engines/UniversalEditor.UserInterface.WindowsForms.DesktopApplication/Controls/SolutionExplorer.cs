using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Project;
using UniversalEditor.ObjectModels.Solution;

namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
	public partial class SolutionExplorer : UserControl
	{
		public SolutionExplorer()
		{
			InitializeComponent();

			mnuContext.Name = "SolutionExplorer.Item";
			Glue.Common.Methods.InitializeCustomizableMenuItems(mnuContext);

			IconMethods.PopulateSystemIcons(ref imlSmallIcons);
			imlSmallIcons.Images.Add("generic-solution", Properties.Resources.solution_icon);
			imlSmallIcons.Images.Add("generic-project", Properties.Resources.project_icon);
		}

		private MainWindow mvarParentWindow = null;
		public MainWindow ParentWindow { get { return mvarParentWindow; } set { mvarParentWindow = value; } }

		private SolutionObjectModel mvarSolution = null;
		public SolutionObjectModel Solution
		{
			get { return mvarSolution; }
			set
			{
				mvarSolution = value;
				RefreshSolution();
			}
		}

		private void RefreshSolution()
		{
			tv.Nodes.Clear();
			if (mvarSolution == null) return;

			TreeNode tnSolution = new TreeNode();
			tnSolution.ImageKey = "generic-solution";
			tnSolution.SelectedImageKey = "generic-solution";
			tnSolution.Text = "Solution '" + mvarSolution.Title + "' (" + mvarSolution.Projects.Count + " project" + (mvarSolution.Projects.Count != 1 ? "s" : String.Empty) + ")";
			tnSolution.Tag = mvarSolution;

			foreach (ProjectObjectModel p in mvarSolution.Projects)
			{
				TreeNode tnProject = new TreeNode();
				tnProject.Tag = p;
				tnProject.Text = p.Title;

				if (p.ProjectType != null && p.ProjectType.SmallIconImageFileName != null)
				{
					if (!imlSmallIcons.Images.ContainsKey(p.ProjectType.ID.ToString("B")))
					{
						imlSmallIcons.Images.Add(p.ProjectType.ID.ToString("B"), Image.FromFile(p.ProjectType.SmallIconImageFileName));
					}
					tnProject.ImageKey = p.ProjectType.ID.ToString("B");
					tnProject.SelectedImageKey = p.ProjectType.ID.ToString("B");
				}
				else
				{
					tnProject.ImageKey = "generic-project";
				}

				#region Properties
				{
					TreeNode tn = new TreeNode();
					tn.ImageKey = "generic-folder-properties-closed";
					tn.Name = "Properties";
					tn.Text = "Properties";
					tnProject.Nodes.Add(tn);
				}
				#endregion
				#region References
				{
					TreeNode tn = new TreeNode();
					tn.ImageKey = "generic-folder-references-closed";
					tn.Name = "References";
					tn.Text = "References";
					tnProject.Nodes.Add(tn);
				}
				#endregion

				LoadFileSystem(p.FileSystem, tnProject);
				tnSolution.Nodes.Add(tnProject);
			}
			tv.Nodes.Add(tnSolution);

			tnSolution.Expand();
		}

		private void LoadFileSystem(ProjectFileSystem pfs, TreeNode tnParent)
		{
			foreach (ProjectFolder folder in pfs.Folders)
			{
				LoadItem(folder, tnParent);
			}
			foreach (ProjectFile file in pfs.Files)
			{
				LoadItem(file, tnParent);
			}
		}
		private void LoadItem(ProjectFolder item, TreeNode tnParent)
		{
			TreeNode tn = new TreeNode();
			tn.ImageKey = "generic-folder-closed";
			tn.SelectedImageKey = "generic-folder-closed";
			tn.Text = item.Name;
			tn.Tag = item;
			foreach (ProjectFolder folder in item.Folders)
			{
				LoadItem(folder, tn);
			}
			foreach (ProjectFile file in item.Files)
			{
				LoadItem(file, tn);
			}
			tnParent.Nodes.Add(tn);
		}
		private void LoadItem(ProjectFile item, TreeNode tnParent)
		{
			TreeNode tn = new TreeNode();
			tn.Text = item.DestinationFileName;
			tn.Tag = item;
			tnParent.Nodes.Add(tn);
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (tsbPreviewSelectedItems.Checked)
			{
				// TODO: do preview of selected item
			}
		}

		private void tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (mvarParentWindow == null) return;
			if (e.Node.Tag is ProjectFile)
			{
				// TODO: open file
				ProjectFile file = (e.Node.Tag as ProjectFile);
				string BasePath = String.Empty;
				FileAccessor fa = (mvarSolution.Accessor as FileAccessor);
				if (fa != null) BasePath = System.IO.Path.GetDirectoryName(fa.FileName);

				mvarParentWindow.OpenFile(UniversalEditor.Common.Path.MakeAbsolutePath(file.SourceFileName, BasePath));
			}
		}

		private void tv_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node.Tag == null)
			{
				e.CancelEdit = true;
				return;
			}

			if (e.Node.Tag is SolutionObjectModel)
			{
				SolutionObjectModel sol = (e.Node.Tag as SolutionObjectModel);
				e.Node.Text = sol.Title;
			}
		}

		private static char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
		private static string[] SystemReservedNames = new string[] { "CON", "AUX", "PRN", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", ".", ".." };

		private bool IsFileNameInvalid(string FileName)
		{
			return FileName.ContainsAny(InvalidFileNameChars) || FileName.ContainsAny(SystemReservedNames);
		}

		private void tv_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			string label = e.Label;
			if (label == null) label = e.Node.Text;

			if (e.Node.Tag is SolutionObjectModel)
			{
				SolutionObjectModel sol = (e.Node.Tag as SolutionObjectModel);
				sol.Title = label;
				e.CancelEdit = true;
				e.Node.Text = "Solution '" + label + "' (" + sol.Projects.Count.ToString() + " project" + (sol.Projects.Count == 1 ? "" : "s") + ")";
			}
			else if (e.Node.Tag is ProjectObjectModel)
			{
				ProjectObjectModel proj = (e.Node.Tag as ProjectObjectModel);
				proj.Title = label;
				e.Node.Text = label;
			}
			else if (e.Node.Tag is ProjectFile)
			{
				if (IsFileNameInvalid(label))
				{
					MessageBox.Show("Item and file names cannot:\r\n- contain any of the following characters: / ? : & \\ * \" < > | # %\r\n- contain Unicode control characters\r\n- contain surrogate characters\r\n- be system reserved names, including 'CON', 'AUX', 'PRN', 'COM1' or 'LPT2'\r\n- be '.' or '..'\r\n\r\nPlease enter a valid name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.CancelEdit = true;
					return;
				}

				ProjectFile file = (e.Node.Tag as ProjectFile);
				file.DestinationFileName = label;
			}
		}

		private void tv_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				TreeViewHitTestInfo tvhti = tv.HitTest(e.Location);
				if (tvhti.Node != null) tv.SelectedNode = tvhti.Node;
			}
		}

		private void mnuContext_Opening(object sender, CancelEventArgs e)
		{
			mnuContextAdd.DropDownItems.Clear();
			mnuContextAdd.DropDownItems.Add(mnuContextAddNewItem);
			mnuContextAdd.DropDownItems.Add(mnuContextAddExistingItem);
			mnuContextAdd.DropDownItems.Add(mnuContextAddSep2);
			mnuContextAdd.DropDownItems.Add(mnuContextAddNewFolder);
			if (tv.SelectedNode.Tag is SolutionObjectModel)
			{
				mnuContextAddNewItem.Text = "Ne&w Project...";
				mnuContextAddExistingItem.Text = "Existin&g Project...";
			}
			else
			{
				mnuContextAddNewItem.Text = "Ne&w Item...";
				mnuContextAddExistingItem.Text = "Existin&g Item...";
			}

			if (tv.SelectedNode.Tag is ProjectObjectModel)
			{
				ProjectObjectModel proj = (tv.SelectedNode.Tag as ProjectObjectModel);
				if (proj.ProjectType != null)
				{
					if (proj.ProjectType.ItemShortcuts.Count > 0)
					{
						mnuContextAdd.DropDownItems.Add(mnuContextAddSep3);
						foreach (ProjectTypeItemShortcut its in proj.ProjectType.ItemShortcuts)
						{
							ToolStripMenuItem tsmi = new ToolStripMenuItem();
							tsmi.Text = its.Title;
							tsmi.Click += tsmiItemShortcut_Click;
							tsmi.Tag = its;
							mnuContextAdd.DropDownItems.Add(tsmi);
						}
					}
				}
			}
		}
		private void tsmiItemShortcut_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			ProjectTypeItemShortcut its = (tsmi.Tag as ProjectTypeItemShortcut);

			WindowsFormsEngine.LastWindow.NewFile();
		}

		private void mnuContextAddNewProject_Click(object sender, EventArgs e)
		{
			ParentWindow.AddSolutionProjectNew();
		}

		private void mnuContextAddExistingProject_Click(object sender, EventArgs e)
		{
			ParentWindow.AddSolutionProjectExisting();
		}

		private void mnuContextAddNewItem_Click(object sender, EventArgs e)
		{
			if (tv.SelectedNode.Tag is SolutionObjectModel)
			{
				// we add a new project to the solution
				ParentWindow.AddSolutionProjectNew();
			}
			else
			{
				// we add a new item to the project
				ParentWindow.AddProjectItemNew();
			}
		}

		private void mnuContextAddExistingItem_Click(object sender, EventArgs e)
		{
			if (tv.SelectedNode.Tag is SolutionObjectModel)
			{
				ParentWindow.AddSolutionProjectExisting();
			}
			else
			{
				ParentWindow.AddProjectItemExisting();
			}
		}

		private void mnuContextAddNewFolder_Click(object sender, EventArgs e)
		{
			TreeNode tn = new TreeNode();
			tn.Text = "NewFolder1";
			tv.SelectedNode.Nodes.Add(tn);
			tv.SelectedNode = tn;

			tn.BeginEdit();
		}
	}
}
