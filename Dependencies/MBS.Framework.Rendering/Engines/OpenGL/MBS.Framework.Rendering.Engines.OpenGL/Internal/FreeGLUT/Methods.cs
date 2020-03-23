using System;
using System.Collections.Generic;
using System.Text;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeGLUT
{
	/// <summary>
	/// FreeGLUT (OpenGL Utility Toolkit) binding for .NET, implementing FreeGlut
	/// 2.8.0. Derived from Tao Framework's FreeGLUT binding.
	/// </summary>
	public static class Methods
	{
		public static void glutInit()
		{
			string[] args = new string[0];
			glutInit(ref args);
		}
		public static void glutInit(ref string[] args)
		{
			int argcp = args.Length;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutInit(ref argcp, ref args);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					List<StringBuilder> sbz = new List<StringBuilder>();
					for (int i = 0; i < args.Length; i++)
					{
						sbz.Add(new StringBuilder(args[i]));
					}
					StringBuilder[] argv = sbz.ToArray();
					Windows.Methods.glutInit(ref argcp, argv);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static int glutGetWindow()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutGetWindow();
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutGetWindow();
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutSwapBuffers()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutSwapBuffers();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutSwapBuffers();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutInitDisplayMode(int mode)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutInitDisplayMode(mode);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutInitDisplayMode(mode);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Adds a menu entry to the bottom of the current menu.
		/// </summary>
		/// <param name="name">string to display in the menu entry.</param>
		/// <param name="val">Value to return to the menu's callback function if the menu entry is selected.</param>
		/// <remarks>
		/// glutAddMenuEntry adds a menu entry to the bottom of the current menu.  The
		/// string name will be displayed for the newly added menu entry. If the menu
		/// entry is selected by the user, the menu's callback will be called passing
		/// val as the callback's parameter.
		/// </remarks>
		public static void glutAddMenuEntry(string name, int val)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutAddMenuEntry(name, val);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutAddMenuEntry(name, val);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Adds a sub-menu trigger to the bottom of the current menu.
		/// </summary>
		/// <param name="name">string to display in the menu item from which to cascade the sub-menu.</param>
		/// <param name="val">Identifier of the menu to cascade from this sub-menu menu item.</param>
		/// <remarks>
		/// glutAddSubMenu adds a sub-menu trigger to the bottom of the current menu.
		/// The string name will be displayed for the newly added sub-menu trigger. If
		/// the sub-menu trigger is entered, the sub-menu numbered menu will be cascaded,
		/// allowing sub-menu menu items to be selected.
		/// </remarks>
		public static void glutAddSubMenu(string name, int menu)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutAddSubMenu(name, menu);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutAddSubMenu(name, menu);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Attaches a mouse button for the current window to the identifier of the current
		/// menu.
		/// </summary>
		/// <param name="button">The button to attach a menu.</param>
		/// <remarks>
		/// glutAttachMenu attaches a mouse button for the current window to the identifier
		/// of the current menu. By attaching a menu identifier to a button, the named
		/// menu will be popped up when the user presses the specified button.  button
		/// should be one of Tao.FreeGlut.Glut.GLUT_LEFT_BUTTON, Tao.FreeGlut.Glut.GLUT_MIDDLE_BUTTON,
		/// and Tao.FreeGlut.Glut.GLUT_RIGHT_BUTTON.  Note that the menu is attached
		/// to the button by identifier, not by reference.
		/// </remarks>
		public static void glutAttachMenu(int button)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutAttachMenu(button);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutAttachMenu(button);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Renders a bitmap character using OpenGL.
		/// </summary>
		/// <param name="font">
		/// Bitmap font to use. Without using any display lists, glutBitmapCharacter
		/// renders the character in the named bitmap font.  The available fonts are:
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_8_BY_13: A fixed width font with every character fitting in an 8 by 13 pixel rectangle.
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_9_BY_15: A fixed width font with every character fitting in an 9 by 15 pixel rectangle.
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_TIMES_ROMAN_10: A 10-point proportional spaced Times Roman font. 
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_TIMES_ROMAN_24: A 24-point proportional spaced Times Roman font.
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_HELVETICA_10: A 10-point proportional spaced Helvetica font.
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_HELVETICA_12: A 12-point proportional spaced Helvetica font.
		/// Tao.FreeGlut.Glut.GLUT_BITMAP_HELVETICA_18: A 18-point proportional spaced Helvetica font.
		/// </param>
		/// <param name="character">Character to render (not confined to 8 bits).</param>
		/// <remarks>
		/// Rendering a nonexistent character has no effect. glutBitmapCharacter automatically
		/// sets the OpenGL unpack pixel storage modes it needs appropriately and saves
		/// and restores the previous modes before returning.  The generated call to
		/// Gl.glBitmap will adjust the current raster position based on the width of
		/// the character.
		/// </remarks>
		/// <example>
		/// Here is a routine that shows how to render a string of text with glutBitmapCharacter:
		/// <code>
		/// private void PrintText(float x, float y, string text)
		/// {
		///     Gl.glRasterPos2f(x, y);
		///     foreach(char c in text)
		///     {
		///         Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_HELVETICA_18, c);
		///     }
		/// }
		/// </code>
		/// </example>
		public static void glutBitmapCharacter(IntPtr font, int character)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutBitmapCharacter(font, character);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutBitmapCharacter(font, character);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Returns the height of a given font, in pixels.
		/// </summary>
		/// <param name="font">A bitmapped font identifier.</param>
		/// <value>0 if font is invalid, otherwise, the font's height, in pixels.</value>
		public static int glutBitmapHeight(IntPtr font)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutBitmapHeight(font);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutBitmapHeight(font);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Returns the length of a bitmap font string.
		/// </summary>
		/// <param name="font">Bitmap font to use. For valid values, see the Tao.FreeGlut.Glut.glutBitmapCharacter(System.IntPtr,System.Int32) description.</param>
		/// <param name="text">Text string.</param>
		/// <returns>Length of string in pixels.</returns>
		/// <remarks>
		/// glutBitmapLength returns the length in pixels of a string (8-bit characters).
		/// This length is equivalent to summing all the widths returned by Tao.FreeGlut.Glut.glutBitmapWidth(System.IntPtr,System.Int32)
		/// for each character in the string.
		/// </remarks>
		public static int glutBitmapLength(IntPtr font, string text)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutBitmapLength(font, text);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutBitmapLength(font, text);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Draw a string of bitmapped characters.
		/// </summary>
		/// <param name="font">A bitmapped font identifier.</param>
		/// <param name="str">The string to draw.</param>
		public static void glutBitmapString(IntPtr font, string str)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutBitmapString(font, str);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutBitmapString(font, str);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Creates a top-level window.
		/// </summary>
		/// <param name="name">Character string for use as window name.</param>
		/// <returns>
		/// The value returned is a unique small integer identifier for the window. The
		/// range of allocated identifiers starts at one. This window identifier can
		/// be used when calling Tao.FreeGlut.Glut.glutSetWindow(System.Int32).
		/// </returns>
		/// <remarks>
		/// glutCreateWindow creates a top-level window. The name will be provided to
		/// the window system as the window’s title. The intent is that the window system
		/// will label the window with name as the title.
		/// Implicitly, the current window is set to the newly created window.
		/// Each created window has a unique associated OpenGL context. State changes
		/// to a window’s associated OpenGL context can be done immediately after the
		/// window is created.
		/// The display state of a window is initially for the window to be shown. But
		/// the window’s display state is not actually acted upon until Tao.FreeGlut.Glut.glutMainLoop()
		/// is entered. This means until glutMainLoop is called, rendering to a created
		/// window is ineffective because the window can not yet be displayed.
		/// X IMPLEMENTATION NOTES
		/// The proper X Inter-Client Communications Convention Manual (ICCCM) top-level
		/// properties are established. The WM_COMMAND property that lists the commandline
		/// used to invoke the GLUT program is only established for the first window
		/// created.
		/// </remarks>
		public static int glutCreateWindow(string name)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutCreateWindow(name);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutCreateWindow(name);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutDestroyWindow(int handle)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutDestroyWindow(handle);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutDestroyWindow(handle);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the display callback for the current window.
		/// </summary>
		/// <param name="func">The new display callback function. See Tao.FreeGlut.Glut.DisplayCallback.</param>
		/// <remarks>
		/// glutDisplayFunc sets the display callback for the current window. When GLUT
		/// determines that the normal plane for the window needs to be redisplayed,
		/// the display callback for the window is called. Before the callback, the current
		/// window is set to the window needing to be redisplayed and (if no overlay
		/// display callback is registered) the layer in use is set to the normal plane.
		/// The display callback is called with no parameters. The entire normal plane
		/// region should be redisplayed in response to the callback (this includes ancillary
		/// buffers if your program depends on their state).
		/// GLUT determines when the display callback should be triggered based on the
		/// window's redisplay state. The redisplay state for a window can be either
		/// set explicitly by calling Tao.FreeGlut.Glut.glutPostRedisplay() or implicitly
		/// as the result of window damage reported by the window system. Multiple posted
		/// redisplays for a window are coalesced by GLUT to minimize the number of display
		/// callbacks called.
		/// When an overlay is established for a window, but there is no overlay display
		/// callback registered, the display callback is used for redisplaying both the
		/// overlay and normal plane (that is, it will be called if either the redisplay
		/// state or overlay redisplay state is set). In this case, the layer in use
		/// is not implicitly changed on entry to the display callback.
		/// See Tao.FreeGlut.Glut.glutOverlayDisplayFunc(Tao.FreeGlut.Glut.OverlayDisplayCallback)
		/// to understand how distinct callbacks for the overlay and normal plane of
		/// a window may be established.
		/// When a window is created, no display callback exists for the window. It is
		/// the responsibility of the programmer to install a display callback for the
		/// window before the window is shown. A display callback must be registered
		/// for any window that is shown. If a window becomes displayed without a display
		/// callback being registered, a fatal error occurs. Passing null to glutDisplayFunc
		/// is illegal as of GLUT 3.0; there is no way to "deregister" a display callback
		/// (though another callback routine can always be registered).
		/// Upon return from the display callback, the normal damaged state of the window
		/// (returned by calling Glut.glutLayerGet(Glut.GLUT_NORMAL_DAMAGED) is cleared.
		/// If there is no overlay display callback registered the overlay damaged state
		/// of the window (returned by calling Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAGED)
		/// is also cleared.
		/// </remarks>
		public static void glutDisplayFunc(Delegates.DisplayCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutDisplayFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutDisplayFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutFullScreen()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutFullScreen();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutFullScreen();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutLeaveFullScreen()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutLeaveFullScreen();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutLeaveFullScreen();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		
		public static void glutIdleFunc(Delegates.IdleCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutIdleFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutIdleFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glutReshapeFunc(Delegates.ReshapeCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutReshapeFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutReshapeFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// Sets the keyboard callback for the current window.
		/// </summary>
		/// <param name="func">The new keyboard callback function. See Tao.FreeGlut.Glut.KeyboardCallback.</param>
		/// <remarks>
		/// glutKeyboardFunc sets the keyboard callback for the current window.  When
		/// a user types into the window, each key press generating an ASCII character
		/// will generate a keyboard callback. The key callback parameter is the generated
		/// ASCII character. The state of modifier keys such as Shift cannot be determined
		/// directly; their only effect will be on the returned ASCII data. The x and
		/// y callback parameters indicate the mouse location in window relative coordinates
		/// when the key was pressed.  When a new window is created, no keyboard callback
		/// is initially registered, and ASCII key strokes in the window are ignored.
		/// Passing null to glutKeyboardFunc disables the generation of keyboard callbacks.
		/// During a keyboard callback, Tao.FreeGlut.Glut.glutGetModifiers() may be called
		/// to determine the state of modifier keys when the keystroke generating the
		/// callback occurred.
		/// Also, see Tao.FreeGlut.Glut.glutSpecialFunc(Tao.FreeGlut.Glut.SpecialCallback)
		/// for a means to detect non-ASCII key strokes.
		/// </remarks>
		public static void glutKeyboardFunc(Delegates.KeyboardCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutKeyboardFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutKeyboardFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the keyboard up (key release) callback for the current window.
		/// </summary>
		/// <param name="func">The new keyboard up callback function. See Tao.FreeGlut.Glut.KeyboardUpCallback.</param>
		/// <remarks>
		/// glutKeyboardUpFunc sets the keyboard up (key release) callback for the current
		/// window. When a user types into the window, each key release matching an ASCII
		/// character will generate a keyboard up callback. The key callback parameter
		/// is the generated ASCII character. The state of modifier keys such as Shift
		/// cannot be determined directly; their only effect will be on the returned
		/// ASCII data. The x and y callback parameters indicate the mouse location in
		/// window relative coordinates when the key was pressed. When a new window is
		/// created, no keyboard callback is initially registered, and ASCII key strokes
		/// in the window are ignored. Passing null to glutKeyboardUpFunc disables the
		/// generation of keyboard up callbacks.
		/// During a keyboard up callback, Tao.FreeGlut.Glut.glutGetModifiers() may be
		/// called to determine the state of modifier keys when the keystroke generating
		/// the callback occurred.
		/// To avoid the reporting of key release/press pairs due to auto repeat, use
		/// Tao.FreeGlut.Glut.glutIgnoreKeyRepeat(System.Int32) to ignore auto repeated
		/// keystrokes.
		/// There is no guarantee that the keyboard press callback will match the exact
		/// ASCII character as the keyboard up callback. For example, the key down may
		/// be for a lowercase b, but the key release may report an uppercase B if the
		/// shift state has changed. The same applies to symbols and control characters.
		/// The precise behavior is window system dependent.
		/// Use Tao.FreeGlut.Glut.glutSpecialUpFunc(Tao.FreeGlut.Glut.SpecialUpCallback)
		/// for a means to detect non-ASCII key release.
		/// </remarks>
		public static void glutKeyboardUpFunc(Delegates.KeyboardUpCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutKeyboardUpFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutKeyboardUpFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Leaves the main loop.
		/// </summary>
		public static void glutLeaveMainLoop()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutLeaveMainLoop();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutLeaveMainLoop();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Enters the GLUT event processing loop.
		/// </summary>
		/// <remarks>
		/// glutMainLoop enters the GLUT event processing loop. This routine should be
		/// called at most once in a GLUT program. Once called, this routine will never
		/// return. It will call as necessary any callbacks (delegates) that have been
		/// registered.
		/// </remarks>
		public static void glutMainLoop()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutMainLoop();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutMainLoop();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the motion callbacks for the current window.
		/// </summary>
		/// <param name="func">The new motion callback function. See Tao.FreeGlut.Glut.MotionCallback.</param>
		/// <remarks>
		/// glutMotionFunc sets the motion callback for the current window. The motion
		/// callback for a window is called when the mouse moves within the window while
		/// one or more mouse buttons are pressed.
		/// The x and y callback parameters indicate the mouse location in window relative
		/// coordinates.
		/// Passing null to glutMotionFunc disables the generation of the motion callback.
		/// </remarks>
		public static void glutMotionFunc(Delegates.MotionCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutMotionFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutMotionFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the mouse callback for the current window.
		/// </summary>
		/// <param name="func">The new mouse callback function. See Tao.FreeGlut.Glut.MouseCallback.</param>
		/// <remarks>
		/// glutMouseFunc sets the mouse callback for the current window. When a user
		/// presses and releases mouse buttons in the window, each press and each release
		/// generates a mouse callback. The button parameter is one of Tao.FreeGlut.Glut.GLUT_LEFT_BUTTON,
		/// Tao.FreeGlut.Glut.GLUT_MIDDLE_BUTTON, or Tao.FreeGlut.Glut.GLUT_RIGHT_BUTTON.
		/// For systems with only two mouse buttons, it may not be possible to generate
		/// the GLUT_MIDDLE_BUTTON callback. For systems with a single mouse button,
		/// it may be possible to generate only a GLUT_LEFT_BUTTON callback. The state
		/// parameter is either Tao.FreeGlut.Glut.GLUT_UP or Tao.FreeGlut.Glut.GLUT_DOWN
		/// indicating whether the callback was due to a release or press respectively.
		/// The x and y callback parameters indicate the window relative coordinates
		/// when the mouse button state changed. If a GLUT_DOWN callback for a specific
		/// button is triggered, the program can assume a GLUT_UP callback for the same
		/// button will be generated (assuming the window still has a mouse callback
		/// registered) when the mouse button is released even if the mouse has moved
		/// outside the window.
		/// If a menu is attached to a button for a window, mouse callbacks will not
		/// be generated for that button.
		/// During a mouse callback, Tao.FreeGlut.Glut.glutGetModifiers() may be called
		/// to determine the state of modifier keys when the mouse event generating the
		/// callback occurred.
		/// Passing null to glutMouseFunc disables the generation of mouse callbacks.
		/// </remarks>
		public static void glutMouseFunc(Delegates.MouseCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutMouseFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutMouseFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the mouse wheel callback.
		/// </summary>
		/// <param name="func">The new mouse wheel callback function. See Tao.FreeGlut.Glut.MouseWheelCallback.</param>
		public static void glutMouseWheelFunc(Delegates.MouseWheelCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutMouseWheelFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutMouseWheelFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the overlay display callback for the current window.
		/// </summary>
		/// <param name="func">The new overlay display callback function. See Tao.FreeGlut.Glut.OverlayDisplayCallback.</param>
		/// <remarks>
		/// glutOverlayDisplayFunc sets the overlay display callback for the current window.
		/// The overlay display callback is functionally the same as the window's display
		/// callback except that the overlay display callback is used to redisplay the
		/// window's overlay.
		/// When GLUT determines that the overlay display for the window needs to be
		/// redisplayed, the overlay display callback for the window is called. Before
		/// the callback, the current window is set to the window needing to be redisplayed
		/// and the layer in use is set to the overlay. The overlay display callback
		/// is called with no parameters. The entire overlay region should be redisplayed
		/// in response to the callback (this includes ancillary buffers if your program
		/// depends on their state).
		/// GLUT determines when the overlay display callback should be triggered based
		/// on the window's overlay redisplay state. The overlay redisplay state for
		/// a window can be either set explicitly by calling Tao.FreeGlut.Glut.glutPostOverlayRedisplay()
		/// or implicitly as the result of window damage reported by the window system.
		/// Multiple posted overlay redisplays for a window are coalesced by GLUT to
		/// minimize the number of overlay display callbacks called.
		/// Upon return from the overlay display callback, the overlay damaged state
		/// of the window (returned by calling Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAMGED)
		/// is cleared.
		/// The overlay display callback can be deregistered by passing null to Tao.FreeGlut.Glut.glutOverlayDisplayFunc(Tao.FreeGlut.Glut.OverlayDisplayCallback).
		/// The overlay display callback is initially null when an overlay is established.
		/// See Tao.FreeGlut.Glut.glutDisplayFunc(Tao.FreeGlut.Glut.DisplayCallback)
		/// to understand how the display callback alone is used if an overlay display
		/// callback is not registered.
		/// </remarks>
		public static void glutOverlayDisplayFunc(Delegates.OverlayDisplayCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutOverlayDisplayFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutOverlayDisplayFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the passive motion callbacks for the current window.
		/// </summary>
		/// <param name="func">The new passive motion callback function. See Tao.FreeGlut.Glut.PassiveMotionCallback.</param>
		/// <remarks>
		/// glutPassiveMotionFunc sets the passive motion callback for the current window.
		/// The passive motion callback for a window is called when the mouse moves within
		/// the window while no mouse buttons are pressed.
		/// The x and y callback parameters indicate the mouse location in window relative
		/// coordinates.
		/// Passing null to glutPassiveMotionFunc disables the generation of the passive
		/// motion callback.
		/// </remarks>
		public static void glutPassiveMotionFunc(Delegates.PassiveMotionCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutPassiveMotionFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutPassiveMotionFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutSetOption(int option, int value)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutSetOption(option, value);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutSetOption(option, value);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the special keyboard callback for the current window.
		/// </summary>
		/// <param name="func">The new special callback function. See Tao.FreeGlut.Glut.SpecialCallback.</param>
		/// <remarks>
		/// glutSpecialFunc sets the special keyboard callback for the current window.
		/// The special keyboard callback is triggered when keyboard function or directional
		/// keys are pressed. The key callback parameter is a GLUT_KEY_* constant for
		/// the special key pressed. The x and y callback parameters indicate the mouse
		/// in window relative coordinates when the key was pressed. When a new window
		/// is created, no special callback is initially registered and special key strokes
		/// in the window are ignored.  Passing null to glutSpecialFunc disables the
		/// generation of special callbacks.
		/// During a special callback, Tao.FreeGlut.Glut.glutGetModifiers() may be called
		/// to determine the state of modifier keys when the keystroke generating the
		/// callback occurred.
		/// An implementation should do its best to provide ways to generate all the
		/// GLUT_KEY_* special keys. The available GLUT_KEY_* values are:
		/// Value Description Tao.FreeGlut.Glut.GLUT_KEY_F1 F1 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F2
		/// F2 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F3 F3 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F4
		/// F4 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F5 F5 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F6
		/// F6 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F7 F7 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F8
		/// F8 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F9 F9 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F10
		/// F10 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F11 F11 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F12
		/// F12 function key.  Tao.FreeGlut.Glut.GLUT_KEY_LEFT Left directional key.
		/// Tao.FreeGlut.Glut.GLUT_KEY_UP Up directional key.  Tao.FreeGlut.Glut.GLUT_KEY_RIGHT
		/// Right directional key.  Tao.FreeGlut.Glut.GLUT_KEY_DOWN Down directional
		/// key.  Tao.FreeGlut.Glut.GLUT_KEY_PAGE_UP Page up directional key.  Tao.FreeGlut.Glut.GLUT_KEY_PAGE_DOWN
		/// Page down directional key.  Tao.FreeGlut.Glut.GLUT_KEY_HOME Home directional
		/// key.  Tao.FreeGlut.Glut.GLUT_KEY_END End directional key.  Tao.FreeGlut.Glut.GLUT_KEY_INSERT
		/// Insert directional key.
		/// Note that the escape, backspace, and delete keys are generated as an ASCII
		/// character.
		/// </remarks>
		public static void glutSpecialFunc(Delegates.SpecialCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutSpecialFunc(func);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutSpecialFunc(func);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Sets the special keyboard up (key release) callback for the current window.
		/// </summary>
		/// <param name="func">The new special keyboard up callback function.  Tao.FreeGlut.Glut.SpecialUpCallback.</param>
		/// <remarks>
		/// glutSpecialUpFunc sets the special keyboard up (key release) callback for
		/// the current window. The special keyboard up callback is triggered when keyboard
		/// function or directional keys are released. The key callback parameter is
		/// a GLUT_KEY_* constant for the special key pressed. The x and y callback parameters
		/// indicate the mouse in window relative coordinates when the key was pressed.
		/// When a new window is created, no special up callback is initially registered
		/// and special key releases in the window are ignored. Passing null to glutSpecialUpFunc
		/// disables the generation of special up callbacks.
		/// During a special up callback, Tao.FreeGlut.Glut.glutGetModifiers() may be
		/// called to determine the state of modifier keys when the key release generating
		/// the callback occurred.
		/// To avoid the reporting of key release/press pairs due to auto repeat, use
		/// Tao.FreeGlut.Glut.glutIgnoreKeyRepeat(System.Int32) to ignore auto repeated
		/// keystrokes.
		/// An implementation should do its best to provide ways to generate all the
		/// GLUT_KEY_* special keys. The available GLUT_KEY_* values are:
		/// Value Description Tao.FreeGlut.Glut.GLUT_KEY_F1 F1 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F2
		/// F2 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F3 F3 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F4
		/// F4 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F5 F5 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F6
		/// F6 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F7 F7 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F8
		/// F8 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F9 F9 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F10
		/// F10 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F11 F11 function key.  Tao.FreeGlut.Glut.GLUT_KEY_F12
		/// F12 function key.  Tao.FreeGlut.Glut.GLUT_KEY_LEFT Left directional key.
		/// Tao.FreeGlut.Glut.GLUT_KEY_UP Up directional key.  Tao.FreeGlut.Glut.GLUT_KEY_RIGHT
		/// Right directional key.  Tao.FreeGlut.Glut.GLUT_KEY_DOWN Down directional
		/// key.  Tao.FreeGlut.Glut.GLUT_KEY_PAGE_UP Page up directional key.  Tao.FreeGlut.Glut.GLUT_KEY_PAGE_DOWN
		/// Page down directional key.  Tao.FreeGlut.Glut.GLUT_KEY_HOME Home directional
		/// key.  Tao.FreeGlut.Glut.GLUT_KEY_END End directional key.  Tao.FreeGlut.Glut.GLUT_KEY_INSERT
		/// Insert directional key.
		/// Note that the escape, backspace, and delete keys are generated as an ASCII
		/// character.
		/// </remarks>
		public static void glutSpecialUpFunc (Delegates.SpecialUpCallback func)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glutSpecialUpFunc (func);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glutSpecialUpFunc (func);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException ();
		}
		public static void glutSetCursor (int cursor)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glutSetCursor (cursor);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glutSetCursor (cursor);
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException ();
		}
		
		public static int glutEnterGameMode()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
				{
					return Linux.Methods.glutEnterGameMode();
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					return Windows.Methods.glutEnterGameMode();
				}
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutLeaveGameMode()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutLeaveGameMode();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutLeaveGameMode();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutGameModeString(string str)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutGameModeString(str);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutGameModeString(str);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutSetWindow(int handle)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutSetWindow(handle);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutSetWindow(handle);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutSetWindowTitle(string title)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutSetWindowTitle(title);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutSetWindowTitle(title);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutPositionWindow(int x, int y)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutPositionWindow(x, y);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutPositionWindow(x, y);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutReshapeWindow(int width, int height)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutReshapeWindow(width, height);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutReshapeWindow(width, height);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutShowWindow()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutShowWindow();
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutShowWindow();
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutHideWindow ()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glutHideWindow ();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glutHideWindow ();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException ();
		}
		public static void glutPostRedisplay ()
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glutPostRedisplay ();
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glutPostRedisplay ();
				return;
			case PlatformID.Xbox:
				break;
			}
			throw new PlatformNotSupportedException ();
		}
		
		public static int glutGetModifiers()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutGetModifiers();
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutGetModifiers();
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutWarpPointer (int x, int y)
		{
			switch (Environment.OSVersion.Platform)
			{
			case PlatformID.MacOSX:
				break;
			case PlatformID.Unix:
				Linux.Methods.glutWarpPointer (x, y);
				return;
			case PlatformID.Win32NT:
			case PlatformID.Win32S:
			case PlatformID.Win32Windows:
			case PlatformID.WinCE:
				Windows.Methods.glutWarpPointer (x, y);
				return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}


		public static void glutWireCone(double baseWidth, double height, int slices, int stacks)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					Linux.Methods.glutWireCone(baseWidth, height, slices, stacks);
					return;
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					Windows.Methods.glutWireCone(baseWidth, height, slices, stacks);
					return;
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}

		public static int glutGet(Constants.GlutStates state)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutGet(state);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutGet(state);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}

		/// <summary>
		/// Returns the height of a given font, in pixels.
		/// </summary>
		/// <param name="font">A bitmapped font identifier.</param>
		/// <value>0 if font is invalid, otherwise, the font's height, in pixels.</value>
		public static float glutStrokeHeight(IntPtr font)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutStrokeHeight(font);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutStrokeHeight(font);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Returns the length of a bitmap font string.
		/// </summary>
		/// <param name="font">Bitmap font to use. For valid values, see the Tao.FreeGlut.Glut.glutBitmapCharacter(System.IntPtr,System.Int32) description.</param>
		/// <param name="text">Text string.</param>
		/// <returns>Length of string in pixels.</returns>
		/// <remarks>
		/// glutBitmapLength returns the length in pixels of a string (8-bit characters).
		/// This length is equivalent to summing all the widths returned by Tao.FreeGlut.Glut.glutBitmapWidth(System.IntPtr,System.Int32)
		/// for each character in the string.
		/// </remarks>
		public static int glutStrokeLength(IntPtr font, string text)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
					return Linux.Methods.glutStrokeLength(font, text);
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					return Windows.Methods.glutStrokeLength(font, text);
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutStrokeCharacter(IntPtr font, char chr)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
				{
					Linux.Methods.glutStrokeCharacter(font, chr);
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					Windows.Methods.glutStrokeCharacter(font, chr);
					return;
				}
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
		public static void glutStrokeString(IntPtr font, string text)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
				{
					Linux.Methods.glutStrokeString(font, text);
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					Windows.Methods.glutStrokeString(font, text);
					return;
				}
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}

		public static void glutTimerFunc(int msecs, Delegates.TimerCallback func, int value)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
					break;
				case PlatformID.Unix:
				{
					Linux.Methods.glutTimerFunc(msecs, func, value);
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					Windows.Methods.glutTimerFunc(msecs, func, value);
					return;
				}
				case PlatformID.Xbox:
					break;
			}
			throw new PlatformNotSupportedException();
		}
	}
}
