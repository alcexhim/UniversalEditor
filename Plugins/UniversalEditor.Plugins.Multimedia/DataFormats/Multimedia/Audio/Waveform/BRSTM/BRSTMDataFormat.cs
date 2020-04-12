//
//  BRSTMDataFormat.cs - provides a DataFormat for manipulating waveform audio files in BRSTM format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.BRSTM
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating waveform audio files in BRSTM format.
	/// </summary>
	public class BRSTMDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel auom = objectModel as WaveformAudioObjectModel;
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			string RSTM = br.ReadFixedLengthString(4);
			ushort magic = br.ReadUInt16();
			byte versionMajor = br.ReadByte();
			byte versionMinor = br.ReadByte();
			uint fileSize = br.ReadUInt32();
			ushort headerSize = br.ReadUInt16();
			ushort chunkCount = br.ReadUInt16();
			uint headChunkOffset = br.ReadUInt32();
			uint headChunkSize = br.ReadUInt32();
			uint adpcChunkOffset = br.ReadUInt32();
			uint adpcChunkSize = br.ReadUInt32();
			uint dataChunkOffset = br.ReadUInt32();
			uint dataChunkSize = br.ReadUInt32();
			byte[] reserved = br.ReadBytes(24u);
			base.Accessor.Seek((long)headChunkOffset, SeekOrigin.Begin);
			byte[] headChunk = br.ReadBytes(headChunkSize);
			base.Accessor.Seek((long)adpcChunkOffset, SeekOrigin.Begin);
			byte[] adpcChunk = br.ReadBytes(adpcChunkSize);
			base.Accessor.Seek((long)dataChunkOffset, SeekOrigin.Begin);
			byte[] dataChunk = br.ReadBytes(dataChunkSize);
			byte[] dataChunkMinusHeader = new byte[dataChunk.Length - 4];
			Array.Copy(dataChunk, 4, dataChunkMinusHeader, 0, dataChunkMinusHeader.Length);
			auom.RawData = dataChunkMinusHeader;
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel auom = objectModel as WaveformAudioObjectModel;
			Writer bw = base.Accessor.Writer;
			bw.Endianness = Endianness.BigEndian;
			bw.WriteFixedLengthString("RSTM");
			ushort magic = 65279;
			bw.WriteUInt16(magic);
			byte versionMajor = 1;
			bw.WriteByte(versionMajor);
			byte versionMinor = 0;
			bw.WriteByte(versionMinor);
			ushort headerSize = 40;
			bw.WriteUInt16(headerSize);
			uint fileSize = (uint)headerSize;
			bw.WriteUInt32(fileSize);
			ushort chunkCount = 0;
			bw.WriteUInt16(chunkCount);
			uint headChunkOffset = 0u;
			bw.WriteUInt32(headChunkOffset);
			uint headChunkSize = 0u;
			bw.WriteUInt32(headChunkSize);
			uint adpcChunkOffset = 0u;
			bw.WriteUInt32(adpcChunkOffset);
			uint adpcChunkSize = 0u;
			bw.WriteUInt32(adpcChunkSize);
			uint dataChunkOffset = 0u;
			bw.WriteUInt32(dataChunkOffset);
			uint dataChunkSize = 0u;
			bw.WriteUInt32(dataChunkSize);
			bw.Flush();
		}
	}
}
