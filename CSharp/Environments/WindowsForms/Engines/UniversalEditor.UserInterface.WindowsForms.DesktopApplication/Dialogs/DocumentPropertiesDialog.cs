using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    public partial class DocumentPropertiesDialogBase : Form
    {
        public DocumentPropertiesDialogBase()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
        }

        private DocumentPropertiesDialogMode mvarMode = DocumentPropertiesDialogMode.Open;
        public DocumentPropertiesDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

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
            if (mvarAccessor is FileAccessor)
            {
                string filename = (mvarAccessor as FileAccessor).FileName;
                DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(filename);

                if (mvarDataFormat == null)
                {
                    if (dfrs.Length > 0)
                    {
                        mvarDataFormat = dfrs[0].Create();
                    }
                }

                mnuDataFormat.Items.Clear();
                foreach (DataFormatReference dfr in dfrs)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Text = DataFormatReferenceToString(dfr);
                    tsmi.Tag = dfr;
                    tsmi.Click += tsmi_Click;
                    if (mvarDataFormat != null)
                    {
                        if (mvarDataFormat.GetType() == dfr.DataFormatType)
                        {
                            tsmi.Checked = true;
                        }
                    }
                    mnuDataFormat.Items.Add(tsmi);
                }

                mnuDataFormat.Items.Add(mnuDataFormatSep1);
                mnuDataFormat.Items.Add(mnuDataFormatSelect);
                mnuDataFormat.Items.Add(mnuDataFormatClear);
            }
            if (mvarObjectModel == null)
            {
                if (mvarDataFormat != null)
                {
                    ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(mvarDataFormat.MakeReference());
                    if (omrs.Length > 0)
                    {
                        mvarObjectModel = omrs[0].Create();
                    }
                }
                else if (mvarAccessor is FileAccessor)
                {
                    string filename = (mvarAccessor as FileAccessor).FileName;
                    ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels(filename);
                    if (omrs.Length == 1)
                    {
                        mvarObjectModel = omrs[0].Create();
                    }
                }
            }

            if (mvarAccessor != null)
            {
                if (mvarAccessor is FileAccessor)
                {
                    txtAccessor.Text = "File: " + (mvarAccessor as FileAccessor).FileName;
                }
            }
            if (mvarDataFormat != null)
            {
                DataFormatReference dfr = mvarDataFormat.MakeReference();
                txtDataFormat.Text = DataFormatReferenceToString(dfr);
            }
            if (mvarObjectModel != null)
            {
                ObjectModelReference omr = mvarObjectModel.MakeReference();
                txtObjectModel.Text = omr.Title;
            }

            cmdOK.Enabled = (mvarObjectModel != null && mvarDataFormat != null && mvarAccessor != null);
        }

        private void tsmi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
            mvarDataFormat = (tsmi.Tag as DataFormatReference).Create();
            RefreshButtons();
        }

        private string DataFormatReferenceToString(DataFormatReference dfr)
        {
            return dfr.Title + " (" + DataFormatFilterCollectionToString(dfr.Filters) + ")";
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

        private void mnuAccessorLocalFile_Click(object sender, EventArgs e)
        {
            switch (mvarMode)
            {
                case DocumentPropertiesDialogMode.Open:
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        mvarAccessor = new FileAccessor(ofd.FileName);
                        txtAccessor.Text = "File: " + ofd.FileName;
                    }
                    break;
                }
                case DocumentPropertiesDialogMode.Save:
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        mvarAccessor = new FileAccessor(sfd.FileName, true, true);
                        txtAccessor.Text = "File: " + sfd.FileName;
                    }
                    break;
                }
            }
            RefreshButtons();
        }
    }

    public enum DocumentPropertiesDialogMode
    {
        Open,
        Save
    }
    public class DocumentPropertiesDialog
    {
        private DocumentPropertiesDialogBase dlg = null;

        private DocumentPropertiesDialogMode mvarMode = DocumentPropertiesDialogMode.Open;
        public DocumentPropertiesDialogMode Mode { get { return mvarMode; } set { mvarMode = value; } }

        private ObjectModel mvarObjectModel = null;
        public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

        private DataFormat mvarDataFormat = null;
        public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

        private Accessor mvarAccessor = null;
        public Accessor Accessor { get { return mvarAccessor; } set { mvarAccessor = value; } }

        public DialogResult ShowDialog()
        {
            if (dlg == null) dlg = new DocumentPropertiesDialogBase();
            if (dlg.IsDisposed) dlg = new DocumentPropertiesDialogBase();

            dlg.Mode = mvarMode;
            dlg.ObjectModel = mvarObjectModel;
            dlg.DataFormat = mvarDataFormat;
            dlg.Accessor = mvarAccessor;

            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                mvarObjectModel = dlg.ObjectModel;
                mvarDataFormat = dlg.DataFormat;
                mvarAccessor = dlg.Accessor;
                return DialogResult.OK;
            }
            return DialogResult.Cancel;
        }
    }
}
