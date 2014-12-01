using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    /// <summary>
    /// The exception that is thrown when reading is attempted past the end of a stream.
    /// </summary>
    public class EndOfStreamException : Exception
    {
    }
}
