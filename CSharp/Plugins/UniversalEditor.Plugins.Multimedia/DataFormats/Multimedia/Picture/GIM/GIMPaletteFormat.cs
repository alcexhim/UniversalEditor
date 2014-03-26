/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 4:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Description of GIMPaletteFormat.
	/// </summary>
	public enum GIMPaletteFormat
	{
		/// <summary>
		/// 5 bits Red, 6 bits Green, 5 bits Red, 0 bits Alpha
		/// </summary>
		RGBA5650 = 0x00,
		/// <summary>
		/// 5 bits Red, 5 bits Green, 5 bits Red, 1 bit Alpha
		/// </summary>
		RGBA5551 = 0x01,
		/// <summary>
		/// 4 bits Red, 4 bits Green, 4 bits Red, 4 bits Alpha
		/// </summary>
		RGBA4444 = 0x02,
		/// <summary>
		/// 8 bits Red, 8 bits Green, 8 bits Red, 8 bits Alpha
		/// </summary>
		RGBA8888 = 0x03
	}
}
