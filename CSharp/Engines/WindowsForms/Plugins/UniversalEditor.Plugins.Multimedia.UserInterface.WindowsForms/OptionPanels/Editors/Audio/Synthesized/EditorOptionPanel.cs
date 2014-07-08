using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

namespace UniversalEditor.OptionPanels.Editors.Audio.Synthesized
{
    public partial class EditorOptionPanel : OptionPanel
    {
        public EditorOptionPanel()
        {
            InitializeComponent();
        }

        private string[] mvarOptionGroups = new string[] { "Editors", "Synthesized Audio" };
        public override string[] OptionGroups { get { return mvarOptionGroups; } }

        public override bool IsAvailable { get { return true; } }
    }
}
