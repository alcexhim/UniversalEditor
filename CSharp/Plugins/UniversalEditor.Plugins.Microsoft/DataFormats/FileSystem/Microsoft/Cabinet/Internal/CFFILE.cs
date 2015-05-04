using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
    internal struct CFFILE
    {
        /// <summary>
        /// uncompressed size of this file in bytes
        /// </summary>
        public uint decompressedSize;
        /// <summary>
        /// uncompressed offset of this file in the folder
        /// </summary>
        public uint offset;
        /// <summary>
        /// index into the CFFOLDER area
        /// </summary>
        public ushort folderIndex;
        /// <summary>
        /// date stamp for this file
        /// </summary>
        public ushort date;
        /// <summary>
        /// time stamp for this file
        /// </summary>
        public ushort time;
        /// <summary>
        /// attribute flags for this file
        /// </summary>
        public ushort attribs;
        /// <summary>
        /// name of this file
        /// </summary>
        public string name;
    }
}
