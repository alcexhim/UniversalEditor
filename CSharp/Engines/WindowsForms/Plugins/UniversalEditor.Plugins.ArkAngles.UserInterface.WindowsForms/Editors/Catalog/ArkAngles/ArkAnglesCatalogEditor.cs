using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Dialogs.Catalog.ArkAngles;

namespace UniversalEditor.Editors.Catalog.ArkAngles
{
	public partial class ArkAnglesCatalogEditor : UserControl
	{
		public ArkAnglesCatalogEditor()
		{
			InitializeComponent();
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

		private void cmdProductAdd_Click(object sender, EventArgs e)
		{
			ProductPropertiesDialog dlg = new ProductPropertiesDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				
			}
		}
	}
}
