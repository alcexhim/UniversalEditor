//
//  TIFilesDataFormat.cs - provides a DataFormat for manipulating disk images in TIFILES format
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

using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TexasInstruments.TIFiles
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating disk images in TIFILES format.
	/// </summary>
	public class TIFilesDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			byte isMultiTransfer = reader.ReadByte();
			string signature = reader.ReadFixedLengthString(7);
			if (signature != "TIFILES")
				throw new InvalidDataFormatException("file does not contain 'TIFILES' signature");


			if (isMultiTransfer == 0x07)
			{
			}
			else if (isMultiTransfer == 0x08)
			{
				throw new InvalidDataFormatException("MXT YModem multiple file transfer not supported");
			}
			else
			{
				throw new InvalidDataFormatException("first byte of file unrecognized (0x07 = normal, 0x08 = MXT YModem multiple file transfer)");
			}

			ushort sectorCount = reader.ReadUInt16();
			TIFilesFlags flags = (TIFilesFlags)reader.ReadByte();
			byte recordsPerSector = reader.ReadByte();
			byte eofOffset = reader.ReadByte();
			byte recordLength = reader.ReadByte();
			ushort level3RecordCount = reader.ReadUInt16();
			string fileName = reader.ReadFixedLengthString(10);
			fileName = fileName.TrimNull();
			if (String.IsNullOrEmpty(fileName))
			{
				fileName = System.IO.Path.GetFileNameWithoutExtension(Accessor.GetFileName()).ToUpper();
			}

			byte mxt = reader.ReadByte();
			byte reserved1 = reader.ReadByte();
			ushort extendedHeader = reader.ReadUInt16();
			uint creationTimestamp = reader.ReadUInt32();
			uint modificationTimestamp = reader.ReadUInt32();

			File f = fsom.AddFile(fileName);
			f.DataRequest += F_DataRequest;
			f.Properties.Add("offset", reader.Accessor.Position);
			f.Properties.Add("reader", reader);
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader reader = (Reader)f.Properties["reader"];
			long offset = (long)f.Properties["offset"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadToEnd();
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

		}
	}
}
