using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	public struct HFSExtentDescriptor
	{
		public short firstAllocationBlockIndex;
		public short allocationBlockCount;
	}
}
