//
//  MachODataFormat.cs - provides a DataFormat to read and write Apple Mach-O executable files
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

using UniversalEditor.DataFormats.Executable.Apple.MachO.Internal;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to read and write Apple Mach-O executable files.
	/// </summary>
	public class MachODataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("https://developer.apple.com/library/mac/documentation/DeveloperTools/Conceptual/MachORuntime/index.html#//apple_ref/c/tag/mach_header");
			}
			return _dfr;
		}

		private MachOCpuType mvarCpuType = MachOCpuType.X86;
		/// <summary>
		/// The architecture you intend to use the file on.
		/// </summary>
		public MachOCpuType CpuType { get { return mvarCpuType; } set { mvarCpuType = value; } }

		private MachOCpuSubType mvarCpuSubType = MachOCpuSubType.PowerPC_All;
		public MachOCpuSubType CpuSubType { get { return mvarCpuSubType; } set { mvarCpuSubType = value; } }

		private MachOFileType mvarFileType = MachOFileType.Executable;
		public MachOFileType FileType { get { return mvarFileType; } set { mvarFileType = value; } }

		private MachOFlags mvarFlags = MachOFlags.None;
		public MachOFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		private static readonly byte[] m_MachOMagicBigEndian = new byte[] { 0xFE, 0xED, 0xFA, 0xCE };
		private static readonly byte[] m_MachOMagicLittleEndian = new byte[] { 0xCE, 0xFA, 0xED, 0xFE };

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			// An integer containing a value identifying this file as a 32-bit Mach-O file. Use the
			// constant MH_MAGIC if the file is intended for use on a CPU with the same endianness
			// as the computer on which the compiler is running. The constant MH_CIGAM can be used
			// when the byte ordering scheme of the target machine is the reverse of the host CPU.
			reader.Accessor.Position = 0;

			byte[] bytes = reader.ReadBytes(4);
			MachOMagic magic = MachOMagic.None;
			if (bytes.Match(m_MachOMagicBigEndian))
			{
				magic = MachOMagic.MachOBigEndian;
			}
			else if (bytes.Match(m_MachOMagicLittleEndian))
			{
				magic = MachOMagic.MachOLittleEndian;
			}

			// set up endianness
			switch (magic)
			{
				case MachOMagic.MachOBigEndian:
				{
					reader.Endianness = Endianness.BigEndian;
					break;
				}
				case MachOMagic.MachOLittleEndian:
				{
					reader.Endianness = Endianness.LittleEndian;
					break;
				}
			}

			// parse the format
			switch (magic)
			{
				case MachOMagic.MachOBigEndian:
				case MachOMagic.MachOLittleEndian:
				{
					mvarCpuType = (MachOCpuType)reader.ReadInt32();
					mvarCpuSubType = (MachOCpuSubType)reader.ReadInt32();
					mvarFileType = (MachOFileType)reader.ReadInt32();

					uint loadCommandCount = reader.ReadUInt32();
					uint loadCommandAreaSize = reader.ReadUInt32();

					mvarFlags = (MachOFlags)reader.ReadInt32();

					for (uint i = 0; i < loadCommandCount; i++)
					{
						MachOLoadCommandType loadCommandType = (MachOLoadCommandType)reader.ReadUInt32();
						uint loadCommandSize = reader.ReadUInt32();
						switch (loadCommandType)
						{
							case MachOLoadCommandType.Segment:
							{
								MachOSegment segment = ReadMachOSegment(reader);
								for (uint j = 0; j < segment.nsects; j++)
								{
									MachOSection section = ReadMachOSection(reader);
								}
								break;
							}
							case (MachOLoadCommandType)12:
							{
								// BFD_MACH_O_LC_LOAD_DYLIB
								uint filenameLength = reader.ReadUInt32();
								uint libraryBuildTimestamp = reader.ReadUInt32();
								uint currentVersion = reader.ReadUInt32();
								uint compatibilityVersion = reader.ReadUInt32();
								string fileName = reader.ReadFixedLengthString(filenameLength);
								uint unknown5 = reader.ReadUInt32();
								uint unknown6 = reader.ReadUInt32();
								uint unknown7 = reader.ReadUInt32();
								break;
							}
							case (MachOLoadCommandType)24:
							{
								// BFD_MACH_O_LC_LOAD_WEAK_DYLIB
								uint unknown1 = reader.ReadUInt32();
								uint unknown2 = reader.ReadUInt32();
								break;
							}
							default:
							{
								break;
							}
						}
					}
					break;
				}
				default:
				{
					throw new InvalidDataFormatException("The executable format 0x" + ((uint)magic).ToString("X").PadLeft(8, '0') + " is not supported");
				}
			}
		}

		private MachOSection ReadMachOSection(Reader reader)
		{
			MachOSection section = new MachOSection();
			section.sectname = reader.ReadFixedLengthString(16).TrimNull();
			section.segname = reader.ReadFixedLengthString(16).TrimNull();
			section.addr = reader.ReadUInt32();
			section.size = reader.ReadUInt32();
			section.offset = reader.ReadUInt32();
			section.align = reader.ReadUInt32();
			section.reloff = reader.ReadUInt32();
			section.nreloc = reader.ReadUInt32();
			section.flags = (MachOSectionFlags)reader.ReadUInt32();
			section.reserved1 = reader.ReadUInt32();
			section.reserved2 = reader.ReadUInt32();
			return section;
		}

		private MachOSegment ReadMachOSegment(Reader reader)
		{
			MachOSegment segment = new MachOSegment();
			segment.segname = reader.ReadFixedLengthString(16).TrimNull();
			segment.vmaddr = reader.ReadUInt32();
			segment.vmsize = reader.ReadUInt32();
			segment.fileoff = reader.ReadUInt32();
			segment.filesize = reader.ReadUInt32();
			segment.maxprot = (MachOVMProtection)reader.ReadUInt32();
			segment.initprot = (MachOVMProtection)reader.ReadUInt32();
			segment.nsects = reader.ReadUInt32();
			segment.flags = (MachOSegmentFlags)reader.ReadUInt32();
			return segment;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			MachOMagic magic = MachOMagic.MachOBigEndian;

			writer.WriteUInt32((uint)magic);

			switch (magic)
			{
				case MachOMagic.MachOBigEndian:
				{
					writer.Endianness = Endianness.BigEndian;
					break;
				}
				case MachOMagic.MachOLittleEndian:
				{
					writer.Endianness = Endianness.LittleEndian;
					break;
				}
			}


			switch (magic)
			{
				case MachOMagic.MachOBigEndian:
				case MachOMagic.MachOLittleEndian:
				{
					writer.WriteInt32((int)mvarCpuType);
					writer.WriteInt32((int)mvarCpuSubType);
					writer.WriteInt32((int)mvarFileType);

					uint loadCommandCount = 0;
					uint loadCommandSize = 0;
					writer.WriteUInt32(loadCommandCount);
					writer.WriteUInt32(loadCommandSize);

					writer.WriteInt32((int)mvarFlags);
					break;
				}
				default:
				{
					throw new InvalidDataFormatException("The executable format 0x" + ((uint)magic).ToString("X") + " is not supported");
				}
			}
		}
	}
}
