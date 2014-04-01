using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid.Concert
{
    public class Performance
    {
        public class PerformanceCollection
            : System.Collections.ObjectModel.Collection<Performance>
        {
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private Song mvarSong = null;
        public Song Song { get { return mvarSong; } set { mvarSong = value; } }

        private Performer.PerformerCollection mvarPerformers = new Performer.PerformerCollection();
        public Performer.PerformerCollection Performers { get { return mvarPerformers; } }

        private Musician.MusicianCollection mvarGuestMusicians = new Musician.MusicianCollection();
        public Musician.MusicianCollection GuestMusicians { get { return mvarGuestMusicians; } }
    }
}
