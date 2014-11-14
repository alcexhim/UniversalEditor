using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.FileSystem
{
	public interface IFileSystemObject
	{
	}
	public class IFileSystemObjectCollection
		: System.Collections.ObjectModel.Collection<IFileSystemObject>
	{

	}
}
