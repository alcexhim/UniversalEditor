using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioSampleRequestEventArgs : EventArgs
	{
		public int Offset { get; set; } = 0;
		public int Length { get; set; } = 0;

		public short[] Samples { get; set; } = null;

		public WaveformAudioSampleRequestEventArgs(int offset, int length)
		{
			Offset = offset;
			Length = length;
		}
	}
}
