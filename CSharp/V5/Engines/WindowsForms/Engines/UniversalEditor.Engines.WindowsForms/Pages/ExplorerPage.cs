using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using UniversalEditor.Engines.WindowsForms.Controls;
using UniversalEditor.UserInterface.WindowsForms.Controls;

namespace UniversalEditor.Engines.WindowsForms.Pages
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

        private void explorer_Navigate(object sender, NavigateEventArgs e)
        {
            if (Navigate != null) Navigate(this, e);
        }
    }
}
