using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Blockchain.Bitcoin.ObjectModels;

namespace UniversalEditor.Plugins.Blockchain.Bitcoin.DataFormats
{
	public class BitcoinBlockchainDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(BitcoinBlockchainObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public int HashLength { get; set; } = 32;

		private readonly DateTime UNIX_EPOCH = new DateTime(1970, 01, 01, 00, 00, 00);

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BitcoinBlockchainObjectModel blockchain = (objectModel as BitcoinBlockchainObjectModel);
			if (blockchain == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			while (!reader.EndOfStream)
			{
				uint signature = reader.ReadUInt32();
				if (signature != 0xD9B4BEF9)
					throw new InvalidDataFormatException("file does not begin with 0xD9B4BEF9");

				BitcoinBlock block = new BitcoinBlock();
				uint datasize = reader.ReadUInt32();
				block.Version = reader.ReadUInt32();
				block.PreviousBlockHash = reader.ReadBytes(HashLength);
				block.MerkelRoot = reader.ReadBytes(HashLength);
				uint timestamp = reader.ReadUInt32();
				block.Timestamp = UNIX_EPOCH.AddSeconds(timestamp);
				block.Bits = reader.ReadUInt32();
				block.Nonce = reader.ReadUInt32();
				ulong transactionCount = ReadCompactNumber(reader);
				for (ulong i = 0; i < transactionCount; i++)
				{
					BitcoinBlockTransaction transaction = new BitcoinBlockTransaction();
					uint transactionVersion = reader.ReadUInt32();
					ulong inputs = ReadCompactNumber(reader);
					for (ulong j = 0; j < inputs; j++)
					{
						byte[] previousOutput = reader.ReadBytes(HashLength);
						uint previousOutput2 = reader.ReadUInt32();
						ulong scriptLength = ReadCompactNumber(reader);
						byte[] scriptData = reader.ReadBytes(scriptLength);
						uint sequence = reader.ReadUInt32();
					}
					byte outputs = reader.ReadByte();
					for (int j = 0; j < outputs; j++)
					{
						ulong output_c = reader.ReadUInt64();
						ulong publicKeyScriptLength = ReadCompactNumber(reader);
						transaction.PublicKeyScript = reader.ReadBytes(publicKeyScriptLength);
					}
					uint locktime = reader.ReadUInt32();
					block.Transactions.Add(transaction);
				}
				blockchain.Blocks.Add(block);
			}
		}

		private ulong ReadCompactNumber(Reader reader)
		{
			byte num = reader.ReadByte();
			if (num == 0xFD)
			{
				return reader.ReadUInt16();
			}
			else if (num == 0xFE)
			{
				return reader.ReadUInt32();
			}
			else if (num == 0xFF)
			{
				return reader.ReadUInt64();
			}
			return num;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
