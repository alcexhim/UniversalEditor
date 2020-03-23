using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows
{
	internal static class Delegates
	{
		public delegate int WindowProc
		(
			[In()] IntPtr hwnd,
			[In()] Internal.Windows.Constants.User32.WindowMessages uMsg,
			[In()] IntPtr wParam,
			[In()] IntPtr lParam
		);

		/// <summary>
		/// A MonitorEnumProc function is an application-defined callback function that is called by the EnumDisplayMonitors function.
		/// 
		/// A value of type MONITORENUMPROC is a pointer to a MonitorEnumProc function.
		/// </summary>
		/// <param name="hMonitor">A handle to the display monitor. This value will always be non-NULL.</param>
		/// <param name="hdcMonitor">
		/// A handle to a device context.
		/// 
		/// The device context has color attributes that are appropriate for the display monitor identified by hMonitor. The clipping area of the device context is set to the
		/// intersection of the visible region of the device context identified by the hdc parameter of <see cref="Methods.User32.EnumDisplayMonitors" />, the rectangle pointed to
		/// by the lprcClip parameter of <see cref="Methods.User32.EnumDisplayMonitors" />, and the display monitor rectangle.
		/// 
		/// This value is NULL if the hdc parameter of EnumDisplayMonitors was NULL.
		/// </param>
		/// <param name="lprcMonitor"></param>
		/// <param name="dwData"></param>
		/// <returns></returns>
		public delegate bool MonitorEnumProc
		(
			[In()] IntPtr hMonitor,
			[In()] IntPtr hdcMonitor,
			[In()] Structures.User32.RECT lprcMonitor,
			[In()] IntPtr dwData
		);
	}
}
