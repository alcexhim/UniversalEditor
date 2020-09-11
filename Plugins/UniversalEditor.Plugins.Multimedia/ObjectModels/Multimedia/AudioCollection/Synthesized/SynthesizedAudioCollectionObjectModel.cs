//
//  SynthesizedAudioCollectionObjectModel.cs - provides an ObjectModel for manipulating a collection of synthesized audio files
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

namespace UniversalEditor.ObjectModels.Multimedia.AudioCollection.Synthesized
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating a collection of synthesized audio files.
	/// </summary>
	public class SynthesizedAudioCollectionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "Audio", "Synthesized audio collection" };
			}
			return _omr;
		}

		/// <summary>
		/// Gets a collection of <see cref="SynthesizedAudioCollectionTrack" /> instances representing the tracks in this file.
		/// </summary>
		/// <value>The tracks in this file.</value>
		public SynthesizedAudioCollectionTrack.SynthesizedAudioCollectionTrackCollection Tracks { get; } = new SynthesizedAudioCollectionTrack.SynthesizedAudioCollectionTrackCollection();

		public override void Clear()
		{
			Tracks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			SynthesizedAudioCollectionObjectModel clone = (where as SynthesizedAudioCollectionObjectModel);
			foreach (SynthesizedAudioCollectionTrack track in Tracks)
			{
				clone.Tracks.Add(track.Clone() as SynthesizedAudioCollectionTrack);
			}
		}
	}
}
