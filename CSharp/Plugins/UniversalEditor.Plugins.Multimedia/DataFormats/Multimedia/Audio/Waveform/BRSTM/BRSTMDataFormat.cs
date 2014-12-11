using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.BRSTM
{
	public class BRSTMDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("BRSTM/ADPCM sound data", new byte?[][] { new byte?[] { new byte?(77), new byte?(84), new byte?(83), new byte?(82), new byte?(255), new byte?(254) } }, new string[] { "*.brstm" });
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
