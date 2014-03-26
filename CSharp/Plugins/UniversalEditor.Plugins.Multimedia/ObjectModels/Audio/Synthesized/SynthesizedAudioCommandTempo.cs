using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioCommandTempo : SynthesizedAudioCommand
	{
		private double mvarTempo = 0.0;
		public double Tempo
		{
			get
			{
				return this.mvarTempo;
			}
			set
			{
				this.mvarTempo = value;
			}
		}
		public SynthesizedAudioCommandTempo()
		{
		}
		public SynthesizedAudioCommandTempo(double tempo)
		{
			this.mvarTempo = tempo;
		}
		public override string ToString()
		{
			return "MM = " + this.mvarTempo.ToString() + " BPM";
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandTempo
			{
				Tempo = this.mvarTempo
			};
		}
	}
}
