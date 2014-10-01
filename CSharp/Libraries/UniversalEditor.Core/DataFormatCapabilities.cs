using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public enum DataFormatCapabilities
	{
		None = 0,
		Load = 1,
		Save = 2,
		Bootstrap = 4,
		All = Load | Save | Bootstrap
	}
}
