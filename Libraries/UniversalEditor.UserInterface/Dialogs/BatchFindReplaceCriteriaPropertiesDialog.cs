//
//  BatchFindReplaceCriteriaPropertiesDialog.cs
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
using MBS.Framework.Logic.Conditional;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;

namespace UniversalEditor.UserInterface.Dialogs
{
	[ContainerLayout("~/Dialogs/BatchFindReplaceCriteriaPropertiesDialog.glade", "GtkDialog")]
	public class BatchFindReplaceCriteriaPropertiesDialog : CustomDialog
	{
		private Button cmdOK;
		private ComboBox cboObjectName;
		private ComboBox cboPropertyName;
		private ComboBox cboComparisonType;
		private ComboBox cboValue;

		public CriteriaObject Object { get; set; } = null;
		public CriteriaProperty Property { get; set; }
		public ConditionComparison ComparisonType { get; set; }
		public string Value { get; set; }

		public Editor Editor { get; set; } = null;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			cboValue.Text = Value;

			(cboObjectName.Model as DefaultTreeModel).Rows.Clear();
			CriteriaObject[] objects = Editor.ObjectModel.GetCriteriaObjects();
			for (int i = 0; i < objects.Length; i++)
			{
				TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboObjectName.Model.Columns[0], objects[i].Name)
				});
				row.SetExtraData<CriteriaObject>("object", objects[i]);
				(cboObjectName.Model as DefaultTreeModel).Rows.Add(row);

				if (objects[i] == Object)
					cboObjectName.SelectedItem = row;
			}

			DefaultButton = cmdOK;
		}

		[EventHandler(nameof(cboObjectName), "Changed")]
		private void cboObjectName_Changed(object sender, EventArgs e)
		{
			TreeModelRow row = cboObjectName.SelectedItem;
			if (row == null) return;

			CriteriaObject obj = row.GetExtraData<CriteriaObject>("object");
			if (obj == null) return;

			(cboPropertyName.Model as DefaultTreeModel).Rows.Clear();
			for (int i = 0; i < obj.Properties.Count; i++)
			{
				TreeModelRow row2 = new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboPropertyName.Model.Columns[0], obj.Properties[i].Name)
				});
				row2.SetExtraData<CriteriaProperty>("property", obj.Properties[i]);
				(cboPropertyName.Model as DefaultTreeModel).Rows.Add(row2);

				if (obj.Properties[i] == Property)
					cboPropertyName.SelectedItem = row2;
			}
		}

		[EventHandler(nameof(cboPropertyName), "Changed")]
		private void cboPropertyName_Changed(object sender, EventArgs e)
		{
			TreeModelRow row = cboPropertyName.SelectedItem;
			if (row == null) return;

			CriteriaProperty prop = row.GetExtraData<CriteriaProperty>("property");
			if (prop == null) return;

			(cboComparisonType.Model as DefaultTreeModel).Rows.Clear();

			(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Equal to"),
				new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 1)
			}));
			if (prop.DataType == typeof(string))
			{
				// string-only comparisons
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Starts with"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 32)
				}));
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Ends with"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 64)
				}));
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Contains"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 128)
				}));
			}
			else if (prop.DataType == typeof(byte) || prop.DataType == typeof(sbyte) ||
				prop.DataType == typeof(short) || prop.DataType == typeof(ushort) ||
				prop.DataType == typeof(int) || prop.DataType == typeof(uint) ||
				prop.DataType == typeof(long) || prop.DataType == typeof(ulong) ||
				prop.DataType == typeof(float) || prop.DataType == typeof(double))
			{
				// integer comparisons
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Greater than"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 4)
				}));
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Less than"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 8)
				}));
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Greater than or equal to"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 5)
				}));
				(cboComparisonType.Model as DefaultTreeModel).Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
				{
					new TreeModelRowColumn(cboComparisonType.Model.Columns[0], "Less than or equal to"),
					new TreeModelRowColumn(cboComparisonType.Model.Columns[1], 9)
				}));
			}
		}

		[EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			Object = cboObjectName.SelectedItem?.GetExtraData<CriteriaObject>("object");
			Property = cboPropertyName.SelectedItem?.GetExtraData<CriteriaProperty>("property");
			if (cboComparisonType.SelectedItem != null)
			{
				ComparisonType = (ConditionComparison)((int)cboComparisonType.SelectedItem.RowColumns[1].Value);
			}
			Value = cboValue.Text;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
