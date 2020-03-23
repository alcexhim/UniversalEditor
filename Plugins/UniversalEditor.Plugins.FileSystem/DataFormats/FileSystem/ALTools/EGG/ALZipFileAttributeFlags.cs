using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG
{
	[Flags()]
	public enum ALZipFileAttributeFlags
	{
		None = 0x00,
		ReadOnly = 0x01,
		Hidden = 0x02,
		System = 0x04,
		SymbolicLink = 0x08,
		Undefined10 = 0x10,
		Undefined20 = 0x20,
		Undefined40 = 0x40,
		Directory = 0x80
	}
}
