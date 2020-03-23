using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
    public enum DirectDrawSurfaceLoadInfoType : uint
    {
        None = 0x0000,
        UnsignedByte = 0x1401,
        UnsignedShort565 = 0x8363,
        UnsignedShort1555REV = 0x8366
    }
    public enum DirectDrawSurfaceLoadInfoFormat
    {
        RGB = 0x1907,
        CompressedRGBAS3TCDXT1 = 0x83F1,
        CompressedRGBAS3TCDXT3 = 0x83F2,
        CompressedRGBAS3TCDXT5 = 0x83F3,
        RGB5 = 0x8050,
        RGB8 = 0x8051,
        RGB5A1 = 0x8057,
        RGBA8 = 0x8058,
        BGR = 0x80E0,
        BGRA = 0x80E1
    }
	internal struct DirectDrawSurfaceLoadInfo
	{
		private bool mvarCompressed;
        public bool Compressed { get { return mvarCompressed; } }

		private bool mvarSwap;
        public bool Swap { get { return mvarSwap; } }

		private bool mvarPalette;
        public bool Palette { get { return mvarPalette; } }

		private uint mvarDivSize;
        public uint DivSize { get { return mvarDivSize; } }

		private uint mvarBlockBytes;
        public uint BlockBytes { get { return mvarBlockBytes; } }

		private DirectDrawSurfaceFormat mvarFormat;
		public DirectDrawSurfaceFormat Format { get { return mvarFormat; } }

        public DirectDrawSurfaceLoadInfo(bool compressed, bool swap, bool palette, uint divSize, uint blockBytes, DirectDrawSurfaceFormat format)
		{
			mvarCompressed = compressed;
			mvarSwap = swap;
			mvarPalette = palette;
			mvarDivSize = divSize;
			mvarBlockBytes = blockBytes;
			mvarFormat = format;
		}
	}
}
