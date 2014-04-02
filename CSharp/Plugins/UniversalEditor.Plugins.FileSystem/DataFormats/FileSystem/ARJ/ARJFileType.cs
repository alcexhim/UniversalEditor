using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	public enum ARJFileType
	{
		Binary = 0,
		/// <summary>
		/// 7-bit text
		/// </summary>
		Text = 1,
		CommentHeader = 2,
		Directory = 3,
		VolumeLabel = 4
	}
}
