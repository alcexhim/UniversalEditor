using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    public partial class DocumentPropertiesDialog : Form
    {
        public DocumentPropertiesDialog()
        {
            InitializeComponent();
        }

        private ObjectModel mvarObjectModel = null;
        public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

        private DataFormat mvarDataFormat = null;
        public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

        private Accessor mvarAccessor = null;
        public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (mvarObjectModel == null)
            {
                MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (mvarDataFormat == null)
            {
                MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (mvarAccessor == null)
            {
                MessageBox.Show("Please choose an object model for the document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void cmdObjectModel_Click(object sender, EventArgs e)
        {
            ShowContextMenuBelow(mnuObjectModel, cmdObjectModel);
        }

        private void cmdDataFormat_Click(object sender, EventArgs e)
        {
            ShowContextMenuBelow(mnuDataFormat, cmdDataFormat);
        }

        private void cmdAccessor_Click(object sender, EventArgs e)
        {
            ShowContextMenuBelow(mnuAccessor, cmdAccessor);
        }

        private void ShowContextMenuBelow(ContextMenuStrip menu, Control parent)
        {
            Point pt = PointToScreen(parent.Location);
            menu.Show(pt.X, pt.Y + parent.Height);
        }

        private void mnuObjectModelSelect_Click(object sender, EventArgs e)
        {

        }

        private void mnuObjectModelClear_Click(object sender, EventArgs e)
        {
            mvarObjectModel = null;
            txtObjectModel.Text = String.Empty;
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            if (mvarDataFormat != null)
            {
                DataFormatReference dfr = mvarDataFormat.MakeReference();
                txtDataFormat.Text = dfr.Title + " (" + DataFormatFilterCollectionToString(dfr.Filters) + ")";
            }
            cmdOK.Enabled = (mvarObjectModel != null && mvarDataFormat != null && mvarAccessor != null);
        }

        private string DataFormatFilterCollectionToString(DataFormatFilter.DataFormatFilterCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataFormatFilter filter in collection)
            {
                sb.Append(StringArrayToString(filter.FileNameFilters));
                if (collection.IndexOf(filter) < collection.Count - 1)
                {
                    sb.Append("; ");
                }
            }
            return sb.ToString();
        }

        private string StringArrayToString(System.Collections.Specialized.StringCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in collection)
            {
                sb.Append(s);
                if (collection.IndexOf(s) < collection.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            return sb.ToString();
        }

        private void mnuDataFormatSelect_Click(object sender, EventArgs e)
        {

        }

        private void mnuDataFormatClear_Click(object sender, EventArgs e)
        {
            mvarDataFormat = null;
            RefreshButtons();
        }
    }
}
