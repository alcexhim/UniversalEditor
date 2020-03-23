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
using MBS.Framework.UserInterface;

namespace UniversalEditor.Editors.FileSystem
{
	public class FileSystemEditorSettingsProvider : ApplicationSettingsProvider
	{
		public FileSystemEditorSettingsProvider ()
		{
			SettingsGroups.Add (new SettingsGroup ("Editors:File System:Views", new Setting[]
			{
				new BooleanSetting ("Sort _folders before files"),
				new BooleanSetting ("Allow folders to be _expanded")
			}));
			SettingsGroups.Add (new SettingsGroup ("Editors:File System:Behavior", new Setting[]
			{
				new BooleanSetting ("_Single-click to open items"),
				new BooleanSetting ("Show action to create symbolic _links"),
				new ChoiceSetting ("Default option for _executable text files", null, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue ("Display them in text editor", 1),
					new ChoiceSetting.ChoiceSettingValue ("Run them as executable", 2),
					new ChoiceSetting.ChoiceSettingValue ("Ask me what to do", 3)
				}),
				new BooleanSetting ("Ask before _emptying the Trash/Recycle Bin"),
				new BooleanSetting ("Show action to _permanently delete files and folders")
			}));
			SettingsGroups.Add (new SettingsGroup ("Editors:File System:List Columns", new Setting[]
			{
			}));
			SettingsGroups.Add (new SettingsGroup ("Editors:File System:Search and Preview", new Setting[]
			{
			}));
		}
	}
}

