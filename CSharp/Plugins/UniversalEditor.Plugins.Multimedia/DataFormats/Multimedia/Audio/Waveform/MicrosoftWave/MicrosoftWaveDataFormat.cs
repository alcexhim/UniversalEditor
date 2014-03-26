using System;
using System.Collections.Generic;
using System.IO;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MicrosoftWave
{
	public class MicrosoftWaveDataFormat : RIFFDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Title = "Microsoft waveform audio";
			dfr.Filters.Add("Microsoft waveform audio", new byte?[][] { new byte?[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F', null, null, null, null, (byte)'W', (byte)'A', (byte)'V', (byte)'E', (byte)'f', (byte)'m', (byte)'t', (byte)' ' } }, new string[] { "*.wav", "*.wave" });
			dfr.Filters.Add("Sony ATRAC3+ encoded waveform audio", new string[] { "*.at3" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		// TODO: Test IsObjectModelSupported!!!

		protected override bool IsObjectModelSupported(ObjectModel omb)
		{
			ChunkedObjectModel riff = (omb as ChunkedObjectModel);
			if (riff != null)
			{
				RIFFGroupChunk waveChunk = (riff.Chunks["WAVE"] as RIFFGroupChunk);
				if (waveChunk != null)
				{
					RIFFDataChunk fmtChunk = (waveChunk.Chunks["fmt "] as RIFFDataChunk);
					if (fmtChunk != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			ChunkedObjectModel riff = objectModels.Pop() as ChunkedObjectModel;
			WaveformAudioObjectModel wave = objectModels.Pop() as WaveformAudioObjectModel;
			if (wave == null) throw new ObjectModelNotSupportedException();

			RIFFGroupChunk waveChunk = (riff.Chunks["WAVE"] as RIFFGroupChunk);
            if (waveChunk == null) throw new InvalidDataFormatException("File does not contain a \"WAVE\" chunk");

			RIFFDataChunk fmtChunk = (waveChunk.Chunks["fmt "] as RIFFDataChunk);
			UniversalEditor.IO.BinaryReader br = new UniversalEditor.IO.BinaryReader(fmtChunk.Data);
			if (fmtChunk.Size >= 16)
			{
				wave.Header.FormatTag = br.ReadUInt16();
				wave.Header.ChannelCount = br.ReadInt16();
				wave.Header.SampleRate = br.ReadInt32();
				wave.Header.DataRate = br.ReadInt32();
				wave.Header.BlockAlignment = br.ReadInt16();
				wave.Header.BitsPerSample = br.ReadInt16();
				if (fmtChunk.Size >= 18)
				{
					short cbSize = br.ReadInt16();
					if (fmtChunk.Size >= 40)
					{
						wave.ExtendedHeader.Enabled = true;
						wave.ExtendedHeader.ValidBitsPerSample = br.ReadInt16();
						wave.ExtendedHeader.ChannelMask = br.ReadInt32();
						wave.ExtendedHeader.SubFormatGUID = br.ReadGuid();
					}
				}
			}
			RIFFDataChunk dataChunk = (waveChunk.Chunks["data"] as RIFFDataChunk);
            if (dataChunk == null)
            {
                // TODO: FIX THIS UGLY HACK!!!
                RIFFGroupChunk infoChunk = (waveChunk.Chunks["INFO"] as RIFFGroupChunk);
                if (infoChunk != null)
                {
                    dataChunk = (infoChunk.Chunks["data"] as RIFFDataChunk);
                }
            }
			br = new UniversalEditor.IO.BinaryReader(dataChunk.Data);
			short[] dataa = new short[dataChunk.Data.Length / 2];
			for (int i = 0; i < dataa.Length; i++)
			{
				dataa[i] = br.ReadInt16();
			}
			wave.RawData = dataChunk.Data;
			wave.RawSamples = dataa;
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			WaveformAudioObjectModel wave = objectModels.Pop() as WaveformAudioObjectModel;
			ChunkedObjectModel riff = new ChunkedObjectModel();

			RIFFGroupChunk WAVE = new RIFFGroupChunk();
			WAVE.TypeID = "RIFF";
			WAVE.ID = "WAVE";

			RIFFDataChunk fmtChunk = new RIFFDataChunk();
			fmtChunk.ID = "fmt ";
			MemoryStream ms = new MemoryStream();
			UniversalEditor.IO.BinaryWriter bw = new UniversalEditor.IO.BinaryWriter(ms);
			bw.Write(wave.Header.FormatTag);
			bw.Write(wave.Header.ChannelCount);
			bw.Write(wave.Header.SampleRate);
			bw.Write(wave.Header.DataRate);
			bw.Write(wave.Header.BlockAlignment);
			bw.Write(wave.Header.BitsPerSample);
			if (wave.ExtendedHeader.Enabled)
			{
				bw.Write(22);
				bw.Write(wave.ExtendedHeader.ValidBitsPerSample);
				bw.Write(wave.ExtendedHeader.ChannelMask);
				bw.Write(wave.ExtendedHeader.SubFormatGUID);
			}
			bw.Flush();
			bw.Close();
			fmtChunk.Data = ms.ToArray();

			RIFFDataChunk dataChunk = new RIFFDataChunk();
			dataChunk.ID = "data";
			if (wave.RawData == null)
			{
				MemoryStream ms2 = new MemoryStream();
				UniversalEditor.IO.BinaryWriter bw2 = new UniversalEditor.IO.BinaryWriter(ms2);
				short[] rawSamples = wave.RawSamples;
				for (int i = 0; i < rawSamples.Length; i++)
				{
					short s = rawSamples[i];
					bw2.Write(s);
				}
				bw2.Flush();
				bw2.Close();
				wave.RawData = ms2.ToArray();
			}
			dataChunk.Data = wave.RawData;

			WAVE.Chunks.Add(fmtChunk);
			WAVE.Chunks.Add(dataChunk);
			
			riff.Chunks.Add(WAVE);
			objectModels.Push(riff);
		}
	}
}
