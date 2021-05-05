using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet
{
	public class CABDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://msdn.microsoft.com/en-us/library/bb267310.aspx#struct_spec");

				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(CabinetReservedAreaSize), "Per-_cabinet reserved area size", 0, 0, UInt16.MaxValue));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(FolderReservedAreaSize), "Per-_folder reserved area size", 0, 0, Byte.MaxValue));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(DatablockReservedAreaSize), "Per-_datablock reserved area size", 0, 0, Byte.MaxValue));

				_dfr.ExportOptions.Add(new CustomOptionText(nameof(NextCabinetName), "_Next cabinet file name"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(NextDiskName), "Ne_xt cabinet disk name"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(PreviousCabinetName), "_Previous cabinet file name"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(PreviousDiskName), "Pre_vious cabinet disk name"));
			}
			return _dfr;
		}

		private ushort mvarCabinetReservedAreaSize = 0;
		public ushort CabinetReservedAreaSize { get { return mvarCabinetReservedAreaSize; } set { mvarCabinetReservedAreaSize = value; } }

		private byte mvarFolderReservedAreaSize = 0;
		public byte FolderReservedAreaSize { get { return mvarFolderReservedAreaSize; } set { mvarFolderReservedAreaSize = value; } }

		private byte mvarDatablockReservedAreaSize = 0;
		public byte DatablockReservedAreaSize { get { return mvarDatablockReservedAreaSize; } set { mvarDatablockReservedAreaSize = value; } }

		private string mvarNextCabinetName = String.Empty;
		public string NextCabinetName { get { return mvarNextCabinetName; } set { mvarNextCabinetName = value; } }

		private string mvarNextDiskName = String.Empty;
		public string NextDiskName { get { return mvarNextDiskName; } set { mvarNextDiskName = value; } }

		private string mvarPreviousCabinetName = String.Empty;
		public string PreviousCabinetName { get { return mvarPreviousCabinetName; } set { mvarPreviousCabinetName = value; } }

		private string mvarPreviousDiskName = String.Empty;
		public string PreviousDiskName { get { return mvarPreviousDiskName; } set { mvarPreviousDiskName = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			Reader br = base.Accessor.Reader;

			Internal.CFHEADER cfheader = ReadCFHEADER(br);
			mvarCabinetReservedAreaSize = cfheader.cabinetReservedAreaSize;
			mvarDatablockReservedAreaSize = cfheader.datablockReservedAreaSize;
			mvarFolderReservedAreaSize = cfheader.folderReservedAreaSize;

			List<Internal.CFFOLDER> folders = new List<Internal.CFFOLDER>();
			for (ushort cfFolderID = 0; cfFolderID < cfheader.folderCount; cfFolderID++)
			{
				Internal.CFFOLDER cffolder = ReadCFFOLDER(br, cfheader);
				folders.Add(cffolder);
			}
			for (ushort cfFileID = 0; cfFileID < cfheader.fileCount; cfFileID++)
			{
				Internal.CFFILE cffile = ReadCFFILE(br);
				File file = fsom.AddFile(cffile.name);
				file.Size = cffile.decompressedSize;
				file.Properties.Add("cffile", cffile);
				file.Properties.Add("cffolder", folders[cffile.folderIndex]);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
			}
		}

		private Internal.CFHEADER ReadCFHEADER(Reader br)
		{
			Internal.CFHEADER cfheader = new Internal.CFHEADER();
			cfheader.signature = br.ReadFixedLengthString(4); // cabinet file signature
			if (cfheader.signature != "MSCF") throw new InvalidDataFormatException("File does not begin with \"MSCF\"");

			cfheader.reserved1 = br.ReadUInt32(); // reserved
			cfheader.cabinetFileSize = br.ReadUInt32(); // size of this cabinet file in bytes
			if (br.Accessor.Length != cfheader.cabinetFileSize) throw new DataCorruptedException("Stored cabinet length does not match actual file length");

			cfheader.reserved2 = br.ReadUInt32(); // reserved
			cfheader.firstFileOffset = br.ReadUInt32(); // offset of the first CFFILE entry
			cfheader.reserved3 = br.ReadUInt32(); // reserved

			cfheader.versionMinor = br.ReadByte(); // cabinet file format version, minor
			cfheader.versionMajor = br.ReadByte(); // cabinet file format version, major

			cfheader.folderCount = br.ReadUInt16(); // number of CFFOLDER entries in this cabinet
			cfheader.fileCount = br.ReadUInt16(); // number of CFFILE entries in this cabinet

			cfheader.flags = (CABFlags)br.ReadUInt16(); // cabinet file option indicators:

			cfheader.setID = br.ReadUInt16(); // must be the same for all cabinets in a set
			cfheader.iCabinet = br.ReadUInt16(); // number of this cabinet file in a set

			if ((cfheader.flags & CABFlags.HasReservedArea) == CABFlags.HasReservedArea)
			{
				cfheader.cabinetReservedAreaSize = br.ReadUInt16(); // (optional) size of per-cabinet reserved area
				cfheader.folderReservedAreaSize = br.ReadByte(); // (optional) size of per-folder reserved area
				cfheader.datablockReservedAreaSize = br.ReadByte(); // (optional) size of per-datablock reserved area
			}

			cfheader.reservedArea = br.ReadBytes(mvarCabinetReservedAreaSize); // (optional) per-cabinet reserved area

			if ((cfheader.flags & CABFlags.HasPreviousCabinet) == CABFlags.HasPreviousCabinet)
			{
				cfheader.previousCabinetName = br.ReadNullTerminatedString();  /* (optional) name of previous cabinet file */
				cfheader.previousDiskName = br.ReadNullTerminatedString();     /* (optional) name of previous disk */
			}
			if ((cfheader.flags & CABFlags.HasNextCabinet) == CABFlags.HasNextCabinet)
			{
				cfheader.nextCabinetName = br.ReadNullTerminatedString();  /* (optional) name of next cabinet file */
				cfheader.nextDiskName = br.ReadNullTerminatedString();     /* (optional) name of next disk */
			}
			return cfheader;
		}
		private Internal.CFFILE ReadCFFILE(Reader br)
		{
			Internal.CFFILE cffile = new Internal.CFFILE();

			cffile.decompressedSize = br.ReadUInt32();		// uncompressed size of this file in bytes
			cffile.offset = br.ReadUInt32();				// uncompressed offset of this file in the folder
			cffile.folderIndex = br.ReadUInt16();           // index into the CFFOLDER area

			cffile.date = br.ReadUInt16();                  // date stamp for this file
			cffile.time = br.ReadUInt16();                  // time stamp for this file

			cffile.attribs = br.ReadUInt16();               // attribute flags for this file
			cffile.name = br.ReadNullTerminatedString();    // name of this file
			return cffile;
		}
		private Internal.CFFOLDER ReadCFFOLDER(Reader br, Internal.CFHEADER cfheader)
		{
			Internal.CFFOLDER cffolder = new Internal.CFFOLDER();
			cffolder.firstDataBlockOffset = br.ReadUInt32(); // offset of the first CFDATA block in this folder
			cffolder.dataBlockCount = br.ReadUInt16(); // number of CFDATA blocks in this folder
			cffolder.compressionMethod = (CABCompressionMethod)br.ReadUInt16(); // compression type indicator
			if ((cfheader.flags & CABFlags.HasReservedArea) == CABFlags.HasReservedArea)
			{
				cffolder.reservedArea = br.ReadBytes(mvarFolderReservedAreaSize);   // (optional) per-folder reserved area
			}
			return cffolder;
		}
		private Internal.CFDATA ReadCFDATA(Reader br)
		{
			Internal.CFDATA data = new Internal.CFDATA();
			data.checksum = br.ReadUInt32();
			data.compressedLength = br.ReadUInt16();
			data.decompressedLength = br.ReadUInt16();
			data.reservedArea = br.ReadBytes(mvarDatablockReservedAreaSize);
			data.data = br.ReadBytes(data.compressedLength);
			return data;
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			Internal.CFFILE cffile = (Internal.CFFILE)file.Properties["cffile"];
			Internal.CFFOLDER cffolder = (Internal.CFFOLDER)file.Properties["cffolder"];

			uint decompressedSize = cffile.decompressedSize;
			uint offset = cffile.offset;
			Reader br = (Reader)file.Properties["reader"];

			br.Accessor.Position = cffolder.firstDataBlockOffset;

			byte[] decompressedData = new byte[decompressedSize];
			int j = 0;
			for (int i = 0; i < cffolder.dataBlockCount; i++)
			{
				Internal.CFDATA data = ReadCFDATA(br);
				switch (cffolder.compressionMethod)
				{
					case CABCompressionMethod.None:
					{
						// no compression
						file.Properties["CompressionMethod"] = CompressionMethod.None;
						Array.Copy(data.data, offset, decompressedData, j, decompressedData.Length);
						j += decompressedData.Length;
						break;
					}
					case CABCompressionMethod.MSZIP:
					{
						Reader br1 = new Reader(new MemoryAccessor(data.data));
						byte mszipSig1 = br1.ReadByte();
						byte mszipSig2 = br1.ReadByte();

						byte[] rest = br1.ReadToEnd();
						byte[] decompressedDataLocal = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Deflate).Decompress(rest);
						// Array.Copy(decompressedDataLocal, offset, decompressedData, j, decompressedData.Length);
						// Array.Copy(decompressedDataLocal, j, decompressedData, 0, decompressedDataLocal.Length);
						j += decompressedDataLocal.Length;
						break;
					}
					case CABCompressionMethod.LZX:
					{
						file.Properties["CompressionMethod"] = Compression.CompressionMethod.LZX;
						decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.LZX).Decompress(data.data);

						// no compression
						Array.Copy(data.data, 0, decompressedData, j, data.data.Length);
						j += data.data.Length;
						break;
					}
					default:
					{
						throw new InvalidOperationException("unknown compression method");
					}
				}
			}

			e.Data = decompressedData;
		}



		#region Save
		private ushort PackDate(DateTime value)
		{
			return (ushort)(((value.Year - 1980) << 9) + (value.Month << 5) + value.Day);
		}
		private ushort PackTime(DateTime value)
		{
			return (ushort)((value.Hour << 11) + (value.Minute << 5) + (value.Second / 2));
		}

		private int CalculateHeaderSize(Internal.CFHEADER cfheader)
		{
			int cbsize = 36;
			if ((cfheader.flags & CABFlags.HasReservedArea) == CABFlags.HasReservedArea)
			{
				cbsize += 4 + cfheader.cabinetReservedAreaSize;
			}
			if ((cfheader.flags & CABFlags.HasPreviousCabinet) == CABFlags.HasPreviousCabinet)
			{
				cbsize += (cfheader.previousCabinetName?.Length).GetValueOrDefault(0) + 1 + (cfheader.previousDiskName?.Length).GetValueOrDefault(0) + 1;
			}
			if ((cfheader.flags & CABFlags.HasNextCabinet) == CABFlags.HasNextCabinet)
			{
				cbsize += (cfheader.nextCabinetName?.Length).GetValueOrDefault(0) + 1 + (cfheader.nextDiskName?.Length).GetValueOrDefault(0) + 1;
			}
			return cbsize;
		}
		private int CalculateHeaderSize(Internal.CFFOLDER cffolder)
		{
			return 8 + (cffolder.reservedArea?.Length).GetValueOrDefault(0);
		}
		private int CalculateHeaderSize(Internal.CFFILE cffile)
		{
			return 16 + (cffile.name?.Length).GetValueOrDefault(0) + 1;
		}


		private void WriteCFHEADER(Writer bw, Internal.CFHEADER cfheader)
		{
			bw.WriteFixedLengthString(cfheader.signature, 4);

			bw.WriteUInt32(cfheader.reserved1); // reserved
			bw.WriteUInt32(cfheader.cabinetFileSize); // size of this cabinet file in bytes

			bw.WriteUInt32(cfheader.reserved2); // reserved
			bw.WriteUInt32(cfheader.firstFileOffset); // offset of the first CFFILE entry
			bw.WriteUInt32(cfheader.reserved3); // reserved

			bw.WriteByte(cfheader.versionMinor); // cabinet file format version, minor
			bw.WriteByte(cfheader.versionMajor); // cabinet file format version, major

			bw.WriteUInt16(cfheader.folderCount); // number of CFFOLDER entries in this cabinet
			bw.WriteUInt16(cfheader.fileCount); // number of CFFILE entries in this cabinet

			bw.WriteUInt16((ushort)cfheader.flags); // cabinet file option indicators

			bw.WriteUInt16(cfheader.setID); // must be the same for all cabinets in a set
			bw.WriteUInt16(cfheader.iCabinet); // number of this cabinet file in a set

			if ((cfheader.flags & CABFlags.HasReservedArea) == CABFlags.HasReservedArea)
			{
				bw.WriteUInt16(cfheader.cabinetReservedAreaSize); // (optional) size of per-cabinet reserved area
				bw.WriteByte(cfheader.folderReservedAreaSize); // (optional) size of per-folder reserved area
				bw.WriteByte(cfheader.datablockReservedAreaSize); // (optional) size of per-datablock reserved area
			}

			bw.WriteFixedLengthBytes(cfheader.reservedArea, cfheader.cabinetReservedAreaSize);

			if ((cfheader.flags & CABFlags.HasPreviousCabinet) == CABFlags.HasPreviousCabinet)
			{
				bw.WriteNullTerminatedString(cfheader.previousCabinetName);  // (optional) name of previous cabinet file
				bw.WriteNullTerminatedString(cfheader.previousDiskName);     // (optional) name of previous disk
			}
			if ((cfheader.flags & CABFlags.HasNextCabinet) == CABFlags.HasNextCabinet)
			{
				bw.WriteNullTerminatedString(cfheader.nextCabinetName);  // (optional) name of next cabinet file
				bw.WriteNullTerminatedString(cfheader.nextDiskName);     // (optional) name of next disk
			}
		}
		private void WriteCFFILE(Writer bw, Internal.CFFILE cffile)
		{
			bw.WriteUInt32(cffile.decompressedSize);
			bw.WriteUInt32(cffile.offset);
			bw.WriteUInt16(cffile.folderIndex);
			bw.WriteUInt16(cffile.date);
			bw.WriteUInt16(cffile.time);
			bw.WriteUInt16(cffile.attribs);
			bw.WriteNullTerminatedString(cffile.name);
		}
		private void WriteCFFOLDER(Writer bw, Internal.CFFOLDER cffolder, Internal.CFHEADER cfheader)
		{
			bw.WriteUInt32(cffolder.firstDataBlockOffset);
			bw.WriteUInt16(cffolder.dataBlockCount);
			bw.WriteUInt16((ushort)cffolder.compressionMethod);
			if ((cfheader.flags & CABFlags.HasReservedArea) == CABFlags.HasReservedArea)
			{
				bw.WriteFixedLengthBytes(cffolder.reservedArea, cfheader.folderReservedAreaSize);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			Writer bw = base.Accessor.Writer;

			// go through and build a list of files
			File[] files = fsom.GetAllFiles();

			List<Internal.CFFILE> cffiles = new List<Internal.CFFILE>();
			List<Internal.CFFOLDER> cffolders = new List<Internal.CFFOLDER>();

			Internal.CFHEADER cfheader = new Internal.CFHEADER();
			cfheader.signature = "MSCF";

			long totalDataSize = 0;
			foreach (File file in files)
			{
				Internal.CFFILE cffile = new Internal.CFFILE();
				cffile.name = file.Name;
				cffile.decompressedSize = (uint)file.Size;
				cffile.offset = 0; // TODO: how to get the offset of the file?
				cffile.folderIndex = 0;
				cfheader.cabinetFileSize += cffile.decompressedSize;
				cffile.file = file;
				totalDataSize += file.GetData().Length;
				cffiles.Add(cffile);
			}

			#region cffolder
			{
				Internal.CFFOLDER cffolder = new Internal.CFFOLDER();
				cffolder.compressionMethod = CABCompressionMethod.None;
				cffolder.dataBlockCount = 1;
				cffolder.firstDataBlockOffset = 0;
				cffolders.Add(cffolder);
			}
			#endregion

			cfheader.firstFileOffset = 0;

			cfheader.versionMajor = 3;
			cfheader.versionMinor = 1;

			cfheader.folderCount = (ushort)cffolders.Count;
			cfheader.fileCount = (ushort)cffiles.Count;

			cfheader.setID = 12345;
			cfheader.iCabinet = 0;

			cfheader.cabinetReservedAreaSize = mvarCabinetReservedAreaSize;
			cfheader.folderReservedAreaSize = mvarFolderReservedAreaSize;
			cfheader.datablockReservedAreaSize = mvarDatablockReservedAreaSize;

			cfheader.nextCabinetName = mvarNextCabinetName;
			cfheader.nextDiskName = mvarNextDiskName;
			cfheader.previousCabinetName = mvarPreviousCabinetName;
			cfheader.previousDiskName = mvarPreviousDiskName;

			if (cfheader.cabinetReservedAreaSize != 0 || cfheader.folderReservedAreaSize != 0 || cfheader.datablockReservedAreaSize != 0) cfheader.flags |= CABFlags.HasReservedArea;
			if (!String.IsNullOrEmpty(cfheader.nextCabinetName) || !String.IsNullOrEmpty(cfheader.nextDiskName)) cfheader.flags |= CABFlags.HasNextCabinet;
			if (!String.IsNullOrEmpty(cfheader.previousCabinetName) || !String.IsNullOrEmpty(cfheader.previousCabinetName)) cfheader.flags |= CABFlags.HasPreviousCabinet;

			long cabinetFileSize = CalculateHeaderSize(cfheader);
			for (int i = 0; i < cffolders.Count; i++)
			{
				cabinetFileSize += CalculateHeaderSize(cffolders[i]);
			}
			for (int i = 0; i < cffiles.Count; i++)
			{
				cabinetFileSize += CalculateHeaderSize(cffiles[i]);
			}
			cabinetFileSize += totalDataSize;
			cfheader.cabinetFileSize = (uint)cabinetFileSize;

			WriteCFHEADER(bw, cfheader);

			long offset = Accessor.Position;
			for (int i = 0; i < cffolders.Count; i++)
			{
				offset += CalculateHeaderSize(cffolders[i]);
			}
			for (int i = 0; i < cffiles.Count; i++)
			{
				offset += CalculateHeaderSize(cffiles[i]);
			}

			for (int i = 0; i < cffolders.Count; i++)
			{
				WriteCFFOLDER(bw, cffolders[i], cfheader);
			}
			for (int i = 0;  i < cffiles.Count;  i++)
			{
				Internal.CFFILE cffile = cffiles[i];
				cffile.offset = (uint)offset;
				WriteCFFILE(bw, cffile);
			}
			for (int i = 0; i < cffiles.Count; i++)
			{
				byte[] data = cffiles[i].file.GetData();
				bw.WriteBytes(data);
			}
		}
		#endregion
	}
}
