//
//  Z64DataFormat.cs - provides a DataFormat for manipulating Nintendo 64 game dump files in Z64 format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Nintendo.N64
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Nintendo 64 game dump files in Z64 format.
	/// </summary>
	public class Z64DataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);

			Reader reader = Accessor.Reader;

			byte endiannessIndicator = reader.ReadByte();
			byte initialPI_BSB_DOM1_LAT_REG = reader.ReadByte();
			if (endiannessIndicator == 0x80 && initialPI_BSB_DOM1_LAT_REG == 0x37)
			{
				reader.Endianness = Endianness.BigEndian;
			}
			else if (endiannessIndicator == 0x37 && initialPI_BSB_DOM1_LAT_REG == 0x80)
			{
				reader.Endianness = Endianness.LittleEndian;
			}
			byte initialPI_BSD_DOM1_PWD_REG = reader.ReadByte();
			byte initialPI_BSB_DOM1_PGS_REG = reader.ReadByte();

			uint clockRateOverride = reader.ReadUInt32();
			uint programCounter = reader.ReadUInt32();
			uint releaseAddress = reader.ReadUInt32();
			uint crc1 = reader.ReadUInt32();
			uint crc2 = reader.ReadUInt32();
			ulong unknown1 = reader.ReadUInt64(); // zero
			string imageName = reader.ReadFixedLengthString(20).TrimNull().Trim();
			uint unknown2 = reader.ReadUInt32(); // zero
			N64MediaFormat mediaformat = (N64MediaFormat)reader.ReadUInt32();
			string cartridgeID = reader.ReadFixedLengthString(2);
			N64CountryCode countryCode = (N64CountryCode)reader.ReadByte();
			byte version = reader.ReadByte();

			byte[] bootloader = reader.ReadBytes(4032);
			exe.Sections.Add("bootloader", bootloader);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
