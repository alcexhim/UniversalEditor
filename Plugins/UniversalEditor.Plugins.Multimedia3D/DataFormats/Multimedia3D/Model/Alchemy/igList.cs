using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public class igList : igBase
	{
		public uint Capacity { get; set; } = 0;
	}
	public class igList<T> : igList
	{
		public List<T> Items { get; } = new List<T>();
	}
}
