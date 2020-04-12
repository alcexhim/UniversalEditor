//
//  SynthesizedAudioCommandTimeSignature.cs - represents a time signature change command in a synthesized audio file
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

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Represents a time signature change command in a synthesized audio file.
	/// </summary>
	public class SynthesizedAudioCommandTimeSignature : SynthesizedAudioCommand
	{
		public byte Numerator { get; set; } = 0;
		public byte Denominator { get; set; } = 0;
		public byte TicksPerMetronomeClick { get; set; } = 0;
		public byte NumberOf32ndNotesPerQuarterNote { get; set; } = 0;

		public SynthesizedAudioCommandTimeSignature()
		{
		}
		public SynthesizedAudioCommandTimeSignature(byte numerator, byte denominator, byte ticksPerMetronomeClick, byte numberOf32ndNotesPerQuarterNote)
		{
			this.Numerator = numerator;
			this.Denominator = denominator;
			this.TicksPerMetronomeClick = ticksPerMetronomeClick;
			this.NumberOf32ndNotesPerQuarterNote = numberOf32ndNotesPerQuarterNote;
		}
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"TS = ",
				this.Numerator.ToString(),
				"/",
				this.Denominator.ToString(),
				"; ♪ = ",
				this.NumberOf32ndNotesPerQuarterNote.ToString()
			});
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandTimeSignature
			{
				Denominator = this.Denominator,
				NumberOf32ndNotesPerQuarterNote = this.NumberOf32ndNotesPerQuarterNote,
				Numerator = this.Numerator,
				TicksPerMetronomeClick = this.TicksPerMetronomeClick
			};
		}
	}
}
