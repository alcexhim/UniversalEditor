using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	[Flags()]
	public enum FileAttributes
	{
		None = 0,
		ReadOnly = 1,
		Hidden = 2,
		System = 4,
		Archive = 8,
		Compressed = 16,
		Encrypted = 32,
		Deleted = 64,
		Indexed = 128
	}
}
