using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Kronosaur.TDB
{
	public struct ALDFEntryStruct
	{
		/// <summary>
		/// Block Number (-1 = unused)
		/// </summary>
		public int dwBlock;
		/// <summary>
		/// Number of blocks reserved for entry
		/// </summary>
		public int dwBlockCount;
		/// <summary>
		/// Size of entry
		/// </summary>
		public int dwSize;
		/// <summary>
		/// Version number
		/// </summary>
		public int dwVersion;
		/// <summary>
		/// Previous version
		/// </summary>
		public int dwPrevEntry;
		/// <summary>
		/// Latest entry (-1 if this is latest)
		/// </summary>
		public int dwLatestEntry;
		/// <summary>
		/// Misc flags
		/// </summary>
		public ALDFEntryFlags dwFlags;
	}
}
