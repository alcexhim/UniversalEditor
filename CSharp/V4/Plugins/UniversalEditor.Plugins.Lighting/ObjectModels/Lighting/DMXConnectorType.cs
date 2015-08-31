using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Lighting
{
	[Flags()]
	public enum DMXConnectorType
	{
		None = 0,
		DMX3Pin = 1,
		DMX5Pin = 2
	}
}
