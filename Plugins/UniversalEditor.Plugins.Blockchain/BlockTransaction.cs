using System;
namespace UniversalEditor.Plugins.Blockchain
{
	public abstract class BlockTransaction : ICloneable
	{
		public abstract object Clone();
	}
}
