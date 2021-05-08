//
//  BatchFindReplaceDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework;
using MBS.Framework.Logic.Conditional;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Keyboard;

namespace UniversalEditor.UserInterface.Dialogs
{
	[ContainerLayout("~/Dialogs/BatchFindReplaceDialog.glade")]
	public class BatchFindReplaceWindow : Window
	{
		private ListViewControl tvCriteria;
		private ListViewControl tvResults;

		private Toolbar tbCriteria;
		private DefaultTreeModel tmCriteria;

		private Button cmdFind;
		private Button cmdReplace;

		public Editor Editor { get; set; } = null;

		public BatchFindReplaceWindow()
		{
			MenuBar.Visible = false;
			StatusBar.Visible = false;
			CommandDisplayMode = CommandDisplayMode.None;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			(tbCriteria.Items["tsbCriteriaAdd"] as ToolbarItemButton).Click += tsbCriteriaAdd_Click;
			(tbCriteria.Items["tsbCriteriaEdit"] as ToolbarItemButton).Click += tsbCriteriaEdit_Click;
			(tbCriteria.Items["tsbCriteriaRemove"] as ToolbarItemButton).Click += tsbCriteriaRemove_Click;
		}

		[EventHandler(nameof(tvCriteria), "RowActivated")]
		private void tvCriteria_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			tsbCriteriaEdit_Click(sender, e);
		}


		private void tsbCriteriaAdd_Click(object sender, EventArgs e)
		{
			BatchFindReplaceCriteriaPropertiesDialog dlg = new BatchFindReplaceCriteriaPropertiesDialog();
			dlg.Editor = Editor;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tmCriteria.Columns[0], dlg.Object.Name),
					new TreeModelRowColumn(tmCriteria.Columns[1], dlg.Property.Name),
					new TreeModelRowColumn(tmCriteria.Columns[2], dlg.ComparisonType.ToString()),
					new TreeModelRowColumn(tmCriteria.Columns[3], dlg.Value)
				});

				query.Criteria.Add(new Criterion(dlg.Object, dlg.Property, dlg.ComparisonType, dlg.Value));
				tmCriteria.Rows.Add(row);
			}
		}
		private void tsbCriteriaEdit_Click(object sender, EventArgs e)
		{
			if (tvCriteria.SelectedRows.Count != 1)
				return;

			TreeModelRow row = tvCriteria.SelectedRows[0];

			BatchFindReplaceCriteriaPropertiesDialog dlg = new BatchFindReplaceCriteriaPropertiesDialog();
			dlg.Editor = Editor;
			// dlg.Object = row.RowColumns[0].Value.ToString();
			// dlg.Property = row.RowColumns[1].Value.ToString();
			if (Enum.TryParse<ConditionComparison>(row.RowColumns[2].Value.ToString(), out ConditionComparison comparisonType))
			{
				dlg.ComparisonType = comparisonType;
			}

			dlg.Value = row.RowColumns[3].Value.ToString();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// row.RowColumns[0].Value = dlg.ObjectName;
				// row.RowColumns[1].Value = dlg.PropertyName;
				row.RowColumns[2].Value = dlg.ComparisonType.ToString();
				row.RowColumns[3].Value = dlg.Value;
			}
		}
		private void tsbCriteriaRemove_Click(object sender, EventArgs e)
		{
			if (MessageDialog.ShowDialog("Are you sure you want to remove the selected criteria?", "Remove Criteria", MessageDialogButtons.YesNo, MessageDialogIcon.Question) == DialogResult.No)
				return;


		}

		private CriteriaQuery query = new CriteriaQuery();

		[EventHandler(nameof(cmdFind), "Click")]
		private void cmdFind_Click(object sender, EventArgs e)
		{
			CriteriaResult[] results = Editor.ObjectModel.Find(query);
			if (results == null || results.Length == 0)
			{
				MessageDialog.ShowDialog("No match found", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			for (int i = 0; i < results.Length; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(tvResults.Model.Columns[0], results[i].Value?.ToString())
				});
				row.SetExtraData<CriteriaResult>("result", results[i]);
				tvResults.Model.Rows.Add(row);
			}
		}

		[EventHandler(nameof(tvResults), "RowActivated")]
		private void tvResults_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			if (e.Row == null) return;

			CriteriaResult result = e.Row.GetExtraData<CriteriaResult>("result");
			if (result == null) return;

			Editor editor = ((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).GetCurrentEditor();
			if (editor == null) return;

			editor.Selections.Add(editor.CreateSelection(result.Value));

			((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).Present(DateTime.Now);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Key == KeyboardKey.Escape)
			{
				Close();
			}
		}
	}
}
