//
//  AVIDataFormat.cs - provides a DataFormat for manipulating video in Audio/Video Interleaved (AVI) format
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

using System.Collections.Generic;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Video;

namespace UniversalEditor.DataFormats.Multimedia.Video.AVI
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating video in Audio/Video Interleaved (AVI) format.
	/// </summary>
	public class AVIDataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(VideoObjectModel), DataFormatCapabilities.All);
			return dfr;
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
			VideoObjectModel video = objectModels.Pop() as VideoObjectModel;

			RIFFChunk aviChunk = riff.Chunks["AVI "];

			if (aviChunk == null) throw new InvalidDataFormatException("Could not read initial AVI chunk");
		}
	}
}
