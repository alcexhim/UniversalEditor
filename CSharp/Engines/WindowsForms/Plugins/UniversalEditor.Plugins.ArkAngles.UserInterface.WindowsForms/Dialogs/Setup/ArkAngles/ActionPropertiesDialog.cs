using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Setup.ArkAngles
{
	internal partial class ActionPropertiesDialogImpl : Form
	{
		public ActionPropertiesDialogImpl()
		{
			InitializeComponent();
		}
	}
	public class ActionPropertiesDialog
	{
		public DialogResult ShowDialog()
		{
			ActionPropertiesDialogImpl dlg = new ActionPropertiesDialogImpl();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
	}
}
