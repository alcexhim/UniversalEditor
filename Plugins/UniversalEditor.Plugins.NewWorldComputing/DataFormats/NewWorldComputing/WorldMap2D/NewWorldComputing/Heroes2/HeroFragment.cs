//
//  HeroFragment.cs
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
using UniversalEditor.ObjectModels.NewWorldComputing;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.WorldMap2D.NewWorldComputing.Heroes2
{
	public class HeroFragment : DataFormatFragment<MapHero>
	{
		protected override MapHero ReadInternal(Reader reader)
		{
			MapHero hero = new MapHero();
			bool unk = reader.ReadBoolean();
			hero.HasCustomTroops = reader.ReadBoolean();

			byte[] monsters = reader.ReadBytes(5);
			ushort[] monsterCount = reader.ReadUInt16Array(5);
			for (int i = 0; i < monsters.Length; i++)
			{
				if (monsters[i] != 0xFF)
				{
					MapMonsterType monsterType = (MapMonsterType)monsters[i];
					hero.Monsters.Add(new MapArmyMonster(monsterType, monsterCount[i]));
				}
			}

			hero.HasCustomPortrait = reader.ReadBoolean();
			hero.CustomPortraitIndex = reader.ReadByte();

			byte[] customArtifacts = reader.ReadBytes(3); // -1 if unset
			for (int i = 0; i < customArtifacts.Length; i++)
			{
				if (customArtifacts[i] != 0xFF)
				{
					MapArtifact artifact = new MapArtifact((MapArtifactType)customArtifacts[i]);
					hero.Artifacts.Add(artifact);
				}
			}

			byte unknown2 = reader.ReadByte();

			hero.Experience = reader.ReadUInt32();

			hero.HasCustomSkills = reader.ReadBoolean();
			byte[] skills = reader.ReadBytes(8);                // 0xff none, pathfinding, archery, logistic, scouting, diplomacy, navigation, leadership, wisdom,
															// mysticism, luck, ballistics, eagle, necromance, estate
			byte[] skillLevels = reader.ReadBytes(8);           // 1 = basic, 2 = advanced, 3 = expert, 0 = unset
			for (int i = 0; i < skills.Length; i++)
			{
				if (skills[i] != 0xFF)
				{
					HeroSkillType type = (HeroSkillType)skills[i];
					HeroSkillLevel level = (HeroSkillLevel)skillLevels[i];
					hero.Skills.Add(new HeroSkill(type, level));
				}
			}

			byte unknown3 = reader.ReadByte();
			hero.HasCustomName = reader.ReadBoolean();
			hero.Name = reader.ReadFixedLengthString(13).TrimNull();
			hero.Patrol = reader.ReadBoolean();
			hero.PatrolSquareCount = reader.ReadByte();

			byte[] unknown4 = reader.ReadBytes(15);
			return hero;
		}

		protected override void WriteInternal(Writer writer, MapHero value)
		{
			writer.WriteBoolean(false);
			writer.WriteBoolean(value.HasCustomTroops);

			for (int i = 0; i < 5; i++)
			{
				if (i < value.Monsters.Count)
				{
					writer.WriteByte((byte)value.Monsters[i].MonsterType);
				}
				else
				{
					writer.WriteByte(255);
				}
			}
			for (int i = 0; i < 5; i++)
			{
				if (i < value.Monsters.Count)
				{
					writer.WriteUInt16((byte)value.Monsters[i].Amount);
				}
				else
				{
					writer.WriteUInt16(0);
				}
			}

			writer.WriteBoolean(value.HasCustomPortrait);
			writer.WriteByte(value.CustomPortraitIndex);

			for (int i = 0; i < 3; i++)
			{
				if (i < value.Artifacts.Count)
				{
					writer.WriteByte((byte)value.Artifacts[i].Type);
				}
				else
				{
					writer.WriteByte(255);
				}
			}

			writer.WriteByte(0);
			writer.WriteUInt32(value.Experience);

			writer.WriteBoolean(value.HasCustomSkills);
			for (int i = 0; i < 8; i++)
			{
				if (i < value.Skills.Count)
				{
					writer.WriteByte((byte)value.Skills[i].Type);
				}
				else
				{
					writer.WriteByte(255);
				}
			}
			for (int i = 0; i < 8; i++)
			{
				if (i < value.Skills.Count)
				{
					writer.WriteByte((byte)value.Skills[i].Level);
				}
				else
				{
					writer.WriteByte(0);
				}
			}

			writer.WriteByte(0); // unknown3
			writer.WriteBoolean(value.HasCustomName);
			writer.WriteFixedLengthString(value.Name, 13);
			writer.WriteBoolean(value.Patrol);
			writer.WriteByte(value.PatrolSquareCount);

			writer.WriteBytes(new byte[15]);
		}
	}
}
