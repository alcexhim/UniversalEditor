//
//  BINDataFormat.cs
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
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.FreeHeroes2Configuration
{
	public class BINDataFormat : DataFormat
	{
		// fs << static_cast<u16>( CURRENT_FORMAT_VERSION ) << opt_game << opt_world << opt_battle << opt_addons << pos_radr << pos_bttn << pos_icon << pos_stat;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
				throw new ObjectModelNotSupportedException();

			Accessor.Reader.Endianness = IO.Endianness.BigEndian;
			ushort formatVersion = Accessor.Reader.ReadUInt16();
			if (formatVersion != 3269)
			{
				Console.Error.WriteLine("unknown format version {0}, we might crash!", formatVersion);
			}

			uint usGame = Accessor.Reader.ReadUInt32();
			FH2GameFlags game = (FH2GameFlags)(usGame | 0x10000000);
			FH2WorldFlags world = (FH2WorldFlags)(Accessor.Reader.ReadUInt32() | 0x20000000);
			FH2BattleFlags battle = (FH2BattleFlags)(Accessor.Reader.ReadUInt32() | 0x40000000);
			FH2AddonFlags addon = (FH2AddonFlags)(Accessor.Reader.ReadUInt32() | 0x30000000);

			ushort radarX = Accessor.Reader.ReadUInt16();
			ushort radarY = Accessor.Reader.ReadUInt16();
			ushort buttonX = Accessor.Reader.ReadUInt16();
			ushort buttonY = Accessor.Reader.ReadUInt16();
			ushort iconX = Accessor.Reader.ReadUInt16();
			ushort iconY = Accessor.Reader.ReadUInt16();
			ushort statusX = Accessor.Reader.ReadUInt16();
			ushort statusY = Accessor.Reader.ReadUInt16();

			Group grpGame = new Group("game");

			AddProperty(grpGame, "also confirm autosave", game, FH2GameFlags.GAME_ALSO_CONFIRM_AUTOSAVE);
			AddProperty(grpGame, "autosave at beginning of day", game, FH2GameFlags.GAME_AUTOSAVE_BEGIN_DAY);
			AddProperty(grpGame, "autosave", game, FH2GameFlags.GAME_AUTOSAVE_ON);

			Group grpGameBattle = new Group("battle");
			AddProperty(grpGameBattle, "show damage", game, FH2GameFlags.GAME_BATTLE_SHOW_DAMAGE);
			AddProperty(grpGameBattle, "show grid", game, FH2GameFlags.GAME_BATTLE_SHOW_GRID);
			AddProperty(grpGameBattle, "show mouse shadow", game, FH2GameFlags.GAME_BATTLE_SHOW_MOUSE_SHADOW);
			AddProperty(grpGameBattle, "show move shadow", game, FH2GameFlags.GAME_BATTLE_SHOW_MOVE_SHADOW);
			grpGame.Items.Add(grpGameBattle);

			Group grpCastle = new Group("castle");
			AddProperty(grpCastle, "flash building", game, FH2GameFlags.GAME_CASTLE_FLASH_BUILDING);
			grpGame.Items.Add(grpCastle);

			AddProperty(grpGame, "continue after victory", game, FH2GameFlags.GAME_CONTINUE_AFTER_VICTORY);
			AddProperty(grpGame, "dynamic interface", game, FH2GameFlags.GAME_DYNAMIC_INTERFACE);
			AddProperty(grpGame, "evil interface", game, FH2GameFlags.GAME_EVIL_INTERFACE);
			AddProperty(grpGame, "hide interface", game, FH2GameFlags.GAME_HIDE_INTERFACE);
			AddProperty(grpGame, "remember last focus", game, FH2GameFlags.GAME_REMEMBER_LAST_FOCUS);
			AddProperty(grpGame, "confirm save rewrite", game, FH2GameFlags.GAME_SAVE_REWRITE_CONFIRM);
			AddProperty(grpGame, "show sdl logo", game, FH2GameFlags.GAME_SHOW_SDL_LOGO);
			AddProperty(grpGame, "show system info", game, FH2GameFlags.GAME_SHOW_SYSTEM_INFO);
			AddProperty(grpGame, "use fade", game, FH2GameFlags.GAME_USE_FADE);

			Group grpPocketPC = new Group("pocketpc");
			AddProperty(grpPocketPC, "drag drop scroll", game, FH2GameFlags.POCKETPC_DRAG_DROP_SCROLL);
			AddProperty(grpPocketPC, "hide cursor", game, FH2GameFlags.POCKETPC_HIDE_CURSOR);
			AddProperty(grpPocketPC, "low memory", game, FH2GameFlags.POCKETPC_LOW_MEMORY);
			AddProperty(grpPocketPC, "low resolution", game, FH2GameFlags.POCKETPC_LOW_RESOLUTION);
			AddProperty(grpPocketPC, "tap mode", game, FH2GameFlags.POCKETPC_TAP_MODE);
			grpGame.Items.Add(grpPocketPC);

			plom.Items.Add(grpGame);

			Group grpBattle = new Group("battle");
			AddProperty(grpBattle, "archmage resists bad spells", battle, FH2BattleFlags.BATTLE_ARCHMAGE_RESIST_BAD_SPELL);
			AddProperty(grpBattle, "magic troop resist", battle, FH2BattleFlags.BATTLE_MAGIC_TROOP_RESIST);
			AddProperty(grpBattle, "merge armies", battle, FH2BattleFlags.BATTLE_MERGE_ARMIES);
			AddProperty(grpBattle, "archer object penalty", battle, FH2BattleFlags.BATTLE_OBJECTS_ARCHERS_PENALTY);
			AddProperty(grpBattle, "reverse wait order", battle, FH2BattleFlags.BATTLE_REVERSE_WAIT_ORDER);
			AddProperty(grpBattle, "skip increases defense", battle, FH2BattleFlags.BATTLE_SKIP_INCREASE_DEFENSE);
			AddProperty(grpBattle, "soft wait troops", battle, FH2BattleFlags.BATTLE_SOFT_WAITING);
			plom.Items.Add(grpBattle);

			Group grpWorld = new Group("world");
			Group grpWorldCastle = new Group("castle");
			AddProperty(grpWorldCastle, "allow recruit from well", world, FH2WorldFlags.CASTLE_ALLOW_BUY_FROM_WELL);
			AddProperty(grpWorldCastle, "allow guardians", world, FH2WorldFlags.CASTLE_ALLOW_GUARDIANS);
			grpWorld.Items.Add(grpWorldCastle);

			Group grpWorldHeroes = new Group("heroes");
			AddProperty(grpWorldHeroes, "allow banned secondary skills upgrade", world, FH2WorldFlags.HEROES_ALLOW_BANNED_SECSKILLS);
			AddProperty(grpWorldHeroes, "auto move to target cell after battle", world, FH2WorldFlags.HEROES_AUTO_MOVE_BATTLE_DST);
			AddProperty(grpWorldHeroes, "allow buy spell book from shrine", world, FH2WorldFlags.HEROES_BUY_BOOK_FROM_SHRINES);
			AddProperty(grpWorldHeroes, "recruit cost dependent on level", world, FH2WorldFlags.HEROES_COST_DEPENDED_FROM_LEVEL);
			AddProperty(grpWorldHeroes, "learn new spells with day", world, FH2WorldFlags.HEROES_LEARN_SPELLS_WITH_DAY);
			grpWorld.Items.Add(grpWorldHeroes);

			plom.Items.Add(grpWorld);

			Group grpMetrics = new Group("metrics");
			AddMetric(grpMetrics, "radar", radarX, radarY);
			AddMetric(grpMetrics, "button", buttonX, buttonY);
			AddMetric(grpMetrics, "icon", iconX, iconY);
			AddMetric(grpMetrics, "status", statusX, statusY);
			plom.Items.Add(grpMetrics);
		}

		private void AddMetric(Group group, string name, ushort x, ushort y)
		{
			Group grp = new Group(name);
			grp.Items.AddProperty("x", x);
			grp.Items.AddProperty("y", y);
			group.Items.Add(grp);
		}

		private void AddProperty(Group group, string name, FH2GameFlags flagSrc, FH2GameFlags flagChk)
		{
			AddProperty(group, name, (uint)flagSrc, (uint)flagChk);
		}
		private void AddProperty(Group group, string name, FH2BattleFlags flagSrc, FH2BattleFlags flagChk)
		{
			AddProperty(group, name, (uint)flagSrc, (uint)flagChk);
		}
		private void AddProperty(Group group, string name, FH2AddonFlags flagSrc, FH2AddonFlags flagChk)
		{
			AddProperty(group, name, (uint)flagSrc, (uint)flagChk);
		}
		private void AddProperty(Group group, string name, FH2WorldFlags flagSrc, FH2WorldFlags flagChk)
		{
			AddProperty(group, name, (uint)flagSrc, (uint)flagChk);
		}
		private void AddProperty(Group group, string name, uint flagSrc, uint flagChk)
		{
			Property prop = new Property(name);
			prop.Value = ((flagSrc & flagChk) == flagChk) ? "on" : "off";
			group.Items.Add(prop);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
				throw new ObjectModelNotSupportedException();

		}
	}
}
