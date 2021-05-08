//
//  OBJDataFormat.cs - a DataFormat to read and write intermediate object files
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
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.RelocatableObject
{
	/// <summary>
	/// A <see cref="DataFormat" /> to read and write intermediate object files.
	/// </summary>
	public class OBJDataFormat : DataFormat
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
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			while (!reader.EndOfStream)
			{
				OBJRecordType recordType = (OBJRecordType)reader.ReadByte();
				ushort dataLength = reader.ReadUInt16();
				byte[] data = reader.ReadBytes(dataLength);
				byte checksum = reader.ReadByte();

				switch (recordType)
				{
					case OBJRecordType.CodeDataText0xA0:
					case OBJRecordType.CodeDataText0xA1:
					{
						break;
					}
					case OBJRecordType.Comment:
					{
						break;
					}
					case OBJRecordType.CommonDataInitialized0xC2:
					case OBJRecordType.CommonDataInitialized0xC3:
					{
						break;
					}
					case OBJRecordType.CommonDataUninitialized:
					{
						break;
					}
					case OBJRecordType.ExternalReference:
					{
						break;
					}
					case OBJRecordType.ExternalSymbols0x90:
					case OBJRecordType.ExternalSymbols0x91:
					{
						break;
					}
					case OBJRecordType.ModuleEnd0x8A:
					case OBJRecordType.ModuleEnd0x8B:
					{
						break;
					}
					case OBJRecordType.Relocation0x9C:
					case OBJRecordType.Relocation0x9D:
					{
						break;
					}
					case OBJRecordType.Segment0x98:
					case OBJRecordType.Segment0x99:
					{
						break;
					}
					case OBJRecordType.SegmentGroup:
					{
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
