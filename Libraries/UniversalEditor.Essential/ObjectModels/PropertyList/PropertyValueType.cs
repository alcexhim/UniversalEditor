using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.PropertyList
{
	public enum PropertyValueType
	{
		Unknown = -1,
		None = 0,
		Binary,
		Link,
		String,
		StringList,
		ExpandedString,
		DoubleWord,
		QuadWord
	}
}
