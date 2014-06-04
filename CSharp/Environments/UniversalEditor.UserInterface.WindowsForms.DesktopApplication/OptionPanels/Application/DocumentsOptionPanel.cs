using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.OptionPanels.Application
{
    public partial class DocumentsOptionPanel : OptionPanel
    {
        public DocumentsOptionPanel()
        {
            InitializeComponent();
        }

        private string[] mvarOptionGroups = new string[] { "Application", "Documents" };
        public override string[] OptionGroups { get { return mvarOptionGroups; } }
    }
}
