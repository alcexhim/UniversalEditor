using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	public class FATExtendedBiosParameterBlock
	{
		private byte mvarPhysicalDriveNumber = 0;
		public byte PhysicalDriveNumber { get { return mvarPhysicalDriveNumber; } set { mvarPhysicalDriveNumber = value; } }

		private FATCheckDiskFlags mvarCheckDiskFlags = FATCheckDiskFlags.None;
        /// <summary>
        /// CHKDSK flags, bits 7-2 always cleared
        /// </summary>
		public FATCheckDiskFlags CheckDiskFlags { get { return mvarCheckDiskFlags; } set { mvarCheckDiskFlags = value; } }

        private FATExtendedBootSignature mvarExtendedBootSignature = new FATExtendedBootSignature();
        public FATExtendedBootSignature ExtendedBootSignature { get { return mvarExtendedBootSignature; } }
	}
}
