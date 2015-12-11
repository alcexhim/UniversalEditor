using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO.Internal
{
	public struct MachOSection
	{
		/// <summary>
		/// section's name
		/// </summary>
		public string sectname;
		/// <summary>
		/// segment the section is in
		/// </summary>
		public string segname;
		/// <summary>
		/// section's memory address
		/// </summary>
		public uint addr;
		/// <summary>
		/// section's size in bytes
		/// </summary>
		public uint size;
		/// <summary>
		/// section's file offset
		/// </summary>
		public uint offset;
		/// <summary>
		/// section's alignment
		/// </summary>
		public uint align;
		/// <summary>
		/// file offset of relocation entries
		/// </summary>
		public uint reloff;
		/// <summary>
		/// number of relocation entries
		/// </summary>
		public uint nreloc;
		/// <summary>
		/// flags
		/// </summary>
		public MachOSectionFlags flags;
		/// <summary>
		/// reserved
		/// </summary>
		public uint reserved1;
		/// <summary>
		/// reserved
		/// </summary>
		public uint reserved2;
	}
}
