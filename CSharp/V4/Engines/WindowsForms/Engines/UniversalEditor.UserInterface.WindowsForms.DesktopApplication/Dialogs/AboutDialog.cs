using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class AboutDialog : Glue.GlueWindow
	{
		public AboutDialog()
		{
			InitializeComponent();
			InitializeImageLists();
			InitializeInstalledComponentsTab();

			Font = SystemFonts.MenuFont;
			lblApplicationTitle.Font = new Font(Font, FontStyle.Bold);
			lblApplicationTitle.Text = Engine.CurrentEngine.DefaultLanguage.GetStringTableEntry("ApplicationTitle", "Universal Editor");
			lblPlatform.Visible = (lblApplicationTitle.Text != "Universal Editor");

			this.Text = "About " + lblApplicationTitle.Text;

			lblVersion.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		private void InitializeImageLists()
		{
			IconMethods.PopulateSystemIcons(ref imlSmallIcons);
		}
		private void InitializeInstalledComponentsTab()
		{
			tvComponents.Nodes.Clear();

			#region Object Models
			{
				TreeNode tnParent = null;
				ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
				foreach (ObjectModelReference omr in omrs)
				{
					string title = omr.Type.Assembly.GetName().Name;
					object[] atts = omr.Type.Assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
					if (atts.Length > 0)
					{
						title = (atts[0] as System.Reflection.AssemblyTitleAttribute).Title;
					}

					if (tnParent == null)
					{
						if (tvComponents.Nodes.ContainsKey(title))
						{
							tnParent = tvComponents.Nodes[title];
						}
						else
						{
							tnParent = tvComponents.Nodes.Add(title, title, "LibraryClosed");
						}
					}
					else
					{
						if (tvComponents.Nodes.ContainsKey(title))
						{
							tnParent = tnParent.Nodes[title];
						}
						else
						{
							tnParent = tnParent.Nodes.Add(title, title, "LibraryClosed", "LibraryClosed");
						}
					}
					tnParent.Tag = omr.Type.Assembly;

					foreach (string s in omr.Path)
					{
						if (tnParent == null)
						{
							if (tvComponents.Nodes.ContainsKey(s))
							{
								tnParent = tvComponents.Nodes[s];
							}
							else
							{
								tnParent = tvComponents.Nodes.Add(s, s, "generic-folder-closed", "generic-folder-closed");
							}
						}
						else
						{
							if (tnParent.Nodes.ContainsKey(s))
							{
								tnParent = tnParent.Nodes[s];
							}
							else
							{
								tnParent = tnParent.Nodes.Add(s, s, "generic-folder-closed", "generic-folder-closed");
							}
						}

						if (Array.IndexOf<string>(omr.Path, s) == omr.Path.Length - 1)
						{
							tnParent.ImageKey = "ObjectModel";
							tnParent.SelectedImageKey = "ObjectModel";
							tnParent.Tag = omr;

							DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(omr);
							if (dfrs.Length > 0)
							{
								TreeNode tnParentDataFormats = null;
								if (!tnParent.Nodes.ContainsKey("DataFormats"))
								{
									tnParentDataFormats = new TreeNode();
									tnParentDataFormats.Name = "DataFormats";
									tnParentDataFormats.Text = "DataFormats";
									tnParentDataFormats.ImageKey = "generic-folder-closed";
									tnParentDataFormats.SelectedImageKey = "generic-folder-closed";
									tnParent.Nodes.Add(tnParentDataFormats);
								}
								else
								{
									tnParentDataFormats = tnParent.Nodes["DataFormats"];
								}
								foreach (DataFormatReference dfr in dfrs)
								{
									if (!tnParentDataFormats.Nodes.ContainsKey(dfr.Title))
									{
										tnParentDataFormats.Nodes.Add(dfr.Title, dfr.Title, "DataFormat", "DataFormat");
										tnParentDataFormats.Nodes[tnParentDataFormats.Nodes.Count - 1].Tag = dfr;
									}
								}
							}

							EditorReference[] reditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(omr);
							if (reditors.Length > 0)
							{
								TreeNode tnParentEditors = null;
								if (!tnParent.Nodes.ContainsKey("Editors"))
								{
									tnParentEditors = new TreeNode();
									tnParentEditors.Name = "Editors";
									tnParentEditors.Text = "Editors";
									tnParentEditors.ImageKey = "generic-folder-closed";
									tnParentEditors.SelectedImageKey = "generic-folder-closed";
									tnParent.Nodes.Add(tnParentEditors);
								}
								else
								{
									tnParentEditors = tnParent.Nodes["Editors"];
								}
								foreach (EditorReference reditor in reditors)
								{
									if (!tnParentEditors.Nodes.ContainsKey(reditor.Title))
									{
										tnParentEditors.Nodes.Add(reditor.Title, reditor.Title, "Editor", "Editor");
										tnParentEditors.Nodes[tnParentEditors.Nodes.Count - 1].Tag = reditor;
									}
								}
							}
						}
					}
					tnParent = null;
				}
			}
			#endregion
			tvComponents.Sort();
		}

		private void tvComponents_AfterExpand(object sender, TreeViewEventArgs e)
		{
			UpdateNodeImage(e.Node);
		}
		private void tvComponents_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			UpdateNodeImage(e.Node);
		}

		private void UpdateNodeImage(TreeNode node)
		{
			if (node == null) return;
			switch (node.ImageKey)
			{
				case "LibraryClosed":
				{
					node.ImageKey = "LibraryOpen";
					node.SelectedImageKey = "LibraryOpen";
					break;
				}
				case "LibraryOpen":
				{
					node.ImageKey = "LibraryClosed";
					node.SelectedImageKey = "LibraryClosed";
					break;
				}
				case "generic-folder-closed":
				{
					node.ImageKey = "generic-folder-open";
					node.SelectedImageKey = "generic-folder-open";
					break;
				}
				case "generic-folder-open":
				{
					node.ImageKey = "generic-folder-closed";
					node.SelectedImageKey = "generic-folder-closed";
					break;
				}
			}
		}

		private void tvComponents_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node == null) return;

			pnlAssemblyInfo.Visible = false;
			pnlAssemblyInfo.Enabled = false;

			pnlObjectModelInfo.Visible = false;
			pnlObjectModelInfo.Enabled = false;

			pnlDataFormatInfo.Visible = false;
			pnlDataFormatInfo.Enabled = false;

			if (e.Node.Tag is System.Reflection.Assembly)
			{
				System.Reflection.Assembly asm = (e.Node.Tag as System.Reflection.Assembly);

				txtAssemblyFullName.Text = asm.FullName;
				txtAssemblyLocation.Text = asm.Location;

				object[] atts = asm.GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false);
				if (atts.Length > 0)
				{
					txtAssemblyDescription.Text = ((atts[0] as System.Reflection.AssemblyDescriptionAttribute).Description);
				}

				pnlAssemblyInfo.Enabled = true;
				pnlAssemblyInfo.Visible = true;
			}
			else if (e.Node.Tag is ObjectModelReference)
			{
				ObjectModelReference omr = (e.Node.Tag as ObjectModelReference);
				txtObjectModelID.Text = omr.ID.ToString("B");
				if (omr.TypeName == null)
				{
					txtObjectModelTypeName.Text = "(null)";
				}
				else
				{
					txtObjectModelTypeName.Text = omr.TypeName;
				}
				txtObjectModelTitle.Text = omr.Title;

				pnlObjectModelInfo.Enabled = true;
				pnlObjectModelInfo.Visible = true;
			}
			else if (e.Node.Tag is DataFormatReference)
			{
				DataFormatReference dfr = (e.Node.Tag as DataFormatReference);
				txtDataFormatID.Text = dfr.ID.ToString("B");
				if (dfr.Type != null)
				{
					txtDataFormatTypeName.Text = dfr.Type.FullName;
				}
				else
				{
					txtDataFormatTypeName.Text = "(null)";
				}

				lvDataFormatFilters.Items.Clear();

				Association[] assocs = Association.FromCriteria(new AssociationCriteria() { DataFormat = dfr });
				foreach (Association assoc in assocs)
				{
					foreach (DataFormatFilter filter in assoc.Filters)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = filter.Title;

						StringBuilder sb = new StringBuilder();
						foreach (string s in filter.FileNameFilters)
						{
							sb.Append(s);
							if (filter.FileNameFilters.IndexOf(s) < filter.FileNameFilters.Count - 1)
							{
								sb.Append(", ");
							}
						}
						lvi.SubItems.Add(sb.ToString());
						lvDataFormatFilters.Items.Add(lvi);
					}
				}
				foreach (string ct in dfr.ContentTypes)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = ct;
					lvDataFormatContentTypes.Items.Add(lvi);
				}

				pnlDataFormatInfo.Enabled = true;
				pnlDataFormatInfo.Visible = true;
			}
		}

		private void cmdOpenContainingFolder_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(txtAssemblyLocation.Text));
		}

	}
}