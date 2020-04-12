//
//  WaveformAudioHeader.cs - describes the characteristics of a waveform audio file
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

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	/// <summary>
	/// Describes the characteristics of a waveform audio file.
	/// </summary>
	public class WaveformAudioHeader
	{
		public ushort FormatTag { get; set; } = 1;

		public WaveformAudioKnownFormat Format
		{
			get { return (WaveformAudioKnownFormat)FormatTag; }
			set { FormatTag = (ushort)value; }
		}

		public short ChannelCount { get; set; } = 1;
		public int SampleRate { get; set; } = 44100;
		public int DataRate { get; set; } = 88200;

		public short BlockAlignment { get; set; } = 2;
		public short BitsPerSample { get; set; } = 16;
	}
}
