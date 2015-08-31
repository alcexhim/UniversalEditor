using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	public class ExecutableRelativeVirtualAddress : ICloneable
	{
		public class ExecutableRelativeVirtualAddressCollection
			: System.Collections.ObjectModel.Collection<ExecutableRelativeVirtualAddress>
		{
		}

		private ushort mvarDataDirectoryVirtualAddress = 0;
		public ushort DataDirectoryVirtualAddress { get { return mvarDataDirectoryVirtualAddress; } set { mvarDataDirectoryVirtualAddress = value; } }

		private ushort mvarDataDirectorySize = 0;
		public ushort DataDirectorySize { get { return mvarDataDirectorySize; } set { mvarDataDirectorySize = value; } }

		public object Clone()
		{
			ExecutableRelativeVirtualAddress rva = new ExecutableRelativeVirtualAddress();
			rva.DataDirectorySize = mvarDataDirectorySize;
			rva.DataDirectoryVirtualAddress = mvarDataDirectoryVirtualAddress;
			return rva;
		}
	}
}
