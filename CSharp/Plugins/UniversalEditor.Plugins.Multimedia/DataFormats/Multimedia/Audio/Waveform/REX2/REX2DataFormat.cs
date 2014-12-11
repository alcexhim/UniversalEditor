using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.REX2
{
	public class REX2DataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();

			dfr.Filters.Add("Propellerhead ReCycle EXport", new byte?[][] { new byte?[] { new byte?(67), new byte?(65), new byte?(84), new byte?(32), null, null, null, null, new byte?(82), new byte?(69), new byte?(88), new byte?(50) } }, new string[] { "*.rex", "*.rx2" });
			dfr.Capabilities.Add(typeof(ChunkedObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private string[] mvarRIFFTags = new string[] { "CAT " };
		public override string[] RIFFTagsLittleEndian { get { return mvarRIFFTags; } }

		protected override bool IsObjectModelSupported(ObjectModel objectModel)
		{
			if (objectModel is ChunkedObjectModel)
			{
				ChunkedObjectModel riff = (objectModel as ChunkedObjectModel);
				// TODO: Figure this out
			}
			return base.IsObjectModelSupported(objectModel);
		}

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
