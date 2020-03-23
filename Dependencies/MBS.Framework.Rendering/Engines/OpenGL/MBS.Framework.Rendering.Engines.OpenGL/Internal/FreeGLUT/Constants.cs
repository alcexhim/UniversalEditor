using System;
namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeGLUT
{
    public class Constants
    {
        /// <summary>
        /// FreeGLUT API marker.
        /// </summary>
        public const int FREEGLUT = 1;
        /// <summary>
        /// FreeGLUT API version marker.
        /// </summary>
        public const int FREEGLUT_VERSION_2_0 = 1;
        /// <summary>
        /// Bit mask to select a window with an accumulation buffer.
        /// </summary>
        public const int GLUT_ACCUM = 4;
        /// <summary>
        /// Continue execution on window close button click.
        /// </summary>
        public const int GLUT_ACTION_CONTINUE_EXECUTION = 2;
        /// <summary>
        /// Close window on window close button click.
        /// </summary>
        public const int GLUT_ACTION_EXIT = 0;
        /// <summary>
        /// Return from main loop on window close button click.
        /// </summary>
        public const int GLUT_ACTION_GLUTMAINLOOP_RETURNS = 1;
        /// <summary>
        /// Gets current action for window-close.
        /// </summary>
        public const int GLUT_ACTION_ON_WINDOW_CLOSE = 505;
        /// <summary>
        /// Set if the Alt modifier is active.
        /// </summary>
        public const int GLUT_ACTIVE_ALT = 4;
        /// <summary>
        /// Set if the Ctrl modifier is active.
        /// </summary>
        public const int GLUT_ACTIVE_CTRL = 2;
        /// <summary>
        /// Set if the Shift modifier or Caps Lock is active.
        /// </summary>
        public const int GLUT_ACTIVE_SHIFT = 1;
        /// <summary>
        /// Bit mask to select a window with an alpha component to the color buffer(s).
        /// </summary>
        public const int GLUT_ALPHA = 8;
        /// <summary>
        /// GLUT API revision.
        /// <remarks>
        /// GLUT_API_VERSION is updated to reflect incompatible GLUT API changes (interface
        /// changes, semantic changes, deletions, or additions).
        /// GLUT_API_VERSION=1 First public release of GLUT. 11/29/94
        /// GLUT_API_VERSION=2 Added support for OpenGL/GLX multisampling, extension.
        /// Supports new input devices like tablet, dial and button box, and Spaceball.
        /// Easy to query OpenGL extensions.
        /// GLUT_API_VERSION=3 glutMenuStatus added.
        /// GLUT_API_VERSION=4 glutInitDisplayString, glutWarpPointer, glutBitmapLength,
        /// glutStrokeLength, glutWindowStatusFunc, dynamic video resize subAPI, glutPostWindowRedisplay,
        /// glutKeyboardUpFunc, glutSpecialUpFunc, glutIgnoreKeyRepeat, glutSetKeyRepeat,
        /// glutJoystickFunc, glutForceJoystickFunc (NOT FINALIZED!).
        /// </remarks>
        /// </summary>
        public const int GLUT_API_VERSION = 4;
        /// <summary>
        /// Blue color component.
        /// </summary>
        public const int GLUT_BLUE = 2;
        /// <summary>
        /// Create a new context when user opens a new window.
        /// </summary>
        public const int GLUT_CREATE_NEW_CONTEXT = 0;
        /// <summary>
        /// Arrow pointing to bottom-left corner.
        /// </summary>
        public const int GLUT_CURSOR_BOTTOM_LEFT_CORNER = 19;
        /// <summary>
        /// Arrow pointing to bottom-right corner.
        /// </summary>
        public const int GLUT_CURSOR_BOTTOM_RIGHT_CORNER = 18;
        /// <summary>
        /// Arrow pointing to bottom side.
        /// </summary>
        public const int GLUT_CURSOR_BOTTOM_SIDE = 13;
        //
        // Summary:
        //     Simple cross-hair.
        public const int GLUT_CURSOR_CROSSHAIR = 9;
        //
        // Summary:
        //     Arrows rotating in a circle.
        public const int GLUT_CURSOR_CYCLE = 5;
        //
        // Summary:
        //     Skull and cross bones.
        public const int GLUT_CURSOR_DESTROY = 3;
        //
        // Summary:
        //     Full-screen cross-hair cursor (if possible, otherwise Tao.FreeGlut.Glut.GLUT_CURSOR_CROSSHAIR.
        public const int GLUT_CURSOR_FULL_CROSSHAIR = 102;
        //
        // Summary:
        //     Question mark.
        public const int GLUT_CURSOR_HELP = 4;
        //
        // Summary:
        //     Pointing hand.
        public const int GLUT_CURSOR_INFO = 2;
        //
        // Summary:
        //     Use parent's cursor.
        public const int GLUT_CURSOR_INHERIT = 100;
        //
        // Summary:
        //     Arrow pointing up and to the left.
        public const int GLUT_CURSOR_LEFT_ARROW = 1;
        //
        // Summary:
        //     Bi-directional pointing left and right.
        public const int GLUT_CURSOR_LEFT_RIGHT = 11;
        //
        // Summary:
        //     Arrow pointing to left side.
        public const int GLUT_CURSOR_LEFT_SIDE = 14;
        //
        // Summary:
        //     Invisible cursor.
        public const int GLUT_CURSOR_NONE = 101;
        //
        // Summary:
        //     Arrow pointing up and to the right.
        public const int GLUT_CURSOR_RIGHT_ARROW = 0;
        //
        // Summary:
        //     Arrow pointing to right side.
        public const int GLUT_CURSOR_RIGHT_SIDE = 15;
        //
        // Summary:
        //     Spray can.
        public const int GLUT_CURSOR_SPRAY = 6;
        //
        // Summary:
        //     Insertion point cursor for text.
        public const int GLUT_CURSOR_TEXT = 8;
        //
        // Summary:
        //     Arrow pointing to top-left corner.
        public const int GLUT_CURSOR_TOP_LEFT_CORNER = 16;
        //
        // Summary:
        //     Arrow pointing to top-right corner.
        public const int GLUT_CURSOR_TOP_RIGHT_CORNER = 17;
        //
        // Summary:
        //     Arrow pointing to top side.
        public const int GLUT_CURSOR_TOP_SIDE = 12;
        //
        // Summary:
        //     Bi-directional pointing up and down.
        public const int GLUT_CURSOR_UP_DOWN = 10;
        //
        // Summary:
        //     Wrist watch.
        public const int GLUT_CURSOR_WAIT = 7;
        //
        // Summary:
        //     Bit mask to select a window with a depth buffer.
        public const int GLUT_DEPTH = 16;
        //
        // Summary:
        //     Returns true if the current window's auto repeated keys are ignored. This
        //     state is controlled by Tao.FreeGlut.Glut.glutIgnoreKeyRepeat(System.Int32).
        public const int GLUT_DEVICE_IGNORE_KEY_REPEAT = 610;
        //
        // Summary:
        //     The window system's global key repeat state. Returns either Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_OFF,
        //     Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_ON, or Tao.FreeGlut.Glut.GLUT_KEY_REPEAT_DEFAULT.
        //     This will not necessarily return the value last passed to Tao.FreeGlut.Glut.glutSetKeyRepeat(System.Int32).
        public const int GLUT_DEVICE_KEY_REPEAT = 611;
        //
        // Summary:
        //     Whether the current display mode is supported or not.
        public const int GLUT_DISPLAY_MODE_POSSIBLE = 400;
        //
        // Summary:
        //     Bit mask to select a double buffered window. This overrides Tao.FreeGlut.Glut.GLUT_SINGLE
        //     if it is also specified.
        public const int GLUT_DOUBLE = 2;
        //
        // Summary:
        //     Mouse button down.
        public const int GLUT_DOWN = 0;
        //
        // Summary:
        //     Number of milliseconds since Tao.FreeGlut.Glut.glutInit() called (or first
        //     call to glutGet(GLUT_ELAPSED_TIME)).
        public const int GLUT_ELAPSED_TIME = 700;
        //
        // Summary:
        //     Mouse pointer has entered the window.
        public const int GLUT_ENTERED = 1;
        //
        // Summary:
        //     The window is shown but no part of the window is visible.
        public const int GLUT_FULLY_COVERED = 3;
        //
        // Summary:
        //     No pixels belonging to the window are covered by other windows.
        public const int GLUT_FULLY_RETAINED = 1;
        //
        // Summary:
        //     Non-zero if GLUT's game mode is active; zero if not active. Game mode is
        //     not active initially. Game mode becomes active when Tao.FreeGlut.Glut.glutEnterGameMode()
        //     is called. Game mode becomes inactive when Tao.FreeGlut.Glut.glutLeaveGameMode()
        //     is called.
        public const int GLUT_GAME_MODE_ACTIVE = 0;
        //
        // Summary:
        //     Non-zero if entering game mode actually changed the display settings. If
        //     the game mode string is not possible or the display mode could not be changed
        //     for any other reason, zero is returned.
        public const int GLUT_GAME_MODE_DISPLAY_CHANGED = 6;
        //
        // Summary:
        //     Height in pixels of the screen when game mode is activated.
        public const int GLUT_GAME_MODE_HEIGHT = 3;
        //
        // Summary:
        //     Pixel depth of the screen when game mode is activiated.
        public const int GLUT_GAME_MODE_PIXEL_DEPTH = 4;
        //
        // Summary:
        //     Non-zero if the game mode string last specified to Tao.FreeGlut.Glut.glutGameModeString(System.String)
        //     is a possible game mode configuration; zero otherwise. Being "possible" does
        //     not guarantee that if game mode is entered with Tao.FreeGlut.Glut.glutEnterGameMode()
        //     that the display settings will actually changed. Tao.FreeGlut.Glut.GLUT_GAME_MODE_DISPLAY_CHANGED
        //     should be called once game mode is entered to determine if the display mode
        //     is actually changed.
        public const int GLUT_GAME_MODE_POSSIBLE = 1;
        //
        // Summary:
        //     Screen refresh rate in cyles per second (hertz) when game mode is activated.
        //      Zero is returned if the refresh rate is unknown or cannot be queried.
        public const int GLUT_GAME_MODE_REFRESH_RATE = 5;
        //
        // Summary:
        //     Width in pixels of the screen when game mode is activated.
        public const int GLUT_GAME_MODE_WIDTH = 2;
        //
        // Summary:
        //     Green color component.
        public const int GLUT_GREEN = 1;
        //
        // Summary:
        //     Non-zero if a dial and button box is available; zero if not available.
        public const int GLUT_HAS_DIAL_AND_BUTTON_BOX = 603;
        //
        // Summary:
        //     Non-zero if a joystick is available; zero if not available.
        public const int GLUT_HAS_JOYSTICK = 612;
        //
        // Summary:
        //     Non-zero if a keyboard is available; zero if not available. For most GLUT
        //     implementations, a keyboard can be assumed.
        public const int GLUT_HAS_KEYBOARD = 600;
        //
        // Summary:
        //     Non-zero if a mouse is available; zero if not available. For most GLUT implementations,
        //     a keyboard can be assumed.
        public const int GLUT_HAS_MOUSE = 601;
        //
        // Summary:
        //     If the current window has an overlay established.
        public const int GLUT_HAS_OVERLAY = 802;
        //
        // Summary:
        //     Non-zero if a Spaceball is available; zero if not available.
        public const int GLUT_HAS_SPACEBALL = 602;
        //
        // Summary:
        //     Non-zero if a tablet is available; zero if not available.
        public const int GLUT_HAS_TABLET = 604;
        //
        // Summary:
        //     The window is not shown or iconified.
        public const int GLUT_HIDDEN = 0;
        //
        // Summary:
        //     Bit mask to select a color index mode window. This overrides Tao.FreeGlut.Glut.GLUT_RGB
        //     or Tao.FreeGlut.Glut.GLUT_RGBA if they are also specified.
        public const int GLUT_INDEX = 1;
        //
        // Summary:
        //     The initial display mode bit mask.
        public const int GLUT_INIT_DISPLAY_MODE = 504;
        //
        // Summary:
        //     Unknown.
        public const int GLUT_INIT_STATE = 124;
        //
        // Summary:
        //     Number of axes supported by the joystick. If no joystick is supposrted, zero
        //     is returned.
        public const int GLUT_JOYSTICK_AXES = 615;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_JOYSTICK_BUTTON_A = 1;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_JOYSTICK_BUTTON_B = 2;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_JOYSTICK_BUTTON_C = 4;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_JOYSTICK_BUTTON_D = 8;
        //
        // Summary:
        //     Number of buttons supported by the joystick. If no joystick is supported,
        //     zero is returned.
        public const int GLUT_JOYSTICK_BUTTONS = 614;
        //
        // Summary:
        //     Returns the current window's joystick poll rate as set by Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32).
        //     If no joystick is supported, the poll rate will always be zero. The joystick
        //     poll rate also returns zero if the poll rate last specified to Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32)
        //     is negative or a NULL callback was registered.
        public const int GLUT_JOYSTICK_POLL_RATE = 616;
        //
        // Summary:
        //     Down directional key.
        public const int GLUT_KEY_DOWN = 103;
        //
        // Summary:
        //     End directional key.
        public const int GLUT_KEY_END = 107;
        //
        // Summary:
        //     F1 function key.
        public const int GLUT_KEY_F1 = 1;
        //
        // Summary:
        //     F10 function key.
        public const int GLUT_KEY_F10 = 10;
        //
        // Summary:
        //     F11 function key.
        public const int GLUT_KEY_F11 = 11;
        //
        // Summary:
        //     F12 function key.
        public const int GLUT_KEY_F12 = 12;
        //
        // Summary:
        //     F2 function key.
        public const int GLUT_KEY_F2 = 2;
        //
        // Summary:
        //     F3 function key.
        public const int GLUT_KEY_F3 = 3;
        //
        // Summary:
        //     F4 function key.
        public const int GLUT_KEY_F4 = 4;
        //
        // Summary:
        //     F5 function key.
        public const int GLUT_KEY_F5 = 5;
        //
        // Summary:
        //     F6 function key.
        public const int GLUT_KEY_F6 = 6;
        //
        // Summary:
        //     F7 function key.
        public const int GLUT_KEY_F7 = 7;
        //
        // Summary:
        //     F8 function key.
        public const int GLUT_KEY_F8 = 8;
        //
        // Summary:
        //     F9 function key.
        public const int GLUT_KEY_F9 = 9;
        //
        // Summary:
        //     Home directional key.
        public const int GLUT_KEY_HOME = 106;
        //
        // Summary:
        //     Insert directional key.
        public const int GLUT_KEY_INSERT = 108;
        //
        // Summary:
        //     Left directional key.
        public const int GLUT_KEY_LEFT = 100;
        //
        // Summary:
        //     Page Down directional key.
        public const int GLUT_KEY_PAGE_DOWN = 105;
        //
        // Summary:
        //     Page Up directional key.
        public const int GLUT_KEY_PAGE_UP = 104;
        //
        // Summary:
        //     Reset the key repeat mode for the window system to its default state.
        public const int GLUT_KEY_REPEAT_DEFAULT = 2;
        //
        // Summary:
        //     Disable key repeat for the window system on a global basis.
        public const int GLUT_KEY_REPEAT_OFF = 0;
        //
        // Summary:
        //     Enable key repeat for the window system on a global basis.
        public const int GLUT_KEY_REPEAT_ON = 1;
        //
        // Summary:
        //     Right directional key.
        public const int GLUT_KEY_RIGHT = 102;
        //
        // Summary:
        //     Up directional key.
        public const int GLUT_KEY_UP = 101;
        //
        // Summary:
        //     Either Tao.FreeGlut.Glut.GLUT_NORMAL or Tao.FreeGlut.Glut.GLUT_OVERLAY depending
        //     on whether the normal plane or overlay is the layer in use.
        public const int GLUT_LAYER_IN_USE = 801;
        //
        // Summary:
        //     Mouse pointer has left the window.
        public const int GLUT_LEFT = 0;
        //
        // Summary:
        //     Left mouse button.
        public const int GLUT_LEFT_BUTTON = 0;
        //
        // Summary:
        //     Bit mask to select a window with a "luminance" color model. This model provides
        //     the functionality of OpenGL's RGBA color model, but the green and blue components
        //     are not maintained in the frame buffer. Instead each pixel's red component
        //     is converted to an index between zero and Glut.glutGet(Glut.GLUT_WINDOW_COLORMAP_SIZE)
        //     - 1 and looked up in a per-window color map to determine the color of pixels
        //     within the window. The initial colormap of Tao.FreeGlut.Glut.GLUT_LUMINANCE
        //     windows is initialized to be a linear gray ramp, but can be modified with
        //     GLUT's colormap routines.
        public const int GLUT_LUMINANCE = 512;
        //
        // Summary:
        //     Pop-up menus are in use by the user.
        public const int GLUT_MENU_IN_USE = 1;
        //
        // Summary:
        //     Pop-up menus are not in use by the user.
        public const int GLUT_MENU_NOT_IN_USE = 0;
        //
        // Summary:
        //     Number of menu items in the current menu.
        public const int GLUT_MENU_NUM_ITEMS = 300;
        //
        // Summary:
        //     Middle mouse button.
        public const int GLUT_MIDDLE_BUTTON = 1;
        //
        // Summary:
        //     Bit mask to select a window with multisampling support. If multisampling
        //     is not available, a non-multisampling window will automatically be chosen.
        //     Note: both the OpenGL client-side and server-side implementations must support
        //     the GLX_SAMPLE_SGIS extension for multisampling to be available.
        public const int GLUT_MULTISAMPLE = 128;
        //
        // Summary:
        //     The normal plane.
        public const int GLUT_NORMAL = 0;
        //
        // Summary:
        //     True if the normal plane of the current window has damaged (by window system
        //     activity) since the last display callback was triggered. Calling Tao.FreeGlut.Glut.glutPostRedisplay()
        //     will not set this true.
        public const int GLUT_NORMAL_DAMAGED = 804;
        //
        // Summary:
        //     The window is not visible. No part of the window is visible. All further
        //     rendering to the window is discarded until the window's visibility changes.
        public const int GLUT_NOT_VISIBLE = 0;
        //
        // Summary:
        //     Number of buttons supported by the dial and button box device. If no dials
        //     and button box device is supported, zero is returned.
        public const int GLUT_NUM_BUTTON_BOX_BUTTONS = 607;
        //
        // Summary:
        //     Number of dials supported by the dial and button box device. If no dials
        //     and button box device is supported, zero is returned.
        public const int GLUT_NUM_DIALS = 608;
        //
        // Summary:
        //     Number of buttons supported by the mouse. If no mouse is supported, zero
        //     is returned.
        public const int GLUT_NUM_MOUSE_BUTTONS = 605;
        //
        // Summary:
        //     Number of buttons supported by the Spaceball. If no Spaceball is supported,
        //     zero is returned.
        public const int GLUT_NUM_SPACEBALL_BUTTONS = 606;
        //
        // Summary:
        //     Number of buttons supported by the tablet. If no tablet is supported, zero
        //     is returned.
        public const int GLUT_NUM_TABLET_BUTTONS = 609;
        //
        // Summary:
        //     The overlay plane.
        public const int GLUT_OVERLAY = 1;
        //
        // Summary:
        //     True if the overlay plane of the current window has damaged (by window system
        //     activity) since the last display callback was triggered. Calling Tao.FreeGlut.Glut.glutPostRedisplay()
        //     or Tao.FreeGlut.Glut.glutPostOverlayRedisplay() will not set this true. Negative
        //     one is returned if no overlay is in use.
        public const int GLUT_OVERLAY_DAMAGED = 805;
        //
        // Summary:
        //     Whether an overlay could be established for the current window given the
        //     current initial display mode. If false, Tao.FreeGlut.Glut.glutEstablishOverlay()
        //     will fail with a fatal error if called.
        public const int GLUT_OVERLAY_POSSIBLE = 800;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        //
        // Remarks:
        //     Unofficially, this doesn't appear to be implemented.
        public const int GLUT_OWNS_JOYSTICK = 613;
        //
        // Summary:
        //     Some but not all pixels belonging to the window are covered by other windows.
        public const int GLUT_PARTIALLY_RETAINED = 2;
        //
        // Summary:
        //     Red color component.
        public const int GLUT_RED = 0;
        //
        // Summary:
        //     Gets GLUT's rendering context.
        public const int GLUT_RENDERING_CONTEXT = 509;
        //
        // Summary:
        //     An alias for Tao.FreeGlut.Glut.GLUT_RGBA.
        public const int GLUT_RGB = 0;
        //
        // Summary:
        //     Bit mask to select an RGBA mode window. This is the default if neither Tao.FreeGlut.Glut.GLUT_RGB,
        //     GLUT_RGBA, nor Tao.FreeGlut.Glut.GLUT_INDEX are specified.
        public const int GLUT_RGBA = 0;
        //
        // Summary:
        //     Right mouse button.
        public const int GLUT_RIGHT_BUTTON = 2;
        //
        // Summary:
        //     Height of the screen in millimeters. Zero indicates the height is unknown
        //     or not available.
        public const int GLUT_SCREEN_HEIGHT_MM = 203;
        //
        // Summary:
        //     Width of the screen in millimeters. Zero indicates the width is unknown or
        //     not available.
        public const int GLUT_SCREEN_WIDTH_MM = 202;
        //
        // Summary:
        //     Bit mask to select a single buffered window. This is the default if neither
        //     Tao.FreeGlut.Glut.GLUT_DOUBLE or GLUT_SINGLE are specified.
        public const int GLUT_SINGLE = 0;
        //
        // Summary:
        //     Bit mask to select a window with a stencil buffer.
        public const int GLUT_STENCIL = 32;
        //
        // Summary:
        //     Bit mask to select a stereo window.
        public const int GLUT_STEREO = 256;
        //
        // Summary:
        //     The transparent color index of the overlay of the current window; negative
        //     one is returned if no overlay is in use.
        public const int GLUT_TRANSPARENT_INDEX = 803;
        //
        // Summary:
        //     Mouse button up.
        public const int GLUT_UP = 1;
        //
        // Summary:
        //     Use current context when user opens a new window.
        public const int GLUT_USE_CURRENT_CONTEXT = 1;
        //
        // Summary:
        //     Gets GLUT version.
        public const int GLUT_VERSION = 508;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_HEIGHT = 909;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_HEIGHT_DELTA = 905;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_IN_USE = 901;
        //
        // Summary:
        //     Non-zero if video resizing is supported by the underlying system; zero if
        //     not supported. If this is zero, the other video resize GLUT calls do nothing
        //     when called.
        public const int GLUT_VIDEO_RESIZE_POSSIBLE = 900;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_WIDTH = 908;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_WIDTH_DELTA = 904;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_X = 906;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_X_DELTA = 902;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_Y = 907;
        //
        // Summary:
        //     Unknown. Unable to locate definitive documentation on this constant.
        public const int GLUT_VIDEO_RESIZE_Y_DELTA = 903;
        //
        // Summary:
        //     The window is visible. Does not distinguish a window being totally versus
        //     partially visible.
        public const int GLUT_VISIBLE = 1;
        //
        // Summary:
        //     Number of bits of alpha stored in the current window's accumulation buffer.
        //      Zero if the window is color index.
        public const int GLUT_WINDOW_ACCUM_ALPHA_SIZE = 114;
        //
        // Summary:
        //     Number of bits of blue stored in the current window's accumulation buffer.
        //      Zero if the window is color index.
        public const int GLUT_WINDOW_ACCUM_BLUE_SIZE = 113;
        //
        // Summary:
        //     Number of bits of green stored in the current window's accumulation buffer.
        //      Zero if the window is color index.
        public const int GLUT_WINDOW_ACCUM_GREEN_SIZE = 112;
        //
        // Summary:
        //     Number of bits of red stored in the current window's accumulation buffer.
        //      Zero if the window is color index.
        public const int GLUT_WINDOW_ACCUM_RED_SIZE = 111;
        //
        // Summary:
        //     Number of bits of alpha stored the current window's color buffer. Zero if
        //     the window is color index.
        public const int GLUT_WINDOW_ALPHA_SIZE = 110;
        //
        // Summary:
        //     Number of bits of blue stored the current window's color buffer. Zero if
        //     the window is color index.
        public const int GLUT_WINDOW_BLUE_SIZE = 109;
        //
        // Summary:
        //     Gets the window border width.
        public const int GLUT_WINDOW_BORDER_WIDTH = 506;
        //
        // Summary:
        //     Total number of bits for current window's color buffer. For an RGBA window,
        //     this is the sum of Tao.FreeGlut.Glut.GLUT_WINDOW_RED_SIZE, Tao.FreeGlut.Glut.GLUT_WINDOW_GREEN_SIZE,
        //     Tao.FreeGlut.Glut.GLUT_WINDOW_BLUE_SIZE, and Tao.FreeGlut.Glut.GLUT_WINDOW_ALPHA_SIZE.
        //     For color index windows, this is the size of the color indexes.
        public const int GLUT_WINDOW_BUFFER_SIZE = 104;
        //
        // Summary:
        //     Size of current window's color index colormap; zero for RGBA color model
        //     windows.
        public const int GLUT_WINDOW_COLORMAP_SIZE = 119;
        //
        // Summary:
        //     Current cursor for the current window.
        public const int GLUT_WINDOW_CURSOR = 122;
        //
        // Summary:
        //     Number of bits in the current window's depth buffer.
        public const int GLUT_WINDOW_DEPTH_SIZE = 106;
        //
        // Summary:
        //     One if the current window is double buffered, zero otherwise.
        public const int GLUT_WINDOW_DOUBLEBUFFER = 115;
        //
        // Summary:
        //     The window system dependent format ID for the current layer of the current
        //     window. On X11 GLUT implementations, this is the X visual ID. On Win32 GLUT
        //     implementations, this is the Win32 Pixel Format Descriptor number. This value
        //     is returned for debugging, benchmarking, and testing ease.
        public const int GLUT_WINDOW_FORMAT_ID = 123;
        //
        // Summary:
        //     Number of bits of green stored the current window's color buffer. Zero if
        //     the window is color index.
        public const int GLUT_WINDOW_GREEN_SIZE = 108;
        //
        // Summary:
        //     Gets window header height.
        public const int GLUT_WINDOW_HEADER_HEIGHT = 507;
        //
        // Summary:
        //     The number of subwindows the current window has (not counting children of
        //     children).
        public const int GLUT_WINDOW_NUM_CHILDREN = 118;
        //
        // Summary:
        //     Number of samples for multisampling for the current window.
        public const int GLUT_WINDOW_NUM_SAMPLES = 120;
        //
        // Summary:
        //     The window number of the current window's parent; zero if the window is a
        //     top-level window.
        public const int GLUT_WINDOW_PARENT = 117;
        //
        // Summary:
        //     Number of bits of red stored the current window's color buffer. Zero if the
        //     window is color index.
        public const int GLUT_WINDOW_RED_SIZE = 107;
        //
        // Summary:
        //     One if the current window is RGBA mode, zero otherwise (i.e., color index).
        public const int GLUT_WINDOW_RGBA = 116;
        //
        // Summary:
        //     Number of bits in the current window's stencil buffer.
        public const int GLUT_WINDOW_STENCIL_SIZE = 105;
        //
        // Summary:
        //     One if the current window is stereo, zero otherwise.
        public const int GLUT_WINDOW_STEREO = 121;

        // Summary:
        //     A fixed width font with every character fitting in an 8 by 13 pixel rectangle.
        public static readonly IntPtr GLUT_BITMAP_8_BY_13;
        //
        // Summary:
        //     A fixed width font with every character fitting in an 9 by 15 pixel rectangle.
        public static readonly IntPtr GLUT_BITMAP_9_BY_15;
        //
        // Summary:
        //     A 10-point proportional spaced Helvetica font.
        public static readonly IntPtr GLUT_BITMAP_HELVETICA_10;
        //
        // Summary:
        //     A 12-point proportional spaced Helvetica font.
        public static readonly IntPtr GLUT_BITMAP_HELVETICA_12;
        //
        // Summary:
        //     A 18-point proportional spaced Helvetica font.
        public static readonly IntPtr GLUT_BITMAP_HELVETICA_18;
        //
        // Summary:
        //     A 10-point proportional spaced Times Roman font.
        public static readonly IntPtr GLUT_BITMAP_TIMES_ROMAN_10;
        //
        // Summary:
        //     A 24-point proportional spaced Times Roman font.
        public static readonly IntPtr GLUT_BITMAP_TIMES_ROMAN_24;
        //
        // Summary:
        //     A mono-spaced spaced Roman Simplex font (same characters as Tao.FreeGlut.Glut.GLUT_STROKE_ROMAN)
        //     for ASCII characters 32 through 127. The maximum top character in the font
        //     is 119.05 units; the bottom descends 33.33 units. Each character is 104.76
        //     units wide.
        public static readonly IntPtr GLUT_STROKE_MONO_ROMAN;
        //
        // Summary:
        //     A proportionally spaced Roman Simplex font for ASCII characters 32 through
        //     127. The maximum top character in the font is 119.05 units; the bottom descends
        //     33.33 units.
        public static readonly IntPtr GLUT_STROKE_ROMAN;

        public enum GlutStates
        {
            /// <summary>
            /// X location in pixels (relative to the screen origin) of the current window.
            /// </summary>
            WindowX = 100,
            /// <summary>
            /// Y location in pixels (relative to the screen origin) of the current window.
            /// </summary>
            WindowY = 101,
            /// <summary>
            /// Width of the screen in pixels. Zero indicates the width is unknown or not available.
            /// </summary>
            WindowWidth = 102,
            /// <summary>
            /// Height of the screen in pixels. Zero indicates the width is unknown or not available.
            /// </summary>
            WindowHeight = 103,
            ScreenWidth = 200,
            /// <summary>
            /// Height of the screen in pixels. Zero indicates the height is unknown or not available.
            /// </summary>
            ScreenHeight = 201,
            /// <summary>
            /// The X value of the initial window position.
            /// </summary>
            InitialWindowX = 500,
            /// <summary>
            /// The Y value of the initial window position.
            /// </summary>
            InitialWindowY = 501,
            /// <summary>
            /// The width value of the initial window size.
            /// </summary>
            InitialWindowWidth = 502,
            /// <summary>
            /// The height value of the initial window size.
            /// </summary>
            InitialWindowHeight = 503,
        }
    }
}

