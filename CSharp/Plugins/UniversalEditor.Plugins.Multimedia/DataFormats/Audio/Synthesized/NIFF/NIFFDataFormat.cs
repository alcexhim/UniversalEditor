using System;
using UniversalEditor.DataFormats.Chunked;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Synthesized;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Synthesized.NIFF
{
	public class NIFFDataFormat : RIFFDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Notation Interchange File Format", new byte?[][] { new byte?[] { new byte?(82), new byte?(73), new byte?(70), new byte?(88), null, null, null, null, new byte?(78), new byte?(73), new byte?(70), new byte?(70) } }, new string[] { "*.nif" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

	}
}
