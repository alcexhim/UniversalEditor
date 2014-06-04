using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using UniversalEditor.UserInterface.WindowsForms.Controls;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class ExplorerPage : System.Windows.Forms.UserControl
    {
        public ExplorerPage()
        {
            InitializeComponent();
        }

        public string Path
        {
            get { return explorer.Path; }
            set { explorer.Path = value; }
        }

        public event NavigateEventHandler Navigate;

        private void explorer_Navigate(object sender, Controls.NavigateEventArgs e)
        {
            if (Navigate != null) Navigate(this, e);
        }
    }
}
