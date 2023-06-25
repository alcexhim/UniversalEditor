//
//  LGPDataFormat.cs - provides a DataFormat for manipulating archives in SquareSoft LGP format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.SquareSoft
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in SquareSoft LGP format.
	/// </summary>
	public class LGPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(Creator), "_Creator", String.Empty, 10));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(Description), "_Description", String.Empty, 14));
				_dfr.Title = "SquareSoft LGP archive";
			}
			return _dfr;
		}

		public string Creator { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = Accessor.Reader;
			short twonulls = br.ReadInt16();
			Creator = br.ReadFixedLengthString(10);
			int u1 = br.ReadInt32();
			for (int i = 0; i < u1; i++)
			{
				string fileName = br.ReadFixedLengthString(20).TrimNull();
				short u = br.ReadInt16();
				byte b = br.ReadByte();
				byte[] ww = br.ReadBytes((uint)4);
				fsom.Files.Add(fileName, new byte[0]);
			}

			br.Seek((uint)0xE12, SeekOrigin.Current);

			for (int i = 0; i < u1; i++)
			{
				string fileName = br.ReadFixedLengthString(20).TrimNull();
				int dataLength = br.ReadInt32();

				fsom.Files[i].Source = new EmbeddedFileSource(br, base.Accessor.Position, dataLength);
				br.Seek(dataLength, SeekOrigin.Current);
			}
			Description = br.ReadFixedLengthString(14).TrimNull();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = Accessor.Writer;
			bw.WriteInt16(0);
			bw.WriteFixedLengthString(Creator, 10);

			File[] files = fsom.GetAllFiles();
			bw.WriteInt32(files.Length);

			for (int i = 0; i < files.Length; i++)
			{
				bw.WriteFixedLengthString(files[i].Name, 20);
				bw.WriteInt16(0);  // u
				bw.WriteByte(0); // b
				bw.WriteInt32(0); // ww
			}

			bw.WriteBytes(new byte[0xE12]); // skip

			for (int i = 0; i < files.Length; i++)
			{
				bw.WriteFixedLengthString(files[i].Name, 20);
				byte[] data = files[i].GetData();
				bw.WriteInt32(data.Length);

				bw.WriteBytes(data);
			}
			bw.WriteFixedLengthString(Description, 14);
		}
	}
}
