using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.Plugins.UnrealEngine;

namespace UniversalEditor.DataFormats.UnrealEngine.Installer
{
    public class UMODDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Unreal Tournament UMOD installer", new byte?[][] { new byte?[] { 0xA3, 0xC5, 0xE3, 0x9F } }, new string[] { "*.umod" });
                _dfr.Filters[0].MagicByteOffsets = new int[] { -20 };
				_dfr.Sources.Add("http://wiki.beyondunreal.com/Legacy:UMOD/File_Format");
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            Reader br = base.Accessor.Reader;
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            // The UMOD file "header" is 20 bytes long. The header is stored in the last 20 bytes of
            // the file (hence the quotes around the term "header").
            br.Accessor.Seek(-20, SeekOrigin.End);

            // Magic number. Used to verify this file as a UMOD installer. Always 0x9FE3C5A3.
            uint magic = br.ReadUInt32();
            if (magic != 0x9FE3C5A3) throw new InvalidDataFormatException("Footer does not begin with 0x9FE3C5A3");

            // Byte offset of file directory in the UMOD file. (See below.)
            uint fileDirectoryOffset = br.ReadUInt32();

            // Total byte size of the UMOD file.
            uint fileSize = br.ReadUInt32();

            // UMOD file version.
            uint formatVersion = br.ReadUInt32();

            // CRC32 checksum over the file content.
            uint checksum = br.ReadUInt32();

            // The file directory describes the files stored in the first part of the UMOD file. Its
            // byte offset in the UMOD file is given in the file "header" (see above).
            br.Accessor.Seek(fileDirectoryOffset, SeekOrigin.Begin);

            // The directory consists of an index-type file count (the index data type is described
            // below), followed by variable-size records, each describing one file in the UMOD
            // installer.
            int fileCount = br.ReadINDEX();

            for (int i = 0; i < fileCount; i++)
            {
                // Length of file name (including trailing null byte).
                int fileNameLength = br.ReadINDEX();

                // File name, with trailing null byte.
                string fileName = br.ReadNullTerminatedString();

                // Byte offset of file in UMOD file.
                uint offset = br.ReadUInt32();
                // Byte length of file.
                uint length = br.ReadUInt32();


                // Bit field describing file flags.

                // Mychaeel: Feel free to investigate and contribute information about the file flags.
                // (I know that they have to be set to 0x03 for Manifest.ini and Manifest.int to
                // prevent those files from being copied to the user's System directory, and set to
                // 0x00 for all other files.)
                UMODFileFlags flags = (UMODFileFlags)br.ReadUInt32();

                File file = new File();
                file.Name = fileName;
                file.Size = length;
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                fsom.Files.Add(file);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            Writer bw = null;

            #region File data
            MemoryAccessor msData = new MemoryAccessor();
            bw = new Writer(msData);
            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());
            }
            bw.Close();

            byte[] data = msData.ToArray();
            #endregion
            #region File records
            uint fileOffset = 0;

			MemoryAccessor msDirectory = new MemoryAccessor();
            bw = new Writer(msDirectory);
            // The directory consists of an index-type file count (the index data type is described
            // below), followed by variable-size records, each describing one file in the UMOD
            // installer.
            bw.WriteINDEX(fsom.Files.Count);
            foreach (File file in fsom.Files)
            {
                // Length of file name (including trailing null byte).
                bw.WriteINDEX(file.Name.Length);

                // File name, with trailing null byte.
                bw.WriteNullTerminatedString(file.Name);

                // Byte offset of file in UMOD file.
                bw.WriteUInt32(fileOffset);
                // Byte length of file.
                bw.WriteUInt32((uint)file.Size);


                // Bit field describing file flags.

                // Mychaeel: Feel free to investigate and contribute information about the file flags.
                // (I know that they have to be set to 0x03 for Manifest.ini and Manifest.int to
                // prevent those files from being copied to the user's System directory, and set to
                // 0x00 for all other files.)
                UMODFileFlags flags = UMODFileFlags.None;
                bw.WriteUInt32((uint)flags);
                fileOffset += (uint)file.Size;
            }
            bw.Close();

            byte[] directoryData = msDirectory.ToArray();
            #endregion
            #region Header data
            // The UMOD file "header" is 20 bytes long. The header is stored in the last 20 bytes of
            // the file (hence the quotes around the term "header").
            MemoryAccessor msHeader = new MemoryAccessor();
            bw = new Writer(msHeader);

            // Magic number. Used to verify this file as a UMOD installer. Always 0x9FE3C5A3.
            bw.WriteUInt32((uint)0x9FE3C5A3);

            // Byte offset of file directory in the UMOD file. (See below.)
            uint fileDirectoryOffset = (uint)data.Length;
            bw.WriteUInt32(fileDirectoryOffset);

            // Total byte size of the UMOD file.
            uint fileSize = 20;
            bw.WriteUInt32(fileSize);

            // UMOD file version.
            uint formatVersion = 0;
            bw.WriteUInt32(formatVersion);

            // CRC32 checksum over the file content.
            uint checksum = 0;
            bw.WriteUInt32(checksum);

            bw.Flush();
            bw.Close();

            byte[] headerData = msHeader.ToArray();
            #endregion

            bw = base.Accessor.Writer;

            bw.WriteBytes(data);
            bw.WriteBytes(directoryData);
            bw.WriteBytes(headerData);
            bw.Flush();
        }
    }
}
