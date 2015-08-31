using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal enum HFSDInfoFlags : short
	{
		Inited = 0x0100,
		Locked = 0x1000,
		Invisible = 0x4000
	}
	internal struct HFSDInfo
	{
		// hfs_finfo
		public HFSRect rect;
		public HFSDInfoFlags flags;
		public HFSPoint location;
		public short view;
	}
}
