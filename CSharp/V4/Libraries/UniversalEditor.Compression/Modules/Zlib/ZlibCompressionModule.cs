using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.Zlib
{
	public class ZlibCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "zlib"; }
		}

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			Internal.ZOutputStream zout = new Internal.ZOutputStream(outputStream, 5);
			
			byte[] buffer = new byte[2048];
			while (!inputStream.get_EndOfStream())
			{
				inputStream.Read(buffer, 0, buffer.Length);
				zout.Write(buffer, 0, buffer.Length);
			}
			zout.Flush();
			zout.Close();
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			int data = 0;
			int stopByte = -1;
			Internal.ZInputStream inZStream = new Internal.ZInputStream(inputStream);
			while (stopByte != (data = inZStream.Read()))
			{
				byte _dataByte = (byte)data;
				outputStream.WriteByte(_dataByte);
			}
			inZStream.Close();
		}
	}
}
