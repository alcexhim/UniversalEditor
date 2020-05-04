﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting.Script
{
    public class TrackEntry
    {
        public class TrackEntryCollection
            : System.Collections.ObjectModel.Collection<TrackEntry>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private TrackEntryCommand.TrackEntryCommandCollection mvarCommands = new TrackEntryCommand.TrackEntryCommandCollection();
        public TrackEntryCommand.TrackEntryCommandCollection Commands { get { return mvarCommands; } }

        private int mvarStartFrame = 0;
        public int StartFrame { get { return mvarStartFrame; } set { mvarStartFrame = value; } }

        private int mvarDuration = 0;
        /// <summary>
        /// The duration, in frames, of the entry.
        /// </summary>
        public int Duration { get { return mvarDuration; } set { mvarDuration = value; } }
    }
}