using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.InnoSetup
{
	public class Idska32DataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Inno Setup BIN archive", new byte?[][] { new byte?[] { (byte)'i', (byte)'d', (byte)'s', (byte)'k', (byte)'a', (byte)'3', (byte)'2' } }, new string[] { "*.bin" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.BinaryReader br = base.Stream.BinaryReader;
			br.BaseStream.Position = 0;

			string idska32 = br.ReadFixedLengthString(8);
			if (idska32 != "idska32\x1A") throw new InvalidDataFormatException();

			uint compressedSize = br.ReadUInt32();
			string format = br.ReadFixedLengthString(4);
			int unknown2 = br.ReadInt32();
			short unknown3 = br.ReadInt16();

			Compression.Zlib.Internal.ZInputStream zis = new Compression.Zlib.Internal.ZInputStream(base.Stream.BaseStream);

			long pos = br.BaseStream.Position;
			while (!br.EndOfStream)
			{
				byte[] data = new byte[2048];
				zis.read(data, (int)pos, data.Length);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.BinaryWriter bw = base.Stream.BinaryWriter;
			bw.WriteFixedLengthString("idska32\x1A");

			bw.Flush();
		}
	}
}
