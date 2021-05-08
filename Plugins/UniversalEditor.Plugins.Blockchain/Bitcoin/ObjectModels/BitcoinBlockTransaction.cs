using System;
namespace UniversalEditor.Plugins.Blockchain.Bitcoin.ObjectModels
{
	public class BitcoinBlockTransaction : BlockTransaction
	{
		public byte[] PublicKeyScript { get; set; }

		public class BitcoinBlockTransactionCollection
			: System.Collections.ObjectModel.Collection<BitcoinBlockTransaction>
		{

		}

		public override object Clone()
		{
			BitcoinBlockTransaction clone = new BitcoinBlockTransaction();
			return clone;
		}
	}
}
