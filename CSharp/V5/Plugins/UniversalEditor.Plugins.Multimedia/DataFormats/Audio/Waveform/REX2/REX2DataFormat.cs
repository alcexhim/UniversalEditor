using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Chunked;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Waveform;
using UniversalEditor.ObjectModels.Chunked;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.REX2
{
	public class REX2DataFormat : RIFFDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();

			dfr.Filters.Add("Propellerhead ReCycle EXport", new byte?[][] { new byte?[] { new byte?(67), new byte?(65), new byte?(84), new byte?(32), null, null, null, null, new byte?(82), new byte?(69), new byte?(88), new byte?(50) } }, new string[] { "*.rex", "*.rx2" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private string[] mvarRIFFTags = new string[] { "CAT " };
		public override string[] RIFFTagsLittleEndian { get { return mvarRIFFTags; } }

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			ChunkedObjectModel rom = (objectModels.Pop() as ChunkedObjectModel);
			WaveformAudioObjectModel wave = (objectModels.Pop() as WaveformAudioObjectModel);
		}
	}
}
