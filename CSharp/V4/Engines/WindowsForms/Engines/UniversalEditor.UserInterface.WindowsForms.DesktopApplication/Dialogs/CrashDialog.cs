using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class CrashDialog : Form
	{
		public CrashDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private Exception mvarException = null;
		public Exception Exception { get { return mvarException; } set { mvarException = value; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			txtType.Text = mvarException.GetType().FullName;
			txtMessage.Text = mvarException.Message;
			txtSource.Text = mvarException.Source;
			txtStackTrace.Text = mvarException.StackTrace;
		}
	}
}
