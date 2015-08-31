using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class GenericBrowserPopup<TObj, TRef> : Form
		where TObj : class, References<TRef>
		where TRef : class, ReferencedBy<TObj>
	{
		public event EventHandler SelectionChanged;

		public GenericBrowserPopup()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private bool mvarAutoClose = true;
		public bool AutoClose { get { return mvarAutoClose; } set { mvarAutoClose = value; } }

		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			if (mvarAutoClose) this.Close();
		}

		private System.Collections.ObjectModel.Collection<TRef> mvarAvailableObjects = new System.Collections.ObjectModel.Collection<TRef>();
		public System.Collections.ObjectModel.Collection<TRef> AvailableObjects { get { return mvarAvailableObjects; } }

		private TObj mvarSelectedObject = default(TObj);
		public TObj SelectedObject { get { return mvarSelectedObject; } set { mvarSelectedObject = value; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			UpdateSearch();
		}

		private void UpdateSearch()
		{
			lv.Items.Clear();
			foreach (TRef item in mvarAvailableObjects)
			{
				bool itemShouldFilter = false;
				string[] details = item.GetDetails();
				foreach (string detail in details)
				{
					if (detail == null) continue;
					if (detail.ToLower().Trim().Contains(txtSearch.Text.ToLower().Trim()))
					{
						itemShouldFilter = true;
						break;
					}
				}
				if (String.IsNullOrEmpty(txtSearch.Text.Trim()) || itemShouldFilter)
				{
					AddObjectToList(item);
				}
			}

			if (lv.Items.Count == 1)
			{
				lv.Items[0].Selected = true;
			}
			lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void AddObjectToList(TRef itmr)
		{
			ListViewItem lvi = new ListViewItem();
			string[] details = itmr.GetDetails();
			lvi.Text = details[0];
			for (int i = 1; i < details.Length; i++)
			{
				lvi.SubItems.Add(details[i]);
			}
			lvi.Tag = itmr;
			lv.Items.Add(lvi);
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			UpdateSearch();
		}

		private void lv_ItemActivate(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count != 1) return;

			mvarSelectedObject = (lv.SelectedItems[0].Tag as TRef).Create();
			if (SelectionChanged != null) SelectionChanged(this, e);

			Close();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (lv.SelectedItems.Count != 1) return;

				mvarSelectedObject = (lv.SelectedItems[0].Tag as TRef).Create();
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
			mvarSelectedObject = null;
			if (SelectionChanged != null) SelectionChanged(this, e);
			Close();
		}
	}
}
