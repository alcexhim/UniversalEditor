using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.BluRay
{
	public class BDJODataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				// _dfr.Capabilities.Add(typeof(
				_dfr.Filters.Add("Blu-Ray Disc Java Object", new string[] { "*.bdjo" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			// PlaylistObjectModel pom = objectModel as PlaylistObjectModel;
			IO.BinaryReader br = base.Stream.BinaryReader;
			br.Endianness = IO.Endianness.BigEndian;
			string signature = br.ReadFixedLengthString(4);
			string version = br.ReadFixedLengthString(4);

			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
