using System;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Synthesized.MIDI
{
	public class MIDICommand
	{
		private byte mvarChannel = 0;
		private byte mvarCommand = 0;
		public MIDICommandType CommandType
		{
			get
			{
				MIDICommandType result;
				switch (this.mvarCommand)
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
						this.mvarCommand = 0;
						return;
					}
					case MIDICommandType.NoteOff:
					{
						this.mvarCommand = 8;
						return;
					}
					case MIDICommandType.NoteOn:
					{
						this.mvarCommand = 9;
						return;
					}
					case MIDICommandType.KeyAfterTouch:
					{
						this.mvarCommand = 10;
						return;
					}
					case MIDICommandType.ControlChange:
					{
						this.mvarCommand = 11;
						return;
					}
					case MIDICommandType.ProgramChange:
					{
						this.mvarCommand = 12;
						return;
					}
					case MIDICommandType.ChannelAfterTouch:
					{
						this.mvarCommand = 13;
						return;
					}
					case MIDICommandType.PitchWheelChange:
					{
						this.mvarCommand = 14;
						return;
					}
				}
				this.mvarCommand = 0;
			}
		}
		public byte Channel
		{
			get
			{
				return this.mvarChannel;
			}
			set
			{
				this.mvarChannel = value;
			}
		}
		public byte Command
		{
			get
			{
				return this.mvarCommand;
			}
			set
			{
				this.mvarCommand = value;
			}
		}
	}
}
