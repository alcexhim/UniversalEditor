using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioCommand : ICloneable
	{
		public class SynthesizedAudioCommandCollection : Collection<SynthesizedAudioCommand>
		{
            public SynthesizedAudioCommandRest Add(double length)
            {
                SynthesizedAudioCommandRest rest = new SynthesizedAudioCommandRest();
                rest.Length = length;

                base.Add(rest);
                return rest;
            }
            public SynthesizedAudioCommandNote Add(SynthesizedAudioPredefinedNote note, double length, int octave, float volume)
            {
                SynthesizedAudioCommandNote command = new SynthesizedAudioCommandNote();
                command.Length = length;
                command.Frequency = SynthesizedAudioPredefinedNoteConverter.GetFrequency(note, octave);

                base.Add(command);
                return command;
            }


		}
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
