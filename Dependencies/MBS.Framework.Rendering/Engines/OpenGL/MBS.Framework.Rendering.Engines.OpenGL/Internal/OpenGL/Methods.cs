/*
 * Created by SharpDevelop.
 * User: BEC16770
 * Date: 2010-12-13
 * Time: 8:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.OpenGL
{
	/// <summary>
	/// Description of GL.
	/// </summary>
	public static class Methods
	{
		#region Normalize Coordinates - special magic needed on (at least) Windows
		private static void glNormalizeCoordinates(ref double x, ref double y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}

		private static void glNormalizeCoordinates(ref float x, ref float y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}

		private static void glNormalizeCoordinates(ref int x, ref int y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}

		private static void glNormalizeCoordinates(ref short x, ref short y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += (short)global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += (short)global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref double x, ref double y, ref double z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref float x, ref float y, ref float z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref int x, ref int y, ref int z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref short x, ref short y, ref short z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += (short)global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += (short)global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref double x, ref double y, ref double z, ref double w)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref float x, ref float y, ref float z, ref float w)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref int x, ref int y, ref int z, ref int w)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}
		private static void glNormalizeCoordinates(ref short x, ref short y, ref short z, ref short w)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				break;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				// small adjustments required on Windows, who knows why
				// y += (short)global::System.Windows.Forms.SystemInformation.CaptionHeight;
				// y += (short)global::System.Windows.Forms.SystemInformation.Border3DSize.Height;
				y++;
				break;
			case PlatformID.Xbox:
				break;
			}
		}

		internal static void glGetShaderiv(uint shader, int parm, ref int status)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGetShaderiv(shader, parm, ref status);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGetShaderiv(shader, parm, ref status);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		internal static void glGetProgramiv(uint program, int parm, ref int status)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGetProgramiv(program, parm, ref status);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGetProgramiv(program, parm, ref status);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		internal static void glGetProgramInfoLog(uint program, int bufSize, ref int length, IntPtr hInfoLog)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGetProgramInfoLog(program, bufSize, ref length, hInfoLog);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGetProgramInfoLog(program, bufSize, ref length, hInfoLog);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		#endregion

		public static void glAccum(int num1, float num2)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glAccum(num1, num2);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glAccum(num1, num2);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Specifies the reference value and the comparison function for the alpha test, which discards fragments
		/// depending on the outcome of a comparison between an incoming fragment's alpha value and a constant
		/// reference value. The comparison is performed only if alpha testing is enabled.
		/// </summary>
		/// <param name="func">Specifies the alpha comparison function.</param>
		/// <param name="value">Specifies the reference value that incoming alpha values are compared to. This value is clamped to the range [0, 1], where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
		public static void glAlphaFunc(Constants.GLAlphaFunc func, float value)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glAlphaFunc(func, value);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glAlphaFunc(func, value);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glBegin(int mode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBegin(mode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBegin(mode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glEnd()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEnd();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEnd();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glEndList()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEndList();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEndList();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glCallList(int list)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glCallList(list);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glCallList(list);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glClear(int mask)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glClear(mask);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glClear(mask);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glClearColor(int r, int g, int b, int a)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glClearColor(r, g, b, a);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glClearColor(r, g, b, a);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glClearColor(float r, float g, float b, float a)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glClearColor(r, g, b, a);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glClearColor(r, g, b, a);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glClearColor(double r, double g, double b, double a)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glClearColor((float)r, (float)g, (float)b, (float)a);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glClearColor((float)r, (float)g, (float)b, (float)a);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glClearDepth(float d)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glClearDepth(d);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glClearDepth(d);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glCullFace(Constants.GLFace face)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glCullFace(face);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glCullFace(face);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static bool glIsEnabled(Constants.GLCapabilities capability)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glIsEnabled(capability);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glIsEnabled(capability);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glEnable(Constants.GLCapabilities capability)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEnable(capability);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEnable(capability);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDisable(Constants.GLCapabilities capability)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDisable(capability);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDisable(capability);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glEnable(int capability)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEnable(capability);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEnable(capability);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDisable(int capability)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDisable(capability);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDisable(capability);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glEnableClientState(int state)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEnableClientState(state);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEnableClientState(state);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDisableClientState(int state)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDisableClientState(state);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDisableClientState(state);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glLightfv(int light, int pname, float[] parameters)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glLightfv(light, pname, parameters);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glLightfv(light, pname, parameters);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glBlendFunc(Constants.GLBlendFunc sourceFactorMode, Constants.GLBlendFunc destinationFactorMode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBlendFunc(sourceFactorMode, destinationFactorMode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBlendFunc(sourceFactorMode, destinationFactorMode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glMatrixMode(Constants.MatrixMode mode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMatrixMode(mode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMatrixMode(mode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glPushMatrix()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glPushMatrix();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glPushMatrix();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glPopMatrix()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glPopMatrix();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glPopMatrix();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glLoadIdentity()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glLoadIdentity();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glLoadIdentity();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTranslated(double x, double y, double z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTranslated(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTranslated(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTranslatef(float x, float y, float z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTranslatef(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTranslatef(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glRotated(double angle, double x, double y, double z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRotated(angle, x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRotated(angle, x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glRotatef(float angle, float x, float y, float z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRotatef(angle, x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRotatef(angle, x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glVertex2d(double x, double y)
		{
			glNormalizeCoordinates(ref x, ref y);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2d(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2d(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2f(float x, float y)
		{
			glNormalizeCoordinates(ref x, ref y);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2f(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2f(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2i(int x, int y)
		{
			glNormalizeCoordinates(ref x, ref y);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2i(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2i(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2s(short x, short y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2s(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2s(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glVertex2dv(double[] values)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2dv(values);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2dv(values);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2fv(float[] values)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2fv(values);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2fv(values);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2iv(int[] values)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2iv(values);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2iv(values);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex2sv(short[] values)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex2sv(values);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex2sv(values);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glVertex3d(double x, double y, double z)
		{
			glNormalizeCoordinates(ref x, ref y, ref z);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex3d(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex3d(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glVertex3f(float x, float y, float z)
		{
			glNormalizeCoordinates(ref x, ref y, ref z);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex3f(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex3f(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex3i(int x, int y, int z)
		{
			glNormalizeCoordinates(ref x, ref y, ref z);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex3i(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex3i(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex3s(short x, short y, short z)
		{
			glNormalizeCoordinates(ref x, ref y, ref z);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex3s(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex3s(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glVertex4d(double x, double y, double z, double w)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex4d(x, y, z, w);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex4d(x, y, z, w);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex4f(float x, float y, float z, float w)
		{
			glNormalizeCoordinates(ref x, ref y, ref z, ref w);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex4f(x, y, z, w);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex4f(x, y, z, w);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex4i(int x, int y, int z, int w)
		{
			glNormalizeCoordinates(ref x, ref y, ref z, ref w);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex4i(x, y, z, w);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex4i(x, y, z, w);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex4s(short x, short y, short z, short w)
		{
			glNormalizeCoordinates(ref x, ref y, ref z, ref w);
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex4s(x, y, z, w);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex4s(x, y, z, w);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="target">Specifies the target texture.</param>
		/// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
		/// <param name="internalFormat">Specifies the number of color components in the texture.</param>
		/// <param name="width">Specifies the width of the texture image including the border if any. If the GL version does not support non-power-of-two sizes, this value must be ((2^n +  2) * border) for some integer n. All implementations support texture images that are at least 64 texels wide.</param>
		/// <param name="height">Specifies the height of the texture image including the border if any. If the GL version does not support non-power-of-two sizes, this value must be ((2^m + 2) * border) for some integer m. All implementations support texture images that are at least 64 texels high.</param>
		/// <param name="border">Specifies the width of the border. Must be either 0 or 1.</param>
		/// <param name="format">Specifies the format of the pixel data.</param>
		/// <param name="type">Specifies the data type of the pixel data.</param>
		/// <param name="pixelData">Specifies a pointer to the image data in memory.</param>
		public static void glTexImage2D(Constants.TextureTarget target, int level, Constants.GLTextureInternalFormat internalFormat, int width, int height, int border, Constants.GLTextureFormat format, Constants.GLTextureType type, IntPtr pixelData)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixelData);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixelData);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// It returns an integer n such that range contiguous empty display lists, named n, n + 1, ..., n + (range - 1), are created. If range is 0, if there is no group of range contiguous names available, or if any error is generated, no display lists are generated, and 0 is returned.
		/// </summary>
		/// <param name="range">Specifies the number of contiguous empty display lists to be generated.</param>
		public static int glGenLists(int range)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glGenLists(range);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glGenLists(range);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glGenTextures(int textureCount, uint[] textureIDs)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGenTextures(textureCount, textureIDs);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGenTextures(textureCount, textureIDs);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBindTexture(Constants.TextureTarget target, uint texture)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBindTexture(target, texture);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBindTexture(target, texture);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glPixelStorei(int a, int b)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glPixelStorei(a, b);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glPixelStorei(a, b);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexParameter(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, float param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexParameterf(target, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexParameterf(target, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexParameter(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, int param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexParameteri(target, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexParameteri(target, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// Sets up texture environment parameters.
		/// </summary>
		/// <param name="target">Specifies a texture environment.</param>
		/// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter.</param>
		/// <param name="param">Specifies a single symbolic constant.</param>
		public static void glTexEnv(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, Constants.TextureEnvironmentParameterValue param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexEnvi(target, pname, (int)param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexEnvi(target, pname, (int)param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets up texture environment parameters.
		/// </summary>
		/// <param name="target">Specifies a texture environment.</param>
		/// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter.</param>
		/// <param name="param">Specifies a single symbolic constant.</param>
		public static void glTexEnv(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, float param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexEnvf(target, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexEnvf(target, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets up texture environment parameters.
		/// </summary>
		/// <param name="target">Specifies a texture environment.</param>
		/// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter.</param>
		/// <param name="param">Specifies a single symbolic constant.</param>
		public static void glTexEnv(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, int param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexEnvi(target, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexEnvi(target, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexCoord(short s)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord1s(s);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord1s(s);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(int s)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord1i(s);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord1i(s);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(float s)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord1f(s);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord1f(s);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(double s)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord1d(s);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord1d(s);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexCoord(short u, short v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord2s(u, v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord2s(u, v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(int u, int v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord2i(u, v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord2i(u, v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(float u, float v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord2f(u, v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord2f(u, v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(double u, double v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord2d(u, v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord2d(u, v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexCoord(short s, short t, short r)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord3s(s, t, r);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord3s(s, t, r);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(int s, int t, int r)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord3i(s, t, r);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord3i(s, t, r);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(float s, float t, float r)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord3f(s, t, r);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord3f(s, t, r);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(double s, double t, double r)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord3d(s, t, r);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord3d(s, t, r);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexCoord(short s, short t, short r, short q)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord4s(s, t, r, q);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord4s(s, t, r, q);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(int s, int t, int r, int q)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord4i(s, t, r, q);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord4i(s, t, r, q);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(float s, float t, float r, float q)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord4f(s, t, r, q);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord4f(s, t, r, q);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoord(double s, double t, double r, double q)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoord4d(s, t, r, q);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoord4d(s, t, r, q);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}


		public static void glTexCoord(short[] coords)
		{
			switch (coords.Length)
			{
			case 1:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord1sv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord1sv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 2:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord2sv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord2sv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 3:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord3sv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord3sv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 4:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord4sv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord4sv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			default:
				{
					throw new ArgumentOutOfRangeException("coords must be of length 1, 2, 3, or 4");
				}
			}
		}
		public static void glTexCoord(int[] coords)
		{
			switch (coords.Length)
			{
			case 1:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord1iv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord1iv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 2:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord2iv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord2iv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 3:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord3iv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord3iv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 4:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord4iv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord4iv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			default:
				{
					throw new ArgumentOutOfRangeException("coords must be of length 1, 2, 3, or 4");
				}
			}
		}
		public static void glTexCoord(float[] coords)
		{
			switch (coords.Length)
			{
			case 1:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord1fv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord1fv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 2:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord2fv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord2fv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 3:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord3fv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord3fv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 4:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord4fv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord4fv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			default:
				{
					throw new ArgumentOutOfRangeException("coords must be of length 1, 2, 3, or 4");
				}
			}
		}
		public static void glTexCoord(double[] coords)
		{
			switch (coords.Length)
			{
			case 1:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord1dv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord1dv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 2:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord2dv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord2dv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 3:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord3dv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord3dv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			case 4:
				{
					switch (Environment.OSVersion.Platform)
					{
					case PlatformID.MacOSX:
						break;
					case PlatformID.Unix:
						Linux.Methods.glTexCoord4dv(coords);
						return;
					case PlatformID.Win32NT:
					case PlatformID.Win32S:
					case PlatformID.Win32Windows:
					case PlatformID.WinCE:
						Windows.Methods.glTexCoord4dv(coords);
						return;
					case PlatformID.Xbox:
						break;
					}
					throw new PlatformNotSupportedException();
				}
			default:
				{
					throw new ArgumentOutOfRangeException("coords must be of length 1, 2, 3, or 4");
				}
			}
		}

		public static void glDrawElements(int mode, int count, byte[] indices)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedByte, indices);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedByte, indices);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDrawElements(int mode, int count, ushort[] indices)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedShort, indices);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedShort, indices);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDrawElements(int mode, int count, uint[] indices)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedInt, indices);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDrawElements(mode, count, Internal.OpenGL.Constants.GLElementType.UnsignedInt, indices);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}


		public static void glVertexPointer(int size, int type, int stride, IntPtr ptr)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertexPointer(size, type, stride, ptr);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertexPointer(size, type, stride, ptr);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glNormal3fv(float[] v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glNormal3fv(v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glNormal3fv(v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glNewList(int list, Internal.OpenGL.Constants.GLDisplayListMode mode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glNewList(list, mode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glNewList(list, mode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glNormal3dv(double[] v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glNormal3dv(v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glNormal3dv(v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertex3fv(float[] v)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertex3fv(v);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertex3fv(v);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glNormalPointer(int type, int stride, IntPtr ptr)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glNormalPointer(type, stride, ptr);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glNormalPointer(type, stride, ptr);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexCoordPointer(int size, int type, int stride, IntPtr ptr)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexCoordPointer(size, type, stride, ptr);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexCoordPointer(size, type, stride, ptr);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glTexGend(int coord, int pname, double param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGend(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGend(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexGenf(int coord, int pname, float param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGenf(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGenf(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexGeni(int coord, int pname, int param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGeni(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGeni(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexGendv(int coord, int pname, double[] param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGendv(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGendv(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexGenfv(int coord, int pname, float[] param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGenfv(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGenfv(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glTexGeniv(int coord, int pname, int[] param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glTexGeniv(coord, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glTexGeniv(coord, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glColor4b(byte red, byte green, byte blue, byte alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4b(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4b(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4d(double red, double green, double blue, double alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4d(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4d(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4f(float red, float green, float blue, float alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4f(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4f(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4i(int red, int green, int blue, int alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4i(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4i(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4s(short red, short green, short blue, short alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4s(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4s(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4ub(byte red, byte green, byte blue, byte alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4ub(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4ub(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4ui(uint red, uint green, uint blue, uint alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4ui(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4ui(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glColor4us(ushort red, ushort green, ushort blue, ushort alpha)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glColor4us(red, green, blue, alpha);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glColor4us(red, green, blue, alpha);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glMaterialf(Constants.GLFace face, int pname, float param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMaterialf(face, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMaterialf(face, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glMateriali(Constants.GLFace face, int pname, int param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMateriali(face, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMateriali(face, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glMaterialfv(Constants.GLFace face, int pname, float[] param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMaterialfv(face, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMaterialfv(face, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glMaterialiv(Constants.GLFace face, int pname, int[] param)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMaterialiv(face, pname, param);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMaterialiv(face, pname, param);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glRasterPos2d(double x, double y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRasterPos2d(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRasterPos2d(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glRasterPos2f(float x, float y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRasterPos2f(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRasterPos2f(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glRasterPos2i(int x, int y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRasterPos2i(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRasterPos2i(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glRasterPos2s(short x, short y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glRasterPos2s(x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glRasterPos2s(x, y);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glLineWidth(float width)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glLineWidth(width);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glLineWidth(width);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glMultMatrixd(double[] m)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMultMatrixd(m);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMultMatrixd(m);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glMultMatrixf(float[] m)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glMultMatrixf(m);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glMultMatrixf(m);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glFlush()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glFlush();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glFlush();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glShadeModel(Constants.GLShadeModel mode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glShadeModel(mode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glShadeModel(mode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		#region OS-specific
		public static bool glMakeCurrent(IntPtr hdc, IntPtr hglrc)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return (Linux.Methods.glXMakeCurrent(hdc, hglrc) == 0);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.wglMakeCurrent(hdc, hglrc);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static IntPtr glGetCurrentDC()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glXGetCurrentDrawable();
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.wglGetCurrentDC();
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static IntPtr glCreateContext(IntPtr hdc)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glXCreateContext(hdc);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.wglCreateContext(hdc);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static bool glSwapBuffers(IntPtr hdc)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				// glXSwapBuffers returns void!
				Linux.Methods.glXSwapBuffers(IntPtr.Zero, hdc);
				return true;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				bool valu = Windows.Methods.SwapBuffers(hdc);
				return valu;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		#endregion


		public static void glDepthFunc(Constants.GLComparisonFunc func)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDepthFunc(func);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDepthFunc(func);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glHint(Constants.GLHintTarget target, Constants.GLHintMode mode)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glHint(target, mode);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glHint(target, mode);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glViewport(int x, int y, int w, int h)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glViewport(x, y, w, h);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glViewport(x, y, w, h);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glScalef(float x, float y, float z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glScalef(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glScalef(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glScaled(double x, double y, double z)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glScaled(x, y, z);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glScaled(x, y, z);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glOrtho(double left, double right, double bottom, double top, double near, double far)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glOrtho(left, right, bottom, top, near, far);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glOrtho(left, right, bottom, top, near, far);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glFrontFace(Constants.GLFaceOrientation orientation)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glFrontFace(orientation);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glFrontFace(orientation);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glGetIntegerv(int property, int[] retval)
		{
			if (retval == null) throw new ArgumentNullException("Please initialize retval array before calling glGetIntegerv");

			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGetIntegerv(property, retval);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGetIntegerv(property, retval);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static Constants.GLError glGetError()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glGetError();
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glGetError();
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// Retrieves the next glError and converts it to an exception if it is any value other than GL_NO_ERROR.
		/// </summary>
		[DebuggerNonUserCode()]
		internal static void glErrorToException()
		{
			Internal.OpenGL.Constants.GLError error = Internal.OpenGL.Methods.glGetError();
			switch (error)
			{
			case Constants.GLError.InvalidEnum:
				{
					throw new InvalidEnumerationException();
				}
			case Constants.GLError.InvalidFrameBufferOperation:
				{
					throw new InvalidFrameBufferException();
				}
			case Constants.GLError.InvalidOperation:
				{
					throw new InvalidOperationException();
				}
			case Constants.GLError.InvalidValue:
				{
					throw new ArgumentOutOfRangeException();
				}
			case Constants.GLError.OutOfMemory:
				{
					throw new OutOfMemoryException();
				}
			case Constants.GLError.StackOverflow:
				{
					throw new StackOverflowException();
				}
			case Constants.GLError.StackUnderflow:
				{
					throw new StackUnderflowException();
				}
			}
		}







		// EXTENSIONS
		public static uint glCreateShader(Constants.GLShaderType type)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glCreateShader(type);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glCreateShader(type);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static uint glCreateProgram()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glCreateProgram();
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glCreateProgram();
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glShaderSource(uint shader, int count, string[] str, int[] length)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glShaderSource(shader, count, str, length);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glShaderSource(shader, count, str, length);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glCompileShader(uint shader)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glCompileShader(shader);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glCompileShader(shader);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glAttachShader(uint shaderProgram, uint shader)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glAttachShader(shaderProgram, shader);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glAttachShader(shaderProgram, shader);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glLinkProgram(uint shaderProgram)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glLinkProgram(shaderProgram);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glLinkProgram(shaderProgram);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDeleteShader(uint handle)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDeleteShader(handle);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDeleteShader(handle);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUseProgram(uint handle)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUseProgram(handle);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUseProgram(handle);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static int glGetUniformLocation(uint program, string name)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glGetUniformLocation(program, name);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glGetUniformLocation(program, name);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glUniform1i(int location, int v0)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniform1i(location, v0);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniform1i(location, v0);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniform1f(int location, float v0)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniform1f(location, v0);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniform1f(location, v0);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniform2f(int location, float v0, float v1)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniform2f(location, v0, v1);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniform2f(location, v0, v1);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniform3f(int location, float v0, float v1, float v2)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniform3f(location, v0, v1, v2);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniform3f(location, v0, v1, v2);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniformMatrix2fv(int location, int count, bool transpose, float[] value)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniformMatrix2fv(location, count, transpose, value);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniformMatrix2fv(location, count, transpose, value);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniformMatrix3fv(int location, int count, bool transpose, float[] value)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniformMatrix3fv(location, count, transpose, value);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniformMatrix3fv(location, count, transpose, value);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glUniformMatrix4fv(int location, int count, bool transpose, float[] value)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glUniformMatrix4fv(location, count, transpose, value);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glUniformMatrix4fv(location, count, transpose, value);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}


		public static void glActiveTexture(Constants.GLTexture texture)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glActiveTexture(texture);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glActiveTexture(texture);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}


		public static void glGenVertexArrays(int n, uint[] arrays)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGenVertexArrays(n, arrays);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGenVertexArrays(n, arrays);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glDeleteVertexArrays(int n, uint[] arrays)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDeleteVertexArrays(n, arrays);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDeleteVertexArrays(n, arrays);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glGenBuffers(int n, uint[] arrays)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glGenBuffers(n, arrays);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glGenBuffers(n, arrays);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBindVertexArray(uint array)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBindVertexArray(array);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBindVertexArray(array);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBindBuffer(Constants.GLBindBufferTarget target, uint buffer)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBindBuffer(target, buffer);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBindBuffer(target, buffer);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBufferData(Constants.GLBindBufferTarget target, int size, IntPtr data, Constants.GLBufferDataUsage usage)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBufferData(target, size, data, usage);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBufferData(target, size, data, usage);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBufferData(Constants.GLBindBufferTarget target, int size, float[] data, Constants.GLBufferDataUsage usage)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBufferData(target, size, data, usage);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBufferData(target, size, data, usage);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBufferSubData(Constants.GLBindBufferTarget target, int offset, int size, IntPtr data)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBufferSubData(target, offset, size, data);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBufferSubData(target, offset, size, data);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glBufferSubData(Constants.GLBindBufferTarget target, int offset, float[][] data)
		{
			int size = 0;
			for (int i = 0; i < data.Length; i++)
			{
				for (int j = 0; j < data[i].Length; j++)
				{
					size += 4;
				}
			}
			IntPtr hData = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
			int ofs = 0;
			for (int i = 0; i < data.Length; i++)
			{
				for (int j = 0; j < data[i].Length; j++)
				{
					IntPtr hptr = new IntPtr(hData.ToInt64() + ofs);
					byte[] bits = BitConverter.GetBytes(data[i][j]);
					int val = BitConverter.ToInt32(bits, 0);
					global::System.Runtime.InteropServices.Marshal.WriteInt32(hptr, val);
					ofs += 4;
				}
			}

			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glBufferSubData(target, offset, size, hData);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glBufferSubData(target, offset, size, hData);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glEnableVertexAttribArray(uint index)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glEnableVertexAttribArray(index);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glEnableVertexAttribArray(index);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertexAttribPointer(uint index, int size, Constants.GLElementType type, bool normalized, int stride, IntPtr pointer)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertexAttribPointer(index, size, type, normalized, stride, pointer);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertexAttribPointer(index, size, type, normalized, stride, pointer);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glVertexAttribPointer(uint index, int size, Constants.GLElementType type, bool normalized, int stride, float[] pointer)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glVertexAttribPointer(index, size, type, normalized, stride, pointer);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glVertexAttribPointer(index, size, type, normalized, stride, pointer);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glDrawArrays(Constants.RenderMode triangles, int start, int count)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDrawArrays(triangles, start, count);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDrawArrays(triangles, start, count);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glDeleteBuffers(int n, uint[] buffers)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glDeleteBuffers(n, buffers);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glDeleteBuffers(n, buffers);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
		
		public static uint glGetAttribLocation(uint program, string name)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				return Linux.Methods.glGetAttribLocation(program, name);
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				return Windows.Methods.glGetAttribLocation(program, name);
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException();
		}
	}
}