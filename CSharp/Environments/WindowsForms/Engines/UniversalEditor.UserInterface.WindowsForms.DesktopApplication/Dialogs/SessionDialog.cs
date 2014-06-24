using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class SessionDialog : AwesomeControls.Dialog
	{
		public SessionDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;

			foreach (SessionManager.Session session in Engine.CurrentEngine.SessionManager.Sessions)
			{
				AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
				lvi.Text = session.Title;
				lvi.Data = session;
				lv.Items.Add(lvi);
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void lv_SelectionChanged(object sender, EventArgs e)
		{
			cmdLoad.Enabled = (lv.SelectedItems.Count == 1);
			if (lv.SelectedItems.Count < 1) return;
			txtSessionName.Text = lv.SelectedItems[0].Text;
			cmdSave.Enabled = !String.IsNullOrEmpty(txtSessionName.Text);
		}

		private void cmdLoad_Click(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count < 1) return;

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();

			WindowsFormsEngine.SessionLoading = true;
			Engine.CurrentEngine.CloseAllWindows();

			SessionManager.Session session = (lv.SelectedItems[0].Data as SessionManager.Session);

			Dictionary<MainWindow, string[]> filenames = new Dictionary<MainWindow, string[]>();
			foreach (SessionManager.Window window in session.Windows)
			{
				MainWindow wnd = new MainWindow();
				wnd.StartPosition = FormStartPosition.Manual;
				wnd.Left = window.Left;
				wnd.Top = window.Top;
				wnd.Width = window.Width;
				wnd.Height = window.Height;
				filenames.Add(wnd, window.FileNames.ToArray());

				wnd.Show();
				Engine.CurrentEngine.Windows.Add(wnd);
			}

			foreach (KeyValuePair<MainWindow, string[]> fkvp in filenames)
			{
				fkvp.Key.OpenFile(fkvp.Value);
			}

			WindowsFormsEngine.SessionLoading = false;
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			SessionManager.Session session = new SessionManager.Session();
			session.Title = txtSessionName.Text;

			foreach (MainWindow wnd in Engine.CurrentEngine.Windows)
			{
				SessionManager.Window window = new SessionManager.Window();
				window.Left = wnd.Left;
				window.Top = wnd.Top;
				window.Width = wnd.Width;
				window.Height = wnd.Height;
				switch (wnd.WindowState)
				{
					case FormWindowState.Maximized:
					{
						window.WindowState = UserInterface.WindowState.Maximized;
						break;
					}
					case FormWindowState.Minimized:
					{
						window.WindowState = UserInterface.WindowState.Minimized;
						break;
					}
					case FormWindowState.Normal:
					{
						window.WindowState = UserInterface.WindowState.Normal;
						break;
					}
				}

				System.Collections.ObjectModel.ReadOnlyCollection<Document> documents = wnd.Documents;
				foreach (Document doc in documents)
				{
					if (System.IO.File.Exists(doc.Title))
					{
						window.FileNames.Add(doc.Title);
					}
				}
				session.Windows.Add(window);
			}

			Engine.CurrentEngine.SessionManager.Sessions.Add(session);

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();

			if (MessageBox.Show("Would you like to close all active windows and start a new session at this time?", "Close Session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				WindowsFormsEngine.SessionLoading = true;
				Engine.CurrentEngine.CloseAllWindows();
				Engine.CurrentEngine.OpenWindow();
				WindowsFormsEngine.SessionLoading = false;
			}
		}

		private void txtSessionName_TextChanged(object sender, EventArgs e)
		{
			cmdSave.Enabled = !String.IsNullOrEmpty(txtSessionName.Text);
		}
	}
}
