//
//  AVIFDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Chunked.ISOMediaBase;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.AVIF
{
	public class AVIFDataFormat : ISOMediaBaseDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Title = "AVIF picture";

				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
			PictureObjectModel picture = (objectModels.Pop() as PictureObjectModel);

			byte[] ftyp = ((RIFFDataChunk)chunked.Chunks["ftyp"]).Source.GetData();
			MemoryAccessor ma_ftyp = new MemoryAccessor(ftyp);
			ma_ftyp.Reader.Endianness = Endianness.BigEndian;

			string major_brand = ma_ftyp.Reader.ReadFixedLengthString(4);
			int minor_version = ma_ftyp.Reader.ReadInt32();
			string[] compatible_brands = ReadFixedStringArray(ma_ftyp.Reader, 4);

			byte[] meta = ((RIFFDataChunk)chunked.Chunks["meta"]).Source.GetData();
			MemoryAccessor ma_meta = new MemoryAccessor(meta);

			Dictionary<string, byte[]> metaChunks = new Dictionary<string, byte[]>();
			ma_meta.Reader.Endianness = Endianness.BigEndian;
			uint unknown1 = ma_meta.Reader.ReadUInt32();
			while (!ma_meta.Reader.EndOfStream)
			{
				uint metaChunkLen = ma_meta.Reader.ReadUInt32();
				string metaChunkName = ma_meta.Reader.ReadFixedLengthString(4);
				byte[] metaChunkData = ma_meta.Reader.ReadBytes(metaChunkLen - 8);
				metaChunks[metaChunkName] = metaChunkData;
			}

			objectModels.Push(picture);
		}

		private string[] ReadFixedStringArray(Reader reader, int fixedSize)
		{
			List<string> list = new List<string>();
			while (!reader.EndOfStream)
			{
				string i = reader.ReadFixedLengthString(fixedSize);
				list.Add(i);
			}
			return list.ToArray();
		}

		/*
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			uint totalChunkSize = reader.ReadUInt32(); // including this field

			string ftyp = reader.ReadFixedLengthString(4); // ftyp
			string avif = reader.ReadFixedLengthString(4); // avif
			int unkw1 = reader.ReadInt32(); // unknown
			string avif2 = reader.ReadFixedLengthString(4); //avif
			string mif1 = reader.ReadFixedLengthString(4); //mif1
			string miaf = reader.ReadFixedLengthString(4); //miaf
			string MA1B = reader.ReadFixedLengthString(4); //MA1B

			uint metadataChunkSize = reader.ReadUInt32(); // NOT including this field...
			reader.Seek(metadataChunkSize, SeekOrigin.Current);

			string mdat = reader.ReadFixedLengthString(4); // mdat
			int mdatLen = reader.ReadInt32();
			string mdatVal = reader.ReadFixedLengthString(mdatLen).TrimNull();

		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
		*/
	}
}
