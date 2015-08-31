using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Kronosaur.ResourceTable
{
	public enum ResourceTableEntryFlags : int
	{
		/// <summary>
		/// No flags are specified.
		/// </summary>
		None = 0x00000000,
		/// <summary>
		/// The resource is compressed with the Zlib algorithm.
		/// </summary>
		CompressZlib = 0x00000001
	}
}
