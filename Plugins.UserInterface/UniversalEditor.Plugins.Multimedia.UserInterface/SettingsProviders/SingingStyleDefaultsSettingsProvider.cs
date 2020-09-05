//
//  SingingStyleDefaultsOptionProvider.cs - provides a SettingsProvider for managing singing style defaults for SynthesizedAudioEditor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.Plugins.Multimedia.UserInterface.SettingsProviders
{
	/// <summary>
	/// Provides a <see cref="SettingsProvider" /> for managing singing style defaults for SynthesizedAudioEditor.
	/// </summary>
	public class SingingStyleDefaultsSettingsProvider : ApplicationSettingsProvider
	{
		public SingingStyleDefaultsSettingsProvider()
		{
			SettingsGroups.Add("Editors:Synthesized Audio:External Programs", new Setting[]
			{
				new BooleanSetting("ExternalWavefileManipulationToolEnabled", "Use external _wavefile manipulation tool"),
				new TextSetting("ExternalWavefileManipulationToolFileName", "Path"),
				new BooleanSetting("ExternalResamplingToolEnabled", "Use external _resampling tool"),
				new TextSetting("ExternalResamplingToolFileName", "Path"),
				new TextSetting("VoicebankDirectory", "Voicebank _directory")
			});
			SettingsGroups.Add("Editors:Synthesized Audio:MIDI Settings", new Setting[]
			{
				new GroupSetting("MIDIDevices", "MIDI Devices", new Setting[]
				{
					new ChoiceSetting("MIDIInputDeviceName", "_Input device"),
					new ChoiceSetting("MIDIOutputDeviceName", "_Output device")
				}),
				new BooleanSetting("MIDIPlayOnSelection", "Play notes through MIDI output upon _selection"),
				new BooleanSetting("MIDIProgramChangeEnabled", "Send _Program Change for the following program before continuing"),
				new ChoiceSetting("MIDIProgramChangeProgram", String.Empty)
			});
			SettingsGroups.Add("Editors:Synthesized Audio:Phoneme Dictionary", new Setting[]
			{
			});
			SettingsGroups.Add("Editors:Synthesized Audio:Synthesizers", new Setting[]
			{
			});
			SettingsGroups.Add("Editors:Synthesized Audio:Singing Style Defaults", new Setting[]
			{
				// new CommandOption("_Load settings from VOCALOID2"),
				// 2q22wnew CommandOption("_Save settings from VOCALOID2"),
				new ChoiceSetting("Template", "_Template", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("custom", "(Custom)", "custom"),
					new ChoiceSetting.ChoiceSettingValue("normal", "Normal", "normal"),
					new ChoiceSetting.ChoiceSettingValue("accent", "Accent", "accent"),
					new ChoiceSetting.ChoiceSettingValue("strongaccent", "Strong Accent", "strongaccent"),
					new ChoiceSetting.ChoiceSettingValue("legato", "Legato", "legato"),
					new ChoiceSetting.ChoiceSettingValue("slowlegato", "Slow Legato", "slowlegato"),
				}),
				new GroupSetting("PitchControl", "Pitch control", new Setting[]
				{
					new RangeSetting("BendDepth", "_Bend depth", 8, 0, 100),
					new RangeSetting("BendLength", "Bend _length", 0, 0, 100),
					new BooleanSetting("AddPortamentoRising", "Add portamento in _rising movement"),
					new BooleanSetting("AddPortamentoFalling", "Add portamento in _falling movement")
				}),
				new GroupSetting("DynamicsControl", "Dynamics control", new Setting[]
				{
					new RangeSetting("Decay", "_Decay", 50, 0, 100),
					new RangeSetting("Accent", "_Accent", 50, 0, 100)
				})
			});
		}
	}
}

