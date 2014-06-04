using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	internal partial class SelectEnvironmentDialogBase : Form
	{
		public SelectEnvironmentDialogBase()
		{
			InitializeComponent();

			foreach (Perspective perspective in PerspectiveManager.Perspectives)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = perspective.Title;
				lvi.SubItems.Add(perspective.Description);
				lvi.Tag = perspective;
				lvEnvironments.Items.Add(lvi);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void lvEnvironments_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdOK.Enabled = (lvEnvironments.CheckedItems.Count == 1);
		}
	}
	public class SelectEnvironmentDialog
	{
		public DialogResult ShowDialog()
		{
			SelectEnvironmentDialogBase dlg = new SelectEnvironmentDialogBase();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
