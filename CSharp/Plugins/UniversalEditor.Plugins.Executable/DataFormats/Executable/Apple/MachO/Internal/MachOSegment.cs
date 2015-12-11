using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO.Internal
{
	internal struct MachOSegment
	{
		/// <summary>
		/// Segment's name
		/// </summary>
		public string segname;
		/// <summary>
		/// segment's memory address
		/// </summary>
		public uint vmaddr;
		/// <summary>
		/// segment's memory size
		/// </summary>
		public uint vmsize;
		/// <summary>
		/// segment's file offset
		/// </summary>
		public uint fileoff;
		/// <summary>
		/// amount to map from file
		/// </summary>
		public uint filesize;
		/// <summary>
		/// maximum VM protection
		/// </summary>
		public MachOVMProtection maxprot;
		/// <summary>
		/// initial VM protection
		/// </summary>
		public MachOVMProtection initprot;
		/// <summary>
		/// number of sections
		/// </summary>
		public uint nsects;
		/// <summary>
		/// flags
		/// </summary>
		public MachOSegmentFlags flags;
	}
}
