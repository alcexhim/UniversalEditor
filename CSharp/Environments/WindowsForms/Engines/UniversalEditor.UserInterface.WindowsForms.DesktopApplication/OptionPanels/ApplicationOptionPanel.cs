using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.OptionPanels
{
    public partial class ApplicationOptionPanel : OptionPanel
    {
        public ApplicationOptionPanel()
        {
            InitializeComponent();
        }

        private string[] mvarOptionGroups = new string[] { "Application" };
        public override string[] OptionGroups { get { return mvarOptionGroups; } }
    }
}
