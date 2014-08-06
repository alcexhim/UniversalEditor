using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Shortcut;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors
{
	public partial class ShortcutEditor : Editor
	{
		public ShortcutEditor()
		{
			InitializeComponent();

			base.SupportedObjectModels.Add(typeof(ShortcutObjectModel));
			cboTargetType.SelectedIndex = 0;
		}

		private void cboTargetType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtTarget.ReadOnly = (cboTargetType.SelectedIndex == 1);
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			ShortcutObjectModel doc = (base.ObjectModel as ShortcutObjectModel);
			if (doc == null) return;

			/*
			if (doc.Type == ShortcutType.Normal)
			{
				cboTargetType.SelectedIndex = 0;
			}
			else
			{
				cboTargetType.SelectedIndex = 1;
				switch (doc.Type)
				{
					case ShortcutType.Computer:
						txtTarget.Text = "My Computer";
						break;
					case ShortcutType.Documents:
						txtTarget.Text = "My Documents";
						break;
					case ShortcutType.Network:
						txtTarget.Text = "My Network Places";
						break;
					case ShortcutType.Trash:
						txtTarget.Text = "Recycle Bin";
						break;
					case ShortcutType.Unknown:
						txtTarget.Text = Common.Methods.ArrayToString<byte>(doc.ShellTarget);
						break;
					case ShortcutType.Normal:
						txtTarget.Text = doc.Target;
						break;
				}
			}
			*/

			if (doc.IconFileName != "")
			{
				txtIconFileName.Text = doc.IconFileName;
				
				// Read the icons from the file, through Resource Viewer
			}
		}

		private void cmdBrowseTarget_Click(object sender, EventArgs e)
		{
			switch (cboTargetType.SelectedIndex)
			{
				case 0:
					break;
				case 1:
					break;
			}
		}
	}
}
