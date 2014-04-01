using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class Song
    {
        public class SongCollection
            : System.Collections.ObjectModel.Collection<Song>
        {
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private string mvarAudioFileName = String.Empty;
        public string AudioFileName { get { return mvarAudioFileName; } set { mvarAudioFileName = value; } }

        private SongProducer.SongProducerCollection mvarProducers = new SongProducer.SongProducerCollection();
        public SongProducer.SongProducerCollection Producers { get { return mvarProducers; } }

        private int mvarDelay = 0;
        /// <summary>
        /// The millisecond delay between when the song is played and when the animation frame is rendered.
        /// </summary>
        public int Delay { get { return mvarDelay; } set { mvarDelay = value; } }

        private decimal mvarTempo = 120.0M;
        public decimal Tempo { get { return mvarTempo; } set { mvarTempo = value; } }
    }
}
