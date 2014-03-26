/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 4:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Description of GIMPartType.
	/// </summary>
	public enum GIMPartType
	{
		EndOfFileAddress = 0x02,
		FileInfoAddress = 0x03,
		ImageData = 0x04,
		PaletteData = 0x05,
		FileInfoData = 0xFF
	}
}
