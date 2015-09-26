using System;
using System.Drawing;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Video;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Video.UEV
{
	public class UEVDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Universal Editor video", new byte?[][] { new byte?[] { new byte?(85), new byte?(69), new byte?(86), new byte?(105), new byte?(100), new byte?(101), new byte?(111), new byte?(90) } }, new string[] { "*.uev" });
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private Version mvarVersion = new Version();
		public Version Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VideoObjectModel vom = objectModel as VideoObjectModel;
			BinaryReader br = base.Stream.BinaryReader;
			string UEVideoZ = br.ReadFixedLengthString(8);
			if (!(UEVideoZ != "UEVideoZ"))
			{
				int versionMajor = br.ReadInt32();
				int versionMinor = br.ReadInt32();
				int versionBuild = br.ReadInt32();
				int versionRevision = br.ReadInt32();
				this.mvarVersion = new Version(versionMajor, versionMinor, versionBuild, versionRevision);
				int audioTrackCount = br.ReadInt32();
				for (int i = 0; i < audioTrackCount; i++)
				{
					int audioTrackFormat = br.ReadInt32();
					int audioTrackSize = br.ReadInt32();
				}
				int videoTrackCount = br.ReadInt32();
				for (int i = 0; i < videoTrackCount; i++)
				{
					int frameCount = br.ReadInt32();
					for (int j = 0; j < frameCount; j++)
					{
						int frameSize = br.ReadInt32();
						byte[] frameBytes = br.ReadBytes(frameSize);
						System.IO.MemoryStream ms = new System.IO.MemoryStream(frameBytes);
						Image frameImage = Image.FromStream(ms);
						ms.Close();
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			VideoObjectModel vom = objectModel as VideoObjectModel;
			BinaryWriter bw = base.Stream.BinaryWriter;
			bw.WriteFixedLengthString("UEVideoZ");
			bw.Write(this.mvarVersion.Major);
			bw.Write(this.mvarVersion.Minor);
			bw.Write(this.mvarVersion.Build);
			bw.Write(this.mvarVersion.Revision);
			bw.Write(vom.AudioTracks.Count);
			for (int i = 0; i < vom.AudioTracks.Count; i++)
			{
				AudioTrack audio = vom.AudioTracks[i];
				bw.Write(audio.ObjectModel.Header.BitsPerSample);
				bw.Write(audio.ObjectModel.Header.BlockAlignment);
				bw.Write(audio.ObjectModel.Header.ChannelCount);
				bw.Write(audio.ObjectModel.Header.DataRate);
				bw.Write(audio.ObjectModel.Header.FormatTag);
				bw.Write(audio.ObjectModel.Header.SampleRate);
				bw.Write(audio.ObjectModel.ExtendedHeader.Enabled);
				if (audio.ObjectModel.ExtendedHeader.Enabled)
				{
					bw.Write(audio.ObjectModel.ExtendedHeader.ChannelMask);
					bw.Write(audio.ObjectModel.ExtendedHeader.SubFormatGUID);
					bw.Write(audio.ObjectModel.ExtendedHeader.ValidBitsPerSample);
				}
				// bw.WriteArray<short>(audio.ObjectModel.RawSamples);
				throw new NotImplementedException();
			}
			bw.Write(vom.VideoTracks.Count);
			for (int i = 0; i < vom.VideoTracks.Count; i++)
			{
				VideoTrack video = vom.VideoTracks[i];
				bw.Write(video.Frames.Count);
				for (int j = 0; j < video.Frames.Count; j++)
				{
					VideoFrame frame = video.Frames[j];
				}
			}
		}
	}
}
