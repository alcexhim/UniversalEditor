//
//  SynthesizedAudioTrack.cs - represents a track in a synthesized audio file
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
using System.Collections.Generic;
using System.Collections.ObjectModel;

using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Represents a track in a synthesized audio file.
	/// </summary>
	public class SynthesizedAudioTrack : ICloneable
	{
		public class SynthesizedAudioTrackCollection : Collection<SynthesizedAudioTrack>
		{
			private Dictionary<string, SynthesizedAudioTrack> tracksByID = new Dictionary<string, SynthesizedAudioTrack>();
			public SynthesizedAudioTrack this[string ID]
			{
				get
				{
					SynthesizedAudioTrack result;
					if (this.tracksByID.ContainsKey(ID))
					{
						result = this.tracksByID[ID];
					}
					else
					{
						result = null;
					}
					return result;
				}
			}

			protected override void InsertItem(int index, SynthesizedAudioTrack item)
			{
				base.InsertItem(index, item);

				if (!string.IsNullOrEmpty(item.ID))
					tracksByID[item.ID] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (!string.IsNullOrEmpty(this[index].ID))
					tracksByID.Remove(this[index].ID);

				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				base.ClearItems();
				tracksByID.Clear();
			}
		}

		public string ID { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Comment { get; set; } = string.Empty;
		public SynthesizedAudioCommand.SynthesizedAudioCommandCollection Commands { get; } = new SynthesizedAudioCommand.SynthesizedAudioCommandCollection();
		public VoicebankObjectModel Synthesizer { get; set; } = null;
		public bool IsMuted { get; set; } = false;
		public bool IsSolo { get; set; } = false;
		public byte Panpot { get; set; } = 64;
		public byte Volume { get; set; } = 0;
		public object Clone()
		{
			SynthesizedAudioTrack clone = new SynthesizedAudioTrack();
			foreach (SynthesizedAudioCommand command in this.Commands)
			{
				clone.Commands.Add(command.Clone() as SynthesizedAudioCommand);
			}
			clone.ID = (this.ID.Clone() as string);
			clone.Name = (this.Name.Clone() as string);
			return clone;
		}

		public double Tempo { get; set; } = 120;
		public Color Color { get; set; } = Color.Empty;
	}
}
