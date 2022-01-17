//
//  EditorPropertiesPanel.cs
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
using UniversalEditor.UserInterface.Panels;

namespace UniversalEditor.UserInterface
{
	public class EditorPropertiesPanel
	{
		public Editor Parent { get; private set; } = null;
		public EditorPropertiesPanel(Editor parent)
		{
			Parent = parent;
		}

		public PropertyPanelObject.PropertyPanelObjectCollection Objects { get; } = new PropertyPanelObject.PropertyPanelObjectCollection(null);

		private PropertyPanelObject _SelectedObject = null;
		public PropertyPanelObject SelectedObject
		{
			get { return _SelectedObject; }
			set
			{
				_SelectedObject = value;

				if (Parent?.ParentWindow is EditorWindow)
				{
					((PropertyListPanel)(Parent.ParentWindow as EditorWindow).FindPanel(PropertyListPanel.ID)).SelectedObject = value;
				}
			}
		}

		public bool ShowObjectSelector { get; set; } = true;
	}
}
