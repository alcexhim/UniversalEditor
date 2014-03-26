using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
    public class ScriptObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
                _omr.Title = "Lighting script";
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
