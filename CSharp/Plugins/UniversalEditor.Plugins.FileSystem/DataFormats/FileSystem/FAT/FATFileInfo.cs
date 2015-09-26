using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	public struct FATFileInfo
	{
		public string LongFileName;
		public string ShortFileName;
		public uint Length;
		public short Offset;
		public FileAttributes Attributes;
	}
}
