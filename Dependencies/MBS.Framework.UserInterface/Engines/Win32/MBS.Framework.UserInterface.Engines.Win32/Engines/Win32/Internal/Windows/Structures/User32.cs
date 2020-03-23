using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Structures
{
	internal static class User32
	{
		/// <summary>
		/// Contains message information from a thread's message queue.
		/// </summary>
		public struct MSG
		{
			/// <summary>
			/// A handle to the window whose window procedure receives the message. This member is NULL when the message is a
			/// thread message.
			/// </summary>
			public IntPtr hwnd;
			/// <summary>
			/// The message identifier. Applications can only use the low word; the high word is reserved by the system.
			/// </summary>
			public Constants.User32.WindowMessages message;
			/// <summary>
			/// Additional information about the message. The exact meaning depends on the value of the message member.
			/// </summary>
			public IntPtr wParam;
			/// <summary>
			/// Additional information about the message. The exact meaning depends on the value of the message member.
			/// </summary>
			public IntPtr lParam;
			/// <summary>
			/// The time at which the message was posted.
			/// </summary>
			public uint time;
			/// <summary>
			/// The cursor position, in screen coordinates, when the message was posted.
			/// </summary>
			public POINT pt;
		}

		public struct POINT
		{
			public int x;
			public int y;
		}

		public struct WNDCLASSEX
		{
			public uint cbSize;
			public uint style;
			public Delegates.WindowProc lpfnWndProc;
			public int cbClsExtra;
			public int cbWndExtra;
			public IntPtr hInstance;
			public IntPtr hIcon;
			public IntPtr hCursor;
			public IntPtr hbrBackground;
			public string lpszMenuName;
			public string lpszClassName;
			public IntPtr hIconSm;
		};

		[StructLayout(LayoutKind.Sequential)]
		public struct COLORREF
		{
			public byte a;
			public byte b;
			public byte g;
			public byte r;
		}

		/// <summary>
		/// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
		/// </summary>
		/// <remarks>
		/// By convention, the right and bottom edges of the rectangle are normally considered exclusive. In other words, the pixel whose coordinates are ( right, bottom ) lies
		/// immediately outside of the rectangle. For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including, the right column and
		/// bottom row of pixels. This structure is identical to the RECTL structure.
		/// </remarks>
		public struct RECT
		{
			/// <summary>
			/// The x-coordinate of the upper-left corner of the rectangle.
			/// </summary>
			public int left;
			/// <summary>
			/// The y-coordinate of the upper-left corner of the rectangle.
			/// </summary>
			public int top;
			/// <summary>
			/// The x-coordinate of the lower-right corner of the rectangle.
			/// </summary>
			public int right;
			/// <summary>
			/// The y-coordinate of the lower-right corner of the rectangle.
			/// </summary>
			public int bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}

		public struct MONITORINFOEX
		{
			public int cbSize;
			public RECT rcMonitor;
			public RECT rcWork;
			public int dwFlags;
			public string szDevice /* [CCHDEVICENAME] */;
		}
	}
}
