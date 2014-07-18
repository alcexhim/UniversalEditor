using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	internal partial class SelectPerspectiveDialogBase : Form
	{
		public SelectPerspectiveDialogBase()
		{
			InitializeComponent();

			foreach (Perspective perspective in Engine.CurrentEngine.Perspectives)
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
	public class SelectPerspectiveDialog
	{
		public DialogResult ShowDialog()
		{
			SelectPerspectiveDialogBase dlg = new SelectPerspectiveDialogBase();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
