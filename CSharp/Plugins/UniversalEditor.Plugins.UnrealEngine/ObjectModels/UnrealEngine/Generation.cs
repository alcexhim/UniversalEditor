using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.UnrealEngine
{
    public class Generation
    {
        public class GenerationCollection
            : System.Collections.ObjectModel.Collection<Generation>
        {
        }

        private uint mvarExportCount = 0;
        public uint ExportCount { get { return mvarExportCount; } set { mvarExportCount = value; } }

        private uint mvarNameCount = 0;
        public uint NameCount { get { return mvarNameCount; } set { mvarNameCount = value; } }

        public override string ToString()
        {
            return mvarExportCount.ToString() + " exports, " + mvarNameCount.ToString() + " names";
        }
    }
}
