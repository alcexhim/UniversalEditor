using System;
namespace UniversalEditor.Plugins.Blockchain.Bitcoin.ObjectModels
{
	public class BitcoinBlock : Block
	{
		public class BitcoinBlockCollection
			: System.Collections.ObjectModel.Collection<BitcoinBlock>
		{

		}

		public uint Version { get; internal set; }
		public byte[] PreviousBlockHash { get; set; }
		public byte[] MerkelRoot { get; set; }
		public DateTime Timestamp { get; set; }
		public uint Bits { get; set; }
		public uint Nonce { get; set; }
		public BitcoinBlockTransaction.BitcoinBlockTransactionCollection Transactions { get; } = new BitcoinBlockTransaction.BitcoinBlockTransactionCollection();

		public override object Clone()
		{
			BitcoinBlock clone = new BitcoinBlock();
			clone.Version = Version;
			return clone;
		}
	}
}
