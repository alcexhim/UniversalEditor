/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 5:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Description of GIMImageFormat.
	/// </summary>
	public enum GIMImageFormat
	{
		/// <summary>
		/// Pixels are comprised of 5 bits Red, 6 bits Green, 5 bits Blue, 0 bits
		/// Alpha.
		/// </summary>
		RGBA5650 = 0x00,
		/// <summary>
		/// Pixels are comprised of 5 bits Red, 5 bits Green, 5 bits Blue, 1 bit
		/// Alpha.
		/// </summary>
		RGBA5551 = 0x01,
		/// <summary>
		/// Pixels are comprised of 4 bits Red, 4 bits Green, 4 bits Blue, 4 bits
		/// Alpha.
		/// </summary>
		RGBA4444 = 0x02,
		/// <summary>
		/// Pixels are comprised of 8 bits Red, 8 bits Green, 8 bits Blue, 8 bits
		/// Alpha.
		/// </summary>
		RGBA8888 = 0x03,
		/// <summary>
		/// Pixels are comprised of 4-bit indices into the palette color table.
		/// </summary>
		Index4 = 0x04,
		/// <summary>
		/// Pixels are comprised of 8-bit indices into the palette color table.
		/// </summary>
		Index8 = 0x05,
		/// <summary>
		/// Pixels are comprised of 16-bit indices into the palette color table.
		/// </summary>
		Index16 = 0x06,
		/// <summary>
		/// Pixels are comprised of 32-bit indices into the palette color table.
		/// </summary>
		Index32 = 0x07
	}
}
