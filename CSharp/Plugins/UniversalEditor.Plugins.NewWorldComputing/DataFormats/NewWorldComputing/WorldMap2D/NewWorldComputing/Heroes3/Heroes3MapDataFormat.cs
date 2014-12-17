using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.Gaming.WorldMap2D.NewWorldComputing.Heroes3
{
	public class Heroes3MapDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MapObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			MapObjectModel map = (objectModel as MapObjectModel);

			#region decompress the map file
			IO.Reader br = base.Accessor.Reader;
			byte[] gzipped = br.ReadToEnd();
			byte[] decompressed = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(gzipped);
			br = new IO.Reader(new MemoryAccessor(decompressed));
			#endregion

			Heroes3GameType u0 = (Heroes3GameType)br.ReadInt32();
			byte discard = br.ReadByte();
			int mapsize = br.ReadByte();
			int unknown = br.ReadInt32();

			map.Name = br.ReadInt32String();
			map.Description = br.ReadInt32String();

			short u1 = br.ReadInt16();
			short u2 = br.ReadInt16();
			short u3 = br.ReadInt16();
			byte u7 = br.ReadByte();

			short a1 = br.ReadInt16();
			short a2 = br.ReadInt16();
			short a3 = br.ReadInt16();

			string name = br.ReadInt32String();


			/*
			string sectTitle = br.ReadInt32String();
			string sectDesc = br.ReadInt32String();
			*/
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);




			bw.Flush();
			bw.Close();

			#region compress the map file
			bw = base.Accessor.Writer;
			byte[] decompressed = ma.ToArray();
			byte[] gzipped = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Compress(decompressed);
			bw.WriteBytes(gzipped);
			bw.Flush();
			#endregion
		}
	}
}
