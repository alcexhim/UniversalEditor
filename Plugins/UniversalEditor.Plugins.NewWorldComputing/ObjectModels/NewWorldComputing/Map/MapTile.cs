//
//  MapTile.cs - describes a tile placed on a map
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
	/// <summary>
	/// Describes a tile placed on a map.
	/// </summary>
	public class MapTile
	{
		public class MapTileCollection
			: System.Collections.ObjectModel.Collection<MapTile>
		{
		}

		/// <summary>
		/// Indicates the type of ground for a ground tile placed on a map.
		/// </summary>
		/// <value>The type of the ground.</value>
		public MapGroundType GroundType { get; set; } = MapGroundType.Unknown;
		public byte ObjectName1 { get; set; }
		public byte IndexName1 { get; set; }
		public byte Quantity1 { get; set; }
		public byte Quantity2 { get; set; }
		public byte ObjectName2 { get; set; }
		public byte IndexName2 { get; set; }
		public byte Shape { get; set; }
		public byte GeneralObject { get; set; }
		public ushort IndexAddon { get; set; }
		public uint UniqNumber1 { get; set; }
		public uint UniqNumber2 { get; set; }
	}
}
