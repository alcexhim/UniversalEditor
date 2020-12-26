using System;
namespace UniversalEditor.Plugins.Blockchain
{
	public abstract class Block : ICloneable
	{
		public abstract object Clone();
	}
}
