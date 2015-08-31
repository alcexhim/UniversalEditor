using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.WindowsForms.OptionPanels.Multimedia3D.Model
{
    public partial class GeneralOptionPanel : OptionPanel
    {
        public GeneralOptionPanel()
        {
            InitializeComponent();
        }

        private string[] mvarOptionGroups = new string[] { "Editors", "Model" };
        public override string[] OptionGroups { get { return mvarOptionGroups; } }

        public override bool IsAvailable { get { return true; } }

        private void chkLimitScreenFPS_CheckedChanged(object sender, EventArgs e)
        {
            txtFPSLimit.Enabled = chkLimitScreenFPS.Checked;
            lblFPSLimit.Enabled = chkLimitScreenFPS.Checked;
        }
    }
}
