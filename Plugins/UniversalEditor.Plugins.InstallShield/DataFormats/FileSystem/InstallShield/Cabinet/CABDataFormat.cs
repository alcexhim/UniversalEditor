//
//  CABDataFormat.cs - provides a DataFormat for manipulating InstallShield cabinet archive files
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.InstallShield.Cabinet
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating InstallShield cabinet archive files.
	/// </summary>
	public class CABDataFormat : DataFormat
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

		private const int MAX_FILE_GROUP_COUNT = 71;
		private const int MAX_COMPONENT_COUNT = 71;

		private const int OFFSET_COUNT = 0x47;

		private const int CAB_SIGNATURE = 0x28635349;
		private const int MSCF_SIGNATURE = 0x4643534d;

		private const int COMMON_HEADER_SIZE = 20;
		private const int VOLUME_HEADER_SIZE_V5 = 40;
		private const int VOLUME_HEADER_SIZE_V6 = 64;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = Accessor.Reader;
			br.Accessor.Seek(0, SeekOrigin.Begin);
			br.Endianness = Endianness.LittleEndian;

			string ISc_ = br.ReadFixedLengthString(4);
			if (ISc_ != "ISc(") throw new InvalidDataFormatException();


			string hdrfilename = System.IO.Path.ChangeExtension(Accessor.GetFileName(), ".hdr");
			if (System.IO.File.Exists(hdrfilename))
			{
				FileAccessor fa = new FileAccessor(hdrfilename, false, false, true);

				HDRDataFormat hdr = new HDRDataFormat();
				hdr.IgnoreCabinet = true; // set this because WE are the cabinet

				Document.Load(fsom, hdr, fa);
			}

			uint version = br.ReadUInt32();
			int majorVersion = 0;
			if (version >> 24 == 1)
			{
				majorVersion = (int)((version >> 12) & 0xf);
			}
			else if (version >> 24 == 2 || version >> 24 == 4)
			{
				majorVersion = (int)(version & 0xffff);
				if (majorVersion != 0)
				{
					majorVersion = (int)(majorVersion / 100);
				}
			}

			uint volumeInfo = br.ReadUInt32();
			uint cabinetDescriptorOffset = br.ReadUInt32();
			uint cabinetDescriptorSize = br.ReadUInt32();

			br.Accessor.Seek(cabinetDescriptorOffset, SeekOrigin.Begin);


			int unknown1 = br.ReadInt32();
			int unknown2 = br.ReadInt32();
			int unknown3 = br.ReadInt32();

			uint fileTableOffset = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();
			uint fileTableSize = br.ReadUInt32();
			uint fileTableSize2 = br.ReadUInt32();
			uint directoryCount = br.ReadUInt32();
			uint unknown5 = br.ReadUInt32();
			uint unknown6 = br.ReadUInt32();
			uint fileCount = br.ReadUInt32();
			uint fileTableOffset2 = br.ReadUInt32();
			return;

			// if (br.Accessor.Position != 0x30) throw new InvalidDataFormatException();
			// if (fileTableSize != fileTableSize2) throw new InvalidDataFormatException("File table sizes do not match");

			uint unknown7 = br.ReadUInt32();
			uint unknown8 = br.ReadUInt32();
			uint unknown9 = br.ReadUInt32();
			ushort unknown10 = br.ReadUInt16();

			uint[] fileGroupOffsets = new uint[MAX_FILE_GROUP_COUNT];
			for (int i = 0; i < MAX_FILE_GROUP_COUNT; i++)
			{
				fileGroupOffsets[i] = br.ReadUInt32();
			}
			uint[] componentOffsets = new uint[MAX_COMPONENT_COUNT];
			for (int i = 0; i < MAX_COMPONENT_COUNT; i++)
			{
				componentOffsets[i] = br.ReadUInt32();
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("ISc(");

			int unknown1 = 0;
			int unknown2 = 0;
			uint fileTableOffset = 0;
			uint unknown3 = 0;
			uint fileTableSize = 0;
			uint fileTableSize2 = 0;
			uint directoryCount = 0;
			uint unknown4 = 0;
			uint unknown5 = 0;
			uint fileCount = 0;
			uint fileTableOffset2 = 0;
			uint unknown6 = 0;
			uint unknown7 = 0;
			uint unknown8 = 0;
			ushort unknown9 = 0;

			bw.WriteInt32(unknown1);
			bw.WriteInt32(unknown2);
			bw.WriteUInt32(fileTableOffset);
			bw.WriteUInt32(unknown3);
			bw.WriteUInt32(fileTableSize);
			bw.WriteUInt32(fileTableSize2);
			bw.WriteUInt32(directoryCount);
			bw.WriteUInt32(unknown4);
			bw.WriteUInt32(unknown5);
			bw.WriteUInt32(fileCount);
			bw.WriteUInt32(fileTableOffset2);
			bw.WriteUInt32(unknown6);
			bw.WriteUInt32(unknown7);
			bw.WriteUInt32(unknown8);
			bw.WriteUInt16(unknown9);

			uint[] fileGroupOffsets = new uint[MAX_FILE_GROUP_COUNT];
			uint[] componentOffsets = new uint[MAX_COMPONENT_COUNT];

			for (int i = 0; i < MAX_FILE_GROUP_COUNT; i++)
			{
				bw.WriteUInt32(fileGroupOffsets[i]);
			}
			for (int i = 0; i < MAX_COMPONENT_COUNT; i++)
			{
				bw.WriteUInt32(componentOffsets[i]);
			}
			bw.Flush();
		}
	}
}
