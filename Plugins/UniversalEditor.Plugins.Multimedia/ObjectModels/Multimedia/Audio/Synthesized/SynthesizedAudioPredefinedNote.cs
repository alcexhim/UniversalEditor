using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
    public enum SynthesizedAudioPredefinedNote
    {
        A = 0,
        ASharp,
        BFlat = ASharp,
        B,
        C,
        CSharp,
        DFlat = CSharp,
        D,
        DSharp,
        EFlat = DSharp,
        E,
        F,
        FSharp,
        GFlat = FSharp,
        G,
        GSharp,
        AFlat = GSharp
    }
	public static class SynthesizedAudioPredefinedNoteConverter
	{
		public static double GetFrequency(SynthesizedAudioPredefinedNote note, int octave)
		{
			return GetFrequency((int)note, octave);
		}
		public static double GetFrequency(int note, int octave)
		{
			return GetFrequency(note + ((octave - 4) * 12));
		}
		public static double GetFrequency(int note)
		{
			/*
				C5 = the C an octave above middle C. This is 3 half steps above A4 and so the frequency is
				f3 = 440 * (1.059463..)^3 = 523.3 Hz
				If your calculator does not have the ability to raise to powers, then use the fact that
				(1.059463..)3 = (1.059463..)*(1.059463..)*(1.059463..)
				That is, you multiply it by itself 3 times.

				Middle C is 9 half steps below A4 and the frequency is:
				f -9 = 440 * (1.059463..)-9 = 261.6 Hz
				If you don't have powers on your calculator, remember that the negative sign on the power means you divide instead of multiply. For this example, you divide by (1.059463..) 9 times. 
			 */

			double d = Math.Pow(2, (double)1 / 12);
			int halfSteps = note;
			double value = 440 * Math.Pow(d, halfSteps);
			return value;
		}

		public static int GetNote(double frequency)
		{
			double d = Math.Pow(2, (double)1 / 12);
			double halfSteps = Math.Pow(frequency / 440, (1 / d));
			int note = (int)Math.Round((halfSteps - 1) * 12) + 1;
			return note;
		}

		public static double ChangeFrequency(double oldFrequency, int detuneBy)
		{
			if (detuneBy == 0) return oldFrequency;

			int note = GetNote(oldFrequency);
			double freq = GetFrequency(note + detuneBy);
			return freq;
		}
	}
}
