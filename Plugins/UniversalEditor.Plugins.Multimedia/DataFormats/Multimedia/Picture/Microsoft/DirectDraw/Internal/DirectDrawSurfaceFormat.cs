//
//  DirectDrawSurfaceFormat.cs - indicates the texture format of a DirectDraw Surface texture
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.DirectDraw.Internal
{
	/// <summary>
	/// Indicates the texture format of a DirectDraw Surface texture.
	/// </summary>
	public enum DirectDrawSurfaceFormat
	{
		Unknown = 0,
		#region DXTn Compressed Formats - These flags are used for compressed textures.
		/// <summary>
		/// DXT1 compression texture format
		/// </summary>
		DXT1 = 827611204,
		/// <summary>
		/// DXT2 compression texture format
		/// </summary>
		DXT2 = 844388420,
		/// <summary>
		/// DXT3 compression texture format
		/// </summary>
		DXT3 = 861165636,
		/// <summary>
		/// DXT4 compression texture format
		/// </summary>
		DXT4 = 877942852,
		/// <summary>
		/// DXT5 compression texture format
		/// </summary>
		DXT5 = 894720068,
		#endregion
		#region Floating-Point Formats - These 16-bits-per-channel formats are also known as s10e5 formats.
		/// <summary>
		/// 16-bit float format using 16 bits for the red channel.
		/// </summary>
		R16F = 111,
		/// <summary>
		/// 32-bit float format using 16 bits for the red channel and 16 bits for the green channel.
		/// </summary>
		G16R16F = 112,
		/// <summary>
		/// 64-bit float format using 16 bits for the each channel (alpha, blue, green, red).
		/// </summary>
		A16B16G16R16F = 113,
		#endregion
		#region FourCC - Data in a FOURCC format is compressed data.
		Multi2ARGB8 = 827606349,
		/// <summary>
		/// A 16-bit packed RGB format analogous to YUY2 (Y0U0, Y1V0, Y2U2, and so on). It requires a pixel pair in order to properly represent the color value. The first pixel in the pair contains 8 bits of green (in the high 8 bits) and 8 bits of red (in the low 8 bits). The second pixel contains 8 bits of green (in the high 8 bits) and 8 bits of blue (in the low 8 bits). Together, the two pixels share the red and blue components, while each has a unique green component (G0R0, G1B0, G2R2, and so on). The texture sampler does not normalize the colors when looking up into a pixel shader; they remain in the range of 0.0f to 255.0f. This is true for all programmable pixel shader models. For the fixed function pixel shader, the hardware should normalize to the 0.f to 1.f range and essentially treat it as the YUY2 texture. Hardware that exposes this format must have the PixelShader1xMaxValue member of D3DCAPS9 set to a value capable of handling that range.
		/// </summary>
		G8R8G8B8 = 1111970375,
		R8G8B8G8 = 1195525970,
		/// <summary>
		/// UYVY format (PC98 compliance)
		/// </summary>
		UYVY = 1498831189,
		/// <summary>
		/// YUY2 format (PC98 compliance)
		/// </summary>
		YUY2 = 844715353,
		#endregion
		#region IEEE Formats - These 32-bits-per-channel formats are also known as s23e8 formats.
		/// <summary>
		/// 32-bit float format using 32 bits for the red channel.
		/// </summary>
		R32F = 114,
		/// <summary>
		/// 64-bit float format using 32 bits for the red channel and 32 bits for the green channel.
		/// </summary>
		G32R32F = 115,
		/// <summary>
		/// 128-bit float format using 32 bits for the each channel (alpha, blue, green, red).
		/// </summary>
		A32B32G32R32F = 116,
		#endregion
		#region Mixed Formats - data in mixed formats can contain a combination of unsigned data and signed data.
		/// <summary>
		/// 16-bit bump-map format with luminance using 6 bits for luminance, and 5 bits each for v and u.
		/// </summary>
		L6V5U5 = 61,
		/// <summary>
		/// 32-bit bump-map format with luminance using 8 bits for each channel.
		/// </summary>
		X8L8V8U8 = 62,
		/// <summary>
		/// 32-bit bump-map format using 2 bits for alpha and 10 bits each for w, v, and u.
		/// </summary>
		A2W10V10U10 = 67,
		#endregion
		#region Signed Formats - data in a signed format can be both positive and negative. Signed formats use combinations of (U), (V), (W), and (Q) data.
		/// <summary>
		/// 16-bit bump-map format using 8 bits each for u and v data.
		/// </summary>
		V8U8 = 60,
		/// <summary>
		/// 32-bit bump-map format using 8 bits for each channel.
		/// </summary>
		Q8W8V8U8 = 63,
		/// <summary>
		/// 32-bit bump-map format using 16 bits for each channel.
		/// </summary>
		V16U16 = 64,
		/// <summary>
		/// 64-bit bump-map format using 16 bits for each component.
		/// </summary>
		Q16W16V16U16 = 110,
		/// <summary>
		/// 16-bit normal compression format. The texture sampler computes the C channel from: C = sqrt(1 - U2 - V2).
		/// </summary>
		CxV8U8 = 117,
		#endregion
		#region Unsigned Formats
		/// <summary>
		/// 24-bit RGB pixel format with 8 bits per channel.
		/// </summary>
		R8G8B8 = 20,
		/// <summary>
		/// 32-bit ARGB pixel format with alpha, using 8 bits per channel.
		/// </summary>
		A8R8G8B8 = 21,
		/// <summary>
		/// 32-bit RGB pixel format, where 8 bits are reserved for each color.
		/// </summary>
		X8R8G8B8 = 22,
		/// <summary>
		/// 16-bit RGB pixel format with 5 bits for red, 6 bits for green, and 5 bits for blue.
		/// </summary>
		R5G6B5 = 23,
		/// <summary>
		/// 16-bit pixel format where 5 bits are reserved for each color.
		/// </summary>
		X1R5G5B5 = 24,
		/// <summary>
		/// 16-bit pixel format where 5 bits are reserved for each color and 1 bit is reserved for alpha.
		/// </summary>
		A1R5G5B5 = 25,
		/// <summary>
		/// 16-bit ARGB pixel format with 4 bits for each channel.
		/// </summary>
		A4R4G4B4 = 26,
		/// <summary>
		/// 8-bit RGB texture format using 3 bits for red, 3 bits for green, and 2 bits for blue.
		/// </summary>
		R3G3B2 = 27,
		/// <summary>
		/// 8-bit alpha only.
		/// </summary>
		A8 = 28,
		/// <summary>
		/// 16-bit ARGB texture format using 8 bits for alpha, 3 bits each for red and green, and 2 bits for blue.
		/// </summary>
		A8R3G3B2 = 29,
		/// <summary>
		/// 16-bit RGB pixel format using 4 bits for each color.
		/// </summary>
		X4R4G4B4 = 30,
		/// <summary>
		/// 32-bit pixel format using 10 bits for each color and 2 bits for alpha.
		/// </summary>
		A2B10G10R10 = 31,
		/// <summary>
		/// 32-bit ARGB pixel format with alpha, using 8 bits per channel.
		/// </summary>
		A8B8G8R8 = 32,
		/// <summary>
		/// 32-bit RGB pixel format, where 8 bits are reserved for each color.
		/// </summary>
		X8B8G8R8 = 33,
		/// <summary>
		/// 32-bit pixel format using 16 bits each for green and red.
		/// </summary>
		G16R16 = 34,
		/// <summary>
		/// 32-bit pixel format using 10 bits each for red, green, and blue, and 2 bits for alpha.
		/// </summary>
		A2R10G10B10 = 35,
		/// <summary>
		/// 64-bit pixel format using 16 bits for each component.
		/// </summary>
		A16B16G16R16 = 36,
		/// <summary>
		/// 8-bit color indexed with 8 bits of alpha.
		/// </summary>
		A8P8 = 40,
		/// <summary>
		/// 8-bit color indexed.
		/// </summary>
		P8 = 41,
		/// <summary>
		/// 8-bit luminance only.
		/// </summary>
		L8 = 50,
		/// <summary>
		/// 16-bit using 8 bits each for alpha and luminance.
		/// </summary>
		A8L8 = 51,
		/// <summary>
		/// 8-bit using 4 bits each for alpha and luminance.
		/// </summary>
		A4L4 = 52,
		/// <summary>
		/// 16-bit luminance only.
		/// </summary>
		L16 = 81,
		/// <summary>
		/// 1-bit monochrome. This flag is available in Direct3D 9Ex only.
		/// </summary>
		A1 = 118,
		/// <summary>
		/// 2.8-biased fixed point. This flag is available in Direct3D 9Ex only.
		/// </summary>
		A2B10G10R10_XR_BIAS = 119,
		/// <summary>
		/// Binary format indicating that the data has no inherent type. This flag is available in Direct3D 9Ex only.
		/// </summary>
		BinaryBuffer = 199
		#endregion
	}
}
