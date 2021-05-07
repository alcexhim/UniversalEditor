//
//  VoicebankSynthesisParameters.cs - represents parameters for vocal synthesis in a synthesizer voicebank file
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

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	/// <summary>
	/// Represents parameters for vocal synthesis in a synthesizer voicebank file.
	/// </summary>
	public class VoicebankSynthesisParameters
	{
		public byte Breathiness { get; set; } = 0;
		public byte Brightness { get; set; } = 0;
		public byte Clearness { get; set; } = 0;
		public byte GenderFactor { get; set; } = 0;
		public byte Openness { get; set; } = 0;

		public void Clear()
		{
			Breathiness = 0;
			Brightness = 0;
			Clearness = 0;
			GenderFactor = 0;
			Openness = 0;
		}
	}
}
