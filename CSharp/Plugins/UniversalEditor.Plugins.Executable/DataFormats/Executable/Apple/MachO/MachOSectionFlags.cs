using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	[Flags()]
	public enum MachOSectionFlags
	{
		/// <summary>
		/// zero-filled on demand
		/// </summary>
		ZeroFill = 0x01,
		/// <summary>
		/// section has only literal C strings
		/// </summary>
		LiteralCStrings = 0x02,
		/// <summary>
		/// section has only 4-byte literals
		/// </summary>
		Literal4Bytes = 0x03,
		/// <summary>
		/// section has only 8-byte literals
		/// </summary>
		Literal8Bytes = 0x04,
		/// <summary>
		/// section has only pointers to literals
		/// </summary>
		LiteralPointers = 0x05
	}
}
