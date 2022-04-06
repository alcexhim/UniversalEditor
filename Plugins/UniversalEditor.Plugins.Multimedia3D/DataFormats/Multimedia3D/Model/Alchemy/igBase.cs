using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public abstract class igBase
	{
		public string TypeName { get; set; } = String.Empty;

		public override string ToString()
		{
			return TypeName;
		}
	}
}
