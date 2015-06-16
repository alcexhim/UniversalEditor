using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	[Flags()]
	public enum HFSFileFlags : short
	{
		None = 0x00,
		/// <summary>
		/// If set, this file is locked and cannot be written to.
		/// </summary>
		Locked = 0x01,
		/// <summary>
		/// If set, a file thread record exists for this file.
		/// </summary>
		ThreadRecordExists = 0x02,
		/// <summary>
		/// If set, the file record is used.
		/// </summary>
		Used = 0x40
	}
}
