//
//  GenericBrowserPopup.Designer.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.Drawing;

namespace UniversalEditor.UserInterface.Dialogs
{
	partial class GenericBrowserPopup<TObj, TRef>
		where TObj : class, References<TRef>
		where TRef : class, ReferencedBy<TObj>
	{
		private CheckBox chkShowAll = null;
		private TextBox txtSearch = null;
		private ListView lv = null;

		private Container ctSearchAndShowAll = null;
		private DefaultTreeModel tm = null;

		private void InitializeComponent()
		{
			this.Decorated = false;
			this.Layout = new BoxLayout(Orientation.Vertical);

			
			this.ctSearchAndShowAll = new Container();
			this.ctSearchAndShowAll.Layout = new BoxLayout(Orientation.Horizontal);
			
			this.txtSearch = new TextBox();
			this.txtSearch.Changed += txtSearch_Changed;
			this.txtSearch.KeyDown += txtSearch_KeyDown;
			this.ctSearchAndShowAll.Controls.Add(this.txtSearch, new BoxLayout.Constraints(true, true));

			this.chkShowAll = new CheckBox();
			this.chkShowAll.Text = "Show _all";
			this.chkShowAll.Click += chkShowAll_Click;
			this.ctSearchAndShowAll.Controls.Add(this.chkShowAll, new BoxLayout.Constraints(false, false));
			
			this.Controls.Add(ctSearchAndShowAll, new BoxLayout.Constraints(false, true));

			this.tm = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });

			this.lv = new ListView();
			lv.Columns.Add(new ListViewColumnText(tm.Columns[0], "Name"));
			lv.Columns.Add(new ListViewColumnText(tm.Columns[1], "Description"));
			lv.RowActivated += this.lv_RowActivated;
			this.lv.Model = tm;
			this.Controls.Add(this.lv, new BoxLayout.Constraints(true, true));

			this.MinimumSize = new Dimension2D (300, 200);
		}

	}
}
