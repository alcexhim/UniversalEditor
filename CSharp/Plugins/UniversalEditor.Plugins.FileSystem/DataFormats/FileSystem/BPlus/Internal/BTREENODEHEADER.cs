using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.BPlus.Internal
{
	internal struct BTREENODEHEADER
	{
		/// <summary>
		/// Number of free bytes at the end of this page.
		/// </summary>
		public ushort UnusedByteCount;
		/// <summary>
		/// Number of entries in this index-page.
		/// </summary>
		public short PageEntriesCount;
		/// <summary>
		/// Page number of previous page, or -1 if first.
		/// </summary>
		public short PreviousPageIndex;
		/// <summary>
		/// Page number of next page, or -1 if last.
		/// </summary>
		public short NextPageIndex;
	}
}
