using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Methods
{
	internal static class User32
	{
		/// <summary>
		/// The CreateWindowEx function creates an overlapped, pop-up, or child window with an extended window style; otherwise, this function is identical to the CreateWindow function.
		/// </summary>
		/// <param name="dwExStyle">Specifies the extended window style of the window being created.</param>
		/// <param name="lpClassName">Pointer to a null-terminated string or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero. If lpClassName is a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, provided that the module that registers the class is also the module that creates the window. The class name can also be any of the predefined system class names.</param>
		/// <param name="lpWindowName">Pointer to a null-terminated string that specifies the window name. If the window style specifies a title bar, the window title pointed to by lpWindowName is displayed in the title bar. When using CreateWindow to create controls, such as buttons, check boxes, and static controls, use lpWindowName to specify the text of the control. When creating a static control with the SS_ICON style, use lpWindowName to specify the icon name or identifier. To specify an identifier, use the syntax "#num". </param>
		/// <param name="dwStyle">Specifies the style of the window being created. This parameter can be a combination of window styles, plus the control styles indicated in the Remarks section.</param>
		/// <param name="x">Specifies the initial horizontal position of the window. For an overlapped or pop-up window, the x parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates. For a child window, x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent window's client area. If x is set to CW_USEDEFAULT, the system selects the default position for the window's upper-left corner and ignores the y parameter. CW_USEDEFAULT is valid only for overlapped windows; if it is specified for a pop-up or child window, the x and y parameters are set to zero.</param>
		/// <param name="y">Specifies the initial vertical position of the window. For an overlapped or pop-up window, the y parameter is the initial y-coordinate of the window's upper-left corner, in screen coordinates. For a child window, y is the initial y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's client area. For a list box y is the initial y-coordinate of the upper-left corner of the list box's client area relative to the upper-left corner of the parent window's client area.
		/// <para>If an overlapped window is created with the WS_VISIBLE style bit set and the x parameter is set to CW_USEDEFAULT, then the y parameter determines how the window is shown. If the y parameter is CW_USEDEFAULT, then the window manager calls ShowWindow with the SW_SHOW flag after the window has been created. If the y parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow parameter.</para></param>
		/// <param name="nWidth">Specifies the width, in device units, of the window. For overlapped windows, nWidth is the window's width, in screen coordinates, or CW_USEDEFAULT. If nWidth is CW_USEDEFAULT, the system selects a default width and height for the window; the default width extends from the initial x-coordinates to the right edge of the screen; the default height extends from the initial y-coordinate to the top of the icon area. CW_USEDEFAULT is valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, the nWidth and nHeight parameter are set to zero.</param>
		/// <param name="nHeight">Specifies the height, in device units, of the window. For overlapped windows, nHeight is the window's height, in screen coordinates. If the nWidth parameter is set to CW_USEDEFAULT, the system ignores nHeight.</param> <param name="hWndParent">Handle to the parent or owner window of the window being created. To create a child window or an owned window, supply a valid window handle. This parameter is optional for pop-up windows.
		/// <para>Windows 2000/XP: To create a message-only window, supply HWND_MESSAGE or a handle to an existing message-only window.</para></param>
		/// <param name="hMenu">Handle to a menu, or specifies a child-window identifier, depending on the window style. For an overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be NULL if the class menu is to be used. For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog box control to notify its parent about events. The application determines the child-window identifier; it must be unique for all child windows with the same parent window.</param>
		/// <param name="hInstance">Handle to the instance of the module to be associated with the window.</param> <param name="lpParam">Pointer to a value to be passed to the window through the CREATESTRUCT structure (lpCreateParams member) pointed to by the lParam param of the WM_CREATE message. This message is sent to the created window by this function before it returns.
		/// <para>If an application calls CreateWindow to create a MDI client window, lpParam should point to a CLIENTCREATESTRUCT structure. If an MDI client window calls CreateWindow to create an MDI child window, lpParam should point to a MDICREATESTRUCT structure. lpParam may be NULL if no additional data is needed.</para></param>
		/// <returns>If the function succeeds, the return value is a handle to the new window.
		/// <para>If the function fails, the return value is NULL. To get extended error information, call GetLastError.</para>
		/// <para>This function typically fails for one of the following reasons:</para>
		/// <list type="">
		/// <item>an invalid parameter value</item>
		/// <item>the system class was registered by a different module</item>
		/// <item>The WH_CBT hook is installed and returns a failure code</item>
		/// <item>if one of the controls in the dialog template is not registered, or its window window procedure fails WM_CREATE or WM_NCCREATE</item>
		/// </list></returns>

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr CreateWindowEx
		(
			Constants.User32.WindowStylesEx dwExStyle,
			string lpClassName,
			string lpWindowName,
			Constants.User32.WindowStyles dwStyle,
			int x,
			int y,
			int nWidth,
			int nHeight,
			IntPtr hWndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam
		);

		/// <summary>
		/// Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a posted message is available for retrieval.
		/// </summary>
		/// <param name="lpMsg">A pointer to an <see cref="Structures.MSG"/> structure that receives message information from the thread's message queue.</param>
		/// <param name="hWnd">
		///	A handle to the window whose messages are to be retrieved. The window must belong to the current thread.
		///	
		/// If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the current thread, and any messages
		/// on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL,
		/// both window messages and thread messages are processed.
		/// 
		/// If hWnd is -1, GetMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL,
		/// that is, thread messages as posted by <see cref="PostMessage" /> (when the hWnd parameter is NULL) or
		/// <see cref="PostThreadMessage" />.
		/// </param>
		/// <param name="wMsgFilterMin">
		/// The integer value of the lowest message value to be retrieved. Use WM_KEYFIRST (0x0100) to specify the first
		/// keyboard message or WM_MOUSEFIRST (0x0200) to specify the first mouse message.
		/// 
		/// Use WM_INPUT here and in wMsgFilterMax to specify only the WM_INPUT messages.
		/// 
		/// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range filtering is performed).
		/// </param>
		/// <param name="wMsgFilterMax">
		/// The integer value of the highest message value to be retrieved. Use WM_KEYLAST to specify the last keyboard message or WM_MOUSELAST to specify the last mouse message.
		/// 
		/// Use WM_INPUT here and in wMsgFilterMin to specify only the WM_INPUT messages.
		/// 
		/// If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range filtering is performed).
		/// </param>
		/// <returns>
		/// If the function retrieves a message other than WM_QUIT, the return value is nonzero.
		/// 
		/// If the function retrieves the WM_QUIT message, the return value is zero.
		/// 
		/// If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle
		/// or lpMsg is an invalid pointer. To get extended error information, call GetLastError.
		/// </returns>
		[DllImport("user32.dll")]
		public static extern int GetMessage
		(
			[Out()] out Structures.User32.MSG lpMsg,
			[In()] [Optional()] IntPtr hWnd,
			[In()] uint wMsgFilterMin,
			[In()] uint wMsgFilterMax
		);

		[DllImport("user32.dll")]
		public static extern IntPtr RegisterClassEx
		(
			ref Structures.User32.WNDCLASSEX lpwcx
		);

		/// <summary>
		/// Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the GetMessage function.
		/// </summary>
		/// <param name="lpMsg">A pointer to a structure that contains the message.</param>
		/// <returns>
		/// The return value specifies the value returned by the window procedure. Although its meaning depends on the message
		/// being dispatched, the return value generally is ignored.
		/// </returns>
		/// <remarks>
		/// The MSG structure must contain valid message values. If the lpmsg parameter points to a WM_TIMER message and the
		/// lParam parameter of the WM_TIMER message is not NULL, lParam points to a function that is called instead of the
		/// window procedure.
		/// 
		/// Note that the application is responsible for retrieving and dispatching input messages to the dialog box. Most
		/// applications use the main message loop for this. However, to permit the user to move to and to select controls by
		/// using the keyboard, the application must call IsDialogMessage. For more information, see Dialog Box Keyboard
		/// Interface.
		/// </remarks>
		[DllImport("user32.dll")]
		public static extern bool DispatchMessage
		(
			ref Structures.User32.MSG lpMsg
		);
		/// <summary>
		/// Translates virtual-key messages into character messages. The character messages are posted to the calling thread's
		/// message queue, to be read the next time the thread calls the <see cref="GetMessage" /> or
		/// <see cref="PeekMessage" /> function.
		/// </summary>
		/// <param name="lpMsg">
		/// A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue
		/// by using the <see cref="GetMessage" /> or <see cref="PeekMessage" /> function.
		/// </param>
		/// <returns>
		/// If the message is translated (that is, a character message is posted to the thread's message queue), the return
		/// value is nonzero.
		/// 
		/// If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is nonzero, regardless of
		/// the translation.
		/// 
		/// If the message is not translated (that is, a character message is not posted to the thread's message queue), the
		/// return value is zero.
		/// </returns>
		/// <remarks>
		/// The TranslateMessage function does not modify the message pointed to by the lpMsg parameter.
		/// 
		/// WM_KEYDOWN and WM_KEYUP combinations produce a WM_CHAR or WM_DEADCHAR message. WM_SYSKEYDOWN and WM_SYSKEYUP
		/// combinations produce a WM_SYSCHAR or WM_SYSDEADCHAR message.
		/// 
		/// TranslateMessage produces WM_CHAR messages only for keys that are mapped to ASCII characters by the keyboard
		/// driver.
		/// 
		/// If applications process virtual-key messages for some other purpose, they should not call TranslateMessage. For
		/// instance, an application should not call TranslateMessage if the TranslateAccelerator function returns a nonzero
		/// value. Note that the application is responsible for retrieving and dispatching input messages to the dialog box.
		/// Most applications use the main message loop for this. However, to permit the user to move to and to select
		/// controls by using the keyboard, the application must call IsDialogMessage. For more information, see Dialog Box
		/// Keyboard Interface.
		/// </remarks>
		[DllImport("user32.dll")]
		public static extern bool TranslateMessage
		(
			ref Structures.User32.MSG lpMsg
		);

		[DllImport("user32.dll")]
		public static extern int DefWindowProc(IntPtr hwnd, Constants.User32.WindowMessages uMsg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen. You can use the returned handle in
		/// subsequent GDI functions to draw in the DC. The device context is an opaque data structure, whose values are used internally by GDI.
		/// 
		/// The GetDCEx function is an extension to GetDC, which gives an application more control over how and whether clipping occurs in the client area.
		/// </summary>
		/// <param name="hWnd">A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen.</param>
		/// <returns>
		/// If the function succeeds, the return value is a handle to the DC for the specified window's client area.
		/// 
		/// If the function fails, the return value is NULL.
		/// </returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetSysColorBrush(Constants.User32.SystemColors nIndex);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hInstance">A handle to an instance of the module whose executable file contains the cursor to be loaded.</param>
		/// <param name="lpCursorName">
		/// The name of the cursor resource to be loaded. Alternatively, this parameter can consist of the resource identifier in the low-order word and zero in the high-order
		/// word. The MAKEINTRESOURCE macro can also be used to create this value. To use one of the predefined cursors, the application must set the <see cref="hInstance"/>
		/// parameter to NULL and the lpCursorName parameter to one the following values.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursor([In(), Optional()] IntPtr hInstance, [In()] string lpCursorName);
		[DllImport("user32.dll")]
		public static extern IntPtr LoadCursor([In(), Optional()] IntPtr hInstance, [In()] Internal.Windows.Constants.User32.Cursors lpCursorName);

		/// <summary>
		/// Displays a modal dialog box that contains a system icon, a set of buttons, and a brief application-specific message, such as status or error information. The message
		/// box returns an integer value that indicates which button the user clicked.
		/// </summary>
		/// <param name="hWnd">A handle to the owner window of the message box to be created. If this parameter is NULL, the message box has no owner window.</param>
		/// <param name="lpText">
		/// The message to be displayed. If the string consists of more than one line, you can separate the lines using a carriage return and/or linefeed character between each
		/// line.
		/// </param>
		/// <param name="lpCaption">The dialog box title. If this parameter is NULL, the default title is Error.</param>
		/// <param name="uType">The contents and behavior of the dialog box. This parameter can be a combination of flags from the <see cref="Internal.Windows.Constants.User32.MessageDialogStyles"/> groups of flags.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern Constants.User32.MessageDialogResponses MessageBox([In(), Optional()] IntPtr hWnd, [In(), Optional()] string lpText, [In(), Optional()] string lpCaption, [In()] Internal.Windows.Constants.User32.MessageDialogStyles uType);

		/// <summary>
		/// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window
		/// procedure has processed the message.
		/// 
		/// To send a message and return immediately, use the SendMessageCallback or SendNotifyMessage function. To post a message to a thread's message queue and return
		/// immediately, use the PostMessage or PostThreadMessage function.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level
		/// windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.
		/// 
		/// Message sending is subject to UIPI. The thread of a process can send messages only to message queues of threads in processes of lesser or equal integrity level.
		/// </param>
		/// <param name="Msg">The message to be sent.</param>
		/// <param name="wParam">Additional message-specific information.</param>
		/// <param name="lParam">Additional message-specific information.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int SendMessage([In()] IntPtr hWnd, [In()] Constants.User32.WindowMessages Msg, [In()] IntPtr wParam, [In()] IntPtr lParam);

		/// <summary>
		/// The EnumDisplayMonitors function enumerates display monitors (including invisible pseudo-monitors associated with the mirroring drivers) that intersect a region
		/// formed by the intersection of a specified clipping rectangle and the visible region of a device context. EnumDisplayMonitors calls an application-defined
		/// MonitorEnumProc callback function once for each monitor that is enumerated. Note that GetSystemMetrics (SM_CMONITORS) counts only the display monitors.
		/// </summary>
		/// <param name="hdc">
		/// A handle to a display device context that defines the visible region of interest.
		/// 
		/// If this parameter is NULL, the hdcMonitor parameter passed to the callback function will be NULL, and the visible region of interest is the virtual screen that
		/// encompasses all the displays on the desktop.
		/// </param>
		/// <param name="lprcClip">
		/// A pointer to a RECT structure that specifies a clipping rectangle. The region of interest is the intersection of the clipping rectangle with the visible region
		/// specified by hdc.
		/// 
		/// If hdc is non-NULL, the coordinates of the clipping rectangle are relative to the origin of the hdc. If hdc is NULL, the coordinates are virtual-screen coordinates.
		/// 
		/// This parameter can be NULL if you don't want to clip the region specified by hdc.
		/// </param>
		/// <param name="lpfnEnum">A pointer to a MonitorEnumProc application-defined callback function.</param>
		/// <param name="dwData">Application-defined data that EnumDisplayMonitors passes directly to the MonitorEnumProc function.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool EnumDisplayMonitors([In()] IntPtr hdc, ref Structures.User32.RECT lprcClip, [In()] Delegates.MonitorEnumProc lpfnEnum, [In()] IntPtr dwData);

		/// <summary>
		/// The GetMonitorInfo function retrieves information about a display monitor.
		/// </summary>
		/// <param name="hMonitor">A handle to the display monitor of interest.</param>
		/// <param name="lpmi">
		/// A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.
		/// 
		/// You must set the cbSize member of the structure to sizeof(MONITORINFO) or sizeof(MONITORINFOEX) before calling the GetMonitorInfo function. Doing so lets the function
		/// determine the type of structure you are passing to it.
		/// 
		/// The MONITORINFOEX structure is a superset of the MONITORINFO structure. It has one additional member: a string that contains a name for the display monitor. Most
		/// applications have no use for a display monitor name, and so can save some bytes by using a MONITORINFO structure.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// c
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport("user32.dll")]
		public static extern bool GetMonitorInfo([In()] IntPtr hMonitor, ref Structures.User32.MONITORINFOEX lpmi);

		/// <summary>
		/// Indicates to the system that a thread has made a request to terminate (quit). It is typically used in response to a WM_DESTROY message.
		/// </summary>
		/// <param name="exitCode">The application exit code. This value is used as the wParam parameter of the WM_QUIT message.</param>
		[DllImport("user32.dll")]
		public static extern void PostQuitMessage(int exitCode);

		[DllImport("user32.dll")]
		public static extern void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

		/// <summary>
		/// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen
		/// coordinates that are relative to the upper-left corner of the screen.
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="rect"></param>
		[DllImport("user32.dll")]
		public static extern void GetWindowRect(IntPtr hWnd, ref Structures.User32.RECT rect);


		/// <summary>
		/// Fills a rectangle by using the specified brush. This function includes the left and top borders, but excludes the right and bottom borders of the rectangle.
		/// </summary>
		/// <param name="hdc">A handle to the device context.</param>
		/// <param name="rect">A pointer to a <see cref="Structures.User32.RECT" /> structure that contains the logical coordinates of the rectangle to be filled.</param>
		/// <param name="hbr">A handle to the brush used to fill the rectangle.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// 
		/// If the function fails, the return value is zero.
		/// </returns>
		/// <remarks>
		/// The brush identified by the hbr parameter may be either a handle to a logical brush or a color value. If specifying a handle to a logical brush, call one of the
		/// following functions to obtain the handle: CreateHatchBrush, CreatePatternBrush, or CreateSolidBrush. Additionally, you may retrieve a handle to one of the stock
		/// brushes by using the GetStockObject function. If specifying a color value for the hbr parameter, it must be one of the standard system colors (the value 1 must be
		/// added to the chosen color). For example: (COLOR_WINDOW + 1)
		/// 
		/// When filling the specified rectangle, FillRect does not include the rectangle's right and bottom sides. GDI fills a rectangle up to, but not including, the right
		/// column and bottom row, regardless of the current mapping mode.
		/// </remarks>
		[DllImport("user32.dll")]
		public static extern int FillRect(IntPtr hdc, ref Structures.User32.RECT rect, ref IntPtr hbr);

		/// <summary>
		/// Updates the client area of the specified window by sending a WM_PAINT message to the window if the window's update
		/// region is not empty. The function sends a WM_PAINT message directly to the window procedure of the specified
		/// window, bypassing the application queue. If the update region is empty, no message is sent.
		/// </summary>
		/// <param name="handle">Handle to the window to be updated.</param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// 
		/// If the function fails, the return value is zero.
		/// </returns>
		[DllImport("user32.dll")]
		public static extern bool UpdateWindow([In()] IntPtr handle);

		/// <summary>
		/// Adds a rectangle to the specified window's update region. The update region represents the portion of the window's
		/// client area that must be redrawn.
		/// </summary>
		/// <param name="hWnd">
		/// A handle to the window whose update region has changed. If this parameter is NULL, the system invalidates and
		/// redraws all windows, not just the windows for this application, and sends the WM_ERASEBKGND and WM_NCPAINT
		/// messages before the function returns. Setting this parameter to NULL is not recommended.
		/// </param>
		/// <param name="lpRect">
		/// A pointer to a RECT structure that contains the client coordinates of the rectangle to be added to the update
		/// region. If this parameter is NULL, the entire client area is added to the update region.
		/// </param>
		/// <param name="bErase">
		/// Specifies whether the background within the update region is to be erased when the update region is processed. If
		/// this parameter is TRUE, the background is erased when the BeginPaint function is called. If this parameter is
		/// FALSE, the background remains unchanged.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is nonzero.
		/// 
		/// If the function fails, the return value is zero.
		/// </returns>
		/// <remarks>
		/// The invalidated areas accumulate in the update region until the region is processed when the next WM_PAINT message
		/// occurs or until the region is validated by using the ValidateRect or ValidateRgn function.
		/// 
		/// The system sends a WM_PAINT message to a window whenever its update region is not empty and there are no other
		/// messages in the application queue for that window.
		/// 
		/// If the bErase parameter is TRUE for any part of the update region, the background is erased in the entire region,
		/// not just in the specified part.
		/// </remarks>
		[DllImport("user32.dll")]
		public static extern bool InvalidateRect([In()] IntPtr hWnd, ref Structures.User32.RECT lpRect, [In()] bool bErase);
	}
}
