//
//  DatabaseEditor.Designer.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.Database
{
	partial class DatabaseEditor : Editor
	{
		private SplitContainer scExplorerContent = null;
		private SplitContainer scContentResults = null;
		private SyntaxTextBox txtQuery = null;
		private TabContainer tbsResults = null;
		private TabPage tabResultsResults = null;
		private TabPage tabSpatialResults = null;

		private DefaultTreeModel tmResults = null;
		private ListView lvResults = null;

		private DefaultTreeModel tmDatabase = null;
		private ListView tvDatabase = null;

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Horizontal);

			this.tmDatabase = new DefaultTreeModel(new Type[] { typeof(string) });

			this.tvDatabase = new ListView();
			this.tvDatabase.Columns.Add(new ListViewColumnText(this.tmDatabase.Columns[0], "Item"));
			this.tvDatabase.Model = this.tmDatabase;

			this.scExplorerContent = new SplitContainer();
			this.scExplorerContent.Orientation = Orientation.Vertical;
			this.scExplorerContent.Panel1.Layout = new BoxLayout(Orientation.Horizontal);
			this.scExplorerContent.Panel1.Controls.Add(tvDatabase, new BoxLayout.Constraints(true, true));

			this.scExplorerContent.Panel2.Layout = new BoxLayout(Orientation.Horizontal);

			this.scContentResults = new SplitContainer();
			this.scContentResults.Panel1.Layout = new BoxLayout(Orientation.Horizontal);

			this.txtQuery = new SyntaxTextBox();
			this.scContentResults.Panel1.Controls.Add(this.txtQuery, new BoxLayout.Constraints(true, true));

			this.lvResults = new ListView();

			this.tbsResults = new TabContainer();
			this.tabResultsResults = new TabPage();
			this.tabResultsResults.Text = "Results";
			this.tabResultsResults.Layout = new BoxLayout(Orientation.Vertical);
			this.tabResultsResults.Controls.Add(this.lvResults, new BoxLayout.Constraints(true, true));
			this.tbsResults.TabPages.Add(this.tabResultsResults);

			this.tabSpatialResults = new TabPage();
			this.tabSpatialResults.Text = "Spatial Results";
			this.tbsResults.TabPages.Add(this.tabSpatialResults);

			this.scContentResults.Panel2.Layout = new BoxLayout(Orientation.Vertical);
			this.scContentResults.Panel2.Controls.Add(this.tbsResults, new BoxLayout.Constraints(true, true));
			this.scContentResults.SplitterPosition = 128;

			this.scExplorerContent.Panel2.Controls.Add(this.scContentResults, new BoxLayout.Constraints(true, true));
			this.scExplorerContent.SplitterPosition = 128;

			this.Controls.Add(scExplorerContent, new BoxLayout.Constraints(true, true));
		}
	}
}
