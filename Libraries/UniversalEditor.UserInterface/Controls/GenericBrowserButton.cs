//
//  GenericBrowserButton.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls.ListView;

namespace UniversalEditor.UserInterface.Controls
{
	public class GenericBrowserButton<TObj, TRef> : DropDownButton
		where TObj : class, References<TRef>
		where TRef : class, ReferencedBy<TObj>
	{
		public GenericBrowserButton ()
		{
			initct ();
		}

		public event EventHandler SelectionChanged;

		private ListViewControl lv = null;
		private DefaultTreeModel tm = null;
		public Collection<TRef> AvailableObjects { get; } = new Collection<TRef>();

		public string Label { get; set; } = null;

		private TObj _SelectedObject = default(TObj);
		public TObj SelectedObject
		{
			get { return _SelectedObject; }
			set
			{
				_SelectedObject = value;

				Text = Label;
				if (_SelectedObject != null)
				{
					TRef _ref = _SelectedObject.MakeReference();
					if (_ref != null)
					{
						string[] deets = _ref.GetDetails();
						if (deets.Length > 0)
							Text = Label + deets[0];
					}
				}
				else
				{
					ResetLabel();
				}
			}
		}

		private void ResetLabel()
		{
			Text = Label + "(not selected)";
		}

		private TextBox txtSearch = null;

		private void initct()
		{
			Container ctObjectModel = new Container ();
			ctObjectModel.Layout = new BoxLayout (Orientation.Vertical);
			ctObjectModel.MinimumSize = new MBS.Framework.Drawing.Dimension2D (300, 200);

			txtSearch = new TextBox ();
			txtSearch.Changed += txtSearch_Changed;
			txtSearch.KeyDown += txtSearch_KeyDown;
			ctObjectModel.Controls.Add (txtSearch, new BoxLayout.Constraints (false, true));

			tm = new DefaultTreeModel (new Type[] { typeof(string), typeof(string) });

			lv = new ListViewControl();
			lv.Model = tm;
			lv.Columns.Add(new ListViewColumn("Name", new CellRenderer[] { new CellRendererText(lv.Model.Columns[0]) }));
			lv.Columns.Add(new ListViewColumn("Description", new CellRenderer[] { new CellRendererText(lv.Model.Columns[1]) }));
			lv.HeaderStyle = ColumnHeaderStyle.None;
			ctObjectModel.Controls.Add (lv, new BoxLayout.Constraints (true, true));

			this.Container = ctObjectModel;
		}

		protected override void OnCreated (EventArgs e)
		{
			base.OnCreated (e);

			ResetLabel();
			UpdateSearch ();
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
				if (String.IsNullOrEmpty (txtSearch.Text)) {
					itemShouldFilter = true;
				} else {
					string [] details = item.GetDetails ();
					foreach (string detail in details) {
						if (detail == null) continue;
						if (detail.ToLower ().Trim ().Contains (txtSearch.Text.ToLower ().Trim ())) {
							itemShouldFilter = true;
							break;
						}
					}
				}
				if (itemShouldFilter) {
					AddObjectToList (item);
				}
			}

			lv.SelectedRows.Clear();
			if (tm.Rows.Count == 1)
			{
				lv.SelectedRows.Add(tm.Rows[0]);
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
			CloseDropDown();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == KeyboardKey.Enter)
			{
				if (lv.SelectedRows.Count != 1)
					return;

				SelectedObject = lv.SelectedRows[0].GetExtraData<TRef>("TRef")?.Create();
				if (SelectionChanged != null) SelectionChanged(this, e);

				CloseDropDown();
			}
			else if (e.Key == KeyboardKey.Escape)
			{
				// already handled by GTK? but what about other platforms
				// Close();
			}
		}

		protected override void OnDropDownOpened(EventArgs e)
		{
			base.OnDropDownOpened(e);
			txtSearch.Focus();
		}
	}
}
