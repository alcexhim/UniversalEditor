//
//  ExecutableEditor.cs
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
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.UserInterface;
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Input.Mouse;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	public class ExecutableEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ExecutableObjectModel));
			}
			return _er;
		}

		protected override EditorSelection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		private ListView tvSections = null;
		private DefaultTreeModel tmSections = null;
		private TabContainer tbs = null;

		private Label lblAssemblyName = null;
		private TextBox txtAssemblyName = null;
		private Label lblAssemblyVersion = null;
		private TextBox txtAssemblyVersion = null;

		public ExecutableEditor()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			tmSections = new DefaultTreeModel(new Type[] { typeof(string),typeof(string),typeof(string), typeof(string) });
			tvSections = new ListView();
			tvSections.Model = tmSections;

			tvSections.Columns.Add(new ListViewColumnText(tmSections.Columns[0], "Name"));
			tvSections.Columns.Add(new ListViewColumnText(tmSections.Columns[1], "Physical address"));
			tvSections.Columns.Add(new ListViewColumnText(tmSections.Columns[2], "Virtual address"));
			tvSections.Columns.Add(new ListViewColumnText(tmSections.Columns[3], "Size"));
			tvSections.BeforeContextMenu += tvSections_BeforeContextMenu;

			tbs = new TabContainer();
			TabPage tabSections = new TabPage("Sections (0)");
			tabSections.Layout = new BoxLayout(Orientation.Vertical);

			tabSections.Controls.Add(tvSections, new BoxLayout.Constraints(true, true));
			tbs.TabPages.Add(tabSections);


			TabPage tabManagedAssembly = new TabPage("Managed Assembly");
			tabManagedAssembly.Layout = new BoxLayout(Orientation.Vertical);

			lblAssemblyName = new Label("Assembly name: ");
			lblAssemblyName.HorizontalAlignment = HorizontalAlignment.Left;

			txtAssemblyName = new TextBox();

			lblAssemblyVersion = new Label("Assembly version: ");
			lblAssemblyVersion.HorizontalAlignment = HorizontalAlignment.Left;

			txtAssemblyVersion = new TextBox();

			Container pnlMetadata = new Container();
			pnlMetadata.Layout = new GridLayout();

			pnlMetadata.Controls.Add(lblAssemblyName, new GridLayout.Constraints(0, 0));
			pnlMetadata.Controls.Add(txtAssemblyName, new GridLayout.Constraints(0, 1));
			pnlMetadata.Controls.Add(lblAssemblyVersion, new GridLayout.Constraints(1, 0));
			pnlMetadata.Controls.Add(txtAssemblyVersion, new GridLayout.Constraints(1, 1));

			tabManagedAssembly.Controls.Add(pnlMetadata);

			tbs.TabPages.Add(tabManagedAssembly);


			this.contextMenuItemSelected = new Menu();
			this.contextMenuItemSelected.Items.AddRange(new UniversalWidgetToolkit.MenuItem[]
			{
				new UniversalWidgetToolkit.CommandMenuItem("_Copy to", null, ContextMenu_CopyTo_Click)
			});


			this.Controls.Add(tbs, new BoxLayout.Constraints(true, true));
		}

		private void ContextMenu_CopyTo_Click(object sender, EventArgs e)
		{
			FileDialog fd = new FileDialog();
			if (tvSections.SelectedRows.Count == 1)
			{
				fd.Mode = FileDialogMode.Save;

				ExecutableSection section = tvSections.SelectedRows[0].GetExtraData<ExecutableSection>("section");
				fd.SelectedFileNames.Add(section.Name);

				if (fd.ShowDialog() == DialogResult.OK)
				{
					System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1], section.Data);
				}
			}
			else if (tvSections.SelectedRows.Count > 1)
			{
				// select a folder
				fd.Mode = FileDialogMode.SelectFolder;

				if (fd.ShowDialog() == DialogResult.OK)
				{
					foreach (TreeModelRow row in tvSections.SelectedRows)
					{
						ExecutableSection section = tvSections.SelectedRows[0].GetExtraData<ExecutableSection>("section");
						System.IO.File.WriteAllBytes(fd.SelectedFileNames[fd.SelectedFileNames.Count - 1] + System.IO.Path.DirectorySeparatorChar.ToString() + section.Name, section.Data);
					}
				}
			}
		}

		private Menu contextMenuItemSelected = null;
		void tvSections_BeforeContextMenu(object sender, EventArgs e)
		{
			bool selected = tvSections.SelectedRows.Count > 0;
			if (e is MouseEventArgs)
			{
				MouseEventArgs ee = (e as MouseEventArgs);
				ListViewHitTestInfo lvih = tvSections.HitTest(ee.X, ee.Y);
				if (lvih.Row == null)
					selected = false;
			}

			if (selected)
			{
				tvSections.ContextMenu = contextMenuItemSelected;
			}
			else
			{
				tvSections.ContextMenu = null;
			}
		}


		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			// tv.Nodes.Clear();
			// lvSections.Items.Clear();

			tbs.TabPages[0].Text = "Sections (0)";
			tbs.TabPages[1].Visible = false;

			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null) return;

			tbs.TabPages[0].Text = "Sections (" + executable.Sections.Count.ToString() + ")";

			foreach (ExecutableSection section in executable.Sections)
			{
				tmSections.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmSections.Columns[0], section.Name),
					new TreeModelRowColumn(tmSections.Columns[1], section.PhysicalAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[2], section.VirtualAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[3], section.VirtualSize.ToString())
				}));
				tmSections.Rows[tmSections.Rows.Count - 1].SetExtraData<ExecutableSection>("section", section);
			}

			if (executable.ManagedAssembly != null)
			{
				tbs.TabPages[1].Visible = true;

				txtAssemblyName.Text = executable.ManagedAssembly.GetName().Name;
				txtAssemblyVersion.Text = executable.ManagedAssembly.GetName().Version.ToString();
			}
		}
	}
}
