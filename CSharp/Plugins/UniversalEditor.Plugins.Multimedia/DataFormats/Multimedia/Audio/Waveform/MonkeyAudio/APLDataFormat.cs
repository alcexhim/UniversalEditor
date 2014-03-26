using System;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MonkeyAudio
{
	public class APLDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Monkey's Audio track metadata", new string[] { "*.apl" });
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
