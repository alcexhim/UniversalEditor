using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.Executable.ELF
{
	public class ELFDataFormat : DataFormat
	{
		private const byte EV_CURRENT = 1;

		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(ExecutableObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Executable and Linkable Format", new byte?[][] { new byte?[] { (byte)0x7F, (byte)'E', (byte)'L', (byte)'F' } }, new string[] { "*.elf" });
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

		private struct ELFSectionEntry
		{
			public string name;
			/// <summary>
			/// This member specifies the name of the section. Its value is an index into
			/// the section header string table section, giving the location of a
			/// null-terminated string.
			/// </summary>
			public uint nameindex;
			/// <summary>
			/// This member categorizes the section’s contents and semantics. Section types
			/// and their descriptions appear below.
			/// </summary>
			public ELFSectionType type;
			/// <summary>
			/// Sections support 1-bit flags that describe miscellaneous attributes.
			/// </summary>
			public ELFSectionFlags flags;
			/// <summary>
			/// If the section will appear in the memory image of a process, this member
			/// gives the address at which the section’s first byte should reside.
			/// Otherwise, the member contains 0.
			/// </summary>
			public uint addr;
			/// <summary>
			/// This member’s value gives the byte offset from the beginning of the file
			/// to the first byte in the section. One section type, SHT_NOBITS described
			/// below, occupies no space in the file, and its sh_offset member locates
			/// the conceptual placement in the file.
			/// </summary>
			public uint offset;
			/// <summary>
			/// This member gives the section’s size in bytes. Unless the section type is
			/// SHT_NOBITS, the section occupies sh_size bytes in the file. A section of
			/// type SHT_NOBITS may have a non-zero size, but it occupies no space in the
			/// file.
			/// </summary>
			public uint size;
			/// <summary>
			/// This member holds a section header table index link, whose interpretation
			/// depends on the section type. A table below describes the values.
			/// </summary>
			public uint link;
			/// <summary>
			/// This member holds extra information, whose interpretation depends on the
			/// section type. A table below describes the values.
			/// </summary>
			public uint info;
			/// <summary>
			/// Some sections have address alignment constraints. For example, if a
			/// section holds a doubleword, the system must ensure doubleword alignment
			/// for the entire section. That is, the value of sh_addr must be congruent
			/// to 0, modulo the value of sh_addralign. Currently, only 0 and positive
			/// integral powers of two are allowed. Values 0 and 1 mean the section has
			/// no alignment constraints.
			/// </summary>
			public uint addralign;
			/// <summary>
			/// Some sections hold a table of fixed-size entries, such as a symbol table.
			/// For such a section, this member gives the size in bytes of each entry.
			/// The member contains 0 if the section does not hold a table of fixed-size
			/// entries.
			/// </summary>
			public uint entsize;
		}

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
			uint e_entry = br.ReadUInt32();

			// This member holds the program header table’s file offset in bytes. If the
			// file has no program header table, this member holds zero.
			uint e_phoff = br.ReadUInt32();

			// This member holds the section header table’s file offset in bytes. If the
			// file has no section header table, this member holds zero.
			uint e_shoff = br.ReadUInt32();

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
			br.Accessor.Position = baseoffset + e_shoff;
			List<ELFSectionEntry> sections = new List<ELFSectionEntry>();
			for (ushort i = 0; i < e_shnum; i++)
			{
				ELFSectionEntry sh = new ELFSectionEntry();
				sh.nameindex = br.ReadUInt32();
				sh.type = (ELFSectionType)br.ReadUInt32();
				sh.flags = (ELFSectionFlags)br.ReadUInt32();
				sh.addr = br.ReadUInt32();
				sh.offset = br.ReadUInt32();
				sh.size = br.ReadUInt32();
				sh.link = br.ReadUInt32();
				sh.info = br.ReadUInt32();
				sh.addralign = br.ReadUInt32();
				sh.entsize = br.ReadUInt32();
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
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{

		}
	}
}
