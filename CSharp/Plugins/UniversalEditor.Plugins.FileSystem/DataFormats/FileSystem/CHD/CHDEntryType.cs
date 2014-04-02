using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	public enum CHDEntryType
	{
		/// <summary>
		/// Invalid entry type.
		/// </summary>
		Invalid = 0,
		/// <summary>
		/// Standard compression.
		/// </summary>
		Compressed = 1,
		/// <summary>
		/// Uncompressed data.
		/// </summary>
		Uncompressed = 2,
		/// <summary>
		/// Mini: Use offset as raw data.
		/// </summary>
		Miniature = 3,
		/// <summary>
		/// The hunk contains the same data as another hunk in this file.
		/// </summary>
		SelfHunk = 4,
		/// <summary>
		/// The hunk contains the same data as another hunk in the parent file.
		/// </summary>
		ParentHunk = 5,
		/// <summary>
		/// The hunk is compressed with a secondary algorithm (usually FLAC CDDA).
		/// </summary>
		SecondaryCompressed = 6
	}
}
