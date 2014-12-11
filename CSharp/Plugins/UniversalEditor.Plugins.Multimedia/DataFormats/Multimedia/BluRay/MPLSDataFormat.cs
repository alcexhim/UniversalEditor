using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
namespace UniversalEditor.DataFormats.Multimedia.BluRay
{
	public class MPLSDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Blu-Ray/AVCHD Media PlayList", new byte?[][] { new byte?[] { new byte?(77), new byte?(80), new byte?(76), new byte?(83) } }, new string[] { "*.mpls", "*.mpl" });
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PlaylistObjectModel pom = objectModel as PlaylistObjectModel;
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			string signature = br.ReadFixedLengthString(4);
			string version = br.ReadFixedLengthString(4);
			byte[] unknown = br.ReadBytes(54u);
			int count = br.ReadInt32();
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
