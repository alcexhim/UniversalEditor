//
//  RIFFDataFormat.cs - provides a DataFormat for manipulating chunked binary data in Resource Interchange File Format (RIFF)
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

using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;

namespace UniversalEditor.DataFormats.Chunked.RIFF
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating chunked binary data in Resource Interchange File Format (RIFF).
	/// </summary>
	public class RIFFDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();

			dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private string[] mvarRIFFtagsLittleEndian = new string[]
		{
			"RIFF",
			"RIFX"
		};
		public virtual string[] RIFFTagsLittleEndian
		{
			get
			{
				return this.mvarRIFFtagsLittleEndian;
			}
		}

		private string[] mvarRIFFtagsBigEndian = new string[]
		{
			"FORM",
			"LIST",
			"CAT "
		};
		public virtual string[] RIFFTagsBigEndian
		{
			get
			{
				return this.mvarRIFFtagsBigEndian;
			}
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ChunkedObjectModel riff = objectModel as ChunkedObjectModel;
			if (riff == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string tagRIFF = br.ReadFixedLengthString(4);

			bool found = false;
			for (int i = 0; i < RIFFTagsLittleEndian.Length; i++)
			{
				string w = RIFFTagsLittleEndian[i];
				if (w == tagRIFF)
				{
					found = true;
					break;
				}
			}
			if (!found)
			{
				for (int i = 0; i < RIFFTagsBigEndian.Length; i++)
				{
					if (RIFFTagsBigEndian[i] == tagRIFF)
					{
						br.Endianness = Endianness.BigEndian;
						found = true;
						break;
					}
				}
			}

			if (!found)
			{
				throw new InvalidDataFormatException("File does not begin with one of the chunked tags for this data format");
			}

			Accessor.Seek(-4, SeekOrigin.Current);

			RIFFChunk chunk = null;
			int l = 0;
			while (!br.EndOfStream)
			{
				chunk = ReadChunk(br, out l);
				if (chunk != null) riff.Chunks.Add(chunk);
			}
		}

		private RIFFChunk ReadChunk(IO.Reader br, out int length)
		{
			try
			{
				string typeID = br.ReadFixedLengthString(4);
				switch (typeID)
				{
					case "RIFF":
					case "RIFX":
					case "CAT ":
					case "FORM":
					case "LIST":
					{
						RIFFGroupChunk chunk = new RIFFGroupChunk();
						chunk.TypeID = typeID;

						length = br.ReadInt32();

						string id = br.ReadFixedLengthString(4);
						chunk.ID = id;

						long chunkPos = 12;
						while (chunkPos < length)
						{
							int l = 0;
							RIFFChunk chunkChild = ReadChunk(br, out l);
							chunkPos += l;
							chunk.Chunks.Add(chunkChild);

							if (br.EndOfStream) return chunk;
						}

						return chunk;
					}
					default:
					{
						RIFFDataChunk chunk = new RIFFDataChunk();
						length = br.ReadInt32();

						byte[] data = br.ReadBytes(length);
						chunk.ID = typeID;
						chunk.Data = data;
						return chunk;
					}
				}
			}
			catch (System.IO.EndOfStreamException)
			{
				length = 0;
				return null;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			ChunkedObjectModel riff = (objectModel as ChunkedObjectModel);
			if (riff == null) throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;

			foreach (RIFFChunk chunk in riff.Chunks)
			{
				this.WriteChunk(chunk, bw);
			}
			bw.Flush();
		}
		private void WriteChunk(RIFFChunk chunk, IO.Writer bw)
		{
			if (chunk is RIFFGroupChunk)
			{
				RIFFGroupChunk gchunk = (chunk as RIFFGroupChunk);
				bw.WriteFixedLengthString(gchunk.TypeID.PadRight(4, ' '));
				bw.WriteInt32(gchunk.Size);
				bw.WriteFixedLengthString(gchunk.ID.PadRight(4, ' '));
				foreach (RIFFChunk subChunk in gchunk.Chunks)
				{
					WriteChunk(subChunk, bw);
				}
			}
			else if (chunk is RIFFDataChunk)
			{
				RIFFDataChunk dchunk = (chunk as RIFFDataChunk);
				bw.WriteFixedLengthString(dchunk.ID.PadRight(4, ' '));
				bw.WriteInt32(dchunk.Size);
				if (dchunk.Data != null)
				{
					bw.WriteBytes(dchunk.Data);
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			/*
			RIFFObjectModel riff = (objectModels.Pop() as RIFFObjectModel);
			if (riff.Chunks.Count > 0 && riff.Information.Count > 0)
			{
				RIFFGroupChunk chunkInfo = new RIFFGroupChunk();
				chunkInfo.TypeID = "LIST";
				chunkInfo.ID = "INFO";
				foreach (RIFFMetadataItem item in riff.Information)
				{
					RIFFDataChunk chunkData = new RIFFDataChunk();
					chunkData.ID = item.Name;
					if (item.Value != null)
					{
						System.IO.MemoryStream ms = new System.IO.MemoryStream();
						Writer bw = new Writer(ms);
						if (item.Value is string)
						{
							bw.WriteFixedLengthString((string)item.Value, System.Text.Encoding.UTF8);
						}
						else if (item.Value is bool)
						{
							bw.Write((bool)item.Value);
						}
						else if (item.Value is byte)
						{
							bw.Write((byte)item.Value);
						}
						else if (item.Value is byte[])
						{
							bw.Write((byte[])item.Value);
						}
						else if (item.Value is short)
						{
							bw.Write((short)item.Value);
						}
						else if (item.Value is int)
						{
							bw.Write((int)item.Value);
						}
						else if (item.Value is long)
						{
							bw.Write((long)item.Value);
						}
						else if (item.Value is ushort)
						{
							bw.Write((ushort)item.Value);
						}
						else if (item.Value is uint)
						{
							bw.Write((uint)item.Value);
						}
						else if (item.Value is ulong)
						{
							bw.Write((ulong)item.Value);
						}
						else if (item.Value is float)
						{
							bw.Write((float)item.Value);
						}
						else if (item.Value is double)
						{
							bw.Write((double)item.Value);
						}
						else if (item.Value is Guid)
						{
							bw.Write((Guid)item.Value);
						}
						else if (item.Value is DateTime)
						{
							bw.Write((DateTime)item.Value);
						}
						bw.Close();
						chunkData.Data = ms.ToArray();
						chunkInfo.Chunks.Add(chunkData);
					}
				}
				riff.Chunks.Add(chunkInfo);
			}
			objectModels.Push(riff);
			*/
		}

	}
}
