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
				TreeNode nodeSection = new TreeNode(section.Name);
				nodeSection.Name = "nodeSection" + executable.Sections.IndexOf(section).ToString();
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
				section.Name = ofd.FileName;
				section.Load(ofd.FileName);

				BeginEdit();
				executable.Sections.Add(section);
				EndEdit();

				ListViewItem lvi = new ListViewItem();
				lvi.Tag = section;
				lvi.Text = section.Name;
				lvi.SubItems.Add((0).ToString());
				lvi.SubItems.Add((0).ToString());

				lvSections.Items.Add(lvi);
			}
		}
	}
}
