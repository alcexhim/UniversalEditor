//
//  MIDIEventType.cs - indicates the type of MIDI event
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI
{
	/// <summary>
	/// Indicates the type of MIDI event.
	/// </summary>
	public enum MIDIEventType : byte
	{
		None,

		MIDIChannelMask = 0x0F,
		MIDIEventMask = 0xF0,

		NoteOff = 0x8,
		NoteOn = 0x9,
		PolyphonicKeyPressureAftertouch = 0xA,
		ControlChange = 0xB,
		ProgramChange = 0xC,
		ChannelPressureAftertouch = 0xD,
		PitchWheelChange = 0xE,


		SysEx = 0xF0,
		Escape = 0xF7,
		Meta = 0xFF
	}
}
