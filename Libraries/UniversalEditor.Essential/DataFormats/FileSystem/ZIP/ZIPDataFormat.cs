//
//  ZIPDataFormat.cs - provides a DataFormat for manipulating archives in the standardized PKWARE ZIP format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.Compression;
using UniversalEditor.IO;

using UniversalEditor.DataFormats.FileSystem.ZIP.ExtraDataFields;
using MBS.Framework;
using UniversalEditor.UserInterface;

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	// TODO: Fix the ZIP data format's saving!!!

	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in the standardized PKWARE ZIP format.
	/// </summary>
	public class ZIPDataFormat : DataFormat
	{
		private static readonly byte[] SIG_CENTRAL_DIRECTORY_ENTRY = new byte[] { 0x50, 0x4B, 0x01, 0x02 };
		private static readonly byte[] SIG_DIRECTORY_ENTRY = new byte[] { 0x50, 0x4B, 0x03, 0x04 };
		private static readonly byte[] SIG_END_OF_CENTRAL_DIRECTORY = new byte[] { 0x50, 0x4B, 0x05, 0x06 };

		/// <summary>
		/// Gets or sets the comment written in the header of the ZIP archive.
		/// </summary>
		/// <value>The comment to be written in the header of the ZIP archive.</value>
		public string Comment { get; set; } = String.Empty;

		private long _offsetToBeginningOfZIPFile = 0;

		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			dfr.ContentTypes.Add("application/zip");
			dfr.ExportOptions.Add(new CustomOptionText(nameof(Comment), "_Comment", String.Empty, Int16.MaxValue));
			return dfr;
		}

		/// <summary>
		/// Loads the data from the <see cref="Accessor" /> into the given <see cref="FileSystemObjectModel" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> into which to load the data.</param>
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			br.Accessor.SavePosition();
			while (!br.EndOfStream)
			{
				byte[] siggy = br.ReadBytes(4);
				if (siggy.Match(SIG_DIRECTORY_ENTRY))
				{
					_offsetToBeginningOfZIPFile = br.Accessor.Position - 4;
					break;
				}
				else
				{
					br.Seek(-3, SeekOrigin.Current);
				}
			}
			br.Accessor.LoadPosition();

			long eocdOffset = zip_FindEndOfCentralDirectory(br);
			if (eocdOffset != -1)
			{
				// let's work with the central directory since it's more reliable than the file header
				Internal.ZIPCentralDirectoryFooter footer = ReadZIPCentralDirectoryFooter(br);
				if (footer.centralDirectoryOffset > 0 && footer.centralDirectoryOffset < br.Accessor.Length)
				{
					br.Seek(_offsetToBeginningOfZIPFile + footer.centralDirectoryOffset, SeekOrigin.Begin);
					long pos = br.Accessor.Position;

					while (br.Accessor.Position < footer.centralDirectoryLength + pos)
					{
						byte[] centralDirectorySignature = br.ReadBytes(4);
						if (!centralDirectorySignature.Match(SIG_CENTRAL_DIRECTORY_ENTRY))
						{
							Console.WriteLine("zip: @" + br.Accessor.Position.ToString() + " - invalid central directory entry, shit might get real now");
						}

						ushort unknown1 = br.ReadUInt16();
						ushort unknown2 = br.ReadUInt16();
						ushort unknown3 = br.ReadUInt16();
						ushort unknown4 = br.ReadUInt16();
						uint unknown5 = br.ReadUInt32();
						uint unknown6 = br.ReadUInt32();
						uint compressedLength = br.ReadUInt32();
						uint decompressedLength = br.ReadUInt32();
						ushort fileNameLength = br.ReadUInt16();        // oops... had this as Int32
						ushort extraFieldLength = br.ReadUInt16();
						ushort unknown7 = br.ReadUInt16();
						ushort unknown8 = br.ReadUInt16();
						ushort unknown9 = br.ReadUInt16();
						ushort unknown10 = br.ReadUInt16();
						ushort unknown11 = br.ReadUInt16();
						uint fileOffset = br.ReadUInt32();
						string fileName = br.ReadFixedLengthString(fileNameLength);

						long local_pos = br.Accessor.Position;
						while (br.Accessor.Position < (local_pos + extraFieldLength) && br.Accessor.Position < br.Accessor.Length)
						{
							short chunkIDCode = br.ReadInt16();
							short chunkLength = br.ReadInt16();
							byte[] data = br.ReadBytes(chunkLength);
						}

						File file = fsom.AddFile(fileName);
						file.Properties.Add("offset", fileOffset);
						file.Properties.Add("compressedLength", compressedLength);
						file.Properties.Add("decompressedLength", decompressedLength);
						file.Size = decompressedLength;
						file.DataRequest += File_DataRequest; // file sources are unreliable atm
						/*
						file.Source = new CompressedEmbeddedFileSource(br, fileOffset, decompressedLength, compressedLength, new FileSourceTransformation[]
						{
							new FileSourceTransformation(FileSourceTransformationType.Output, HandleFileSourceTransformationFunction)
						});
						*/
					}
					return;
				}
				else
				{
					// try from the beginning of the entire stream
					br.Accessor.Position = 0;
					Console.WriteLine("zip: central directory offset " + footer.centralDirectoryOffset.ToString() + " out of bounds for file (" + br.Accessor.Length.ToString() + ")");
				}
			}

			int firstFile = this.zip_FindFirstFileOffset(br);
			if (firstFile == -1)
			{
				throw new InvalidDataFormatException();
			}
			br.Accessor.Seek((long)firstFile, SeekOrigin.Begin);

			while (!br.EndOfStream)
			{
				File file = null;
				try
				{
					file = zip_ReadFile(br, fsom);

					if (file == null)
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

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			uint decompressedLength = (uint)file.Properties["decompressedLength"];
			uint compressedLength = (uint)file.Properties["compressedLength"];
			uint offset = (uint)file.Properties["offset"];
			offset += (uint)_offsetToBeginningOfZIPFile;

			Reader br = Accessor.Reader;
			long curpos = Accessor.Position;

			Accessor.Seek(offset, SeekOrigin.Begin);

			byte[] headerSignature = br.ReadBytes(4);
			if (!headerSignature.Match(new byte[] { 0x50, 0x4B, 0x03, 0x04 }))
			{

			}

			CompressionMethod method = CompressionMethod.None;
			short iMinimumVersionNeededToExtract = br.ReadInt16();
			short iGeneralPurposeBitFlag = br.ReadInt16();
			short iCompressionMethod = br.ReadInt16();
			switch (iCompressionMethod)
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
			short local_fileNameLength = br.ReadInt16();
			short extraFieldLength = br.ReadInt16();
			string local_fileName = br.ReadFixedLengthString(local_fileNameLength);
			long local_pos = br.Accessor.Position;
			while (br.Accessor.Position < (local_pos + extraFieldLength))
			{
				short chunkIDCode = br.ReadInt16();
				short chunkLength = br.ReadInt16();
				byte[] data = br.ReadBytes(chunkLength);
			}


			byte[] compressedData = br.ReadBytes(compressedLength);
			br.Accessor.Position = curpos;

			byte[] decompressedData = compressedData;
			if (compressedLength != decompressedLength)
			{
				decompressedData = UniversalEditor.Compression.CompressionModules.Deflate.Decompress(compressedData);
				if (decompressedData.Length != decompressedLength)
				{
					Console.WriteLine("zip: sanity check - decompressed data length (" + decompressedData.Length.ToString() + ") does not match expected value (" + decompressedLength.ToString() + ")");
				}
			}
			e.Data = decompressedData;
		}


		void HandleFileSourceTransformationFunction(object sender, System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			File file = (sender as File);

			long decompressedLength = (long)file.Properties["decompressedLength"];
			long compressedLength = (long)file.Properties["compressedLength"];

			Reader br = (Reader)file.Properties["reader"];
			long curpos = br.Accessor.Position;

			br.Accessor.Position = (long)file.Properties["offset"];

			byte[] headerSignature = br.ReadBytes(4);
			if (!headerSignature.Match(new byte[] { 0x50, 0x4B, 0x03, 0x04 }))
			{

			}

			CompressionMethod method = CompressionMethod.None;
			short iMinimumVersionNeededToExtract = br.ReadInt16();
			short iGeneralPurposeBitFlag = br.ReadInt16();
			short iCompressionMethod = br.ReadInt16();
			switch (iCompressionMethod)
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
			short local_fileNameLength = br.ReadInt16();
			short extraFieldLength = br.ReadInt16();
			string local_fileName = br.ReadFixedLengthString(local_fileNameLength);
			long local_pos = br.Accessor.Position;
			while (br.Accessor.Position < (local_pos + extraFieldLength))
			{
				short chunkIDCode = br.ReadInt16();
				short chunkLength = br.ReadInt16();
				byte[] data = br.ReadBytes(chunkLength);
			}


			byte[] compressedData = br.ReadBytes(compressedLength);
			br.Accessor.Position = curpos;

			byte[] decompressedData = compressedData;
			if (compressedLength != decompressedLength)
			{
				decompressedData = UniversalEditor.Compression.CompressionModules.Deflate.Decompress(compressedData);
				if (decompressedData.Length != decompressedLength)
				{
					Console.WriteLine("zip: sanity check - decompressed data length (" + decompressedData.Length.ToString() + ") does not match expected value (" + decompressedLength.ToString() + ")");
				}
			}
		}


		private static Internal.ZIPCentralDirectoryFooter ReadZIPCentralDirectoryFooter(Reader reader)
		{
			Internal.ZIPCentralDirectoryFooter item = new Internal.ZIPCentralDirectoryFooter();
			item.unknown1 = reader.ReadUInt32();
			item.unknown2 = reader.ReadUInt16();
			item.unknown3 = reader.ReadUInt16();
			item.centralDirectoryLength = reader.ReadUInt32();
			item.centralDirectoryOffset = reader.ReadUInt32();
			item.unknown4 = reader.ReadUInt16();
			return item;
		}

		private bool readCentralDirectory(IO.Reader br)
		{
			int ctdir = this.seekCentralDirectory(br);
			if (ctdir == -1)
			{
				return false;
			}
			br.Accessor.Seek((long)ctdir, SeekOrigin.Begin);
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

		/// <summary>
		/// Saves the <see cref="FileSystemObjectModel" /> data to the <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> that contains the data to save.</param>
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Dictionary<File, int> relativeOffsetsOfLocalHeaders = new Dictionary<File, int>();

			File[] files = fsom.GetAllFiles();
			IO.Writer bw = base.Accessor.Writer;
			foreach (File file in files)
			{
				relativeOffsetsOfLocalHeaders.Add(file, (int)bw.Accessor.Position);
				WriteLocalFileEntry(bw, file);
			}

			long ofs = bw.Accessor.Position;
			// write the central directory
			foreach (File file in files)
			{
				WriteCentralFileEntry(bw, file, relativeOffsetsOfLocalHeaders);
			}
			long centralDirectoryLength = (bw.Accessor.Position - ofs);
			WriteCentralDirectoryFooter(bw, 0, 0, files.Length, files.Length, centralDirectoryLength, ofs, Comment.Length, Comment);
		}

		/// <summary>
		/// Writes the End of Central Directory record (0x0605)
		/// </summary>
		/// <param name="bw">Bw.</param>
		/// <param name="centralDirectoryEndDiskNumber">Central directory end disk number.</param>
		/// <param name="centralDirectoryStartDiskNumber">Central directory start disk number.</param>
		/// <param name="diskCentralDirectoryFileCount">Disk central directory file count.</param>
		/// <param name="totalFileCount">Total file count.</param>
		/// <param name="centralDirectoryLength">Central directory length.</param>
		/// <param name="centralDirectoryOffset">Central directory offset.</param>
		/// <param name="commentLength">Comment length.</param>
		/// <param name="comment">Comment.</param>
		private void WriteCentralDirectoryFooter(Writer bw, short centralDirectoryEndDiskNumber, short centralDirectoryStartDiskNumber, int diskCentralDirectoryFileCount, int totalFileCount, long centralDirectoryLength, long centralDirectoryOffset, int commentLength, string comment)
		{
			bw.WriteBytes(new byte[] { 0x50, 0x4B, 0x05, 0x06 });

			// The number of this disk (containing the end of central
			// directory record)
			bw.WriteInt16(centralDirectoryEndDiskNumber);

			// Number of the disk on which the central directory starts
			bw.WriteInt16(centralDirectoryStartDiskNumber);

			// The number of central directory entries on this disk
			bw.WriteInt16((short)diskCentralDirectoryFileCount);

			// Total number of entries in the central directory.
			bw.WriteInt16((short)totalFileCount);

			// Size of the central directory in bytes
			bw.WriteInt32((int)centralDirectoryLength);

			// Offset of the start of the central directory on the disk on
			// which the central directory starts
			bw.WriteInt32((int)centralDirectoryOffset);

			// The length of the following comment field
			bw.WriteInt16((short)Comment.Length);

			// Optional comment for the Zip file
			bw.WriteFixedLengthString(Comment);
		}

		private void WriteCentralFileEntry(Writer bw, File file, Dictionary<File, int> relativeOffsetsOfLocalHeaders)
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
			CompressionMethod _compressionMethod = CompressionMethod.None;
			switch (_compressionMethod)
			{
				case CompressionMethod.Deflate: compressionMethod = 8; break;
				case CompressionMethod.Deflate64: compressionMethod = 9; break;
				case CompressionMethod.Bzip2: compressionMethod = 12; break;
				case CompressionMethod.LZMA: compressionMethod = 14; break;
			}
			bw.WriteInt16(compressionMethod);

			bw.WriteDOSFileTime(DateTime.Now);

			bool isEncrypted = false;
			byte[] uncompressedData = file.GetData();
			int iCRC32 = (int)(new UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule()).Calculate(uncompressedData);
			bw.WriteInt32(iCRC32);

			byte[] compressedData = CompressionModule.FromKnownCompressionMethod(_compressionMethod).Compress(file.GetData());
			bw.WriteInt32((int)compressedData.Length);
			bw.WriteInt32((int)uncompressedData.Length);

			short fileNameLength = (short)file.Name.Length;

			ZIPExtraDataField[] edfs = new ZIPExtraDataField[]
			{
				new ZIPExtraDataFieldExtendedTimestamp(DateTime.Now, DateTime.Now, DateTime.Now)
			};

			short extraFieldLength = 0;
			foreach (ZIPExtraDataField edf in edfs)
			{
				extraFieldLength += (short)(4 + edf.CentralData.Length);
			}

			bw.WriteInt16(fileNameLength);
			bw.WriteInt16(extraFieldLength);

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

			foreach (ZIPExtraDataField edf in edfs)
			{
				bw.WriteInt16(edf.TypeCode);
				bw.WriteInt16((short)edf.CentralData.Length);
				bw.WriteBytes(edf.CentralData);
			}

			bw.WriteFixedLengthString(fileComment);
		}

		private void WriteLocalFileEntry(Writer bw, IFileSystemObject item)
		{
			// signature first
			bw.WriteBytes(new byte[] { 0x50, 0x4b, 3, 4 });

			short iMinimumVersionNeededToExtract = 0x14;
			bw.WriteInt16(iMinimumVersionNeededToExtract);

			ZIPGeneralPurposeFlags iGeneralPurposeBitFlag = ZIPGeneralPurposeFlags.None;
			bw.WriteInt16((short)iGeneralPurposeBitFlag);

			// If bit 3 (0x08) of the general-purpose flags field is set, then the CRC-32 and file sizes are not known when the header is written.
			// The fields in the local header are filled with zero, and the CRC-32 and size are appended in a 12-byte structure (optionally preceded
			// by a 4-byte signature) immediately after the compressed data:

			ZIPCompressionMethod compressionMethod = ZIPCompressionMethod.None; // 0 - also indicates "directory entry"

			CompressionMethod _compressionMethod = CompressionMethod.None; // FIXME: for some reason Deflate does not work on Mono...
			if (item is File)
			{
				switch (_compressionMethod)
				{
					case CompressionMethod.Deflate: compressionMethod = ZIPCompressionMethod.Deflate; break;
					case CompressionMethod.Deflate64: compressionMethod = ZIPCompressionMethod.Deflate64; break;
					case CompressionMethod.Bzip2: compressionMethod = ZIPCompressionMethod.BZip2; break;
					case CompressionMethod.LZMA: compressionMethod = ZIPCompressionMethod.LZMA; break;
				}
			}
			bw.WriteInt16((short)compressionMethod);

			bw.WriteDOSFileTime(DateTime.Now);

			bool isEncrypted = false;

			byte[] compressedData = null;
			byte[] uncompressedData = null;

			if (item is File)
			{
				File file = (item as File);

				uncompressedData = file.GetData();

				int iCRC32 = (int)(new UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule()).Calculate(uncompressedData);
				bw.WriteInt32(iCRC32);

				compressedData = CompressionModule.FromKnownCompressionMethod(_compressionMethod).Compress(uncompressedData);
				bw.WriteInt32((int)compressedData.Length);
				bw.WriteInt32((int)uncompressedData.Length);
			}
			else if (item is Folder)
			{
				Folder fldr = (item as Folder);
				bw.WriteInt32(0);
				bw.WriteInt32(0);
				bw.WriteInt32(0);
			}

			ZIPExtraDataField[] edfs = new ZIPExtraDataField[]
			{
				new ZIPExtraDataFieldExtendedTimestamp(DateTime.Now, DateTime.Now, DateTime.Now)
			};

			short fileNameLength = (short)item.Name.Length;
			short extraFieldLength = 0;
			foreach (ZIPExtraDataField edf in edfs)
			{
				extraFieldLength += (short)(4 + edf.LocalData.Length);
			}

			bw.WriteInt16(fileNameLength);
			bw.WriteInt16(extraFieldLength);
			bw.WriteFixedLengthString(item.Name, fileNameLength);

			foreach (ZIPExtraDataField edf in edfs)
			{
				bw.WriteInt16(edf.TypeCode);
				bw.WriteInt16((short)edf.LocalData.Length);
				bw.WriteBytes(edf.LocalData);
			}

			if (item is File)
			{
				bw.WriteBytes(compressedData);
				if (isEncrypted)
				{
					throw new System.Security.SecurityException("File is encrypted");
				}

				// local file entry footer
				bw.WriteBytes(new byte[] { (byte)'P', (byte)'K', (byte)0x07, (byte)0x08 });
				bw.WriteBytes(new byte[] { 0xE8, 0xD0, 0x01, 0x23 });
				bw.WriteInt32(compressedData.Length);
				bw.WriteInt32(uncompressedData.Length);
			}
		}

		private void WriteDate(Writer bw, DateTime date)
		{
			short iFileLastModificationTime = (short)(date.ToFileTime());
			bw.WriteInt16(iFileLastModificationTime);

			short iFileLastModificationDate = (short)(date.ToFileTime() >> 2);
			bw.WriteInt16(iFileLastModificationDate);
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
				br.Seek((long)l, SeekOrigin.Begin);
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

		private long zip_FindEndOfCentralDirectory(IO.Reader reader)
		{
			reader.Seek(-4, SeekOrigin.End);
			while (reader.Accessor.Position > 0)
			{
				byte[] test = reader.ReadBytes(4);
				if (test.Match(SIG_END_OF_CENTRAL_DIRECTORY))
				{
					return reader.Accessor.Position;
				}
				reader.Seek(-5, SeekOrigin.Current);
			}
			((IHostApplication)Application.Instance).Messages.Add(HostApplicationMessageSeverity.Warning, "end of central directory signature not found", Accessor.GetFileName());
			return -1;
		}

		private File zip_ReadFile(IO.Reader br, IFileSystemContainer fsom)
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
				unpackedData = CompressionModule.FromKnownCompressionMethod(method).Decompress(packedData);
			}

			if (unpackedData.Length != unpackedFileLength)
			{
				// (Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Error, "File size mismatch, source archive may be corrupted", fileName);
			}

			File f = fsom.AddFile(fileName);
			f.SetData(unpackedData);
			return f;
		}
	}
}
