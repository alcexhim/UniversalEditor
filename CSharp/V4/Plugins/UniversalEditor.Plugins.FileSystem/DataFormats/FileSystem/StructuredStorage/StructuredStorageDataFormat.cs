using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.DataFormats.FileSystem.StructuredStorage.Internal;

namespace UniversalEditor.DataFormats.FileSystem.StructuredStorage
{
    public class StructuredStorageDataFormat : DataFormat
    {


        private const int SECID_FREESECT = unchecked((int)0xFFFFFFFF);
        private const int SECID_ENDOFCHAIN = unchecked((int)0xFFFFFFFE);
        private const int SECID_FATSECT = unchecked((int)0xFFFFFFFD);
        private const int SECID_DIFSECT = unchecked((int)0xFFFFFFFC);

        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("COM/OLE Structured Storage compound document format", new byte?[][] { new byte?[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } }, new string[] { "*.cfs", "*.ole", "*.nss" });
            return dfr;
        }

        private Version mvarVersion = new Version(0x0003, 0x003E);
        public Version Version
        {
            get { return mvarVersion; }
            set
            {
                mvarVersion = value;
                if (!mvarSectorShiftSet)
                {
                    switch (mvarVersion.Major)
                    {
                        case 3:
                        {
                            mvarSectorShift = 9;
                            break;
                        }
                        case 4:
                        {
                            mvarSectorShift = 12;
                            break;
                        }
                    }
                }
            }
        }

		private readonly byte[] OLE_CFS_SIGNATURE = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

