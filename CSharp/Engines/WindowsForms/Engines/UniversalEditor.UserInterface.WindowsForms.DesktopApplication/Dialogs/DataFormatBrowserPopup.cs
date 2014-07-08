using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
    public partial class DataFormatBrowserPopup : Form
    {
        public event EventHandler SelectionChanged;

        public DataFormatBrowserPopup()
        {
            InitializeComponent();
            Font = SystemFonts.MenuFont;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Close();
        }

        private System.Collections.ObjectModel.Collection<DataFormat> mvarDataFormats = new System.Collections.ObjectModel.Collection<DataFormat>();
        public System.Collections.ObjectModel.Collection<DataFormat> DataFormats { get { return mvarDataFormats; } }
        
        private DataFormat mvarDataFormat = null;
        public DataFormat DataFormat { get { return mvarDataFormat; } set { mvarDataFormat = value; } }

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

        
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            UpdateSearch();
        }

        private void UpdateSearch()
        {
            lv.Items.Clear();
            foreach (DataFormat df in mvarDataFormats)
            {
                DataFormatReference dfr = df.MakeReference();
                if (txtSearch.Text == String.Empty)
                {
                    AddDataFormatToList(dfr);
                }
                else
                {
                    if (dfr.Title.ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        AddDataFormatToList(dfr);
                    }
                    else
                    {
                        bool nextDFR = false;
                        foreach (DataFormatFilter filter in dfr.Filters)
                        {
                            foreach (string s in filter.FileNameFilters)
                            {
                                if (s.ToLower().Contains(txtSearch.Text.ToLower()))
                                {
                                    AddDataFormatToList(dfr);
                                    nextDFR = true;
                                    break;
                                }
                            }
                            if (nextDFR) break;
                        }
                        if (nextDFR) continue;
                    }
                }
            }

            if (lv.Items.Count == 1)
            {
                lv.Items[0].Selected = true;
            }
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void AddDataFormatToList(DataFormatReference dfr)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = dfr.Title;
            lvi.Tag = dfr;
            lvi.SubItems.Add(DataFormatFilterCollectionToString(dfr.Filters));
            lv.Items.Add(lvi);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateSearch();
        }

        private void lv_ItemActivate(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count != 1) return;

            mvarDataFormat = (lv.SelectedItems[0].Tag as DataFormatReference).Create();
            if (SelectionChanged != null) SelectionChanged(this, e);

            Close();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lv.SelectedItems.Count != 1) return;

                mvarDataFormat = (lv.SelectedItems[0].Tag as DataFormatReference).Create();
                if (SelectionChanged != null) SelectionChanged(this, e);

                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            mvarDataFormat = null;
            if (SelectionChanged != null) SelectionChanged(this, e);
            Close();
        }
    }
}
