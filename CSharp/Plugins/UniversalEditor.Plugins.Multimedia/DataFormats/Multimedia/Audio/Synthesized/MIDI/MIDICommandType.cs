using System;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.MIDI
{
	public enum MIDICommandType
	{
		Unknown = -1,
		None,
		NoteOff = 8,
		NoteOn,
		KeyAfterTouch,
		ControlChange,
		ProgramChange,
		ChannelAfterTouch,
		PitchWheelChange
	}
}
