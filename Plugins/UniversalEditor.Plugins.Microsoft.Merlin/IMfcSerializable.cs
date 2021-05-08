using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor
{
	public interface IMfcSerialisable<T> where T : IMfcSerialisable<T>
	{
		void LoadObject(Reader reader);
		void SaveObject(Writer writer);
	}
}
