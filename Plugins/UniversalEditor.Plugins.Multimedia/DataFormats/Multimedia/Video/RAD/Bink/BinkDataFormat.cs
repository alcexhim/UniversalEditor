//
//  BinkDataFormat.cs - provides a DataFormat for manipulating video in Bink (BIK) format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Video;

namespace UniversalEditor.DataFormats.Multimedia.Video.RAD.Bink
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating video in Bink (BIK) format.
	/// </summary>
	public class BinkDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			dfr.Sources.Add("http://wiki.multimedia.cx/index.php?title=Smacker");
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VideoObjectModel video = (objectModel as VideoObjectModel);
			if (video != null)
			{
				Reader br = base.Accessor.Reader;
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
				Writer bw = base.Accessor.Writer;
				bw.WriteFixedLengthString("BIK");
				byte videoCodecRevision = 98;
				bw.WriteByte(videoCodecRevision);
				int fileSize = 0;
				bw.WriteInt32(fileSize);
				int frameCount = 0;
				bw.WriteInt32(frameCount);
				int largestFrameSize = 0;
				bw.WriteInt32(largestFrameSize);
				bw.WriteInt32(frameCount);
				int videoWidth = 320;
				bw.WriteInt32(videoWidth);
				int videoHeight = 240;
				bw.WriteInt32(videoHeight);
				int videoFramesPerSecondDividend = 24;
				bw.WriteInt32(videoFramesPerSecondDividend);
				int videoFramesPerSecondDivisor = 1;
				bw.WriteInt32(videoFramesPerSecondDivisor);
				int videoFlags = 0;
				bw.WriteInt32(videoFlags);
				int audioTrackCount = 0;
				bw.WriteInt32(audioTrackCount);
				for (int i = 0; i < audioTrackCount; i++)
				{
					byte[] unknown = new byte[2];
					bw.WriteBytes(unknown);
					short audioChannels = 1;
					bw.WriteInt16(audioChannels);
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					ushort audioSampleRate = 44100;
					bw.WriteUInt16(audioSampleRate);
					short flags = 0;
					bw.WriteInt16(flags);
				}
				for (int i = 0; i < audioTrackCount; i++)
				{
					int audioTrackID = 0;
					bw.WriteInt32(audioTrackID);
				}
				bw.Flush();
			}
		}
	}
}
