//
//  HomeworldDataFormat.cs - provides a DataFormat for manipulating archives in Homeworld format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Homeworld
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Homeworld format.
	/// </summary>
	public class HomeworldDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(Version), "Format _version:", (uint)0, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("VCE0", "Version \"VCE0\"", (uint)0),
					new ChoiceSetting.ChoiceSettingValue("WXD1", "Version \"WXD1\"", (uint)1)
				}));
			}
			return _dfr;
		}

		private uint mvarVersion = 0;
		public uint Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string header = br.ReadFixedLengthString(4);
			if (!(header == "VCE0" || header == "WXD1")) throw new InvalidDataFormatException("File does not begin with 'VCE0' or 'WXD1'");

			uint unknown1 = br.ReadUInt32();

			while (!br.EndOfStream)
			{
				string tag = br.ReadFixedLengthString(4);
				switch (tag)
				{
					case "INFO":
					{
						uint unknown2 = br.ReadUInt32();
						break;
					}
					case "DATA":
					{
						uint fileLength = br.ReadUInt32();
						byte[] fileData = br.ReadBytes(fileLength);
						fsom.Files.Add((fsom.Files.Count + 1).ToString().PadLeft(8, '0'), fileData);
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			if (mvarVersion >= 1)
			{
				bw.WriteFixedLengthString("WXD1");
			}
			else
			{
				bw.WriteFixedLengthString("VCE0");
			}

			uint unknown1 = 0;
			bw.WriteUInt32(unknown1);

			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString("DATA");

				byte[] data = file.GetData();
				bw.WriteUInt32((uint)data.Length);
				bw.WriteBytes(data);
			}
		}
	}
}
