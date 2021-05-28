//
//  PropertyListEditor.cs - cross-platform (UWT) property list editor for Universal Editor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using System.Linq;
using MBS.Framework;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Editors.PropertyList
{
	[ContainerLayout("~/Editors/PropertyList/PropertyListEditor.glade")]
	public class PropertyListEditor : Editor
	{
		private ListViewControl tv = null;
		private DefaultTreeModel tm = null;

		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.ID = new Guid("{452b75d6-6818-4cb8-a18d-f94485cb0b29}");
				_er.SupportedObjectModels.Add(typeof(PropertyListObjectModel));
			}
			return _er;
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}
		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			Context.AttachCommandEventHandler("PropertyListContextMenu_New_Property", PropertyListContextMenu_New_Property);
			Context.AttachCommandEventHandler("PropertyListContextMenu_New_Group", PropertyListContextMenu_New_Group);

			OnObjectModelChanged(EventArgs.Empty);
		}

		[EventHandler(nameof(tv), nameof(Control.BeforeContextMenu))]
		private void tv_BeforeContextMenu(object sender, EventArgs e)
		{
			tv.ContextMenuCommandID = "PropertyListContextMenu";
		}

		[EventHandler(nameof(tv), nameof(ListViewControl.CellEdited))]
		private void tv_CellEdited(object sender, CellEditedEventArgs e)
		{
			Group group = e.Row.GetExtraData<Group>("group");
			Property property = e.Row.GetExtraData<Property>("property");

			if (e.Column == tv.Model.Columns[0])
			{
				// changing the Name
				if (group != null)
				{
					group.Name = e.NewValue?.ToString();
				}
				else if (property != null)
				{
					property.Name = e.NewValue?.ToString();
				}
			}
			else if (e.Column == tv.Model.Columns[1])
			{
				// changing the Value
				if (group != null)
				{

				}
				else if (property != null)
				{
					property.Value = e.NewValue;
				}
			}
		}

		private int GetNextIndex<T>(IPropertyListContainer parent = null) where T : PropertyListItem
		{
			if (parent == null)
				parent = (ObjectModel as PropertyListObjectModel);

			int lastIndex = 0;
			for (int i = 0; i < parent.Items.Count; i++)
			{
				if (parent.Items[i] is T)
				{
					string newName = String.Format("New {0} ", typeof(T).Name);
					if (parent.Items[i].Name.StartsWith(newName, StringComparison.CurrentCulture))
					{
						if (Int32.TryParse(parent.Items[i].Name.Substring(newName.Length), out int thisIndex))
						{
							if (thisIndex > lastIndex)
								lastIndex = thisIndex;
						}
					}
				}
			}
			return lastIndex + 1;
		}

		private void PropertyListContextMenu_New_Property(object sender, EventArgs e)
		{
			Property p = new Property();

			TreeModelRow rowParent = null;
			IPropertyListContainer parent = ObjectModel as PropertyListObjectModel;
			if (tv.SelectedRows.Count == 1)
			{
				rowParent = tv.SelectedRows[0];
				parent = rowParent.GetExtraData<Group>("group");
			}

			p.Name = String.Format("New Property {0}", GetNextIndex<Property>());
			parent.Items.Add(p);
			RecursiveAddProperty(p, rowParent);
		}
		private void PropertyListContextMenu_New_Group(object sender, EventArgs e)
		{
			Group p = new Group();

			TreeModelRow rowParent = null;
			IPropertyListContainer parent = ObjectModel as PropertyListObjectModel;
			if (tv.SelectedRows.Count == 1)
			{
				rowParent = tv.SelectedRows[0];
				parent = rowParent.GetExtraData<Group>("group");
			}

			p.Name = String.Format("New Group {0}", GetNextIndex<Group>());
			parent.Items.Add(p);
			RecursiveAddGroup(p, rowParent);
		}

		[EventHandler(nameof(tv), nameof(ListViewControl.SelectionChanged))]
		private void tv_SelectionChanged(object sender, EventArgs e)
		{
			if (tv.SelectedRows.Count > 0)
			{
				Application.Instance.Commands["EditCut"].Enabled = true;
				Application.Instance.Commands["EditCopy"].Enabled = true;
				Application.Instance.Commands["EditDelete"].Enabled = true;

				if (tv.SelectedRows[0].GetExtraData<Group>("group") != null)
				{
					tv.Columns[1].Renderers[0].Editable = false;
				}
				else
				{
					tv.Columns[1].Renderers[0].Editable = true;
				}
			}
			else
			{
				Application.Instance.Commands["EditCut"].Enabled = false;
				Application.Instance.Commands["EditCopy"].Enabled = false;
				Application.Instance.Commands["EditDelete"].Enabled = false;
			}
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			if (!IsCreated) return;

			PropertyListObjectModel plom = (ObjectModel as PropertyListObjectModel);
			for (int i = 0; i < plom.Items.Count; i++)
			{
				if (plom.Items[i] is Property)
				{
					RecursiveAddProperty(plom.Items[i] as Property, null);
				}
				else if (plom.Items[i] is Group)
				{
					RecursiveAddGroup(plom.Items[i] as Group, null);
				}
			}
		}

		private void RecursiveAddProperty(Property p, TreeModelRow parent = null)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], p.Name),
				new TreeModelRowColumn(tm.Columns[1], p.Value),
				new TreeModelRowColumn(tm.Columns[2], Image.FromStock(StockType.File, 16))
			});

			if (parent == null)
			{
				tm.Rows.Add(row);
			}
			else
			{
				parent.Rows.Add(row);
			}
			row.SetExtraData<Property>("property", p);
		}
		private void RecursiveAddGroup(Group g, TreeModelRow parent = null)
		{
			TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tm.Columns[0], g.Name),
				new TreeModelRowColumn(tm.Columns[2], Image.FromStock(StockType.Folder, 16))
			});

			if (parent == null)
			{
				tm.Rows.Add(row);
			}
			else
			{
				parent.Rows.Add(row);
			}

			for (int i = 0; i < g.Items.Count; i++)
			{
				if (g.Items[i] is Property)
				{
					RecursiveAddProperty(g.Items[i] as Property, row);
				}
				else if (g.Items[i] is Group)
				{
					RecursiveAddGroup(g.Items[i] as Group, row);
				}
			}
			row.SetExtraData<Group>("group", g);
		}
	}
}
