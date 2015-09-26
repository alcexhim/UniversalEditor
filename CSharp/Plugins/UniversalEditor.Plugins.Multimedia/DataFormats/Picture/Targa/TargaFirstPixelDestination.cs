using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Targa
{
    /// <summary>
    /// Screen destination of first pixel based on the VerticalTransferOrder and HorizontalTransferOrder.
    /// </summary>
    public enum TargaFirstPixelDestination
    {
        /// <summary>
        /// Unknown first pixel destination.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// First pixel destination is the top-left corner of the image.
        /// </summary>
        TopLeft = 1,
        /// <summary>
        /// First pixel destination is the top-right corner of the image.
        /// </summary>
        TopRight = 2,
        /// <summary>
        /// First pixel destination is the bottom-left corner of the image.
        /// </summary>
        BottomLeft = 3,
        /// <summary>
        /// First pixel destination is the bottom-right corner of the image.
        /// </summary>
        BottomRight = 4
    }
}
