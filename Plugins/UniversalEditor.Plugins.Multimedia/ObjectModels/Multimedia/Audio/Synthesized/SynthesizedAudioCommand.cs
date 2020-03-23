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

			public event EventHandler ItemsChanged;
			protected virtual void OnItemsChanged(EventArgs e)
			{
				if (ItemsChanged != null) ItemsChanged(this, e);
			}
			protected override void InsertItem(int index, SynthesizedAudioCommand item)
			{
				base.InsertItem(index, item);
				OnItemsChanged(EventArgs.Empty);
			}

			protected override void RemoveItem(int index)
			{
				base.RemoveItem(index);
				OnItemsChanged(EventArgs.Empty);
			}
			protected override void ClearItems()
			{
				base.ClearItems();
				OnItemsChanged(EventArgs.Empty);
			}
			protected override void SetItem(int index, SynthesizedAudioCommand item)
			{
				base.SetItem(index, item);
				OnItemsChanged(EventArgs.Empty);
			}

		}

		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
