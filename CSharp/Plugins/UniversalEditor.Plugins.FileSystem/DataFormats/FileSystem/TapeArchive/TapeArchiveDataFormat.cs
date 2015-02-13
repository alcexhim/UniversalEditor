using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
	public class TapeArchiveDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionBoolean("IsUnixStandardTAR", "Create a UNIX standard tape archive (ustar)", true));
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

				string fileMode = br.ReadFixedLengthString(8).TrimNull().Trim();
				string owner = br.ReadFixedLengthString(8).TrimNull().Trim();
				string group = br.ReadFixedLengthString(8).TrimNull().Trim();
				string fileSizeInBytesOctal = br.ReadFixedLengthString(12).TrimNull().Trim();
				string lastModificationTimeUnixOctal = br.ReadFixedLengthString(12).TrimNull().Trim();
				string headerChecksum = br.ReadFixedLengthString(8).TrimNull().Trim();
				
				char c = br.ReadChar();
				TapeArchiveRecordType type = (TapeArchiveRecordType)((int)c);

				string linkedFileName = br.ReadFixedLengthString(100).TrimNull().Trim();

				string ustar = br.ReadFixedLengthString(6);
				if (ustar == "ustar ")
				{
					if (!ustar_set)
					{
						mvarIsUnixStandardTAR = true;
						ustar_set = true;
					}
					string ustarVersion = br.ReadFixedLengthString(2).TrimNull().Trim();
					string ownerName = br.ReadFixedLengthString(32).TrimNull().Trim();
					string groupName = br.ReadFixedLengthString(32).TrimNull().Trim();
					string deviceMajor = br.ReadFixedLengthString(8).TrimNull().Trim();
					string deviceMinor = br.ReadFixedLengthString(8).TrimNull().Trim();
					string filenamePrefix = br.ReadFixedLengthString(155).TrimNull().Trim();
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

			string fileMode = "       \0";
			bw.WriteFixedLengthString(fileMode, 8);
			string owner = "       \0";
			bw.WriteFixedLengthString(owner, 8);
			string group = "       \0";
			bw.WriteFixedLengthString(group, 8);
			string fileSizeInBytesOctal = "           \0";
			bw.WriteFixedLengthString(fileSizeInBytesOctal, 12);
			string lastModificationTimeUnixOctal = "           \0";
			bw.WriteFixedLengthString(lastModificationTimeUnixOctal, 12);
			string headerChecksum = "           \0";
			bw.WriteFixedLengthString(headerChecksum, 12);

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

			string fileMode = "       \0";
			bw.WriteFixedLengthString(fileMode, 8);
			string owner = "       \0";
			bw.WriteFixedLengthString(owner, 8);
			string group = "       \0";
			bw.WriteFixedLengthString(group, 8);
			string fileSizeInBytesOctal = Convert.ToString(file.Size, 8).PadLeft(11, ' ') + "\0";
			bw.WriteFixedLengthString(fileSizeInBytesOctal, 12);
			string lastModificationTimeUnixOctal = "           \0";
			bw.WriteFixedLengthString(lastModificationTimeUnixOctal, 12);
			string headerChecksum = "           \0";
			bw.WriteFixedLengthString(headerChecksum, 12);

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
