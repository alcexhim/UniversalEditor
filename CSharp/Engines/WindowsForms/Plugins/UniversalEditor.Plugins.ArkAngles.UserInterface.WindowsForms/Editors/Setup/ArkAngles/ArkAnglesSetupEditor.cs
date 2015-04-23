using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Setup.ArkAngles;
using UniversalEditor.ObjectModels.Setup.ArkAngles.Actions;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Setup.ArkAngles
{
	public partial class ArkAnglesSetupEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.Title = "Ark Angles Setup editor";
				_er.SupportedObjectModels.Add(typeof(SetupObjectModel));
			}
			return _er;
		}

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
				lvi.Tag = cmd;
				if (cmd == null)
				{
					lvi.Checked = true;
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

			SwitchTo("General");
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

		private void SwitchTo(string name)
		{
			foreach (Control ctl in scMain.Panel2.Controls)
			{
				if (name != null && (ctl.Name.Substring(3) == name))
				{
					ctl.Enabled = true;
					ctl.Visible = true;
					continue;
				}

				ctl.Visible = false;
				ctl.Enabled = false;
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
			{
				SwitchTo(e.Node.Name.Substring(4));
			}
			else
			{
				SwitchTo(null);
			}
		}
	}
}
