using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering
{
    public enum TextureTarget
    {
        Texture2D,
        ProxyTexture2D,
        CubeMapNegativeX,
        CubeMapPositiveX,
        CubeMapPositiveY,
        CubeMapNegativeY,
        CubeMapNegativeZ,
        CubeMapPositiveZ
    }
}
