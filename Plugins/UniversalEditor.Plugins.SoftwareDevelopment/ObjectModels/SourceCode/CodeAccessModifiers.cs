using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
	public enum CodeAccessModifiers
	{
		None,
		Private,
		Family,
		FamilyANDAssembly,
		FamilyORAssembly,
		Assembly,
		Public
	}
}
