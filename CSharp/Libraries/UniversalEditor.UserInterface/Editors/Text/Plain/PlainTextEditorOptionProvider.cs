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
using UniversalWidgetToolkit;

namespace UniversalEditor.Editors.Text.Plain
{
	public class PlainTextEditorOptionProvider : OptionProvider
	{
		public PlainTextEditorOptionProvider()
		{
			OptionGroups.Add(new OptionGroup("Editors:Plain Text Editor", new Option[]
			{
				new BooleanOption("_Display line numbers"),
				new BooleanOption("_Display right margin"),
				new TextOption("Right margin at _column"),
				new BooleanOption("Display _overview map"),

				new BooleanOption("Enable text _wrapping"),
				new BooleanOption("Do not _split words over two lines"),

				new BooleanOption("Highlight current _line"),
				new BooleanOption("Highlight matching _brackets"),
				
				new RangeOption("_Tab width", 8, 1, 24),
				new BooleanOption("Insert _spaces instead of tabs"),
				new BooleanOption("_Enable automatic indentation", true)
			}));
		}
	}
}

