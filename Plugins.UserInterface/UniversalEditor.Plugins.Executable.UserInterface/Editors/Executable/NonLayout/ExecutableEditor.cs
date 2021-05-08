//
//  ExecutableEditor.cs - provides an Editor for the ExecutableObjectModel
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
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.UserInterface;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.UserInterface.Controls.ListView;

namespace UniversalEditor.Plugins.Executable.UserInterface.Editors.Executable
{
	/// <summary>
	/// Provides an <see cref="Editor" /> for the <see cref="ExecutableObjectModel" />.
	/// </summary>
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

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		private ListViewControl tvSections = null;
		private DefaultTreeModel tmSections = null;
		private TabContainer tbs = null;

		private Label lblAssemblyName = null;
		private TextBox txtAssemblyName = null;
		private Label lblAssemblyVersion = null;
		private TextBox txtAssemblyVersion = null;

		private DefaultTreeModel tmOtherInformation = null;
		private ManagedAssemblyPanel pnlManagedAssembly = null;

		public ExecutableEditor()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			tmSections = new DefaultTreeModel(new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) });
			tvSections = new ListViewControl();
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

			TabPage tabVersion = new TabPage("Version");
			tabVersion.Layout = new GridLayout();

			Label lblFileVersion = new Label();
			lblFileVersion.Text = "File version:";
			lblFileVersion.HorizontalAlignment = HorizontalAlignment.Left;
			tabVersion.Controls.Add(lblFileVersion, new GridLayout.Constraints(0, 0));

			TextBox txtFileVersion = new TextBox();
			tabVersion.Controls.Add(txtFileVersion, new GridLayout.Constraints(0, 1, 1, 1, ExpandMode.Horizontal));

			Label lblDescription = new Label();
			lblDescription.Text = "Description:";
			lblDescription.HorizontalAlignment = HorizontalAlignment.Left;
			tabVersion.Controls.Add(lblDescription, new GridLayout.Constraints(1, 0));

			TextBox txtDescription = new TextBox();
			tabVersion.Controls.Add(txtDescription, new GridLayout.Constraints(1, 1, 1, 1, ExpandMode.Horizontal));

			Label lblCopyright = new Label();
			lblCopyright.Text = "Copyright:";
			lblCopyright.HorizontalAlignment = HorizontalAlignment.Left;
			tabVersion.Controls.Add(lblCopyright, new GridLayout.Constraints(2, 0));

			TextBox txtCopyright = new TextBox();
			tabVersion.Controls.Add(txtCopyright, new GridLayout.Constraints(2, 1, 1, 1, ExpandMode.Horizontal));

			Label lblOtherInformationLabel = new Label();
			lblOtherInformationLabel.Text = "Other version information:";
			lblOtherInformationLabel.HorizontalAlignment = HorizontalAlignment.Left;
			tabVersion.Controls.Add(lblOtherInformationLabel, new GridLayout.Constraints(3, 0, 1, 2));

			tmOtherInformation = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });

			ListViewControl lvOtherInformation = new ListViewControl();
			lvOtherInformation.Model = tmOtherInformation;
			lvOtherInformation.Columns.Add(new ListViewColumnText(tmOtherInformation.Columns[0], "Name"));
			lvOtherInformation.Columns.Add(new ListViewColumnText(tmOtherInformation.Columns[1], "Value"));
			tabVersion.Controls.Add(lvOtherInformation, new GridLayout.Constraints(4, 0, 1, 2, ExpandMode.Both));

			tbs.TabPages.Add(tabVersion);

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
			pnlMetadata.Controls.Add(txtAssemblyName, new GridLayout.Constraints(0, 1, 1, 1, ExpandMode.Horizontal));
			pnlMetadata.Controls.Add(lblAssemblyVersion, new GridLayout.Constraints(1, 0));
			pnlMetadata.Controls.Add(txtAssemblyVersion, new GridLayout.Constraints(1, 1, 1, 1, ExpandMode.Horizontal));

			tabManagedAssembly.Controls.Add(pnlMetadata, new BoxLayout.Constraints(false, true));

			pnlManagedAssembly = new ManagedAssemblyPanel();
			tabManagedAssembly.Controls.Add(pnlManagedAssembly, new BoxLayout.Constraints(true, true));

			tbs.TabPages.Add(tabManagedAssembly);

			this.Controls.Add(tbs, new BoxLayout.Constraints(true, true));
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Context.AttachCommandEventHandler("ExecutableEditor_ContextMenu_Sections_Selected_CopyTo", ContextMenu_CopyTo_Click);
			Context.AttachCommandEventHandler("ExecutableEditor_ContextMenu_Sections_Selected_Add_ExistingItem", ContextMenu_Sections_Selected_Add_ExistingItem_Click);
		}

		private void ContextMenu_Sections_Selected_Add_ExistingItem_Click(object sender, EventArgs e)
		{
			ExecutableObjectModel exe = (ObjectModel as ExecutableObjectModel);
			if (exe == null)
			{
				return;
			}

			FileDialog dlg = new FileDialog();
			dlg.Mode = FileDialogMode.Open;
			dlg.MultiSelect = true;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				BeginEdit();
				for (int i = 0; i < dlg.SelectedFileNames.Count; i++)
				{
					string fn = dlg.SelectedFileNames[i];
					if (!System.IO.File.Exists(fn))
						continue;

					ExecutableSection section = new ExecutableSection();
					section.Name = System.IO.Path.GetFileName(fn);
					section.Data = System.IO.File.ReadAllBytes(fn);
					exe.Sections.Add(section);

					TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tmSections.Columns[0], section.Name),
						new TreeModelRowColumn(tmSections.Columns[1], section.PhysicalAddress.ToString()),
						new TreeModelRowColumn(tmSections.Columns[2], section.VirtualAddress.ToString()),
						new TreeModelRowColumn(tmSections.Columns[3], section.VirtualSize.ToString())
					});
					row.SetExtraData<ExecutableSection>("section", section);
					tmSections.Rows.Add(row);
				}
				EndEdit();
			}
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
				tvSections.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Sections_Selected";
			}
			else
			{
				tvSections.ContextMenuCommandID = "ExecutableEditor_ContextMenu_Sections_Unselected";
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
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmSections.Columns[0], section.Name),
					new TreeModelRowColumn(tmSections.Columns[1], section.PhysicalAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[2], section.VirtualAddress.ToString()),
					new TreeModelRowColumn(tmSections.Columns[3], section.VirtualSize.ToString())
				});
				row.SetExtraData<ExecutableSection>("section", section);
				tmSections.Rows.Add(row);
			}

			if (executable.ManagedAssembly != null)
			{
				tbs.TabPages[1].Visible = true;

				txtAssemblyName.Text = executable.ManagedAssembly.GetName().Name;
				txtAssemblyVersion.Text = executable.ManagedAssembly.GetName().Version.ToString();

				pnlManagedAssembly.Assembly = executable.ManagedAssembly;
			}
		}
	}
}
