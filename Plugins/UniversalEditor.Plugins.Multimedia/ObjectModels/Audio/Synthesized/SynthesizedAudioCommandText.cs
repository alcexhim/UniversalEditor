using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioCommandText : SynthesizedAudioCommand
	{
		private string mvarText = string.Empty;
		public string Text
		{
			get
			{
				return this.mvarText;
			}
			set
			{
				this.mvarText = value;
			}
		}
		public SynthesizedAudioCommandText()
		{
		}
		public SynthesizedAudioCommandText(string text)
		{
			this.mvarText = text;
		}
		public override string ToString()
		{
			return "\"" + this.mvarText + "\"";
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandText
			{
				Text = this.mvarText.Clone() as string
			};
		}
	}
}
