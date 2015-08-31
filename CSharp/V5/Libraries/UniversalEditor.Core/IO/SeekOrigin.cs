using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    /// <summary>
    /// Provides various options for determining where seeking within a stream should start.
    /// </summary>
    public enum SeekOrigin
    {
        /// <summary>
        /// Indicates that seeking should start from the beginning of a stream.
        /// </summary>
        Begin = 0,
        /// <summary>
        /// Indicates that seeking should start from the current position within a stream.
        /// </summary>
        Current = 1,
        /// <summary>
        /// Indicates that seeking should start from the end of a stream. This usually necessiates that the amount
        /// be specified as a negative value.
        /// </summary>
        End = 2
    }
}
