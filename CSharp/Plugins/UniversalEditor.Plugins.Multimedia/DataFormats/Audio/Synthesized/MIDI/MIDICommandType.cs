using System;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Synthesized.MIDI
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
