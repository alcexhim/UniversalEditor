//
//  GenericBrowserPopup.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Input.Keyboard;

namespace UniversalEditor.UserInterface.Dialogs
{
	public partial class GenericBrowserPopup<TObj, TRef> : Dialog
		where TObj : class, References<TRef>
		where TRef : class, ReferencedBy<TObj>
	{
		public GenericBrowserPopup()
		{
			this.InitializeComponent();
		}

		public event EventHandler SelectionChanged;

		private bool mvarAutoClose = true;
		public bool AutoClose { get { return mvarAutoClose; } set { mvarAutoClose = value; } }
		/*
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			if (mvarAutoClose) this.Close();
		}
		*/
		public Collection<TRef> AvailableObjects { get; } = new Collection<TRef>();
		public TObj SelectedObject { get; set; } = default(TObj);

		/*
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			UpdateSearch();
		}
		*/

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			UpdateSearch();
		}

		private void txtSearch_Changed(object sender, EventArgs e)
		{
			UpdateSearch();
		}

		private void UpdateSearch()
		{
			tm.Rows.Clear();
			foreach (TRef item in AvailableObjects)
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

			if (tm.Rows.Count == 1)
			{
				// lv.Items[0].Selected = true;
			}
			// lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void AddObjectToList(TRef itmr)
		{
			string[] details = itmr.GetDetails();
			List<TreeModelRowColumn> columns = new List<TreeModelRowColumn>();
			
			for (int i = 0; i < details.Length; i++)
			{
				string str = details[i];
				if (String.IsNullOrEmpty(str)) str = String.Empty;
				
				columns.Add(new TreeModelRowColumn(tm.Columns[i], str));
			}

			TreeModelRow lvi = new TreeModelRow(columns.ToArray()); 
			lvi.SetExtraData<TRef>("TRef", itmr);
			tm.Rows.Add(lvi);
		}

		private void lv_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			// if (lv.SelectedItems.Count != 1) return;

			SelectedObject = e.Row.GetExtraData<TRef>("TRef")?.Create();
			SelectionChanged?.Invoke(this, e);
			Close();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == KeyboardKey.Enter)
			{
				if (lv.SelectedRows.Count != 1) return;
				
				SelectedObject = lv.SelectedRows[0].GetExtraData<TRef>("TRef")?.Create();
				if (SelectionChanged != null) SelectionChanged(this, e);
				
				Close();
			}
			else if (e.KeyData == KeyboardKey.Escape)
			{
				// already handled by GTK? but what about other platforms
				// Close();
			}
		}

		private void chkShowAll_Click(object sender, EventArgs e)
		{
			UpdateSearch();
		}

		private void cmdClear_Click(object sender, EventArgs e)
		{
			// mvarSelectedObject = null;
			if (SelectionChanged != null) SelectionChanged(this, e);
			// Close();
		}
	}
}
