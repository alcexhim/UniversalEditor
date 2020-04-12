//
//  SynthesizedAudioPredefinedNote.cs - defines various commonly-used note pitches for synthesized audio files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Defines various commonly-used note pitches for synthesized audio files.
	/// </summary>
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
	/// <summary>
	/// Converts to and from <see cref="SynthesizedAudioPredefinedNote" /> values and their associated frequencies.
	/// </summary>
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
				(1.059463..)^3 = (1.059463..)*(1.059463..)*(1.059463..)
				That is, you multiply it by itself 3 times.

				Middle C is 9 half steps below A4 and the frequency is:
				f -9 = 440 * (1.059463..)^(-9) = 261.6 Hz
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
