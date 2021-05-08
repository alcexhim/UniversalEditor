//
//  TileFragment.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.WorldMap2D.NewWorldComputing.Heroes2
{
	public class TileFragment : DataFormatFragment<MapTile>
	{
		protected override MapTile ReadInternal(Reader reader)
		{
			MapTile tile = new MapTile();
			tile.GroundType = (MapGroundType)reader.ReadUInt16();       // tile (ocean, grass, snow, swamp, lava, desert, dirt, wasteland, beach)
			tile.ObjectName1 = reader.ReadByte();       // level 1.0
			tile.IndexName1 = reader.ReadByte();        // index level 1.0 or 0xFF
			tile.Quantity1 = reader.ReadByte();         // count
			tile.Quantity2 = reader.ReadByte();         // count
			tile.ObjectName2 = reader.ReadByte();       // level 2.0
			tile.IndexName2 = reader.ReadByte();        // index level 2.0 or 0xFF
			tile.Shape = reader.ReadByte();             // shape reflect % 4, 0 none, 1 vertical, 2 horizontal, 3 any
			tile.GeneralObject = reader.ReadByte();     // zero or object
			tile.IndexAddon = reader.ReadUInt16();    // zero or index addons_t
			tile.UniqNumber1 = reader.ReadUInt32();     // level 1.0
			tile.UniqNumber2 = reader.ReadUInt32();     // level 2.0
			return tile;
		}
		protected override void WriteInternal(Writer writer, MapTile value)
		{
			writer.WriteUInt16((ushort)value.GroundType);
			writer.WriteByte(value.ObjectName1);
			writer.WriteByte(value.IndexName1);
			writer.WriteByte(value.Quantity1);
			writer.WriteByte(value.Quantity2);
			writer.WriteByte(value.ObjectName2);
			writer.WriteByte(value.IndexName2);
			writer.WriteByte(value.Shape);
			writer.WriteByte(value.GeneralObject);
			writer.WriteUInt16(value.IndexAddon);
			writer.WriteUInt32(value.UniqNumber1);
			writer.WriteUInt32(value.UniqNumber2);
		}
	}
}
