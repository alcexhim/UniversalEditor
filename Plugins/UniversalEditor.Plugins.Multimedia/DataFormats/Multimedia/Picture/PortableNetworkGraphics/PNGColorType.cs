using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
    /// <summary>
    /// Describes the interpolation of the image data.
    /// </summary>
    [Flags()]
    public enum PNGColorType
    {
        None = 0,
        /// <summary>
        /// Each pixel is referenced by an index into the palette table.
        /// </summary>
        Palette = 1,
        /// <summary>
        /// Each pixel is an R, G, B triple.
        /// </summary>
        Color = 2,
        /// <summary>
        /// Each pixel is followed by an alpha sample.
        /// </summary>
        AlphaChannel = 4
    }
}
