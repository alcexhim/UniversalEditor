using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Shortcut.Microsoft
{
	[Flags()]
	public enum LNKLocationFlags
	{
		None = 0x00000000,
		/// <summary>
		/// The linked file is on a volume. If set, the volume information and the local path
		/// contain data.
		/// </summary>
		VolumeIDAndLocalBasePath = 0x0001,
		/// <summary>
		/// The linked file is on a network share. If set, the network share information and
		/// common path contain data.
		/// </summary>
		/// <remarks>
		/// Although [MS-SHLLINK] states that when the CommonNetworkRelativeLinkAndPathSuffix
		/// location flag is not set the offset to the network share information should be zero,
		/// the value can still be set, but is not necessarily valid. This behavior was seen on
		/// Windows 95.
		/// </remarks>
		CommonNetworkRelativeLinkAndPathSuffix = 0x0002
	}
}
