//
//  TreeModelRowColumnEvent.cs
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
namespace MBS.Framework.UserInterface
{
	public class TreeModelRowColumnEditingEventArgs : System.ComponentModel.CancelEventArgs
	{
		public TreeModelRow Row { get; private set; }
		public TreeModelRowColumn Column { get; private set; }
		public object OldValue { get; private set; }
		public object NewValue { get; set; } = null;

		public TreeModelRowColumnEditingEventArgs(TreeModelRow row, TreeModelRowColumn column, object oldvalue, object newvalue)
		{
			Row = row;
			Column = column;
			OldValue = oldvalue;
			NewValue = newvalue;
		}
	}
	public delegate void TreeModelRowColumnEditingEventHandler(object sender, TreeModelRowColumnEditingEventArgs e);
	public class TreeModelRowColumnEditedEventArgs : EventArgs
	{
		public TreeModelRow Row { get; private set; }
		public TreeModelRowColumn Column { get; private set; }
		public object OldValue { get; private set; }
		public object NewValue { get; private set; }

		public TreeModelRowColumnEditedEventArgs(TreeModelRow row, TreeModelRowColumn column, object oldvalue, object newvalue)
		{
			Row = row;
			Column = column;
			OldValue = oldvalue;
			NewValue = newvalue;
		}
	}
	public delegate void TreeModelRowColumnEditedEventHandler(object sender, TreeModelRowColumnEditedEventArgs e);
}
