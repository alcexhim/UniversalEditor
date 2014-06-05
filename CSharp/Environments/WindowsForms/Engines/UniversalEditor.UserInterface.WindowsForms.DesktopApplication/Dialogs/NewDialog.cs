using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	internal partial class NewDialogBase : AwesomeControls.Dialog
	{
		public NewDialogBase()
		{
			InitializeComponent();
			Mode = NewDialogMode.File;

			lvFileTemplates.Columns.Add("Title", 300);
			lvFileTemplates.Columns.Add("Description");

			Font = SystemFonts.MenuFont;

			RefreshProjectTemplates();
			tvProject.Sort();
		}

		private NewDialogMode mvarMode = NewDialogMode.File;
		public NewDialogMode Mode
		{
			get { return mvarMode; }
			set
			{
				mvarMode = value;

				switch (mvarMode)
				{
					case NewDialogMode.File:
					{
						pnlNewFile.Enabled = true;
						pnlNewFile.Visible = true;

						pnlNewProject.Visible = false;
						pnlNewProject.Enabled = false;
						break;
					}
					case NewDialogMode.Project:
					{
						pnlNewProject.Enabled = true;
						pnlNewProject.Visible = true;

						pnlNewFile.Visible = false;
						pnlNewFile.Enabled = false;
						
						RefreshProjectTemplates();
						break;
					}
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			switch (mvarMode)
			{
				case NewDialogMode.File:
				{
					if (lvFileTemplates.Items.Count == 1 && lvFileTemplates.SelectedItems.Count == 0)
					{
						lvFileTemplates.Items[0].Selected = true;
					}

					if (lvFileTemplates.SelectedItems.Count == 0)
					{
						MessageBox.Show("Please select a template to use as a base for the new document.  If you do not wish to use a template, select \"Blank Document\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					mvarSelectedItem = (lvFileTemplates.SelectedItems[0].Data as Template);
					break;
				}
				case NewDialogMode.Project:
				{
					if (lvProjectTemplates.Items.Count == 1 && lvProjectTemplates.SelectedItems.Count == 0)
					{
						lvProjectTemplates.Items[0].Selected = true;
					}

					if (lvProjectTemplates.SelectedItems.Count == 0)
					{
						MessageBox.Show("Please select a template to use as a base for the new project.  If you do not wish to use a template, select \"Blank Project\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					mvarSelectedItem = (lvProjectTemplates.SelectedItems[0].Data as Template);
					break;
				}
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private Template mvarSelectedItem = null;
		public Template SelectedItem { get { return mvarSelectedItem; } }

		private void dts_SelectionChanged(object sender, EventArgs e)
		{
			lvFileTemplates.Items.Clear();

			if (dts.SelectedObject == null)
			{
				cmdOK.Enabled = false;
				return;
			}

			ObjectModelReference omr = (dts.SelectedObject as ObjectModelReference);

			AwesomeControls.ListView.ListViewItem lviBlank = new AwesomeControls.ListView.ListViewItem();
			lviBlank.Text = "Blank Document";
			lviBlank.Details.Add("Creates a blank " + omr.Title + " document.");

			DocumentTemplate dtBlank = new DocumentTemplate();
			dtBlank.Title = "Blank Document";
			dtBlank.ObjectModelReference = omr;
			dtBlank.Description = "Creates a blank " + omr.Title + " document.";
			lviBlank.Data = dtBlank;

			lvFileTemplates.Items.Add(lviBlank);

			#region Load document templates
			{
				DocumentTemplate[] templates = UniversalEditor.Common.Reflection.GetAvailableDocumentTemplates(omr);
				foreach (DocumentTemplate dt in templates)
				{
					AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
					if (System.IO.File.Exists(dt.LargeIconImageFileName))
					{
						lvi.Image = Image.FromFile(dt.LargeIconImageFileName);
					}
					lvi.Text = dt.Title;
					lvi.Details.Add(dt.Description);
					lvi.Data = dt;
					lvFileTemplates.Items.Add(lvi);
				}
			}
			#endregion

			if (lvFileTemplates.Items.Count > 0)
			{
				lvFileTemplates.Items[0].Selected = true;
			}
			
			cmdOK.Enabled = lvFileTemplates.SelectedItems.Count == 1;
		}

		private void lvFileTemplates_SelectionChanged(object sender, EventArgs e)
		{
			if (lvFileTemplates.SelectedItems.Count == 0)
			{
				cmdOK.Enabled = false;
				return;
			}
			mvarSelectedItem = (lvFileTemplates.SelectedItems[0].Data as Template);
			cmdOK.Enabled = true;
		}

		private void tvProject_AfterSelect(object sender, TreeViewEventArgs e)
		{
			RefreshProjectTemplates();
		}

		private void RefreshProjectTemplates()
		{
			lvProjectTemplates.Items.Clear();

			ProjectTemplate[] templates = UniversalEditor.Common.Reflection.GetAvailableProjectTemplates();
			foreach (ProjectTemplate dt in templates)
			{
				if (dt.Path != null)
				{
					TreeNode tn = null;
					for (int i = 0; i < dt.Path.Length; i++)
					{
						if (tn == null)
						{
							if (tvProject.Nodes.ContainsKey(dt.Path[i]))
							{
								tn = tvProject.Nodes[dt.Path[i]];
							}
							else
							{
								tn = tvProject.Nodes.Add(dt.Path[i], dt.Path[i]);
							}
						}
						else
						{
							if (tn.Nodes.ContainsKey(dt.Path[i]))
							{
								tn = tn.Nodes[dt.Path[i]];
							}
							else
							{
								tn = tn.Nodes.Add(dt.Path[i], dt.Path[i]);
							}
						}
					}

					if (tn == tvProject.SelectedNode)
					{
						AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
						if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
						{
							lvi.Image = Image.FromFile(dt.LargeIconImageFileName);
						}
						else
						{
							Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
						}
						lvi.Text = dt.Title;
						lvi.Details.Add(dt.Description);
						lvi.Data = dt;
						lvProjectTemplates.Items.Add(lvi);
					}
				}
				else
				{
					AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
					if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
					{
						lvi.Image = Image.FromFile(dt.LargeIconImageFileName);
					}
					else
					{
						Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
					}
					lvi.Text = dt.Title;
					lvi.Details.Add(dt.Description);
					lvi.Data = dt;
					lvProjectTemplates.Items.Add(lvi);
				}
			}

			if (lvProjectTemplates.Items.Count > 0)
			{
				lvProjectTemplates.SelectedItems.Clear();
				lvProjectTemplates.Items[0].Selected = true;

				lvProjectTemplates_SelectionChanged(null, EventArgs.Empty);
				cmdOK.Enabled = true;
			}
			else
			{
				cmdOK.Enabled = false;
			}
		}


		private void lvProjectTemplates_SelectionChanged(object sender, EventArgs e)
		{
			if (lvProjectTemplates.SelectedItems.Count != 1) return;
			ProjectTemplate pt = (lvProjectTemplates.SelectedItems[0].Data as ProjectTemplate);
			if (pt == null) return;

			if (!txtProjectTitle_IsChanged) txtProjectTitle.Text = pt.ProjectNamePrefix + "1";
		}

		private bool txtProjectTitle_IsChanged = false;
		private void txtProjectTitle_KeyPress(object sender, KeyPressEventArgs e)
		{
			txtProjectTitle_IsChanged = true;
		}

		private bool txtSolutionTitle_IsChanged = false;
		private void txtSolutionTitle_KeyPress(object sender, KeyPressEventArgs e)
		{
			txtSolutionTitle_IsChanged = true;
		}

		private void txtProjectTitle_TextChanged(object sender, EventArgs e)
		{
			if (!txtSolutionTitle_IsChanged) txtSolutionTitle.Text = txtProjectTitle.Text;
		}
	}
	public enum NewDialogMode
	{
		File,
		Project
	}
	public class NewDialog
	{
		private Template mvarSelectedItem = null;
		public Template SelectedItem { get { return mvarSelectedItem; } }

		private bool mvarCombineObjects = false;
		public bool CombineObjects { get { return mvarCombineObjects; } set { mvarCombineObjects = value; } }

		private NewDialogMode mvarMode = NewDialogMode.File;
		public NewDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

		public DialogResult ShowDialog()
		{
			NewDialogBase dlg = new NewDialogBase();
			if (mvarCombineObjects)
			{
				dlg.optCombine.Checked = true;
			}
			else
			{
				dlg.optSeparate.Checked = true;
			}
			dlg.Mode = mvarMode;
			dlg.txtProjectTitle.Text = mvarProjectTitle;
			dlg.txtSolutionTitle.Text = mvarSolutionTitle;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarSelectedItem = dlg.SelectedItem;
				mvarCombineObjects = dlg.optCombine.Checked;
				mvarProjectTitle = dlg.txtProjectTitle.Text;
				mvarSolutionTitle = dlg.txtSolutionTitle.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}

		private string mvarProjectTitle = String.Empty;
		public string ProjectTitle { get { return mvarProjectTitle; } set { mvarProjectTitle = value; } }
		private string mvarSolutionTitle = String.Empty;
		public string SolutionTitle { get { return mvarSolutionTitle; } set { mvarSolutionTitle = value; } }
	}
}
