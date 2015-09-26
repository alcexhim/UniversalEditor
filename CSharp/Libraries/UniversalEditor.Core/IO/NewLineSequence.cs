using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.IO
{
    public enum NewLineSequence
    {
        /// <summary>
        /// Determines the new line sequence based on the system default (CR on Mac OS up to version 9, LF on Linux,
        /// CRLF on Windows).
        /// </summary>
        Default = -1,
        /// <summary>
        /// Uses the carriage return ('\r', ^M, 0x0D) as the new line sequence.
        /// </summary>
        CarriageReturn = 1,
        /// <summary>
        /// Uses the line feed ('\n', ^J, 0x0A) as the new line sequence.
        /// </summary>
        LineFeed = 2,
        /// <summary>
        /// Uses a combination of carriage return and line feed ("\r\n", ^M^J, {0x0D, 0x0A}) as the new line sequence.
        /// </summary>
        CarriageReturnLineFeed = 3,
        /// <summary>
        /// Uses a combination of line feed and carriage return ("\n\r", ^J^M, {0x0A, 0x0D}) as the new line sequence.
        /// </summary>
        LineFeedCarriageReturn = 4
    }
}
