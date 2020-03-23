using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Constants
{
	internal static class GDI
	{
		public enum PenStyles
		{
			/// <summary>
			/// The pen is solid.
			/// </summary>
			Solid = 0x00,
			/// <summary>
			/// The pen is dashed. This style is valid only when the pen width is one or less in device units.
			/// </summary>
			Dash = 0x01,
			/// <summary>
			/// The pen is dotted. This style is valid only when the pen width is one or less in device units.
			/// </summary>
			Dot = 0x02,
			/// <summary>
			/// The pen has alternating dashes and dots. This style is valid only when the pen width is one or less in device units.
			/// </summary>
			DashDot = 0x03,
			/// <summary>
			/// The pen has alternating dashes and double dots. This style is valid only when the pen width is one or less in device units.
			/// </summary>
			DashDotDot = 0x04,
			/// <summary>
			/// The pen is invisible.
			/// </summary>
			None = 0x05,
			/// <summary>
			/// The pen is solid. When this pen is used in any GDI drawing function that takes a bounding rectangle, the dimensions of the figure are shrunk so that it fits
			/// entirely in the bounding rectangle, taking into account the width of the pen. This applies only to geometric pens.
			/// </summary>
			InsideFrame = 0x06
		}

		public enum DeviceCapsIndex : int
		{
			/// <summary>
			/// The device driver version.
			/// </summary>
			DriverVersion = 0,
			/// <summary>
			/// Device technology.
			/// </summary>
			Technology = 2,
			/// <summary>
			/// Width, in millimeters, of the physical screen.
			/// </summary>
			HorizontalSize = 4,
			/// <summary>
			/// Height, in millimeters, of the physical screen.
			/// </summary>
			VerticalSize = 6,
			/// <summary>
			/// Width, in pixels, of the screen; or for printers, the width, in pixels, of the printable area of the page.
			/// </summary>
			HorizontalResolution = 8,
			/// <summary>
			/// Height, in raster lines, of the screen; or for printers, the height, in pixels, of the printable area of the page.
			/// </summary>
			VerticalResolution = 10,
			/// <summary>
			/// Number of adjacent color bits for each pixel.
			/// </summary>
			BitsPerPixel = 12,
			/// <summary>
			/// Number of color planes.
			/// </summary>
			Planes = 14,
			/// <summary>
			/// Number of device-specific brushes.
			/// </summary>
			Brushes = 16,
			/// <summary>
			/// Number of device-specific pens.
			/// </summary>
			Pens = 18,
			NUMMARKERS     = 20 ,   // Number of markers the device has
			/// <summary>
			/// Number of device-specific fonts.
			/// </summary>
			Fonts = 22,
			/// <summary>
			/// Number of entries in the device's color table, if the device has a color depth of no more than 8 bits per pixel. For devices with greater color depths, 1 is
			/// returned.
			/// </summary>
			Colors = 24,
			PDEVICESIZE    = 26 ,   // Size required for device descriptor
			/// <summary>
			/// Value that indicates the curve capabilities of the device.
			/// </summary>
			CurveCaps = 28,
			/// <summary>
			/// Value that indicates the line capabilities of the device.
			/// </summary>
			LineCaps = 30,
			/// <summary>
			/// Value that indicates the polygon capabilities of the device.
			/// </summary>
			PolygonalCaps = 32,
			/// <summary>
			/// Value that indicates the text capabilities of the device.
			/// </summary>
			TextCaps = 34,
			/// <summary>
			/// Flag that indicates the clipping capabilities of the device. If the device can clip to a rectangle, it is 1. Otherwise, it is 0.
			/// </summary>
			ClipCaps = 36,
			/// <summary>
			/// Value that indicates the raster capabilities of the device.
			/// </summary>
			RasterCaps = 38,
			/// <summary>
			/// Relative width of a device pixel used for line drawing.
			/// </summary>
			AspectX = 40,
			/// <summary>
			/// Relative height of a device pixel used for line drawing.
			/// </summary>
			AspectY = 42,
			/// <summary>
			/// Diagonal width of the device pixel used for line drawing.
			/// </summary>
			AspectXY = 44,
			/// <summary>
			/// Value that indicates the shading and blending capabilities of the device.
			/// </summary>
			ShadeBlendCaps = 45,
			LogPixelsX = 88 ,   // Logical pixels/inch in X
			LogPixelsY = 90 ,   // Logical pixels/inch in Y
			/// <summary>
			/// Number of entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available only if the
			/// driver is compatible with 16-bit Windows.
			/// </summary>
			PaletteSize = 104,
			/// <summary>
			/// Number of reserved entries in the system palette. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is available
			/// only if the driver is compatible with 16-bit Windows.
			/// </summary>
			ReservedPaletteSize = 106,
			/// <summary>
			/// Actual color resolution of the device, in bits per pixel. This index is valid only if the device driver sets the RC_PALETTE bit in the RASTERCAPS index and is
			/// available only if the driver is compatible with 16-bit Windows.
			/// </summary>
			ColorResolution = 108,
			/// <summary>
			///  For display devices: the current vertical refresh rate of the device, in cycles per second (Hz).
			///  
			///  A vertical refresh rate value of 0 or 1 represents the display hardware's default refresh rate. This default rate is typically set by switches on a display card
			///  or computer motherboard, or by a configuration program that does not use display functions such as ChangeDisplaySettings.
			/// </summary>
			VerticalRefreshRate = 116,
			DESKTOPVERTRES = 117,   // Horizontal width of entire desktop in pixels (NT5)
			DESKTOPHORZRES = 118,   // Vertical height of entire desktop in pixels (NT5)
			/// <summary>
			/// Preferred horizontal drawing alignment, expressed as a multiple of pixels. For best drawing performance, windows should be horizontally aligned to a multiple of
			/// this value. A value of zero indicates that the device is accelerated, and any alignment may be used.
			/// </summary>
			BLTAlignment = 119,
			/// <summary>
			/// For printing devices: the width of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-x11-inch paper has a physical width
			/// value of 5100 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
			/// </summary>
			PHYSICALWIDTH,
			/// <summary>
			/// For printing devices: the height of the physical page, in device units. For example, a printer set to print at 600 dpi on 8.5-by-11-inch paper has a physical
			/// height value of 6600 device units. Note that the physical page is almost always greater than the printable area of the page, and never smaller.
			/// </summary>
			PHYSICALHEIGHT,
			/// <summary>
			/// For printing devices: the distance from the left edge of the physical page to the left edge of the printable area, in device units. For example, a printer set to
			/// print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the leftmost 0.25-inch of paper, has a horizontal physical offset of 150 device units.
			/// </summary>
			PHYSICALOFFSETX,
			/// <summary>
			/// For printing devices: the distance from the top edge of the physical page to the top edge of the printable area, in device units. For example, a printer set to
			/// print at 600 dpi on 8.5-by-11-inch paper, that cannot print on the topmost 0.5-inch of paper, has a vertical physical offset of 300 device units.
			/// </summary>
			PHYSICALOFFSETY,
			/// <summary>
			/// Scaling factor for the x-axis of the printer.
			/// </summary>
			SCALINGFACTORX,
			/// <summary>
			/// Scaling factor for the y-axis of the printer.
			/// </summary>
			SCALINGFACTORY,
			/// <summary>
			/// Value that indicates the color management capabilities of the device.
			/// </summary>
			COLORMGMTCAPS
		}

		public enum LogFontQuality : byte
		{
			Default = 0,
			Draft = 1,
			Proof = 2,
			NonAntiAliased = 3,
			AntiAliased = 4,
			ClearType = 5,
			ClearTypeNatural = 6
		}
		[Flags()]
		public enum LogFontPitchAndFamily : byte
		{
			PitchDefault = 0,
			PitchFixed = 1,
			PitchVariable = 2,

			FamilyDontCare = (0 << 4),
			FamilyRoman = (1 << 4),
			FamilySwiss = (2 << 4),
			FamilyModern = (3 << 4),
			FamilyScript = (4 << 4),
			FamilyDecorative = (5 << 4)
		}
		public enum LogFontCharSet : byte
		{
			Ansi = 0,
			Default = 1,
			Symbol = 2,
			ShiftJIS = 128,
			Hangul = 129,
			GB2312 = 134,
			ChineseBIG5 = 136,
			Oem = 255,
			Johab = 130,
			Hebrew = 177,
			Arabic = 178,
			Greek = 161,
			Turkish = 162,
			Vietnamese = 163,
			Thai = 222,
			EastEurope = 238,
			Russian = 204,
			Mac = 77,
			Baltic = 186,
		}
	}
}
