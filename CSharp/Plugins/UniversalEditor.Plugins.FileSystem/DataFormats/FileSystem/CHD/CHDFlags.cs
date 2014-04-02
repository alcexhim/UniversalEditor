using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	[Flags()]
	public enum CHDFlags
	{
		None = 0x00000000,
		/// <summary>
		/// Set if this drive has a parent.
		/// </summary>
		HasParent = 0x00000001,
		/// <summary>
		/// Set if this drive allows writes.
		/// </summary>
		AllowWrite = 0x00000002
	}
}
