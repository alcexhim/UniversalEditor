using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms.Pages
{
    public partial class MultipleObjectModelPage : FilePage
    {
        public MultipleObjectModelPage()
        {
            InitializeComponent();
        }

        private string mvarFileType = String.Empty;
        public string FileType { get { return mvarFileType; } set { mvarFileType = value; } }
    }
}
