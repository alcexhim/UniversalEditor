using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms
{
    public partial class OptionPanel : UserControl, IOptionPanelImplementation
    {
        public OptionPanel()
        {
            InitializeComponent();
            mvarOptionGroups = base.GetType().FullName.Split(new char[] { '.' });
        }

        public virtual void LoadSettings()
        {
            throw new NotImplementedException();
        }
        public virtual void SaveSettings()
        {
            throw new NotImplementedException();
        }
        public virtual void ResetSettings()
        {
            throw new NotImplementedException();
        }

        private string[] mvarOptionGroups = null;
        public virtual string[] OptionGroups
        {
            get { return mvarOptionGroups; }
        }

        public virtual bool IsAvailable
        {
            get { return false; }
        }

        private Image mvarIconImage = null;
        public Image IconImage { get { return mvarIconImage; } set { mvarIconImage = value; } }
    }
}
