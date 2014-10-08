using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Checksum.Modules.CRC32;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG
{
	public class EGGDataFormat : DataFormat
	{
		private DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("ALTools egg", new byte?[][] { new byte?[] { (byte)'E', (byte)'G', (byte)'G', (byte)'A' } }, new string[] { "*.egg" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;
			string EGGA = br.ReadFixedLengthString(4);
			if (EGGA != "EGGA") throw new InvalidOperationException();
			br.Accessor.Position -= 4;

			List<Internal.FileInfo> fileinfos = new List<Internal.FileInfo>();
			Internal.FileInfo currentFile = null;

			while (!br.EndOfStream)
			{
				uint chunkType = br.ReadUInt32();
				switch (chunkType)
				{
					#region Archive Header
					case 0x41474745: // EGGA
					{
						short version = br.ReadInt16();

						uint programID = br.ReadUInt32();       // 0xde5e1152
						uint reserved1 = br.ReadUInt32();
						break;
					}
					#endregion
					#region File Header
					case 0x0A8590E3:
					{
						// file header
						currentFile = new Internal.FileInfo();

						uint fileID = br.ReadUInt32();
						currentFile.length = br.ReadUInt64();

						fileinfos.Add(currentFile);
						break;
					}
					#endregion
					#region File Name Header
					case 0x0A8591AC:
					{
						// filename header
						ALZipFileNameFlags flag = (ALZipFileNameFlags)br.ReadByte();
						if ((flag & ALZipFileNameFlags.Encrypted) == ALZipFileNameFlags.Encrypted)
						{
							// encrypted
						}
						if ((flag & ALZipFileNameFlags.UseAreaCode) == ALZipFileNameFlags.RelativePath)
						{
							// use area code (as oppose to UTF-8)
						}
						if ((flag & ALZipFileNameFlags.RelativePath) == ALZipFileNameFlags.RelativePath)
						{
							// path is relative (as oppose to absolute)
						}

						ushort size = br.ReadUInt16();
						if ((flag & ALZipFileNameFlags.UseAreaCode) == ALZipFileNameFlags.UseAreaCode)
						{
							short locale = br.ReadInt16();
						}

						if ((flag & ALZipFileNameFlags.RelativePath) == ALZipFileNameFlags.RelativePath)
						{
							uint parentID = br.ReadUInt32();
						}

						currentFile.name = br.ReadFixedLengthString(size);
						break;
					}
					#endregion
					#region File Attributes
					case 0x2C86950B:
					{
						byte flag = br.ReadByte();
						ushort size = br.ReadUInt16();

						// 100-nanosecond time since the epoch 00:00:00 UTC, 1601-01-01
						long lastModifiedTimestamp = br.ReadInt64();

						ALZipFileAttributeFlags attribute = (ALZipFileAttributeFlags)br.ReadByte();
						break;
					}
					#endregion
					#region Solid Compression
					case 0x24E5A060:
					{
						byte bitFlag = br.ReadByte();
						short size = br.ReadInt16();
						break;
					}
					#endregion
					#region Block Header
					case 0x02B50C13:
					{
						// 0 - store, 1 - deflate, 2 - bzip2, 3 - AZO, 4 - LZMA
						ALZipCompressionMethod compressionMethod = (ALZipCompressionMethod)br.ReadByte();
						byte hint = br.ReadByte();

						uint decompressedSize = br.ReadUInt32();
						uint compressedSize = br.ReadUInt32();
						uint crc32 = br.ReadUInt32(); // 0x85f0cac8

						uint endOfBlock = br.ReadUInt32();
						if (endOfBlock != 0x08E28222) throw new InvalidDataFormatException("End of block header not found");


						currentFile.blocks.Add(new Internal.BlockInfo(compressionMethod, hint, decompressedSize, compressedSize, crc32, br.Accessor.Position));
						br.Accessor.Position += compressedSize;
						break;
					}
					#endregion
					#region End of Block
					case 0x08E28222:
					{
						// end of block
						break;
					}
					#endregion
					#region Extra Field
					default:
					{
						// general purpose bit flag
						byte flag = br.ReadByte();

						int size = 0;
						if ((flag & 0x1) == 0x1)
						{
							size = br.ReadInt32();
						}
						else
						{
							size = br.ReadInt16();
						}

						byte[] data = br.ReadBytes(size);

						break;
					}
					#endregion
				}
			}

			foreach (Internal.FileInfo fi in fileinfos)
			{
				File file = new File();
				file.Name = fi.name;
				file.Size = (long)fi.length;
				file.Properties.Add("fileinfo", fi);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			IO.Reader br = (IO.Reader)file.Properties["reader"];
			Internal.FileInfo fi = (Internal.FileInfo)file.Properties["fileinfo"];

			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);

			foreach (Internal.BlockInfo block in fi.blocks)
			{
				br.Accessor.Seek(block.offset, SeekOrigin.Begin);

				byte[] compressedData = br.ReadBytes(block.compressedSize);
				byte[] decompressedData = null;
				switch (block.compressionMethod)
				{
					case ALZipCompressionMethod.Store:
					{
						decompressedData = compressedData;
						break;
					}
					case ALZipCompressionMethod.Deflate:
					{
						decompressedData = CompressionModules.Deflate.Decompress(compressedData);
						break;
					}
					case ALZipCompressionMethod.Bzip2:
					{
						decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Bzip2).Decompress(compressedData);
						break;
					}
					case ALZipCompressionMethod.AZO:
					{
						break;
					}
					case ALZipCompressionMethod.LZMA:
					{
						decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.LZMA).Decompress(compressedData);
						break;
					}
				}

				CRC32ChecksumModule module = new CRC32ChecksumModule();
				uint crc32 = block.crc32;
				uint crc32_real = (uint)module.Calculate(decompressedData);

				if (crc32 != crc32_real)
				{
					// TODO: display a warning to the user?
				}

				bw.WriteBytes(decompressedData);
			}

			bw.Close();
			e.Data = ma.ToArray();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;

			#region EGG header
			{
				bw.WriteFixedLengthString("EGGA");
				short version = 1;
				bw.WriteInt16(version);

				uint programID = 0xde5e1152;
				bw.WriteUInt32(programID);

				uint reserved1 = 0;
				bw.WriteUInt32(reserved1);
			}
			#endregion
			#region End Header
			{
				bw.WriteInt32((int)0x08E28222);

			}
			#endregion

			List<Internal.FileInfo> fileinfos = new List<Internal.FileInfo>();
			foreach (File file in fsom.Files)
			{
				Internal.FileInfo fi = new Internal.FileInfo();
				fi.name = file.Name;
				fi.length = (ulong)file.Size;

				byte[] decompressedData = file.GetDataAsByteArray();
				byte[] compressedData = CompressionModules.Bzip2.Compress(decompressedData);

				Checksum.Modules.CRC32.CRC32ChecksumModule module = new Checksum.Modules.CRC32.CRC32ChecksumModule();
				uint crc32 = (uint)module.Calculate(decompressedData);
				fi.blocks.Add(new Internal.BlockInfo(ALZipCompressionMethod.Bzip2, 1, (uint)fi.length, (uint)compressedData.Length, crc32, 0, compressedData));
				fileinfos.Add(fi);
			}

			uint fileID = 0;
			foreach (Internal.FileInfo fi in fileinfos)
			{
				#region File Header
				{
					bw.WriteInt32((int)0x0A8590E3);

					bw.WriteUInt32(fileID);
					bw.WriteUInt64(fi.length);

					fileID++;
				}
				#endregion
				#region File Name header
				{
					bw.WriteInt32((int)0x0A8591AC);

					// filename header
					ALZipFileNameFlags flag = ALZipFileNameFlags.None;
					bw.WriteByte((byte)flag);

					if ((flag & ALZipFileNameFlags.Encrypted) == ALZipFileNameFlags.Encrypted)
					{
						// encrypted
					}
					if ((flag & ALZipFileNameFlags.UseAreaCode) == ALZipFileNameFlags.RelativePath)
					{
						// use area code (as oppose to UTF-8)
					}
					if ((flag & ALZipFileNameFlags.RelativePath) == ALZipFileNameFlags.RelativePath)
					{
						// path is relative (as oppose to absolute)
					}

					bw.WriteUInt16((ushort)fi.name.Length);

					if ((flag & ALZipFileNameFlags.UseAreaCode) == ALZipFileNameFlags.UseAreaCode)
					{
						short locale = 0;
						bw.WriteInt16(locale);
					}

					if ((flag & ALZipFileNameFlags.RelativePath) == ALZipFileNameFlags.RelativePath)
					{
						uint parentID = 0;
						bw.WriteUInt32(parentID);
					}

					bw.WriteFixedLengthString(fi.name);
				}
				#endregion
				#region End Header
				{
					bw.WriteInt32((int)0x08E28222);

				}
				#endregion
				#region Blocks
				{
					foreach (Internal.BlockInfo bi in fi.blocks)
					{
						bw.WriteInt32((int)0x02B50C13); // block header

						// 0 - store, 1 - deflate, 2 - bzip2, 3 - AZO, 4 - LZMA
						bw.WriteByte((byte)bi.compressionMethod);
						bw.WriteByte(bi.hint);

						bw.WriteUInt32(bi.decompressedSize);
						bw.WriteUInt32(bi.compressedSize);
						bw.WriteUInt32(bi.crc32);

						bw.WriteUInt32((uint)0x08E28222);

						bw.WriteBytes(bi.compressedData);
					}
				}
				#endregion
			}

			#region End Header
			{
				bw.WriteInt32((int)0x08E28222);

			}
			#endregion
		}
	}
}
