using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public enum DataFormatHintComparison
	{
		Unspecified = -1,
		Never = 0,
		FilterOnly = 1,
		MagicOnly = 2,
		FilterThenMagic = 3,
		MagicThenFilter = 4,
		Always = 5
	}
}
