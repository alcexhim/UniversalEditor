using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
	/// <summary>
	/// The top-to-bottom ordering in which pixel data is transferred from the file to the screen.
	/// </summary>
	public enum TargaVerticalTransferOrder
	{
		/// <summary>
		/// Unknown transfer order.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Transfer order of pixels is from the bottom to top.
		/// </summary>
		BottomToTop = 0,
		/// <summary>
		/// Transfer order of pixels is from the top to bottom.
		/// </summary>
		TopToBottom = 1
	}
}
