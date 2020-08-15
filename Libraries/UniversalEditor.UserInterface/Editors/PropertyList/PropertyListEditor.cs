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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.ListView;

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
				_er.SupportedObjectModels.Add(typeof(PropertyListObjectModel));
			}
			return _er;
		}

		protected override EditorSelection CreateSelectionInternal(object content)
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
			OnObjectModelChanged(EventArgs.Empty);
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
				new TreeModelRowColumn(tm.Columns[1], p.Value)
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
				new TreeModelRowColumn(tm.Columns[0], g.Name)
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
