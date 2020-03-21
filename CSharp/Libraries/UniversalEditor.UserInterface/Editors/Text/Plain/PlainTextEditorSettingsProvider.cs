//
//  PlainTextEditorOptionProvider.cs
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
using MBS.Framework.UserInterface;

namespace UniversalEditor.Editors.Text.Plain
{
	public class PlainTextEditorSettingsProvider : ApplicationSettingsProvider
	{
		public PlainTextEditorSettingsProvider()
		{
			SettingsGroups.Add(new SettingsGroup("Editors:Plain Text Editor", new Setting[]
			{
				new BooleanSetting("_Display line numbers"),
				new BooleanSetting("_Display right margin"),
				new TextSetting("Right margin at _column"),
				new BooleanSetting("Display _overview map"),

				new BooleanSetting("Enable text _wrapping"),
				new BooleanSetting("Do not s_plit words over two lines"),

				new BooleanSetting("Highlight current _line"),
				new BooleanSetting("Highlight matching _brackets"),
				
				new RangeSetting("_Tab width", 8, 1, 24),
				new BooleanSetting("Insert _spaces instead of tabs"),
				new BooleanSetting("_Enable automatic indentation", true)
			}));
		}
	}
}

