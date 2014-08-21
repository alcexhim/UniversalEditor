using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Dialogs.Icarus
{
    public partial class IcarusExpressionHelperDialog : Form
    {
        public IcarusExpressionHelperDialog()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
        }

        private string mvarExpression = String.Empty;
        public string Expression
        {
            get { return mvarExpression; }
            set { mvarExpression = value; }
        }
    }
}
