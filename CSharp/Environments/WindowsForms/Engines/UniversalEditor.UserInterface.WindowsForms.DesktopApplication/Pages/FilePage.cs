using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class FilePage : Page
    {
        public FilePage()
        {
            InitializeComponent();
        }

        private string mvarFileName = String.Empty;
        public string FileName
        {
            get { return mvarFileName; }
            set
            {
                mvarFileName = value;

                try
                {
                    Title = System.IO.Path.GetFileName(mvarFileName);
                }
                catch
                {
                    Title = mvarFileName;
                }
                Description = mvarFileName;
            }
        }
    }
}
