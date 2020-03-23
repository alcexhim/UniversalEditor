using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeGLUT.Windows
{
    /// <summary>
    /// FreeGLUT (OpenGL Utility Toolkit) binding for .NET, implementing FreeGlut
    /// 2.4.0. Derived from Tao Framework's FreeGLUT binding.
    /// </summary>
    public class Methods
    {
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Winapi;
        
        private const string FREEGLUT_LIBRARY = "freeglut.dll";

        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutAddMenuEntry(string name, int val);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutAddSubMenu(string name, int menu);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutAttachMenu(int button);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutBitmapCharacter(IntPtr font, int character);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutBitmapHeight(IntPtr font);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutBitmapLength(IntPtr font, string text);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutBitmapString(IntPtr font, string str);
        //
        // Summary:
        //     Returns the width of a bitmap character.
        //
        // Parameters:
        //   font:
        //     Bitmap font to use. For valid values see the Tao.FreeGlut.Glut.glutBitmapCharacter(System.IntPtr,System.Int32)
        //     description.
        //
        //   character:
        //     Character to return width of (not confined to 8 bits).
        //
        // Returns:
        //     Returns the width in pixels of a bitmap character in a supported bitmap font.
        //
        // Remarks:
        //     glutBitmapWidth returns the width in pixels of a bitmap character in a supported
        //     bitmap font. While the width of characters in a font may vary (though fixed
        //     width fonts do not vary), the maximum height characteristics of a particular
        //     font are fixed.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutBitmapWidth(IntPtr font, int character);
        //
        // Summary:
        //     Sets the dial and button box button callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new button box callback function. See Tao.FreeGlut.Glut.ButtonBoxCallback.
        //
        // Remarks:
        //      glutButtonBoxFunc sets the dial and button box button callback for the current
        //     window. The dial and button box button callback for a window is called when
        //     the window has dial and button box input focus (normally, when the mouse
        //     is in the window) and the user generates dial and button box button presses.
        //     The button parameter will be the button number (starting at one). The number
        //     of available dial and button box buttons can be determined with Glut.glutDeviceGet(Glut.GLUT_NUM_BUTTON_BOX_BUTTONS).
        //     The state is either Tao.FreeGlut.Glut.GLUT_UP or Tao.FreeGlut.Glut.GLUT_DOWN
        //     indicating whether the callback was due to a release or press respectively.
        //     Registering a dial and button box button callback when a dial and button
        //     box device is not available is ineffectual and not an error. In this case,
        //     no dial and button box button callbacks will be generated.
        //     Passing null to glutButtonBoxFunc disables the generation of dial and button
        //     box button callbacks. When a new window is created, no dial and button box
        //     button callback is initially registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutButtonBoxFunc(FreeGLUT.Delegates.ButtonBoxCallback func);
        //
        // Summary:
        //     Changes the specified menu item in the current menu into a menu entry.
        //
        // Parameters:
        //   entry:
        //     Index into the menu items of the current menu (1 is the topmost menu item).
        //
        //   name:
        //     string to display in the menu entry.
        //
        //   val:
        //     Value to return to the menu's callback function if the menu entry is selected.
        //
        // Remarks:
        //     glutChangeToMenuEntry changes the specified menu entry in the current menu
        //     into a menu entry. The entry parameter determines which menu item should
        //     be changed, with one being the topmost item. entry must be between 1 and
        //     Glut.glutGet(Glut.GLUT_MENU_NUM_ITEMS) inclusive. The menu item to change
        //     does not have to be a menu entry already. The string name will be displayed
        //     for the newly changed menu entry. The val will be returned to the menu's
        //     callback if this menu entry is selected.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutChangeToMenuEntry(int entry, string name, int val);
        //
        // Summary:
        //     Changes the specified menu item in the current menu into a sub-menu trigger.
        //
        // Parameters:
        //   entry:
        //     Index into the menu items of the current menu (1 is the topmost menu item).
        //
        //   name:
        //     string to display in the menu item to cascade the sub-menu from.
        //
        //   menu:
        //     Identifier of the menu to cascade from this sub-menu menu item.
        //
        // Remarks:
        //     glutChangeToSubMenu changes the specified menu item in the current menu into
        //     a sub-menu trigger. The entry parameter determines which menu item should
        //     be changed, with one being the topmost item. entry must be between 1 and
        //     Glut.glutGet(Glut.GLUT_MENU_NUM_ITEMS) inclusive. The menu item to change
        //     does not have to be a sub-menu trigger already. The string name will be displayed
        //     for the newly changed sub-menu trigger.  The menu identifier names the sub-menu
        //     to cascade from the newly added sub-menu trigger.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutChangeToSubMenu(int entry, string name, int menu);
        //
        // Summary:
        //     Sets the close callback.
        //
        // Parameters:
        //   func:
        //     The new close callback function. See Tao.FreeGlut.Glut.CloseCallback.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutCloseFunc(FreeGLUT.Delegates.CloseCallback func);
        //
        // Summary:
        //     Copies the logical colormap for the layer in use from a specified window
        //     to the current window.
        //
        // Parameters:
        //   win:
        //     The identifier of the window to copy the logical colormap from.
        //
        // Remarks:
        //      glutCopyColormap copies (lazily if possible to promote sharing) the logical
        //     colormap from a specified window to the current window's layer in use.  The
        //     copy will be from the normal plane to the normal plane; or from the overlay
        //     to the overlay (never across different layers). Once a colormap has been
        //     copied, avoid setting cells in the colormap with Tao.FreeGlut.Glut.glutSetColor(System.Int32,System.Single,System.Single,System.Single)
        //     since that will force an actual copy of the colormap if it was previously
        //     copied by reference. glutCopyColormap should only be called when both the
        //     current window and the win window are color index windows.
        //     EXAMPLE
        //     Here is an example of how to create two color index GLUT windows with their
        //     colormaps loaded identically and so that the windows are likely to share
        //     the same colormap:
        //     int win1, win2; Glut.glutInitDisplayMode(Glut.GLUT_INDEX); win1 = Glut.glutCreateWindow("First
        //     color index window"); Glut.glutSetColor(0, 0.0f, 0.0f, 0.0f); // black Glut.glutSetColor(1,
        //     0.5f, 0.5f, 0.5f); // gray Glut.glutSetColor(2, 1.0f, 1.0f, 1.0f); // white
        //     Glut.glutSetColor(3, 1.0f, 0.0f, 0.0f); // red win2 = Glut.glutCreateWindow("Second
        //     color index window"); Glut.glutCopyColormap(win1);
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutCopyColormap(int win);
        //
        // Summary:
        //     Creates a new pop-up menu.
        //
        // Parameters:
        //   func:
        //     The callback function for the menu that is called when a menu entry from
        //     the menu is selected. The value passed to the callback is determined by the
        //     value for the selected menu entry. See Tao.FreeGlut.Glut.CreateMenuCallback.
        //
        // Returns:
        //     Returns a unique small integer identifier. The range of allocated identifiers
        //     starts at one. The menu identifier range is separate from the window identifier
        //     range.
        //
        // Remarks:
        //      glutCreateMenu creates a new pop-up menu and returns a unique small integer
        //     identifier. The range of allocated identifiers starts at one. The menu identifier
        //     range is separate from the window identifier range.  Implicitly, the current
        //     menu is set to the newly created menu. This menu identifier can be used when
        //     calling Tao.FreeGlut.Glut.glutSetMenu(System.Int32).
        //     When the menu callback is called because a menu entry is selected for the
        //     menu, the current menu will be implicitly set to the menu with the selected
        //     entry before the callback is made.
        //     EXAMPLE
        //     Here is a quick example of how to create a GLUT popup menu with two submenus
        //     and attach it to the right button of the current window:
        //     int submenu1, submenu2; submenu1 = Glut.glutCreateMenu(selectMessage); Glut.glutAddMenuEntry("abc",
        //     1); Glut.glutAddMenuEntry("ABC", 2); submenu2 = Glut.glutCreateMenu(selectColor);
        //     Glut.glutAddMenuEntry("Green", 1); Glut.glutAddMenuEntry("Red", 2); Glut.glutAddMenuEntry("White",
        //     3); Glut.glutCreateMenu(selectFont); Glut.glutAddMenuEntry("9 by 15", 0);
        //     Glut.glutAddMenuEntry("Times Roman 10", 1); Glut.glutAddMenuEntry("Times
        //     Roman 24", 2); Glut.glutAddSubMenu("Messages", submenu1); Glut.glutAddSubMenu("Color",
        //     submenu2); Glut.glutAttachMenu(Glut.GLUT_RIGHT_BUTTON);
        //     X IMPLEMENTATION NOTES
        //     If available, GLUT for X will take advantage of overlay planes for implementing
        //     pop-up menus. The use of overlay planes can eliminate display callbacks when
        //     pop-up menus are deactivated. The SERVER_OVERLAY_VISUALS convention is used
        //     to determine if overlay visuals are available.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern int glutCreateMenu(FreeGLUT.Delegates.CreateMenuCallback func);
        //
        // Summary:
        //     Creates a subwindow.
        //
        // Parameters:
        //   win:
        //     Identifier of the subwindow’s parent window.
        //
        //   x:
        //     Window X location in pixels relative to parent window’s origin.
        //
        //   y:
        //     Window Y location in pixels relative to parent window’s origin.
        //
        //   width:
        //     Width in pixels.
        //
        //   height:
        //     Height in pixels.
        //
        // Returns:
        //     The value returned is a unique small integer identifier for the window. The
        //     range of allocated identifiers starts at one.
        //
        // Remarks:
        //      glutCreateSubWindow creates a subwindow of the window identified by win
        //     of size width and height at location x and y within the current window. Implicitly,
        //     the current window is set to the newly created subwindow.
        //     Each created window has a unique associated OpenGL context. State changes
        //     to a window’s associated OpenGL context can be done immediately after the
        //     window is created.
        //     The display state of a window is initially for the window to be shown. But
        //     the window’s display state is not actually acted upon until Tao.FreeGlut.Glut.glutMainLoop()
        //     is entered. This means until glutMainLoop is called, rendering to a created
        //     window is ineffective. Subwindows can not be iconified.
        //     Subwindows can be nested arbitrarily deep.
        [DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutCreateSubWindow(int win, int x, int y, int width, int height);
        [DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutCreateWindow(string name);
        //
        // Summary:
        //     Destroys the specified menu.
        //
        // Parameters:
        //   menu:
        //     The identifier of the menu to destroy.
        //
        // Remarks:
        //     glutDestroyMenu destroys the specified menu by menu. If menu was the current
        //     menu, the current menu becomes invalid and Tao.FreeGlut.Glut.glutGetMenu()
        //     will return zero.
        [DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutDestroyMenu(int menu);
        //
        // Summary:
        //     Destroys the specified window.
        //
        // Parameters:
        //   win:
        //     Identifier of GLUT window to destroy.
        //
        // Remarks:
        //     glutDestroyWindow destroys the window specified by win and the window’s associated
        //     OpenGL context, logical colormap (if the window is color index), and overlay
        //     and related state (if an overlay has been established).  Any subwindows of
        //     destroyed windows are also destroyed by glutDestroyWindow. If win was the
        //     current window, the current window becomes invalid (Tao.FreeGlut.Glut.glutGetWindow()
        //     will return zero).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutDestroyWindow(int win);
        //
        // Summary:
        //     Detaches an attached mouse button from the current window.
        //
        // Parameters:
        //   button:
        //     The button to detach a menu.
        //
        // Remarks:
        //     glutDetachMenu detaches an attached mouse button from the current window.
        //     button should be one of Tao.FreeGlut.Glut.GLUT_LEFT_BUTTON, Tao.FreeGlut.Glut.GLUT_MIDDLE_BUTTON,
        //     and Tao.FreeGlut.Glut.GLUT_RIGHT_BUTTON. Note that the menu is attached to
        //     the button by identifier, not by reference.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutDetachMenu(int button);
        //
        // Summary:
        //     Retrieves GLUT device information represented by integers.
        //
        // Parameters:
        //   info:
        //      Name of device information to retrieve. Valid values are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_HAS_KEYBOARD Non-zero if a keyboard
        //     is available; zero if not available. For most GLUT implementations, a keyboard
        //     can be assumed.  Tao.FreeGlut.Glut.GLUT_HAS_MOUSE Non-zero if a mouse is
        //     available; zero if not available. For most GLUT implementations, a keyboard
        //     can be assumed.  Tao.FreeGlut.Glut.GLUT_HAS_SPACEBALL Non-zero if a Spaceball
        //     is available; zero if not available.  Tao.FreeGlut.Glut.GLUT_HAS_DIAL_AND_BUTTON_BOX
        //     Non-zero if a dial and button box is available; zero if not available.  Tao.FreeGlut.Glut.GLUT_HAS_TABLET
        //     Non-zero if a tablet is available; zero if not available.  Tao.FreeGlut.Glut.GLUT_NUM_MOUSE_BUTTONS
        //     Number of buttons supported by the mouse. If no mouse is supported, zero
        //     is returned.  Tao.FreeGlut.Glut.GLUT_NUM_SPACEBALL_BUTTONS Number of buttons
        //     supported by the Spaceball. If no Spaceball is supported, zero is returned.
        //      Tao.FreeGlut.Glut.GLUT_NUM_BUTTON_BOX_BUTTONS Number of buttons supported
        //     by the dial and button box device.  If no dials and button box device is
        //     supported, zero is returned.  Tao.FreeGlut.Glut.GLUT_NUM_DIALS Number of
        //     dials supported by the dial and button box device. If no dials and button
        //     box device is supported, zero is returned.  Tao.FreeGlut.Glut.GLUT_NUM_TABLET_BUTTONS
        //     Number of buttons supported by the tablet. If no tablet is supported, zero
        //     is returned.
        //
        // Returns:
        //     glutDeviceGet retrieves GLUT device information represented by integers.
        //     The info parameter determines what type of device information to return.
        //     Requesting device information for an invalid GLUT device information name
        //     returns negative one.
        //
        // Remarks:
        //     glutDeviceGet retrieves GLUT device information represented by integers.
        //     The info parameter determines what type of device information to return.
        //     Requesting device information for an invalid GLUT device information name
        //     returns negative one.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutDeviceGet(int info);
        //
        // Summary:
        //     Sets the dial and button box dials callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new dials callback function. See Tao.FreeGlut.Glut.DialsCallback.
        //
        // Remarks:
        //      glutDialsFunc sets the dial and button box dials callback for the current
        //     window. The dial and button box dials callback for a window is called when
        //     the window has dial and button box input focus (normally, when the mouse
        //     is in the window) and the user generates dial and button box dial changes.
        //      The dial parameter will be the dial number (starting at one). The number
        //     of available dial and button box dials can be determined with Glut.glutDeviceGet(Glut.GLUT_NUM_DIALS).
        //     The val measures the absolute rotation in degrees. Dial values do not "roll
        //     over" with each complete rotation but continue to accumulate degrees (until
        //     the int dial value overflows).
        //     Registering a dial and button box dials callback when a dial and button box
        //     device is not available is ineffectual and not an error. In this case, no
        //     dial and button box dials callbacks will be generated.
        //     Passing null to glutDialsFunc disables the generation of dial and button
        //     box dials callbacks. When a new window is created, no dial and button box
        //     dials callback is initially registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutDialsFunc(FreeGLUT.Delegates.DialsCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutDisplayFunc(FreeGLUT.Delegates.DisplayCallback func);
        //
        // Summary:
        //     Enters GLUT's game mode.
        //
        // Returns:
        //     This is defined in the header as an int, however, from the documentation
        //     that I've seen, I believe it should be a void. You should check your game
        //     mode state after entering game mode with: Tao.FreeGlut.Glut.glutGameModeGet(System.Int32)
        //     passing appropriate parameters.
        //
        // Remarks:
        //      glutEnterGameMode is designed to enable high-performance fullscreen GLUT
        //     rendering, possibly at a different screen display format. Calling glutEnterGameMode
        //     creates a special fullscreen GLUT window (with its own callbacks and OpenGL
        //     rendering context state). If the game mode string describes a possible screen
        //     display format, GLUT also changes the screen display format to the one described
        //     by the game mode string.
        //     When game mode is entered, certain GLUT functionality is disable to facilitate
        //     high-performance fullscreen rendering. GLUT pop-up menus are not available
        //     while in game mode. Other created windows and subwindows are not displayed
        //     in GLUT game mode. Game mode will also hide all other applications running
        //     on the computer's display screen. The intent of these restrictions is to
        //     eliminate window clipping issues, permit screen display format changes, and
        //     permit fullscreen rendering optimization such as page flipping for fullscreen
        //     buffer swaps.
        //     The following GLUT routines are ignored in game mode:
        //     Tao.FreeGlut.Glut.glutFullScreen() Tao.FreeGlut.Glut.glutHideWindow() Tao.FreeGlut.Glut.glutIconifyWindow()
        //     Tao.FreeGlut.Glut.glutPopWindow() Tao.FreeGlut.Glut.glutPositionWindow(System.Int32,System.Int32)
        //     Tao.FreeGlut.Glut.glutPushWindow() Tao.FreeGlut.Glut.glutReshapeWindow(System.Int32,System.Int32)
        //     Tao.FreeGlut.Glut.glutSetIconTitle(System.String) Tao.FreeGlut.Glut.glutSetWindowTitle(System.String)
        //     Tao.FreeGlut.Glut.glutShowWindow()
        //     glutEnterGameMode can be called when already in game mode. This will destroy
        //     the previous game mode window (including any OpenGL rendering state) and
        //     create a new game mode window with a new OpenGL rendering context. Also if
        //     glutEnterGameMode is called when already in game mode and if the game mode
        //     string has changed and describes a possible screen display format, the new
        //     screen display format takes effect. A reshape callback is generated if the
        //     game mode window changes size due to a screen display format change.
        //     Re-entering game mode provides a mechanism for changing the screen display
        //     format while already in game mode. Note though that the game mode window's
        //     OpenGL state is lost in this process and the application is responsible for
        //     re-initializing the newly created game mode window OpenGL state when re-entering
        //     game mode.
        //     Game mode cannot be entered while pop-up menus are in use.
        //     Note that the glutEnterGameMode and Tao.FreeGlut.Glut.glutFullScreen() routines
        //     operate differently. Tao.FreeGlut.Glut.glutFullScreen() simply makes the
        //     current window match the size of the screen. Tao.FreeGlut.Glut.glutFullScreen()
        //     does not change the screen display format and does not disable any GLUT features
        //     such as pop-up menus; Tao.FreeGlut.Glut.glutFullScreen() continues to operate
        //     in a "windowed" mode of operation. glutEnterGameMode creates a new window
        //     style, possibly changes the screen display mode, limits GLUT functionality,
        //     and hides other applications.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutEnterGameMode();
        //
        // Summary:
        //     Sets the mouse enter/leave callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new entry callback function. See Tao.FreeGlut.Glut.EntryCallback.
        //
        // Remarks:
        //      glutEntryFunc sets the mouse enter/leave callback for the current window.
        //     The state callback parameter is either Tao.FreeGlut.Glut.GLUT_LEFT or Tao.FreeGlut.Glut.GLUT_ENTERED
        //     depending on if the mouse pointer has last left or entered the window.
        //     Passing null to glutEntryFunc disables the generation of the mouse enter/leave
        //     callback.
        //     Some window systems may not generate accurate enter/leave callbacks.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutEntryFunc(FreeGLUT.Delegates.EntryCallback func);
        //
        // Summary:
        //     Establishes an overlay (if possible) for the current window.
        //
        // Remarks:
        //      glutEstablishOverlay establishes an overlay (if possible) for the current
        //     window. The requested display mode for the overlay is determined by the initial
        //     display mode. glutLayerGet(GLUT_OVERLAY_POSSIBLE) can be called to determine
        //     if an overlay is possible for the current window with the current initial
        //     display mode. Do not attempt to establish an overlay when one is not possible;
        //     GLUT will terminate the program.
        //     If glutEstablishOverlay is called when an overlay already exists, the existing
        //     overlay is first removed, and then a new overlay is established. The state
        //     of the old overlay's OpenGL context is discarded.
        //     The initial display state of an overlay is shown, however the overlay is
        //     only actually shown if the overlay's window is shown.
        //     Implicitly, the window's layer in use changes to the overlay immediately
        //     after the overlay is established.
        //     EXAMPLE
        //     Establishing an overlay is a bit involved, but easy once you get the hang
        //     of it. Here is an example:
        //     int overlaySupport; int transparent, red, white; Glut.glutInitDisplayMode(Glut.GLUT_SINGLE
        //     | Glut.GLUT_INDEX); overlaySupport = Glut.glutLayerGet(Glut.GLUT_OVERLAY_POSSIBLE);
        //     if(overlaySupport) { Glut.glutEstablishOverlay(); Glut.glutHideOverlay();
        //     transparent = Glut.glutLayerGet(Glut.GLUT_TRANSPARENT_INDEX); Gl.glClearIndex(transparent);
        //     red = (transparent + 1) % Glut.glutGet(Glut.GLUT_WINDOW_COLORMAP_SIZE); white
        //     = (transparent + 2) % Glut.glutGet(Glut.GLUT_WINDOW_COLORMAP_SIZE); Glut.glutSetColor(red,
        //     1.0f, 0.0f, 0.0f); Glut.glutSetColor(white, 1.0f, 1.0f, 1.0f); Glut.glutOverlayDisplayFunc(redrawOverlay);
        //     Glut.glutReshapFunc(reshape); } else { System.Console.WriteLine("Sorry, no
        //     nifty overlay support!"); }
        //     If you setup an overlay and you install a reshape callback, you need to update
        //     the viewports and possibly projection matrices of both the normal plane and
        //     the overlay. For example, your reshape callback might look like this:
        //     private void Reshape(int w, int h) { if(overlaySupport) { Glut.glutUseLayer(Glut.GLUT_OVERLAY);
        //     Gl.glViewport(0, 0, w, h); Gl.glMatrixMode(Gl.GL_PROJECTION); Gl.glLoadIdentity();
        //     Glu.gluOrtho2D(0, w, 0, h); Gl.glScalef(1, -1, 1); Gl.glTranslatef(0, -h,
        //     0); Gl.glMatrixMode(Gl.GL_MODELVIEW); Glut.glutUseLayer(Glut.GLUT_NORMAL);
        //     } Gl.glViewport(0, 0, w, h); }
        //     See Tao.FreeGlut.Glut.glutOverlayDisplayFunc(Tao.FreeGlut.Glut.OverlayDisplayCallback)
        //     for an example showing one way to write your overlay display callback.
        //     X IMPLEMENTATION NOTES
        //     GLUT for X uses the SERVER_OVERLAY_VISUALS convention to determine if overlay
        //     visuals are available. While the convention allows for opaque overlays (no
        //     transparency) and overlays with the transparency specified as a bitmask,
        //     GLUT overlay management only provides access to transparent pixel overlays.
        //     Until RGBA overlays are better understood, GLUT only supports color index
        //     overlays.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutEstablishOverlay();
        //
        // Summary:
        //     Helps to easily determine whether a given OpenGL extension is supported.
        //
        // Parameters:
        //   extension:
        //     Name of OpenGL extension to query.
        //
        // Returns:
        //     Returns non-zero if the extension is supported, zero if not supported.
        //
        // Remarks:
        //      glutExtensionSupported helps to easily determine whether a given OpenGL
        //     extension is supported or not. The extension parameter names the extension
        //     to query. The supported extensions can also be determined with Gl.glGetString(Gl.GL_EXTENSIONS),
        //     but glutExtensionSupported does the correct parsing of the returned string.
        //     There must be a valid current window to call glutExtensionSupported.
        //     glutExtensionSupported only returns information about OpenGL extensions only.
        //     This means window system dependent extensions (for example, GLX extensions)
        //     are not reported by glutExtensionSupported.
        //     EXAMPLE
        //     if(!Glut.glutExtensionSupported("GL_EXT_texture")) { System.Console.WriteLine("Missing
        //     the texture extension!"); System.Environment.Exit(1); }
        //     Notice that the name argument includes both the GL prefix and the extension
        //     family prefix (EXT).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutExtensionSupported(string extension);
        //
        // Summary:
        //     Forces current window's joystick callback to be called.
        //
        // Remarks:
        //      glutForceJoystickFunc forces the current window's joystick callback to be
        //     called, reporting the latest joystick state.
        //     The joystick callback is called either due to polling of the joystick at
        //     the uniform timer interval set by Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32)'s
        //     pollInterval (specified in milliseconds) or in response to calling glutForceJoystickFunc.
        //     If the pollInterval is non-positive, no joystick polling is performed and
        //     the GLUT application must frequently (usually from an idle callback) call
        //     glutForceJoystickFunc.
        //     The joystick callback will be called once (if one exists) for each time glutForceJoystickFunc
        //     is called. The callback is called from Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32).
        //     That is, when Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32)
        //     returns, the callback will have already happened.
        //     X IMPLEMENTATION NOTES
        //     The GLUT 3.7 implementation of GLUT for X11 supports the joystick API, but
        //     not actual joystick input. A future implementation of GLUT for X11 may add
        //     joystick support.
        //     WIN32 IMPLEMENTATION NOTES
        //     The GLUT 3.7 implementation of GLUT for Win32 supports the joystick API and
        //     joystick input, but does so through the dated joySetCapture and joyGetPosEx
        //     Win32 Multimedia API. The GLUT 3.7 joystick support for Win32 has all the
        //     limitations of the Win32 Multimedia API joystick support.  A future implementation
        //     of GLUT for Win32 may use DirectInput.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutForceJoystickFunc();
        //
        // Summary:
        //     Requests that the current window be made full screen.
        //
        // Remarks:
        //      glutFullScreen requests that the current window be made full screen.  The
        //     exact semantics of what full screen means may vary by window system. The
        //     intent is to make the window as large as possible and disable any window
        //     decorations or borders added by the window system. The window width and height
        //     are not guaranteed to be the same as the screen width and height, but that
        //     is the intent of making a window full screen.
        //     glutFullScreen is defined to work only on top-level windows.
        //     The glutFullScreen requests are not processed immediately. The request is
        //     executed after returning to the main event loop. This allows multiple Tao.FreeGlut.Glut.glutReshapeWindow(System.Int32,System.Int32),
        //     Tao.FreeGlut.Glut.glutPositionWindow(System.Int32,System.Int32), and glutFullScreen
        //     requests to the same window to be coalesced.
        //     Subsequent glutReshapeWindow and glutPositionWindow requests on the window
        //     will disable the full screen status of the window.
        //     X IMPLEMENTATION NOTES
        //     In the X implementation of GLUT, full screen is implemented by sizing and
        //     positioning the window to cover the entire screen and posting the _MOTIF_WM_HINTS
        //     property on the window requesting absolutely no decorations.  Non-Motif window
        //     managers may not respond to _MOTIF_WM_HINTS.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutFullScreen();
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutLeaveFullScreen();
        //
        // Summary:
        //     Retrieves GLUT device information represented by integers.
        //
        // Parameters:
        //   mode:
        //     Name of game mode information to retrieve.
        //
        // Returns:
        //      Value Description Tao.FreeGlut.Glut.GLUT_GAME_MODE_ACTIVE Non-zero if GLUT's
        //     game mode is active; zero if not active. Game mode is not active initially.
        //     Game mode becomes active when Tao.FreeGlut.Glut.glutEnterGameMode() is called.
        //     Game mode becomes inactive when Tao.FreeGlut.Glut.glutLeaveGameMode() is
        //     called.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_POSSIBLE Non-zero if the game mode
        //     string last specified to Tao.FreeGlut.Glut.glutGameModeString(System.String)
        //     is a possible game mode configuration; zero otherwise. Being "possible" does
        //     not guarantee that if game mode is entered with Tao.FreeGlut.Glut.glutEnterGameMode()
        //     that the display settings will actually changed. Tao.FreeGlut.Glut.GLUT_GAME_MODE_DISPLAY_CHANGED
        //     should be called once game mode is entered to determine if the display mode
        //     is actually changed.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_WIDTH Width in pixels
        //     of the screen when game mode is activated.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_HEIGHT
        //     Height in pixels of the screen when game mode is activated.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_PIXEL_DEPTH
        //     Pixel depth of the screen when game mode is activiated.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_REFRESH_RATE
        //     Screen refresh rate in cyles per second (hertz) when game mode is activated.
        //     Zero is returned if the refresh rate is unknown or cannot be queried.  Tao.FreeGlut.Glut.GLUT_GAME_MODE_DISPLAY_CHANGED
        //     Non-zero if entering game mode actually changed the display settings. If
        //     the game mode string is not possible or the display mode could not be changed
        //     for any other reason, zero is returned.
        //
        // Remarks:
        //     glutGameModeGet retrieves GLUT game mode information represented by integers.
        //     The mode parameter determines what type of game mode information to return.
        //     Requesting game mode information for an invalid GLUT game mode information
        //     name returns negative one.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutGameModeGet(int mode);
        //
        // Summary:
        //     Sets the game mode configuration via a string.
        //
        // Parameters:
        //   str:
        //     string for selecting a game mode configuration.
        //
        // Remarks:
        //      glutGameModeString sets the game mode configuration via a string. The game
        //     mode configuration string for GLUT's fullscreen game mode describes the suitable
        //     screen width and height in pixels, the pixel depth in bits, and the video
        //     refresh frequency in hertz. The game mode configuration string can also specify
        //     a window system dependent display mode.
        //     The string is a list of zero or more capability descriptions seperated by
        //     spaces and tabs. Each capability description is a capability name that is
        //     followed by a comparator and a numeric value. (Unlike the display mode string
        //     specified using Tao.FreeGlut.Glut.glutInitDisplayString(System.String), the
        //     comparator and numeric value are not optional.) For example, "width>=640"
        //     and "bpp=32" are both valid criteria.
        //     The capability descriptions are translated into a set of criteria used to
        //     select the appropriate game mode configuration.
        //     The criteria are matched in strict left to right order of precdence. That
        //     is, the first specified criteria (leftmost) takes precedence over the later
        //     criteria for non-exact criteria (greater than, less than, etc. comparators).
        //      Exact criteria (equal, not equal compartors) must match exactly so precedence
        //     is not relevant.
        //     The numeric value is an integer that is parsed according to ANSI C's strtol(str,
        //     strptr, 0) behavior. This means that decimal, octal (leading 0), and hexidecimal
        //     values (leading 0x) are accepeted.
        //     The valid compartors are:
        //     Value Description = Equal.  != Not equal.  < Less than and preferring larger
        //     difference (the least is best).  > Greater than and preferring larger differences
        //     (the most is best).  <= Less than or equal and preferring larger difference
        //     (the least is best).  >= Greater than or equal and preferring more instead
        //     of less. This comparator is useful for allocating resources like color precsion
        //     or depth buffer precision where the maximum precison is generally preferred.
        //     Contrast with the tilde (~) comprator.  ~ Greater than or equal but preferring
        //     less instead of more. This compartor is useful for allocating resources such
        //     as stencil bits or auxillary color buffers where you would rather not over
        //     allocate.
        //     The valid capability names are:
        //     Value Description bpp Bits per pixel for the frame buffer.  height Height
        //     of the screen in pixels.  hertz Video refresh rate of the screen in hertz.
        //      num Number of the window system depenedent display mode configuration. 
        //     width Width of the screen in pixels.
        //     An additional compact screen resolution description format is supported.
        //      This compact description convienently encodes the screen resolution description
        //     in a single phrase. For example, "640x480:16@60" requests a 640 by 480 pixel
        //     screen with 16 bits per pixel at a 60 hertz video refresh rate. A compact
        //     screen resolution description can be mixed with conventional capability descriptions.
        //     The compact screen resolution description format is as follows:
        //     [width "x" height][":" bitsPerPixel]["@" videoRate]
        //     Unspecifed capability descriptions will result in unspecified criteria being
        //     generated. These unspecified criteria help glutGameModeString behave sensibly
        //     with terse game mode description strings.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutGameModeString(string str);
        //
        // Summary:
        //     Retrieves simple GLUT state represented by integers.
        //
        // Parameters:
        //   state:
        //      Name of state to retrieve. Valid values are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_WINDOW_X X location in pixels (relative
        //     to the screen origin) of the current window.  Tao.FreeGlut.Glut.GLUT_WINDOW_Y
        //     Y location in pixels (relative to the screen origin) of the current window.
        //      Tao.FreeGlut.Glut.GLUT_WINDOW_WIDTH Width in pixels of the current window.
        //      Tao.FreeGlut.Glut.GLUT_WINDOW_HEIGHT Height in pixels of the current window.
        //      Tao.FreeGlut.Glut.GLUT_WINDOW_BUFFER_SIZE Total number of bits for current
        //     window's color buffer. For an RGBA window, this is the sum of Tao.FreeGlut.Glut.GLUT_WINDOW_RED_SIZE,
        //     Tao.FreeGlut.Glut.GLUT_WINDOW_GREEN_SIZE, Tao.FreeGlut.Glut.GLUT_WINDOW_BLUE_SIZE,
        //     and Tao.FreeGlut.Glut.GLUT_WINDOW_ALPHA_SIZE. For color index windows, this
        //     is the size of the color indexes.  Tao.FreeGlut.Glut.GLUT_WINDOW_STENCIL_SIZE
        //     Number of bits in the current window's stencil buffer.  Tao.FreeGlut.Glut.GLUT_WINDOW_DEPTH_SIZE
        //     Number of bits in the current window's depth buffer.  Tao.FreeGlut.Glut.GLUT_WINDOW_RED_SIZE
        //     Number of bits of red stored the current window's color buffer.  Zero if
        //     the window is color index.  Tao.FreeGlut.Glut.GLUT_WINDOW_GREEN_SIZE Number
        //     of bits of green stored the current window's color buffer.  Zero if the window
        //     is color index.  Tao.FreeGlut.Glut.GLUT_WINDOW_BLUE_SIZE Number of bits of
        //     blue stored the current window's color buffer.  Zero if the window is color
        //     index.  Tao.FreeGlut.Glut.GLUT_WINDOW_ALPHA_SIZE Number of bits of alpha
        //     stored the current window's color buffer.  Zero if the window is color index.
        //      Tao.FreeGlut.Glut.GLUT_WINDOW_ACCUM_RED_SIZE Number of bits of red stored
        //     in the current window's accumulation buffer. Zero if the window is color
        //     index.  Tao.FreeGlut.Glut.GLUT_WINDOW_ACCUM_GREEN_SIZE Number of bits of
        //     green stored in the current window's accumulation buffer. Zero if the window
        //     is color index.  Tao.FreeGlut.Glut.GLUT_WINDOW_ACCUM_BLUE_SIZE Number of
        //     bits of blue stored in the current window's accumulation buffer. Zero if
        //     the window is color index.  Tao.FreeGlut.Glut.GLUT_WINDOW_ACCUM_ALPHA_SIZE
        //     Number of bits of alpha stored in the current window's accumulation buffer.
        //     Zero if the window is color index.  Tao.FreeGlut.Glut.GLUT_WINDOW_DOUBLEBUFFER
        //     One if the current window is double buffered, zero otherwise.  Tao.FreeGlut.Glut.GLUT_WINDOW_RGBA
        //     One if the current window is RGBA mode, zero otherwise (i.e., color index).
        //      Tao.FreeGlut.Glut.GLUT_WINDOW_PARENT The window number of the current window's
        //     parent; zero if the window is a top-level window.  Tao.FreeGlut.Glut.GLUT_WINDOW_NUM_CHILDREN
        //     The number of subwindows the current window has (not counting children of
        //     children).  Tao.FreeGlut.Glut.GLUT_WINDOW_COLORMAP_SIZE Size of current window's
        //     color index colormap; zero for RGBA color model windows.  Tao.FreeGlut.Glut.GLUT_WINDOW_NUM_SAMPLES
        //     Number of samples for multisampling for the current window.  Tao.FreeGlut.Glut.GLUT_WINDOW_STEREO
        //     One if the current window is stereo, zero otherwise.  Tao.FreeGlut.Glut.GLUT_WINDOW_CURSOR
        //     Current cursor for the current window.  Tao.FreeGlut.Glut.GLUT_SCREEN_WIDTH
        //     Width of the screen in pixels. Zero indicates the width is unknown or not
        //     available.  Tao.FreeGlut.Glut.GLUT_SCREEN_HEIGHT Height of the screen in
        //     pixels. Zero indicates the height is unknown or not available.  Tao.FreeGlut.Glut.GLUT_SCREEN_WIDTH_MM
        //     Width of the screen in millimeters. Zero indicates the width is unknown or
        //     not available.  Tao.FreeGlut.Glut.GLUT_SCREEN_HEIGHT_MM Height of the screen
        //     in millimeters. Zero indicates the height is unknown or not available.  Tao.FreeGlut.Glut.GLUT_MENU_NUM_ITEMS
        //     Number of menu items in the current menu.  Tao.FreeGlut.Glut.GLUT_DISPLAY_MODE_POSSIBLE
        //     Whether the current display mode is supported or not.  Tao.FreeGlut.Glut.GLUT_INIT_DISPLAY_MODE
        //     The initial display mode bit mask.  Tao.FreeGlut.Glut.GLUT_INIT_WINDOW_X
        //     The X value of the initial window position.  Tao.FreeGlut.Glut.GLUT_INIT_WINDOW_Y
        //     The Y value of the initial window position.  Tao.FreeGlut.Glut.GLUT_INIT_WINDOW_WIDTH
        //     The width value of the initial window size.  Tao.FreeGlut.Glut.GLUT_INIT_WINDOW_HEIGHT
        //     The height value of the initial window size.  Tao.FreeGlut.Glut.GLUT_ELAPSED_TIME
        //     Number of milliseconds since Tao.FreeGlut.Glut.glutInit() called (or first
        //     call to Glut.glutGet(Glut.GLUT_ELAPSED_TIME)).
        //
        // Returns:
        //     GLUT state represented by integers.
        //
        // Remarks:
        //     glutGet retrieves simple GLUT state represented by integers. The state parameter
        //     determines what type of state to return. Window capability state is returned
        //     for the layer in use. GLUT state names beginning with GLUT_WINDOW_ return
        //     state for the current window. GLUT state names beginning with GLUT_MENU_
        //     return state for the current menu. Other GLUT state names return global state.
        //     Requesting state for an invalid GLUT state name returns negative one.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutGet(Internal.FreeGLUT.Constants.GlutStates state);
        //
        // Summary:
        //     Retrieves a red, green, or blue component for a given color index colormap
        //     entry for the layer in use's logical colormap for the current window.
        //
        // Parameters:
        //   cell:
        //     Color cell index (starting at zero).
        //
        //   component:
        //     One of Tao.FreeGlut.Glut.GLUT_RED, Tao.FreeGlut.Glut.GLUT_GREEN, or Tao.FreeGlut.Glut.GLUT_BLUE.
        //
        // Returns:
        //     For valid color indices, the value returned is a floating point value between
        //     0.0 and 1.0 inclusive. glutGetColor will return -1.0 if the color index specified
        //     is an overlay's transparent index, less than zero, or greater or equal to
        //     the value returned by Glut.glutGet(Glut.GLUT_WINDOW_COLORMAP_SIZE), that
        //     is if the color index is transparent or outside the valid range of color
        //     indices.
        //
        // Remarks:
        //     glutGetColor retrieves a red, green, or blue component for a given color
        //     index colormap entry for the current window's logical colormap. The current
        //     window should be a color index window. cell should be zero or greater and
        //     less than the total number of colormap entries for the window.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static float glutGetColor(int cell, int component);
        //
        // Summary:
        //     Returns the identifier of the current menu.
        //
        // Returns:
        //     Returns the identifier of the current menu. If no menus exist or the previous
        //     current menu was destroyed, glutGetMenu returns zero.
        //
        // Remarks:
        //     Returns the identifier of the current menu. If no menus exist or the previous
        //     current menu was destroyed, glutGetMenu returns zero.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutGetMenu();
        //
        // Summary:
        //     Rerieves user data from a menu.
        //
        // Returns:
        //     A previously stored arbitrary user data System.IntPtr from the current menu.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static IntPtr glutGetMenuData();
        //
        // Summary:
        //     Returns the modifier key state when certain callbacks were generated.
        //
        // Returns:
        //      Returns the modifier key state at the time the input event for a keyboard,
        //     special, or mouse callback is generated. Valid values are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_ACTIVE_SHIFT Set if the Shift modifier
        //     or Caps Lock is active.  Tao.FreeGlut.Glut.GLUT_ACTIVE_CTRL Set if the Ctrl
        //     modifier is active.  Tao.FreeGlut.Glut.GLUT_ACTIVE_ALT Set if the Alt modifier
        //     is active.
        //
        // Remarks:
        //     glutGetModifiers returns the modifier key state at the time the input event
        //     for a keyboard, special, or mouse callback is generated. This routine may
        //     only be called while a keyboard, special, or mouse callback is being handled.
        //     The window system is permitted to intercept window system defined modifier
        //     key strokes or mouse buttons, in which case, no GLUT callback will be generated.
        //     This interception will be independent of use of glutGetModifiers.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutGetModifiers();
        //
        // Summary:
        //     Determine if an procedure or extension is available.
        //
        // Parameters:
        //   procName:
        //     Procedure name.
        //
        // Returns:
        //      Given a function name, searches for the function (or "procedure", hence
        //     "Proc") in internal tables.  If the function is found, a pointer to the function
        //     is returned. If the function is not found, System.IntPtr.Zero is returned.
        //     In addition to an internal freeglut table, this function will also consult
        //     glX (on X systems) or wgl (on WIN32 and WINCE), if the freeglut tables do
        //     not have the requested function. It should return any OpenGL, glX, or wgl
        //     function if those functions are available.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static IntPtr glutGetProcAddress(string procName);
        //
        // Summary:
        //     Returns the identifier of the current window.
        //
        // Returns:
        //     glutGetWindow returns the identifier of the current window. If no windows
        //     exist or the previously current window was destroyed, glutGetWindow returns
        //     zero.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutGetWindow();
        //
        // Summary:
        //     Get the user data for the current window.
        //
        // Returns:
        //     An System.IntPtr associated with the current window as set with Tao.FreeGlut.Glut.glutSetupWindowData(System.IntPtr).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static IntPtr glutGetWindowData();
        //
        // Summary:
        //     Hides the overlay of the current window.
        //
        // Remarks:
        //     glutHideOverlay hides the overlay of the current window. The effect of hiding
        //     an overlay takes place immediately. It is typically faster and less resource
        //     intensive to use these routines to control the display status of an overlay
        //     as opposed to removing and re-establishing the overlay.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutHideOverlay();
        //
        // Summary:
        //     Changes the display status of the current window.
        //
        // Remarks:
        //     glutHideWindow will hide the current window. The effect of hiding windows
        //     does not take place immediately. Instead the requests are saved for execution
        //     upon return to the GLUT event loop. Subsequent hide requests on a window
        //     replace the previously saved request for that window. The effect of hiding
        //     top-level windows is subject to the window system's policy for displaying
        //     windows.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutHideWindow();
        //
        // Summary:
        //     Changes the display status of the current window.
        //
        // Remarks:
        //     glutIconifyWindow will iconify a top-level window, but GLUT prohibits iconification
        //     of a subwindow. The effect of iconifying windows does not take place immediately.
        //     Instead the requests are saved for execution upon return to the GLUT event
        //     loop. Subsequent iconification requests on a window replace the previously
        //     saved request for that window. The effect of iconifying top-level windows
        //     is subject to the window system's policy for displaying windows.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutIconifyWindow();
        //
        // Summary:
        //     Sets the global idle callback.
        //
        // Parameters:
        //   func:
        //     The new idle callback function. See Tao.FreeGlut.Glut.IdleCallback.
        //
        // Remarks:
        //      glutIdleFunc sets the global idle callback to be func so a GLUT program
        //     can perform background processing tasks or continuous animation when window
        //     system events are not being received. If enabled, the idle callback is continuously
        //     called when events are not being received. The callback routine has no parameters.
        //     The current window and current menu will not be changed before the idle callback.
        //     Programs with multiple windows and/or menus should explicitly set the current
        //     window and/or current menu and not rely on its current setting.
        //     The amount of computation and rendering done in an idle callback should be
        //     minimized to avoid affecting the program's interactive response. In general,
        //     not more than a single frame of rendering should be done in an idle callback.
        //     Passing null to glutIdleFunc disables the generation of the idle callback.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutIdleFunc(FreeGLUT.Delegates.IdleCallback func);
        //
        // Summary:
        //     Determines if auto repeat keystrokes are reported to the current window.
        //
        // Parameters:
        //   ignore:
        //     Non-zero indicates auto repeat keystrokes should not be reported by the keyboard
        //     and special callbacks; zero indicates that auto repeat keystrokes will be
        //     reported.
        //
        // Remarks:
        //      glutIgnoreKeyRepeat determines if auto repeat keystrokes are reported to
        //     the current window. The ignore auto repeat state of a window can be queried
        //     with Glut.glutDeviceGet(Glut.GLUT_DEVICE_IGNORE_KEY_REPEAT).
        //     Ignoring auto repeated keystrokes is generally done in conjunction with using
        //     the Tao.FreeGlut.Glut.glutKeyboardUpFunc(Tao.FreeGlut.Glut.KeyboardUpCallback)
        //     and Tao.FreeGlut.Glut.glutSpecialUpFunc(Tao.FreeGlut.Glut.SpecialUpCallback)
        //     callbacks to repeat key releases. If you do not ignore auto repeated keystrokes,
        //     your GLUT application will experience repeated release/press callbacks. Games
        //     using the keyboard will typically want to ignore key repeat.
        //     X IMPLEMENTATION NOTES
        //     X11 sends KeyPress events repeatedly when the window system's global auto
        //     repeat is enabled. glutIgnoreKeyRepeat can prevent these auto repeated keystrokes
        //     from being reported as keyboard or special callbacks, but there is still
        //     some minimal overhead by the X server to continually stream KeyPress events
        //     to the GLUT application. The Tao.FreeGlut.Glut.glutSetKeyRepeat(System.Int32)
        //     routine can be used to actually disable the global sending of auto repeated
        //     KeyPress events. Note that Tao.FreeGlut.Glut.glutSetKeyRepeat(System.Int32)
        //     affects the global window system auto repeat state so other applications
        //     will not auto repeat if you disable auto repeat globally through Tao.FreeGlut.Glut.glutSetKeyRepeat(System.Int32).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutIgnoreKeyRepeat(int ignore);
        //
        // Summary:
        //     Initializes the GLUT library.
        //
        // Parameters:
        //   argcp:
        //     A pointer to the program’s unmodified argc variable from main. Upon return,
        //     the value pointed to by argcp will be updated, because glutInit extracts
        //     any command line options intended for the GLUT library.
        //
        //   argv:
        //     The program’s unmodified argv variable from main. Like argcp, the data for
        //     argv will be updated because glutInit extracts any command line options understood
        //     by the GLUT library.
        //
        // Remarks:
        //      glutInit will initialize the GLUT library and negotiate a session with the
        //     window system. During this process, glutInit may cause the termination of
        //     the GLUT program with an error message to the user if GLUT cannot be properly
        //     initialized. Examples of this situation include the failure to connect to
        //     the window system, the lack of window system support for OpenGL, and invalid
        //     command line options.
        //     glutInit also processes command line options, but the specific options parsed
        //     are window system dependent.
        //     X IMPLEMENTATION NOTES
        //     The X Window System specific options parsed by glutInit are as follows:
        //     Value Description -display DISPLAY Specify the X server to connect to. If
        //     not specified, the value of the DISPLAY environment variable is used.  -geometry
        //     WxH+X+Y Determines where window's should be created on the screen. The parameter
        //     following -geometry should be formatted as a standard X geometry specification.
        //     The effect of using this option is to change the GLUT initial size and initial
        //     position the same as if Tao.FreeGlut.Glut.glutInitWindowSize(System.Int32,System.Int32)
        //     or Tao.FreeGlut.Glut.glutInitWindowPosition(System.Int32,System.Int32) were
        //     called directly.  -iconic Requests all top-level windows be created in an
        //     iconic state.  -indirect Force the use of indirect OpenGL rendering contexts.
        //      -direct
        //     Force the use of direct OpenGL rendering contexts (not all GLX implementations
        //     support direct rendering contexts). A fatal error is generated if direct
        //     rendering is not supported by the OpenGL implementation.
        //     If neither -indirect or -direct are used to force a particular behavior,
        //     GLUT will attempt to use direct rendering if possible and otherwise fallback
        //     to indirect rendering.
        //     -gldebug After processing callbacks and/or events, check if there are any
        //     OpenGL errors by calling Tao.OpenGl.Gl.glGetError(). If an error is reported,
        //     print out a warning by looking up the error code with /*see cref="Glu.gluErrorString"
        //     />*/. Using this option is helpful in detecting OpenGL run-time errors. 
        //     -sync Enable synchronous X protocol transactions. This option makes it easier
        //     to track down potential X protocol errors.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutInit(ref int argcp, StringBuilder[] argv);
        //
        // Summary:
        //     Sets the initial display mode.
        //
        // Parameters:
        //   mode:
        //      Display mode, normally the bitwise OR-ing of GLUT display mode bit masks.
        //     See values below:
        //     Value Description Tao.FreeGlut.Glut.GLUT_RGBA Bit mask to select an RGBA
        //     mode window. This is the default if neither Tao.FreeGlut.Glut.GLUT_RGBA nor
        //     Tao.FreeGlut.Glut.GLUT_INDEX are specified.  Tao.FreeGlut.Glut.GLUT_RGB An
        //     alias for Tao.FreeGlut.Glut.GLUT_RGBA.  Tao.FreeGlut.Glut.GLUT_INDEX Bit
        //     mask to select a color index mode window. This overrides Tao.FreeGlut.Glut.GLUT_RGBA
        //     if it is also specified.  Tao.FreeGlut.Glut.GLUT_SINGLE Bit mask to select
        //     a single buffered window. This is the default if neither Tao.FreeGlut.Glut.GLUT_DOUBLE
        //     or Tao.FreeGlut.Glut.GLUT_SINGLE are specified.  Tao.FreeGlut.Glut.GLUT_DOUBLE
        //     Bit mask to select a double buffered window. This overrides Tao.FreeGlut.Glut.GLUT_SINGLE
        //     if it is also specified.  Tao.FreeGlut.Glut.GLUT_ACCUM Bit mask to select
        //     a window with an accumulation buffer.  Tao.FreeGlut.Glut.GLUT_ALPHA Bit mask
        //     to select a window with an alpha component to the color buffer(s).  Tao.FreeGlut.Glut.GLUT_DEPTH
        //     Bit mask to select a window with a depth buffer.  Tao.FreeGlut.Glut.GLUT_STENCIL
        //     Bit mask to select a window with a stencil buffer.  Tao.FreeGlut.Glut.GLUT_MULTISAMPLE
        //     Bit mask to select a window with multisampling support. If multisampling
        //     is not available, a non-multisampling window will automatically be chosen.
        //     Note: both the OpenGL client-side and server-side implementations must support
        //     the GLX_SAMPLE_SGIS extension for multisampling to be available.  Tao.FreeGlut.Glut.GLUT_STEREO
        //     Bit mask to select a stereo window.  Tao.FreeGlut.Glut.GLUT_LUMINANCE Bit
        //     mask to select a window with a "luminance" color model. This model provides
        //     the functionality of OpenGL's RGBA color model, but the green and blue components
        //     are not maintained in the frame buffer. Instead each pixel's red component
        //     is converted to an index between zero and Glut.glutGet(Glut.GLUT_WINDOW_COLORMAP_SIZE)
        //     - 1 and looked up in a per-window color map to determine the color of pixels
        //     within the window. The initial colormap of Tao.FreeGlut.Glut.GLUT_LUMINANCE
        //     windows is initialized to be a linear gray ramp, but can be modified with
        //     GLUT's colormap routines.
        //
        // Remarks:
        //      The initial display mode is used when creating top-level windows, subwindows,
        //     and overlays to determine the OpenGL display mode for the to-be-created window
        //     or overlay.
        //     Note that Tao.FreeGlut.Glut.GLUT_RGBA selects the RGBA color model, but it
        //     does not request any bits of alpha (sometimes called an alpha buffer or destination
        //     alpha) be allocated. To request alpha, specify Tao.FreeGlut.Glut.GLUT_ALPHA.
        //      The same applies to Tao.FreeGlut.Glut.GLUT_LUMINANCE.
        //     NOTE
        //     Tao.FreeGlut.Glut.GLUT_LUMINANCE is not supported on most OpenGL platforms.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutInitDisplayMode(int mode);
        //
        // Summary:
        //     Sets the initial display mode via a string.
        //
        // Parameters:
        //   str:
        //     Display mode description string, see below.
        //
        // Remarks:
        //      The initial display mode description string is used when creating top-level
        //     windows, subwindows, and overlays to determine the OpenGL display mode for
        //     the to-be-created window or overlay.
        //     The string is a list of zero or more capability descriptions separated by
        //     spaces and tabs. Each capability description is a capability name that is
        //     optionally followed by a comparator and a numeric value. For example, "double"
        //     and "depth>=12" are both valid criteria.
        //     The capability descriptions are translated into a set of criteria used to
        //     select the appropriate frame buffer configuration.
        //     The criteria are matched in strict left to right order of precdence. That
        //     is, the first specified criteria (leftmost) takes precedence over the later
        //     criteria for non-exact criteria (greater than, less than, etc. comparators).
        //      Exact criteria (equal, not equal compartors) must match exactly so precedence
        //     is not relevant.
        //     The numeric value is an integer that is parsed according to ANSI C's strtol(str,
        //     strptr, 0) behavior. This means that decimal, octal (leading 0), and hexidecimal
        //     values (leading 0x) are accepted.
        //     The valid compartors are:
        //     Value Description = Equal.  != Not equal.  < Less than and preferring larger
        //     difference (the least is best).  > Greeater than and preferring larger differences
        //     (the most is best).  <= Less than or equal and preferring larger difference
        //     (the least is best).  >= Greater than or equal and preferring more instead
        //     of less. This comparator is useful for allocating resources like color precsion
        //     or depth buffer precision where the maximum precison is generally preferred.
        //     Contrast with the tilde (~) comprator.  ~ Greater than or equal but preferring
        //     less instead of more. This compartor is useful for allocating resources such
        //     as stencil bits or auxillary color buffers where you would rather not over
        //     allocate.
        //     When the compartor and numeric value are not specified, each capability name
        //     has a different default (one default is to require a compartor and numeric
        //     value).
        //     The valid capability names are:
        //     Value Description alpha
        //     Alpha color buffer precision in bits.
        //     Default is ">=1".
        //     acca
        //     Red, green, blue, and alpha accumulation buffer precision in bits.
        //     Default is ">=1" for red, green, blue, and alpha capabilities.
        //     acc
        //     Red, green, and green accumulation buffer precision in bits and zero bits
        //     of alpha accumulation buffer precision.
        //     Default is ">=1" for red, green, and blue capabilities, and "~0" for the
        //     alpha capability.
        //     blue
        //     Blue color buffer precision in bits.
        //     Default is ">=1".
        //     buffer
        //     Number of bits in the color index color buffer.
        //     Default is ">=1".
        //     conformant
        //     bool indicating if the frame buffer configuration is conformant or not. Conformance
        //     information is based on GLX's EXT_visual_rating extension if supported. If
        //     the extension is not supported, all visuals are assumed conformat.
        //     Default is "=1".
        //     depth
        //     Number of bits of precsion in the depth buffer.
        //     Default is ">=12".
        //     double
        //     bool indicating if the color buffer is double buffered.
        //     Default is "=1".
        //     green
        //     Green color buffer precision in bits.
        //     Default is ">=1".
        //     index
        //     bool if the color model is color index or not. True is color index.
        //     Default is ">=1".
        //     num A special capability name indicating where the value represents the Nth
        //     frame buffer configuration matching the description string. When not specified,
        //     glutInitDisplayString also returns the first (best matching) configuration.
        //     num requires a compartor and numeric value.  red
        //     Red color buffer precision in bits.
        //     Default is ">=1".
        //     rgba
        //     Number of bits of red, green, blue, and alpha in the RGBA color buffer.
        //     Default is ">=1" for red, green, blue, and alpha capabilities, and "=1" for
        //     the RGBA color model capability.
        //     rgb
        //     Number of bits of red, green, and blue in the RGBA color buffer and zero
        //     bits of alpha color buffer precision.
        //     Default is ">=1" for the red, green, and blue capabilities, and "~0" for
        //     alpha capability, and "=1" for the RGBA color model capability.
        //     luminance
        //     Number of bits of red in the RGBA and zero bits of green, blue (alpha not
        //     specified) of color buffer precision.
        //     Default is ">=1" for the red capabilities, and "=0" for the green and blue
        //     capabilities, and "=1" for the RGBA color model capability, and, for X11,
        //     "=1" for the StaticGray ("xstaticgray") capability.
        //     SGI InfiniteReality (and other future machines) support a 16-bit luminance
        //     (single channel) display mode (an additional 16-bit alpha channel can also
        //     be requested). The red channel maps to gray scale and green and blue channels
        //     are not available. A 16-bit precision luminance display mode is often appropriate
        //     for medical imaging applications. Do not expect many machines to support
        //     extended precision luminance display modes.
        //     stencil Number of bits in the stencil buffer.  single
        //     bool indicate the color buffer is single buffered.
        //     double buffer capability "=1".
        //     stereo
        //     bool indicating the color buffer is supports OpenGL-style stereo.
        //     Default is "=1".
        //     samples
        //     Indicates the number of multisamples to use based on GLX's SGIS_multisample
        //     extension (for antialiasing).
        //     Default is "<=4". This default means that a GLUT application can request
        //     multipsampling if available by simply specifying "samples".
        //     slow
        //     bool indicating if the frame buffer configuration is slow or not. Slowness
        //     information is based on GLX's EXT_visual_rating extension if supported. If
        //     the extension is not supported, all visuals are assumed fast. Note that slowness
        //     is a relative designation relative to other frame buffer configurations available.
        //     The intent of the slow capability is to help programs avoid frame buffer
        //     configurations that are slower (but perhaps higher precision) for the current
        //     machine.
        //     Default is ">=0". This default means that slow visuals are used in preference
        //     to fast visuals, but fast visuals will still be allowed.
        //     win32pfd Only recognized on GLUT implementations for Win32, this capability
        //     name matches the Win32 Pixel Format Descriptor by number. win32pfd requires
        //     a compartor and numeric value.  xvisual Only recognized on GLUT implementations
        //     for the X Window System, this capability name matches the X visual ID by
        //     number.  xvisual requires a compartor and numeric value.  xstaticgray
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type StaticGray.
        //     Default is "=1".
        //     xgrayscale
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type GrayScale.
        //     Default is "=1".
        //     xstaticcolor
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type StaticColor.
        //     Default is "=1".
        //     xpseudocolor
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type PsuedoColor.
        //     Default is "=1".
        //     xtruecolor
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type TrueColor.
        //     Default is "=1".
        //     xdirectcolor
        //     Only recognized on GLUT implementations for the X Window System, boolean
        //     indicating if the frame buffer configuration's X visual is of type DirectColor.
        //     Default is "=1".
        //     Unspecifed capability descriptions will result in unspecified criteria being
        //     generated. These unspecified criteria help glutInitDisplayString behave sensibly
        //     with terse display mode description strings.
        //     EXAMPLE
        //     Here is an example using glutInitDisplayString:
        //     Glut.glutInitDisplayString("stencil~2 rgb double depth>=16 samples");
        //     The above call requests a window with an RGBA color model (but requesting
        //     no bits of alpha), a depth buffer with at least 16 bits of precsion but preferring
        //     more, mutlisampling if available, and at least 2 bits of stencil (favoring
        //     less stencil to more as long as 2 bits are available).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutInitDisplayString(string str);
        //
        // Summary:
        //     Sets the initial window position.
        //
        // Parameters:
        //   x:
        //     Window X location in pixels.
        //
        //   y:
        //     Window Y location in pixels.
        //
        // Remarks:
        //      Windows created by Tao.FreeGlut.Glut.glutCreateWindow(System.String) will
        //     be requested to be created with the current initial window position. The
        //     initial value of the initial window position GLUT state is -1 and -1. If
        //     either the X or Y component to the initial window position is negative, the
        //     actual window position is left to the window system to determine.
        //     The intent of the initial window position values is to provide a suggestion
        //     to the window system for a window’s initial position. The window system is
        //     not obligated to use this information. Therefore, GLUT programs should not
        //     assume the window was created at the specified position.
        //     Example
        //     If you would like your GLUT program to default to starting at a given screen
        //     location and at a given size, but you would also like to let the user override
        //     these default via a command line argument (such as -geometry for X11), call
        //     Tao.FreeGlut.Glut.glutInitWindowSize(System.Int32,System.Int32) and Tao.FreeGlut.Glut.glutInitWindowPosition(System.Int32,System.Int32)
        //     before your call to Tao.FreeGlut.Glut.glutInit(). For example:
        //     using Tao.OpenGL; [STAThread] public static void Main(string[] args) { Glut.glutInitWindowSize(500,
        //     300); Glut.glutInitWindowPosition(100, 100); Glut.glutInit(); }
        //     However, if you'd like to force your program to start up at a given size
        //     and position, call Tao.FreeGlut.Glut.glutInitWindowSize(System.Int32,System.Int32)
        //     and Tao.FreeGlut.Glut.glutInitWindowPosition(System.Int32,System.Int32) after
        //     your call to Tao.FreeGlut.Glut.glutInit(). For example:
        //     using Tao.OpenGL; [STAThread] public static void Main(string[] args) { Glut.glutInit();
        //     Glut.glutInitWindowSize(500, 300); Glut.glutInitWindowPosition(100, 100);
        //     }
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutInitWindowPosition(int x, int y);
        //
        // Summary:
        //     Sets the initial window size.
        //
        // Parameters:
        //   width:
        //     Width in pixels.
        //
        //   height:
        //     Height in pixels.
        //
        // Remarks:
        //      Windows created by Tao.FreeGlut.Glut.glutCreateWindow(System.String) will
        //     be requested to be created with the current initial window size. The initial
        //     value of the initial window size GLUT state is 300 by 300. The initial window
        //     size components must be greater than zero.
        //     The intent of the initial window size values is to provide a suggestion to
        //     the window system for a window’s initial size. The window system is not obligated
        //     to use this information. Therefore, GLUT programs should not assume the window
        //     was created at the specified size. A GLUT program should use the window’s
        //     reshape callback to determine the true size of the window.
        //     Example
        //     If you would like your GLUT program to default to starting at a given screen
        //     location and at a given size, but you would also like to let the user override
        //     these default via a command line argument (such as -geometry for X11), call
        //     Tao.FreeGlut.Glut.glutInitWindowSize(System.Int32,System.Int32) and Tao.FreeGlut.Glut.glutInitWindowPosition(System.Int32,System.Int32)
        //     before your call to Tao.FreeGlut.Glut.glutInit(). For example:
        //     using Tao.OpenGL; [STAThread] public static void Main(string[] args) { Glut.glutInitWindowSize(500,
        //     300); Glut.glutInitWindowPosition(100, 100); Glut.glutInit(); }
        //     However, if you'd like to force your program to start up at a given size
        //     and position, call Tao.FreeGlut.Glut.glutInitWindowSize(System.Int32,System.Int32)
        //     and Tao.FreeGlut.Glut.glutInitWindowPosition(System.Int32,System.Int32) after
        //     your call to Tao.FreeGlut.Glut.glutInit(). For example:
        //     using Tao.OpenGL; [STAThread] public static void Main(string[] args) { Glut.glutInit();
        //     Glut.glutInitWindowSize(500, 300); Glut.glutInitWindowPosition(100, 100);
        //     }
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutInitWindowSize(int width, int height);
        //
        // Summary:
        //     Sets the joystick callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new joystick callback function. See Tao.FreeGlut.Glut.JoystickCallback.
        //
        //   pollInterval:
        //     Joystick polling interval in milliseconds.
        //
        // Remarks:
        //      glutJoystickFunc sets the joystick callback for the current window.
        //     The joystick callback is called either due to polling of the joystick at
        //     the uniform timer interval specified by pollInterval (in milliseconds) or
        //     in response to calling Tao.FreeGlut.Glut.glutForceJoystickFunc(). If the
        //     pollInterval is non-positive, no joystick polling is performed and the GLUT
        //     application must frequently (usually from an idle callback) call glutForceJoystickFunc.
        //     The joystick buttons are reported by the callback's buttonMask parameter.
        //     The constants Tao.FreeGlut.Glut.GLUT_JOYSTICK_BUTTON_A (0x1), Tao.FreeGlut.Glut.GLUT_JOYSTICK_BUTTON_B
        //     (0x2), Tao.FreeGlut.Glut.GLUT_JOYSTICK_BUTTON_C (0x4), and Tao.FreeGlut.Glut.GLUT_JOYSTICK_BUTTON_D
        //     (0x8) are provided for programming convience.
        //     The x, y, and z callback parameters report the X, Y, and Z axes of the joystick.
        //     The joystick is centered at (0, 0, 0). X, Y, and Z are scaled to range between
        //     -1000 and 1000. Moving the joystick left reports negative X; right reports
        //     positive X. Pulling the stick towards you reports negative Y; push the stick
        //     away from you reports positive Y. If the joystick has a third axis (rudder
        //     or up/down), down reports negative Z; up reports positive Z.
        //     Passing a null to glutJoystickFunc disables the generation of joystick callbacks.
        //     Without a joystick callback registered, glutForceJoystickFunc does nothing.
        //     When a new window is created, no joystick callback is initially registered.
        //     LIMITATIONS
        //     The GLUT joystick callback only reports the first 3 axes and 32 buttons.
        //      GLUT supports only a single joystick.
        //     X IMPLEMENTATION NOTES
        //     The GLUT 3.7 implementation of GLUT for X11 supports the joystick API, but
        //     not joystick input. A future implementation of GLUT for X11 may add joystick
        //     support.
        //     WIN32 IMPLEMENTATION NOTES
        //     The GLUT 3.7 implementation of GLUT for Win32 supports the joystick API and
        //     joystick input, but does so through the dated joySetCapture and joyGetPosEx
        //     Win32 Multimedia API. The GLUT 3.7 joystick support for Win32 has all the
        //     limitations of the Win32 Multimedia API joystick support.  A future implementation
        //     of GLUT for Win32 may use DirectInput.
        //     GLUT IMPLEMENTATION NOTES FOR NON-ANALOG JOYSTICKS
        //     If the connected joystick does not return (x, y, z) as a continuous range
        //     (for example, an 8 position Atari 2600 joystick), the implementation should
        //     report the most extreme (x, y, z) location. That is, if a 2D joystick is
        //     pushed to the upper left, report (-1000, 1000, 0).
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutJoystickFunc(FreeGLUT.Delegates.JoystickCallback func, int pollInterval);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutKeyboardFunc(FreeGLUT.Delegates.KeyboardCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutKeyboardUpFunc(FreeGLUT.Delegates.KeyboardUpCallback func);
        //
        // Summary:
        //     Retrieves GLUT state pertaining to the layers of the current window.
        //
        // Parameters:
        //   info:
        //      Name of device information to retrieve. Valid values are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_OVERLAY_POSSIBLE Whether an overlay
        //     could be established for the current window given the current initial display
        //     mode. If false, Tao.FreeGlut.Glut.glutEstablishOverlay() will fail with a
        //     fatal error if called.  Tao.FreeGlut.Glut.GLUT_LAYER_IN_USE Either Tao.FreeGlut.Glut.GLUT_NORMAL
        //     or Tao.FreeGlut.Glut.GLUT_OVERLAY depending on whether the normal plane or
        //     overlay is the layer in use.  Tao.FreeGlut.Glut.GLUT_HAS_OVERLAY If the current
        //     window has an overlay established.  Tao.FreeGlut.Glut.GLUT_TRANSPARENT_INDEX
        //     The transparent color index of the overlay of the current window; negative
        //     one is returned if no overlay is in use.  Tao.FreeGlut.Glut.GLUT_NORMAL_DAMAGED
        //     True if the normal plane of the current window has damaged (by window system
        //     activity) since the last display callback was triggered. Calling Tao.FreeGlut.Glut.glutPostRedisplay()
        //     will not set this true.  Tao.FreeGlut.Glut.GLUT_OVERLAY_DAMAGED True if the
        //     overlay plane of the current window has damaged (by window system activity)
        //     since the last display callback was triggered. Calling Tao.FreeGlut.Glut.glutPostRedisplay()
        //     or Tao.FreeGlut.Glut.glutPostOverlayRedisplay() will not set this true. 
        //     Negative one is returned if no overlay is in use.
        //
        // Returns:
        //     Retrieves GLUT layer information for the current window represented by integers.
        //     The info parameter determines what type of layer information to return.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutLayerGet(int info);
        //
        // Summary:
        //     Leaves GLUT's game mode.
        //
        // Remarks:
        //      glutLeaveGameMode leaves the GLUT game mode and returns the screen display
        //     format to its default format.
        //     After leaving game mode, the GLUT functionality disabled in game mode is
        //     available again. The game mode window (and its OpenGL rendering state) is
        //     destroyed when leaving game mode. Any windows and subwindows created before
        //     entering the game mode are displayed in their previous locations. The OpenGL
        //     state of normal GLUT windows and subwindows is not disturbed by entering
        //     and/or leaving game mode.
        [DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutLeaveGameMode();
        [DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutLeaveMainLoop();
        [DllImport(FREEGLUT_LIBRARY, CallingConvention=CALLING_CONVENTION), SuppressUnmanagedCodeSecurity()]
        public extern static void glutMainLoop();
        //
        // Summary:
        //     Performs the main loop event and returns control.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutMainLoopEvent();
        //
        // Summary:
        //     Sets the menu destroy callback.
        //
        // Parameters:
        //   func:
        //     The new menu destroy callback function. See Tao.FreeGlut.Glut.MenuDestroyCallback.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMenuDestroyFunc(FreeGLUT.Delegates.MenuDestroyCallback func);
        //
        // Summary:
        //     A deprecated version of the Tao.FreeGlut.Glut.glutMenuStatusFunc(Tao.FreeGlut.Glut.MenuStatusCallback)
        //     routine.
        //
        // Parameters:
        //   func:
        //     The new menu state callback function. Tao.FreeGlut.Glut.MenuStateCallback.
        //
        // Remarks:
        //     The only difference is glutMenuStateFunc callback prototype does not deliver
        //     the two additional x and y coordinates.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMenuStateFunc(FreeGLUT.Delegates.MenuStateCallback func);
        //
        // Summary:
        //     Sets the global menu status callback.
        //
        // Parameters:
        //   func:
        //     The new menu status button callback function. See Tao.FreeGlut.Glut.MenuStatusCallback.
        //
        // Remarks:
        //      glutMenuStatusFunc sets the global menu status callback so a GLUT program
        //     can determine when a menu is in use or not. When a menu status callback is
        //     registered, it will be called with the value Tao.FreeGlut.Glut.GLUT_MENU_IN_USE
        //     for its val parameter when pop-up menus are in use by the user; and the callback
        //     will be called with the value Tao.FreeGlut.Glut.GLUT_MENU_NOT_IN_USE for
        //     its status parameter when pop-up menus are no longer in use. The x and y
        //     parameters indicate the location in window coordinates of the button press
        //     that caused the menu to go into use, or the location where the menu was released
        //     (may be outside the window). The func parameter names the callback function.
        //      Other callbacks continue to operate (except mouse motion callbacks) when
        //     pop-up menus are in use so the menu status callback allows a program to suspend
        //     animation or other tasks when menus are in use. The cascading and unmapping
        //     of sub-menus from an initial pop-up menu does not generate menu status callbacks.
        //     There is a single menu status callback for GLUT.
        //     When the menu status callback is called, the current menu will be set to
        //     the initial pop-up menu in both the GLUT_MENU_IN_USE and GLUT_MENU_NOT_IN_USE
        //     cases. The current window will be set to the window from which the initial
        //     menu was popped up from, also in both cases.
        //     Passing null to glutMenuStatusFunc disables the generation of the menu status
        //     callback.
        //     Tao.FreeGlut.Glut.glutMenuStateFunc(Tao.FreeGlut.Glut.MenuStateCallback)
        //     is a deprecated version of the glutMenuStatusFunc routine. The only difference
        //     is glutMenuStateFunc callback prototype does not deliver the two additional
        //     x and y coordinates.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMenuStatusFunc(FreeGLUT.Delegates.MenuStatusCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMotionFunc(FreeGLUT.Delegates.MotionCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMouseFunc(FreeGLUT.Delegates.MouseCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutMouseWheelFunc(FreeGLUT.Delegates.MouseWheelCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutOverlayDisplayFunc(FreeGLUT.Delegates.OverlayDisplayCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutPassiveMotionFunc(FreeGLUT.Delegates.PassiveMotionCallback func);
        //
        // Summary:
        //     Changes the stacking order of the current window relative to its siblings.
        //
        // Remarks:
        //     glutPopWindow works on both top-level windows and subwindows. The effect
        //     of popping windows does not take place immediately. Instead the pop is saved
        //     for execution upon return to the GLUT event loop. Subsequent pop requests
        //     on a window replace the previously saved request for that window.  The effect
        //     of popping top-level windows is subject to the window system's policy for
        //     restacking windows.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPopWindow();
        //
        // Summary:
        //     Requests a change to the position of the current window.
        //
        // Parameters:
        //   x:
        //     New X location of window in pixels.
        //
        //   y:
        //     New Y location of window in pixels.
        //
        // Remarks:
        //      glutPositionWindow requests a change in the position of the current window.
        //     For top-level windows, the x and y parameters are pixel offsets from the
        //     screen origin. For subwindows, the x and y parameters are pixel offsets from
        //     the window's parent window origin.
        //     The requests by glutPositionWindow are not processed immediately. The request
        //     is executed after returning to the main event loop. This allows multiple
        //     glutPositionWindow, Tao.FreeGlut.Glut.glutReshapeWindow(System.Int32,System.Int32),
        //     and Tao.FreeGlut.Glut.glutFullScreen() requests to the same window to be
        //     coalesced.
        //     In the case of top-level windows, a glutPositionWindow call is considered
        //     only a request for positioning the window. The window system is free to apply
        //     its own policies to top-level window placement. The intent is that top-level
        //     windows should be repositioned according to glutPositionWindow's parameters.
        //     glutPositionWindow disables the full screen status of a window if previously
        //     enabled.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPositionWindow(int x, int y);
        //
        // Summary:
        //     Marks the overlay of the current window as needing to be redisplayed.
        //
        // Remarks:
        //      glutPostOverlayRedisplay marks the overlay of current window as needing
        //     to be redisplayed. The next iteration through Tao.FreeGlut.Glut.glutMainLoop(),
        //     the window's overlay display callback (or simply the display callback if
        //     no overlay display callback is registered) will be called to redisplay the
        //     window's overlay plane. Multiple calls to glutPostOverlayRedisplay before
        //     the next display callback opportunity (or overlay display callback opportunity
        //     if one is registered) generate only a single redisplay.  glutPostOverlayRedisplay
        //     may be called within a window's display or overlay display callback to re-mark
        //     that window for redisplay.
        //     Logically, overlay damage notification for a window is treated as a glutPostOverlayRedisplay
        //     on the damaged window. Unlike damage reported by the window system, glutPostOverlayRedisplay
        //     will not set to true the overlay's damaged status (returned by Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAGED).
        //     If the window you want to post an overlay redisplay on is not already current
        //     (and you do not require it to be immediately made current), using Tao.FreeGlut.Glut.glutPostWindowOverlayRedisplay(System.Int32)
        //     is more efficient than calling Tao.FreeGlut.Glut.glutSetWindow(System.Int32)
        //     to the desired window and then calling glutPostOverlayRedisplay.
        //     EXAMPLE
        //     If you are doing an interactive effect like rubberbanding in the overlay,
        //     it is a good idea to structure your rendering to minimize flicker (most overlays
        //     are single-buffered). Only clear the overlay if you know that the window
        //     has been damaged. Otherwise, try to simply erase what you last drew and redraw
        //     it in an updated position. Here is an example overlay display callback used
        //     to implement overlay rubberbanding:
        //     private void redrawOverlay() { static int prevStretchX, prevStretchY; if(Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAGED))
        //     { // Damage means we need a full clear.  Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
        //     } else { // Undraw last rubber-band.  Gl.glBegin(Gl.GL_LINE_LOOP); Gl.glVertex2i(anchorX,
        //     anchorY); Gl.glVertex2i(anchorX, prevStretchY); Gl.glVertex2i(prevStretchX,
        //     anchorY); Gl.glEnd(); } Gl.glIndexi(red); Gl.glBegin(Gl.GL_LINE_LOOP); Gl.glVertex2i(anchorX,
        //     anchorY); Gl.glVertex2i(anchorX, stretchY); Gl.glVertex2i(stretchX, stretchY);
        //     Gl.glVertex2i(stretchX, anchorY); Gl.glEnd(); prevStretchX = stretchX; prevStretchY
        //     = stretchY; }
        //     Notice how Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAGED) is used to determine
        //     if a clear needs to take place because of damage; if a clear is unnecessary,
        //     it is faster to just draw the last rubberband using the transparent pixel.
        //     When the application is through with the rubberbanding effect, the best way
        //     to get rid of the rubberband is to simply hide the overlay by calling Tao.FreeGlut.Glut.glutHideOverlay().
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPostOverlayRedisplay();
        //
        // Summary:
        //     Marks the current window as needing to be redisplayed.
        //
        // Remarks:
        //      glutPostRedisplay marks the normal plane of current window as needing to
        //     be redisplayed. The next iteration through Tao.FreeGlut.Glut.glutMainLoop(),
        //     the window's display callback will be called to redisplay the window's normal
        //     plane. Multiple calls to glutPostRedisplay before the next display callback
        //     opportunity generates only a single redisplay callback.  glutPostRedisplay
        //     may be called within a window's display or overlay display callback to re-mark
        //     that window for redisplay.
        //     Logically, normal plane damage notification for a window is treated as a
        //     glutPostRedisplay on the damaged window. Unlike damage reported by the window
        //     system, glutPostRedisplay will not set to true the normal plane's damaged
        //     status (returned by Glut.glutLayerGet(Glut.GLUT_NORMAL_DAMAGED).
        //     If the window you want to post a redisplay on is not already current (and
        //     you do not require it to be immediately made current), using Tao.FreeGlut.Glut.glutPostWindowRedisplay(System.Int32)
        //     is more efficient than calling Tao.FreeGlut.Glut.glutSetWindow(System.Int32)
        //     to the desired window and then calling glutPostRedisplay.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPostRedisplay();
        //
        // Summary:
        //     Marks the overlay of the specified window as needing to be redisplayed.
        //
        // Parameters:
        //   win:
        //     Identifier of GLUT window for which to post the overlay redisplay.
        //
        // Remarks:
        //      glutPostWindowOverlayRedisplay marks the overlay of specified window as
        //     needing to be redisplayed. The next iteration through Tao.FreeGlut.Glut.glutMainLoop(),
        //     the window's overlay display callback (or simply the display callback if
        //     no overlay display callback is registered) will be called to redisplay the
        //     window's overlay plane. Multiple calls to glutPostWindowOverlayRedisplay
        //     before the next display callback opportunity (or overlay display callback
        //     opportunity if one is registered) generate only a single redisplay. glutPostWindowOverlayRedisplay
        //     may be called within a window's display or overlay display callback to re-mark
        //     that window for redisplay.
        //     Logically, overlay damage notification for a window is treated as a glutPostWindowOverlayRedisplay
        //     on the damaged window. Unlike damage reported by the window system, glutPostWindowOverlayRedisplay
        //     will not set to true the overlay's damaged status (returned by Glut.glutLayerGet(Glut.GLUT_OVERLAY_DAMAGED).
        //     If the window you want to post an overlay redisplay on is not already current
        //     (and you do not require it to be immediately made current), using glutPostWindowOverlayRedisplay
        //     is more efficient than calling Tao.FreeGlut.Glut.glutSetWindow(System.Int32)
        //     to the desired window and then calling Tao.FreeGlut.Glut.glutPostOverlayRedisplay().
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPostWindowOverlayRedisplay(int win);
        //
        // Summary:
        //     Marks the specified window as needing to be redisplayed.
        //
        // Parameters:
        //   win:
        //     Identifier of GLUT window to mark for redisplay.
        //
        // Remarks:
        //      glutPostWindowRedisplay marks the specified window as needing to be redisplayed.
        //     The next iteration through Tao.FreeGlut.Glut.glutMainLoop(), the window's
        //     display callback will be called to redisplay the window's normal plane.
        //     If the window you want to post a redisplay on is not already current (and
        //     you do not require it to be immediately made current), using glutPostWindowRedisplay
        //     is more efficient than calling Tao.FreeGlut.Glut.glutSetWindow(System.Int32)
        //     to the desired window and then calling Tao.FreeGlut.Glut.glutPostRedisplay().
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPostWindowRedisplay(int win);
        //
        // Summary:
        //     Changes the stacking order of the current window relative to its siblings.
        //
        // Remarks:
        //     glutPushWindow works on both top-level windows and subwindows. The effect
        //     of pushing windows does not take place immediately. Instead the push is saved
        //     for execution upon return to the GLUT event loop. Subsequent push requests
        //     on a window replace the previously saved request for that window.  The effect
        //     of pushing top-level windows is subject to the window system's policy for
        //     restacking windows.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutPushWindow();
        //
        // Summary:
        //     Removes the specified menu item.
        //
        // Parameters:
        //   entry:
        //     Index into the menu items of the current menu (1 is the topmost menu item).
        //
        // Remarks:
        //     glutRemoveMenuItem remove the entry menu item regardless of whether it is
        //     a menu entry or sub-menu trigger. entry must be between 1 and Glut.glutGet(Glut.GLUT_MENU_NUM_ITEMS)
        //     inclusive. Menu items below the removed menu item are renumbered.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutRemoveMenuItem(int entry);
        //
        // Summary:
        //     Removes the overlay (if one exists) from the current window.
        //
        // Remarks:
        //      glutRemoveOverlay removes the overlay (if one exists). It is safe to call
        //     glutRemoveOverlay even if no overlay is currently established -- it does
        //     nothing in this case. Implicitly, the window's layer in use changes to the
        //     normal plane immediately once the overlay is removed.
        //     If the program intends to re-establish the overlay later, it is typically
        //     faster and less resource intensive to use Tao.FreeGlut.Glut.glutHideOverlay()
        //     and Tao.FreeGlut.Glut.glutShowOverlay() to simply change the display status
        //     of the overlay.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutRemoveOverlay();
        //
        // Summary:
        //     Prints out OpenGL run-time errors.
        //
        // Remarks:
        //      glutReportErrors prints out any OpenGL run-time errors pending and clears
        //     the errors. This routine typically should only be used for debugging purposes
        //     since calling it will slow OpenGL programs. It is provided as a convenience;
        //     all the routine does is call Tao.OpenGl.Gl.glGetError() until no more errors
        //     are reported. Any errors detected are reported with a GLUT warning and the
        //     corresponding text message generated by /*see cref="Glu.gluErrorString" />*/.
        //     Calling glutReportErrors repeatedly in your program can help isolate OpenGL
        //     errors to the offending OpenGL command. Remember that you can use the -gldebug
        //     option to detect OpenGL errors in any GLUT program.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutReportErrors();
        //
        // Summary:
        //     Sets the reshape callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new reshape callback function. See Tao.FreeGlut.Glut.ReshapeCallback.
        //
        // Remarks:
        //      glutReshapeFunc sets the reshape callback for the current window. The reshape
        //     callback is triggered when a window is reshaped. A reshape callback is also
        //     triggered immediately before a window's first display callback after a window
        //     is created or whenever an overlay for the window is established. The width
        //     and height parameters of the callback specify the new window size in pixels.
        //     Before the callback, the current window is set to the window that has been
        //     reshaped.
        //     If a reshape callback is not registered for a window or null is passed to
        //     glutReshapeFunc (to deregister a previously registered callback), the default
        //     reshape callback is used. This default callback will simply call Gl.glViewport(0,
        //     0, width, height) on the normal plane (and on the overlay if one exists).
        //     If an overlay is established for the window, a single reshape callback is
        //     generated. It is the callback's responsibility to update both the normal
        //     plane and overlay for the window (changing the layer in use as necessary).
        //     When a top-level window is reshaped, subwindows are not reshaped. It is up
        //     to the GLUT program to manage the size and positions of subwindows within
        //     a top-level window. Still, reshape callbacks will be triggered for subwindows
        //     when their size is changed using Tao.FreeGlut.Glut.glutReshapeWindow(System.Int32,System.Int32).
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutReshapeFunc(FreeGLUT.Delegates.ReshapeCallback func);
        //
        // Summary:
        //     Requests a change to the size of the current window.
        //
        // Parameters:
        //   width:
        //     New width of window in pixels.
        //
        //   height:
        //     New height of window in pixels.
        //
        // Remarks:
        //      glutReshapeWindow requests a change in the size of the current window. 
        //     The width and height parameters are size extents in pixels. The width and
        //     height must be positive values.
        //     The requests by glutReshapeWindow are not processed immediately. The request
        //     is executed after returning to the main event loop. This allows multiple
        //     glutReshapeWindow, Tao.FreeGlut.Glut.glutPositionWindow(System.Int32,System.Int32),
        //     and Tao.FreeGlut.Glut.glutFullScreen() requests to the same window to be
        //     coalesced.
        //     In the case of top-level windows, a glutReshapeWindow call is considered
        //     only a request for sizing the window. The window system is free to apply
        //     its own policies to top-level window sizing. The intent is that top-level
        //     windows should be reshaped according to glutReshapeWindow's parameters. Whether
        //     a reshape actually takes effect and, if so, the reshaped dimensions are reported
        //     to the program by a reshape callback.
        //     glutReshapeWindow disables the full screen status of a window if previously
        //     enabled.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutReshapeWindow(int width, int height);
        //
        // Summary:
        //     Sets the color of a colormap entry in the layer of use for the current window.
        //
        // Parameters:
        //   cell:
        //     Color cell index (starting at zero).
        //
        //   red:
        //     Red intensity (clamped between 0.0 and 1.0 inclusive).
        //
        //   green:
        //     Green intensity (clamped between 0.0 and 1.0 inclusive).
        //
        //   blue:
        //     Blue intensity (clamped between 0.0 and 1.0 inclusive).
        //
        // Remarks:
        //     Sets the cell color index colormap entry of the current window's logical
        //     colormap for the layer in use with the color specified by red, green, and
        //     blue. The layer in use of the current window should be a color index window.
        //     cell should be zero or greater and less than the total number of colormap
        //     entries for the window. If the layer in use's colormap was copied by reference,
        //     a glutSetColor call will force the duplication of the colormap. Do not attempt
        //     to set the color of an overlay's transparent index.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetColor(int cell, float red, float green, float blue);
        //
        // Summary:
        //     Changes the cursor image of the current window.
        //
        // Parameters:
        //   cursor:
        //      Name of cursor image to change to. Possible values follow:
        //     Value Description Tao.FreeGlut.Glut.GLUT_CURSOR_RIGHT_ARROW Arrow pointing
        //     up and to the right.  Tao.FreeGlut.Glut.GLUT_CURSOR_LEFT_ARROW Arrow pointing
        //     up and to the left.  Tao.FreeGlut.Glut.GLUT_CURSOR_INFO Pointing hand.  Tao.FreeGlut.Glut.GLUT_CURSOR_DESTROY
        //     Skull and cross bones.  Tao.FreeGlut.Glut.GLUT_CURSOR_HELP Question mark.
        //      Tao.FreeGlut.Glut.GLUT_CURSOR_CYCLE Arrows rotating in a circle.  Tao.FreeGlut.Glut.GLUT_CURSOR_SPRAY
        //     Spray can.  Tao.FreeGlut.Glut.GLUT_CURSOR_WAIT Wrist watch.  Tao.FreeGlut.Glut.GLUT_CURSOR_TEXT
        //     Insertion point cursor for text.  Tao.FreeGlut.Glut.GLUT_CURSOR_CROSSHAIR
        //     Simple cross-hair.  Tao.FreeGlut.Glut.GLUT_CURSOR_UP_DOWN Bi-directional
        //     pointing up and down.  Tao.FreeGlut.Glut.GLUT_CURSOR_LEFT_RIGHT Bi-directional
        //     pointing left and right.  Tao.FreeGlut.Glut.GLUT_CURSOR_TOP_SIDE Arrow pointing
        //     to top side.  Tao.FreeGlut.Glut.GLUT_CURSOR_BOTTOM_SIDE Arrow pointing to
        //     bottom side.  Tao.FreeGlut.Glut.GLUT_CURSOR_LEFT_SIDE Arrow pointing to left
        //     side.  Tao.FreeGlut.Glut.GLUT_CURSOR_RIGHT_SIDE Arrow pointing to right side.
        //      Tao.FreeGlut.Glut.GLUT_CURSOR_TOP_LEFT_CORNER Arrow pointing to top-left
        //     corner.  Tao.FreeGlut.Glut.GLUT_CURSOR_TOP_RIGHT_CORNER Arrow pointing to
        //     top-right corner.  Tao.FreeGlut.Glut.GLUT_CURSOR_BOTTOM_RIGHT_CORNER Arrow
        //     pointing to bottom-right corner.  Tao.FreeGlut.Glut.GLUT_CURSOR_BOTTOM_LEFT_CORNER
        //     Arrow pointing to bottom-left corner.  Tao.FreeGlut.Glut.GLUT_CURSOR_FULL_CROSSHAIR
        //     Full-screen cross-hair cursor (if possible, otherwise Tao.FreeGlut.Glut.GLUT_CURSOR_CROSSHAIR).
        //      Tao.FreeGlut.Glut.GLUT_CURSOR_NONE Invisible cursor.  Tao.FreeGlut.Glut.GLUT_CURSOR_INHERIT
        //     Use parent's cursor.
        //
        // Remarks:
        //      glutSetCursor changes the cursor image of the current window. Each call
        //     requests the window system change the cursor appropriately. The cursor image
        //     when a window is created is Tao.FreeGlut.Glut.GLUT_CURSOR_INHERIT. The exact
        //     cursor images used are implementation dependent. The intent is for the image
        //     to convey the meaning of the cursor name. For a top-level window, GLUT_CURSOR_INHERIT
        //     uses the default window system cursor.
        //     X IMPLEMENTATION NOTES
        //     GLUT for X uses SGI's _SGI_CROSSHAIR_CURSOR convention to access a full-screen
        //     cross-hair cursor if possible.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetCursor(int cursor);
        //
        // Summary:
        //     Changes the icon title of the current top-level window.
        //
        // Parameters:
        //   name:
        //     Character string for the icon name to be set for the window.
        //
        // Remarks:
        //     glutSetIconTitle should be called only when the current window is a top-level
        //     window. Upon creation of a top-level window, the icon name is determined
        //     by the name parameter to Tao.FreeGlut.Glut.glutCreateWindow(System.String).
        //      Once created, glutSetIconTitle can change the icon name of top-level windows.
        //     Each call requests the window system change the name appropriately.  Requests
        //     are not buffered or coalesced. The policy by which the icon name are displayed
        //     is window system dependent.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetIconTitle(string name);
        //
        // Summary:
        //     Sets the key repeat mode for the window system.
        //
        // Parameters:
        //   repeatMode:
        //      Mode for setting key repeat to. Available modes are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_OFF Disable key repeat
        //     for the window system on a global basis if possible.  Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_ON
        //     Enable key repeat for the window system on a global basis if possible.  Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_DEFAULT
        //     Reset the key repeat mode for the window system to its default state if possible.
        //
        // Remarks:
        //      glutSetKeyRepeat sets the key repeat mode for the window system on a global
        //     basis if possible. If supported by the window system, the key repeat can
        //     either be enabled, disabled, or set to the window system's default key repeat
        //     state.
        //     X IMPLEMENTATION NOTES
        //     X11 sends KeyPress events repeatedly when the window system's global auto
        //     repeat is enabled. Tao.FreeGlut.Glut.glutIgnoreKeyRepeat(System.Int32) can
        //     prevent these auto repeated keystrokes from being reported as keyboard or
        //     special callbacks, but there is still some minimal overhead by the X server
        //     to continually stream KeyPress events to the GLUT application. The glutSetKeyRepeat
        //     routine can be used to actually disable the global sending of auto repeated
        //     KeyPress events. Note that glutSetKeyRepeat affects the global window system
        //     auto repeat state so other applications will not auto repeat if you disable
        //     auto repeat globally through glutSetKeyRepeat.
        //     GLUT applications using the X11 GLUT implemenation should disable key repeat
        //     with glutSetKeyRepeat to disable key repeats most efficiently.
        //     WIN32 IMPLEMENTATION NOTES
        //     The Win32 implementation of glutSetKeyRepeat does nothing. The Tao.FreeGlut.Glut.glutIgnoreKeyRepeat(System.Int32)
        //     routine can be used in the Win32 GLUT implementation to ignore repeated keys
        //     on a per-window basis without changing the global window system key repeat.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetKeyRepeat(int repeatMode);
        //
        // Summary:
        //     Sets the current menu.
        //
        // Parameters:
        //   menu:
        //     The identifier of the menu to make the current menu.
        //
        // Remarks:
        //     glutSetMenu sets the current menu.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetMenu(int menu);
        //
        // Summary:
        //     Stores user data in a menu.
        //
        // Parameters:
        //   data:
        //     An arbitrary client System.IntPtr.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetMenuData(IntPtr data);
        //
        // Summary:
        //     Sets simple GLUT state represented by integers.
        //
        // Parameters:
        //   optionFlag:
        //     The option to set.
        //
        //   value:
        //     The value to set for the option.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetOption(int optionFlag, int value);
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Remarks:
        //     Unknown. Unable to locate definitive documentation on this method.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetupVideoResizing();
        //
        // Summary:
        //     Set the user data for the current window.
        //
        // Parameters:
        //   data:
        //     Arbitrary client-supplied System.IntPtr.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetupWindowData(IntPtr data);
        //
        // Summary:
        //     Sets the current window.
        //
        // Parameters:
        //   win:
        //     Identifier of GLUT window to make the current window.
        //
        // Remarks:
        //     glutSetWindow sets the current window. glutSetWindow does not change the
        //     layer in use for the window; this is done using Tao.FreeGlut.Glut.glutUseLayer(System.Int32).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetWindow(int win);
        //
        // Summary:
        //     Changes the window title of the current top-level window.
        //
        // Parameters:
        //   name:
        //     Character string for the window name to be set for the window.
        //
        // Remarks:
        //     glutSetWindowTitle should be called only when the current window is a top-level
        //     window. Upon creation of a top-level window, the window title is determined
        //     by the name parameter to Tao.FreeGlut.Glut.glutCreateWindow(System.String).
        //      Once created, glutSetWindowTitle can change the window title of top-level
        //     windows. Each call requests the window system change the title appropriately.
        //     Requests are not buffered or coalesced. The policy by which the window title
        //     is displayed is window system dependent.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSetWindowTitle(string name);
        //
        // Summary:
        //     Shows the overlay of the current window.
        //
        // Remarks:
        //     glutShowOverlay shows the overlay of the current window. The effect of showing
        //     an overlay takes place immediately. Note that glutShowOverlay will not actually
        //     display the overlay unless the window is also shown (and even a shown window
        //     may be obscured by other windows, thereby obscuring the overlay). It is typically
        //     faster and less resource intensive to use these routines to control the display
        //     status of an overlay as opposed to removing and re-establishing the overlay.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutShowOverlay();
        //
        // Summary:
        //     Changes the display status of the current window.
        //
        // Remarks:
        //     glutShowWindow will show the current window (though it may still not be visible
        //     if obscured by other shown windows). The effect of showing windows does not
        //     take place immediately. Instead the requests are saved for execution upon
        //     return to the GLUT event loop. Subsequent show requests on a window replace
        //     the previously saved request for that window. The effect of showing top-level
        //     windows is subject to the window system's policy for displaying windows.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutShowWindow();
        //
        // Summary:
        //     Renders a solid cone.
        //
        // Parameters:
        //   baseRadius:
        //     The radius of the base of the cone.
        //
        //   height:
        //     The height of the cone.
        //
        //   slices:
        //     The number of subdivisions around the Z axis.
        //
        //   stacks:
        //     The number of subdivisions along the Z axis.
        //
        // Remarks:
        //     glutSolidCone renders a solid cone oriented along the Z axis. The baseRadius
        //     of the cone is placed at Z = 0, and the top at Z = height. The cone is subdivided
        //     around the Z axis into slices, and along the Z axis into stacks.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidCone(double baseRadius, double height, int slices, int stacks);
        //
        // Summary:
        //     Renders a solid cube.
        //
        // Parameters:
        //   size:
        //     Length of the sides of the cube.
        //
        // Remarks:
        //     glutSolidCube renders a solid cube. The cube is centered at the modeling
        //     coordinates origin with sides of length size.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidCube(double size);
        //
        // Summary:
        //     Draw a solid cylinder.
        //
        // Parameters:
        //   radius:
        //     Radius of cylinder.
        //
        //   height:
        //     Z height.
        //
        //   slices:
        //     Number of divisions around the z axis.
        //
        //   stacks:
        //     Number of divisions along the z axis.
        //
        // Remarks:
        //     Draws a solid of a cylinder, the center of whose base is at the origin, and
        //     whose axis parallels the z axis.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidCylinder(double radius, double height, int slices, int stacks);
        //
        // Summary:
        //     Renders a solid dodecahedron (12-sided regular solid).
        //
        // Remarks:
        //     glutSolidDodecahedron renders a solid dodecahedron centered at the modeling
        //     coordinates origin with a radius of Sqrt(3).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidDodecahedron();
        //
        // Summary:
        //     Renders a solid icosahedron (20-sided regular solid).
        //
        // Remarks:
        //     glutSolidIcosahedron renders a solid icosahedron. The icosahedron is centered
        //     at the modeling coordinates origin and has a radius of 1.0.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidIcosahedron();
        //
        // Summary:
        //     Renders solid octahedron (8-sided regular solid).
        //
        // Remarks:
        //     glutSolidOctahedron renders a solid octahedron centered at the modeling coordinates
        //     origin with a radius of 1.0.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidOctahedron();
        //
        // Summary:
        //     Draw a solid rhombic dodecahedron.
        //
        // Remarks:
        //     This function draws a solid-shaded dodecahedron whose facets are rhombic
        //     and whose vertices are at unit radius.  No facet lies normal to any coordinate
        //     axes. The polyhedron is centered at the origin.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidRhombicDodecahedron();
        //
        // Summary:
        //     Draw a solid Spierspinski's sponge.
        //
        // Parameters:
        //   levels:
        //     Recursive depth.
        //
        //   offset:
        //     Location vector.
        //
        //   scale:
        //     Relative size.
        //
        // Remarks:
        //     This function recursively draws a few levels of a solid-shaded Sierpinski's
        //     Sponge. If levels is 0, draws 1 tetrahedron. The offset is a translation.
        //     The z axis is normal to the base. The sponge is centered at the origin.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidSierpinskiSponge(int levels, double[] offset, double scale);
        //
        // Summary:
        //     Renders a solid sphere.
        //
        // Parameters:
        //   radius:
        //     The radius of the sphere.
        //
        //   slices:
        //     The number of subdivisions around the Z axis (similar to lines of longitude).
        //
        //   stacks:
        //     The number of subdivisions along the Z axis (similar to lines of latitude).
        //
        // Remarks:
        //     glutSolidSphere renders a solid sphere centered at the modeling coordinates
        //     origin of the specified radius. The sphere is subdivided around the Z axis
        //     into slices and along the Z axis into stacks.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidSphere(double radius, int slices, int stacks);
        //
        // Summary:
        //     Renders a solid teapot.
        //
        // Parameters:
        //   size:
        //     Relative size of the teapot.
        //
        // Remarks:
        //      glutSolidTeapot renders a solid teapot. Both surface normals and texture
        //     coordinates for the teapot are generated. The teapot is generated with OpenGL
        //     evaluators.
        //     Footnote
        //     Yes, the classic computer graphics teapot modeled by Martin Newell in 1975.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidTeapot(double size);
        //
        // Summary:
        //     Renders a solid tetrahedron (4-sided regular solid).
        //
        // Remarks:
        //     glutSolidTetrahedron renders a solid tetrahedron centered at the modeling
        //     coordinates origin with a radius of Sqrt(3).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidTetrahedron();
        //
        // Summary:
        //     Renders a solid torus (doughnut).
        //
        // Parameters:
        //   innerRadius:
        //     Inner radius of the torus.
        //
        //   outerRadius:
        //     Outer radius of the torus.
        //
        //   sides:
        //     Number of sides for each radial section.
        //
        //   rings:
        //     Number of radial divisions for the torus.
        //
        // Remarks:
        //     glutSolidTorus renders a solid torus (doughnut) centered at the modeling
        //     coordinates origin whose axis is aligned with the Z axis.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSolidTorus(double innerRadius, double outerRadius, int sides, int rings);
        //
        // Summary:
        //     Sets the Spaceball button callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new Spaceball button callback function. See Tao.FreeGlut.Glut.SpaceballButtonCallback.
        //
        // Remarks:
        //      glutSpaceballButtonFunc sets the Spaceball button callback for the current
        //     window. The Spaceball button callback for a window is called when the window
        //     has Spaceball input focus (normally, when the mouse is in the window) and
        //     the user generates Spaceball button presses. The button parameter will be
        //     the button number (starting at one). The number of available Spaceball buttons
        //     can be determined with glutDeviceGet(GLUT_NUM_SPACEBALL_BUTTONS). The state
        //     is either Tao.FreeGlut.Glut.GLUT_UP or Tao.FreeGlut.Glut.GLUT_DOWN indicating
        //     whether the callback was due to a release or press respectively.
        //     Registering a Spaceball button callback when a Spaceball device is not available
        //     is ineffectual and not an error. In this case, no Spaceball button callbacks
        //     will be generated.
        //     Passing null to glutSpaceballButtonFunc disables the generation of Spaceball
        //     button callbacks. When a new window is created, no Spaceball button callback
        //     is initially registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutSpaceballButtonFunc(FreeGLUT.Delegates.SpaceballButtonCallback func);
        //
        // Summary:
        //     Sets the Spaceball motion callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new Spaceball motion callback function. See Tao.FreeGlut.Glut.SpaceballMotionCallback.
        //
        // Remarks:
        //      glutSpaceballMotionFunc sets the Spaceball motion callback for the current
        //     window. The Spaceball motion callback for a window is called when the window
        //     has Spaceball input focus (normally, when the mouse is in the window) and
        //     the user generates Spaceball translations. The x, y, and z callback parameters
        //     indicate the translations along the X, Y, and Z axes. The callback parameters
        //     are normalized to be within the range of -1000 to 1000 inclusive.
        //     Registering a Spaceball motion callback when a Spaceball device is not available
        //     has no effect and is not an error. In this case, no Spaceball motion callbacks
        //     will be generated.
        //     Passing null to glutSpaceballMotionFunc disables the generation of Spaceball
        //     motion callbacks. When a new window is created, no Spaceball motion callback
        //     is initially registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutSpaceballMotionFunc(FreeGLUT.Delegates.SpaceballMotionCallback func);
        //
        // Summary:
        //     Sets the Spaceball rotation callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new Spaceball rotate callback function. See Tao.FreeGlut.Glut.SpaceballRotateCallback.
        //
        // Remarks:
        //      glutSpaceballRotateFunc sets the Spaceball rotate callback for the current
        //     window. The Spaceball rotate callback for a window is called when the window
        //     has Spaceball input focus (normally, when the mouse is in the window) and
        //     the user generates Spaceball rotations. The x, y, and z callback parameters
        //     indicate the rotation along the X, Y, and Z axes. The callback parameters
        //     are normalized to be within the range of -1800 to 1800 inclusive.
        //     Registering a Spaceball rotate callback when a Spaceball device is not available
        //     is ineffectual and not an error. In this case, no Spaceball rotate callbacks
        //     will be generated.
        //     Passing null to glutSpaceballRotateFunc disables the generation of Spaceball
        //     rotate callbacks. When a new window is created, no Spaceball rotate callback
        //     is initially registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutSpaceballRotateFunc(FreeGLUT.Delegates.SpaceballRotateCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutSpecialFunc(FreeGLUT.Delegates.SpecialCallback func);
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutSpecialUpFunc(FreeGLUT.Delegates.SpecialUpCallback func);
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Remarks:
        //     Unknown. Unable to locate definitive documentation on this method.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutStopVideoResizing();
        //
        // Summary:
        //     Renders a stroke character using OpenGL.
        //
        // Parameters:
        //   font:
        //      Stroke font to use. Without using any display lists, glutStrokeCharacter
        //     renders the character in the named stroke font.  The available fonts are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_STROKE_ROMAN A proportionally spaced
        //     Roman Simplex font for ASCII characters 32 through 127. The maximum top character
        //     in the font is 119.05 units; the bottom descends 33.33 units.  Tao.FreeGlut.Glut.GLUT_STROKE_MONO_ROMAN
        //     A mono-spaced spaced Roman Simplex font (same characters as Tao.FreeGlut.Glut.GLUT_STROKE_ROMAN)
        //     for ASCII characters 32 through 127. The maximum top character in the font
        //     is 119.05 units; the bottom descends 33.33 units. Each character is 104.76
        //     units wide.
        //
        //   character:
        //     Character to render (not confined to 8 bits).
        //
        // Remarks:
        //      Rendering a nonexistent character has no effect. A Gl.glTranslatef is used
        //     to translate the current model view matrix to advance the width of the character.
        //     EXAMPLE
        //     Here is a routine that shows how to render a text string with glutStrokeCharacter:
        //     private void PrintText(float x, float y, string text) { GL.glPushMatrix();
        //     Gl.glTranslatef(x, y, 0); foreach(char c in text) { Glut.glutStrokeCharacter(Glut.GLUT_STROKE_ROMAN,
        //     c); } Gl.glPopMatrix(); }
        //     If you want to draw stroke font text using wide, antialiased lines, use:
        //     Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA); Gl.glEnable(Gl.GL_BLEND);
        //     Gl.glEnable(Gl.GL_LINE_SMOOTH); Gl.glLineWidth(2.0f); PrintText(200, 225,
        //     "This is antialiased.");
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutStrokeCharacter(IntPtr font, int character);
        //
        // Summary:
        //     Returns the height of a given font.
        //
        // Parameters:
        //   font:
        //     A GLUT stroked font identifier.
        //
        // Returns:
        //     Returns 0 if fontID is invalid, otherwise, the height of the font in pixels.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static float glutStrokeHeight(IntPtr font);
        //
        // Summary:
        //     Returns the length of a stroke font string.
        //
        // Parameters:
        //   font:
        //     Stroke font to use. For valid values see the Tao.FreeGlut.Glut.glutStrokeCharacter(System.IntPtr,System.Int32)
        //     description.
        //
        //   text:
        //     Text string.
        //
        // Returns:
        //     The length in modeling units of a string.
        //
        // Remarks:
        //     glutStrokeLength returns the length in modeling units of a string (8-bit
        //     characters). This length is equivalent to summing all the widths returned
        //     by Tao.FreeGlut.Glut.glutStrokeWidth(System.IntPtr,System.Int32) for each
        //     character in the string.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutStrokeLength(IntPtr font, string text);
        //
        // Summary:
        //     Draw a string of stroked characters.
        //
        // Parameters:
        //   font:
        //     A GLUT stroked font identifier.
        //
        //   str:
        //     The string to draw.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutStrokeString(IntPtr font, string str);
        //
        // Summary:
        //     Returns the width of a stroke character.
        //
        // Parameters:
        //   font:
        //     Stroke font to use. For valid values see the Tao.FreeGlut.Glut.glutStrokeCharacter(System.IntPtr,System.Int32)
        //     description.
        //
        //   character:
        //     Character to return width of (not confined to 8 bits).
        //
        // Returns:
        //     Returns the width in pixels of a stroke character in a supported stroke font.
        //
        // Remarks:
        //     glutStrokeWidth returns the width in pixels of a stroke character in a supported
        //     stroke font. While the width of characters in a font may vary (though fixed
        //     width fonts do not vary), the maximum height characteristics of a particular
        //     font are fixed.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutStrokeWidth(IntPtr font, int character);
        //
        // Summary:
        //     Swaps the buffers of the current window if double buffered.
        //
        // Remarks:
        //      Performs a buffer swap on the layer in use for the current window.  Specifically,
        //     glutSwapBuffers promotes the contents of the back buffer of the layer in
        //     use of the current window to become the contents of the front buffer. The
        //     contents of the back buffer then become undefined. The update typically takes
        //     place during the vertical retrace of the monitor, rather than immediately
        //     after glutSwapBuffers is called.
        //     An implicit Tao.OpenGl.Gl.glFlush() is done by glutSwapBuffers before it
        //     returns. Subsequent OpenGL commands can be issued immediately after calling
        //     glutSwapBuffers, but are not executed until the buffer exchange is completed.
        //     If the layer in use is not double buffered, glutSwapBuffers has no effect.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutSwapBuffers();
        //
        // Summary:
        //     Sets the tablet button callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new tablet button callback function. See Tao.FreeGlut.Glut.TabletButtonCallback.
        //
        // Remarks:
        //      glutTabletButtonFunc sets the tablet button callback for the current window.
        //     The tablet button callback for a window is called when the window has tablet
        //     input focus (normally, when the mouse is in the window) and the user generates
        //     tablet button presses. The button parameter will be the button number (starting
        //     at one). The number of available tablet buttons can be determined with Glut.glutDeviceGet(Glut.GLUT_NUM_TABLET_BUTTONS).
        //      The state is either Tao.FreeGlut.Glut.GLUT_UP or Tao.FreeGlut.Glut.GLUT_DOWN
        //     indicating whether the callback was due to a release or press respectively.
        //      The x and y callback parameters indicate the window relative coordinates
        //     when the tablet button state changed.
        //     Registering a tablet button callback when a tablet device is not available
        //     is ineffectual and not an error. In this case, no tablet button callbacks
        //     will be generated.
        //     Passing null to glutTabletButtonFunc disables the generation of tablet button
        //     callbacks. When a new window is created, no tablet button callback is initially
        //     registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutTabletButtonFunc(FreeGLUT.Delegates.TabletButtonCallback func);
        //
        // Summary:
        //     Sets the tablet motion callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new tablet motion callback function. See Tao.FreeGlut.Glut.TabletMotionCallback.
        //
        // Remarks:
        //      glutTabletMotionFunc sets the tablet motion callback for the current window.
        //     The tablet motion callback for a window is called when the window has tablet
        //     input focus (normally, when the mouse is in the window) and the user generates
        //     tablet motion. The x and y callback parameters indicate the absolute position
        //     of the tablet "puck" on the tablet. The callback parameters are normalized
        //     to be within the range of 0 to 2000 inclusive.
        //     Registering a tablet motion callback when a tablet device is not available
        //     is ineffectual and not an error. In this case, no tablet motion callbacks
        //     will be generated.
        //     Passing null to glutTabletMotionFunc disables the generation of tablet motion
        //     callbacks. When a new window is created, no tablet motion callback is initially
        //     registered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutTabletMotionFunc(FreeGLUT.Delegates.TabletMotionCallback func);
        //
        // Summary:
        //     Registers a timer callback to be triggered in a specified number of milliseconds.
        //
        // Parameters:
        //   msecs:
        //     The number of milliseconds between calls to the timer callback.
        //
        //   func:
        //     The new timer callback function. See Tao.FreeGlut.Glut.TimerCallback.
        //
        //   val:
        //     The value to be passed to the timer callback.
        //
        // Remarks:
        //      glutTimerFunc registers the timer callback func to be triggered in at least
        //     msecs milliseconds. The val parameter to the timer callback will be the value
        //     of the val parameter to glutTimerFunc. Multiple timer callbacks at same or
        //     differing times may be registered simultaneously.
        //     The number of milliseconds is a lower bound on the time before the callback
        //     is generated. GLUT attempts to deliver the timer callback as soon as possible
        //     after the expiration of the callback's time interval.
        //     There is no support for canceling a registered callback. Instead, ignore
        //     a callback based on its val parameter when it is triggered.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutTimerFunc(int msecs, FreeGLUT.Delegates.TimerCallback func, int val);
        //
        // Summary:
        //     Changes the layer in use for the current window.
        //
        // Parameters:
        //   layer:
        //     Either Tao.FreeGlut.Glut.GLUT_NORMAL or Tao.FreeGlut.Glut.GLUT_OVERLAY, selecting
        //     the normal plane or overlay respectively.
        //
        // Remarks:
        //      glutUseLayer changes the per-window layer in use for the current window,
        //     selecting either the normal plane or overlay. The overlay should only be
        //     specified if an overlay exists, however windows without an overlay may still
        //     call Glut.glutUseLayer(Glut.GLUT_NORMAL). OpenGL commands for the window
        //     are directed to the current layer in use.
        //     To query the layer in use for a window, call Glut.glutLayerGet(Glut.GLUT_LAYER_IN_USE).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutUseLayer(int layer);
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Parameters:
        //   x:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   y:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   width:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   height:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Remarks:
        //     Unknown. Unable to locate definitive documentation on this method.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutVideoPan(int x, int y, int width, int height);
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Parameters:
        //   x:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   y:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   width:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        //   height:
        //     Unknown. Unable to locate definitive documentation on this method.
        //
        // Remarks:
        //     Unknown. Unable to locate definitive documentation on this method.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutVideoResize(int x, int y, int width, int height);
        //
        // Summary:
        //     Retrieves GLUT video resize information represented by integers.
        //
        // Parameters:
        //   param:
        //      Name of video resize information to retrieve. Available values are:
        //     Value Description Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_POSSIBLE Non-zero if
        //     video resizing is supported by the underlying system; zero if not supported.
        //     If this is zero, the other video resize GLUT calls do nothing when called.
        //      Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_X_DELTA Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_Y_DELTA
        //     Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_WIDTH_DELTA Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_HEIGHT_DELTA
        //     Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_X Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_Y
        //     Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_WIDTH Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_HEIGHT
        //     Unknown Tao.FreeGlut.Glut.GLUT_VIDEO_RESIZE_IN_USE Unknown
        //
        // Remarks:
        //      glutVideoResizeGet retrieves GLUT video resizing information represented
        //     by integers. The param parameter determines what type of video resize information
        //     to return.
        //     X IMPLEMENTATION NOTES
        //     The current implementation uses the SGIX_video_resize GLX extension.  This
        //     extension is currently supported on SGI's InfiniteReality-based systems.
        //     WIN32 IMPLEMENTATION NOTES
        //     The current implementation never reports that video resizing is possible.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static int glutVideoResizeGet(int param);
        //
        // Summary:
        //     Sets the visibility callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new visibility callback function. See Tao.FreeGlut.Glut.VisibilityCallback.
        //
        // Remarks:
        //      glutVisibilityFunc sets the visibility callback for the current window.
        //      The visibility callback for a window is called when the visibility of a
        //     window changes. The state callback parameter is either Tao.FreeGlut.Glut.GLUT_NOT_VISIBLE
        //     or Tao.FreeGlut.Glut.GLUT_VISIBLE depending on the current visibility of
        //     the window. GLUT_VISIBLE does not distinguish a window being totally versus
        //     partially visible.  GLUT_NOT_VISIBLE means no part of the window is visible,
        //     i.e., until the window's visibility changes, all further rendering to the
        //     window is discarded.
        //     GLUT considers a window visible if any pixel of the window is visible or
        //     any pixel of any descendant window is visible on the screen.
        //     Passing null to glutVisibilityFunc disables the generation of the visibility
        //     callback.
        //     If the visibility callback for a window is disabled and later re-enabled,
        //     the visibility status of the window is undefined; any change in window visibility
        //     will be reported, that is if you disable a visibility callback and re-enable
        //     the callback, you are guaranteed the next visibility change will be reported.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutVisibilityFunc(FreeGLUT.Delegates.VisibilityCallback func);
        //
        // Summary:
        //     Warps the pointer's location.
        //
        // Parameters:
        //   x:
        //     X offset relative to the current window's origin (upper left).
        //
        //   y:
        //     Y offset relative to the current window's origin (upper left).
        //
        // Remarks:
        //      glutWarpPointer warps the window system's pointer to a new location relative
        //     to the origin of the current window. The new location will be offset x pixels
        //     on the X axis and y pixels on the Y axis. These parameters may be negative.
        //     The warp is done immediately.
        //     If the pointer would be warped outside the screen's frame buffer region,
        //     the location will be clamped to the nearest screen edge. The window system
        //     is allowed to further constrain the pointer's location in window system dependent
        //     ways.
        //     The following is good advice that applies to glutWarpPointer: "There is seldom
        //     any reason for calling this function. The pointer should normally be left
        //     to the user." (from Xlib's XWarpPointer man page.)
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWarpPointer(int x, int y);
        //
        // Summary:
        //     Sets the window status callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new window status callback function. See Tao.FreeGlut.Glut.WindowStatusCallback.
        //
        // Remarks:
        //      glutWindowStatusFunc sets the window status callback for the current window.
        //     The window status callback for a window is called when the window status
        //     (visibility) of a window changes. The state callback parameter is one of
        //     Tao.FreeGlut.Glut.GLUT_HIDDEN, Tao.FreeGlut.Glut.GLUT_FULLY_RETAINED, Tao.FreeGlut.Glut.GLUT_PARTIALLY_RETAINED,
        //     or Tao.FreeGlut.Glut.GLUT_FULLY_COVERED depending on the current window status
        //     of the window. GLUT_HIDDEN means that the window is either not shown (often
        //     meaning that the window is iconified). GLUT_FULLY_RETAINED means that the
        //     window is fully retained (no pixels belonging to the window are covered by
        //     other windows).  GLUT_PARTIALLY_RETAINED means that the window is partially
        //     retained (some but not all pixels belonging to the window are covered by
        //     other windows). GLUT_FULLY_COVERED means the window is shown but no part
        //     of the window is visible, i.e., until the window's status changes, all further
        //     rendering to the window is discarded.
        //     GLUT considers a window visible if any pixel of the window is visible or
        //     any pixel of any descendant window is visible on the screen.
        //     GLUT applications are encouraged to disable rendering and/or animation when
        //     windows have a status of either GLUT_HIDDEN or GLUT_FULLY_COVERED.
        //     Passing null to glutWindowStatusFunc disables the generation of the window
        //     status callback.
        //     If the window status callback for a window is disabled and later re-enabled,
        //     the window status of the window is undefined; any change in window window
        //     status will be reported, that is if you disable a window status callback
        //     and re-enable the callback, you are guaranteed the next window status change
        //     will be reported.
        //     Setting the window status callback for a window disables the visibility callback
        //     set for the window (and vice versa). The visibility callback is set with
        //     Tao.FreeGlut.Glut.glutVisibilityFunc(Tao.FreeGlut.Glut.VisibilityCallback).
        //     glutVisibilityFunc is deprecated in favor of the more informative glutWindowStatusFunc.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutWindowStatusFunc(FreeGLUT.Delegates.WindowStatusCallback func);
        //
        // Summary:
        //     Renders a wireframe cone.
        //
        // Parameters:
        //   baseRadius:
        //     The radius of the base of the cone.
        //
        //   height:
        //     The height of the cone.
        //
        //   slices:
        //     The number of subdivisions around the Z axis.
        //
        //   stacks:
        //     The number of subdivisions along the Z axis.
        //
        // Remarks:
        //     glutWireCone renders a wireframe cone oriented along the Z axis. The baseRadius
        //     of the cone is placed at Z = 0, and the top at Z = height. The cone is subdivided
        //     around the Z axis into slices, and along the Z axis into stacks.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireCone(double baseRadius, double height, int slices, int stacks);
        //
        // Summary:
        //     Renders a wireframe cube.
        //
        // Parameters:
        //   size:
        //     Length of the sides of the cube.
        //
        // Remarks:
        //     glutWireCube renders a wireframe cube. The cube is centered at the modeling
        //     coordinates origin with sides of length size.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireCube(double size);
        //
        // Summary:
        //     Draw a wireframe cylinder.
        //
        // Parameters:
        //   radius:
        //     Radius of cylinder.
        //
        //   height:
        //     Z height.
        //
        //   slices:
        //     Number of divisions around the z axis.
        //
        //   stacks:
        //     Number of divisions along the z axis.
        //
        // Remarks:
        //     Draws a wireframe of a cylinder, the center of whose base is at the origin,
        //     and whose axis parallels the z axis.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireCylinder(double radius, double height, int slices, int stacks);
        //
        // Summary:
        //     Renders a wireframe dodecahedron (12-sided regular solid).
        //
        // Remarks:
        //     glutWireDodecahedron renders a wireframe dodecahedron centered at the modeling
        //     coordinates origin with a radius of Sqrt(3).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireDodecahedron();
        //
        // Summary:
        //     Renders a wireframe icosahedron (20-sided regular solid).
        //
        // Remarks:
        //     glutWireIcosahedron renders a wireframe icosahedron. The icosahedron is centered
        //     at the modeling coordinates origin and has a radius of 1.0.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireIcosahedron();
        //
        // Summary:
        //     Renders wireframe octahedron (8-sided regular solid).
        //
        // Remarks:
        //     glutWireOctahedron renders a wireframe octahedron centered at the modeling
        //     coordinates origin with a radius of 1.0.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireOctahedron();
        //
        // Summary:
        //     Draw a wireframe rhombic dodecahedron.
        //
        // Remarks:
        //     This function draws a wireframe dodecahedron whose facets are rhombic and
        //     whose vertices are at unit radius.  No facet lies normal to any coordinate
        //     axes. The polyhedron is centered at the origin.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireRhombicDodecahedron();
        //
        // Summary:
        //     Draw a wireframe Spierspinski's sponge
        //
        // Parameters:
        //   levels:
        //     Recursive depth.
        //
        //   offset:
        //     Location vector.
        //
        //   scale:
        //     Relative size.
        //
        // Remarks:
        //     This function recursively draws a few levels of Sierpinski's Sponge in wireframe.
        //      If levels is 0, draws 1 tetrahedron. The offset is a translation.  The z
        //     axis is normal to the base. The sponge is centered at the origin.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireSierpinskiSponge(int levels, double[] offset, double scale);
        //
        // Summary:
        //     Renders a wireframe sphere.
        //
        // Parameters:
        //   radius:
        //     The radius of the sphere.
        //
        //   slices:
        //     The number of subdivisions around the Z axis (similar to lines of longitude).
        //
        //   stacks:
        //     The number of subdivisions along the Z axis (similar to lines of latitude).
        //
        // Remarks:
        //     glutWireSphere renders a wireframe sphere centered at the modeling coordinates
        //     origin of the specified radius. The sphere is subdivided around the Z axis
        //     into slices and along the Z axis into stacks.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireSphere(double radius, int slices, int stacks);
        //
        // Summary:
        //     Renders a wireframe teapot.
        //
        // Parameters:
        //   size:
        //     Relative size of the teapot.
        //
        // Remarks:
        //      glutWireTeapot renders a wireframe teapot. Both surface normals and texture
        //     coordinates for the teapot are generated. The teapot is generated with OpenGL
        //     evaluators.
        //     Footnote
        //     Yes, the classic computer graphics teapot modeled by Martin Newell in 1975.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireTeapot(double size);
        //
        // Summary:
        //     Renders a wireframe tetrahedron (4-sided regular solid).
        //
        // Remarks:
        //     glutWireTetrahedron renders a wireframe tetrahedron centered at the modeling
        //     coordinates origin with a radius of Sqrt(3).
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireTetrahedron();
        //
        // Summary:
        //     Renders a wireframe torus (doughnut).
        //
        // Parameters:
        //   innerRadius:
        //     Inner radius of the torus.
        //
        //   outerRadius:
        //     Outer radius of the torus.
        //
        //   sides:
        //     Number of sides for each radial section.
        //
        //   rings:
        //     Number of radial divisions for the torus.
        //
        // Remarks:
        //     glutWireTorus renders a wireframe torus (doughnut) centered at the modeling
        //     coordinates origin whose axis is aligned with the Z axis.
        [SuppressUnmanagedCodeSecurity, DllImport(FREEGLUT_LIBRARY)]
        public extern static void glutWireTorus(double innerRadius, double outerRadius, int sides, int rings);
        //
        // Summary:
        //     Sets the window close callback for the current window.
        //
        // Parameters:
        //   func:
        //     The new window close callback function. See Tao.FreeGlut.Glut.WindowCloseCallback.
        [DllImport(FREEGLUT_LIBRARY)]
        public static extern void glutWMCloseFunc(FreeGLUT.Delegates.WindowCloseCallback func);
    }
}
