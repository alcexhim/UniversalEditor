using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.GLU.Windows
{
    public static class Methods
    {
        [DllImport("glu32.dll")]
        public static extern void gluPerspective(double fovy, double aspect, double zNear, double zFar);
    }
}
