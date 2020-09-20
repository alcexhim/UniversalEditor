//
//  AboutDialog.cs - provides a UWT ContainerLayout-based CustomDialog for displaying information about the installed Universal Editor platform components
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using MBS.Framework.UserInterface.Controls.ListView;

namespace UniversalEditor.UserInterface.Dialogs
{
	/// <summary>
	/// Provides a UWT <see cref="ContainerLayoutAttribute" />-based <see cref="CustomDialog" /> for displaying information about the installed Universal Editor
	/// platform components.
	/// </summary>
	[ContainerLayout("~/Dialogs/AboutDialog.glade", "GtkDialog")]
	public class AboutDialog : Dialog
	{
		private Label lblApplicationTitle;
		private Label lblApplicationVersion;
		private Label lblApplicationCopyright;
		private TextBox txtLicense;
		private ListViewControl tvComponents;
		private DefaultTreeModel tmComponents;

		public AboutDialog()
		{
			Buttons.Add(new Button(StockType.Close, DialogResult.Cancel));
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Text = String.Format(Text, Application.Title);
			lblApplicationTitle.Text = Application.Title;
			lblApplicationVersion.Text = String.Format("Version {0}", System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());

			object[] atts = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyCopyrightAttribute), false);
			if (atts.Length > 0)
			{
				lblApplicationCopyright.Text = (atts[0] as System.Reflection.AssemblyCopyrightAttribute).Copyright;
			}
			InitializeInstalledComponentsTab();
		}

		private void InitializeInstalledComponentsTab()
		{
			#region Object Models
			{
				TreeModelRow tnParent = null;
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
						if (tmComponents.Rows.Contains(title))
						{
							tnParent = tmComponents.Rows[title];
						}
						else
						{
							tnParent = new TreeModelRow(
							new TreeModelRowColumn[]
							{
								new TreeModelRowColumn(tmComponents.Columns[0], title)
							}); // LibraryClosed
							tnParent.Name = title;
							tmComponents.Rows.Add(tnParent);
						}
					}
					else
					{
						if (tnParent.Rows.Contains(title))
						{
							tnParent = tnParent.Rows[title];
						}
						else
						{
							tnParent = new TreeModelRow(
							new TreeModelRowColumn[]
							{
								new TreeModelRowColumn(tmComponents.Columns[0], title)
							}); // LibraryClosed
							tnParent.Name = title;
							tnParent.Rows.Add(tnParent);
						}
					}
					tnParent.SetExtraData<System.Reflection.Assembly>("asm", omr.Type.Assembly);

					foreach (string s in omr.Path)
					{
						if (tnParent == null)
						{
							if (tmComponents.Rows.Contains(s))
							{
								tnParent = tmComponents.Rows[s];
							}
							else
							{
								tnParent = new TreeModelRow(new TreeModelRowColumn[]
								{
								new TreeModelRowColumn(tmComponents.Columns[0], s)
								}); //"generic-folder-closed", "generic-folder-closed");
								tnParent.Name = s;
								tmComponents.Rows.Add(tnParent);
							}
						}
						else
						{
							if (tnParent.Rows.Contains(s))
							{
								tnParent = tnParent.Rows[s];
							}
							else
							{
								TreeModelRow tnNew = new TreeModelRow(new TreeModelRowColumn[]
								{
								new TreeModelRowColumn(tmComponents.Columns[0], s)
								}); //"generic-folder-closed", "generic-folder-closed");
								tnNew.Name = s;
								tnParent.Rows.Add(tnNew);
								tnParent = tnNew;
							}
						}

						if (Array.IndexOf<string>(omr.Path, s) == omr.Path.Length - 1)
						{
							// tnParent.ImageKey = "ObjectModel";
							// tnParent.SelectedImageKey = "ObjectModel";
							tnParent.SetExtraData<ObjectModelReference>("omr", omr);

							DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(omr);
							if (dfrs.Length > 0)
							{
								TreeModelRow tnParentDataFormats = null;
								if (!tnParent.Rows.Contains("DataFormats"))
								{
									tnParentDataFormats = new TreeModelRow(new TreeModelRowColumn[] {
										new TreeModelRowColumn(tmComponents.Columns[0], "DataFormats")
									});
									tnParentDataFormats.Name = "DataFormats";
									// tnParentDataFormats.Text = "DataFormats";
									// tnParentDataFormats.ImageKey = "generic-folder-closed";
									// tnParentDataFormats.SelectedImageKey = "generic-folder-closed";
									tnParent.Rows.Add(tnParentDataFormats);
								}
								else
								{
									tnParentDataFormats = tnParent.Rows["DataFormats"];
								}
								foreach (DataFormatReference dfr in dfrs)
								{
									if (!tnParentDataFormats.Rows.Contains(dfr.Title))
									{
										string[] deets = dfr.GetDetails();

										TreeModelRow tnDataFormat = new TreeModelRow(new TreeModelRowColumn[]
										{
											new TreeModelRowColumn(tmComponents.Columns[0], deets.Length > 0 ? deets[0] : String.Empty),
											new TreeModelRowColumn(tmComponents.Columns[1], deets.Length > 1 ? deets[1] : String.Empty)
										});
										// "DataFormat", "DataFormat"
										tnDataFormat.SetExtraData<DataFormatReference>("dfr", dfr);
										tnParentDataFormats.Rows.Add(tnDataFormat);
									}
								}
							}

							EditorReference[] reditors = UniversalEditor.UserInterface.Common.Reflection.GetAvailableEditors(omr);
							if (reditors.Length > 0)
							{
								TreeModelRow tnParentEditors = null;
								if (!tnParent.Rows.Contains("Editors"))
								{
									tnParentEditors = new TreeModelRow(new TreeModelRowColumn[]
									{
										new TreeModelRowColumn(tmComponents.Columns[0], "Editors")
									});
									tnParentEditors.Name = "Editors";
									// tnParentEditors.ImageKey = "generic-folder-closed";
									// tnParentEditors.SelectedImageKey = "generic-folder-closed";
									tnParent.Rows.Add(tnParentEditors);
								}
								else
								{
									tnParentEditors = tnParent.Rows["Editors"];
								}
								foreach (EditorReference reditor in reditors)
								{
									if (!tnParentEditors.Rows.Contains(reditor.Title))
									{
										TreeModelRow tnEditor = new TreeModelRow(new TreeModelRowColumn[]
										{
											new TreeModelRowColumn(tmComponents.Columns[0], reditor.Title)
										});
										tnParentEditors.Rows.Add(tnEditor); // Editor
										tnEditor.SetExtraData<EditorReference>("er", reditor);
									}
								}
							}
						}
					}
					tnParent = null;
				}
			}
			#endregion
		}
	}
}
