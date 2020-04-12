//
//  MIDICommand.cs - represents a command in a MIDI synthesized audio file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
	/// Represents a command in a MIDI synthesized audio file.
	/// </summary>
	public class MIDICommand
	{
		/// <summary>
		/// Gets or sets the type of the <see cref="MIDICommand" /> to send.
		/// </summary>
		/// <value>The type of the <see cref="MIDICommand" /> to send.</value>
		public MIDICommandType CommandType
		{
			get
			{
				MIDICommandType result;
				switch (Command)
				{
					case 0:
					{
						result = MIDICommandType.None;
						return result;
					}
					case 8:
					{
						result = MIDICommandType.NoteOff;
						return result;
					}
					case 9:
					{
						result = MIDICommandType.NoteOn;
						return result;
					}
					case 10:
					{
						result = MIDICommandType.KeyAfterTouch;
						return result;
					}
					case 11:
					{
						result = MIDICommandType.ControlChange;
						return result;
					}
					case 12:
					{
						result = MIDICommandType.ProgramChange;
						return result;
					}
					case 13:
					{
						result = MIDICommandType.ChannelAfterTouch;
						return result;
					}
					case 14:
					{
						result = MIDICommandType.PitchWheelChange;
						return result;
					}
				}
				result = MIDICommandType.Unknown;
				return result;
			}
			set
			{
				switch (value)
				{
					case MIDICommandType.None:
					{
						Command = 0;
						return;
					}
					case MIDICommandType.NoteOff:
					{
						Command = 8;
						return;
					}
					case MIDICommandType.NoteOn:
					{
						Command = 9;
						return;
					}
					case MIDICommandType.KeyAfterTouch:
					{
						Command = 10;
						return;
					}
					case MIDICommandType.ControlChange:
					{
						Command = 11;
						return;
					}
					case MIDICommandType.ProgramChange:
					{
						Command = 12;
						return;
					}
					case MIDICommandType.ChannelAfterTouch:
					{
						Command = 13;
						return;
					}
					case MIDICommandType.PitchWheelChange:
					{
						Command = 14;
						return;
					}
				}
				Command = 0;
			}
		}

		/// <summary>
		/// Gets or sets the channel on which to send this <see cref="MIDICommand" />.
		/// </summary>
		/// <value>The channel on which to send this <see cref="MIDICommand" />.</value>
		public byte Channel { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value of the <see cref="MIDICommand" /> to send.
		/// </summary>
		/// <value>The value of the <see cref="MIDICommand" /> to send.</value>
		public byte Command { get; set; } = 0;
	}
}
