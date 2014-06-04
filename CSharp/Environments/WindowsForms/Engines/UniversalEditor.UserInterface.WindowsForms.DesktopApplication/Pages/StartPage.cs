using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();

            Title = "Start Page";
            Description = "Start Page";

            lblApplicationTitle.Text = Configuration.ApplicationName;

            pnlTop.BackColor = Configuration.ColorScheme.DarkColor;
            pnlSide.BackColor = Configuration.ColorScheme.LightColor;
            BackColor = Configuration.ColorScheme.BackgroundColor;

            if (Configuration.MainIcon != null)
            {
                picIcon.Image = Configuration.MainIcon.ToBitmap();
            }

            foreach (string FileName in RecentFileManager.FileNames)
            {
                AwesomeControls.ListView.ListViewItem lvi = new AwesomeControls.ListView.ListViewItem();
                lvi.Text = System.IO.Path.GetFileName(FileName);
                lvi.TooltipText = FileName;
                lvRecent.Items.Add(lvi);
            }
        }

        public event EventHandler NewProjectClicked;
        public event EventHandler OpenProjectClicked;

        private void lblNewProject_Click(object sender, EventArgs e)
        {
            if (NewProjectClicked != null) NewProjectClicked(sender, e);
        }

        private void lblOpenProject_Click(object sender, EventArgs e)
        {
            if (OpenProjectClicked != null) OpenProjectClicked(sender, e);
        }

        private void lvRecent_ItemActivate(object sender, EventArgs e)
        {
            if (lvRecent.SelectedItems.Count < 1) return;

            if (!System.IO.File.Exists(lvRecent.SelectedItems[0].TooltipText))
            {
            	if (MessageBox.Show("The file \"" + lvRecent.SelectedItems[0].TooltipText + "\" does not exist.  Would you like to remove it from the Recent Documents list?", "File Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            	{
            		RecentFileManager.FileNames.Remove(lvRecent.SelectedItems[0].TooltipText);
            		lvRecent.Items.Remove(lvRecent.SelectedItems[0]);
            	}
            	return;
            }
            WindowsFormsEngine.OpenFile(lvRecent.SelectedItems[0].TooltipText);
        }
    }
}
