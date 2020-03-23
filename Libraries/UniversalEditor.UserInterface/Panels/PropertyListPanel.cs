//
//  PropertyListPanel.cs
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
using System.Collections.Generic;
using MBS.Framework.UserInterface;

namespace UniversalEditor.UserInterface.Panels
{
	public class PropertyPanelProperty
	{
		public class PropertyPanelPropertyCollection
			: System.Collections.ObjectModel.Collection<PropertyPanelProperty>
		{
		}

		public string Name { get; set; } = String.Empty;
		public object Value { get; set; } = null;
		public Type DataType { get; set; } = null;

		public PropertyPanelProperty.PropertyPanelPropertyCollection Properties { get; } = new PropertyPanelPropertyCollection();

		public PropertyPanelProperty(string name, Type dataType = null, object value = null, PropertyPanelProperty[] properties = null)
		{
			Name = name;
			DataType = dataType;
			Value = value;
			if (properties != null)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					Properties.Add(properties[i]);
				}
			}
		}
	}
	public class PropertyPanelClass
	{
		public class PropertyPanelClassCollection
			: System.Collections.ObjectModel.Collection<PropertyPanelClass>
		{
		}

		public string Name { get; set; } = null;
		public PropertyPanelProperty.PropertyPanelPropertyCollection Properties { get; } = new PropertyPanelProperty.PropertyPanelPropertyCollection();

		public PropertyPanelClass(string name, PropertyPanelProperty[] properties = null)
		{
			Name = name;
			if (properties != null)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					Properties.Add(properties[i]);
				}
			}
		}
	}
	public class PropertyPanelObject
	{
		public class PropertyPanelObjectCollection
			: System.Collections.ObjectModel.Collection<PropertyPanelObject>
		{
			private PropertyListPanel _parent = null;
			internal PropertyPanelObjectCollection(PropertyListPanel parent)
			{
				_parent = parent;
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_parent.ClearPropertyPanelObjects();
			}
			protected override void InsertItem(int index, PropertyPanelObject item)
			{
				base.InsertItem(index, item);
				_parent.AddPropertyPanelObject(item);
			}
			protected override void RemoveItem(int index)
			{
				_parent.RemovePropertyPanelObject(this[index]);
				base.RemoveItem(index);
			}

		}

		public string Name { get; } = null;
		public PropertyPanelClass ObjectClass { get; } = null;

		public PropertyPanelProperty.PropertyPanelPropertyCollection Properties { get; } = new PropertyPanelProperty.PropertyPanelPropertyCollection();

		public PropertyPanelObject(string name, PropertyPanelClass clas)
		{
			Name = name;
			ObjectClass = clas;
		}
	}

	partial class PropertyListPanel : Panel
	{
		public PropertyPanelObject.PropertyPanelObjectCollection Objects { get; private set; } = null;

		public PropertyListPanel()
		{
			InitializeComponent();

			Objects = new PropertyPanelObject.PropertyPanelObjectCollection(this);
		}

		internal void ClearPropertyPanelObjects()
		{
			tmObject.Rows.Clear();
		}
		internal void AddPropertyPanelObject(PropertyPanelObject item)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmObject.Columns[0], item.Name),
				new TreeModelRowColumn(tmObject.Columns[1], item.ObjectClass?.Name)
			});
			row.SetExtraData<PropertyPanelObject>("obj", item);
			_rowsByObject[item] = row;

			tmObject.Rows.Add(row);
		}
		private Dictionary<PropertyPanelObject, TreeModelRow> _rowsByObject = new Dictionary<PropertyPanelObject, TreeModelRow>();
		internal void RemovePropertyPanelObject(PropertyPanelObject item)
		{
			if (!_rowsByObject.ContainsKey(item)) return;
			tmObject.Rows.Remove(_rowsByObject[item]);
		}
		internal void RefreshList()
		{
			tmObject.Rows.Clear();
			for (int i = 0; i < Objects.Count; i++)
			{
				AddPropertyPanelObject(Objects[i]);
			}
		}

		private PropertyPanelObject _SelectedObject = null;
		public PropertyPanelObject SelectedObject
		{
			get { return _SelectedObject; }
			set
			{
				_SelectedObject = value;

				tmPropertyGrid.Rows.Clear();
				if (_SelectedObject != null)
				{
					if (SelectedObject.ObjectClass != null)
					{
						for (int i = 0; i < SelectedObject.ObjectClass.Properties.Count; i++)
						{
							AddPropertyPanelProperty(SelectedObject.ObjectClass.Properties[i]);
						}
					}
					for (int i = 0; i < SelectedObject.Properties.Count; i++)
					{
						AddPropertyPanelProperty(SelectedObject.Properties[i]);
					}
				}
			}
		}

		private void AddPropertyPanelProperty(PropertyPanelProperty property, TreeModelRow parentRow = null)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmPropertyGrid.Columns[0], property.Name),
				new TreeModelRowColumn(tmPropertyGrid.Columns[1], property.Value == null ? String.Empty : property.Value.ToString())
			});

			for (int i = 0; i < property.Properties.Count; i++)
			{
				AddPropertyPanelProperty(property.Properties[i], row);
			}

			if (parentRow != null)
			{
				parentRow.Rows.Add(row);
			}
			else
			{
				tmPropertyGrid.Rows.Add(row);
			}
		}

		void cboObject_Changed(object sender, EventArgs e)
		{
			SelectedObject = cboObject.SelectedItem?.GetExtraData<PropertyPanelObject>("obj");
		}

		private void lvPropertyGrid_SelectionChanged(object sender, EventArgs e)
		{

		}

	}
}
