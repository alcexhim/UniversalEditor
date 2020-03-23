//
//  PropertyListPanel.Designer.cs
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

namespace UniversalEditor.UserInterface.Panels
{
	public partial class PropertyListPanel : Panel
	{
		private ComboBox cboObject = null;
		private SplitContainer scPropertiesDescription = null;
		private ListView lvPropertyGrid = null;
		private TextBox txtCommands = null;
		private TextBox txtDescription = null;
		private DefaultTreeModel tmPropertyGrid = null;
		private DefaultTreeModel tmObject = null;
		private SplitContainer scCommandsDescription = null;

		private void InitializeComponent()
		{
			Text = "Property List";
			Layout = new BoxLayout(Orientation.Vertical);

			cboObject = new ComboBox();
			tmObject = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });
			cboObject.Model = tmObject;
			// i'd hate to limit it like this.. we should be able to type in an object name, hit "enter", and have it work
			// cboObject.ReadOnly = true;
			cboObject.KeyDown += CboObject_KeyDown;
			cboObject.Changed += cboObject_Changed;

			Controls.Add(cboObject);

			scPropertiesDescription = new SplitContainer(Orientation.Horizontal);
			scPropertiesDescription.Panel1.Layout = new BoxLayout(Orientation.Vertical);

			tmPropertyGrid = new DefaultTreeModel(new Type[] { typeof(string), typeof(string) });

			lvPropertyGrid = new ListView();
			lvPropertyGrid.SelectionChanged += lvPropertyGrid_SelectionChanged; ;
			lvPropertyGrid.Model = tmPropertyGrid;
			lvPropertyGrid.Columns.Add(new ListViewColumnText(tmPropertyGrid.Columns[0], "Name"));
			lvPropertyGrid.Columns.Add(new ListViewColumnText(tmPropertyGrid.Columns[1], "Value"));

			scPropertiesDescription.Panel1.Controls.Add(lvPropertyGrid, new BoxLayout.Constraints(true, true));
			scPropertiesDescription.Panel2.Layout = new BoxLayout(Orientation.Vertical);

			scCommandsDescription = new SplitContainer(Orientation.Horizontal);
			txtCommands = new TextBox();
			txtCommands.Editable = false;
			txtCommands.Multiline = true;
			txtCommands.Text = "<A>Command 1</A> <A>Command 2</A>";
			scCommandsDescription.SplitterPosition = 100;
			scCommandsDescription.Panel1.Layout = new BoxLayout(Orientation.Vertical);
			// scCommandsDescription.Panel1.Controls.Add(txtCommands, new BoxLayout.Constraints(true, true));
			// not sure how to do this yet

			txtDescription = new TextBox();
			txtDescription.Editable = false;
			txtDescription.Multiline = true;
			scCommandsDescription.Panel2.Layout = new BoxLayout(Orientation.Vertical);
			scCommandsDescription.Panel2.Controls.Add(txtDescription, new BoxLayout.Constraints(true, true));

			scPropertiesDescription.SplitterPosition = 100;
			scPropertiesDescription.Panel2.Controls.Add(scCommandsDescription, new BoxLayout.Constraints(true, true));

			Controls.Add(scPropertiesDescription, new BoxLayout.Constraints(true, true));
		}

		void CboObject_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
		{
			// this don't work, Gtk blocks keyboard input (probably cause it's technically a text box)
			if (e.Key == MBS.Framework.UserInterface.Input.Keyboard.KeyboardKey.Enter)
			{
				if (cboObject.Model != null)
				{
					TreeModelRow row = cboObject.Model.Find(cboObject.Text);
					if (row != null)
					{
						cboObject.SelectedItem = row;
					}
				}
			}
		}

	}
}
