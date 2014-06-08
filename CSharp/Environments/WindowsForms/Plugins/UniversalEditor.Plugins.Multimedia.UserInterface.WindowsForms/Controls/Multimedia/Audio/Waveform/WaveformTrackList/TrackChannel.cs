using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Controls.Multimedia.Audio.Waveform.WaveformTrackList
{
    public class TrackChannel
    {
        public class TrackChannelCollection
            : System.Collections.ObjectModel.Collection<TrackChannel>
        {
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
    }
}
