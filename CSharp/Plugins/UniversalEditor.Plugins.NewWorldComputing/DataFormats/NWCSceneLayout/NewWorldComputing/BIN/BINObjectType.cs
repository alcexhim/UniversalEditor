using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.NWCSceneLayout.NewWorldComputing.BIN
{
    public enum BINObjectType : ushort
    {
        EOF = 0x00,
        /// <summary>
        /// Information about the screen. Contains three UInt16s, purpose unknown, followed by UInt16 width, UInt16 height, UInt16 unknown (-1),
        /// UInt16 unknown (2049?), FixedLengthString[13] file name of ICN file for background, UInt16 extraDataLength, byte[*] extraData, UInt16
        /// extraData2Length, byte[*] extraData2, FixedLengthString[13] file name of default font file, UInt16 * 4 unknown
        /// </summary>
        Screen = 0x01,
        /// <summary>
        /// Image?
        /// </summary>
        Button = 0x02,
        /// <summary>
        /// Unknown. Contains 2 * UInt16, values unknown
        /// </summary>
        Unknown0x0B = 0x0B,
        /// <summary>
        /// A label. Consists of UInt16 * 4 unknown (possibly Left, Top, Width, Height), UInt16 (size of text for the label, including null terminator...
        /// don't ask), NullTerminatedString (text for the label), FixedLengthString[13] (font name to use for the label), UInt16 * 4 unknown
        /// </summary>
        Label = 0x08,
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown0x09 = 0x09,
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown0x0A = 0x0A,
        /// <summary>
        /// Button (?). Contains UInt16 * 4 unknown (possibly left, top, width, height), FixedLengthString[13] file name of ICN file, UInt16 * 5 unknown.
        /// </summary>
        Image = 0x10,
        /// <summary>
        /// Unknown. 2 * UInt16, values unknown
        /// </summary>
        Unknown0x16 = 0x16,
        /// <summary>
        /// Unknown. 2 * UInt16, values unknown
        /// </summary>
        Unknown0x19 = 0x19,
        Background = 0xFF,
        Background2 = 0x03E9
    }
}
