//
//  SingingStyleDefaultsOptionProvider.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalWidgetToolkit;

namespace UniversalEditor.Plugins.Multimedia.UserInterface.SettingsProviders
{
	public class SingingStyleDefaultsSettingsProvider : ApplicationSettingsProvider
	{
		public SingingStyleDefaultsSettingsProvider ()
		{
			SettingsGroups.Add ("Editors:Synthesized Audio:External Programs", new Setting[]
			{
				new BooleanSetting("Use external _wavefile manipulation tool"),
				new TextSetting("Path"),
				new BooleanSetting("Use external _resampling tool"),
				new TextSetting("Path"),
				new TextSetting("Voicebank _directory")
			});
			SettingsGroups.Add ("Editors:Synthesized Audio:MIDI Settings", new Setting[]
			{
				new GroupSetting("MIDI Devices", new Setting[]
				{
					new ChoiceSetting("_Input device"),
					new ChoiceSetting("_Output device")
				}),
				new BooleanSetting("Play notes through MIDI output upon _selection"),
				new BooleanSetting("Send _Program Change for the following program before continuing"),
				new ChoiceSetting(String.Empty)
			});
			SettingsGroups.Add ("Editors:Synthesized Audio:Phoneme Dictionary", new Setting[]
			{
			});
			SettingsGroups.Add ("Editors:Synthesized Audio:Synthesizers", new Setting[]
			{
			});
			SettingsGroups.Add("Editors:Synthesized Audio:Singing Style Defaults", new Setting[]
			{
				// new CommandOption("_Load settings from VOCALOID2"),
				// 2q22wnew CommandOption("_Save settings from VOCALOID2"),
				new ChoiceSetting("_Template", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("(Custom)", "custom"),
					new ChoiceSetting.ChoiceSettingValue("Normal", "normal"),
					new ChoiceSetting.ChoiceSettingValue("Accent", "accent"),
					new ChoiceSetting.ChoiceSettingValue("Strong Accent", "strongaccent"),
					new ChoiceSetting.ChoiceSettingValue("Legato", "legato"),
					new ChoiceSetting.ChoiceSettingValue("Slow Legato", "slowlegato"),
				}),
				new GroupSetting("Pitch control", new Setting[]
				{
					new RangeSetting("_Bend depth", 8, 0, 100),
					new RangeSetting("Bend _length", 0, 0, 100),
					new BooleanSetting("Add portamento in _rising movement"),
					new BooleanSetting("Add portamento in _falling movement")
				}),
				new GroupSetting("Dynamics control", new Setting[]
				{
					new RangeSetting("_Decay", 50, 0, 100),
					new RangeSetting("_Accent", 50, 0, 100)
				})
			});
		}
	}
}

