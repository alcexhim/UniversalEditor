using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Concertroid
{
    public class SongProducer
    {
        public class SongProducerCollection
            : System.Collections.ObjectModel.Collection<SongProducer>
        {
        }

        private PermissionStatus mvarPermissionStatus = PermissionStatus.Unknown;
        public PermissionStatus PermissionStatus { get { return mvarPermissionStatus; } set { mvarPermissionStatus = value; } }

        private Producer mvarProducer = null;
        public Producer Producer { get { return mvarProducer; } set { mvarProducer = value; } }
    }
}
