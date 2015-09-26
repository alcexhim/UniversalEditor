using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
    public partial class DataFormatAboutDialog : Form
    {
        public DataFormatAboutDialog()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
        }

        private DataFormatReference mvarDataFormatReference = null;
        public DataFormatReference DataFormatReference
        {
            get { return mvarDataFormatReference; }
            set
            {
                mvarDataFormatReference = value;
                if (mvarDataFormatReference != null)
                {
                    txtTitle.Text = mvarDataFormatReference.Title;
                }
                else
                {
                }
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
