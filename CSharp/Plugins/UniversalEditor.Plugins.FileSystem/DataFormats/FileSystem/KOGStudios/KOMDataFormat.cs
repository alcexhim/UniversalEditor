using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors.Stream;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.FileSystem.KOGStudios
{
    public class KOMDataFormat : DataFormat
    {
        private struct KOMFileEntry
        {
            public string FileName;
            public int CompressedSize;
            public int DecompressedSize;
            public int Checksum;
            public long Timestamp1;
            public int Algorithm;
        }

        private Version mvarVersion = new Version(0, 4);
        public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

        private int mvarInitialKey = 0xB290207;
        public int InitialKey { get { return mvarInitialKey; } set { mvarInitialKey = value; } }

        private string mvarPassword = "1846201835";
        public string Password { get { return mvarPassword; } set { mvarPassword = value; } }

        private string mvarFlavor = "GC TEAM MASSFILE";
        public string Flavor { get { return mvarFlavor; } set { mvarFlavor = value; } }

        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("KOG Studios archive", new byte?[][] { new byte?[] { (byte)'K', (byte)'O', (byte)'G', (byte)' ' } }, new string[] { "*.kom" });

                // _dfr.ImportOptions.Add(new ExportOptionNumber("InitialKey", "Initial key: ", 0xB290207));
                _dfr.ImportOptions.Add(new ExportOptionText("Password", "Password: ", "1846201835"));

                _dfr.ExportOptions.Add(new ExportOptionNumber("InitialKey", "Initial key: ", 0xB290207));
                _dfr.ExportOptions.Add(new ExportOptionText("Password", "Password: ", "1846201835"));
                _dfr.ExportOptions.Add(new ExportOptionText("Flavor", "Flavor: ", "GC TEAM MASSFILE", 16));
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            if (br.BaseStream.Length < 20) throw new InvalidDataFormatException("File must be greater than 20 bytes in length");

            string signature = br.ReadFixedLengthString(52);
            signature = signature.TrimNull();
            if (!signature.StartsWith("KOG ")) throw new InvalidDataFormatException("File does not begin with \"KOG \"");

            mvarFlavor = signature.Substring(4, 16);

            int fileCount = br.ReadInt32();

            long base_off = 0;

            string xml = String.Empty;
            
            #region Version-specific
            string version = signature.Substring(21);
            switch (version)
            {
                case "V.0.1.":
                case "V.0.2.":
                {
                    int dummy = br.ReadInt32();
                    base_off = br.BaseStream.Position + (fileCount * 0x48);
                    break;
                }
                case "V.0.3.":
                {
                    int dummy = br.ReadInt32();
                    string dummystr = br.ReadFixedLengthString(8);
                    int xmlsz = br.ReadInt32();
                    base_off = br.BaseStream.Position + xmlsz;
                    xml = br.ReadFixedLengthString(xmlsz);
                    break;
                }
                case "V.0.4.":
                {
                    int key = br.ReadInt32();
                    mvarInitialKey = key;

                    string dummystr = br.ReadFixedLengthString(8);
                    int xmlsz = br.ReadInt32();

                    base_off = br.BaseStream.Position;

                    if (key == 0xB290207)
                    {
                        key = 0x6E0ACDEB; // 64-bit number (%11d)
                    }

                    // byte[] bkey = BitConverter.GetBytes(key);
                    string password = mvarPassword;
                    byte[] sbkey = System.Text.Encoding.ASCII.GetBytes(password);

                    // SHA-1
                    System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                    byte[] hash = sha1.ComputeHash(sbkey);

                    //BF-ECB
                    UniversalEditor.Cryptography.BlowFish bf_ecb = new Cryptography.BlowFish(hash);

                    br.BaseStream.Position = base_off;
                    byte[] data = br.ReadBytes(xmlsz);

                    try
                    {
                        data = bf_ecb.Decrypt_ECB(data);
                    }
                    catch (Exception ex)
                    {
                    }

                    xml = System.Text.Encoding.UTF8.GetString(data);
                    break;
                }
            }
            #endregion
            #region File index table loading
            MarkupObjectModel mom = new MarkupObjectModel();
            ObjectModel om = mom;
            XMLDataFormat xdf = new XMLDataFormat();

            UniversalEditor.Accessors.Text.TextAccessor accessor = new Accessors.Text.TextAccessor(om, xdf);
            try
            {
                accessor.Open(xml);
                accessor.Load();
                accessor.Close();
            }
            catch (InvalidDataFormatException ex)
            {
                accessor.Close();
                throw new DataFormatOptionArgumentException("The password is invalid or the file is corrupted.  Please check that you provided the correct password and try again.");
            }

            MarkupTagElement tagFiles = (mom.Elements["Files"] as MarkupTagElement);
            if (tagFiles == null)
                throw new DataFormatOptionArgumentException("The password is invalid or the file is corrupted.  Please check that you provided the correct password and try again.");  //throw new InvalidDataFormatException();

            List<KOMFileEntry> entries = new List<KOMFileEntry>();

            foreach (MarkupElement el in tagFiles.Elements)
            {
                MarkupTagElement tag = (el as MarkupTagElement);
                if (tag == null) continue;

                if (tag.FullName != "File") continue;

                KOMFileEntry entry = new KOMFileEntry();

                entry.FileName = tag.Attributes["Name"].Value;
                entry.CompressedSize = Int32.Parse(tag.Attributes["CompressedSize"].Value);
                entry.DecompressedSize = Int32.Parse(tag.Attributes["Size"].Value);
                entry.Checksum = Int32.Parse(tag.Attributes["Checksum"].Value, System.Globalization.NumberStyles.HexNumber);

                entry.Timestamp1 = Int64.Parse(tag.Attributes["FileTime"].Value, System.Globalization.NumberStyles.HexNumber);
                // DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
                // DateTime Timestamp = DateTime.FromFileTimeUtc(timestamp1); // UnixEpoch.AddSeconds(timestamp1);

                entry.Algorithm = Int32.Parse(tag.Attributes["Algorithm"].Value);

                entries.Add(entry);
            }

            long offset = br.BaseStream.Position;
            foreach (KOMFileEntry entry in entries)
            {
                File file = new File();
                file.Name = entry.FileName;
                file.Size = entry.DecompressedSize;
                file.DataRequest += file_DataRequest;
                file.Properties.Add("reader", br);
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", entry.CompressedSize);

                br.BaseStream.Seek(entry.CompressedSize, System.IO.SeekOrigin.Current);
                offset += entry.CompressedSize;

                fsom.Files.Add(file);
            }
            #endregion
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.BinaryReader br = (IO.BinaryReader)file.Properties["reader"];
            long offset = (long)file.Properties["offset"];
            int length = (int)file.Properties["length"];
            
            br.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);

            byte[] data = br.ReadBytes(length);
            data = UniversalEditor.Compression.CompressionStream.Decompress(Compression.CompressionMethod.Zlib, data);
            e.Data = data;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            bw.WriteFixedLengthString("KOG " + mvarFlavor + " V." + mvarVersion.ToString(2) + ".", 52);

            bw.Write(fsom.Files.Count);

            switch (mvarVersion.Major)
            {
                case 0:
                {
                    switch (mvarVersion.Minor)
                    {
                        case 4:
                        {
                            bw.Write(mvarInitialKey);
                            bw.WriteFixedLengthString(String.Empty, 8);

                            #region XML file
                            byte[] bkey = BitConverter.GetBytes(mvarInitialKey);
                            string password = mvarPassword;
                            byte[] sbkey = System.Text.Encoding.ASCII.GetBytes(password);

                            // SHA-1
                            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                            byte[] hash = sha1.ComputeHash(sbkey);

                            System.IO.MemoryStream msXML = new System.IO.MemoryStream();
                            XMLDataFormat xdf = new XMLDataFormat();
                            MarkupObjectModel mom = new MarkupObjectModel();

                            List<byte[]> datas = new List<byte[]>();

                            MarkupTagElement tagFiles = new MarkupTagElement();
                            tagFiles.FullName = "Files";
                            foreach (File file in fsom.Files)
                            {
                                MarkupTagElement tag = new MarkupTagElement();
                                tag.FullName = "File";
                                tag.Attributes.Add("Name", file.Name);

                                byte[] decompressedData = file.GetDataAsByteArray();
                                byte[] compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Zlib, decompressedData);
                                datas.Add(compressedData);
                                
                                int checksum = 0;
                                long timestamp = 0;
                                int algorithm = 0;

                                tag.Attributes.Add("CompressedSize", compressedData.Length.ToString());
                                tag.Attributes.Add("Size", decompressedData.Length.ToString());
                                tag.Attributes.Add("Checksum", checksum.ToString());
                                tag.Attributes.Add("FileTime", timestamp.ToString());
                                tag.Attributes.Add("Algorithm", algorithm.ToString());
                                tagFiles.Elements.Add(tag);
                            }
                            mom.Elements.Add(tagFiles);

                            StreamAccessor.Save(msXML, mom, xdf, true);
                            #endregion

                            //BF-ECB
                            UniversalEditor.Cryptography.BlowFish bf_ecb = new Cryptography.BlowFish(hash);

                            byte[] xmldata = msXML.ToArray();
                            xmldata = bf_ecb.Encrypt_ECB(xmldata);
                            bw.Write(xmldata.Length);
                            bw.Write(xmldata);

                            foreach (byte[] data in datas)
                            {
                                bw.Write(data);
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            bw.Write(fsom.Files.Count);
        }
    }
}
