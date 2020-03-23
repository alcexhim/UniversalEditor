using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.System.Windows
{
    public static class Constants
    {
        public const int BITSPIXEL = 12;
        public const uint PFD_TYPE_RGBA = 0;
        public const uint PFD_TYPE_COLORINDEX = 1;
        public const int PFD_MAIN_PLANE = 0;
        public const int PFD_OVERLAY_PLANE = 1;
        public const int PFD_UNDERLAY_PLANE = -1;
        public const uint PFD_DOUBLEBUFFER = 0x00000001;
        public const uint PFD_STEREO = 0x00000002;
        public const uint PFD_DRAW_TO_WINDOW = 0x00000004;
        public const uint PFD_DRAW_TO_BITMAP = 0x00000008;
        public const uint PFD_SUPPORT_GDI = 0x00000010;
        public const uint PFD_SUPPORT_OPENGL = 0x00000020;
        public const uint PFD_GENERIC_FORMAT = 0x00000040;
        public const uint PFD_NEED_PALETTE = 0x00000080;
        public const uint PFD_NEED_SYSTEM_PALETTE = 0x00000100;
        public const uint PFD_SWAP_EXCHANGE = 0x00000200;
        public const uint PFD_SWAP_COPY = 0x00000400;
        public const uint PFD_SWAP_LAYER_BUFFERS = 0x00000800;
        public const uint PFD_GENERIC_ACCELERATED = 0x00001000;
        public const uint PFD_SUPPORT_DIRECTDRAW = 0x00002000;
        public const uint PFD_DEPTH_DONTCARE = 0x20000000;
        public const uint PFD_DOUBLEBUFFER_DONTCARE = 0x40000000;
        public const uint PFD_STEREO_DONTCARE = 0x80000000;

        public const uint WM_SETICON = 0x80;

        public const uint ICON_SMALL = 0;
        public const uint ICON_BIG = 1;

        [Flags()]
        public enum WindowStyles : int
        {
            Overlapped = 0x00000000,
            /// <summary>
            /// 
            /// </summary>
            Caption = 0x00C00000,
            MaximizeBox = 0x00010000,
            MinimizeBox = 0x00020000,
            /// <summary>
            /// The window has a sizing border.
            /// </summary>
            ThickFrame = 0x00040000,
            /// <summary>
            /// The window has a window menu on its title bar. The <see cref="Caption" /> style must also be specified.
            /// </summary>
            SystemMenu = 0x00080000,
            OverlappedWindow = (Overlapped | Caption | SystemMenu | ThickFrame | MinimizeBox | MaximizeBox)
        }

        [Flags()]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>
            /// No flags are specified.
            /// </summary>
            None = 0x0000,
            /// <summary>
            /// Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            RetainCurrentSize = 0x0001,
            /// <summary>
            /// Retains the current position (ignores X and Y parameters).
            /// </summary>
            RetainCurrentPosition = 0x0002,
            /// <summary>
            /// Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            RetainCurrentZOrder = 0x0004,
            /// <summary>
            /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area
            /// (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When
            /// this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            NoRedraw = 0x0008,
            /// <summary>
            /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost
            /// group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            NoActivate = 0x0010,
            /// <summary>
            /// Draws a frame (defined in the window's class description) around the window. Applies new frame styles set using the SetWindowLong
            /// function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified,
            /// WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            DrawFrame = 0x0020,
            /// <summary>
            /// Displays the window.
            /// </summary>
            ShowWindow = 0x0040,
            /// <summary>
            /// Hides the window.
            /// </summary>
            HideWindow = 0x0080,
            /// <summary>
            /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and
            /// copied back into the client area after the window is sized or repositioned.
            /// </summary>
            NoCopyBits = 0x0100,
            /// <summary>
            /// Does not change the owner window's position in the Z order.
            /// </summary>
            NoReposition = 0x0200,
            /// <summary>
            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            InhibitWindowPosChanging = 0x0400,
            /// <summary>
            /// Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            DeferErase = 0x2000,
            /// <summary>
            /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the
            /// thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            Asynchronous = 0x4000
        }

        public enum WindowLongIndex
        {
            /// <summary>
            /// Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is
            /// initially zero.
            /// </summary>
            UserData = -21,
            /// <summary>
            /// Sets a new extended window style.
            /// </summary>
            ExtendedWindowStyle = -20,
            /// <summary>
            /// Sets a new window style.
            /// </summary>
            WindowStyle = -16,
            /// <summary>
            /// Sets a new identifier of the child window. The window cannot be a top-level window.
            /// </summary>
            ChildWindowIdentifier = -12,
            /// <summary>
            /// Sets a new application instance handle.
            /// </summary>
            InstanceHandle = -6,
            /// <summary>
            /// Sets a new address for the window procedure. You cannot change this attribute if the window does not belong to the same process as the
            /// calling thread.
            /// </summary>
            WindowProcedure = -4
        }
    }
}
