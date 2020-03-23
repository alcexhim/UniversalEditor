using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Methods
{
	internal static class GDI
	{
		[DllImport("gdi32.dll")]
		public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

		[DllImport("gdi32.dll")]
		public static extern bool MoveToEx(IntPtr hdc, int X, int Y, ref Structures.User32.POINT lpPoint);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreatePen(Constants.GDI.PenStyles fnPenStyle, int nWidth, Structures.User32.COLORREF crColor);

		/// <summary>
		/// The CreateFontIndirect function creates a logical font that has the specified characteristics. The font can subsequently be selected as the current font for any device
		/// context.
		/// </summary>
		/// <param name="lplf">A pointer to a <see cref="Structures.GDI.LOGFONT" /> structure that defines the characteristics of the logical font.</param>
		/// <returns></returns>
		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateFontIndirect(ref Structures.GDI.LOGFONT lplf);

		/// <summary>
		/// The GetDeviceCaps function retrieves device-specific information for the specified device.
		/// </summary>
		/// <param name="hdc">A handle to the DC.</param>
		/// <param name="nIndex">The item to be returned.</param>
		/// <returns>
		/// The return value specifies the value of the desired item.
		/// 
		/// When nIndex is BITSPIXEL and the device has 15bpp or 16bpp, the return value is 16.
		/// </returns>
		/// <remarks>
		/// When nIndex is SHADEBLENDCAPS:
		/// 
		/// * For a printer, GetDeviceCaps returns whatever the printer reports.
		/// * For a display device, all blending operations are available; besides SB_NONE, the only return values are SB_CONST_ALPHA and SB_PIXEL_ALPHA, which indicate whether
		///   these operations are accelerated.
		///   
		/// On a multiple monitor system, if hdc is the desktop, GetDeviceCaps returns the capabilities of the primary monitor. If you want info for other monitors, you must use
		/// the multi-monitor APIs or CreateDC to get a HDC for the device context (DC) of a specific monitor. 
		/// </remarks>
		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps([In()] IntPtr hdc, [In()] Constants.GDI.DeviceCapsIndex nIndex);
	}
}
