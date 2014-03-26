using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
namespace UniversalEditor.DataFormats.Multimedia.BluRay
{
	public class MOBJDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Blu-Ray/AVCHD Movie Object", new byte?[][] { new byte?[] { new byte?(77), new byte?(79), new byte?(66), new byte?(74) } }, new string[] { "*.bdmv", "*.bdm" });
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PlaylistObjectModel pom = objectModel as PlaylistObjectModel;
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			string signature = br.ReadFixedLengthString(4);
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
