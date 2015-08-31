using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Executable
{
	public partial class ExecutableEditor : Editor
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

		public ExecutableEditor()
		{
			InitializeComponent();
			tv.PopulateSystemIcons();

			string[] names = Enum.GetNames(typeof(ExecutableSectionCharacteristics));
			Array values = (Enum.GetValues(typeof(ExecutableSectionCharacteristics)) as Array);

			for (int i = 0; i < names.Length; i++)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = names[i];
				lvi.Tag = (ExecutableSectionCharacteristics) values.GetValue(i);
				lvSectionCharacteristics.Items.Add(lvi);
			}

			SwitchTo(null);
		}

		private void SwitchTo(string name)
		{
			foreach (Control ctl in splitContainer1.Panel2.Controls)
			{
				if (name != null)
				{
					if (ctl.Name == "pnl" + name)
					{
						ctl.Enabled = true;
						ctl.Visible = true;
					}
					else
					{
						ctl.Visible = false;
						ctl.Enabled = false;
					}
				}
				else
				{
					ctl.Visible = false;
					ctl.Enabled = false;
				}
			}

			switch (name)
			{
				case "Section":
				{
					ExecutableSection section = (tv.SelectedNode.Tag as ExecutableSection);
					txtSectionName.Text = section.Name;
					foreach (ListViewItem lvi in lvSectionCharacteristics.Items)
					{
						ExecutableSectionCharacteristics value = (ExecutableSectionCharacteristics)lvi.Tag;
						lvi.Checked = ((section.Characteristics & value) == value);
					}
					break;
				}
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tv.Nodes.Clear();
			lvSections.Items.Clear();

			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null) return;

			TreeNode nodeSections = new TreeNode();
			nodeSections.Name = "nodeSections";
			nodeSections.Text = "Sections";
			nodeSections.ImageKey = "generic-folder-closed";
			nodeSections.SelectedImageKey = "generic-folder-closed";

			foreach (ExecutableSection section in executable.Sections)
			{
				TreeNode nodeSection = new TreeNode();
				nodeSection.Name = "nodeSection" + executable.Sections.IndexOf(section).ToString();
				nodeSection.Text = section.Name;
				nodeSection.Tag = section;
				nodeSections.Nodes.Add(nodeSection);

				ListViewItem lvi = new ListViewItem();
				lvi.Tag = section;
				lvi.Text = section.Name;
				lvi.SubItems.Add(section.PhysicalAddress.ToString());
				lvi.SubItems.Add(section.VirtualSize.ToString());
				lvSections.Items.Add(lvi);
			}
			tv.Nodes.Add(nodeSections);
		}

		private void mnuContextListViewSections_Opening(object sender, CancelEventArgs e)
		{
			mnuContextListViewSectionsDelete.Enabled = (lvSections.SelectedItems.Count > 0);
			mnuContextListViewSectionsExport.Enabled = (lvSections.SelectedItems.Count > 0);
		}

		private void mnuContextListViewSectionsExport_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in lvSections.SelectedItems)
			{
				ExecutableSection section = (lvi.Tag as ExecutableSection);

				SaveFileDialog sfd = new SaveFileDialog();
				sfd.FileName = section.Name;
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					section.Save(sfd.FileName);
				}
			}
		}

		private void mnuContextListViewSectionsImport_Click(object sender, EventArgs e)
		{
			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null) return;

			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				ExecutableSection section = new ExecutableSection();
				section.Name = System.IO.Path.GetFileName(ofd.FileName);
				section.Load(ofd.FileName);

				BeginEdit();
				executable.Sections.Add(section);
				EndEdit();

				ListViewItem lvi = new ListViewItem();
				lvi.Tag = section;
				lvi.Text = System.IO.Path.GetFileName(section.Name);
				lvi.SubItems.Add((0).ToString());
				lvi.SubItems.Add((0).ToString());

				lvSections.Items.Add(lvi);
			}
		}

		private void mnuContextListViewSectionsDelete_Click(object sender, EventArgs e)
		{
			ExecutableObjectModel executable = (ObjectModel as ExecutableObjectModel);
			if (executable == null) return;

			if (lvSections.SelectedItems.Count == 0) return;
			if (MessageBox.Show("Are you sure you want to delete the selected sections?", "Delete Sections", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

			while (lvSections.SelectedItems.Count > 0)
			{
				ExecutableSection section = (lvSections.SelectedItems[0].Tag as ExecutableSection);
				if (section != null) executable.Sections.Remove(section);
				lvSections.SelectedItems[0].Remove();
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (tv.SelectedNode != null)
			{
				if (tv.SelectedNode.Tag is ExecutableSection)
				{
					SwitchTo("Section");
				}
				else
				{
					SwitchTo(tv.SelectedNode.Name.Substring("node".Length));
				}
			}
			else
			{
				SwitchTo(null);
			}
		}
	}
}
