using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
				MemoryStream ms = new MemoryStream();
				UniversalEditor.IO.BinaryWriter bw = new UniversalEditor.IO.BinaryWriter(ms);
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
						bw.Write(lastCount);
						bw.Write(last);
						last = input[i];
						lastCount = 1;
					}
				}
				if (input[input.Length - 1] == last)
				{
					bw.Write(lastCount);
					bw.Write(last);
				}
				bw.Flush();
				bw.Close();
				result = ms.ToArray();
			}
			return result;
		}
		public static byte[] Decode(byte[] input)
		{
			MemoryStream ms = new MemoryStream(input);
			UniversalEditor.IO.BinaryReader br = new UniversalEditor.IO.BinaryReader(ms);
			MemoryStream msOutput = new MemoryStream();
			while (ms.Position != ms.Length)
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
