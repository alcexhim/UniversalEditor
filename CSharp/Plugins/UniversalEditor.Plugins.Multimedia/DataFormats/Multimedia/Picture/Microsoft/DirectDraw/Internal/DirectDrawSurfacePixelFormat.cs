using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
	internal struct DirectDrawSurfacePixelFormat
	{
		private uint mvarPixelFormatSize;
		public uint PixelFormatSize { get { return mvarPixelFormatSize; } set { mvarPixelFormatSize = value; } }

		private DirectDrawSurfacePixelFormatFlags mvarFlags;
		public DirectDrawSurfacePixelFormatFlags Flags { get { return mvarFlags; } set { mvarFlags = value; } }

		private string mvarFourCC;
		public string FourCC { get { return mvarFourCC; } set { mvarFourCC = value; } }

		private uint mvarRGBBitCount;
		public uint RGBBitCount { get { return mvarRGBBitCount; } set { mvarRGBBitCount = value; } }

		private uint mvarRBitMask;
		public uint RBitMask { get { return mvarRBitMask; } set { mvarRBitMask = value; } }

		private uint mvarGBitMask;
		public uint GBitMask { get { return mvarGBitMask; } set { mvarGBitMask = value; } }

		private uint mvarBBitMask;
		public uint BBitMask { get { return mvarBBitMask; } set { mvarBBitMask = value; } }

		private uint mvarAlphaBitMask;
		public uint AlphaBitMask { get { return mvarAlphaBitMask; } set { mvarAlphaBitMask = value; } }
	}
}
