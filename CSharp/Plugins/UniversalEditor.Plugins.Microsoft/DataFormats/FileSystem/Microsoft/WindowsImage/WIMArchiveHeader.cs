using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public struct WIMArchiveHeader
	{
		/// <summary>
		/// Signature that identifies the file as a .wim file. Value is set to “MSWIM\0\0”.
		/// </summary>
		public string magic;
		/// <summary>
		/// Size of the WIM header in bytes.
		/// </summary>
		public uint cbSize;
		/// <summary>
		/// The current version of the .wim file. This number will increase if the format of the .wim file changes.
		/// </summary>
		public uint dwVersion;
		public WIMArchiveFlags dwFlags;
		/// <summary>
		/// Size of the compressed .wim file in bytes.
		/// </summary>
		public uint dwCompressionSize;
		/// <summary>
		/// A unique identifier for this WIM archive.
		/// </summary>
		public Guid gWIMGuid;
		/// <summary>
		/// The part number of the current .wim file in a spanned set. This value is 1, unless the data of the .wim file was split into multiple parts (.swm).
		/// </summary>
		public ushort usPartNumber;
		/// <summary>
		/// The total number of .wim file parts in a spanned set.
		/// </summary>
		public ushort usTotalParts;
		/// <summary>
		/// The number of images contained in the .wim file.
		/// </summary>
		public uint dwImageCount;
		public WIMResourceHeaderDiskShort rhOffsetTable;
		public WIMResourceHeaderDiskShort rhXmlData;
		public WIMResourceHeaderDiskShort rhBootMetadata;
		public uint dwBootIndex;
		public WIMResourceHeaderDiskShort rhIntegrity;
		public byte[/*60*/] bUnused;
	}
}