        private bool mvarSectorShiftSet = false;
        private ushort mvarSectorShift = 9;
        public ushort SectorShift { get { return mvarSectorShift; } set { mvarSectorShift = value; mvarSectorShiftSet = true; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            BinaryReader br = base.Stream.BinaryReader;

			byte[] signature = br.ReadBytes(OLE_CFS_SIGNATURE.Length);
			for (int i = 0; i < signature.Length; i++)
			{
				if (signature[i] != OLE_CFS_SIGNATURE[i])
				{
					throw new InvalidDataFormatException("Invalid OLE structured storage file");
				}
			}

            fsom.ID = br.ReadGuid();

            ushort minorVersion = br.ReadUInt16(); // 0x003E
            ushort majorVersion = br.ReadUInt16(); // 0x0003
            mvarVersion = new Version(minorVersion, majorVersion);

			if (mvarVersion.Major != 3 && mvarVersion.Major != 4)
			{
				throw new NotSupportedException("Unsupported binary file format version: " + mvarVersion.Major.ToString() + "; only supports version(s) 3 and 4");
			}

            // 0xFE 0xFF = Little-Endian 0xFF 0xFE = Big-Endian
            ushort byteOrder = br.ReadUInt16(); // 0xFFFE

            // Size of a sector in the compound document file (➜3.1) in power-of-two (ssz), real sector
            // size is sec_size = 2ssz bytes (minimum value is 7 which means 128 bytes, most used 
            // value is 9 which means 512 bytes)
            ushort sectorShift = br.ReadUInt16(); // 9
            //  9 when Version = 3, 12 when Version = 4

            // Size of a short-sector in the short-stream container stream (➜6.1) in power-of-two (sssz),
            // real short-sector size is short_sec_size = 2sssz bytes (maximum value is sector size
            // ssz, see above, most used value is 6 which means 64 bytes)
            ushort miniSectorShift = br.ReadUInt16(); // 6

            byte[] unused = br.ReadBytes(6);

            // Total number of sectors used Directory 
            int directorySectorsNumber = br.ReadInt32();
        
            // Total number of sectors used for the sector allocation table (➜5.2)
            int fatSectorsNumber = br.ReadInt32();

            // SecID of first sector of the directory stream
            int firstDirectorySectorID = br.ReadInt32(); // Sector.ENDOFCHAIN

            uint unused2 = br.ReadUInt32();

            // Minimum size of a standard stream (in bytes, minimum allowed and most used size is 4096
            // bytes), streams with an actual size smaller than (and not equal to) this value are stored as
            // short-streams (➜6)
            uint minimumSizeOfStandardStream = br.ReadUInt32(); // 4096

            // SecID of first sector of the short-sector allocation table (➜6.2), or –2 (End Of Chain
            // SecID, ➜3.1) if not extant
            int firstMiniFATSectorID = br.ReadInt32(); // unchecked((int)0xFFFFFFFE)

            // Total number of sectors used for the short-sector allocation table (➜6.2)
            uint miniFATSectorsNumber = br.ReadUInt32();

            // SecID of first sector of the master sector allocation table (➜5.1), or –2 (End Of Chain
            // SecID, ➜3.1) if no additional sectors used
            int firstDIFATSectorID = br.ReadInt32(); // Sector.ENDOFCHAIN
        
			//72 4 Total number of sectors used for the master sector allocation table (➜5.1)
			uint difatSectorsNumber = br.ReadUInt32();
			
			//76 436 First part of the master sector allocation table (➜5.1) containing 109 SecIDs
			int[] difat = new int[109];
			for (int i = 0; i < difat.Length; i++)
			{
				difat[i] = br.ReadInt32();
			}

			if (majorVersion == 4)
			{
				byte[] zeroHead = br.ReadBytes(3584);
			}


            int sectorSize = (2 << (sectorShift - 1));
            int difatEntriesCount = ((sectorSize / 4) - 1);
            int fatEntriesCount = (sectorSize / 4);


		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            BinaryWriter bw = base.Stream.BinaryWriter;
			
			bw.Write(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 });
            bw.Write(fsom.ID);

			bw.Write((ushort)mvarVersion.Minor);
			bw.Write((ushort)mvarVersion.Major);

            // 0xFE 0xFF = Little-Endian 0xFF 0xFE = Big-Endian
			switch (bw.Endianness)
			{
				case Endianness.LittleEndian:
				{
					bw.Write(new byte[] { 0xFE, 0xFF });
					break;
				}
				case Endianness.BigEndian:
				{
					bw.Write(new byte[] { 0xFF, 0xFE });
					break;
				}
			}

            ushort sectorShift = 9; // 9
			if (mvarVersion.Major == 4)
			{
				sectorShift = 12;
			}
			else if (mvarVersion.Major == 3)
			{
				sectorShift = 9;
			}
			bw.Write(sectorShift);

            // Size of a short-sector in the short-stream container stream (➜6.1) in power-of-two (sssz),
            // real short-sector size is short_sec_size = 2sssz bytes (maximum value is sector size
            // ssz, see above, most used value is 6 which means 64 bytes)
            ushort miniSectorShift = 6; // 6
			bw.Write(miniSectorShift);

            byte[] unused = new byte[6];
			bw.Write(unused);

            // Total number of sectors used Directory 
            int directorySectorsNumber = 0;
            if (mvarVersion.Major == 4)
            {
                // directorySectorsNumber = directorySectors.Count;
            }
			bw.Write(directorySectorsNumber);
        
            // Total number of sectors used for the sector allocation table (➜5.2)
            int fatSectorsNumber = 0;
			bw.Write(fatSectorsNumber);

            // SecID of first sector of the directory stream
            int firstDirectorySectorID = SECID_ENDOFCHAIN;
			bw.Write(firstDirectorySectorID);

            uint unused2 = 0;
			bw.Write(unused2);

            // Minimum size of a standard stream (in bytes, minimum allowed and most used size is 4096
            // bytes), streams with an actual size smaller than (and not equal to) this value are stored as
            // short-streams (➜6)
            uint minimumSizeOfStandardStream = 4096;
			bw.Write(minimumSizeOfStandardStream);

            // SecID of first sector of the short-sector allocation table (➜6.2), or –2 (End Of Chain
            // SecID, ➜3.1) if not extant
            int firstMiniFATSectorID = 0; // unchecked((int)0xFFFFFFFE)
			bw.Write(firstMiniFATSectorID);

            // Total number of sectors used for the short-sector allocation table (➜6.2)
            uint miniFATSectorsNumber = 0;
			bw.Write(miniFATSectorsNumber);

            // SecID of first sector of the master sector allocation table (➜5.1), or –2 (End Of Chain
            // SecID, ➜3.1) if no additional sectors used
            int firstDIFATSectorID = 0; // Sector.ENDOFCHAIN
			bw.Write(firstDIFATSectorID);
        
			//72 4 Total number of sectors used for the master sector allocation table (➜5.1)
			uint difatSectorsNumber = 0;
			bw.Write(difatSectorsNumber);
			
			//76 436 First part of the master sector allocation table (➜5.1) containing 109 SecIDs
			int[] difat = new int[109];
			for (int i = 0; i < difat.Length; i++)
			{
				bw.Write(difat[i]);
			}

			if (mvarVersion.Major == 4)
			{
				byte[] zeroHead = new byte[3584];
				bw.Write(zeroHead);
            }

            List<StructuredStorageDirectoryEntry> entries = new List<StructuredStorageDirectoryEntry>();
            
            // First add the root directory entry
            StructuredStorageDirectoryEntry entryRootEntry = new StructuredStorageDirectoryEntry("Root Entry", StructuredStorageDirectoryType.Root, UniversalEditor.Common.RBTree.RBTreeColor.Black, -1, -1, (fsom.Folders.Count > 0) ? 1 : -1, Guid.NewGuid(), 0, DateTime.Now, DateTime.Now, -2, 0);
            entries.Add(entryRootEntry);

            // Now add directory entries for each of the folders in the collection
            foreach (Folder folder in fsom.Folders)
            {
                RecursiveCreateDirectoryEntry(ref entries, folder, fsom.Folders.IndexOf(folder));
            }
            // And for the files...
            foreach (File file in fsom.Files)
            {
            }

            // Now write the directory entries to the file
            foreach (StructuredStorageDirectoryEntry entry in entries)
            {
                WriteDirectoryEntry(bw, entry);
            }



            bw.Flush();
		}

        private void RecursiveCreateDirectoryEntry(ref List<StructuredStorageDirectoryEntry> entries, Folder folder, int index)
        {
            int childIndex = -1;
            if (folder.Files.Count > 0 || folder.Folders.Count > 0)
            {
                childIndex = index + 2;
            }
        }

        private void WriteDirectoryEntry(BinaryWriter bw, StructuredStorageDirectoryEntry entry)
        {
            bw.WriteFixedLengthString(entry.Name, Encoding.Unicode, 64);
            
            ushort nameLength = (ushort)((entry.Name.Length * 2) + 2);
            bw.Write(nameLength);

            bw.Write((byte)entry.EntryType); // storage type: root
            bw.Write((byte)entry.rbTreeColor); // RBtree color: black
            bw.Write(entry.leftIndex);
            bw.Write(entry.rightIndex);
            bw.Write(entry.childIndex);
            bw.Write(entry.guid);
            bw.Write(entry.startSect);
            bw.Write(entry.creationDate.ToFileTime());
            bw.Write(entry.modifyDate.ToFileTime());
            bw.Write(entry.startSect);
            bw.Write(entry.size);
        }
    }
}
