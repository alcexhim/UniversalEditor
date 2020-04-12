//
//  HFSMasterDirectoryBlock.cs - internal structure representing the master directory block for an HFS filesystem
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	/// <summary>
	/// Internal structure representing the master directory block for an HFS filesystem.
	/// </summary>
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
