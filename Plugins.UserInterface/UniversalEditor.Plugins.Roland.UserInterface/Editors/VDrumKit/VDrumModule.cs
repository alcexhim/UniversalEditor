//
//  VDrumModule.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.Plugins.Roland.UserInterface.Editors.VDrumKit
{
	public class VDrumModule
	{
		public class VDrumModuleCollection
			: System.Collections.ObjectModel.Collection<VDrumModule>
		{
			private System.Collections.Generic.Dictionary<string, VDrumModule> _byID = new System.Collections.Generic.Dictionary<string, VDrumModule>();
			public VDrumModule this[string id]
			{
				get
				{
					if (_byID.ContainsKey(id))
						return _byID[id];
					return null;
				}
			}
			public bool Contains(string id)
			{
				return _byID.ContainsKey(id);
			}
			protected override void ClearItems()
			{
				base.ClearItems();
				_byID.Clear();
			}
			protected override void InsertItem(int index, VDrumModule item)
			{
				base.InsertItem(index, item);
				_byID[item.ID] = item;
			}
			protected override void RemoveItem(int index)
			{
				_byID.Remove(this[index].ID);
				base.RemoveItem(index);
			}
		}

		public string ID { get; set; }
		public string Title { get; set; }
		public VDrumPad.VDrumPadCollection Pads { get; } = new VDrumPad.VDrumPadCollection();
		public VDrumSound.VDrumSoundCollection Sounds { get; } = new VDrumSound.VDrumSoundCollection();
	}
}
