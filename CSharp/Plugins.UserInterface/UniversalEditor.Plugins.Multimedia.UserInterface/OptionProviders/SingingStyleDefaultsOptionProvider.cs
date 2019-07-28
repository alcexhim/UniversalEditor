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

namespace UniversalEditor.Plugins.Multimedia.UserInterface.OptionProviders
{
	public class SingingStyleDefaultsOptionProvider : OptionProvider
	{
		public SingingStyleDefaultsOptionProvider ()
		{
			OptionGroups.Add ("Editors:Synthesized Audio:External Programs", new Option[]
			{
				new BooleanOption("Use external _wavefile manipulation tool"),
				new TextOption("Path"),
				new BooleanOption("Use external _resampling tool"),
				new TextOption("Path"),
				new TextOption("Voicebank _directory")
			});
			OptionGroups.Add ("Editors:Synthesized Audio:MIDI Settings", new Option[]
			{
				new GroupOption("MIDI Devices", new Option[]
				{
					new ChoiceOption<int>("_Input device"),
					new ChoiceOption<int>("_Output device")
				}),
				new BooleanOption("Play notes through MIDI output upon _selection"),
				new BooleanOption("Send _Program Change for the following program before continuing"),
				new ChoiceOption<int>(String.Empty)
			});
			OptionGroups.Add ("Editors:Synthesized Audio:Phoneme Dictionary", new Option[]
			{
			});
			OptionGroups.Add ("Editors:Synthesized Audio:Synthesizers", new Option[]
			{
			});
			OptionGroups.Add("Editors:Synthesized Audio:Singing Style Defaults", new Option[]
			{
				// new CommandOption("_Load settings from VOCALOID2"),
				// 2q22wnew CommandOption("_Save settings from VOCALOID2"),
				new ChoiceOption<string>("_Template", null, new ChoiceOption<string>.ChoiceOptionValue[]
				{
					new ChoiceOption<string>.ChoiceOptionValue("(Custom)", "custom"),
					new ChoiceOption<string>.ChoiceOptionValue("Normal", "normal"),
					new ChoiceOption<string>.ChoiceOptionValue("Accent", "accent"),
					new ChoiceOption<string>.ChoiceOptionValue("Strong Accent", "strongaccent"),
					new ChoiceOption<string>.ChoiceOptionValue("Legato", "legato"),
					new ChoiceOption<string>.ChoiceOptionValue("Slow Legato", "slowlegato"),
				}),
				new GroupOption("Pitch control", new Option[]
				{
					new RangeOption("_Bend depth", 8, 0, 100),
					new RangeOption("Bend _length", 0, 0, 100),
					new BooleanOption("Add portamento in _rising movement"),
					new BooleanOption("Add portamento in _falling movement")
				}),
				new GroupOption("Dynamics control", new Option[]
				{
					new RangeOption("_Decay", 50, 0, 100),
					new RangeOption("_Accent", 50, 0, 100)
				})
			});
		}
	}
}

