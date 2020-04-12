//
//  FLACDataFormat.cs - provides a DataFormat for manipulating waveform audio in Free Lossless Audio Codec (FLAC) format
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
using UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC.Internal;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.FLAC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating waveform audio in Free Lossless Audio Codec (FLAC) format.
	/// </summary>
	public class FLACDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		/// <summary>
		/// Gets a collection of <see cref="FLACMetadataBlock" /> instances representing the blocks of metadata describing this FLAC file.
		/// </summary>
		/// <value>The blocks of metadata describing this FLAC file.</value>
		public FLACMetadataBlock.FLACMetadataBlockCollection MetadataBlocks { get; } = new FLACMetadataBlock.FLACMetadataBlockCollection();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		private void SaveMetadataBlockHeader(Writer bw, FLACMetadataBlockHeader header)
		{
			byte flagAndType = 0;
			if (header.IsLastMetadataBlock)
			{
				flagAndType |= 1;
			}
			flagAndType |= (byte)header.BlockType;
			bw.WriteByte(flagAndType);
			bw.WriteInt24(header.ContentLength);
		}
		private void SaveMetadataBlockStreamInfo(Writer bw, FLACMetadataBlockStreamInfo block)
		{
			this.SaveMetadataBlockHeader(bw, new FLACMetadataBlockHeader
			{
				IsLastMetadataBlock = this.MetadataBlocks.Count == 0, 
				BlockType = FLACMetadataBlockType.StreamInfo, 
				ContentLength = 0
			});
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("fLaC");
			FLACMetadataBlockStreamInfo streamInfo = new FLACMetadataBlockStreamInfo();
			this.SaveMetadataBlockStreamInfo(bw, streamInfo);
			bw.Flush();
		}
	}
}
