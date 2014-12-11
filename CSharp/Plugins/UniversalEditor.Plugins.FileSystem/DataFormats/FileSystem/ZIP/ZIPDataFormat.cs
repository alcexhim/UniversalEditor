using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UniversalEditor;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.Common;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	// TODO: Fix the ZIP data format's saving!!!


	public class ZIPDataFormat : DataFormat
	{
		private ZIPSettings mvarSettings = new ZIPSettings();

		private string mvarComment = String.Empty;
		public string Comment { get { return mvarComment; } set { mvarComment = value; } }

		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("PKWARE ZIP archive", new byte?[][] { new byte?[] { 80, 0x4b } }, new string[] { "*.zip", "*.zipx", "*.zipfs", "*.pk3", "*.pk4", "*.scs" /*, "*.xpi", "*.maff", "*.lwtp", "*.fwtp" */ });
			dfr.ContentTypes.Add("application/zip");
			dfr.ExportOptions.Add(new CustomOptionText("Comment", "&Comment: ", String.Empty, Int16.MaxValue));
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			int firstFile = this.zip_FindFirstFileOffset(br);
			if (firstFile == -1)
			{
				throw new InvalidDataFormatException();
			}
			br.Accessor.Seek((long) firstFile, SeekOrigin.Begin);

			while (!br.EndOfStream)
			{
				File file = null;
				try
				{
					file = (zip_ReadFile(br) as File);
					if (file != null)
					{
						if (file.Name.Contains("/"))
						{
							// file is a directory entry or has directory entries defined, create the directory if it does not exist
							string[] dirs = file.Name.Split('/');

							Folder currentDir = null;
							if (String.IsNullOrEmpty(dirs[dirs.Length - 1])) continue;

							foreach (string dir in dirs)
							{
								if (Array.IndexOf(dirs, dir) < dirs.Length - 1)
								{
									if (currentDir != null)
									{
										if (!currentDir.Folders.Contains(dir))
										{
											currentDir = currentDir.Folders.Add(dir);
										}
										else
										{
											currentDir = currentDir.Folders[dir];
										}
									}
									else
									{
										if (!fsom.Folders.Contains(dir))
										{
											currentDir = fsom.Folders.Add(dir);
										}
										else
										{
											currentDir = fsom.Folders[dir];
										}
									}
								}
								else
								{
									if (!String.IsNullOrEmpty(dir))
									{
										file.Name = dir;
										currentDir.Files.Add(file);
									}
								}
							}
						}
						else
						{
							fsom.Files.Add(file);
						}
					}
					else
					{
						break;
					}
				}
				catch (System.Security.SecurityException)
				{
					// file is encrypted, what do we do?
				}
			}
		}

		private bool readCentralDirectory(IO.Reader br)
		{
			int ctdir = this.seekCentralDirectory(br);
			if (ctdir == -1)
			{
				return false;
			}
			br.Accessor.Seek((long) ctdir, SeekOrigin.Begin);
			byte[] siggy = br.ReadBytes(4);
			short s1 = br.ReadInt16();
			short s2 = br.ReadInt16();
			short s3 = br.ReadInt16();
			short s4 = br.ReadInt16();
			short s5 = br.ReadInt16();
			short fileSiggy = br.ReadInt16();
			short fileSiggy2 = br.ReadInt16();
			short fileSiggy3 = br.ReadInt16();
			short fileSiggy4 = br.ReadInt16();
			int packedFileLength = br.ReadInt32();
			int unpackedFileLength = br.ReadInt32();
			int dumy3 = br.ReadInt32();
			int dummy4 = br.ReadInt32();
			int dummy5 = br.ReadInt32();
			int dummy6 = br.ReadInt32();
			short dummy7 = br.ReadInt16();
			return true;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			Dictionary<File, int> relativeOffsetsOfLocalHeaders = new Dictionary<File, int>();

			File[] files = fsom.GetAllFiles();
			IO.Writer bw = base.Accessor.Writer;
			foreach (File file in files)
			{
				relativeOffsetsOfLocalHeaders.Add(file, (int)bw.Accessor.Position);
				// signature first
				bw.WriteBytes(new byte[] { 80, 0x4b, 3, 4 });

				short iMinimumVersionNeededToExtract = 0x14;
				bw.WriteInt16(iMinimumVersionNeededToExtract);

				ZIPGeneralPurposeFlags iGeneralPurposeBitFlag = ZIPGeneralPurposeFlags.None;
				bw.WriteInt16((short)iGeneralPurposeBitFlag);

				// If bit 3 (0x08) of the general-purpose flags field is set, then the CRC-32 and file sizes are not known when the header is written.
				// The fields in the local header are filled with zero, and the CRC-32 and size are appended in a 12-byte structure (optionally preceded
				// by a 4-byte signature) immediately after the compressed data:

				short compressionMethod = 0;
				CompressionMethod _compressionMethod = CompressionMethod.Deflate;
				switch (_compressionMethod)
				{
					case CompressionMethod.Deflate: compressionMethod = 8; break;
					case CompressionMethod.Deflate64: compressionMethod = 9; break;
					case CompressionMethod.Bzip2: compressionMethod = 12; break;
					case CompressionMethod.LZMA: compressionMethod = 14; break;
				}
				bw.WriteInt16(compressionMethod);

				short iFileLastModificationTime = (short)(DateTime.Now.ToFileTime());
				bw.WriteInt16(iFileLastModificationTime);

				short iFileLastModificationDate = (short)(DateTime.Now.ToFileTime() >> 2);
				bw.WriteInt16(iFileLastModificationDate);

				byte[] uncompressedData = file.GetDataAsByteArray();

				bool isEncrypted = false;
				int iCRC32 = (int)(new UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule()).Calculate(uncompressedData);
				bw.WriteInt32(iCRC32);

				byte[] compressedData = CompressionModule.FromKnownCompressionMethod(_compressionMethod).Compress(file.GetDataAsByteArray());
				bw.WriteInt32((int)compressedData.Length);
				bw.WriteInt32((int)uncompressedData.Length);

				short fileNameLength = (short)file.Name.Length;
				short extraFieldLength = 0;
				bw.WriteInt16(fileNameLength);
				bw.WriteInt16(extraFieldLength);
				bw.WriteFixedLengthString(file.Name, fileNameLength);
				/*
				long pos = br.Accessor.Position;
				while (br.Accessor.Position < (pos + extraFieldLength))
				{
					short chunkIDCode = br.ReadInt16();
					short chunkLength = br.ReadInt16();
					byte[] data = br.ReadBytes(chunkLength);
				}
				*/
				bw.WriteBytes(compressedData);
				if (isEncrypted)
				{
					throw new System.Security.SecurityException("File is encrypted");
				}
			}

			long ofs = bw.Accessor.Position;
			// write the central directory
			foreach (File file in files)
			{
				bw.WriteBytes(new byte[] { (byte)'P', (byte)'K', 0x01, 0x02 });

				ZIPCreationPlatform creationPlatform = ZIPCreationPlatform.WindowsNTFS; // Windows NTFS
				byte formatVersion = 0x3F;
				short u = BitConverter.ToInt16(new byte[] { (byte)creationPlatform, formatVersion }, 0);
				bw.WriteInt16(u);

				short iMinimumVersionNeededToExtract = 0x14;
				bw.WriteInt16(iMinimumVersionNeededToExtract);
				ZIPGeneralPurposeFlags iGeneralPurposeBitFlag = ZIPGeneralPurposeFlags.None;
				bw.WriteInt16((short)iGeneralPurposeBitFlag);

				short compressionMethod = 0;
				CompressionMethod _compressionMethod = CompressionMethod.Deflate;
				switch (_compressionMethod)
				{
					case CompressionMethod.Deflate: compressionMethod = 8; break;
					case CompressionMethod.Deflate64: compressionMethod = 9; break;
					case CompressionMethod.Bzip2: compressionMethod = 12; break;
					case CompressionMethod.LZMA: compressionMethod = 14; break;
				}
				bw.WriteInt16(compressionMethod);

				short iFileLastModificationTime = (short)(DateTime.Now.ToFileTime());
				bw.WriteInt16(iFileLastModificationTime);

				short iFileLastModificationDate = (short)(DateTime.Now.ToFileTime() >> 2);
				bw.WriteInt16(iFileLastModificationDate);

				bool isEncrypted = false;
				byte[] uncompressedData = file.GetDataAsByteArray();
				int iCRC32 = (int)(new UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule()).Calculate(uncompressedData);
				bw.WriteInt32(iCRC32);

				byte[] compressedData = CompressionModule.FromKnownCompressionMethod(_compressionMethod).Compress(file.GetDataAsByteArray());
				bw.WriteInt32((int)compressedData.Length);
				bw.WriteInt32((int)uncompressedData.Length);

				short fileNameLength = (short)file.Name.Length;
				
				byte[] extraField = new byte[0];

				bw.WriteInt16(fileNameLength);
				bw.WriteInt16((short)extraField.Length);

				string fileComment = String.Empty;

				bw.WriteInt16((short)fileComment.Length);
				short diskNumber = 0;
				bw.WriteInt16(diskNumber);
				ZIPInternalFileAttributes internalFileAttributes = ZIPInternalFileAttributes.None;
				bw.WriteInt16((short)internalFileAttributes);

				int externalFileAttributes = 0;
				bw.WriteInt32(externalFileAttributes);

				bw.WriteInt32(relativeOffsetsOfLocalHeaders[file]);

				bw.WriteFixedLengthString(file.Name.Replace("\\", "/"), fileNameLength);

				bw.WriteBytes(extraField);

				bw.WriteFixedLengthString(fileComment);
			}
			long centralDirectoryLength = (bw.Accessor.Position - ofs);

			#region End of Central Directory
			{
				bw.WriteBytes(new byte[] { 0x50, 0x4B, 0x05, 0x06 });

				// The number of this disk (containing the end of central
				// directory record)
				bw.WriteInt16(0);

				// Number of the disk on which the central directory starts
				bw.WriteInt16(0);

				// The number of central directory entries on this disk
				bw.WriteInt16((short)files.Length);

				// Total number of entries in the central directory. 
				bw.WriteInt16((short)files.Length);

				// Size of the central directory in bytes
				bw.WriteInt32((int)centralDirectoryLength);

				// Offset of the start of the central directory on the disk on
				// which the central directory starts
				bw.WriteInt32((int)ofs);

				// The length of the following comment field
				bw.WriteInt16((short)mvarComment.Length);

				// Optional comment for the Zip file
				bw.WriteFixedLengthString(mvarComment);
			}
			#endregion
		}

		private void RecursiveLoadFolder(Folder folder, ref Dictionary<string, File> files, string parentFolderName)
		{
			foreach (Folder folder1 in folder.Folders)
			{
				if (String.IsNullOrEmpty(parentFolderName))
				{
					RecursiveLoadFolder(folder1, ref files, folder.Name);
				}
				else
				{
					RecursiveLoadFolder(folder1, ref files, parentFolderName + "/" + folder.Name);
				}
			}
			foreach (File file in folder.Files)
			{
				if (String.IsNullOrEmpty(parentFolderName))
				{
					files.Add(folder.Name + "/" + file.Name, file);
				}
				else
				{
					files.Add(parentFolderName + "/" + folder.Name + "/" + file.Name, file);
				}
			}
		}

		private int seekCentralDirectory(IO.Reader br)
		{
			byte[] signature = new byte[] { 0x3f, 0x7c, 80, 0x4b };
			byte[] siggy = new byte[4];
			for (int l = 0; l < (br.Accessor.Length - 4L); l++)
			{
				br.Seek((long) l, SeekOrigin.Begin);
				br.Read(siggy, 0, 4);
				if (signature.Match(siggy))
				{
					return l;
				}
			}
			return -1;
		}

		private int zip_FindFirstFileOffset(IO.Reader br)
		{
			byte[] vFileEntrySignal = new byte[] { 80, 0x4b, 3, 4 };
			while (br.Accessor.Length != br.Accessor.Position)
			{
				byte[] vFileEntrySignal1 = br.ReadBytes(vFileEntrySignal.Length);
				if (vFileEntrySignal1.Match(vFileEntrySignal))
				{
					return (((int)br.Accessor.Position) - vFileEntrySignal.Length);
				}
			}
			return -1;
		}

		private File zip_ReadFile(IO.Reader br)
		{
			byte[] unpackedData;
			CompressionMethod method = CompressionMethod.Deflate;
			byte[] vaLocalFileHeaderSignatureCmp = new byte[] { 80, 0x4b, 3, 4 };
			if (br.Remaining < 4) return null;
			
			byte[] vaLocalFileHeaderSignatureCmp1 = br.ReadBytes(4);
			if (!vaLocalFileHeaderSignatureCmp1.Match(vaLocalFileHeaderSignatureCmp))
			{
				return null;
			}
			short iMinimumVersionNeededToExtract = br.ReadInt16();
			short iGeneralPurposeBitFlag = br.ReadInt16();
			switch (br.ReadInt16())
			{
				case 8:
				{
					method = CompressionMethod.Deflate;
					break;
				}
				case 9:
				{
					method = CompressionMethod.Deflate64;
					break;
				}
				case 12:
				{
					method = CompressionMethod.Bzip2;
					break;
				}
				case 14:
				{
					method = CompressionMethod.LZMA;
					break;
				}
			}
			short iFileLastModificationTime = br.ReadInt16();
			short iFileLastModificationDate = br.ReadInt16();
			bool isEncrypted = false;
			int iCRC32 = br.ReadInt32();
			int packedFileLength = br.ReadInt32();
			int unpackedFileLength = br.ReadInt32();
			short fileNameLength = br.ReadInt16();
			short extraFieldLength = br.ReadInt16();
			string fileName = br.ReadFixedLengthString(fileNameLength);
			long pos = br.Accessor.Position;
			while (br.Accessor.Position < (pos + extraFieldLength))
			{
				short chunkIDCode = br.ReadInt16();
				short chunkLength = br.ReadInt16();
				byte[] data = br.ReadBytes(chunkLength);
			}
			byte[] packedData = br.ReadBytes(packedFileLength);
			if (isEncrypted)
			{
				throw new System.Security.SecurityException("File is encrypted");
			}
			if (packedFileLength == unpackedFileLength)
			{
				unpackedData = packedData;
			}
			else
			{
				unpackedData = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(method).Decompress(packedData);
			}

			if (unpackedData.Length != unpackedFileLength)
			{
				HostApplication.Messages.Add(HostApplicationMessageSeverity.Error, "File size mismatch, source archive may be corrupted", fileName);
			}

			File f = new File();
			f.SetDataAsByteArray(unpackedData);
			f.Name = fileName;
			return f;
		}

		public ZIPSettings Settings { get { return mvarSettings; } }
	}
}

