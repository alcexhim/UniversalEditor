//
//  VDrumKitObjectModel.cs
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
namespace UniversalEditor.Plugins.Roland.ObjectModels.VDrumKit
{
	public class VDrumKitObjectModel : ObjectModel
	{
		public DrumKit.DrumKitCollection DrumKits { get; } = new DrumKit.DrumKitCollection();
		public DrumKitChain.DrumKitChainCollection Chains { get; } = new DrumKitChain.DrumKitChainCollection();

		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}