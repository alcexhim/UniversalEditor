using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable.ResourceBlocks
{
	public class IconResourceBlock : ExecutableResourceBlock
	{
		public override ExecutableResourceType Type
		{
			get { return ExecutableResourceType.Icon; }
		}

		public override object Clone()
		{
			throw new NotImplementedException();
		}
	}
}
