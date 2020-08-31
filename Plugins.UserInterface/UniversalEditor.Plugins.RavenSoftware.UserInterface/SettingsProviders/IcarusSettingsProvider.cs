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

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.SettingsProviders
{
	/// <summary>
	/// Provides a <see cref="IcarusSettingsProvider" /> for managing Icarus settings.
	/// </summary>
	public class IcarusSettingsProvider : ApplicationSettingsProvider
	{
		public IcarusSettingsProvider()
		{
			SettingsGroups.Add("Editors:Raven Software:ICARUS Scripting:General", new Setting[]
			{
				new BooleanSetting("Re-open last file at startup"), // UE Platform Setting
				new BooleanSetting("_Alphabetically-sort edit pulldowns"),
				new BooleanSetting("Enable _SourceSafe functions"),
				new BooleanSetting("_Warn before opening BehavEd-incompatible ICARUS script"),
			});
			SettingsGroups.Add("Editors:Raven Software:ICARUS Scripting:Directories", new Setting[]
			{
				new TextSetting("Script path"),
				new TextSetting("SourceSafe script path"),
				new TextSetting("SourceSafe configuration location"),
				new TextSetting("Location of ICARUS compiler (IBIZE)"),
				new TextSetting("Command description file (BehavEd.bhc)"),
				new TextSetting("Source files path"),
			});
		}
	}
}

