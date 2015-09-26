using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

using AwesomeControls.ListView;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
    [DefaultEvent("Navigate")]
    public partial class LocalFileSystemExplorer : System.Windows.Forms.UserControl
    {
        public LocalFileSystemExplorer()
        {
            InitializeComponent();

			imlSmallIcons.PopulateSystemIcons();
            imlLargeIcons.PopulateSystemIcons();
        }

        public bool ShowFavorites
        {
            get { return !scFavoritesFiles.Panel1Collapsed; }
            set { scFavoritesFiles.Panel1Collapsed = !value; }
        }
        public bool ShowDetails
        {
            get { return !scFilesDetails.Panel2Collapsed; }
            set { scFilesDetails.Panel2Collapsed = !value; }
        }
        public bool ShowPreview
        {
            get { return !scFilesPreview.Panel2Collapsed; }
            set { scFilesPreview.Panel2Collapsed = !value; }
        }

        private string mvarPath = String.Empty;
        public string Path
        {
            get { return mvarPath; }
            set
            {
                lv.Items.Clear();
                tv.Items.Clear();
                if (!System.IO.Directory.Exists(value)) return;

                string[] directories = null;
                try
                {
                    directories = System.IO.Directory.GetDirectories(value);
                }
                catch (System.IO.IOException ex)
                {
                }
                catch (System.UnauthorizedAccessException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                mvarPath = value;

                foreach (string filename in directories)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = System.IO.Path.GetFileName(filename);
                    lvi.Data = filename;
                    lvi.ImageKey = "generic-folder-closed";
                    lv.Items.Add(lvi);
                }
                string[] filenames = System.IO.Directory.GetFiles(mvarPath);
                foreach (string filename in filenames)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = System.IO.Path.GetFileName(filename);
                    lvi.Data = filename;
                    lvi.ImageKey = "generic-file";
                    lv.Items.Add(lvi);
                }
            }
        }

        public event NavigateEventHandler Navigate;
        protected virtual void OnNavigate(NavigateEventArgs e)
        {
            if (Navigate != null) Navigate(this, e);
        }

        private void lv_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem lvi = lv.SelectedItems[0];
            string filename = (string)lvi.Data;

            if (System.IO.File.Exists(filename))
            {
                OnNavigate(new NavigateEventArgs(filename));
            }
            else if (System.IO.Directory.Exists(filename))
            {
                Path = filename;
            }
        }
    }

    public delegate void NavigateEventHandler(object sender, NavigateEventArgs e);
    public class NavigateEventArgs
    {
        public NavigateEventArgs(string FileName)
        {
            mvarFileName = FileName;
        }

        private string mvarFileName = String.Empty;
        public string FileName { get { return mvarFileName; } }
    }
}
