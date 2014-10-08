using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.BPlus.Internal
{
	internal struct FILEHEADER
	{
		/// <summary>
		/// size reserved including FILEHEADER
		/// </summary>
		public int ReservedSpace;
		/// <summary>
		/// normally 4
		/// </summary>
		public byte FileFlags;
		/// <summary>
		/// the bytes contained in the internal file
		/// </summary>
		public byte[] FileContent;
		public byte[] FreeSpace;
	}
}
