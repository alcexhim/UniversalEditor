//
//  SynthesizedAudioCollectionTrack.cs - represents a track in a synthesized audio collection
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

using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.ObjectModels.Multimedia.AudioCollection.Synthesized
{
	/// <summary>
	/// Represents a track in a synthesized audio collection.
	/// </summary>
	public class SynthesizedAudioCollectionTrack : ICloneable
	{
		public class SynthesizedAudioCollectionTrackCollection
			: System.Collections.ObjectModel.Collection<SynthesizedAudioCollectionTrack>
		{

		}
		public string SongTitle { get; set; } = String.Empty;
		public string GameTitle { get; set; } = String.Empty;
		public string ArtistName { get; set; } = String.Empty;
		public string DumperName { get; set; } = String.Empty;
		public string Comments { get; set; } = String.Empty;
		public string AlbumTitle { get; set; } = String.Empty;
		public string PublisherName { get; set; } = String.Empty;
		public string OriginalFileName { get; set; } = String.Empty;
		public SynthesizedAudioObjectModel ObjectModel { get; set; } = null;

		public object Clone()
		{
			SynthesizedAudioCollectionTrack clone = new SynthesizedAudioCollectionTrack();
			clone.AlbumTitle = (AlbumTitle.Clone() as string);
			clone.ArtistName = (ArtistName.Clone() as string);
			clone.Comments = (Comments.Clone() as string);
			clone.DumperName = (DumperName.Clone() as string);
			clone.GameTitle = (GameTitle.Clone() as string);
			clone.ObjectModel = (ObjectModel.Clone() as SynthesizedAudioObjectModel);
			clone.OriginalFileName = (OriginalFileName.Clone() as string);
			clone.PublisherName = (PublisherName.Clone() as string);
			clone.SongTitle = (SongTitle.Clone() as string);
			return clone;
		}
	}
}
