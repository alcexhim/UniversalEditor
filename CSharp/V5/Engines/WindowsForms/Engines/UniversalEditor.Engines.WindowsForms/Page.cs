using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }
    }
}
