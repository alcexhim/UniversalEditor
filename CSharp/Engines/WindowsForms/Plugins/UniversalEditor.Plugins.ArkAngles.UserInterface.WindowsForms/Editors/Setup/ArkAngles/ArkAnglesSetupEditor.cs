using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Setup.ArkAngles;
using UniversalEditor.ObjectModels.Setup.ArkAngles.Actions;

namespace UniversalEditor.Editors.Setup.ArkAngles
{
	public partial class ArkAnglesSetupEditor : UserControl
	{
		public ArkAnglesSetupEditor()
		{
			InitializeComponent();

			lvPostInstallActions.Items.Clear();

			List<AutoStartCommand> cmds = new List<AutoStartCommand>();
			cmds.Add(AutoStartCommand.Install);
			cmds.Add(AutoStartCommand.Catalog);
			cmds.Add(null);
			cmds.Add(AutoStartCommand.Restart);
			cmds.Add(AutoStartCommand.Exit);

			foreach (AutoStartCommand cmd in cmds)
			{
				ListViewItem lvi = new ListViewItem();
				if (cmd == null)
				{
					lvi.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
					lvi.Text = "(Installation Process)";
				}
				else
				{
					if (cmd == AutoStartCommand.Install)
					{
						lvi.Text = "Install";
					}
					else if (cmd == AutoStartCommand.Catalog)
					{
						lvi.Text = "Catalog";
					}
					else if (cmd == AutoStartCommand.Restart)
					{
						lvi.Text = "Restart";
					}
					else if (cmd == AutoStartCommand.Exit)
					{
						lvi.Text = "Exit";
					}
				}
				lvPostInstallActions.Items.Add(lvi);
			}
		}

		private void lvPostInstallActions_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (lvPostInstallActions.Items[e.Index].Tag == null) e.NewValue = CheckState.Checked;
		}

		private void lvPostInstallActions_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmdPostInstallActionMoveDown.Enabled = (lvPostInstallActions.SelectedItems.Count == 1 && lvPostInstallActions.SelectedItems[0].Tag != null && lvPostInstallActions.SelectedItems[0].Index < lvPostInstallActions.Items.Count - 1);
			cmdPostInstallActionMoveUp.Enabled = (lvPostInstallActions.SelectedItems.Count == 1 && lvPostInstallActions.SelectedItems[0].Tag != null && lvPostInstallActions.SelectedItems[0].Index > 0);
		}

		private void cmdColor_Click(object sender, EventArgs e)
		{
			Control cmd = (sender as Control);
			ColorDialog dlg = new ColorDialog();
			dlg.Color = cmd.BackColor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmd.BackColor = dlg.Color;
			}
		}
	}
}
