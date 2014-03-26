using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.BRSTM
{
	public class BRSTMDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("BRSTM/ADPCM sound data", new byte?[][] { new byte?[] { new byte?(77), new byte?(84), new byte?(83), new byte?(82), new byte?(255), new byte?(254) } }, new string[] { "*.brstm" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel auom = objectModel as WaveformAudioObjectModel;
			BinaryReader br = base.Stream.BinaryReader;
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
			base.Stream.BaseStream.Seek((long)((ulong)headChunkOffset), System.IO.SeekOrigin.Begin);
			byte[] headChunk = br.ReadBytes(headChunkSize);
			base.Stream.BaseStream.Seek((long)((ulong)adpcChunkOffset), System.IO.SeekOrigin.Begin);
			byte[] adpcChunk = br.ReadBytes(adpcChunkSize);
			base.Stream.BaseStream.Seek((long)((ulong)dataChunkOffset), System.IO.SeekOrigin.Begin);
			byte[] dataChunk = br.ReadBytes(dataChunkSize);
			byte[] dataChunkMinusHeader = new byte[dataChunk.Length - 4];
			Array.Copy(dataChunk, 4, dataChunkMinusHeader, 0, dataChunkMinusHeader.Length);
			auom.RawData = dataChunkMinusHeader;
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel auom = objectModel as WaveformAudioObjectModel;
			BinaryWriter bw = base.Stream.BinaryWriter;
			bw.Endianness = Endianness.BigEndian;
			bw.WriteFixedLengthString("RSTM");
			ushort magic = 65279;
			bw.Write(magic);
			byte versionMajor = 1;
			bw.Write(versionMajor);
			byte versionMinor = 0;
			bw.Write(versionMinor);
			ushort headerSize = 40;
			uint fileSize = (uint)headerSize;
			bw.Write(fileSize);
			bw.Write(headerSize);
			ushort chunkCount = 0;
			bw.Write(chunkCount);
			uint headChunkOffset = 0u;
			bw.Write(headChunkOffset);
			uint headChunkSize = 0u;
			bw.Write(headChunkSize);
			uint adpcChunkOffset = 0u;
			bw.Write(adpcChunkOffset);
			uint adpcChunkSize = 0u;
			bw.Write(adpcChunkSize);
			uint dataChunkOffset = 0u;
			bw.Write(dataChunkOffset);
			uint dataChunkSize = 0u;
			bw.Write(dataChunkSize);
			bw.Flush();
		}
	}
}
