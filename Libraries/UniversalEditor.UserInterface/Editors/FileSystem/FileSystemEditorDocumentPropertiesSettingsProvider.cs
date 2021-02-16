//
//  FileSystemEditorDocumentPropertiesSettingsProvider.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker
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
using MBS.Framework;
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Editors.FileSystem
{
	public class FileSystemEditorDocumentPropertiesSettingsProvider : SettingsProvider
	{
		private FileSystemObjectModel _FileSystemObjectModel = null;
		public FileSystemObjectModel ObjectModel
		{
			get { return _FileSystemObjectModel; }
			set
			{
				_FileSystemObjectModel = value;
				LoadSettings();
			}
		}

		private IFileSystemObject _FileSystemObject = null;
		public IFileSystemObject FileSystemObject
		{
			get { return _FileSystemObject; }
			set
			{
				_FileSystemObject = value;
				LoadSettings();
			}
		}

		public FileSystemEditorDocumentPropertiesSettingsProvider()
		{
			SettingsGroups.Add(new SettingsGroup("General", new Setting[]
			{
				new TextSetting("FileName", "_Name"),
				new TextSetting("FileSize", "_Size"),
				new TextSetting("FileType", "_Type"),
				new TextSetting("FileDateModified", "_Date modified")
			}));
		}

		protected override void LoadSettingsInternal()
		{
			base.LoadSettingsInternal();

			if (FileSystemObject != null)
			{
				if (FileSystemObject is File)
				{
					File f = (File)FileSystemObject;
					SettingsGroups[0].Settings[0].SetValue(f.Name);
					SettingsGroups[0].Settings[1].SetValue(f.Size);
					// SettingsGroups[0].Settings[2].SetValue(f);
					SettingsGroups[0].Settings[3].SetValue(f.ModificationTimestamp);
				}
				return;
			}

			if (ObjectModel != null)
			{
				if (ObjectModel.Accessor != null)
					SettingsGroups[0].Settings[0].SetValue(ObjectModel.Accessor.GetFileName());

				// SettingsGroups[0].Settings[1].SetValue(f.Size);
				// SettingsGroups[0].Settings[2].SetValue(f);
				// SettingsGroups[0].Settings[3].SetValue(f.ModificationTimestamp);
			}
		}
	}
}
