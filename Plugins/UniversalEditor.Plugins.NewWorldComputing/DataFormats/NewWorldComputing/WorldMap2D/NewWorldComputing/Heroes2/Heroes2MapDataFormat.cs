//
//  Heroes2MapDataFormat.cs - provides a DataFormat for manipulating Heroes of Might and Magic II map files
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

using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.WorldMap2D.NewWorldComputing.Heroes2
{
	/// <summary>
	/// Pprovides a <see cref="DataFormat" /> for manipulating Heroes of Might and Magic II map files.
	/// </summary>
	public class Heroes2MapDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MapObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MapObjectModel map = (objectModel as MapObjectModel);
			if (map == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			uint magic = br.ReadUInt32();
			if (magic != 0x0000005C) throw new InvalidDataFormatException();

			map.Difficulty = (MapDifficulty)br.ReadUInt16();
			map.Width = br.ReadByte();
			map.Height = br.ReadByte();

			#region Allow kingdom colors
			{
				bool colorBlue = br.ReadBoolean();
				if (colorBlue) map.AllowedKingdomColors |= MapKingdomColor.Blue;

				bool colorGreen = br.ReadBoolean();
				if (colorGreen) map.AllowedKingdomColors |= MapKingdomColor.Green;

				bool colorRed = br.ReadBoolean();
				if (colorRed) map.AllowedKingdomColors |= MapKingdomColor.Red;

				bool colorYellow = br.ReadBoolean();
				if (colorYellow) map.AllowedKingdomColors |= MapKingdomColor.Yellow;

				bool colorOrange = br.ReadBoolean();
				if (colorOrange) map.AllowedKingdomColors |= MapKingdomColor.Orange;

				bool colorPurple = br.ReadBoolean();
				if (colorPurple) map.AllowedKingdomColors |= MapKingdomColor.Purple;
			}
			#endregion
			#region Allow player colors
			{
				bool colorBlue = br.ReadBoolean();
				if (colorBlue) map.AllowedHumanPlayerColors |= MapKingdomColor.Blue;

				bool colorGreen = br.ReadBoolean();
				if (colorGreen) map.AllowedHumanPlayerColors |= MapKingdomColor.Green;

				bool colorRed = br.ReadBoolean();
				if (colorRed) map.AllowedHumanPlayerColors |= MapKingdomColor.Red;

				bool colorYellow = br.ReadBoolean();
				if (colorYellow) map.AllowedHumanPlayerColors |= MapKingdomColor.Yellow;

				bool colorOrange = br.ReadBoolean();
				if (colorOrange) map.AllowedHumanPlayerColors |= MapKingdomColor.Orange;

				bool colorPurple = br.ReadBoolean();
				if (colorPurple) map.AllowedHumanPlayerColors |= MapKingdomColor.Purple;
			}
			#endregion
			#region Allow AI colors
			{
				bool colorBlue = br.ReadBoolean();
				if (colorBlue) map.AllowedComputerPlayerColors |= MapKingdomColor.Blue;

				bool colorGreen = br.ReadBoolean();
				if (colorGreen) map.AllowedComputerPlayerColors |= MapKingdomColor.Green;

				bool colorRed = br.ReadBoolean();
				if (colorRed) map.AllowedComputerPlayerColors |= MapKingdomColor.Red;

				bool colorYellow = br.ReadBoolean();
				if (colorYellow) map.AllowedComputerPlayerColors |= MapKingdomColor.Yellow;

				bool colorOrange = br.ReadBoolean();
				if (colorOrange) map.AllowedComputerPlayerColors |= MapKingdomColor.Orange;

				bool colorPurple = br.ReadBoolean();
				if (colorPurple) map.AllowedComputerPlayerColors |= MapKingdomColor.Purple;
			}
			#endregion

			// kingdom count
			byte kingdomCount = br.ReadByte();
			byte nCount1 = br.ReadByte(); // idk?
			byte nCount2 = br.ReadByte(); // idk?

			map.WinConditions = (MapWinCondition)br.ReadByte();
			map.ComputerAlsoWins = (MapWinCondition)br.ReadByte();
			map.AllowNormalVictory = br.ReadBoolean();

			ushort wins3 = br.ReadUInt16();

			map.LoseConditions = (MapLoseCondition)br.ReadByte();
			ushort u2 = br.ReadUInt16();

			// starting hero
			byte startingHero = br.ReadByte();
			bool withHeroes = (startingHero == 0);

			byte[] races = br.ReadBytes(6);

			ushort wins2 = br.ReadUInt16();
			ushort loss2 = br.ReadUInt16();

			// map name
			br.Accessor.Seek(0x3A, IO.SeekOrigin.Begin);
			map.Name = br.ReadFixedLengthString(16).TrimNull();

			// map description
			br.Accessor.Seek(0x76, IO.SeekOrigin.Begin);
			map.Description = br.ReadFixedLengthString(143).TrimNull();

			byte[] unknown = br.ReadBytes(157);

			ushort nObjects = br.ReadUInt16();

			// 33044 bytes between here and there - tiles (width * height * 20 bytes per tile)
			uint width = br.ReadUInt32();
			uint height = br.ReadUInt32();

			TileFragment fragTile = new TileFragment();

			for (int y = 0; y < map.Height; y++)
			{
				for (int x = 0; x < map.Width; x++)
				{
					MapTile tile = fragTile.Read(br);
					map.Tiles.Add(tile);
				}
			}

			// addons
			uint nAddons = br.ReadUInt32();
			for (uint i = 0; i < nAddons; i++)
			{
				ushort indexAddon = br.ReadUInt16();
				byte objectNameN1 = br.ReadByte();
				objectNameN1 *= 2; // idk
				byte indexNameN1 = br.ReadByte();
				byte quantityN = br.ReadByte();
				byte objectNameN2 = br.ReadByte();
				byte indexNameN2 = br.ReadByte();
				uint uniqNumberN1 = br.ReadUInt32();
				uint uniqNumberN2 = br.ReadUInt32();
			}

			for (int i = 0; i < 72; i++)
			{
				// castle coordinates
				// 72 x 3 byte (cx, cy, id)
				byte cx = br.ReadByte();
				byte cy = br.ReadByte();
				byte id = br.ReadByte();

				if (cx == 0xFF && cy == 0xFF)
					continue;

				bool isCastle = false;
				if ((id & 0x80) == 0x80)
				{
					isCastle = true;
					id = (byte)(id & ~0x80);
				}

				MapCastleType castleType = (MapCastleType)id;
				// map.Castles.Add(new MapCastle(castleType, cx, cy, isCastle));
			}

			// kingdom resource coordinates
			for (int i = 0; i < 144; i++)
			{
				byte cx = br.ReadByte();
				byte cy = br.ReadByte();
				byte id = br.ReadByte();

				if (cx == 0xFF && cy == 0xFF)
					continue;

				MapResourceType resourceType = (MapResourceType)id;

			}

			map.ObeliskCount = br.ReadByte();
			return;

			// idk wtf this is
			uint countBlock = 0;
			while (!br.EndOfStream)
			{
				byte l = br.ReadByte();
				byte h = br.ReadByte();
				if (l == 0 && h == 0)
					break;
				countBlock = (uint)(256 * (h + l) - 1); // wtf
			}

			MonsterFragment fragMonster = new MonsterFragment();
			CastleFragment fragCastle = new CastleFragment();
			HeroFragment fragHero = new HeroFragment();
			EventFragment fragEvent = new EventFragment();

			for (ushort i = 0; i < countBlock; i++)
			{
				ushort blockSize = br.ReadUInt16();

				MapTile tile = FindObject(map, i);
				switch ((MapItemType)blockSize)
				{
					case MapItemType.Monster:
					{
						MapArmyMonster item = fragMonster.Read(br);
						map.Items.Add(item);
						break;
					}
					case MapItemType.Castle:
					{
						MapCastle castle = fragCastle.Read(br);
						map.Items.Add(castle);
						break;
					}
					case MapItemType.Hero:
					{
						MapHero hero = fragHero.Read(br);
						map.Items.Add(hero);
						break;
					}
				}
			}
		}

		private MapTile FindObject(MapObjectModel map, ushort i)
		{
			int findobject = -1;
			for (int it_index = 0; it_index < map.Tiles.Count && findobject < 0; ++it_index)
			{
				MapTile tile = map.Tiles[it_index];

				// orders(quantity2, quantity1)
				int orders = tile.Quantity2;  // (tile.GetQuantity2() ? tile.GetQuantity2() : 0);
				orders <<= 8;
				orders |= tile.Quantity1; // tile.GetQuantity1();

				if ((orders != 0) && !((orders % 0x08) != 0) && (i + 1 == orders / 0x08))
					findobject = it_index;
			}
			if (findobject == -1)
				return null;

			return map.Tiles[findobject];
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
