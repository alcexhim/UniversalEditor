using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ISO.Internal
{
    internal struct PrimaryVolumeDescriptor
    {
        /// <summary>
        /// Always 0x00.
        /// </summary>
        public byte unused1;
        /// <summary>
        /// The name of the system that can act upon sectors 0x00-0x0F for the volume.
        /// </summary>
        public string systemName;
        /// <summary>
        /// Identification of this volume.
        /// </summary>
        public string volumeName;
        /// <summary>
        /// All zeroes.
        /// </summary>
        public ulong unused2;
        /// <summary>
        /// Number of Logical Blocks in which the volume is recorded.
        /// </summary>
        public uint volumeSpaceSize;
        /// <summary>
        /// All zeroes.
        /// </summary>
        public byte[] unused3;
        /// <summary>
        /// The size of the set in this logical volume (number of disks).
        /// </summary>
        public ushort volumeSetSize;
        /// <summary>
        /// The number of this disk in the Volume Set.
        /// </summary>
        public ushort volumeSequenceNumber;
        /// <summary>
        /// The size in bytes of a logical block. NB: This means that a logical block on a CD could be something
        /// other than 2 KiB!
        /// </summary>
        public ushort logicalBlockSize;
        /// <summary>
        /// The size in bytes of the path table.
        /// </summary>
        public uint pathTableSize;
        /// <summary>
        /// LBA location of the path table. The path table pointed to contains only little-endian values.
        /// </summary>
        public uint pathTableLocationTypeL;
        /// <summary>
        /// LBA location of the optional path table. The path table pointed to contains only little-endian values.
        /// Zero means that no optional path table exists.
        /// </summary>
        public uint optionalPathTableLocationTypeL;
        /// <summary>
        /// LBA location of the path table. The path table pointed to contains only big-endian values.
        /// </summary>
        public uint pathTableLocationTypeM;
        /// <summary>
        /// LBA location of the optional path table. The path table pointed to contains only big-endian values. Zero
        /// means that no optional path table exists.
        /// </summary>
        public uint optionalPathTableLocationTypeM;

        /// <summary>
        /// Note that this is not an LBA address, it is the actual Directory Record, which contains a zero-length
        /// Directory Identifier, hence the fixed 34 byte size.
        /// </summary>
        public byte[] rootDirectoryEntry;

        /// <summary>
        /// Identifier of the volume set of which this volume is a member.
        /// </summary>
        public string volumeSet;
        /// <summary>
        /// The volume publisher. For extended publisher information, the first byte should be 0x5F, followed by the
        /// filename of a file in the root directory. If not specified, all bytes should be 0x20.
        /// </summary>
        public string publisher;
        /// <summary>
        /// The identifier of the person(s) who prepared the data for this volume. For extended preparation
        /// information, the first byte should be 0x5F, followed by the filename of a file in the root directory. If
        /// not specified, all bytes should be 0x20.
        /// </summary>
        public string dataPreparer;
        /// <summary>
        /// Identifies how the data are recorded on this volume. For extended information, the first byte should be
        /// 0x5F, followed by the filename of a file in the root directory. If not specified, all bytes should be
        /// 0x20.
        /// </summary>
        public string application;
        /// <summary>
        /// Filename of a file in the root directory that contains copyright information for this volume set. If not
        /// specified, all bytes should be 0x20.
        /// </summary>
        public string copyrightFile;
        /// <summary>
        /// Filename of a file in the root directory that contains abstract information for this volume set. If not
        /// specified, all bytes should be 0x20.
        /// </summary>
        public string abstractFile;
        /// <summary>
        /// Filename of a file in the root directory that contains bibliographic information for this volume set. If
        /// not specified, all bytes should be 0x20.
        /// </summary>
        public string bibliographicFile;

        /// <summary>
        /// The date and time of when the volume was created.
        /// </summary>
        public DateTime timestampVolumeCreation;
        /// <summary>
        /// The date and time of when the volume was modified.
        /// </summary>
        public DateTime timestampVolumeModification;
        /// <summary>
        /// The date and time after which this volume is considered to be obsolete. If not specified, then the
        /// volume is never considered to be obsolete.
        /// </summary>
        public DateTime timestampVolumeExpiration;
        /// <summary>
        /// The date and time after which the volume may be used. If not specified, the volume may be used
        /// immediately.
        /// </summary>
        public DateTime timestampVolumeEffective;
        /// <summary>
        /// The directory records and path table version (always 0x01).
        /// </summary>
        public byte fileStructureVersion;
        /// <summary>
        /// Always 0x00.
        /// </summary>
        public byte unused4;
        /// <summary>
        /// 512 bytes. Contents not defined by ISO 9660.
        /// </summary>
        public byte[] applicationSpecific;
        /// <summary>
        /// 653 bytes. Reserved by ISO.
        /// </summary>
        public byte[] reserved;
    }
}
