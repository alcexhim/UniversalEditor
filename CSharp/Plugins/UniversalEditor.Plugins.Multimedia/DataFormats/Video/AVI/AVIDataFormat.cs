using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Chunked;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Video;
using UniversalEditor.ObjectModels.Chunked;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Video.AVI
{
	public class AVIDataFormat : RIFFDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
            dfr.Clear();
			dfr.Filters.Add("Audio/Video Interleaved", new string[] { "*.avi" });
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

            if (aviChunk == null) throw new DataFormatException();
		}
	}
}
