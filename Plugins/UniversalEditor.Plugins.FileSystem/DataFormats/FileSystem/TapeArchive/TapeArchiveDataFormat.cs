//
//  TapeArchiveDataFormat.cs - provides a DataFormat for manipulating archives in Tape Archive (TAR) format
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

using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Tape Archive (TAR) format.
	/// </summary>
	public class TapeArchiveDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(IsUnixStandardTAR), "Create a _UNIX standard tape archive (ustar)", true));
			}
			return _dfr;
		}

		private bool mvarIsUnixStandardTAR = true;
		public bool IsUnixStandardTAR { get { return mvarIsUnixStandardTAR; } set { mvarIsUnixStandardTAR = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			byte[] gzhead = br.PeekBytes(4);
			if (gzhead.Match(new byte[] { 31, 139, 8, 0 }))
			{
				byte[] data = br.ReadToEnd();
				data = CompressionModules.Gzip.Decompress(data);
				br = new IO.Reader(new MemoryAccessor(data));
			}

			bool ustar_set = false;
			while (!br.EndOfStream)
			{
				if (br.Remaining < 263) break;

				string fileName = br.ReadFixedLengthString(100).TrimNull().Trim();
				if (fileName == String.Empty) break;

				string fileMode = br.ReadPaddedNullTerminatedString(8);
				string owner = br.ReadPaddedNullTerminatedString(8);
				string group = br.ReadPaddedNullTerminatedString(8);
				string fileSizeInBytesOctal = br.ReadPaddedNullTerminatedString(12);
				string lastModificationTimeUnixOctal = br.ReadPaddedNullTerminatedString(12);
				string headerChecksum = br.ReadPaddedNullTerminatedString(8);

				char c = br.ReadChar();
				TapeArchiveRecordType type = (TapeArchiveRecordType)((int)c);

				string linkedFileName = br.ReadPaddedNullTerminatedString(100);

				string ustar = br.ReadFixedLengthString(6);
				if (ustar == "ustar ")
				{
					if (!ustar_set)
					{
						mvarIsUnixStandardTAR = true;
						ustar_set = true;
					}
					string ustarVersion = br.ReadPaddedNullTerminatedString(2);
					string ownerName = br.ReadPaddedNullTerminatedString(32);
					string groupName = br.ReadPaddedNullTerminatedString(32);
					string deviceMajor = br.ReadPaddedNullTerminatedString(8);
					string deviceMinor = br.ReadPaddedNullTerminatedString(8);
					string filenamePrefix = br.ReadPaddedNullTerminatedString(155);
				}
				else
				{
					if (!ustar_set)
					{
						mvarIsUnixStandardTAR = false;
						ustar_set = true;
					}
					br.Accessor.Position -= 6;
				}
				br.Align(512);

				if (fileName.EndsWith("/"))
				{
					fileName = fileName.Substring(0, fileName.Length - 1);
					Folder folder = fsom.AddFolder(fileName);
				}
				else
				{
					File file = fsom.AddFile(fileName);

					long fileSize = Convert.ToInt64(fileSizeInBytesOctal, 8);
					long fileOffset = (long)br.Accessor.Position;
					file.Properties.Add("reader", br);
					file.Properties.Add("offset", fileOffset);
					file.Properties.Add("length", fileSize);
					file.Size = fileSize;
					file.DataRequest += file_DataRequest;

					br.Accessor.Position += fileSize;
					br.Align(512);
				}
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			long length = (long)file.Properties["length"];
			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		private long OctalToBase10(long octal)
		{
			long value = 0;
			while (octal > 0)
			{
				value += (octal % 8);
				octal /= 8;
			}
			return value;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			foreach (Folder folder in fsom.Folders)
			{
				RecursiveWriteFolder(bw, folder);
			}
			foreach (File file in fsom.Files)
			{
				RecursiveWriteFile(bw, file, String.Empty);
			}
		}

		private void RecursiveWriteFolder(IO.Writer bw, Folder folder)
		{
			bw.WriteFixedLengthString(folder.Name + "/", 100);

			long? fileMode = null;
			bw.WritePaddedNullableInt64(fileMode, 8);
			long? owner = null;
			bw.WritePaddedNullableInt64(owner, 8);
			long? group = null;
			bw.WritePaddedNullableInt64(group, 8);

			long fileSizeInBytesDecimal = folder.GetSize();
			long? fileSizeInBytesOctal = null;
			bw.WritePaddedNullableInt64(fileSizeInBytesOctal, 8);

			long? lastModificationTimeUnixOctal = null;
			bw.WritePaddedNullableInt64(lastModificationTimeUnixOctal, 12);

			long? headerChecksum = null;
			bw.WritePaddedNullableInt64(headerChecksum, 12);

			char c = (char)(int)TapeArchiveRecordType.Directory;

			string linkedFileName = String.Empty;
			bw.WriteFixedLengthString(linkedFileName, 100);

			if (mvarIsUnixStandardTAR)
			{
				bw.WriteFixedLengthString("ustar ", 6);

				string ustarVersion = String.Empty;
				bw.WriteFixedLengthString(ustarVersion, 2);
				string ownerName = String.Empty;
				bw.WriteFixedLengthString(ownerName, 32);
				string groupName = String.Empty;
				bw.WriteFixedLengthString(groupName, 32);
				string deviceMajor = String.Empty;
				bw.WriteFixedLengthString(deviceMajor, 8);
				string deviceMinor = String.Empty;
				bw.WriteFixedLengthString(deviceMinor, 8);
				string filenamePrefix = String.Empty;
				bw.WriteFixedLengthString(filenamePrefix, 155);
			}
			bw.Align(512);

			foreach (Folder folder1 in folder.Folders)
			{
				RecursiveWriteFolder(bw, folder1);
			}
			foreach (File file1 in folder.Files)
			{
				RecursiveWriteFile(bw, file1, folder.Name + "/");
			}
		}
		private void RecursiveWriteFile(IO.Writer bw, File file, string parentPath)
		{
			if (!String.IsNullOrEmpty(parentPath)) parentPath = parentPath + "/";
			parentPath = parentPath + file.Name;

			bw.WriteFixedLengthString(parentPath, 100);

			long? fileMode = null;
			bw.WritePaddedNullableInt64(fileMode, 8);
			long? owner = null;
			bw.WritePaddedNullableInt64(owner, 8);
			long? group = null;
			bw.WritePaddedNullableInt64(group, 8);
			string fileSizeInBytesOctal = Convert.ToString(file.Size, 8).PadLeft(11, ' ') + "\0";
			bw.WriteFixedLengthString(fileSizeInBytesOctal, 12);
			string lastModificationTimeUnixOctal = String.Empty;
			bw.WriteFixedLengthString(lastModificationTimeUnixOctal, 12);
			long? headerChecksum = null;
			bw.WritePaddedNullableInt64(headerChecksum, 12);

			char c = (char)(int)TapeArchiveRecordType.Directory;

			string linkedFileName = String.Empty;
			bw.WriteFixedLengthString(linkedFileName, 100);

			if (mvarIsUnixStandardTAR)
			{
				bw.WriteFixedLengthString("ustar ", 6);

				string ustarVersion = String.Empty;
				bw.WriteFixedLengthString(ustarVersion, 2);
				string ownerName = String.Empty;
				bw.WriteFixedLengthString(ownerName, 32);
				string groupName = String.Empty;
				bw.WriteFixedLengthString(groupName, 32);
				string deviceMajor = String.Empty;
				bw.WriteFixedLengthString(deviceMajor, 8);
				string deviceMinor = String.Empty;
				bw.WriteFixedLengthString(deviceMinor, 8);
				string filenamePrefix = String.Empty;
				bw.WriteFixedLengthString(filenamePrefix, 155);
			}
			bw.Align(512);
		}
	}
}
