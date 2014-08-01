using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.ResourceBlocks
{
	public class CustomResourceBlock : ExecutableResourceBlock
	{
		private ExecutableResourceType mvarType = ExecutableResourceType.Custom;
		public override ExecutableResourceType Type
		{
			get { return mvarType; }
		}

		private byte[] mvarData = new byte[0];
		public byte[] Data { get { return mvarData; } set { mvarData = value; } }

		public override object Clone()
		{
			CustomResourceBlock clone = new CustomResourceBlock();
			clone.Data = (mvarData.Clone() as byte[]);
			return clone;
		}
	}
}
