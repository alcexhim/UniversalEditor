using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.Explode.Internal
{
	public delegate uint ExplodeInputFunction(object how, ref Ptr<byte> buf);
	public delegate int ExplodeOutputFunction(object how, Ptr<byte> buf, uint len);
}
