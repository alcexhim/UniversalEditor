//
//  SynthesizedAudioCommandNote.cs - represents a note command in a synthesized audio file
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
	/// Represents a note command in a synthesized audio file.
	/// </summary>
	public class SynthesizedAudioCommandNote : SynthesizedAudioCommand
	{
		public bool Protected { get; set; } = false;
		public int Position { get; set; } = 0;
		public double Length { get; set; } = 0;
		public string Phoneme { get; set; } = null;
		public string Lyric { get; set; } = null;
		public double Frequency { get; set; } = 0;
		public int PreUtterance { get; set; } = 0;
		public int VoiceOverlap { get; set; } = 0;
		public int Intensity { get; set; } = 0;
		public int Modulation { get; set; } = 0;
		public int PBType { get; set; } = 0;
		public double[] Pitches { get; set; } = new double[0];
		public string[] Envelope { get; set; } = new string[0];
		public double[] VBR { get; set; } = new double[0];
		public int Accent { get; set; } = 50;
		public int PitchBendDepth { get; set; } = 8;
		public int PitchBendLength { get; set; } = 0;
		public int Decay { get; set; } = 50;
		public bool PortamentoFalling { get; set; } = false;
		public int Opening { get; set; } = 127;
		public bool PortamentoRising { get; set; } = false;
		public int VibratoLength { get; set; } = 0;
		public SynthesizedAudioVibratoType VibratoType { get; set; } = SynthesizedAudioVibratoType.None;

		public override object Clone()
		{
			return new SynthesizedAudioCommandNote
			{
				Envelope = this.Envelope.Clone() as string[],
				Intensity = this.Intensity,
				Length = this.Length,
				Lyric = this.Lyric,
				Modulation = this.Modulation,
				PBType = this.PBType,
				Phoneme = this.Phoneme,
				Pitches = this.Pitches.Clone() as double[],
				PortamentoFalling = this.PortamentoFalling,
				PortamentoRising = this.PortamentoRising,
				PreUtterance = this.PreUtterance,
				Protected = this.Protected,
				Position = this.Position,
				Frequency = this.Frequency,
				VBR = this.VBR.Clone() as double[],
				VoiceOverlap = this.VoiceOverlap
			};
		}
	}
}
