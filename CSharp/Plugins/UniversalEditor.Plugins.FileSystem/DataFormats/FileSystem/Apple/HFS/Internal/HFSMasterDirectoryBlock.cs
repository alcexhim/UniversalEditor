using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	internal struct HFSMasterDirectoryBlock
	{
		public int creationTimestamp;
		public int modificationTimestamp;
		public HFSVolumeAttributes volumeAttributes;
		public ushort rootDirectoryFileCount;
		public ushort volumeBitmapFirstBlockIndex;
		public short nextAllocationSearchStart;
		public ushort allocationBlockCount;
		public int allocationBlockSize;
		public int defaultClumpSize;
		public short firstAllocationBlockIndex;
		public int nextUnusedCatalogNodeID;
		public ushort unusedAllocationBlockCount;
		public int lastBackupTimestamp;
		public int clumpSizeForExtentsOverflowFile;
		public int clumpSizeForCatalogFile;
		public ushort rootDirectoryDirectoryCount;
		public int totalFileCount;
		public int totalDirectoryCount;
		public int[] finderInfo;
		public ushort volumeCacheBlockCount;
		public ushort volumeBitmapCacheBlockCount;
		public ushort commonVolumeCacheBlockCount;
		public HFSExtentDescriptor[] extentRecordsForExtentsOverflowFile;
		public HFSExtentDescriptor[] extentRecordsForCatalogFile;
	}
}
