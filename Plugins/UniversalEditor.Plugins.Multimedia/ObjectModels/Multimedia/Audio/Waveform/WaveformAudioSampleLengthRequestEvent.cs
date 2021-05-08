using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioSampleLengthRequestEventArgs : EventArgs
	{
		public int Length { get; set; } = 0;
		public WaveformAudioSampleLengthRequestEventArgs()
		{
		}
	}
}
