using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid.Concert
{
    public class ConcertObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        protected override ObjectModelReference MakeReferenceInternal()
        {
            if (_omr == null)
            {
                _omr = base.MakeReferenceInternal();
                _omr.Title = "Concert";
                _omr.Path = new string[] { "Concertroid", "Concert" };
            }
            return _omr;
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarBandName = String.Empty;
        public string BandName { get { return mvarBandName; } set { mvarBandName = value; } }

        private Musician.MusicianCollection mvarBandMusicians = new Musician.MusicianCollection();
        public Musician.MusicianCollection BandMusicians { get { return mvarBandMusicians; } set { mvarBandMusicians = value; } }

        private Musician.MusicianCollection mvarGuestMusicians = new Musician.MusicianCollection();
        public Musician.MusicianCollection GuestMusicians { get { return mvarGuestMusicians; } set { mvarGuestMusicians = value; } }

        private Performance.PerformanceCollection mvarPerformances = new Performance.PerformanceCollection();
        public Performance.PerformanceCollection Performances { get { return mvarPerformances; } }

        public override void Clear()
        {
            mvarTitle = String.Empty;
            mvarBandName = String.Empty;
            mvarBandMusicians.Clear();
            mvarGuestMusicians.Clear();
        }

        public override void CopyTo(ObjectModel where)
        {
            ConcertObjectModel clone = (where as ConcertObjectModel);
            clone.Title = (mvarTitle.Clone() as string);
            clone.BandName = (mvarBandName.Clone() as string);
            foreach (Musician musician in mvarBandMusicians)
            {
                clone.BandMusicians.Add(musician.Clone() as Musician);
            }
            foreach (Musician musician in mvarGuestMusicians)
            {
                clone.GuestMusicians.Add(musician.Clone() as Musician);
            }
        }
    }
}
