﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
	public class CFLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Compressed File Library archive", new byte?[][] { new byte?[] { (byte)'C', (byte)'F', (byte)'L', (byte)'3' } }, new string[] { "*.cfl" });
				_dfr.Sources.Add("http://sol.gfxile.net/cfl/index.html");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string CFL3 = reader.ReadFixedLengthString(4);
			if (CFL3 != "CFL3") throw new InvalidDataFormatException();

			uint offsetToDirectory = reader.ReadUInt32();
			uint decompressedLibrarySize = reader.ReadUInt32();

			base.Accessor.Seek(offsetToDirectory, SeekOrigin.Begin);
			uint compressionType = reader.ReadUInt32();
			uint directorySize = reader.ReadUInt32();
			long directoryEnd = reader.Accessor.Position + directorySize;
			if (directoryEnd > reader.Accessor.Length) throw new InvalidDataFormatException("Directory extends outside the bounds of the file (possible corruption)");

			#region Directory Entry
			while (reader.Accessor.Position < directoryEnd)
			{
				uint fileSize = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();
				uint compressionMethod = reader.ReadUInt32();
				string fileName = reader.ReadUInt16String();

				File file = fsom.AddFile(fileName);
				file.Size = fileSize;
				file.Properties.Add("reader", reader);
				file.Properties.Add("length", fileSize);
				file.Properties.Add("offset", offset);
				file.DataRequest += file_DataRequest;
			}
			#endregion

			uint offsetToEndOfFile = reader.ReadUInt32();
			string signatureFooter = reader.ReadFixedLengthString(4);
			if (signatureFooter != "3CFL") throw new InvalidDataFormatException("File does not end with '3CFL'");
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint filesize = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];

			reader.Accessor.Seek(offset, SeekOrigin.Begin);
			uint compressedSize = reader.ReadUInt32();
			byte[] compressedData = reader.ReadBytes(compressedSize);
			byte[] decompressedData = compressedData;
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("CFL3");

			File[] files = fsom.GetAllFiles();
			uint[] fileOffsets = new uint[files.Length];

			uint initialDirectoryOffset = 12;
			uint initialDirectoryDecompressedLength = 0;
			for (int i = 0; i < files.Length; i++)
			{
				initialDirectoryDecompressedLength += (uint)(14 + files[i].Name.Length);
				initialDirectoryOffset += (uint)(4 + files[i].Size);
			}
			uint initialDirectoryCompressedLength = initialDirectoryDecompressedLength;
			
			writer.WriteUInt32(initialDirectoryOffset);
			writer.WriteUInt32(initialDirectoryCompressedLength);

			for (int i = 0; i < files.Length; i++)
			{
				fileOffsets[i] = (uint)writer.Accessor.Position;
				byte[] decompressedData = files[i].GetDataAsByteArray();
				byte[] compressedData = decompressedData;
				uint compressedLength = (uint)compressedData.Length;
				writer.WriteUInt32(compressedLength);
				writer.WriteBytes(compressedData);
			}

			uint compressionType = 0;
			writer.WriteUInt32(compressionType);

			writer.WriteUInt32(initialDirectoryCompressedLength);
			for (int i = 0; i < files.Length; i++)
			{
				uint fileSize = (uint)files[i].Size;
				writer.WriteUInt32(fileSize);

				uint offset = fileOffsets[i];
				writer.WriteUInt32(offset);

				uint compressionMethod = 0;
				writer.WriteUInt32(compressionMethod);

				writer.WriteUInt16String(files[i].Name);
			}

			uint offsetToEndOfFile = (uint)(writer.Accessor.Position + 8);
			writer.WriteUInt32(offsetToEndOfFile);

			string signatureFooter = "3CFL";
			writer.WriteFixedLengthString(signatureFooter);

			writer.Flush();
		}
	}
}
