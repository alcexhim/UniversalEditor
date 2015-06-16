using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal struct HFSPlusPermissions
	{
		public uint ownerID;
		public uint groupID;
		public uint permissions;
		public uint specialDevice;
	}
}
