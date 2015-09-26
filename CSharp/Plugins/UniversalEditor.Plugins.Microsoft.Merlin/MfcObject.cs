using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor
{
	public abstract class MfcObject : IMfcSerialisable<MfcObject>
	{
		public abstract void LoadObject(Reader reader);
		public abstract void SaveObject(Writer reader);
	}
}
