using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript;
using UniversalEditor.Dialogs.Setup.Microsoft.ACME.BootstrapScript;

namespace UniversalEditor.Editors.Setup.Microsoft.ACME.BootstrapScript
{
	public partial class BootstrapScriptEditor : Editor
	{
		public BootstrapScriptEditor()
		{
			InitializeComponent();
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Bootstrap Script Editor";
				_er.SupportedObjectModels.Add(typeof(BootstrapScriptObjectModel));
			}
			return _er;
		}

		private void chkRequire31_CheckedChanged(object sender, EventArgs e)
		{
			txtRequire31.ReadOnly = !chkRequire31.Checked;
			lblRequire31.Enabled = chkRequire31.Checked;

			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.Require31Enabled = chkRequire31.Checked;
			EndEdit();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) script = new BootstrapScriptObjectModel();

			txtWindowTitle.Text = script.WindowTitle;
			txtWindowMessage.Text = script.WindowMessage;
			txtTemporaryDirectorySize.Value = script.TemporaryDirectorySize;
			txtTemporaryDirectoryName.Text = script.TemporaryDirectoryName;
			txtCommandLine.Text = script.CommandLine;
			txtWindowClassName.Text = script.WindowClassName;
			
			chkRequire31.Checked = script.Require31Enabled;
			txtRequire31.ReadOnly = !script.Require31Enabled;
			txtRequire31.Text = script.Require31Message;
			lblRequire31.Enabled = script.Require31Enabled;
		}

		private void txtWindowTitle_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtWindowMessage_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtWindowTitle_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowTitle = txtWindowTitle.Text;
			EndEdit();
		}

		private void txtWindowMessage_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowMessage = txtWindowMessage.Text;
			EndEdit();
		}

		private void txtWindowClassName_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.WindowClassName = txtWindowClassName.Text;
			EndEdit();
		}

		private void txtTemporaryDirectoryName_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.TemporaryDirectoryName = txtTemporaryDirectoryName.Text;
			EndEdit();
		}

		private void txtTemporaryDirectorySize_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.TemporaryDirectorySize = (int)txtTemporaryDirectorySize.Value;
			EndEdit();
		}

		private void txtCommandLine_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.CommandLine = txtCommandLine.Text;
			EndEdit();
		}

		private void txtRequire31_Validated(object sender, EventArgs e)
		{
			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			if (script == null) return;

			BeginEdit();
			script.Require31Message = txtRequire31.Text;
			EndEdit();
		}

		private void cmdFilesAdd_Click(object sender, EventArgs e)
		{
			BootstrapFilePropertiesDialog dlg = new BootstrapFilePropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				ListViewItem lvi = new ListViewItem();
				BootstrapFile file = new BootstrapFile();
				file.SourceFileName = dlg.SourceFileName;
				file.DestinationFileName = dlg.DestinationFileName;

				BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
				BeginEdit();
				script.Files.Add(file);
				EndEdit();

				lvi.Tag = file;
				lvi.Text = file.SourceFileName;
				lvi.SubItems.Add(file.DestinationFileName);
				lvFiles.Items.Add(lvi);

				RefreshButtons();
			}
		}

		private void cmdFilesModify_Click(object sender, EventArgs e)
		{
			if (lvFiles.SelectedItems.Count == 1)
			{
				BootstrapFile file = (lvFiles.SelectedItems[0].Tag as BootstrapFile);
				
				BootstrapFilePropertiesDialog dlg = new BootstrapFilePropertiesDialog();
				dlg.SourceFileName = file.SourceFileName;
				dlg.DestinationFileName = file.DestinationFileName;

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					file.SourceFileName = dlg.SourceFileName;
					file.DestinationFileName = dlg.DestinationFileName;

					lvFiles.SelectedItems[0].Text = file.SourceFileName;
					lvFiles.SelectedItems[0].SubItems[1].Text = file.DestinationFileName;
				}
			}
		}

		private void cmdFilesRemove_Click(object sender, EventArgs e)
		{
			if (lvFiles.SelectedItems.Count > 0)
			{
				if (MessageBox.Show("Are you sure you want to remove the selected files from the list?", "Remove Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					return;
				}

				BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
				BeginEdit();
				foreach (ListViewItem lvi in lvFiles.SelectedItems)
				{
					script.Files.Remove(lvi.Tag as BootstrapFile);
				}
				EndEdit();

				while (lvFiles.SelectedItems.Count > 0)
				{
					lvFiles.SelectedItems[0].Remove();
				}
				RefreshButtons();
			}
		}

		private void RefreshButtons()
		{
			cmdFilesModify.Enabled = (lvFiles.SelectedItems.Count == 1);
			cmdFilesRemove.Enabled = (lvFiles.SelectedItems.Count > 0);
			cmdFilesClear.Enabled = (lvFiles.Items.Count > 0);
		}

		private void cmdFilesClear_Click(object sender, EventArgs e)
		{
			lvFiles.Items.Clear();

			BootstrapScriptObjectModel script = (ObjectModel as BootstrapScriptObjectModel);
			BeginEdit();
			script.Files.Clear();
			EndEdit();
		}

		private void lvFiles_ItemActivate(object sender, EventArgs e)
		{
			cmdFilesModify_Click(sender, e);
		}

		private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}
	}
}
