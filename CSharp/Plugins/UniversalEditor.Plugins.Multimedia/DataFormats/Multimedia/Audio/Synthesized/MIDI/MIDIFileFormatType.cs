//
//  MIDIFileFormatType.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI
{
	public enum MIDIFileFormatType : short
	{
		/// <summary>
		/// Single track (format 0). Consists of a header-chunk and a single track-chunk. The single track chunk will contain all the
		/// note and tempo information. 
		/// </summary>
		SingleTrack = 0,
		/// <summary>
		/// Simultaneous multi-track (format 1). Consists of a header-chunk and one or more track-chunks, with all tracks being played
		/// simultaneously. The first track of a Format 1 file is special, and is also known as the 'Tempo Map'. It should contain all
		/// meta-events of the types Time Signature, and Set Tempo. The meta-events Sequence/Track Name, Sequence Number, Marker, and
		/// SMTPE Offset. should also be on the first track of a Format 1 file.
		/// </summary>
		SimultaneousMultitrack = 1,
		/// <summary>
		/// Independent multi-track (format 2). Consists of a header-chunk and one or more track-chunks, where each track represents an
		/// independent sequence.
		/// </summary>
		IndependentMultitrack = 2
	}
}
