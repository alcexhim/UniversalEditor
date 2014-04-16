using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
    public partial class ErrorMessage : UserControl
    {
        public ErrorMessage()
        {
            InitializeComponent();
        }

        public string Details
        {
            get { return txtDetails.Text; }
            set { txtDetails.Text = value; }
        }

        public string Title { get { return lblTitle.Text; } set { lblTitle.Text = value; } }
    }
}
