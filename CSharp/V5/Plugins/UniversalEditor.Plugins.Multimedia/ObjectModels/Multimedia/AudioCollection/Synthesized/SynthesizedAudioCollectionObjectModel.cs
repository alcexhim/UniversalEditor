using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;

namespace UniversalEditor.ObjectModels.Multimedia.AudioCollection.Synthesized
{
	public class SynthesizedAudioCollectionObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Synthesized audio collection";
			}
			return _omr;
		}

		private SynthesizedAudioCollectionTrack.SynthesizedAudioCollectionTrackCollection mvarTracks = new SynthesizedAudioCollectionTrack.SynthesizedAudioCollectionTrackCollection();
		public SynthesizedAudioCollectionTrack.SynthesizedAudioCollectionTrackCollection Tracks { get { return mvarTracks; } }

		public override void Clear()
		{
			mvarTracks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			SynthesizedAudioCollectionObjectModel clone = (where as SynthesizedAudioCollectionObjectModel);
			foreach (SynthesizedAudioCollectionTrack track in mvarTracks)
			{
				clone.Tracks.Add(track.Clone() as SynthesizedAudioCollectionTrack);
			}
		}
	}
}
