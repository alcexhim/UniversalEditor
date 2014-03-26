using System;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
using UniversalEditor.ObjectModels.Multimedia.Video;
namespace UniversalEditor.DataFormats.Multimedia.Video.ROQ
{
	public class ROQDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("id software RoQ video", new byte?[][] { new byte?[] { new byte?(132), new byte?(16), new byte?(255), new byte?(255), new byte?(255), new byte?(255), new byte?(30), new byte?(0) } }, new string[] { "*.roq" });
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			dfr.Sources.Add("http://multimedia.cx/mirror/idroq.txt");
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			byte[] signature = br.ReadBytes(8u);
			VideoObjectModel vom = objectModel as VideoObjectModel;
			WaveformAudioObjectModel wave = new WaveformAudioObjectModel();
			AudioTrack audio = new AudioTrack();
			VideoTrack video = new VideoTrack();
			video.FrameRate = 30;
			wave.Header.BitsPerSample = 16;
			wave.Header.SampleRate = 22050;
			while (!br.EndOfStream)
			{
				ROQChunk chunk = new ROQChunk();
				chunk.ID = br.ReadInt16();
				int size = br.ReadInt32();
				chunk.Argument = br.ReadInt16();
				chunk.Data = br.ReadBytes(size);
				short iD = chunk.ID;
				switch (iD)
				{
					case 4097:
					{
						Reader brch = new Reader(new MemoryAccessor(chunk.Data));
						video.Width = (int)brch.ReadInt16();
						video.Height = (int)brch.ReadInt16();
						video.BlockDimension = (int)brch.ReadInt16();
						video.SubBlockDimension = (int)brch.ReadInt16();
						brch.Close();
						break;
					}
					case 4098:
					{
						break;
					}
					default:
					{
						if (iD != 4113)
						{
							switch (iD)
							{
							}
						}
						break;
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
