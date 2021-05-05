using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.StudioALCE.RFS
{
    /// <summary>
    /// The RFS format is capable of loading/saving a FileSystemObjectModel for my visual novel projects. It stands
    /// for "RIFF File System"
    /// </summary>
    public class RFSDataFormat : RIFFDataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Filters.Add("Studio ALCE RIFF-based file system", new byte?[][] { new byte?[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F', null, null, null, null, (byte)'A', (byte)'R', (byte)'C', (byte)'H' } }, new string[] { "*.rfs" });
            }
            return _dfr;
        }

        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new ChunkedObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            ChunkedObjectModel riff = (objectModels.Pop() as ChunkedObjectModel);
            FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);

            RIFFGroupChunk chunkARCH = (riff.Chunks["ARCH"] as RIFFGroupChunk);
            if (chunkARCH == null) throw new InvalidDataFormatException("File does not contain an \"ARCH\" chunk");

            foreach (RIFFChunk chnk in chunkARCH.Chunks)
            {
                RIFFDataChunk chunk = (chnk as RIFFDataChunk);
                if (chunk == null) continue;

                switch (chunk.ID)
                {
                    case "FILE":
                    {
                        IO.BinaryReader br = new IO.BinaryReader(chunk.Data);
                        string FileName = br.ReadNullTerminatedString();
                        int blockIndex = br.ReadInt32();
                        int fileSize = chunkARCH.Chunks[blockIndex].Size - 4;

                        UniversalEditor.Compression.CompressionMethod compressionMethod = (UniversalEditor.Compression.CompressionMethod)br.ReadInt32();

                        File file = new File();
                        file.Name = FileName;
                        file.Size = fileSize;
                        file.Properties.Add("block", blockIndex);
                        file.Properties.Add("riff", riff);
                        file.Properties.Add("CompressionMethod", compressionMethod);
                        file.DataRequest += file_DataRequest;
                        fsom.Files.Add(file);
                        break;
                    }
                }
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            int blockIndex = (int)file.Properties["block"];
            ChunkedObjectModel riff = (ChunkedObjectModel)file.Properties["riff"];
            UniversalEditor.Compression.CompressionMethod compressionMethod = (UniversalEditor.Compression.CompressionMethod)file.Properties["CompressionMethod"];

            RIFFGroupChunk chunkARCH = (riff.Chunks["ARCH"] as RIFFGroupChunk);
            if (chunkARCH == null) return;

            RIFFDataChunk chunkDATA = (chunkARCH.Chunks[blockIndex] as RIFFDataChunk);
            if (chunkDATA.ID != "DATA") throw new InvalidOperationException("Could not find associated DATA chunk for this file");

            byte[] compressedData = chunkDATA.Data;
            byte[] decompressedData = UniversalEditor.Compression.CompressionStream.Decompress(compressionMethod, compressedData);
            e.Data = decompressedData;
        }

        private Dictionary<File, byte[]> compressedDataBlocks = new Dictionary<File, byte[]>();

        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);

            FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
            ChunkedObjectModel riff = new ChunkedObjectModel();

            RIFFGroupChunk chunkARCH = new RIFFGroupChunk();
            chunkARCH.TypeID = "RIFF";
            chunkARCH.ID = "ARCH";

            int nextDataBlockIndex = fsom.Files.Count;
            foreach (File file in fsom.Files)
            {
                WriteFileInfoChunk(file, ref nextDataBlockIndex, chunkARCH);
            }
            foreach (Folder folder in fsom.Folders)
            {
                WriteFolderInfoChunk(folder, ref nextDataBlockIndex, chunkARCH);
            }
            foreach (File file in fsom.Files)
            {
                WriteFileDataChunk(file, chunkARCH);
            }
            foreach (Folder folder in fsom.Folders)
            {
                WriteFolderDataChunk(folder, chunkARCH);
            }

            riff.Chunks.Add(chunkARCH);

            objectModels.Push(riff);
        }

        private void WriteFolderInfoChunk(Folder folder, ref int nextDataBlockIndex, RIFFGroupChunk parent)
        {
            foreach (File file in folder.Files)
            {
                WriteFileInfoChunk(file, ref nextDataBlockIndex, parent);
            }
            foreach (Folder folder1 in folder.Folders)
            {
                WriteFolderInfoChunk(folder1, ref nextDataBlockIndex, parent);
            }
        }

        private void WriteFileInfoChunk(File file, ref int nextDataBlockIndex, RIFFGroupChunk parent)
        {
            RIFFDataChunk chunkFILE = new RIFFDataChunk();
            chunkFILE.ID = "FILE";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            IO.BinaryWriter bw = new IO.BinaryWriter(ms);
            bw.WriteNullTerminatedString(file.Name);
            bw.Write(nextDataBlockIndex);

            UniversalEditor.Compression.CompressionMethod compressionMethod = Compression.CompressionMethod.Deflate;

            // try multiple compression methods
            SortedList<int, UniversalEditor.Compression.CompressionMethod> fileSizesAndTheirCompressionMethods = new SortedList<int, Compression.CompressionMethod>();
            byte[] compressedData = null;
            byte[] decompressedData = file.GetDataAsByteArray();

            compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Bzip2, decompressedData);
            fileSizesAndTheirCompressionMethods.Add(compressedData.Length, Compression.CompressionMethod.Bzip2);
            compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Deflate, decompressedData);
            fileSizesAndTheirCompressionMethods.Add(compressedData.Length, Compression.CompressionMethod.Deflate);
            compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Gzip, decompressedData);
            fileSizesAndTheirCompressionMethods.Add(compressedData.Length, Compression.CompressionMethod.Gzip);
            compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.None, decompressedData);
            fileSizesAndTheirCompressionMethods.Add(compressedData.Length, Compression.CompressionMethod.None);
            compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Zlib, decompressedData);
            fileSizesAndTheirCompressionMethods.Add(compressedData.Length, Compression.CompressionMethod.Zlib);

            IEnumerator<KeyValuePair<int, UniversalEditor.Compression.CompressionMethod>> enumerator = fileSizesAndTheirCompressionMethods.GetEnumerator();
            enumerator.MoveNext();

            compressionMethod = (Compression.CompressionMethod)enumerator.Current.Value;
            file.Properties.Add("CompressionMethod", compressionMethod);

            bw.Write((int)compressionMethod);

            bw.Close();
            chunkFILE.Data = ms.ToArray();

            parent.Chunks.Add(chunkFILE);
            nextDataBlockIndex++;
        }

        private void WriteFolderDataChunk(Folder folder, RIFFGroupChunk parent)
        {
            foreach (File file in folder.Files)
            {
                WriteFileDataChunk(file, parent);
            }
            foreach (Folder folder1 in folder.Folders)
            {
                WriteFolderDataChunk(folder1, parent);
            }
        }

        private void WriteFileDataChunk(File file, RIFFGroupChunk parent)
        {
            RIFFDataChunk chunkDATA = new RIFFDataChunk();
            chunkDATA.ID = "DATA";

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] decompressedData = file.GetDataAsByteArray();
            byte[] compressedData = UniversalEditor.Compression.CompressionStream.Compress((UniversalEditor.Compression.CompressionMethod)file.Properties["CompressionMethod"], decompressedData);
            ms.Write(compressedData);
            ms.Close();

            chunkDATA.Data = ms.ToArray();

            parent.Chunks.Add(chunkDATA);
        }
    }
}
