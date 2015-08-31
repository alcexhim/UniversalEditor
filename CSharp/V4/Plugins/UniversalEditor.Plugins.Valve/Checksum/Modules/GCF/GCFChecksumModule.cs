using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Checksum.Modules.GCF
{
	public class GCFChecksumModule : ChecksumModule
	{
		public override string Name
		{
			get { return "GCF"; }
		}

		public override void Update(byte[] buffer, int offset, int count)
		{
			Adler32.Adler32 adler32 = new Adler32.Adler32();
			CRC32.CRC32ChecksumModule crc32 = new CRC32.CRC32ChecksumModule();
			Value = (adler32.Calculate(buffer) ^ crc32.Calculate(buffer));
		}
		public override void Update(int value)
		{
			throw new NotImplementedException();
		}
	}
}
