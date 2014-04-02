using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
    public class RARDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Eugene Roshal WinRAR archive", new byte?[][] { new byte?[] { (byte)'R', (byte)'a', (byte)'r', (byte)'!' } }, new string[] { "*.rar" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;

            #region marker block
            string Rar = br.ReadFixedLengthString(4);
            if (Rar != "Rar!") throw new InvalidDataFormatException("File does not begin with \"Rar!\"");

            ushort a10 = br.ReadUInt16();
            byte a11 = br.ReadByte();
            if (a10 != 0x071A || a11 != 0x00) throw new InvalidDataFormatException("Invalid block header");
            #endregion

            #region archive header
            {
                ushort head_crc = br.ReadUInt16();
                RARHeaderType head_type = (RARHeaderType)br.ReadByte();
                RARHeaderFlags head_flags = (RARHeaderFlags)br.ReadUInt16();

                ushort head_size = br.ReadUInt16();
                ushort reserved1 = br.ReadUInt16();
                uint reserved2 = br.ReadUInt32();
            }
            #endregion

            #region File Entry
            while (br.Accessor.Position + 3 < br.Accessor.Length)
            {
                ushort head_crc = br.ReadUInt16();
                RARHeaderType head_type = (RARHeaderType)br.ReadByte();
                RARFileHeaderFlags head_flags = (RARFileHeaderFlags)br.ReadUInt16();

                ushort head_size = br.ReadUInt16();

                if (br.EndOfStream) break;

                uint compressedSize = br.ReadUInt32();
                uint decompressedSize = br.ReadUInt32();
                RARHostOperatingSystem hostOS = (RARHostOperatingSystem)br.ReadByte();
                uint fileCRC = br.ReadUInt32();
                uint dateTimeDOS = br.ReadUInt32();

                // Version number is encoded as 10 * Major version + minor version.
                byte requiredVersionToUnpack = br.ReadByte();

                RARCompressionMethod compressionMethod = (RARCompressionMethod)br.ReadByte();
                ushort fileNameSize = br.ReadUInt16();
                uint fileAttributes = br.ReadUInt32();

                if ((head_flags & RARFileHeaderFlags.SupportLargeFiles) == RARFileHeaderFlags.SupportLargeFiles)
                {
                    // High 4 bytes of 64 bit value of compressed file size.
                    uint highPackSize = br.ReadUInt32();
                    // High 4 bytes of 64 bit value of uncompressed file size.
                    uint highUnpackSize = br.ReadUInt32();
                }

                string filename = br.ReadFixedLengthString(fileNameSize);
                byte nul = br.ReadByte();

                if ((head_flags & RARFileHeaderFlags.EncryptionSaltPresent) == RARFileHeaderFlags.EncryptionSaltPresent)
                {
                    long salt = br.ReadInt64();
                }

                if ((head_flags & RARFileHeaderFlags.ExtendedTimeFieldPresent) == RARFileHeaderFlags.ExtendedTimeFieldPresent)
                {
                    uint exttime = br.ReadUInt32();

                }

                byte[] compressedData = br.ReadBytes(compressedSize);

                byte[] decompressedData = compressedData;

                fsom.Files.Add(filename, decompressedData);
            }
            #endregion
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
