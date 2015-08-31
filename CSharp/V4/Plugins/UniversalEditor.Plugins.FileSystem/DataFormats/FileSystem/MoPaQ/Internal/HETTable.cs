using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
    public partial class MPQDataFormat
    {
        private struct HETTable
        {
            public uint dwVersion;
            public uint dwDataSize;
            public uint dwTableSize;
            public uint dwMaxFileCount;
            public uint dwHashTableSize;
            public uint dwHashEntrySize;
            public uint dwTotalIndexSize; 
            public uint dwIndexSizeExtra; 
            public uint dwIndexSize;
            public uint dwBlockTableSize;

            public static HETTable Read(IO.Reader br)
            {
                // The structure of the HET table, as stored in the MPQ, is the following:

                // Common header, for both HET and BET tables
                string HET = br.ReadFixedLengthString(4); // 'HET\x1A'
                if (HET != "HET\x1A") throw new InvalidDataFormatException("Expected HET signature, \"HET\\x1A\" not found");

                HETTable het = new HETTable();
                het.dwVersion = br.ReadUInt32();                       // Version. Seems to be always 1
                het.dwDataSize = br.ReadUInt32();                      // Size of the contained table
                het.dwTableSize = br.ReadUInt32();                     // Size of the entire hash table, including the header (in bytes)
                het.dwMaxFileCount = br.ReadUInt32();                  // Maximum number of files in the MPQ
                het.dwHashTableSize = br.ReadUInt32();                 // Size of the hash table (in bytes)
                het.dwHashEntrySize = br.ReadUInt32();                 // Effective size of the hash entry (in bits)
                het.dwTotalIndexSize = br.ReadUInt32();                // Total size of file index (in bits)
                het.dwIndexSizeExtra = br.ReadUInt32();                // Extra bits in the file index
                het.dwIndexSize = br.ReadUInt32();                     // Effective size of the file index (in bits)
                het.dwBlockTableSize = br.ReadUInt32();                // Size of the block index subtable (in bytes)
                return het;
            }
        }
    }
}
