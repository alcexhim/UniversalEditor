using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Video;

namespace UniversalEditor.DataFormats.Multimedia.Video.AVI
{
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
