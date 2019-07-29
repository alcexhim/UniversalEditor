//
//  MyClass.cs
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

namespace UniversalEditor.OptionProviders
{
	public class SyntaxEditorOptionProvider : OptionProvider
	{
		public SyntaxEditorOptionProvider ()
		{
			OptionGroups.Add(new OptionGroup("Editors:Syntax", new Option[]
			{
				new BooleanOption("D_rag and drop text editing"),
				new BooleanOption("A_utomatic delimiter highlighting"),
				new BooleanOption("_Track changes"),
				new BooleanOption("Auto-_detect UTF-8 encoding without signature"),
				new BooleanOption("Display _selection margin"),
				new BooleanOption("Display indicator _margin"),
				new BooleanOption("Highlight _current line")
			}));

			OptionGroups.Add(new OptionGroup("Editors:Syntax:Languages", new Option[]
			{
				new BooleanOption("_Map extensionless files to"),
				new ChoiceOption<string>("Default language"),
			}));

			// for each language...
			System.Collections.Generic.Dictionary<string, Option[]> languageOptions = new System.Collections.Generic.Dictionary<string, Option[]>();
			languageOptions.Add ("Basic:General", new Option[] {
				new BooleanOption("Auto list _members"),
				new BooleanOption("_Hide advanced members"),
				new BooleanOption("_Parameter information"),
				new BooleanOption("Enable _virtual space"),
				new BooleanOption("_Word wrap"),
				new BooleanOption("_Show visual glyphs for word wrap"),

			});
			foreach (System.Collections.Generic.KeyValuePair<string, Option[]> kvp in languageOptions) {
				OptionGroups.Add(new OptionGroup("Editors:Syntax:Languages:" + kvp.Key, kvp.Value));
			}
		}
	}
}

