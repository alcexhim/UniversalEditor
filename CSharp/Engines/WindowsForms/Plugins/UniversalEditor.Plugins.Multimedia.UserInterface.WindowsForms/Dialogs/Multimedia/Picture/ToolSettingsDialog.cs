using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace UniversalEditor.Dialogs.Multimedia.Picture
{
	public partial class ToolSettingsDialog
	{
		public ToolSettingsDialog()
		{
			this.InitializeComponent();
		}
		private void cmdColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				this.cmdColor.BackColor = dlg.Color;
			}
		}
		private void cmdOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}
	}
}
