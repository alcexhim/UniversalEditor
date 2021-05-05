using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.DHA
{
    /// <summary>
    /// The DHA format is capable of loading/saving a FileSystemObjectModel for my visual novel projects. It stands for "Denys Hernandez Archive" ;)
    /// </summary>
    public class DHADataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Studio A.L.C.E. DHA archive", new byte?[][] { new byte?[] { (byte)'D', (byte)'H', (byte)'A', (byte)'!' } }, new string[] { "*.dha" });
            }
            return _dfr;
        }

        private int mvarVersion = 1;
        public int Version { get { return mvarVersion; } set { mvarVersion = value; } }

        private int mvarCommentLength = 256;
        private int mvarFolderNameLength = 256;
        private int mvarFileNameLength = 256;


        private struct DHAFILE
        {
            public string Name;
            public int Xor;
            public int FileOffset;
            public int UncompressedFileSize;
            public int CompressedFileSize;
            public Compression.CompressionMethod CompressionMethod;

            public byte[] FileData;
        }
        private struct DHAFOLDER
        {
            public string Name;

            public List<DHAFILE> Files;
            public List<DHAFOLDER> Folders;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            string DHA = br.ReadFixedLengthString(4); // DHA!
            if (DHA != "DHA!") throw new InvalidDataFormatException("File does not begin with \"DHA!\"");

            mvarVersion = br.ReadInt32();
            mvarCommentLength = br.ReadInt32();
            mvarFileNameLength = br.ReadInt32();
            mvarFolderNameLength = br.ReadInt32();

            // file starts with a 64-byte string
            fsom.Title = br.ReadFixedLengthString(mvarCommentLength);

            int folderCount = br.ReadInt32();
            int fileCount = br.ReadInt32();

            List<DHAFOLDER> folders = new List<DHAFOLDER>();
            List<DHAFILE> files = new List<DHAFILE>();

            // then comes the initial folder record
            for (int i = 0; i < folderCount; i++)
            {
                DHAFOLDER folder = ReadFolderIndexRecord(br);
                folders.Add(folder);
            }
            // then come the initial file records
            for (int i = 0; i < fileCount; i++)
            {
                DHAFILE file = ReadFileIndexRecord(br);
                files.Add(file);
            }

            foreach (DHAFOLDER folder in folders)
            {
                Folder folder1 = ProcessFolderIndexRecord(folder);
                fsom.Folders.Add(folder1);
            }
            foreach (DHAFILE file in files)
            {
                File file1 = ProcessFileIndexRecord(file);
                fsom.Files.Add(file1);
            }
        }

        private File ProcessFileIndexRecord(DHAFILE file)
        {
            File fileX = new File();
            fileX.Name = file.Name;
            fileX.SetDataAsByteArray(file.FileData);
            return fileX;
        }
        private Folder ProcessFolderIndexRecord(DHAFOLDER folder)
        {
            Folder folderX = new Folder();
            folderX.Name = folder.Name;
            foreach (DHAFOLDER folder1 in folder.Folders)
            {
                Folder folderY = ProcessFolderIndexRecord(folder1);
                folderX.Folders.Add(folderY);
            }
            foreach (DHAFILE file in folder.Files)
            {
                File fileY = ProcessFileIndexRecord(file);
                folderX.Files.Add(fileY);
            }
            return folderX;
        }

        private DHAFILE ReadFileIndexRecord(IO.BinaryReader br)
        {
            DHAFILE file = new DHAFILE();
            file.Name = br.ReadFixedLengthString(mvarFileNameLength);
            if (file.Name.Contains('\0')) file.Name = file.Name.Substring(0, file.Name.IndexOf('\0'));

            file.Xor = br.ReadInt32();
            file.FileOffset = br.ReadInt32();
            file.CompressedFileSize = br.ReadInt32();
            file.UncompressedFileSize = br.ReadInt32();
            file.CompressionMethod = (Compression.CompressionMethod)br.ReadInt32();

            long pos = br.BaseStream.Position;
            br.BaseStream.Position = file.FileOffset;
            byte[] fileData = br.ReadBytes(file.CompressedFileSize);

            fileData = Compression.CompressionStream.Decompress(file.CompressionMethod, fileData);

            for (int i = 0; i < fileData.Length; i++)
            {
                fileData[i] = (byte)(fileData[i] ^ file.Xor);
            }

            file.FileData = fileData;
            br.BaseStream.Position = pos;
            return file;
        }
        private DHAFOLDER ReadFolderIndexRecord(IO.BinaryReader br)
        {
            DHAFOLDER folder = new DHAFOLDER();
            folder.Name = br.ReadFixedLengthString(mvarFolderNameLength);
            if (folder.Name.Contains('\0')) folder.Name = folder.Name.Substring(0, folder.Name.IndexOf('\0'));

            int FolderCount = br.ReadInt32();
            int FileCount = br.ReadInt32();

            folder.Folders = new List<DHAFOLDER>();
            for (int i = 0; i < FolderCount; i++)
            {
                DHAFOLDER folder1 = ReadFolderIndexRecord(br);
                folder.Folders.Add(folder1);
            }
            folder.Files = new List<DHAFILE>();
            for (int i = 0; i < FileCount; i++)
            {
                DHAFILE file = ReadFileIndexRecord(br);
                folder.Files.Add(file);
            }
            return folder;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            bw.WriteFixedLengthString("DHA!");

            bw.Write(mvarVersion);

            bw.Write(mvarCommentLength);
            bw.Write(mvarFileNameLength);
            bw.Write(mvarFolderNameLength);

            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            bw.WriteFixedLengthString(fsom.Title, mvarCommentLength);

            bw.Write(fsom.Folders.Count);
            bw.Write(fsom.Files.Count);

            int HeaderRecordLength = mvarCommentLength + 28;
            int FileRecordLength = mvarFileNameLength + 20;
            int FolderRecordLength = mvarFolderNameLength + 8;
            int FileOffset = HeaderRecordLength + (CountFoldersRecursive(fsom.Folders) * FolderRecordLength) + (CountFilesRecursive(fsom.Folders) * FileRecordLength) + (fsom.Files.Count * FileRecordLength);

            List<byte[]> FileData = new List<byte[]>();

            foreach (Folder folder in fsom.Folders)
            {
                DHAFOLDER folder1 = ProcessFolderIndexRecord(folder, ref FileOffset, ref FileData);
                WriteFolderIndexRecord(bw, folder1);
            }
            foreach (File file in fsom.Files)
            {
                DHAFILE file1 = ProcessFileIndexRecord(file, ref FileOffset, ref FileData);
                WriteFileIndexRecord(bw, file1);
            }

            foreach (byte[] data in FileData)
            {
                bw.Write(data);
            }
        }

        private int CountFilesRecursive(Folder.FolderCollection folders)
        {
            int i = 0;
            foreach (Folder folder in folders)
            {
                i += CountFilesRecursive(folder.Folders);
                i += folder.Files.Count;
            }
            return i;
        }
        private int CountFoldersRecursive(Folder.FolderCollection folders)
        {
            int i = folders.Count;
            foreach (Folder folder in folders)
            {
                i += CountFoldersRecursive(folder.Folders);
            }
            return i;
        }

        private DHAFOLDER ProcessFolderIndexRecord(Folder folder, ref int FileOffset, ref List<byte[]> FileDatas)
        {
            DHAFOLDER folderX = new DHAFOLDER();
            folderX.Name = folder.Name;
			folderX.Folders = new List<DHAFOLDER>();
			folderX.Files = new List<DHAFILE>();

            foreach (Folder folder1 in folder.Folders)
            {
                folderX.Folders.Add(ProcessFolderIndexRecord(folder1, ref FileOffset, ref FileDatas));
            }
            foreach (File file1 in folder.Files)
            {
                folderX.Files.Add(ProcessFileIndexRecord(file1, ref FileOffset, ref FileDatas));
            }
            return folderX;
        }

        private Random rnd = new Random();
        private DHAFILE ProcessFileIndexRecord(File file, ref int FileOffset, ref List<byte[]> FileDatas)
        {
            DHAFILE fileX = new DHAFILE();
            fileX.Name = file.Name;
            fileX.Xor = rnd.Next(100, 255);
            fileX.FileOffset = FileOffset;

            byte[] fileData = file.GetDataAsByteArray();
            for (int i = 0; i < fileData.Length; i++)
            {
				fileData[i] = (byte)(fileData[i] ^ fileX.Xor);
            }

            fileX.CompressionMethod = Compression.CompressionMethod.Deflate;
            byte[] fileCompressedData = Compression.CompressionStream.Compress(fileX.CompressionMethod, fileData);

            if (fileCompressedData.Length > fileData.Length)
            {
                fileX.CompressionMethod = Compression.CompressionMethod.None;
                fileX.FileData = fileData;
                fileX.CompressedFileSize = fileData.Length;
                fileX.UncompressedFileSize = fileData.Length;
            }
            else
            {
                fileX.FileData = fileCompressedData;
                fileX.CompressedFileSize = fileCompressedData.Length;
                fileX.UncompressedFileSize = fileData.Length;
            }

            FileDatas.Add(fileX.FileData);
            FileOffset += fileX.FileData.Length;

            return fileX;
        }

        private void WriteFileIndexRecord(IO.BinaryWriter bw, DHAFILE file)
        {
            bw.WriteFixedLengthString(file.Name, mvarFileNameLength);
            bw.Write(file.Xor);
            bw.Write(file.FileOffset);
            bw.Write(file.CompressedFileSize);
            bw.Write(file.UncompressedFileSize);
            bw.Write((int)file.CompressionMethod);
        }
        private void WriteFolderIndexRecord(IO.BinaryWriter bw, DHAFOLDER folder)
        {
            bw.WriteFixedLengthString(folder.Name, mvarFolderNameLength);
            bw.Write(folder.Folders.Count);
            bw.Write(folder.Files.Count);

            foreach (DHAFOLDER folder1 in folder.Folders)
            {
                WriteFolderIndexRecord(bw, folder1);
            }
            foreach (DHAFILE file1 in folder.Files)
            {
                WriteFileIndexRecord(bw, file1);
            }
        }
    }
}
