using System;
namespace UniversalEditor.Plugins.Blockchain.Bitcoin.ObjectModels
{
	public class BitcoinBlockchainObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Blockchain", "Bitcoin" };
			}
			return _omr;
		}

		public BitcoinBlock.BitcoinBlockCollection Blocks { get; } = new BitcoinBlock.BitcoinBlockCollection();

		public override void Clear()
		{
			Blocks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			BitcoinBlockchainObjectModel clone = (where as BitcoinBlockchainObjectModel);
			if (clone == null)
				throw new ObjectModelNotSupportedException();

			for (int i = 0; i < Blocks.Count;i++)
			{
				clone.Blocks.Add(Blocks[i].Clone() as BitcoinBlock);
			}
		}
	}
}
