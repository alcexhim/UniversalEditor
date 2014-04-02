using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
    public enum MPQFormatVersion
    {
        /// <summary>
        /// Format 1 (up to The Burning Crusade)
        /// </summary>
        Format1 = 0,
        /// <summary>
        /// Format 2 (The Burning Crusade and newer)
        /// </summary>
        Format2 = 1,
        /// <summary>
        /// Format 3 (WoW - Cataclysm beta or newer)
        /// </summary>
        Format3 = 2,
        /// <summary>
        /// Format 4 (WoW - Cataclysm beta or newer)
        /// </summary>
        Format4 = 3
    }
}
