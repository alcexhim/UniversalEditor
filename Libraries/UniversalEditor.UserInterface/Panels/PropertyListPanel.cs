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
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;

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
	public class PropertyPanelObject : ISupportsExtraData
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
				_parent?.ClearPropertyPanelObjects();
			}
			protected override void InsertItem(int index, PropertyPanelObject item)
			{
				base.InsertItem(index, item);
				_parent?.AddPropertyPanelObject(item);
			}
			protected override void RemoveItem(int index)
			{
				_parent?.RemovePropertyPanelObject(this[index]);
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

		#region ISupportsExtraData members
		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key)) return (T)_ExtraData[key];
			return defaultValue;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			return GetExtraData<object>(key, defaultValue);
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}
		public void SetExtraData(string key, object value)
		{
			SetExtraData<object>(key, value);
		}
		#endregion
	}

	[ContainerLayout("~/Panels/PropertyList/PropertyListPanel.glade")]
	partial class PropertyListPanel : Panel
	{
		internal ComboBox cboObject;
		private SplitContainer scDescriptionCommands;
		private ListViewControl lvPropertyGrid;

		public PropertyPanelObject.PropertyPanelObjectCollection Objects { get; private set; } = null;

		public static readonly Guid ID = new Guid("{c935e3af-e5c6-4b3e-a3a4-e16ba229a91d}");

		protected override void OnEditorChanged(EditorChangedEventArgs e)
		{
			base.OnEditorChanged(e);

			Objects.Clear();

			Editor editor = ((EditorWindow)ParentWindow).GetCurrentEditor();
			if (editor == null) return;

			foreach (PropertyPanelObject obj in editor.PropertiesPanel.Objects)
			{
				Objects.Add(obj);
			}

			cboObject.Visible = editor.PropertiesPanel.ShowObjectSelector;
		}

		public PropertyListPanel()
		{
			Objects = new PropertyPanelObject.PropertyPanelObjectCollection(this);
		}

		[EventHandler(nameof(cboObject), nameof(ComboBox.KeyDown))]
		void cboObject_KeyDown(object sender, MBS.Framework.UserInterface.Input.Keyboard.KeyEventArgs e)
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

		internal void ClearPropertyPanelObjects()
		{
			if (cboObject == null)
			{
				Console.Error.WriteLine("PropertyPanel: cboObject was not created correctly");
				return;
			}

			(cboObject.Model as DefaultTreeModel).Rows.Clear();
		}
		internal void AddPropertyPanelObject(PropertyPanelObject item)
		{
			if (cboObject == null)
			{
				Console.Error.WriteLine("PropertyPanel: cboObject was not created correctly");
				return;
			}

			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn((cboObject.Model as DefaultTreeModel).Columns[0], item.Name),
				new TreeModelRowColumn((cboObject.Model as DefaultTreeModel).Columns[1], item.ObjectClass?.Name)
			});
			row.SetExtraData<PropertyPanelObject>("obj", item);
			_rowsByObject[item] = row;

			(cboObject.Model as DefaultTreeModel).Rows.Add(row);
		}
		private Dictionary<PropertyPanelObject, TreeModelRow> _rowsByObject = new Dictionary<PropertyPanelObject, TreeModelRow>();
		internal void RemovePropertyPanelObject(PropertyPanelObject item)
		{
			if (cboObject == null)
			{
				Console.Error.WriteLine("PropertyPanel: cboObject was not created correctly");
				return;
			}

			if (!_rowsByObject.ContainsKey(item)) return;
			(cboObject.Model as DefaultTreeModel).Rows.Remove(_rowsByObject[item]);
		}
		internal void RefreshList()
		{
			if (cboObject == null)
			{
				Console.Error.WriteLine("PropertyPanel: cboObject was not created correctly");
				return;
			}

			(cboObject.Model as DefaultTreeModel).Rows.Clear();
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

				lvPropertyGrid.Model.Rows.Clear();
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
				new TreeModelRowColumn(lvPropertyGrid.Model.Columns[0], property.Name),
				new TreeModelRowColumn(lvPropertyGrid.Model.Columns[1], property.Value == null ? String.Empty : property.Value.ToString())
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
				lvPropertyGrid.Model.Rows.Add(row);
			}
		}

		[EventHandler(nameof(cboObject), nameof(ComboBox.Changed))]
		void cboObject_Changed(object sender, EventArgs e)
		{
			SelectedObject = cboObject.SelectedItem?.GetExtraData<PropertyPanelObject>("obj");
		}

		[EventHandler(nameof(lvPropertyGrid), nameof(ListViewControl.SelectionChanged))]
		private void lvPropertyGrid_SelectionChanged(object sender, EventArgs e)
		{

		}

	}
}
