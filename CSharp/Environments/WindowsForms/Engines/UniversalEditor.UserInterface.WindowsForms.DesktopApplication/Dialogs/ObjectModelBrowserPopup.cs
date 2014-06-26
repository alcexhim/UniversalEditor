using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class ObjectModelBrowserPopup : Form
	{
		public event EventHandler SelectionChanged;

		public ObjectModelBrowserPopup()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			this.Close();
		}

		private System.Collections.ObjectModel.Collection<ObjectModel> mvarObjectModels = new System.Collections.ObjectModel.Collection<ObjectModel>();
		public System.Collections.ObjectModel.Collection<ObjectModel> ObjectModels { get { return mvarObjectModels; } }
		
		private ObjectModel mvarObjectModel = null;
		public ObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

		/*
		private void documentTypeSelector1_SelectionFinalized(object sender, EventArgs e)
		{
			if (documentTypeSelector1.SelectedObject == null) return;

			mvarObjectModel = (documentTypeSelector1.SelectedObject as ObjectModelReference).Create();
			Close();

			if (SelectionChanged != null) SelectionChanged(this, e);
		}

		private void documentTypeSelector1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				mvarObjectModel = (documentTypeSelector1.SelectedObject as ObjectModel);
				Close();

				if (SelectionChanged != null) SelectionChanged(this, e);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
		*/

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
			foreach (ObjectModel df in mvarObjectModels)
			{
				ObjectModelReference dfr = df.MakeReference();
				if (txtSearch.Text == String.Empty)
				{
					AddObjectModelToList(dfr);
				}
				else
				{
					if (String.IsNullOrEmpty(dfr.Title)) continue;

					if ((dfr.Title.ToLower().Contains(txtSearch.Text.Trim().ToLower()))
					|| (dfr.Description.ToLower().Contains(txtSearch.Text.Trim().ToLower())))
					{
						AddObjectModelToList(dfr);
					}
				}
			}

			if (lv.Items.Count == 1)
			{
				lv.Items[0].Selected = true;
			}
			lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void AddObjectModelToList(ObjectModelReference dfr)
		{
			ListViewItem lvi = new ListViewItem();
			lvi.Text = dfr.Title;
			lvi.SubItems.Add(dfr.Description);
			lvi.Tag = dfr;
			lv.Items.Add(lvi);
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			UpdateSearch();
		}

		private void lv_ItemActivate(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count != 1) return;

			mvarObjectModel = (lv.SelectedItems[0].Tag as ObjectModelReference).Create();
			if (SelectionChanged != null) SelectionChanged(this, e);

			Close();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (lv.SelectedItems.Count != 1) return;

				mvarObjectModel = (lv.SelectedItems[0].Tag as ObjectModelReference).Create();
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
			mvarObjectModel = null;
			if (SelectionChanged != null) SelectionChanged(this, e);
			Close();
		}
	}
}
