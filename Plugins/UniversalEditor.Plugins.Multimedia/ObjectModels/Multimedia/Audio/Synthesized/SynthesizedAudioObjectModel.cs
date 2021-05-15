//
//  SynthesizedAudioObjectModel.cs - provides an ObjectModel for manipulating synthesized audio files (e.g. MIDI, VSQ, etc.)
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

using System.Collections.Generic;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating synthesized audio files (e.g. MIDI, VSQ, etc.).
	/// </summary>
	public class SynthesizedAudioObjectModel : AudioObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "Audio", "Synthesized Audio" };
			}
			return _omr;
		}

		public short ChannelCount { get; set; } = 2;

		public string Name { get; set; } = string.Empty;

		public double Tempo { get; set; } = 120.0;

		public SynthesizedAudioTrack.SynthesizedAudioTrackCollection Tracks { get; } = new SynthesizedAudioTrack.SynthesizedAudioTrackCollection();
		public VoicebankObjectModel.VoicebankObjectModelCollection Voices { get; } = new VoicebankObjectModel.VoicebankObjectModelCollection();

		private CriteriaObject[] _CriteriaObjects = null;

		private CriteriaProperty PROPERTY_LYRIC = new CriteriaProperty("Lyric", typeof(string));
		protected override CriteriaObject[] GetCriteriaObjectsInternal()
		{
			if (_CriteriaObjects == null)
			{
				_CriteriaObjects = new CriteriaObject[]
				{
					new CriteriaObject("Note", new CriteriaProperty[]
					{
						PROPERTY_LYRIC
					})
				};
			}
			return _CriteriaObjects;
		}
		protected override CriteriaResult[] FindInternal(CriteriaQuery query)
		{
			List<CriteriaResult> list = new List<CriteriaResult>();
			for (int i = 0; i < Tracks.Count; i++)
			{
				for (int j = 0; j < Tracks[i].Commands.Count; j++)
				{
					if (Tracks[i].Commands[j] is SynthesizedAudioCommandNote)
					{
						if (query.Check(PROPERTY_LYRIC, (Tracks[i].Commands[j] as SynthesizedAudioCommandNote).Lyric))
						{
							list.Add(new CriteriaResult(Tracks[i].Commands[j]));
						}
					}
				}
			}
			return list.ToArray();
		}

		public override void CopyTo(ObjectModel destination)
		{
			SynthesizedAudioObjectModel clone = destination as SynthesizedAudioObjectModel;
			clone.Name = (this.Name.Clone() as string);
			clone.Tempo = this.Tempo;
			foreach (SynthesizedAudioTrack track in this.Tracks)
			{
				clone.Tracks.Add(track.Clone() as SynthesizedAudioTrack);
			}
		}
		public override void Clear()
		{
			this.Name = string.Empty;
			this.Tempo = 120.0;
			this.Tracks.Clear();
		}
	}
}
