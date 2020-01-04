using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TexasInstruments
{
	public class TIFilesDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			byte isMultiTransfer = reader.ReadByte();
			string signature = reader.ReadFixedLengthString(7);
			if (signature != "TIFILES")
				throw new InvalidDataFormatException("file does not contain 'TIFILES' signature");


			if (isMultiTransfer == 0x07)
			{
			}
			else if (isMultiTransfer == 0x08)
			{
				throw new InvalidDataFormatException("MXT YModem multiple file transfer not supported");
			}
			else
			{
				throw new InvalidDataFormatException("first byte of file unrecognized (0x07 = normal, 0x08 = MXT YModem multiple file transfer)");
			}

			ushort sectorCount = reader.ReadUInt16();
			TIFilesFlags flags = (TIFilesFlags) reader.ReadByte();
			byte recordsPerSector = reader.ReadByte();
			byte eofOffset = reader.ReadByte();
			byte recordLength = reader.ReadByte();
			ushort level3RecordCount = reader.ReadUInt16();
			string fileName = reader.ReadFixedLengthString(10);
			fileName = fileName.TrimNull();
			if (String.IsNullOrEmpty(fileName))
			{
				fileName = System.IO.Path.GetFileNameWithoutExtension(Accessor.GetFileName()).ToUpper();
			}

			byte mxt = reader.ReadByte();
			byte reserved1 = reader.ReadByte();
			ushort extendedHeader = reader.ReadUInt16();
			uint creationTimestamp = reader.ReadUInt32();
			uint modificationTimestamp = reader.ReadUInt32();

			File f = fsom.AddFile(fileName);
			f.DataRequest += F_DataRequest;
			f.Properties.Add("offset", reader.Accessor.Position);
			f.Properties.Add("reader", reader);
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader reader = (Reader)f.Properties["reader"];
			long offset = (long)f.Properties["offset"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadToEnd();
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

		}
	}
}
