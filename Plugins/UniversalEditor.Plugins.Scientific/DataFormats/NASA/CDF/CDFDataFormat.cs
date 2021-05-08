//
//  CDFDataFormat.cs
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
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.Scientific.DataFormats.NASA.CDF
{
	public class CDFDataFormat : DataFormat
	{
		private const int COPYRIGHT_FIELD_LENGTH_BEFORE_2_5 = 1945;
		private const int COPYRIGHT_FIELD_LENGTH_AFTER_2_5 = 256;
		private readonly Version VERSION_2_5 = new Version(2, 5);

		public Version FormatVersion { get; set; } = new Version(2, 5);

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			uint magic = reader.ReadUInt32(); // 0xCDF26002, 0xCDF30000
			uint compressionFlag = reader.ReadUInt32(); // 0x0000FFFF uncompressed, 0xCCCC0001 compressed
			uint dummy = reader.ReadUInt32(); // 0x00000000

			while (reader.EndOfStream)
			{
				long pos = reader.Accessor.Position;
				uint recordSize = reader.ReadUInt32(); // 318
				CDFRecordType recordType = (CDFRecordType)reader.ReadUInt32(); // 1 - CDR

				switch (recordType)
				{
					case CDFRecordType.CDR:
					{
						int GDRoffset = reader.ReadInt32();

						// The version of the CDF distribution (library) that created this CDF. CDF
						// distributions are identi ed with four values: version, release, increment,
						// and sub-increment.For example, CDF V2.5.8a is CDF version 2, release 5,
						// increment 8, sub - increment `a'. Note that the sub-increment is not stored in a CDF.
						int version = reader.ReadInt32();

						// The release of the CDF distribution that created this CDF. See the Version field above.
						int release = reader.ReadInt32();

						// The data encoding for attribute entry and variable values.
						int encoding = reader.ReadInt32();

						CDFFileFlags flags = (CDFFileFlags)reader.ReadInt32();

						int rfuA = reader.ReadInt32(); // reserved
						int rfuB = reader.ReadInt32(); // reserved

						// The increment of the CDF distribution that created this CDF. See the Version field above. Prior to CDF V2.1 this field was always set to zero.
						int increment = reader.ReadInt32();

						int rfuD = reader.ReadInt32(); // reserved
						int rfuE = reader.ReadInt32(); // reserved

						FormatVersion = new Version(version, release, increment);

						string copyright = null;
						if (FormatVersion < VERSION_2_5)
						{
							copyright = reader.ReadFixedLengthString(COPYRIGHT_FIELD_LENGTH_BEFORE_2_5);
						}
						else
						{
							copyright = reader.ReadFixedLengthString(COPYRIGHT_FIELD_LENGTH_AFTER_2_5);
						}

						break;
					}
				}

				if (reader.Accessor.Position < pos + recordSize)
				{
					reader.Accessor.Seek((pos + recordSize) - reader.Accessor.Position, SeekOrigin.Current);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
