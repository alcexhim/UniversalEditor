using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal struct HFSPlusForkData
	{
		public ulong logicalSize;
		public uint clumpSize;
		public uint totalBlocks;
		public HFSPlusExtentDescriptor[] extents;
	}
}
