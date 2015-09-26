using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.ObjectModels.Web.StyleSheet;

using UniversalEditor.UserInterface;
using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Editors.Web.StyleSheet
{
	public partial class StyleSheetEditor : Editor
	{
		public StyleSheetEditor()
		{
			InitializeComponent();

			object[] FontSizes = (base.Configuration.Properties["FontSizes"].Value as object[]);
			foreach (object FontSizeO in FontSizes)
			{
				string FontSize = FontSizeO.ToString();
				txtFontSize.Items.Add(FontSize);
			}
		}

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(StyleSheetObjectModel));
			}
			return _er;
		}

		private void cmdFontColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.Color = cmdFontColor.BackColor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmdFontColor.BackColor = dlg.Color;
			}
		}
	}
}
