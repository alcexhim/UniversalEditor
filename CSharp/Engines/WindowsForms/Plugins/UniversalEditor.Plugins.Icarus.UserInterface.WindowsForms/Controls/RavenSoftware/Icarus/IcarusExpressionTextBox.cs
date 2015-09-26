using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.Dialogs.RavenSoftware.Icarus;

namespace UniversalEditor.Controls.Icarus
{
    public partial class IcarusExpressionTextBox : UserControl
    {
        public IcarusExpressionTextBox()
        {
            InitializeComponent();
        }

        public string Expression
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        private void cmd_Click(object sender, EventArgs e)
        {
            IcarusExpressionHelperDialog dlg = new IcarusExpressionHelperDialog();
            dlg.Expression = txt.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txt.Text = dlg.Expression;
            }
        }
    }
}
