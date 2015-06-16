using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal struct HFSPlusExtentDescriptor
	{
		public uint startBlock;
		public uint blockCount;
	}
}
