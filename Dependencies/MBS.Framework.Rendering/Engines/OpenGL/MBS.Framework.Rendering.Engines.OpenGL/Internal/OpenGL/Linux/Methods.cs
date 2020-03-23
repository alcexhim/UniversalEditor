using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.OpenGL.Linux
{
	/// <summary>
	/// Description of GL.
	/// </summary>
	internal static class Methods
    {
		private const string LIBRARY_OPENGL = "libGL";
        private const global::System.Runtime.InteropServices.CallingConvention CALLINGCONVENTION_OPENGL = CallingConvention.Cdecl;

		#region Linux-specific
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern int glXMakeCurrent(IntPtr hdc, IntPtr hrc);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern IntPtr glXCreateContext(IntPtr hdc);
		/// <summary>
		/// Equivalent to wglGetCurrentDC()
		/// </summary>
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern IntPtr glXGetCurrentDrawable();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern IntPtr glXGetCurrentContext();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glXSwapBuffers(IntPtr display, IntPtr hdc);
		
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern IntPtr[] glXChooseFBConfig(IntPtr display, int screen, int[] attribList, ref int nElements);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern IntPtr glXGetVisualFromFBConfig(IntPtr display, IntPtr config);
		#endregion
		
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glAccum(int operation, float value);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glAlphaFunc(Constants.GLAlphaFunc func, float value);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glBegin(int mode);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glBlendFunc(Constants.GLBlendFunc sourceFactorMode, Constants.GLBlendFunc destinationFactorMode);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glEnd();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glEndList();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glCallList(int list);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glClear(int mask);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glClearColor(float r, float g, float b, float a);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glClearColor(int r, int g, int b, int a);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glCullFace(Constants.GLFace face);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glFrontFace(Constants.GLFaceOrientation orientation);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glLightfv(int light, int pname, float[] parameters);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glMatrixMode(Constants.MatrixMode mode);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glNewList(int list, Internal.OpenGL.Constants.GLDisplayListMode mode);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glNormal3dv(double[] v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glNormal3fv(float[] v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3fv(float[] v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3dv(double[] v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glPushMatrix();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glPopMatrix();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glLoadIdentity();
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTranslated(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTranslatef(float x, float y, float z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glRotated(double angle, double x, double y, double z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glRotatef(float angle, float x, float y, float z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glScaled(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glScalef(float x, float y, float z);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2d(double x, double y);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2f(float x, float y);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2i(int x, int y);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2s(short x, short y);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2dv(double[] values);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2fv(float[] values);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2iv(int[] values);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex2sv(short[] values);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3d(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3f(float x, float y, float z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3i(int x, int y, int z);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex3s(short x, short y, short z);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex4d(double x, double y, double z, double w);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex4f(float x, float y, float z, float w);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex4i(int x, int y, int z, int w);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertex4s(short x, short y, short z, short w);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glEnableClientState(int state);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDisableClientState(int state);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glEnable(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDisable(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern bool glIsEnabled(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glEnable(int capability);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDisable(int capability);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern bool glIsEnabled(int capability);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glBindTexture(Constants.TextureTarget target, uint texture);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glPixelStorei(int a, int b);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexParameterf(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, float param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexParameteri(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, int param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexEnvf(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, float param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexEnvi(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, int param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1s(short s);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1i(int s);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1f(float s);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1d(double s);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2s(short u, short v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2i(int u, int v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2f(float u, float v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2d(double u, double v);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3s(short s, short t, short r);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3i(int s, int t, int r);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3f(float s, float t, float r);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3d(double s, double t, double r);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4s(short s, short t, short r, short q);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4i(int s, int t, int r, int q);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4f(float s, float t, float r, float q);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4d(double s, double t, double r, double q);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1sv(short[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1iv(int[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1fv(float[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord1dv(double[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2sv(short[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2iv(int[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2fv(float[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord2dv(double[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3sv(short[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3iv(int[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3fv(float[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord3dv(double[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4sv(short[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4iv(int[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4fv(float[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoord4dv(double[] coords);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, byte[] indices);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, ushort[] indices);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, uint[] indices);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glVertexPointer(int size, int type, int stride, IntPtr ptr);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glNormalPointer(int type, int stride, IntPtr ptr);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexCoordPointer(int size, int type, int stride, IntPtr ptr);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGend(int coord, int pname, double param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGenf(int coord, int pname, float param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGeni(int coord, int pname, int param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGendv(int coord, int pname, double[] param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGenfv(int coord, int pname, float[] param);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexGeniv(int coord, int pname, int[] param);
		
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4b(byte red, byte green, byte blue, byte alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4d(double red, double green, double blue, double alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4f(float red, float green, float blue, float alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4i(int red, int green, int blue, int alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4s(short red, short green, short blue, short alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);
		
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glMaterialf(Constants.GLFace face, int pname, float param);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glMateriali(Constants.GLFace face, int pname, int param);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glMaterialfv(Constants.GLFace face, int pname, float[] param);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glMaterialiv(Constants.GLFace face, int pname, int[] param);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glRasterPos2d(double x, double y);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glRasterPos2f(float x, float y);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glRasterPos2i(int x, int y);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glRasterPos2s(short x, short y);
		
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glLineWidth(float lineWidth);
		
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glMultMatrixd(double[] m);
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glMultMatrixf(float[] m);
		
		[DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
		public static extern void glFlush();

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glShadeModel(Constants.GLShadeModel mode);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glClearDepth(float d);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glDepthFunc(Constants.GLComparisonFunc func);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glHint(Constants.GLHintTarget target, Constants.GLHintMode mode);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glViewport(int x, int y, int w, int h);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern int glGenLists(int range);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGenTextures(int textureCount, uint[] textureIDs);
        
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glTexImage2D(Constants.TextureTarget target, int level, Constants.GLTextureInternalFormat internalFormat, int width, int height, int border, Constants.GLTextureFormat format, Constants.GLTextureType type, IntPtr pixelData);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glOrtho(double left, double right, double bottom, double top, double near, double far);

        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGetIntegerv(int pname, [Out] Int32[] @params);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGetFloatv(int pname, [Out] float[] @params);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGetDoublev(int pname, [Out] double[] @params);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGetBooleanv(int pname, [Out] bool[] @params);
        [DllImport(LIBRARY_OPENGL, CallingConvention = CALLINGCONVENTION_OPENGL)]
        public static extern void glGetPointv(int pname, [Out] IntPtr @params);

        [DllImport(LIBRARY_OPENGL)]
        public static extern Constants.GLError glGetError();







		// EXTENSIONS
		[DllImport(LIBRARY_OPENGL)]
		public static extern uint glCreateShader(Constants.GLShaderType type);
		[DllImport(LIBRARY_OPENGL)]
		public static extern uint glCreateProgram();
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glShaderSource(uint shader, int count, string[] str, int[] length);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glCompileShader(uint shader);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glAttachShader(uint shaderProgram, uint shader);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glLinkProgram(uint shaderProgram);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glDeleteShader(uint shader);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUseProgram(uint shader);
		[DllImport(LIBRARY_OPENGL)]
		public static extern int glGetUniformLocation(uint program, string name);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniform1i(int location, int v0);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniform1f(int location, float v0);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniform2f(int location, float v0, float v1);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniform3f(int location, float v0, float v1, float v2);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniformMatrix2fv(int location, int count, bool transpose, float[] value);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniformMatrix3fv(int location, int count, bool transpose, float[] value);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glUniformMatrix4fv(int location, int count, bool transpose, float[] value);

		[DllImport(LIBRARY_OPENGL)]
		public static extern void glActiveTexture(Constants.GLTexture texture);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glGenVertexArrays(int n, uint[] arrays);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glDeleteVertexArrays(int n, uint[] arrays);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glGenBuffers(int n, uint[] arrays);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glBindVertexArray(uint array);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glBindBuffer(Constants.GLBindBufferTarget target, uint buffer);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glBufferData(Constants.GLBindBufferTarget target, int size, IntPtr data, Constants.GLBufferDataUsage usage);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glBufferData(Constants.GLBindBufferTarget target, int size, float[] data, Constants.GLBufferDataUsage usage);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glEnableVertexAttribArray(uint index);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glVertexAttribPointer(uint index, int size, Constants.GLElementType type, bool normalized, int stride, IntPtr pointer);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glVertexAttribPointer(uint index, int size, Constants.GLElementType type, bool normalized, int stride, float[] pointer);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glBufferSubData(Constants.GLBindBufferTarget target, int offset, int size, IntPtr data);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glDrawArrays(Constants.RenderMode triangles, int start, int count);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glDeleteBuffers(int n, uint[] buffers);

		[DllImport(LIBRARY_OPENGL)]
		public static extern void glGetProgramInfoLog(uint program, int bufSize, ref int length, IntPtr infoLog);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glGetProgramiv(uint program, int parm, ref int value);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glGetShaderiv(uint program, int parm, ref int value);

		[DllImport(LIBRARY_OPENGL)]
		public static extern uint glGetAttribLocation(uint program, string name);
	}
}