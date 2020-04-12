//
//  ELFDataFormat.cs - provides a DataFormat for reading and writing Executable and Linkable Format (ELF) executable files
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.Executable.ELF
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for reading and writing Executable and Linkable Format (ELF) executable files.
	/// </summary>
	public class ELFDataFormat : DataFormat
	{
		private const byte EV_CURRENT = 1;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private ELFCapacity mvarCapacity = ELFCapacity.elf32Bit;
		public ELFCapacity Capacity { get { return mvarCapacity; } set { mvarCapacity = value; } }

		private ELFEncoding mvarEncoding = ELFEncoding.TwosComplementLSB;
		public ELFEncoding Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

		private ELFMachine mvarMachine = ELFMachine.None;
		/// <summary>
		/// Specifies the required architecture for the executable file.
		/// </summary>
		public ELFMachine Machine { get { return mvarMachine; } set { mvarMachine = value; } }

		private ELFObjectFileType mvarObjectFileType = ELFObjectFileType.None;
		/// <summary>
		/// Identifies the object file type.
		/// </summary>
		public ELFObjectFileType ObjectFileType { get { return mvarObjectFileType; } set { mvarObjectFileType = value; } }

		private byte mvarFormatVersion = EV_CURRENT;
		public byte FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		/*
				Elf32_Addr    = Unsigned 4 byte program address
				Elf32_Half    = Unsigned 2 byte integer
				Elf32_Off     = Unsigned 4 byte file offset
				Elf32_Sword   = Signed   4 byte integer
				Elf32_Word    = Unsigned 4 byte integer
				unsigned char = Unsigned 1 byte integer
		 */

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ExecutableObjectModel exe = (objectModel as ExecutableObjectModel);
			if (exe == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			long baseoffset = br.Accessor.Position;

			#region Header
			#region e_ident
			{
				byte signature1 = br.ReadByte();
				string signature2 = br.ReadFixedLengthString(3);
				if (signature1 != 0x7F || signature2 != "ELF") throw new InvalidDataFormatException("File does not begin with 0x7F, \"ELF\"");

				mvarCapacity = (ELFCapacity)br.ReadByte();
				mvarEncoding = (ELFEncoding)br.ReadByte();
				mvarFormatVersion = br.ReadByte();
				byte[] padding = br.ReadBytes(8);
				byte nident = br.ReadByte(); // should be 16
				// if (nident != 16) throw new InvalidDataFormatException("n_ident is not equal to 16");
			}
			#endregion
			mvarObjectFileType = (ELFObjectFileType)br.ReadUInt16();
			mvarMachine = (ELFMachine)br.ReadUInt16();
			uint e_version = br.ReadUInt32();

			// This member gives the virtual address to which the system first transfers
			// control, thus starting the process. If the file has no associated entry
			// point, this member holds zero.
			ulong e_entry = ReadAddress(br);

			// This member holds the program header table’s file offset in bytes. If the
			// file has no program header table, this member holds zero.
			ulong e_phoff = ReadAddress(br);

			// This member holds the section header table’s file offset in bytes. If the
			// file has no section header table, this member holds zero.
			ulong e_shoff = ReadAddress(br);

			// This member holds processor-specific flags associated with the file. Flag
			// names take the form EF_machine_flag. See "Machine Information" for flag
			// definitions.
			uint e_flags = br.ReadUInt32();

			// This member holds the ELF header’s size in bytes.
			ushort e_ehsize = br.ReadUInt16();

			// This member holds the size in bytes of one entry in the file’s program
			// header table; all entries are the same size.
			ushort e_phentsize = br.ReadUInt16();

			// This member holds the number of entries in the program header table. Thus
			// the product of e_phentsize and e_phnum gives the table’s size in bytes. If a
			// file has no program header table, e_phnum holds the value zero.
			ushort e_phnum = br.ReadUInt16();

			// This member holds a section header’s size in bytes. A section header is one
			// entry in the section header table; all entries are the same size.
			ushort e_shentsize = br.ReadUInt16();

			// This member holds the number of entries in the section header table. Thus
			// the product of e_shentsize and e_shnum gives the section header table's size
			// in bytes. If a file has no section header table, e_shnum holds the value
			// zero.
			ushort e_shnum = br.ReadUInt16();

			// This member holds the section header table index of the entry associated with
			// the section name string table. If the file has no section name string table,
			// this member holds the value SHN_UNDEF.
			ushort e_shstrndx = br.ReadUInt16();
			#endregion
			#region Section Table
			br.Accessor.Position = baseoffset + (long)e_shoff;
			List<ELFSectionEntry> sections = new List<ELFSectionEntry>();
			for (ushort i = 0; i < e_shnum; i++)
			{
				ELFSectionEntry sh = new ELFSectionEntry();
				sh.nameindex = br.ReadUInt32();
				sh.type = (ELFSectionType)br.ReadUInt32();
				sh.flags = (ELFSectionFlags)(Capacity == ELFCapacity.elf64Bit ? br.ReadUInt64() : br.ReadUInt32());
				sh.addr = ReadAddress(br);
				sh.offset = br.ReadUInt32();
				sh.size = (Capacity == ELFCapacity.elf64Bit ? br.ReadUInt64() : br.ReadUInt32());
				sh.link = br.ReadUInt32();
				sh.info = br.ReadUInt32();
				sh.addralign = (Capacity == ELFCapacity.elf64Bit ? br.ReadUInt64() : br.ReadUInt32());
				sh.entsize = (Capacity == ELFCapacity.elf64Bit ? br.ReadUInt64() : br.ReadUInt32());
				sections.Add(sh);
			}
			#endregion

			// find the section header string table
			if (e_shstrndx > 0)
			{
				ELFSectionEntry shstr = sections[e_shstrndx];
				long pos = br.Accessor.Position;
				long stroffset = baseoffset + shstr.offset;
				for (int i = 0; i < sections.Count; i++)
				{
					ELFSectionEntry entry = sections[i];
					br.Accessor.Position = stroffset + entry.nameindex;
					entry.name = br.ReadNullTerminatedString();
					sections[i] = entry;
				}
				br.Accessor.Position = pos;
			}


			for (int i = 0; i < sections.Count; i++)
			{
				ExecutableSection sect = new ExecutableSection();
				sect.PhysicalAddress = sections[i].offset;
				sect.VirtualSize = sections[i].size;
				sect.DataRequest += sect_DataRequest;
				exe.Sections.Add(sect);
			}
		}

		private ulong ReadAddress(Reader br)
		{
			bool is64bit = Capacity == ELFCapacity.elf64Bit;
			return (is64bit ? br.ReadUInt64() : br.ReadUInt32());
		}

		private void sect_DataRequest(object sender, DataRequestEventArgs e)
		{
			ExecutableSection sect = (sender as ExecutableSection);
			if (sect == null)
				return;

			Accessor.Reader.Seek(sect.PhysicalAddress, SeekOrigin.Begin);
			e.Data = Accessor.Reader.ReadBytes(sect.VirtualSize);
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{

		}
	}
}
