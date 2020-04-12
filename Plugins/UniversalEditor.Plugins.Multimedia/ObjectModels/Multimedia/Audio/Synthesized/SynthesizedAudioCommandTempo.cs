//
//  SynthesizedAudioCommandTempo.cs - represents a tempo change command in a synthesized audio file
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
	/// Represents a tempo change command in a synthesized audio file.
	/// </summary>
	public class SynthesizedAudioCommandTempo : SynthesizedAudioCommand
	{
		public double Tempo { get; set; } = 0.0;
		public SynthesizedAudioCommandTempo()
		{
		}
		public SynthesizedAudioCommandTempo(double tempo)
		{
			this.Tempo = tempo;
		}
		public override string ToString()
		{
			return "MM = " + this.Tempo.ToString() + " BPM";
		}
		public override object Clone()
		{
			return new SynthesizedAudioCommandTempo
			{
				Tempo = this.Tempo
			};
		}
	}
}
