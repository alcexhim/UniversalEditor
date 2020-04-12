//
//  SynthesizedAudioCommand.cs - represents a command (e.g. note, control change) in a synthesized audio file
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

using System;
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Represents a command (e.g. note, control change) in a synthesized audio file.
	/// </summary>
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
				ItemsChanged?.Invoke(this, e);
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
