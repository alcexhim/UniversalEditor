using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public enum IFileSystemObjectType
	{
		None = 0,
		Folder = 1,
		File = 2,
		All = Folder | File
	}
}
