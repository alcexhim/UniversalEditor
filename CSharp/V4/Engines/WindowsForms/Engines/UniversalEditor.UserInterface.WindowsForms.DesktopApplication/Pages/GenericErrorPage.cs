using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Pages
{
    public partial class GenericErrorPage : UserControl
    {
        public GenericErrorPage()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return err.Title; }
            set { err.Title = value; }
        }

        private Exception mvarException = null;
        public Exception Exception
        {
            get { return mvarException; }
            set { mvarException = value; Title = mvarException.GetType().FullName; err.Details = mvarException.Message; }
        }
    }
}
