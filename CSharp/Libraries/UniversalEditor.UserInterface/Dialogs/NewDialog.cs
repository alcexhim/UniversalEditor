//
//  NewFileDialog.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Dialogs
{
	public enum NewDialogMode
	{
		File,
		Project
	}

	[ContainerLayout("~/Dialogs/NewDialog.glade", "GtkDialog")]
	public class NewDialog2 : Dialog
	{
		private TextBox txtFileName = null;
		private TextBox txtFilePath = null;

		private CheckBox chkAddToSolution = null;
		private TextBox txtSolutionName = null;

		private ListView tvObjectModel = new ListView();
		private DefaultTreeModel tmObjectModel = null;

		private ListView tvTemplate = new ListView();
		private DefaultTreeModel tmTemplate = null;
		private TextBox txtSearch = null;

		public NewDialog2()
		{
			tmObjectModel = new DefaultTreeModel(new Type[] { typeof(string) });
			tmTemplate = new DefaultTreeModel(new Type[] { typeof(string) });

			tvObjectModel.Model = tmObjectModel;
			tvTemplate.Model = tmTemplate;

			tvObjectModel.Columns.Add(new ListViewColumnText(tmObjectModel.Columns[0], "Name"));
			tvObjectModel.HeaderStyle = ColumnHeaderStyle.None;

			tvTemplate.Columns.Add(new ListViewColumnText(tmTemplate.Columns[0], "Name"));
			tvTemplate.HeaderStyle = ColumnHeaderStyle.None;

			this.InitializeObjectModelTreeView();
			Buttons[0].Enabled = false;
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
			}
		}
		private void InitializeObjectModelTreeViewRow(DefaultTreeModel tm, TreeModelRow rp, string[] path, int index)
		{
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

			InitializeObjectModelTreeViewRow(tm, row, path, index + 1);
		}

		[EventHandler("cmdOK", "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			// holy crapola this actually works!...
			if (String.IsNullOrEmpty(this.txtFileName.Text))
			{
				MessageDialog.ShowDialog("Please enter a file name", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);

				// ... but without DialogResult, something gets
				// corrupted, and with DialogResult, there is no
				// way to cancel the dialog close event... what do?
				return;
			}
			this.Destroy();
		}
	}

	public partial class NewDialog : Dialog
	{
		public NewDialog()
		{
			this.InitializeComponent();
		}

		public NewDialogMode Mode { get; set; }
		public Template SelectedItem { get; set; }
		public bool CombineObjects { get; set; } = false;

		public string SolutionTitle { get; set; }
		public string ProjectTitle { get; set; }

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);

			switch (Mode)
			{
				case NewDialogMode.File:
				{
					this.Text = "New File";
					break;
				}
				case NewDialogMode.Project:
				{
					this.Text = "New Project";
					break;
				}
			}

			tvTemplate.RowActivated += tvTemplate_RowActivated;

			InitializeTreeView();
		}

		private void tvTemplate_RowActivated (object sender, ListViewRowActivatedEventArgs e)
		{
			cmdOK_Click (sender, e);
		}


		private void InitializeTreeView()
		{
			switch (this.Mode)
			{
				case NewDialogMode.File:
				{
					InitializeDocumentTemplateTreeView();
					break;
				}
				case NewDialogMode.Project:
				{
					InitializeProjectTemplateTreeView();
					break;
				}
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
								Console.WriteLine("ue: templates debug: creating a new project template list for " + String.Join("/", dt.Path));
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

			if (tmTemplate.Rows.Count > 0)
			{
				tvTemplate.SelectedRows.Clear();
				tvTemplate.Select(tmTemplate.Rows[0]);

				tvTemplate_SelectionChanged(null, EventArgs.Empty);
				Buttons[0].Enabled = true;
			}
			else
			{
				Buttons[0].Enabled = false;
			}
		}

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
					string projectNamePrefix = pt.ProjectNamePrefix;
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
					// txtFileName.Text = projectNamePrefix + "1";
				}
			}
		}
		
		private void InitializeObjectModelTreeView()
		{
			tmObjectModel.Rows.Clear();

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
					Console.WriteLine("ue: templates debug: creating a new document template list for " + String.Join("/", path));
					dts = new List<DocumentTemplate>();
					row.SetExtraData<List<DocumentTemplate>>("dts", dts);
				}

				DocumentTemplate dtEmpty = new DocumentTemplate();
				dtEmpty.ObjectModelReference = omr;
				dtEmpty.Title = String.Format("Blank {0} Document", path[path.Length - 1]);
				dts.Add(dtEmpty);
			}

			InitializeObjectModelTreeViewRow(tm, row, omr, index + 1);
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
								Console.WriteLine("ue: templates debug: creating a new document template list for " + String.Join("/", dt.Path));
								dts = new List<DocumentTemplate>();
								tn.SetExtraData<List<DocumentTemplate>>("dts", dts);
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
					lvi.SetExtraData<DocumentTemplate>("dt", dt);
					tmTemplate.Rows.Add(lvi);
				}
			}

			if (tmTemplate.Rows.Count > 0)
			{
				tvTemplate.SelectedRows.Clear();
				tvTemplate.Select(tmTemplate.Rows[0]);

				tvTemplate_SelectionChanged(null, EventArgs.Empty);
				Buttons[0].Enabled = true;
			}
			else
			{
				Buttons[0].Enabled = false;
			}

			InitializeObjectModelTreeView();
		}

		

		private void cmdOK_Click(object sender, EventArgs e)
		{
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
					ProjectTemplate template =  tvTemplate.SelectedRows[0].GetExtraData<ProjectTemplate>("dt");
					if (template.ProjectType != null)
					{
						if (template.ProjectType.Variables.Count > 0)
						{
							CustomOption.CustomOptionCollection coll = new CustomOption.CustomOptionCollection();
							foreach (ProjectTypeVariable ptv in template.ProjectType.Variables)
							{
								switch (ptv.Type)
								{
									case ProjectTypeVariableType.Choice:
									{
										List<CustomOptionFieldChoice> choices = new List<CustomOptionFieldChoice>();
										foreach (KeyValuePair<string, object> kvp in ptv.ValidValues)
										{
											choices.Add(new CustomOptionFieldChoice(kvp.Key, kvp.Value, kvp.Value == ptv.DefaultValue));
										}

										CustomOptionChoice co = new CustomOptionChoice(ptv.Name, ptv.Title, true, choices.ToArray());
										coll.Add(co);
										break;
									}
									case ProjectTypeVariableType.FileOpen:
									{
										CustomOptionFile co = new CustomOptionFile(ptv.Name, ptv.Title);
										co.DialogMode = CustomOptionFileDialogMode.Open;
										coll.Add(co);
										break;
									}
									case ProjectTypeVariableType.FileSave:
									{
										CustomOptionFile co = new CustomOptionFile(ptv.Name, ptv.Title);
										co.DialogMode = CustomOptionFileDialogMode.Save;
										coll.Add(co);
										break;
									}
								}
							}

							if (!Engine.CurrentEngine.ShowCustomOptionDialog(ref coll, template.ProjectType.Title + " properties"))
							{
								return;
							}

							foreach (CustomOption co in coll)
							{
								// template.ProjectType.Variables[co.PropertyName].Value = co.GetValue().ToString();
								// TODO: Figure out how to assign variable values to the newly
								// created project from the template
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
			this.Close();
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

		private void txtSearch_Changed(object sender, EventArgs e)
		{
			InitializeTreeView();
		}

		private void tvObjectModel_SelectionChanged(object sender, EventArgs e)
		{
			if (tvObjectModel.SelectedRows.Count < 1) return;
			if (Mode == NewDialogMode.File)
			{
				RefreshDocumentTemplates(tvObjectModel.SelectedRows[0]);
			}
			else if (Mode == NewDialogMode.Project)
			{
				RefreshProjectTemplates(tvObjectModel.SelectedRows[0]);
			}
		}
	}
}
