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
