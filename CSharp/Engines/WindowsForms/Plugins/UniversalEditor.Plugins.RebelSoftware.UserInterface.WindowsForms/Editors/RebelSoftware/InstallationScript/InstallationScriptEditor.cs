using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs;

using ISDialog = UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialog;

namespace UniversalEditor.Editors.RebelSoftware.InstallationScript
{
	public partial class InstallationScriptEditor : Editor
	{
		public InstallationScriptEditor()
		{
			InitializeComponent();
			tvExplorer.PopulateSystemIcons();
		}

		private static UserInterface.EditorReference _er = null;
		public override UserInterface.EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(InstallationScriptObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			lvDialogs.Items.Clear();

			InstallationScriptObjectModel script = (ObjectModel as InstallationScriptObjectModel);
			if (script == null) return;

			lvDialogs.Items.Clear();

			foreach (ISDialog dialog in script.Dialogs)
			{
				ListViewItem lviDialog = new ListViewItem();
				lviDialog.Name = dialog.GetType().FullName;
				lviDialog.Checked = true;
				lviDialog.Tag = dialog;
				lviDialog.Text = GetDialogTitle(dialog);
				lvDialogs.Items.Add(lviDialog);
			}

			Type[] types = typeof(ISDialog).Assembly.GetTypes();
			List<Type> dialogTypes = new List<Type>();
			foreach (Type t in types)
			{
				if (!t.IsAbstract && t.IsSubclassOf(typeof(ISDialog)))
				{
					dialogTypes.Add(t);
				}
			}

			foreach (Type t in dialogTypes)
			{
				if (!lvDialogs.Items.ContainsKey(t.FullName))
				{
					ISDialog dialog = (ISDialog)t.Assembly.CreateInstance(t.FullName);

					ListViewItem lvi = new ListViewItem();
					lvi.Name = t.FullName;
					lvi.Text = GetDialogTitle(dialog);
					lvi.Tag = dialog;
					lvi.Checked = false;
					lvDialogs.Items.Add(lvi);
				}
			}
		}

		private string GetDialogTitle(ISDialog dialog)
		{
			if (dialog is CopyFilesDialog)
			{
				return "Progress";
			}
			else if (dialog is DestinationDialog)
			{
				return "Destination";
			}
			else if (dialog is FinishDialog)
			{
				return "Finish";
			}
			else if (dialog is LicenseDialog)
			{
				return "License";
			}
			else if (dialog is StartCopyingDialog)
			{
				return "Summary";
			}
			else if (dialog is WelcomeDialog)
			{
				return "Welcome";
			}
			return dialog.GetType().FullName;
		}

		private void tvExplorer_AfterSelect(object sender, TreeViewEventArgs e)
		{
			foreach (Control ctl in scMain.Panel2.Controls)
			{
				if (e.Node != null)
				{
					if (ctl.Name.Substring(3) == e.Node.Name.Substring(4))
					{
						ctl.Enabled = true;
						ctl.Visible = true;
						continue;
					}
				}

				ctl.Visible = false;
				ctl.Enabled = false;
			}
		}

		private void lvDialogs_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lvDialogs.SelectedItems.Count != 1) return;

			ListViewItem lvi = lvDialogs.SelectedItems[0];

			picDialogPreview.Image = null;

			ISDialog dialog = (lvi.Tag as ISDialog);
			if (dialog == null) return;

			if (dialog is CopyFilesDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_05_Progress;
			}
			else if (dialog is DestinationDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_03_Destination;
			}
			else if (dialog is FinishDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_06_Finish;
			}
			else if (dialog is LicenseDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_02_License;
			}
			else if (dialog is StartCopyingDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_04_Summary;
			}
			else if (dialog is WelcomeDialog)
			{
				picDialogPreview.Image = Properties.Resources.Screenshot_Install_01_Welcome;
			}
		}
	}
}
