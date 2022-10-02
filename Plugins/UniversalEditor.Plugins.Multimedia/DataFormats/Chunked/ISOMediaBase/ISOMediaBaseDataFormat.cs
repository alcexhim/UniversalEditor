//
//  ISOMediaBaseDataFormat.cs
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.Chunked.ISOMediaBase
{
	public class ISOMediaBaseDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Title = "ISO/IEC base media format";
			dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected virtual byte[] ExpectedSignature { get; } = null;
		protected virtual Endianness Endianness { get; } = Endianness.BigEndian;

		protected virtual ISOMediaBaseChunkLengthType ChunkLengthType { get; } = ISOMediaBaseChunkLengthType.IncludeChunkLengthAndID;
		protected virtual ISOMediaBaseChunkLengthPosition ChunkLengthPosition { get; } = ISOMediaBaseChunkLengthPosition.BeforeChunkID;
		protected virtual ISOMediaBaseChecksumPosition ChecksumPosition { get; } = ISOMediaBaseChecksumPosition.None;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ChunkedObjectModel chunked = (objectModel as ChunkedObjectModel);
			if (chunked == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness;

			if (ExpectedSignature != null)
			{
				byte[] signature = reader.ReadBytes(ExpectedSignature.Length);
				if (!signature.Match(ExpectedSignature))
				{
					throw new InvalidDataFormatException("signature mismatch");
				}
			}

			int chunkLengthOffset = 0;
			if (ChunkLengthType == ISOMediaBaseChunkLengthType.IncludeChunkLengthAndID)
			{
				chunkLengthOffset = 8;
			}
			else if (ChunkLengthType == ISOMediaBaseChunkLengthType.IncludeChunkID)
			{
				chunkLengthOffset = 4;
			}

			while (!reader.EndOfStream)
			{
				uint boxSize = 0;
				string boxName = null;
				if (ChunkLengthPosition == ISOMediaBaseChunkLengthPosition.BeforeChunkID)
				{
					boxSize = reader.ReadUInt32();
					boxName = reader.ReadFixedLengthString(4);
				}
				else if (ChunkLengthPosition == ISOMediaBaseChunkLengthPosition.AfterChunkID)
				{
					boxName = reader.ReadFixedLengthString(4);
					boxSize = reader.ReadUInt32();
				}

				// use the commented offsets for debugging porpoises🐬 only
				// -- we need accurate stream positions cause reading certain
				// -- values can move the stream around (!)

				// long nextOffset = reader.Accessor.Position + boxSize - 8;

				RIFFDataChunk chunk = new RIFFDataChunk();
				chunk.ID = boxName;
				chunk.Source = new EmbeddedFileSource(reader, reader.Accessor.Position, boxSize - chunkLengthOffset);

				// reader.Seek(nextOffset, SeekOrigin.Begin);
				reader.Seek(boxSize - chunkLengthOffset, SeekOrigin.Current);

				if (ChecksumPosition == ISOMediaBaseChecksumPosition.AfterChunkData)
				{
					uint checksum = reader.ReadUInt32();
					chunk.Checksum = checksum;
				}

				chunked.Chunks.Add(chunk);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ChunkedObjectModel chunked = (objectModel as ChunkedObjectModel);
			if (chunked == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.Endianness = Endianness;

			if (ExpectedSignature != null)
			{
				writer.WriteBytes(ExpectedSignature);
			}

			int chunkLengthOffset = 0;
			if (ChunkLengthType == ISOMediaBaseChunkLengthType.IncludeChunkLengthAndID)
			{
				chunkLengthOffset = 8;
			}
			else if (ChunkLengthType == ISOMediaBaseChunkLengthType.IncludeChunkID)
			{
				chunkLengthOffset = 4;
			}

			foreach (RIFFChunk chunk in chunked.Chunks)
			{
				if (chunk is RIFFDataChunk)
				{
					RIFFDataChunk data = ((RIFFDataChunk)chunk);
					uint boxSize = (uint)data.Size;
					string boxName = data.ID;
					if (ChunkLengthPosition == ISOMediaBaseChunkLengthPosition.BeforeChunkID)
					{
						writer.WriteUInt32(boxSize);
						writer.WriteFixedLengthString(boxName);
					}
					else if (ChunkLengthPosition == ISOMediaBaseChunkLengthPosition.AfterChunkID)
					{
						writer.WriteFixedLengthString(boxName);
						writer.WriteUInt32(boxSize);
					}

					byte[] chunkData = data.Source.GetData();
					writer.WriteBytes(chunkData);

					if (ChecksumPosition == ISOMediaBaseChecksumPosition.AfterChunkData)
					{
						uint checksum = CalculateChunkChecksum(chunkData);
						writer.WriteUInt32(checksum);
					}
				}
			}
		}

		protected virtual uint CalculateChunkChecksum(byte[] chunkData)
		{
			return 0;
		}
	}
}
