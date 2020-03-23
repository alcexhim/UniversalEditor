using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin.Mfc
{
	[MfcSerialisable]
	internal class TrailingBytes : MfcObject
	{
		private byte[] trailingBytes = new byte[0];

		public override void LoadObject(IO.Reader reader)
		{
			ushort byteCount = reader.ReadUInt16();
			trailingBytes = reader.ReadBytes(byteCount);
		}
		public override void SaveObject(IO.Writer writer)
		{
			writer.WriteUInt16(0);
		}
	}
}
