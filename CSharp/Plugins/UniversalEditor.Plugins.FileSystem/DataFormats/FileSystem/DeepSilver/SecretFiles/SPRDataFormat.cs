using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.DeepSilver.SecretFiles
{
	public class SPRDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionNumber("EncryptionDomain", "Encryption &domain:", 0xbebe2, Int32.MaxValue, Int32.MinValue));
				_dfr.ExportOptions.Add(new CustomOptionNumber("EncryptionSeed", "Encryption &seed:", 0, Int32.MaxValue, Int32.MinValue));
				_dfr.Filters.Add("Secret Files: Tunguska SPR archive", new byte?[][] { new byte?[] { (byte)'S', (byte)'P', (byte)'C', (byte)'D' } }, new string[] { "*.spr" });
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=Secret_Files:_Tunguska_%28Demo%29_SPR");
			}
			return _dfr;
		}

		private int mvarEncryptionDomain = 0xbebe2;
		public int EncryptionDomain { get { return mvarEncryptionDomain; } set { mvarEncryptionDomain = value; } }

		private int mvarEncryptionSeed = 0;
		public int EncryptionSeed { get { return mvarEncryptionSeed; } set { mvarEncryptionSeed = value; } }

		private byte[] EncryptDecrypt(byte[] input)
		{
			byte[] output = (input.Clone() as byte[]);
			
			int KeyDomain = mvarEncryptionDomain * mvarEncryptionSeed;
			int Key = ((mvarEncryptionSeed << 8) | ((~mvarEncryptionSeed) & 0xff));

			for (int i = 0; i < input.Length; i++)
			{
				Key = (Key * Key) % KeyDomain;
				output[i] = (byte)(output[i] ^ (Key & 0xFF));
				Key = (Key + output[i] + 1);
			}
			
			return output;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "SPCD") throw new InvalidDataFormatException("File does not begin with 'SPCD'");

			uint unknown1 = reader.ReadUInt32();
			uint unknown2 = reader.ReadUInt32();
			mvarEncryptionSeed = reader.ReadInt32();
			uint unknown3 = reader.ReadUInt32();
			uint unknown4 = reader.ReadUInt32();
			uint stringTableEntryCount = reader.ReadUInt32();
			uint stringTableOffset = reader.ReadUInt32();
			uint stringTableLength = reader.ReadUInt32();
			uint fileTableEntryCount = reader.ReadUInt32();
			uint fileTableOffset = reader.ReadUInt32();
			uint fileTableLength = reader.ReadUInt32();
			uint unknown5 = reader.ReadUInt32();
			uint directoryTableEntryCount = reader.ReadUInt32();
			uint directoryTableOffset = reader.ReadUInt32();
			uint directoryTableLength = reader.ReadUInt32();

			List<string> stringTableEntries = new List<string>();
			#region String Table
			{
				reader.Seek(stringTableOffset, SeekOrigin.Begin);
				byte[] data = reader.ReadBytes(stringTableLength);
				data = EncryptDecrypt(data);

				MemoryAccessor ma = new MemoryAccessor(data);
				Reader rdr = new Reader(ma);

				ushort[] stringLengths = new ushort[stringTableEntryCount];
				for (int i = 0; i < stringTableEntryCount; i++)
				{
					stringLengths[i] = reader.ReadUInt16();
				}
				for (int i = 0; i < stringTableEntryCount; i++)
				{
					string value = reader.ReadFixedLengthString(stringLengths[i]);
					stringTableEntries.Add(value);
				}
			}
			#endregion

			#region Directory Table
			{
				reader.Seek(directoryTableOffset, SeekOrigin.Begin);
				byte[] data = reader.ReadBytes(directoryTableLength);
				data = EncryptDecrypt(data);

				MemoryAccessor ma = new MemoryAccessor(data);
				Reader rdr = new Reader(ma);

				for (int i = 0; i < directoryTableEntryCount; i++)
				{
					uint directoryNameIndex = reader.ReadUInt32();
					uint parentDirectoryIndex = reader.ReadUInt32(); // 0x1effffff for root
				}
			}
			#endregion

			#region File Table
			{
				reader.Seek(fileTableOffset, SeekOrigin.Begin);
				byte[] data = reader.ReadBytes(fileTableLength);
				data = EncryptDecrypt(data);

				MemoryAccessor ma = new MemoryAccessor(data);
				Reader rdr = new Reader(ma);

				for (int i = 0; i < fileTableEntryCount; i++)
				{
					uint parentDirectoryIndex = reader.ReadUInt32(); // 0x1effffff for root
					uint fileNamePrefixIndex = reader.ReadUInt32(); // index into string table
					uint fileNameSuffixIndex = reader.ReadUInt32();
					uint crc32 = reader.ReadUInt32();

					#region File date
					ushort year = reader.ReadUInt16();
					ushort month = reader.ReadUInt16();
					ushort dayOfWeek = reader.ReadUInt16();
					ushort day = reader.ReadUInt16();
					ushort hour = reader.ReadUInt16();
					ushort minute = reader.ReadUInt16();
					ushort second = reader.ReadUInt16();
					ushort millisecond = reader.ReadUInt16();

					DateTime fileDate = new DateTime(year, month, day, hour, minute, second, millisecond);
					#endregion

					uint decompressedLength = reader.ReadUInt32();

					// 0xffffffff if file is not compressed
					uint compressionHeaderOffset = reader.ReadUInt32();

					// zero if file is not compressed
					uint compressionHeaderLength = reader.ReadUInt32();

					uint fileDataOffset = reader.ReadUInt32();
					uint fileDataLength = reader.ReadUInt32();
				}
			}
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("SPCD");

			writer.WriteUInt32(0);
			writer.WriteUInt32(0);

			writer.WriteInt32(mvarEncryptionSeed);

			writer.WriteUInt32(0);
			writer.WriteUInt32(0);

			File[] files = fsom.GetAllFiles();
			uint baseOffset = 54;
			for (uint i = 0; i < files.Length; i++)
			{
				baseOffset += (uint)files[i].Size;
			}

			#region Prepare String Table
			List<string> stringTableEntries = new List<string>();
			uint stringTableOffset = baseOffset;
			byte[] stringTableData = null;
			#region Initialize String Table
			{
				MemoryAccessor ma = new MemoryAccessor();
				Writer bwst = new Writer(ma);
				foreach (string s in stringTableEntries)
				{
					bwst.WriteUInt16((ushort)s.Length);
				}
				foreach (string s in stringTableEntries)
				{
					bwst.WriteFixedLengthString(s);
				}
				bwst.Flush();
				bwst.Close();
				stringTableData = ma.ToArray();
				stringTableData = EncryptDecrypt(stringTableData);
			}
			#endregion
			#endregion
			#region Prepare Directory Table
			List<SPRDirectoryInfo> directoryTableEntries = new List<SPRDirectoryInfo>();
			uint directoryTableOffset = (uint)(baseOffset + stringTableData.Length);
			byte[] directoryTableData = null;
			#region Initialize File Table
			{
				MemoryAccessor ma = new MemoryAccessor();
				Writer bwst = new Writer(ma);
				foreach (SPRDirectoryInfo di in directoryTableEntries)
				{
					bwst.WriteUInt32(di.directoryNameIndex);
					bwst.WriteUInt32(di.parentDirectoryIndex);
				}
				bwst.Flush();
				bwst.Close();
				directoryTableData = ma.ToArray();
				directoryTableData = EncryptDecrypt(directoryTableData);
			}
			#endregion
			#endregion
			#region Prepare File Table
			List<SPRFileInfo> fileTableEntries = new List<SPRFileInfo>();
			uint fileTableOffset = (uint)(baseOffset + stringTableData.Length + directoryTableData.Length);
			byte[] fileTableData = null;
			#region Initialize File Table
			{
				MemoryAccessor ma = new MemoryAccessor();
				Writer bwst = new Writer(ma);
				foreach (SPRFileInfo fi in fileTableEntries)
				{
					bwst.WriteInt32(fi.parentDirectoryIndex);
					bwst.WriteInt32(fi.fileNamePrefixIndex);
					bwst.WriteInt32(fi.fileNameSuffixIndex);
					bwst.WriteInt32(fi.crc32);

					// Windows _SYSTEMTIME timestamp format
					bwst.WriteUInt16((ushort)fi.timestamp.Year);
					bwst.WriteUInt16((ushort)fi.timestamp.Month);
					bwst.WriteUInt16((ushort)fi.timestamp.DayOfWeek);
					bwst.WriteUInt16((ushort)fi.timestamp.Day);
					bwst.WriteUInt16((ushort)fi.timestamp.Hour);
					bwst.WriteUInt16((ushort)fi.timestamp.Minute);
					bwst.WriteUInt16((ushort)fi.timestamp.Second);
					bwst.WriteUInt16((ushort)fi.timestamp.Millisecond);

					bwst.WriteUInt32(fi.decompressedLength);
					bwst.WriteInt32(fi.compressionHeaderOffset);
					bwst.WriteUInt32(fi.compressionHeaderLength);
					bwst.WriteUInt32(fi.offset);
					bwst.WriteUInt32(fi.compressedLength);
				}
				bwst.Flush();
				bwst.Close();
				fileTableData = ma.ToArray();
				fileTableData = EncryptDecrypt(fileTableData);
			}
			#endregion
			#endregion

			#region String Table Header
			// Number of strings
			writer.WriteUInt32((uint)stringTableEntries.Count);
			// String table offset
			writer.WriteUInt32(stringTableOffset);
			// String table size
			writer.WriteUInt32((uint)stringTableData.Length);
			#endregion
			#region File Table Header
			writer.WriteUInt32((uint)fileTableEntries.Count);
			writer.WriteUInt32(fileTableOffset);
			writer.WriteUInt32((uint)fileTableData.Length);
			#endregion
			#region Directory Table Data
			writer.WriteUInt32(0);
			writer.WriteUInt32((uint)directoryTableEntries.Count);
			writer.WriteUInt32(directoryTableOffset);
			writer.WriteUInt32((uint)directoryTableData.Length);
			#endregion

			writer.WriteBytes(stringTableData);
			writer.WriteBytes(directoryTableData);
			writer.WriteBytes(fileTableData);
		}
	}
}
