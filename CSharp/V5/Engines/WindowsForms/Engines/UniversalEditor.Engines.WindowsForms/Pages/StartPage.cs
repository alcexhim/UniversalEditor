using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.WindowsForms.Pages
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();

            Title = "Start Page";
            Description = "Start Page";

            lblApplicationTitle.Text = LocalConfiguration.ApplicationName;

            pnlTop.BackColor = LocalConfiguration.ColorScheme.DarkColor;
            pnlSide.BackColor = LocalConfiguration.ColorScheme.LightColor;
            BackColor = LocalConfiguration.ColorScheme.BackgroundColor;

            if (LocalConfiguration.MainIcon != null)
            {
                picIcon.Image = LocalConfiguration.MainIcon.ToBitmap();
            }

            foreach (string FileName in Engine.CurrentEngine.RecentFileManager.FileNames)
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
            		Engine.CurrentEngine.RecentFileManager.FileNames.Remove(lvRecent.SelectedItems[0].TooltipText);
            		lvRecent.Items.Remove(lvRecent.SelectedItems[0]);
            	}
            	return;
            }
            Engine.CurrentEngine.OpenFile(lvRecent.SelectedItems[0].TooltipText);
        }
    }
}
