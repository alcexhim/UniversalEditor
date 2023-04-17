//
//  VDrumSound.cs
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
	public class VDrumSound
	{
		public class VDrumSoundCollection
			: System.Collections.ObjectModel.Collection<VDrumSound>
		{
			private VDrumSound[] _byIndex = new VDrumSound[0];

			protected override void InsertItem(int index, VDrumSound item)
			{
				base.InsertItem(index, item);

				if (item.Index > _byIndex.Length)
				{
					Array.Resize<VDrumSound>(ref _byIndex, item.Index + 1);
				}
				_byIndex[item.Index] = item;
			}

			public new VDrumSound this[int index]
			{
				get
				{
					return _byIndex[index];
				}
			}

		}

		public int Index { get; set; }
		public string Title { get; set; }
	}
}
