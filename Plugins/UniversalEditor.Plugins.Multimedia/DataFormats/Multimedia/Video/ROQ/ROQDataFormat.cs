//
//  ROQDataFormat.cs - provides a DataFormat for manipulating video files in Raven Software's ROQ format
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
using UniversalEditor.ObjectModels.Multimedia.Video;

namespace UniversalEditor.DataFormats.Multimedia.Video.ROQ
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating video files in Raven Software's ROQ format.
	/// </summary>
	public class ROQDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
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
