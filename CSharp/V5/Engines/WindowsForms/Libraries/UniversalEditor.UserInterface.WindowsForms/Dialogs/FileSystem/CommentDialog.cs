using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.FileSystem
{
	public partial class CommentDialog : Form
	{
		public CommentDialog()
		{
			InitializeComponent();
			base.Font = SystemFonts.MenuFont;
		}
	}
}
