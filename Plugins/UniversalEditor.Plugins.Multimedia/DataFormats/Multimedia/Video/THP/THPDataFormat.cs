//
//  THPDataFormat.cs - provides a DataFormat for manipulating video files in THP format
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
using UniversalEditor.ObjectModels.Multimedia.Video;

namespace UniversalEditor.DataFormats.Multimedia.Video.THP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating video files in THP format.
	/// </summary>
	public class THPDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			uint version = br.ReadUInt32();
			uint maxBufferSize = br.ReadUInt32();
			uint maxAudioSamples = br.ReadUInt32();
			float fps = br.ReadSingle();
			uint numFrames = br.ReadUInt32();
			uint firstFrameSize = br.ReadUInt32();
			uint dataSize = br.ReadUInt32();
			uint componentDataOffset = br.ReadUInt32();
			uint offsetsDataOffset = br.ReadUInt32();
			uint firstFrameOffset = br.ReadUInt32();
			uint lastFrameOffset = br.ReadUInt32();
			br.Accessor.Seek((long)((ulong)componentDataOffset), SeekOrigin.Begin);
			uint numComponents = br.ReadUInt32();
			byte[] componentTypes = new byte[(int)((UIntPtr)numComponents)];
			for (uint i = 0u; i < numComponents; i += 1u)
			{
				componentTypes[(int)((UIntPtr)i)] = br.ReadByte();
				switch (componentTypes[(int)((UIntPtr)i)])
				{
					case 0:
					{
						uint width = br.ReadUInt32();
						uint height = br.ReadUInt32();
						if (version == 69632u)
						{
							uint unknown = br.ReadUInt32();
						}
						break;
					}
					case 1:
					{
						uint numChannels = br.ReadUInt32();
						uint frequency = br.ReadUInt32();
						uint numSamples = br.ReadUInt32();
						uint numData = br.ReadUInt32();
						break;
					}
				}
			}
			br.Accessor.Seek((long)((ulong)firstFrameOffset), SeekOrigin.Begin);
			uint nextTotalSize = br.ReadUInt32();
			uint prevTotalSize = br.ReadUInt32();
			uint imageSize = br.ReadUInt32();
			if (maxAudioSamples != 0u)
			{
				uint audioSize = br.ReadUInt32();
			}
			uint channelSize = br.ReadUInt32();
			uint numSamples2 = br.ReadUInt32();
			ushort[] table = br.ReadUInt16Array(16);
			ushort[] table2 = br.ReadUInt16Array(16);
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
