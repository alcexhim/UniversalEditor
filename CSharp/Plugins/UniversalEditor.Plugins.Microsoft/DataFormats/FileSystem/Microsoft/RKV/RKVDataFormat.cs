//
//  RKVDataFormat.cs
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.RKV
{
	/// <summary>
	/// RKV archives (Microsoft XNA)
	/// Blade Kitten, Game Room, Star Wars: The Clone Wars - Repulic Heroes and others
	/// </summary>
	public class RKVDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "Microsoft XNA RKV archive";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;

			/*
				compression type: lzf (type 2)
			 */

			string NAME = br.ReadFixedLengthString(0x40);
			long info_off = br.ReadInt64();
			long files = br.ReadInt64();

			base.Accessor.Seek(info_off, SeekOrigin.Begin);

			for (long i = 0; i < files; i++)
			{
				string fileName = br.ReadFixedLengthString(0x40);
				long timestamp = br.ReadInt64();
				long offset = br.ReadInt64();
				int crc = br.ReadInt32();
				int size = br.ReadInt32();
				int zsize = br.ReadInt32();
				int zero = br.ReadInt32();

				if (size == zsize)
				{
					// log NAME OFFSET SIZE
				}
				else
				{
					long pos = Accessor.Position;
					Accessor.Seek(offset, SeekOrigin.Begin);
					byte type = br.ReadByte();
					switch (type)
					{
					case 2:
						break;
					default:
						Console.WriteLine("RKV: unknown compression method " + type.ToString());
						break;
					}
					Accessor.Seek(pos, SeekOrigin.Begin);

					offset++; // first byte is 0x02
					zsize--;

					// clog NAME OFFSET ZSIZE SIZE
				}

				fsom.Files.Add(fileName, new byte[size]);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
