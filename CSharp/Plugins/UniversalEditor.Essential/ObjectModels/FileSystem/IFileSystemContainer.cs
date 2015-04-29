using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public interface IFileSystemContainer
	{
		File.FileCollection Files { get; }
		Folder.FolderCollection Folders { get; }
	}
}
