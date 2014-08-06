using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.ObjectModels.Multimedia.AudioCollection.Synthesized
{
	public class SynthesizedAudioCollectionTrack : ICloneable
	{
		public class SynthesizedAudioCollectionTrackCollection
			: System.Collections.ObjectModel.Collection<SynthesizedAudioCollectionTrack>
		{

		}

		private string mvarSongTitle = String.Empty;
		public string SongTitle { get { return mvarSongTitle; } set { mvarSongTitle = value; } }

		private string mvarGameTitle = String.Empty;
		public string GameTitle { get { return mvarGameTitle; } set { mvarGameTitle = value; } }

		private string mvarArtistName = String.Empty;
		public string ArtistName { get { return mvarArtistName; } set { mvarArtistName = value; } }

		private string mvarDumperName = String.Empty;
		public string DumperName { get { return mvarDumperName; } set { mvarDumperName = value; } }

		private string mvarComments = String.Empty;
		public string Comments { get { return mvarComments; } set { mvarComments = value; } }

		private string mvarAlbumTitle = String.Empty;
		public string AlbumTitle { get { return mvarAlbumTitle; } set { mvarAlbumTitle = value; } }

		private string mvarPublisherName = String.Empty;
		public string PublisherName { get { return mvarPublisherName; } set { mvarPublisherName = value; } }

		private string mvarOriginalFileName = String.Empty;
		public string OriginalFileName { get { return mvarOriginalFileName; } set { mvarOriginalFileName = value; } }

		private SynthesizedAudioObjectModel mvarObjectModel = null;
		public SynthesizedAudioObjectModel ObjectModel { get { return mvarObjectModel; } set { mvarObjectModel = value; } }

		public object Clone()
		{
			SynthesizedAudioCollectionTrack clone = new SynthesizedAudioCollectionTrack();
			clone.AlbumTitle = (mvarAlbumTitle.Clone() as string);
			clone.ArtistName = (mvarArtistName.Clone() as string);
			clone.Comments = (mvarComments.Clone() as string);
			clone.DumperName = (mvarDumperName.Clone() as string);
			clone.GameTitle = (mvarGameTitle.Clone() as string);
			clone.ObjectModel = (mvarObjectModel.Clone() as SynthesizedAudioObjectModel);
			clone.OriginalFileName = (mvarOriginalFileName.Clone() as string);
			clone.PublisherName = (mvarPublisherName.Clone() as string);
			clone.SongTitle = (mvarSongTitle.Clone() as string);
			return clone;
		}

	}
}
