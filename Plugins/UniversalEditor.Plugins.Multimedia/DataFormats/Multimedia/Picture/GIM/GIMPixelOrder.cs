/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 5/19/2013
 * Time: 5:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Description of GIMPixelOrder.
	/// </summary>
	public enum GIMPixelOrder
	{
		Normal = 0x00,
		/// <summary>
		/// PSP fast pixel storage format [16x8 block relocation]
		/// PSP高速ピクセル格納形式 [16x8ブロック再配置]
		/// </summary>
		Faster = 0x01
	}
}
