using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms.Pages
{
    public partial class InvalidFormatPage : UserControl
    {
        public InvalidFormatPage()
        {
            InitializeComponent();
        }

        private Exception mvarException = null;
        public Exception Exception
        {
            get { return mvarException; }
            set { mvarException = value; }
        }
    }
}
