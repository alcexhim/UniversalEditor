using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.GLU.Linux
{
    public static class Methods
    {
        [DllImport("libGLU.so.1")]
        public static extern void gluPerspective(double fovy, double aspect, double zNear, double zFar);
    }
}
