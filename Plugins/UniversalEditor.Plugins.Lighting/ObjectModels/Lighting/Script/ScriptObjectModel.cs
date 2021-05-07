using System;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
	public class ScriptObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Lighting", "Show script" };
			}
			return _omr;
		}

		public override void Clear()
		{
			mvarFixtures.Clear();
			mvarTracks.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		private string mvarAudioFileName = String.Empty;
		public string AudioFileName { get { return mvarAudioFileName; } set { mvarAudioFileName = value; } }

		private Fixture.FixtureCollection mvarFixtures = new Fixture.FixtureCollection();
		public Fixture.FixtureCollection Fixtures { get { return mvarFixtures; } }

		private Track.TrackCollection mvarTracks = new Track.TrackCollection();
		public Track.TrackCollection Tracks { get { return mvarTracks; } }
	}
}
