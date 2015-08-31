using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Targa
{
	/// <summary>
	/// The left-to-right ordering in which pixel data is transferred from the file to the screen.
	/// </summary>
	public enum TargaHorizontalTransferOrder
	{
		/// <summary>
		/// Unknown transfer order.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Transfer order of pixels is from the right to left.
		/// </summary>
		RightToLeft = 0,
		/// <summary>
		/// Transfer order of pixels is from the left to right.
		/// </summary>
		LeftToRight = 1
	}
}
