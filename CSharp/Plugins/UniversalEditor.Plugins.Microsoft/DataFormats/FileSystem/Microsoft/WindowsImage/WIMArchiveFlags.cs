using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	[Flags()]
	public enum WIMArchiveFlags : uint
	{
		None = 0x00000000,
		Reserved = 0x00000001,
		/// <summary>
		/// Resources within the WIM (both file and metadata) are compressed.
		/// </summary>
		Compressed = 0x00000002,
		/// <summary>
		/// The contents of this WIM should not be changed.
		/// </summary>
		ReadOnly = 0x00000004,
		/// <summary>
		/// Resource data specified by the images within this WIM may be contained in another WIM.
		/// </summary>
		Spanned = 0x00000008,
		/// <summary>
		/// This WIM contains file resources only.  It does not contain any file metadata.
		/// </summary>
		ResourceOnly = 0x00000010,
		/// <summary>
		/// This WIM contains file metadata only.
		/// </summary>
		MetadataOnly = 0x00000020,
		/// <summary>
		/// Limits one writer to the WIM file when opened with the WIM_FLAG_SHARE_WRITE mode. This flag is primarily used in the Windows Deployment Services (WDS) scenario.
		/// </summary>
		WriteInProgress = 0x00000040,
		ReparsePointFixup = 0x00000080,
		CompressReserved = 0x00010000,
		/// <summary>
		/// When <see cref="WIMArchiveFlags.Compressed" /> is set, resources within the wim are compressed using XPRESS compression.
		/// </summary>
		CompressedXpress = 0x00020000,
		/// <summary>
		/// When <see cref="WIMArchiveFlags.Compressed" /> is set, resources within the wim are compressed using LZX compression.
		/// </summary>
		CompressedLZX = 0x00040000
	}
}
