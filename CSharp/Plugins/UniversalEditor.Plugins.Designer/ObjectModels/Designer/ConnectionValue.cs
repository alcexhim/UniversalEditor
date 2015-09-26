using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Designer
{
    public class ConnectionValue
    {
        public class ConnectionValueCollection
             : System.Collections.ObjectModel.Collection<ConnectionValue>
        {
        }

        private Connection mvarSourceConnection = null;
        public Connection SourceConnection { get { return mvarSourceConnection; } set { mvarSourceConnection = value; } }

        private Connection mvarDestinationConnection = null;
        public Connection DestinationConnection { get { return mvarDestinationConnection; } set { mvarDestinationConnection = value; } }

        public ConnectionValue(Connection source, Connection destination)
        {
            mvarSourceConnection = source;
            mvarDestinationConnection = destination;
        }

    }
}
