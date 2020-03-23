using System;
namespace MBS.Framework.UserInterface.Engines.GTK.Internal.Cairo
{
	internal static class Constants
	{
		public enum CairoStatus
		{
			Unknown = -1,
			Success = 0,

			NoMemory,
			InvalidRestore,
			InvalidPopGroup,
			NoCurrentPoint,
			InvalidMatrix,
			InvalidStatus,
			NullPointer,
			InvalidString,
			InvalidPathData,
			ReadError,
			WriteError,
			SurfaceFinished,
			SurfaceTypeMismatch,
			PatternTypeMismatch,
			InvalidContent,
			InvalidFormat,
			InvalidVisual,
			FileNotFound,
			InvalidDash,
			InvalidDSCComment,
			InvalidIndex,
			ClipNotRepresentable,
			TempFileError,
			InvalidStride,
			FontTypeMismatch,
			UserFontImmutable,
			UserFontError,
			NegativeCount,
			InvalidClusters,
			InvalidSlant,
			InvalidWeight,
			InvalidSize,
			UserFontNotImplemented,
			DeviceTypeMismatch,
			DeviceError,
			InvalidMeshConstruction,
			DeviceFinished,
			JBIG2GlobalMissing,
			PNGError,
			FreeTypeError,
			Win32GDIError,
			TagError,

			LastStatus
		}
		public enum CairoFontSlant
		{
			Normal,
			Italic,
			Oblique
		}
		public enum CairoFontWeight
		{
			Normal,
			Bold
		}

		public enum CairoFormat
		{
			/// <summary>
			/// no such format exists or is supported.
			/// </summary>
			Invalid = 0,
			/// <summary>
			/// each pixel is a 32-bit quantity, with alpha in the upper 8 bits, then red, then green, then blue. The 32-bit quantities are stored native-endian. Pre-multiplied alpha is used. (That is, 50% transparent red is 0x80800000, not 0x80ff0000.) (Since 1.0)
			/// </summary>
			ARGB32,
			/// <summary>
			/// each pixel is a 32-bit quantity, with the upper 8 bits unused.Red, Green, and Blue are stored in the remaining 24 bits in that order. (Since 1.0)
			/// </summary>
			RGB24,
			/// <summary>
			/// each pixel is a 8-bit quantity holding an alpha value. (Since 1.0)
			/// </summary>
			A8,
			/// <summary>
			/// each pixel is a 1-bit quantity holding an alpha value.Pixels are packed together into 32-bit quantities. The ordering of the bits matches the endianness of the platform.On a big-endian machine, the first pixel is in the uppermost bit, on a little-endian machine the first pixel is in the least-significant bit. (Since 1.0)
			/// </summary>
			A1,
			/// <summary>
			/// each pixel is a 16-bit quantity with red in the upper 5 bits, then green in the middle 6 bits, and blue in the lower 5 bits. (Since 1.2)
			/// </summary>
			RGB16_565,
			/// <summary>
			/// like RGB24 but with 10bpc. (Since 1.12)
			/// </summary>
			RGB30
		}
		public enum CairoContent
		{
			Color = 0x1000,
			Alpha = 0x2000,
			Both = 0x3000
		}
	}
}
