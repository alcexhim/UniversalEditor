using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public enum TextureFilter
    {
        /// <summary>
        /// Returns the value of the texture element that is nearest (in Manhattan distance) to the center of the pixel being textured. 
        /// </summary>
        Nearest,
        /// <summary>
        /// Returns the weighted average of the four texture elements that are closest to the center of the pixel being textured. These can include border texture elements, depending on the values of GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T, and on the exact mapping. 
        /// </summary>
        Linear,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce a texture value.
        /// </summary>
        NearestMipmapNearest,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the GL_LINEAR criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value.
        /// </summary>
        LinearMipmapNearest,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        NearestMipmapLinear,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the GL_LINEAR criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        LinearMipmapLinear
    }
}
