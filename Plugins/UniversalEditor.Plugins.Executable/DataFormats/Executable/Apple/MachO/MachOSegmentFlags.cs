using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	[Flags()]
	public enum MachOSegmentFlags
	{
		None = 0x00,
		/// <summary>
		/// Indicates that the file contents for this segment occupy the high part of the virtual memory space; the low part is zero-filled (for stacks in core files).
		/// </summary>
		HighVM = 0x01,
		/// <summary>
		/// Indicates that the segment is the virtual memory that's allocated by a fixed virtual memory library for overlap checking in the link editor.
		/// </summary>
		FVMLib = 0x02,
		/// <summary>
		/// Indicates that the segment has nothing that was relocated in it and nothing relocated to it (that is, it may be safely replaced without relocation).
		/// </summary>
		NoReloc = 0x03
	}
}
