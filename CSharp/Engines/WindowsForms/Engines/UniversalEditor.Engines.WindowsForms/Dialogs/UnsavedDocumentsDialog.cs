using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.Engines.WindowsForms.Dialogs
{
    public partial class UnsavedDocumentsDialog : AwesomeControls.Dialog
    {
        public UnsavedDocumentsDialog()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;

            lv.Columns.Add("File name");
            lv.Columns.Add("Action", 100);
        }

        private void cmdDiscard_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to discard all changes made to unsaved documents?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
                this.Close();
            }
        }

        private void cmdContinue_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


        private string mvarPrompt = "The following documents have been changed and not saved.  Select the document(s) you wish to save changes for before exiting.";
        public string Prompt { get { return mvarPrompt; } set { mvarPrompt = value; } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            TextRenderer.DrawText(e.Graphics, mvarPrompt, Font, new Rectangle(13, 13, ClientSize.Width - 24, 32), AwesomeControls.Theming.Theme.CurrentTheme.ColorTable.WindowForeground, TextFormatFlags.Default | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
        }
    }
}
