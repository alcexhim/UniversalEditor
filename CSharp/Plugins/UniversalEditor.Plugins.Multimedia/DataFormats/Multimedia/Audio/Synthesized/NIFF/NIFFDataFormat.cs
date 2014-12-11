using System;

using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.DataFormats.Chunked.RIFF;

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.NIFF
{
	public class NIFFDataFormat : RIFFDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
            DataFormatReference dfr = new DataFormatReference(this.GetType());
			dfr.Filters.Add("Notation Interchange File Format", new byte?[][] { new byte?[] { new byte?(82), new byte?(73), new byte?(70), new byte?(88), null, null, null, null, new byte?(78), new byte?(73), new byte?(70), new byte?(70) } }, new string[] { "*.nif" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void BeforeLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel riff = (objectModels.Pop() as ChunkedObjectModel);
			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);

			throw new InvalidDataFormatException(Localization.StringTable.ErrorDataFormatInvalid);
		}
		protected override void BeforeSaveInternal(System.Collections.Generic.Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			SynthesizedAudioObjectModel audio = (objectModels.Pop() as SynthesizedAudioObjectModel);
			ChunkedObjectModel riff = new ChunkedObjectModel();

			objectModels.Push(riff);
		}
	}
}
