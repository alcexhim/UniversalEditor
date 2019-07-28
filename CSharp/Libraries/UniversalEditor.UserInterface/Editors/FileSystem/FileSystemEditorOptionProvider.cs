//
//  FileSystemEditorOptionProvider.cs
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

namespace UniversalEditor.Editors.FileSystem
{
	public class FileSystemEditorOptionProvider : OptionProvider
	{
		public FileSystemEditorOptionProvider ()
		{
			OptionGroups.Add (new OptionGroup ("Editors:File System:Views", new Option[]
			{
				new BooleanOption ("Sort _folders before files"),
				new BooleanOption ("Allow folders to be _expanded")
			}));
			OptionGroups.Add (new OptionGroup ("Editors:File System:Behavior", new Option[]
			{
				new BooleanOption ("_Single-click to open items"),
				new BooleanOption ("Show action to create symbolic _links"),
				new ChoiceOption<int> ("Default option for _executable text files", null, new ChoiceOption<int>.ChoiceOptionValue[]
				{
					new ChoiceOption<int>.ChoiceOptionValue ("Display them in text editor", 1),
					new ChoiceOption<int>.ChoiceOptionValue ("Run them as executable", 2),
					new ChoiceOption<int>.ChoiceOptionValue ("Ask me what to do", 3)
				}),
				new BooleanOption ("Ask before _emptying the Trash/Recycle Bin"),
				new BooleanOption ("Show action to _permanently delete files and folders")
			}));
			OptionGroups.Add (new OptionGroup ("Editors:File System:List Columns", new Option[]
			{
			}));
			OptionGroups.Add (new OptionGroup ("Editors:File System:Search and Preview", new Option[]
			{
			}));
		}
	}
}

