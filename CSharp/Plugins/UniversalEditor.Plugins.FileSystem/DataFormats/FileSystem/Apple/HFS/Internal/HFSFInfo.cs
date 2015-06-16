using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal enum HFSFInfoFlags : short
	{
		Inited = 0x0100,
		Locked = 0x1000,
		Invisible = 0x4000
	}
	internal struct HFSFInfo
	{
		// hfs_finfo
		public string type;
		public string creator;
		public HFSFInfoFlags fdFlags;
		public HFSPoint location;
		public short parentFolderID;
	}
}
