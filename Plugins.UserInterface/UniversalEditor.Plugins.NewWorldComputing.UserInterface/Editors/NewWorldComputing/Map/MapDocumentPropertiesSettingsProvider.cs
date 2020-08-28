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
using MBS.Framework.UserInterface;
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
				new TextSetting("Map name"),
				new TextSetting("Map description"),
				new ChoiceSetting("Difficulty", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("Easy", MapDifficulty.Easy),
					new ChoiceSetting.ChoiceSettingValue("Normal", MapDifficulty.Normal),
					new ChoiceSetting.ChoiceSettingValue("Hard", MapDifficulty.Hard),
					new ChoiceSetting.ChoiceSettingValue("Expert", MapDifficulty.Expert)
				}),
				new BooleanSetting("Start with hero in each player's castle", true),
				new ChoiceSetting("Special victory condition", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("None", 0),
					new ChoiceSetting.ChoiceSettingValue("Capture a particular castle", 1),
					new ChoiceSetting.ChoiceSettingValue("Defeat a particular hero", 2),
					new ChoiceSetting.ChoiceSettingValue("Find a particular artifact", 3),
					new ChoiceSetting.ChoiceSettingValue("One side defeats another", 4),
					new ChoiceSetting.ChoiceSettingValue("Accumulate gold", 5)
				}),
				new ChoiceSetting("Special loss condition", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("None", 0),
					new ChoiceSetting.ChoiceSettingValue("Lose a particular castle", 1),
					new ChoiceSetting.ChoiceSettingValue("Lose a particular hero", 2),
					new ChoiceSetting.ChoiceSettingValue("Run out of time", 3)
				})
			}));
			SettingsGroups.Add(new SettingsGroup("Rumors and Events", new Setting[]
			{
				new CollectionSetting("Rumors", new SettingsGroup("Rumors Settings Group", new Setting[]
				{
					new TextSetting("Rumor Detail")
				})),
				new CollectionSetting("Events", new SettingsGroup("Event Settings Group", new Setting[]
				{
					new TextSetting("Message Text"),
					new GroupSetting("Resources to give or take", new Setting[]
					{
						new RangeSetting("Wood", 0, -9999, 99999),
						new RangeSetting("Mercury", 0, -9999, 99999),
						new RangeSetting("Ore", 0, -9999, 99999),
						new RangeSetting("Sulfur", 0, -9999, 99999),
						new RangeSetting("Crystal", 0, -9999, 99999),
						new RangeSetting("Gems", 0, -9999, 99999),
						new RangeSetting("Gold", 0, -9999, 99999)
					}),
					new RangeSetting("Day of first occurrence"),
					new ChoiceSetting("Subsequent occurrences", null, new ChoiceSetting.ChoiceSettingValue[]
					{
						new ChoiceSetting.ChoiceSettingValue("Never", 0),
						new ChoiceSetting.ChoiceSettingValue("Every Day", 1),
						new ChoiceSetting.ChoiceSettingValue("Every 2 Days", 2),
						new ChoiceSetting.ChoiceSettingValue("Every 3 Days", 3),
						new ChoiceSetting.ChoiceSettingValue("Every 4 Days", 4),
						new ChoiceSetting.ChoiceSettingValue("Every 5 Days", 5),
						new ChoiceSetting.ChoiceSettingValue("Every 6 Days", 6),
						new ChoiceSetting.ChoiceSettingValue("Every 7 Days", 7),
						new ChoiceSetting.ChoiceSettingValue("Every 14 Days", 14),
						new ChoiceSetting.ChoiceSettingValue("Every 21 Days", 21),
						new ChoiceSetting.ChoiceSettingValue("Every 28 Days", 28)
					}),
					new BooleanSetting("Allow computer to get event")
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
