//
//  NewDialog.Designer.cs
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

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.UserInterface.Dialogs
{
	partial class NewDialog
	{
		private TextBox txtFileName = null;
		private TextBox txtFilePath = null;

		private CheckBox chkAddToSolution = null;
		private TextBox txtSolutionName = null;

		private ListView tvObjectModel = new ListView();
		private DefaultTreeModel tmObjectModel = null;

		private ListView tvTemplate = new ListView();
		private DefaultTreeModel tmTemplate = null;
		private TextBox txtSearch = null;

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);
			
			this.txtSearch = new TextBox();
			txtSearch.Changed += txtSearch_Changed;
			this.Controls.Add(txtSearch, new BoxLayout.Constraints(false, true));

			tmObjectModel = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });
			tmTemplate = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });

			tvObjectModel.Model = tmObjectModel;
			tvObjectModel.SelectionChanged += tvObjectModel_SelectionChanged;
			tvTemplate.Model = tmTemplate;
			// tvTemplate.Mode = ListViewMode.LargeIcon;

			tvObjectModel.Columns.Add(new ListViewColumnText(tmObjectModel.Columns[0], "Name"));
			tvObjectModel.Columns.Add(new ListViewColumnText(tmObjectModel.Columns[1], "Description"));
			tvObjectModel.HeaderStyle = ColumnHeaderStyle.None;

			tvTemplate.Columns.Add(new ListViewColumnText(tmTemplate.Columns[0], "Name"));
			tvTemplate.Columns.Add(new ListViewColumnText(tmTemplate.Columns[1], "Description"));
			tvTemplate.HeaderStyle = ColumnHeaderStyle.None;
			tvTemplate.SelectionChanged += tvTemplate_SelectionChanged;

			Container boxTreeViewAndListView = new Container();
			boxTreeViewAndListView.Layout = new BoxLayout(Orientation.Horizontal);

			boxTreeViewAndListView.Controls.Add(tvObjectModel, new BoxLayout.Constraints(true, true, 16));
			boxTreeViewAndListView.Controls.Add(tvTemplate, new BoxLayout.Constraints(true, true, 16, BoxLayout.PackType.End));

			this.Controls.Add(boxTreeViewAndListView, new BoxLayout.Constraints(true, true, 16));

			this.txtFileName = new TextBox();
			this.txtFilePath = new TextBox();
			this.txtSolutionName = new TextBox();

			Container tableParams = new Container();
			tableParams.Layout = new GridLayout();
			tableParams.Controls.Add(new Label("_Name:"), new GridLayout.Constraints(0, 0));
			tableParams.Controls.Add(txtFileName, new GridLayout.Constraints(0, 1, 1, 1, ExpandMode.Horizontal));
			tableParams.Controls.Add(new Label("_Location:"), new GridLayout.Constraints(1, 0));
			tableParams.Controls.Add(txtFilePath, new GridLayout.Constraints(1, 1, 1, 1, ExpandMode.Horizontal));

			this.chkAddToSolution = new CheckBox();
			this.chkAddToSolution.Text = "Add to " + (Mode == NewDialogMode.File ? "project" : "solution");
			tableParams.Controls.Add(chkAddToSolution, new GridLayout.Constraints(2, 0, 1, 2, ExpandMode.Horizontal));

			tableParams.Controls.Add(new Label((Mode == NewDialogMode.File ? "Project" : "Solution") + " na_me:"), new GridLayout.Constraints(3, 0));
			tableParams.Controls.Add(txtSolutionName, new GridLayout.Constraints(3, 1, 1, 1, ExpandMode.Horizontal));

			this.Controls.Add(tableParams, new BoxLayout.Constraints(true, true, 16, BoxLayout.PackType.End));


			this.Buttons.Add(new Button(ButtonStockType.New, DialogResult.None));
			this.Buttons.Add(new Button(ButtonStockType.Cancel, DialogResult.Cancel));

			this.Buttons[0].Click += cmdOK_Click;

			this.Text = "New File";
		}
	}
}
