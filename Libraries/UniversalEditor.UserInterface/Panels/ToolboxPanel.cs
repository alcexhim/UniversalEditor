//
//  ToolboxPanel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Panels
{
	public class ToolboxPanel : Panel
	{
		public static readonly Guid ID = new Guid("{25332d64-6acb-4611-bcd7-d0c652b8653d}");

		private DefaultTreeModel tmToolbox = new DefaultTreeModel(new Type[] { typeof(string) });

		public ToolboxPanel()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			ListViewControl lvToolbox = new ListViewControl();
			lvToolbox.RowActivated += LvToolbox_RowActivated;
			lvToolbox.Model = tmToolbox;
			lvToolbox.Columns.Add(new ListViewColumn("Item", new CellRenderer[] { new CellRendererText(tmToolbox.Columns[0]) }));
			lvToolbox.HeaderStyle = ColumnHeaderStyle.None;

			Controls.Add(lvToolbox, new BoxLayout.Constraints(true, true));
		}

		protected override void OnEditorChanged(EditorChangedEventArgs e)
		{
			base.OnEditorChanged(e);

			if (e.CurrentEditor != null)
			{
				// initialize toolbox items
				EditorReference er = e.CurrentEditor.MakeReference();
				for (int i = 0; i < er.Toolbox.Items.Count; i++)
				{
					AddToolboxItem(er.Toolbox.Items[i]);
				}
			}
			else
			{
				ClearToolboxItems();
			}
		}

		void LvToolbox_RowActivated(object sender, ListViewRowActivatedEventArgs e)
		{
			MainWindow mw = (MainWindow)ParentWindow;

			Editor ed = mw.GetCurrentEditor();
			if (ed != null)
			{
				ed.ActivateToolboxItem(e.Row.GetExtraData<ToolboxItem>("item"));
			}
		}

		public void AddToolboxItem(ToolboxItem item)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[] { new TreeModelRowColumn(tmToolbox.Columns[0], item.Name) });
			row.SetExtraData<ToolboxItem>("item", item);
			tmToolbox.Rows.Add(row);
		}
		public void ClearToolboxItems()
		{
			tmToolbox.Rows.Clear();
		}
	}
}
