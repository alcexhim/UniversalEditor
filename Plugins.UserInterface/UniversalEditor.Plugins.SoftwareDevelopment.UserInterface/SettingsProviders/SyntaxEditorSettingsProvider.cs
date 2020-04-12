//
//  SyntaxEditorSettingsProvider.cs - provides a SettingsProvider containing settings for a SyntaxEditor
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

using MBS.Framework.UserInterface;

namespace UniversalEditor.SettingsProviders
{
	/// <summary>
	/// Provides a <see cref="SettingsProvider" /> containing settings for a <see cref="Plugins.SoftwareDevelopment.UserInterface.Editors.Syntax.SyntaxEditor" />.
	/// </summary>
	public class SyntaxEditorSettingsProvider : ApplicationSettingsProvider
	{
		public SyntaxEditorSettingsProvider()
		{
			SettingsGroups.Add(new SettingsGroup("Editors:Syntax", new Setting[]
			{
				new BooleanSetting("D_rag and drop text editing"),
				new BooleanSetting("A_utomatic delimiter highlighting"),
				new BooleanSetting("_Track changes"),
				new BooleanSetting("Auto-_detect UTF-8 encoding without signature"),
				new BooleanSetting("Display _selection margin"),
				new BooleanSetting("Display indicator _margin"),
				new BooleanSetting("Highlight _current line")
			}));

			SettingsGroups.Add(new SettingsGroup("Editors:Syntax:Languages", new Setting[]
			{
				new BooleanSetting("_Map extensionless files to"),
				new ChoiceSetting("Default language"),
			}));

			// for each language...
			System.Collections.Generic.Dictionary<string, Setting[]> languageOptions = new System.Collections.Generic.Dictionary<string, Setting[]>();
			languageOptions.Add("Basic:General", new Setting[]
			{
				new BooleanSetting("Auto list _members"),
				new BooleanSetting("_Hide advanced members"),
				new BooleanSetting("_Parameter information"),
				new BooleanSetting("Enable _virtual space"),
				new BooleanSetting("_Word wrap"),
				new BooleanSetting("_Show visual glyphs for word wrap"),

			});

			foreach (System.Collections.Generic.KeyValuePair<string, Setting[]> kvp in languageOptions)
			{
				SettingsGroups.Add(new SettingsGroup("Editors:Syntax:Languages:" + kvp.Key, kvp.Value));
			}
		}
	}
}

