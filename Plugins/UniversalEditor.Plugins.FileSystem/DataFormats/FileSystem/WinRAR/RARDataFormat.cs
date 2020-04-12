//
//  RARDataFormat.cs - provides a DataFormat for manipulating archives in RAR format
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

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in RAR format.
	/// </summary>
	/// <remarks>
	/// Due to the licensing restrictions of the published unrar source, ALL contributions to this codebase MUST NOT have used ANY part of the encumbered
	/// published unrar source in the development of this <see cref="DataFormat" />. It may end up that this <see cref="DataFormat" /> is only able
	/// to handle uncompressed (i.e. stored) RAR files, and until there exists a better solution to the unrar licensing problem, it will have to do.
	/// </remarks>
	public class RARDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
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

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;

			#region marker block
			string Rar = br.ReadFixedLengthString(4);
			if (Rar != "Rar!") throw new InvalidDataFormatException("File does not begin with \"Rar!\"");

			ushort a10 = br.ReadUInt16();
			byte a11 = br.ReadByte();
			if (a10 != 0x071A || a11 != 0x00) throw new InvalidDataFormatException("Invalid block header");
			#endregion

			#region archive header
			{
				ushort head_crc = br.ReadUInt16();
				RARHeaderType head_type = (RARHeaderType)br.ReadByte();
				RARHeaderFlags head_flags = (RARHeaderFlags)br.ReadUInt16();

				ushort head_size = br.ReadUInt16();
				ushort reserved1 = br.ReadUInt16();
				uint reserved2 = br.ReadUInt32();
			}
			#endregion

			#region File Entry
			while (br.Accessor.Position + 3 < br.Accessor.Length)
			{
				ushort head_crc = br.ReadUInt16();
				RARHeaderType head_type = (RARHeaderType)br.ReadByte();
				RARFileHeaderFlags head_flags = (RARFileHeaderFlags)br.ReadUInt16();

				ushort head_size = br.ReadUInt16();

				if (br.EndOfStream) break;

				uint compressedSize = br.ReadUInt32();
				uint decompressedSize = br.ReadUInt32();
				RARHostOperatingSystem hostOS = (RARHostOperatingSystem)br.ReadByte();
				uint fileCRC = br.ReadUInt32();
				uint dateTimeDOS = br.ReadUInt32();

				// Version number is encoded as 10 * Major version + minor version.
				byte requiredVersionToUnpack = br.ReadByte();

				RARCompressionMethod compressionMethod = (RARCompressionMethod)br.ReadByte();
				ushort fileNameSize = br.ReadUInt16();
				uint fileAttributes = br.ReadUInt32();

				if ((head_flags & RARFileHeaderFlags.SupportLargeFiles) == RARFileHeaderFlags.SupportLargeFiles)
				{
					// High 4 bytes of 64 bit value of compressed file size.
					uint highPackSize = br.ReadUInt32();
					// High 4 bytes of 64 bit value of uncompressed file size.
					uint highUnpackSize = br.ReadUInt32();
				}

				string filename = br.ReadFixedLengthString(fileNameSize);
				byte nul = br.ReadByte();

				if ((head_flags & RARFileHeaderFlags.EncryptionSaltPresent) == RARFileHeaderFlags.EncryptionSaltPresent)
				{
					long salt = br.ReadInt64();
				}

				if ((head_flags & RARFileHeaderFlags.ExtendedTimeFieldPresent) == RARFileHeaderFlags.ExtendedTimeFieldPresent)
				{
					uint exttime = br.ReadUInt32();

				}

				byte[] compressedData = br.ReadBytes(compressedSize);

				byte[] decompressedData = compressedData;

				fsom.Files.Add(filename, decompressedData);
			}
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
