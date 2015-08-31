using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript;

namespace UniversalEditor.Dialogs.Setup.Microsoft.ACME.BootstrapScript
{
	internal partial class ManageOperatingSystemsDialogImpl : Form
	{
		public ManageOperatingSystemsDialogImpl()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private BootstrapOperatingSystem.BootstrapOperatingSystemCollection mvarOperatingSystems = new BootstrapOperatingSystem.BootstrapOperatingSystemCollection();
		public BootstrapOperatingSystem.BootstrapOperatingSystemCollection OperatingSystems { get { return mvarOperatingSystems; } }
		
		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			lv.Items.Clear();

			if (mvarOperatingSystems.Count == 0)
			{
				mvarOperatingSystems.Add(BootstrapOperatingSystem.PlatformIndependent);
			}

			foreach (BootstrapOperatingSystem item in mvarOperatingSystems)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Checked = item.Enabled;
				lvi.Text = item.Name;
				lvi.Tag = item;
				lv.Items.Add(lvi);
			}

			RefreshButtons();
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			OperatingSystemPropertiesDialog dlg = new OperatingSystemPropertiesDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				BootstrapOperatingSystem item = new BootstrapOperatingSystem();
				item.Name = dlg.Name;
				item.Enabled = dlg.Enabled;
				mvarOperatingSystems.Add(item);

				ListViewItem lvi = new ListViewItem();
				lvi.Checked = item.Enabled;
				lvi.Text = item.Name;
				lvi.Tag = item;
				lv.Items.Add(lvi);

				RefreshButtons();
			}
		}

		private void cmdModify_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count != 1) return;

			OperatingSystemPropertiesDialog dlg = new OperatingSystemPropertiesDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{

			}
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count == 0) return;

			if (MessageBox.Show("Removing operating systems from the list will delete their associated settings.\r\n\r\nAre you sure you want to remove the selected operating systems and clear their associated settings?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}

			while (lv.SelectedItems.Count > 0)
			{
				lv.SelectedItems[0].Remove();
			}
			RefreshButtons();
		}

		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (lv.Items.Count == 0) return;

			if (MessageBox.Show("Removing operating systems from the list will delete their associated settings.\r\n\r\nAre you sure you want to remove ALL operating systems and clear their associated settings?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}

			lv.Items.Clear();

			mvarOperatingSystems.Clear();
			mvarOperatingSystems.Add(BootstrapOperatingSystem.PlatformIndependent);

			foreach (BootstrapOperatingSystem item in mvarOperatingSystems)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Checked = item.Enabled;
				lvi.Text = item.Name;
				lvi.Tag = item;
				lv.Items.Add(lvi);
			}

			RefreshButtons();
		}

		private void RefreshButtons()
		{
			cmdModify.Enabled = (lv.SelectedItems.Count == 1);
			cmdRemove.Enabled = (lv.SelectedItems.Count > 0);
			cmdClear.Enabled = (lv.Items.Count > 1);

			if (lv.SelectedItems.Count > 0)
			{
				if (lv.SelectedItems[0].Tag == BootstrapOperatingSystem.PlatformIndependent)
				{
					cmdModify.Enabled = false;
					cmdRemove.Enabled = false;
				}
			}
		}

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshButtons();
		}

		private void lv_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			BootstrapOperatingSystem item = (e.Item.Tag as BootstrapOperatingSystem);
			if (item == null) return;

			item.Enabled = e.Item.Checked;
		}
	}

	public class ManageOperatingSystemsDialog
	{
		private BootstrapOperatingSystem.BootstrapOperatingSystemCollection mvarOperatingSystems = new BootstrapOperatingSystem.BootstrapOperatingSystemCollection();
		public BootstrapOperatingSystem.BootstrapOperatingSystemCollection OperatingSystems { get { return mvarOperatingSystems; } }

		public DialogResult ShowDialog()
		{
			ManageOperatingSystemsDialogImpl dlg = new ManageOperatingSystemsDialogImpl();
			foreach (BootstrapOperatingSystem item in mvarOperatingSystems)
			{
				dlg.OperatingSystems.Add(item);
			}
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mvarOperatingSystems.Clear();
				foreach (BootstrapOperatingSystem item in dlg.OperatingSystems)
				{
					mvarOperatingSystems.Add(item);
				}
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
