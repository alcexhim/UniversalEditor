using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AwesomeControls;

namespace UniversalEditor.Engines.WindowsForms.Controls
{
	public partial class OutputWindow : UserControl
	{
		public OutputWindow()
		{
			InitializeComponent();

			txtOutput.BackColor = AwesomeControls.Theming.Theme.CurrentTheme.ColorTable.WindowBackground;
			txtOutput.ForeColor = AwesomeControls.Theming.Theme.CurrentTheme.ColorTable.WindowForeground;
			txtOutput.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 10, FontStyle.Regular);
			txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;

			cbToolBar1.LoadThemeIcons("OutputWindow");
		}

		public void AppendText(string text)
		{
			txtOutput.AppendText(text);
		}
		public void ClearText()
		{
			txtOutput.Text = String.Empty;
		}
	}
}
