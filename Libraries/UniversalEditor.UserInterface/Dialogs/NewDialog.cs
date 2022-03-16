//
//  NewDialog.cs - provides a UWT ContainerLayout-based CustomDialog for creating a new Document or Project in Universal Editor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using MBS.Framework;
using MBS.Framework.Settings;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;

namespace UniversalEditor.UserInterface.Dialogs
{
	public enum NewDialogMode
	{
		File,
		Project
	}

	/// <summary>
	/// Provides a UWT <see cref="ContainerLayoutAttribute" />-based <see cref="CustomDialog" /> for creating a new Document or Project in Universal Editor.
	/// </summary>
	[ContainerLayout("~/Dialogs/NewDialog.glade", "GtkDialog")]
	public class NewDialog : Dialog
	{
		private Button cmdOK;

		private TextBox txtFileName;
		private TextBox txtFilePath;

		private CheckBox chkAddToSolution;
		private Label lblSolutionName;
		private TextBox txtSolutionName;

		private ListViewControl tvObjectModel;
		private DefaultTreeModel tmObjectModel;

		private ListViewControl tvTemplate;
		private DefaultTreeModel tmTemplate;
		private TextBox txtSearch;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			this.InitializeTreeView();

			Buttons[0].Enabled = false;
			DefaultButton = Buttons[0];
		}
		private void InitializeDocumentTemplateTreeView()
		{
			tmObjectModel.Rows.Clear();

			DocumentTemplate[] templates = UniversalEditor.Common.Reflection.GetAvailableDocumentTemplates();
			foreach (DocumentTemplate dt in templates)
			{
				TreeModelRow tn = null;
				if (dt.Path != null)
				{
					string strPath = String.Join("/", dt.Path);
					if (!(String.IsNullOrEmpty(txtSearch.Text) || strPath.ToLower().Contains(txtSearch.Text.ToLower()))) continue;
					for (int i = 0; i < dt.Path.Length; i++)
					{
						if (tn == null)
						{
							if (tmObjectModel.Rows.Contains(dt.Path[i]))
							{
								tn = tmObjectModel.Rows[dt.Path[i]];
							}
							else
							{
								tn = new TreeModelRow(new TreeModelRowColumn[]
								{
									new TreeModelRowColumn(tmObjectModel.Columns[0], dt.Path[i])
								});
								tn.Name = dt.Path[i];
								tmObjectModel.Rows.Add(tn);
							}
						}
						else
						{
							if (tn.Rows.Contains(dt.Path[i]))
							{
								tn = tn.Rows[dt.Path[i]];
							}
							else
							{
								TreeModelRow tn1 = new TreeModelRow(new TreeModelRowColumn[]
								{
									new TreeModelRowColumn(tmObjectModel.Columns[0], dt.Path[i])
								});
								tn1.Name = dt.Path[i];
								tn.Rows.Add(tn1);
								tn = tn1;
							}
						}
						if (i == dt.Path.Length - 1 && tn != null)
						{
							// last one, let's add all the templates
							List<DocumentTemplate> dts = (List<DocumentTemplate>)tn.GetExtraData<List<DocumentTemplate>>("dts", null);
							if (dts == null)
							{
								dts = new List<DocumentTemplate>();
								tn.SetExtraData<List<DocumentTemplate>>("dts", dts);
							}
							dts.Add(dt);
						}
					}
				}

				if (tn != null && tvObjectModel.SelectedRows.Contains(tn))
				{
					TreeModelRow lvi = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmTemplate.Columns[0], dt.Title),
						new TreeModelRowColumn(tmTemplate.Columns[1], dt.Description)
					});
					if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
					{
						// lvi.Image = MBS.Framework.UserInterface.Drawing.Image.FromFile(dt.LargeIconImageFileName);
					}
					else
					{
						Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
					}
					lvi.SetExtraData<DocumentTemplate>("dt", dt);
					tmTemplate.Rows.Add(lvi);
				}
			}

			InitializeObjectModelTreeView();
		}

		[EventHandler(nameof(chkAddToSolution), "Changed")]
		private void chkAddToSolution_Changed(object sender, EventArgs e)
		{
			txtSolutionName.Enabled = chkAddToSolution.Checked;
			lblSolutionName.Enabled = chkAddToSolution.Checked;
		}


		public NewDialogMode Mode { get; set; }
		public Template SelectedItem { get; set; }
		public bool CombineObjects { get; set; } = false;

		public string SolutionTitle { get; set; }
		public string ProjectTitle { get; set; }

		private void InitializeObjectModelTreeView()
		{
			ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
			foreach (ObjectModelReference omr in omrs)
			{
				if (omr.Path == null) continue;

				string strPath = String.Join("/", omr.Path);
				if (String.IsNullOrEmpty(txtSearch.Text) || strPath.ToLower().Contains(txtSearch.Text.ToLower()))
				{
					InitializeObjectModelTreeViewRow(tmObjectModel, null, omr, 0);
				}
			}
		}
		private void InitializeObjectModelTreeViewRow(DefaultTreeModel tm, TreeModelRow rp, ObjectModelReference omr, int index)
		{
			string[] path = omr.Path;
			if (index > path.Length - 1) return;

			TreeModelRow row = null;
			if (rp == null)
			{
				if (tm.Rows.Contains(path[index]))
				{
					row = tm.Rows[path[index]];
				}
				else
				{
					row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmObjectModel.Columns[0], path[index])
					});
					row.Name = path[index];
					tm.Rows.Add(row);
				}
			}
			else
			{
				if (rp.Rows.Contains(path[index]))
				{
					row = rp.Rows[path[index]];
				}
				else
				{
					row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmObjectModel.Columns[0], path[index])
					});
					row.Name = path[index];
					rp.Rows.Add(row);
				}
			}

			if (index == path.Length - 1 && row != null)
			{
				// last one, let's add all the templates
				List<DocumentTemplate> dts = (List<DocumentTemplate>)row.GetExtraData<List<DocumentTemplate>>("dts", null);
				if (dts == null)
				{
					dts = new List<DocumentTemplate>();
					row.SetExtraData<List<DocumentTemplate>>("dts", dts);
				}

				DocumentTemplate dtEmpty = CreateEmptyDocumentTemplate(omr);
				dts.Add(dtEmpty);
			}

			InitializeObjectModelTreeViewRow(tm, row, omr, index + 1);
		}

		private DocumentTemplate CreateEmptyDocumentTemplate(ObjectModelReference omr)
		{
			string[] path = omr.Path;
			DocumentTemplate dtEmpty = new DocumentTemplate();
			dtEmpty.ObjectModelReference = omr;

			dtEmpty.Title = String.Format("Blank {0} Document", path[path.Length - 1]);
			dtEmpty.Prefix = omr.EmptyTemplatePrefix ?? String.Format("Empty{0}Document", path[path.Length - 1]);
			return dtEmpty;
		}

		[EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			SolutionTitle = txtSolutionName.Text;
			ProjectTitle = txtFileName.Text;

			this.DialogResult = DialogResult.OK;
			/*
			if (String.IsNullOrEmpty(this.txtFileName.Text))
			{
				MessageDialog.ShowDialog("Please enter a file name", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);

				// ... but without DialogResult, something gets
				// corrupted, and with DialogResult, there is no
				// way to cancel the dialog close event... what do?
				return;
			}
			*/

			if (tmTemplate.Rows.Count == 1 && tvTemplate.SelectedRows.Count == 0)
			{
				//tvTemplate.Items[0].Selected = true;
			}

			if (tvTemplate.SelectedRows.Count == 0)
			{
				MessageDialog.ShowDialog("Please select a template to use as a base for the new document.  If you do not wish to use a template, select \"Blank Document\".", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			switch (Mode)
			{
				case NewDialogMode.File:
				{
					SelectedItem = tvTemplate.SelectedRows[0].GetExtraData<DocumentTemplate>("dt");
					break;
				}
				case NewDialogMode.Project:
				{
					ProjectTemplate template = tvTemplate.SelectedRows[0].GetExtraData<ProjectTemplate>("dt");
					if (template.ProjectTypes.Count > 0)
					{
						CustomSettingsProvider csp = new CustomSettingsProvider();

						for (int i = 0; i < template.ProjectTypes.Count; i++)
						{
							if (template.ProjectTypes[i].Variables.Count > 0)
							{
								SettingsGroup sg = new SettingsGroup(template.ProjectTypes[i].Title);
								foreach (Setting ptv in template.ProjectTypes[i].Variables)
								{
									sg.Settings.Add(ptv);
								}

								// template.ProjectType.Variables[co.PropertyName].Value = co.GetValue().ToString();
								// TODO: Figure out how to assign variable values to the newly
								// created project from the template

								csp.SettingsGroups.Add(sg);
							}
						}

						SettingsDialog dlg = new SettingsDialog();
						dlg.SettingsProviders.Clear();
						dlg.SettingsProviders.Add(csp);

						if (csp.SettingsGroups.Count > 0)
						{
							if (dlg.ShowDialog() != DialogResult.OK)
							{
								return;
							}
						}
					}
					else
					{
#if DEBUG
						if (MessageDialog.ShowDialog("The specified template does not have a ProjectType set! This may cause problems with certain features that expect a ProjectType.\r\n\r\nIt is recommended that you specify a ProjectType for all project templates.\r\n\r\nYou are using the Debug build of Universal Editor. This message will not be shown in the Release build. Please fix this issue before release to ensure there are no unexpected problems in the future.", "Warning", MessageDialogButtons.OKCancel, MessageDialogIcon.Warning) == DialogResult.Cancel)
						{
							return;
						}
#endif
					}
					SelectedItem = template;
					break;
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		/// <summary>
		/// Recursively detects <see cref="TreeModelRow" /> with a single child row and expands it. If the <see cref="TreeModelRow" /> has zero child rows, returns that <see cref="TreeModelRow" />. Otherwise, if no such row is found, returns null.
		/// </summary>
		/// <returns>A <see cref="TreeModelRow" /> which has zero child rows, or null if no such <see cref="TreeModelRow" /> exists.</returns>
		/// <param name="row">Row.</param>
		private TreeModelRow ExpandSingleChildRows(ListViewControl tv, TreeModelRow row)
		{
			if (row.Rows.Count == 1)
			{
				tv.SetExpanded(row, true);
				return ExpandSingleChildRows(tv, row.Rows[0]);
			}
			else if (row.Rows.Count == 0)
			{
				return row;
			}
			return null;
		}

		[EventHandler(nameof(txtSearch), "Changed")]
		private void txtSearch_Changed(object sender, EventArgs e)
		{
			InitializeTreeView();
		}

		private void InitializeTreeView()
		{
			// setting Text does not work because the ControlImplementation is not a WindowImplementation
			switch (this.Mode)
			{
				case NewDialogMode.File:
				{
					InitializeDocumentTemplateTreeView();
					Text = "Create New Document";
					break;
				}
				case NewDialogMode.Project:
				{
					InitializeProjectTemplateTreeView();
					Text = "Create New Project";
					break;
				}
			}

			if (tmObjectModel.Rows.Count == 1)
			{
				TreeModelRow row = ExpandSingleChildRows(tvObjectModel, tmObjectModel.Rows[0]);
				if (row != null)
				{
					tvObjectModel.SelectedRows.Clear();
					tvObjectModel.SelectedRows.Add(row);
					tvObjectModel_SelectionChanged(null, EventArgs.Empty);
				}
			}

			if (tvTemplate.SelectedRows.Count == 1)
			{
				Buttons[0].Enabled = true;
			}
			else
			{
				Buttons[0].Enabled = false;
			}
		}
		private void InitializeProjectTemplateTreeView()
		{
			tmObjectModel.Rows.Clear();

			ProjectTemplate[] templates = UniversalEditor.Common.Reflection.GetAvailableProjectTemplates();
			foreach (ProjectTemplate dt in templates)
			{
				TreeModelRow tn = null;
				if (dt.Path != null)
				{
					string strPath = String.Join("/", dt.Path);
					if (!(String.IsNullOrEmpty(txtSearch.Text) || strPath.ToLower().Contains(txtSearch.Text.ToLower()))) continue;
					for (int i = 0; i < dt.Path.Length; i++)
					{
						if (tn == null)
						{
							if (tmObjectModel.Rows.Contains(dt.Path[i]))
							{
								tn = tmObjectModel.Rows[dt.Path[i]];
							}
							else
							{
								tn = new TreeModelRow(new TreeModelRowColumn[]
								{
									new TreeModelRowColumn(tmObjectModel.Columns[0], dt.Path[i])
								});
								tn.Name = dt.Path[i];
								tmObjectModel.Rows.Add(tn);
							}
						}
						else
						{
							if (tn.Rows.Contains(dt.Path[i]))
							{
								tn = tn.Rows[dt.Path[i]];
							}
							else
							{
								TreeModelRow tn1 = new TreeModelRow(new TreeModelRowColumn[]
								{
									new TreeModelRowColumn(tmObjectModel.Columns[0], dt.Path[i])
								});
								tn1.Name = dt.Path[i];
								tn.Rows.Add(tn1);
								tn = tn1;
							}
						}
						if (i == dt.Path.Length - 1 && tn != null)
						{
							// last one, let's add all the templates
							List<ProjectTemplate> dts = (List<ProjectTemplate>)tn.GetExtraData<List<ProjectTemplate>>("dts", null);
							if (dts == null)
							{
								dts = new List<ProjectTemplate>();
								tn.SetExtraData<List<ProjectTemplate>>("dts", dts);
							}
							dts.Add(dt);
						}
					}
				}

				if (tn == null || tvObjectModel.SelectedRows.Contains(tn))
				{
					TreeModelRow lvi = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmTemplate.Columns[0], dt.Title),
						new TreeModelRowColumn(tmTemplate.Columns[1], dt.Description)
					});
					if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
					{
						// lvi.Image = MBS.Framework.UserInterface.Drawing.Image.FromFile(dt.LargeIconImageFileName);
					}
					else
					{
						Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
					}
					lvi.SetExtraData<ProjectTemplate>("dt", dt);
					tmTemplate.Rows.Add(lvi);
				}
			}
		}

		[EventHandler(nameof(tvObjectModel), "SelectionChanged")]
		private void tvObjectModel_SelectionChanged(object sender, EventArgs e)
		{
			if (tvObjectModel.SelectedRows.Count < 1) return;

			tmTemplate.Rows.Clear();
			tvTemplate.SelectedRows.Clear();

			if (Mode == NewDialogMode.File)
			{
				RefreshDocumentTemplates(tvObjectModel.SelectedRows[0]);
			}
			else if (Mode == NewDialogMode.Project)
			{
				RefreshProjectTemplates(tvObjectModel.SelectedRows[0]);
			}

			if (tmTemplate.Rows.Count > 0)
			{
				tvTemplate.SelectedRows.Add(tmTemplate.Rows[0]);
			}
		}

		[EventHandler(nameof(tvTemplate), "SelectionChanged")]
		private void tvTemplate_SelectionChanged(object sender, EventArgs e)
		{
			Buttons[0].Enabled = (tvTemplate.SelectedRows.Count > 0);
			if (tvTemplate.SelectedRows.Count != 1) return;

			if (Mode == NewDialogMode.Project)
			{
				ProjectTemplate pt = (tvTemplate.SelectedRows[0].GetExtraData<ProjectTemplate>("dt"));
				if (pt == null) return;

				if (!txtFileName.IsChangedByUser)
				{
					string projectNamePrefix = pt.Prefix;
					if (String.IsNullOrEmpty(projectNamePrefix))
					{
						projectNamePrefix = pt.Title.Replace(" ", String.Empty);
						// projectNamePrefix = "Project";
					}
					txtFileName.Text = projectNamePrefix + "1";
				}
			}
			else if (Mode == NewDialogMode.File)
			{
				DocumentTemplate pt = (tvTemplate.SelectedRows[0].GetExtraData<DocumentTemplate>("dt"));
				if (pt == null) return;
				if (!txtFileName.IsChangedByUser)
				{
					string projectNamePrefix = pt.Prefix;
					if (String.IsNullOrEmpty(projectNamePrefix))
					{
						projectNamePrefix = pt.Title.Replace(" ", String.Empty).Replace("/", String.Empty);
						// projectNamePrefix = "Project";
					}
					txtFileName.Text = projectNamePrefix + "1";
				}
			}
		}
		[EventHandler(nameof(tvTemplate), "RowActivated")]
		private void tvTemplate_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			cmdOK_Click(sender, e);
		}


		private void RefreshProjectTemplates(TreeModelRow row)
		{
			tmTemplate.Rows.Clear();

			List<ProjectTemplate> list = row.GetExtraData<List<ProjectTemplate>>("dts");
			if (list != null)
			{
				foreach (ProjectTemplate dt in list)
				{
					TreeModelRow lvi = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmTemplate.Columns[0], dt.Title),
						new TreeModelRowColumn(tmTemplate.Columns[1], dt.Description)
					});
					if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
					{
						// lvi.Image = Image.FromFile(dt.LargeIconImageFileName);
					}
					else
					{
						Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
					}
					lvi.SetExtraData<ProjectTemplate>("dt", dt);
					tmTemplate.Rows.Add(lvi);
				}
			}
		}
		private void RefreshDocumentTemplates(TreeModelRow row)
		{
			tmTemplate.Rows.Clear();

			List<DocumentTemplate> list = row.GetExtraData<List<DocumentTemplate>>("dts");
			if (list != null)
			{
				foreach (DocumentTemplate dt in list)
				{
					if (dt == null) continue;

					TreeModelRow lvi = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmTemplate.Columns[0], dt.Title),
						new TreeModelRowColumn(tmTemplate.Columns[1], dt.Description)
					});
					if (!String.IsNullOrEmpty(dt.LargeIconImageFileName))
					{
						// lvi.Image = Image.FromFile(dt.LargeIconImageFileName);
					}
					else
					{
						Console.Error.WriteLine("Large icon image not specified for template \"" + dt.Title + "\"");
					}
					lvi.SetExtraData<DocumentTemplate>("dt", dt);
					tmTemplate.Rows.Add(lvi);
				}
			}
		}
	}
}
