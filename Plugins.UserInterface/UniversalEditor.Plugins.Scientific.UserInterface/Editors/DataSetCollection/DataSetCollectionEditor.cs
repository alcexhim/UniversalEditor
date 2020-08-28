//
//  DataSetEditor.cs
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
using System.Collections.Generic;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using UniversalEditor.Plugins.Scientific.ObjectModels.DataSetCollection;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Scientific.UserInterface.Editors.DataSetCollection
{
	[ContainerLayout("~/Editors/Scientific/DataSet/DataSetEditor.glade")]
	public class DataSetCollectionEditor : Editor
	{
		private ListViewControl tv = null;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(DataSetCollectionObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			OnObjectModelChanged(e);
		}

		private void tm_RowCompare(object sender, TreeModelRowCompareEventArgs e)
		{
			float? _Value = e.Left.RowColumns[e.ColumnIndex].GetExtraData<float?>("value");
			if (_Value == null) _Value = 0.0f;

			float? value = e.Right.RowColumns[e.ColumnIndex].GetExtraData<float?>("value");
			if (value == null) value = 0.0f;

			e.Value = _Value.Value.CompareTo(value.Value);
			e.Handled = true;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			tv.Columns.Clear();
			tv.Model = null;

			DataSetCollectionObjectModel dsc = (ObjectModel as DataSetCollectionObjectModel);
			if (dsc == null) return;

			DataSet ds = null;
			if (dsc.DataSets.Count > 0)
				ds = dsc.DataSets[0];

			if (ds != null)
			{
				List<Type> list = new List<Type>();
				for (int i = 0; i < ds.Dimensions; i++)
				{
					list.Add(typeof(string));
				}
				DefaultTreeModel tm = new DefaultTreeModel(list.ToArray());
				tm.RowCompare += tm_RowCompare;

				for (int i = 0; i < ds.Dimensions; i++)
				{
					tv.Columns.Add(new ListViewColumnText(tm.Columns[i], i.ToString()));

					for (int j = 0; j < ds.Sizes[i]; j++)
					{
						float? val = ds.GetValue(i, j);

						TreeModelRow row = null;
						if (j >= tm.Rows.Count)
						{
							row = new TreeModelRow();
							for (int i1 = 0; i1 < ds.Dimensions; i1++)
							{
								row.RowColumns.Add(new TreeModelRowColumn(tm.Columns[i], String.Empty));
							}
							tm.Rows.Add(row);
						}
						else
						{
							row = tm.Rows[j];
						}
						row.RowColumns[i].Value = (val == null ? String.Empty : val.ToString());
						row.RowColumns[i].SetExtraData<float?>("value", val);
					}
				}

				tv.Model = tm;
			}
		}
	}
}
