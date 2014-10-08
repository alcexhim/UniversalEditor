using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.DeepSilver.SecretFiles
{
	internal struct SPRFileInfo
	{
		/// <summary>
		/// Index into directory table, 0x1effffff for root
		/// </summary>
		public int parentDirectoryIndex;

		public int fileNamePrefixIndex;
		public int fileNameSuffixIndex;
		public int crc32;

		public DateTime timestamp;

		public uint decompressedLength;
		/// <summary>
		/// Compression header offset (0xffffffff if file is not compressed)
		/// </summary>
		public int compressionHeaderOffset;
		/// <summary>
		/// Compression header size (zero if file is not compressed)
		/// </summary>
		public uint compressionHeaderLength;

		public uint offset;
		public uint compressedLength;
	}
}
