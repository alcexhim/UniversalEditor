using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
namespace UniversalEditor.DataFormats.Multimedia.BluRay
{
	public class HDMVDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Blu-Ray/AVCHD Clip Info", new byte?[][] { new byte?[] { new byte?(72), new byte?(68), new byte?(77), new byte?(86) } }, new string[] { "*.clpi", "*.cpi" });
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
