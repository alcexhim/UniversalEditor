using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Video;
namespace UniversalEditor.DataFormats.Multimedia.Video.RAD.Bink
{
	public class BinkDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("RAD Video Tools Bink video", new byte?[][] { new byte?[] { new byte?(83), new byte?(77), new byte?(75), new byte?(50) }, new byte?[] { new byte?(83), new byte?(77), new byte?(75), new byte?(52) } }, new string[] { "*.bik" });
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			dfr.Sources.Add("http://wiki.multimedia.cx/index.php?title=Smacker");
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VideoObjectModel video = (objectModel as VideoObjectModel);
			if (video != null)
			{
				BinaryReader br = base.Stream.BinaryReader;
				string BIK = br.ReadFixedLengthString(3);
				byte videoCodecRevision = br.ReadByte();
				int fileSize = br.ReadInt32();
				int frameCount = br.ReadInt32();
				int largestFrameSize = br.ReadInt32();
				int frameCount2 = br.ReadInt32();
				int videoWidth = br.ReadInt32();
				int videoHeight = br.ReadInt32();
				int videoFramesPerSecondDividend = br.ReadInt32();
				int videoFramesPerSecondDivisor = br.ReadInt32();
				int videoFlags = br.ReadInt32();
				int audioTrackCount = br.ReadInt32();
				for (int i = 0; i < audioTrackCount; i++)
				{
					byte[] unknown = br.ReadBytes(2u);
					short audioChannels = br.ReadInt16();
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					ushort audioSampleRate = br.ReadUInt16();
					short flags = br.ReadInt16();
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					int audioTrackID = br.ReadInt32();
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			VideoObjectModel video = objectModel as VideoObjectModel;
			if (video != null)
			{
				BinaryWriter bw = base.Stream.BinaryWriter;
				bw.WriteFixedLengthString("BIK");
				byte videoCodecRevision = 98;
				bw.Write(videoCodecRevision);
				int fileSize = 0;
				bw.Write(fileSize);
				int frameCount = 0;
				bw.Write(frameCount);
				int largestFrameSize = 0;
				bw.Write(largestFrameSize);
				bw.Write(frameCount);
				int videoWidth = 320;
				bw.Write(videoWidth);
				int videoHeight = 240;
				bw.Write(videoHeight);
				int videoFramesPerSecondDividend = 24;
				bw.Write(videoFramesPerSecondDividend);
				int videoFramesPerSecondDivisor = 1;
				bw.Write(videoFramesPerSecondDivisor);
				int videoFlags = 0;
				bw.Write(videoFlags);
				int audioTrackCount = 0;
				bw.Write(audioTrackCount);
				for (int i = 0; i < audioTrackCount; i++)
				{
					byte[] unknown = new byte[2];
					bw.Write(unknown);
					short audioChannels = 1;
					bw.Write(audioChannels);
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					ushort audioSampleRate = 44100;
					bw.Write(audioSampleRate);
					short flags = 0;
					bw.Write(flags);
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					int audioTrackID = 0;
					bw.Write(audioTrackID);
				}
				bw.Flush();
			}
		}
	}
}
