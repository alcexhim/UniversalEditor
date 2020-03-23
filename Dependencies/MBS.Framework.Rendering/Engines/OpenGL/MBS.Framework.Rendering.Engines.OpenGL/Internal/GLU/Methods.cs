using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.GLU
{
    public static class Methods
    {
        public static void gluPerspective(double fovy, double aspect, double zNear, double zFar)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                    break;
                case PlatformID.Unix:
                    Linux.Methods.gluPerspective(fovy, aspect, zNear, zFar);
                    return;
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    Windows.Methods.gluPerspective(fovy, aspect, zNear, zFar);
                    return;
                case PlatformID.Xbox:
                    break;
            }
            throw new PlatformNotSupportedException();
        }
    }
}
