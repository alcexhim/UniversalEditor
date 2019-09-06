﻿//
//  FileSystemEditor.Designer.cs - UWT Designer portions of FIleSystemEditor.cs
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
using UniversalWidgetToolkit.Input.Mouse;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.Editors.FileSystem
{
	partial class FileSystemEditor
	{
		private ListView tv = null;
		private DefaultTreeModel tmTreeView = null;

		private Menu contextMenuUnselected = new Menu();
		private Menu contextMenuSelected = new Menu();

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			this.tmTreeView = new DefaultTreeModel(new Type[] { typeof(string), typeof(string), typeof(string), typeof(string) });

			this.tv = new ListView();
			this.tv.Mode = ListViewMode.Detail;
			this.tv.SelectionMode = SelectionMode.Multiple;
			this.tv.Model = this.tmTreeView;

			contextMenuUnselected.Items.AddRange(new MenuItem[]
			{
				new CommandMenuItem("_View", new MenuItem[]
				{
					new CommandMenuItem("T_humbnails"),
					new CommandMenuItem("Tile_s"),
					new CommandMenuItem("Ico_ns"),
					new CommandMenuItem("_List"),
					new CommandMenuItem("_Details")
				}),
				new SeparatorMenuItem(),
				new CommandMenuItem("Arrange _Icons by", new MenuItem[]
				{
					new CommandMenuItem("_Name"),
					new CommandMenuItem("_Size"),
					new CommandMenuItem("_Type"),
					new CommandMenuItem("_Date modified"),
					new SeparatorMenuItem(),
					new CommandMenuItem("Show in _groups"),
					new CommandMenuItem("_Auto arrange"),
					new CommandMenuItem("A_lign to grid")
				}),
				new CommandMenuItem("_Refresh"),
				new SeparatorMenuItem(),
				new CommandMenuItem("_Paste"),
				new CommandMenuItem("Paste _shortcut"),
				new SeparatorMenuItem(),
				new CommandMenuItem("Ne_w", new MenuItem[]
				{
					new CommandMenuItem("_Folder"),
					new CommandMenuItem("_Shortcut"),
					new SeparatorMenuItem(),
					new CommandMenuItem("Briefcase"),
					new CommandMenuItem("Bitmap image"),
					new CommandMenuItem("Formatted text document"),
					new CommandMenuItem("Plain text document"),
					new CommandMenuItem("Wave sound"),
					new CommandMenuItem("Compressed (zipped) folder")
				}),
				new SeparatorMenuItem(),
				new CommandMenuItem("P_roperties")
			});

			contextMenuSelected.Items.AddRange(new MenuItem[]
			{
				new CommandMenuItem("_Open"),
				new SeparatorMenuItem(),
				new CommandMenuItem("Open in New _Tab"),
				new CommandMenuItem("Open in New _Window"),
				new SeparatorMenuItem(),
				new CommandMenuItem("Se_nd to"),
				new SeparatorMenuItem(),
				new CommandMenuItem("Cu_t"),
				new CommandMenuItem("_Copy"),
				new SeparatorMenuItem(),
				new CommandMenuItem("Move to..."),
				new CommandMenuItem("Copy to...", null, ContextMenuCopyTo_Click),
				new SeparatorMenuItem(),
				new CommandMenuItem("Create _shortcut"),
				new CommandMenuItem("_Delete"),
				new CommandMenuItem("Rena_me"),
				new SeparatorMenuItem(),
				new CommandMenuItem("P_roperties")
			});

			this.tv.BeforeContextMenu += tv_BeforeContextMenu;

			this.tv.Columns.Add(new ListViewColumnText(tmTreeView.Columns[0], "Name"));
			this.tv.Columns.Add(new ListViewColumnText(tmTreeView.Columns[1], "Size"));
			this.tv.Columns.Add(new ListViewColumnText(tmTreeView.Columns[2], "Type"));
			this.tv.Columns.Add(new ListViewColumnText(tmTreeView.Columns[3], "Date modified"));

			this.Controls.Add(this.tv, new BoxLayout.Constraints(true, true));
		}

	}
}
