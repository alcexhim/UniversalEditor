using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UniversalEditor.Accessors;

namespace UniversalEditor.Compression.Common
{
	public class RunLengthEncoding
	{
		public static byte[] Encode(byte[] input)
		{
			byte[] result;
			if (input.Length < 2)
			{
				result = input;
			}
			else
			{
				UniversalEditor.IO.Writer bw = new UniversalEditor.IO.Writer(new MemoryAccessor());
				byte last = input[0];
				byte lastCount = 0;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == last)
					{
						lastCount += 1;
					}
					else
					{
						bw.WriteByte(lastCount);
						bw.WriteByte(last);
						last = input[i];
						lastCount = 1;
					}
				}
				if (input[input.Length - 1] == last)
				{
					bw.WriteByte(lastCount);
					bw.WriteByte(last);
				}
				bw.Flush();
				bw.Close();
				result = (bw.Accessor as MemoryAccessor).ToArray();
			}
			return result;
		}
		public static byte[] Decode(byte[] input)
		{
			IO.Reader br = new IO.Reader(new MemoryAccessor(input));
			MemoryStream msOutput = new MemoryStream();
			while (!br.EndOfStream)
			{
				byte ct = br.ReadByte();
				byte val = br.ReadByte();
				for (int i = 0; i < (int)ct; i++)
				{
					msOutput.WriteByte(val);
				}
			}
			br.Close();
			msOutput.Flush();
			msOutput.Close();
			return msOutput.ToArray();
		}
	}
}
