using System;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Project;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Project.LMMS
{
	public class LMMSCompressedDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Linux MultiMedia Studio (LMMS) project (compressed)", new string[] { "*.mmpz" });
			dfr.Capabilities.Add(typeof(AudioProjectObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BinaryReader br = base.Stream.BinaryReader;
			byte[] input = br.ReadToEnd();
			byte[] output = CompressionStream.Decompress(CompressionMethod.Gzip, input);
			LMMSProjectDataFormat mmp = new LMMSProjectDataFormat();
			mmp.Open(ref output);
			mmp.Load(ref objectModel);
			mmp.Close();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
