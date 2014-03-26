using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
    public class Track
    {
        public class TrackCollection
            : System.Collections.ObjectModel.Collection<Track>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private TrackEntry.TrackEntryCollection mvarEntries = new TrackEntry.TrackEntryCollection();
        public TrackEntry.TrackEntryCollection Entries { get { return mvarEntries; } }
    }
}
