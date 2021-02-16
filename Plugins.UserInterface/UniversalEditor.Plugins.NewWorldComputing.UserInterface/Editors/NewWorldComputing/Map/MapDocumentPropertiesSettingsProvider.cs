//
//  MapDocumentPropertiesSettingsProvider.cs
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
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.Plugins.NewWorldComputing.UserInterface.Editors.NewWorldComputing.Map
{
	public class MapDocumentPropertiesSettingsProvider : SettingsProvider
	{
		public MapEditor Editor { get; private set; } = null;

		public MapDocumentPropertiesSettingsProvider(MapEditor editor)
		{
			Editor = editor;
			SettingsGroups.Add(new SettingsGroup("General", new Setting[]
			{
				new TextSetting("Name", "Map name"),
				new TextSetting("Description", "Map description"),
				new ChoiceSetting("Difficulty", "Difficulty", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("easy", "Easy", MapDifficulty.Easy),
					new ChoiceSetting.ChoiceSettingValue("normal", "Normal", MapDifficulty.Normal),
					new ChoiceSetting.ChoiceSettingValue("hard", "Hard", MapDifficulty.Hard),
					new ChoiceSetting.ChoiceSettingValue("expert", "Expert", MapDifficulty.Expert)
				}),
				new BooleanSetting("StartWithHeroInEachCastle", "Start with hero in each player's castle", true),
				new ChoiceSetting("SpecialVictoryCondition", "Special victory condition", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("none", "None", 0),
					new ChoiceSetting.ChoiceSettingValue("capture-castle", "Capture a particular castle", 1),
					new ChoiceSetting.ChoiceSettingValue("defeat-hero", "Defeat a particular hero", 2),
					new ChoiceSetting.ChoiceSettingValue("find-artifact", "Find a particular artifact", 3),
					new ChoiceSetting.ChoiceSettingValue("defeat-other", "One side defeats another", 4),
					new ChoiceSetting.ChoiceSettingValue("accumulate-gold", "Accumulate gold", 5)
				}),
				new ChoiceSetting("SpecialLossCondition", "Special loss condition", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("none", "None", 0),
					new ChoiceSetting.ChoiceSettingValue("lose-castle", "Lose a particular castle", 1),
					new ChoiceSetting.ChoiceSettingValue("lose-hero", "Lose a particular hero", 2),
					new ChoiceSetting.ChoiceSettingValue("time", "Run out of time", 3)
				})
			}));
			SettingsGroups.Add(new SettingsGroup("Rumors and Events", new Setting[]
			{
				new CollectionSetting("Rumors", "Rumors", new SettingsGroup("Rumors Settings Group", new Setting[]
				{
					new TextSetting("Detail", "Rumor Detail")
				})),
				new CollectionSetting("Events", "Events", new SettingsGroup("Event Settings Group", new Setting[]
				{
					new TextSetting("Message", "Message Text"),
					new GroupSetting("Resources", "Resources to give or take", new Setting[]
					{
						new RangeSetting("Wood", "Wood", 0, -9999, 99999),
						new RangeSetting("Mercury", "Mercury", 0, -9999, 99999),
						new RangeSetting("Ore", "Ore", 0, -9999, 99999),
						new RangeSetting("Sulfur", "Sulfur", 0, -9999, 99999),
						new RangeSetting("Crystal", "Crystal", 0, -9999, 99999),
						new RangeSetting("Gems", "Gems", 0, -9999, 99999),
						new RangeSetting("Gold", "Gold", 0, -9999, 99999)
					}),
					new RangeSetting("FirstOccurrenceDay", "Day of first occurrence"),
					new ChoiceSetting("SubsequentOccurrences", "Subsequent occurrences", null, new ChoiceSetting.ChoiceSettingValue[]
					{
						new ChoiceSetting.ChoiceSettingValue("0", "Never", 0),
						new ChoiceSetting.ChoiceSettingValue("1", "Every Day", 1),
						new ChoiceSetting.ChoiceSettingValue("2", "Every 2 Days", 2),
						new ChoiceSetting.ChoiceSettingValue("3", "Every 3 Days", 3),
						new ChoiceSetting.ChoiceSettingValue("4", "Every 4 Days", 4),
						new ChoiceSetting.ChoiceSettingValue("5", "Every 5 Days", 5),
						new ChoiceSetting.ChoiceSettingValue("6", "Every 6 Days", 6),
						new ChoiceSetting.ChoiceSettingValue("7", "Every 7 Days", 7),
						new ChoiceSetting.ChoiceSettingValue("14", "Every 14 Days", 14),
						new ChoiceSetting.ChoiceSettingValue("21", "Every 21 Days", 21),
						new ChoiceSetting.ChoiceSettingValue("28", "Every 28 Days", 28)
					}),
					new BooleanSetting("AllowComputer", "Allow computer to get event")
				}))
			}));
		}

		protected override void LoadSettingsInternal()
		{
			MapObjectModel map = (Editor.ObjectModel as MapObjectModel);

			SettingsGroups[0].Settings[0].SetValue(map.Name);
			SettingsGroups[0].Settings[1].SetValue(map.Description);
			SettingsGroups[0].Settings[2].SetValue(map.Difficulty);
		}
		protected override void SaveSettingsInternal()
		{
			MapObjectModel map = (Editor.ObjectModel as MapObjectModel);

			map.Name = SettingsGroups[0].Settings[0].GetValue<string>();
			map.Description = SettingsGroups[0].Settings[1].GetValue<string>();
			map.Difficulty = SettingsGroups[0].Settings[2].GetValue<MapDifficulty>();
		}
	}
}
