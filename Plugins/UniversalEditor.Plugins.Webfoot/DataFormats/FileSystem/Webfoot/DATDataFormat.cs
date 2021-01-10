//
//  DATDataFormat.cs - provides a DataFormat for manipulating Webfoot DAT archives
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Webfoot.DataFormats.FileSystem.Webfoot
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Webfoot DAT archives.
	/// </summary>
	public class DATDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionNumber("Key", "Encryption _key", 0xAA, 0x00, 0xFF));
			}
			return _dfr;
		}

		public byte Key { get; set; } = 0xAA;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			fsom.AdditionalDetails.Add("Webfoot.FileKey", "File key");

			Reader reader = Accessor.Reader;
			reader.Seek(-13, SeekOrigin.End);

			reader.Transformations.Clear();
			reader.Transformations.Add(xort);

			xort.XorKey = null;

			// not sure what this field is, crc or something else? but we can keep it in sync in case we open a DAT file and re-save it
			uint unknown = reader.ReadUInt32();
			fsom.SetCustomProperty(MakeReference(), "Unknown", unknown);

			uint directoryOffset = reader.ReadUInt32();
			uint fileCount = reader.ReadUInt32();
			Key = reader.ReadByte();
			xort.XorKey = new byte[] { Key };

			reader.Seek(directoryOffset, SeekOrigin.Begin);
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName1 = reader.ReadNullTerminatedString();
				string fileName2 = reader.ReadNullTerminatedString();

				uint offset = reader.ReadUInt32();
				uint length = reader.ReadUInt32();

				// not sure what this field is, crc or something else? but we can keep it in sync in case we open a DAT file and re-save it
				// using the epoch 1990-01-01 it comes up as September 1999, which kind of makes sense...
				// ... but is the same in 3DFROG.DAT and MISSILE.DAT, one would expect them to be diferent...
				uint maybeTimestamp = reader.ReadUInt32();

				DateTime dt = new DateTime(1990, 01, 01);
				dt = dt.AddSeconds(maybeTimestamp);


				byte fileKey = reader.ReadByte();

				File f = fsom.AddFile(fileName1);
				f.ModificationTimestamp = dt;
				f.Properties["maybeTimestamp"] = maybeTimestamp;
				f.SetAdditionalDetail("Webfoot.FileKey", fileKey.ToString("X").PadRight(2, '0'));
				f.Properties["reader"] = reader;
				f.Properties["xort"] = xort;
				f.Properties["offset"] = offset;
				f.Properties["length"] = length;
				f.Properties["key"] = fileKey;
				f.Properties["index"] = i;
				f.DataRequest += F_DataRequest;
				f.Size = length;
			}
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (File)sender;
			Reader reader = (Reader)f.Properties["reader"];
			IO.Transformations.XorTransformation xort = (IO.Transformations.XorTransformation)f.Properties["xort"];

			uint offset = (uint)f.Properties["offset"];
			uint length = (uint)f.Properties["length"];
			byte fileKey = (byte)f.Properties["key"];

			reader.Seek(offset, SeekOrigin.Begin);
			xort.XorKey = new byte[] { fileKey };
			byte[] data = reader.ReadBytes(length);
			e.Data = data;
		}

		private IO.Transformations.XorTransformation xort = new IO.Transformations.XorTransformation();
		private Checksum.Modules.CRC32.CRC32ChecksumModule crc = new Checksum.Modules.CRC32.CRC32ChecksumModule();

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;

			writer.Transformations.Clear();
			writer.Transformations.Add(xort);

			File[] files = fsom.GetAllFiles("\\");
			Array.Sort<File>(files, (x, y) => x.GetProperty<uint>("index").CompareTo(y.GetProperty<uint>("index")));

			for (uint i = 0; i < files.Length; i++)
			{
				byte key = Byte.Parse(files[i].GetAdditionalDetail("Webfoot.FileKey", "00") as string, System.Globalization.NumberStyles.HexNumber);
				xort.XorKey = new byte[] { key };

				byte[] filedata = files[i].GetData();
				writer.WriteBytes(filedata);
			}

			uint directoryOffset = (uint)writer.Accessor.Position;

			MemoryAccessor maDirectory = new MemoryAccessor();
			maDirectory.Writer.Transformations.Add(xort);

			uint offset = 0;
			for (int i = 0; i < files.Length; i++)
			{
				byte key = Byte.Parse(files[i].GetAdditionalDetail("Webfoot.FileKey", "00") as string, System.Globalization.NumberStyles.HexNumber);
				uint length = (uint)files[i].GetData().Length;

				// must be reset after each call to GetData because the DataRequest twiddles it
				xort.XorKey = new byte[] { Key };

				maDirectory.Writer.WriteNullTerminatedString(files[i].Name);
				maDirectory.Writer.WriteNullTerminatedString(files[i].Name);
				maDirectory.Writer.WriteUInt32(offset);
				maDirectory.Writer.WriteUInt32(length);

				// not sure what this field is, crc or something else? but we can keep it in sync in case we open a DAT file and re-save it
				// using the epoch 1990-01-01 it comes up as September 1999, which kind of makes sense...
				// ... but is the same in 3DFROG.DAT and MISSILE.DAT, one would expect them to be diferent...
				maDirectory.Writer.WriteUInt32(files[i].GetProperty<uint>("maybeTimestamp"));

				maDirectory.Writer.WriteByte(key);
				offset += length;
			}
			maDirectory.Flush();
			xort.XorKey = null;

			byte[] directory = maDirectory.ToArray();
			writer.WriteBytes(directory);

			// not sure what this field is, crc or something else? but we can keep it in sync in case we open a DAT file and re-save it
			uint unknown = fsom.GetCustomProperty<uint>(MakeReference(), "Unknown", 0);
			writer.WriteUInt32(unknown);

			writer.WriteUInt32(directoryOffset);
			writer.WriteUInt32((uint)files.Length);
			writer.WriteByte(Key);
		}
	}
}
