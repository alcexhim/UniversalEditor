//
//  CastleFragment.cs
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.WorldMap2D.NewWorldComputing.Heroes2
{
	public class CastleFragment : DataFormatFragment<MapCastle>
	{
		protected override MapCastle ReadInternal(Reader reader)
		{
			MapCastle castle = new MapCastle();
			castle.Color = (MapCastleColor)reader.ReadByte();
			castle.HasCustomBuilding = reader.ReadBoolean();
			castle.Buildings = (MapBuildingType)reader.ReadUInt16();
			castle.Dwellings = (MapDwellingType)reader.ReadUInt16();
			castle.MageGuildLevel = (MapMageGuildLevel)reader.ReadByte();
			castle.HasCustomTroops = reader.ReadBoolean();

			byte[] monsterTypes = reader.ReadBytes(5);
			ushort[] monsterCounts = reader.ReadUInt16Array(5);
			for (int i = 0; i < monsterTypes.Length; i++)
			{
				if (monsterTypes[i] != 0xFF)
				{
					castle.Monsters[i] = new MapArmyMonster((MapMonsterType)monsterTypes[i], monsterCounts[i]);
				}
			}

			castle.HasCaptain = reader.ReadBoolean();
			castle.HasCustomName = reader.ReadBoolean();
			castle.Name = reader.ReadFixedLengthString(13).TrimNull();
			castle.Type = (MapCastleType)reader.ReadByte();
			castle.IsCastle = reader.ReadBoolean();
			castle.IsUpgradable = reader.ReadBoolean(); // 00 TRUE, 01 FALSE

			byte[] unknown = reader.ReadBytes(29);

			return castle;
		}
		protected override void WriteInternal(Writer writer, MapCastle value)
		{
			writer.WriteByte((byte)value.Color);
			writer.WriteBoolean(value.HasCustomBuilding);
			writer.WriteUInt16((ushort)value.Buildings);
			writer.WriteUInt16((ushort)value.Dwellings);
			writer.WriteByte((byte)value.MageGuildLevel);
			writer.WriteBoolean(value.HasCustomTroops);

			for (int i = 0; i < 5; i++)
			{
				if (i < value.Monsters.Length)
				{
					writer.WriteByte((byte)value.Monsters[i].MonsterType);
				}
				else
				{
					writer.WriteByte(0);
				}
			}
			for (int i = 0; i < 5; i++)
			{
				if (i < value.Monsters.Length)
				{
					writer.WriteUInt16(value.Monsters[i].Amount);
				}
				else
				{
					writer.WriteUInt16(0);
				}
			}

			writer.WriteBoolean(value.HasCaptain);
			writer.WriteBoolean(value.HasCustomName);
			writer.WriteFixedLengthString(value.Name, 13);
			writer.WriteByte((byte)value.Type);
			writer.WriteBoolean(value.IsCastle);
			writer.WriteBoolean(value.IsUpgradable); // 00 TRUE, 01 FALSE

			writer.WriteBytes(new byte[29]);// idk
		}
	}
}
