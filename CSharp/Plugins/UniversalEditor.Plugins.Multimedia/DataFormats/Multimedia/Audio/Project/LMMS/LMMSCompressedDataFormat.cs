using System;
using UniversalEditor.Accessors.Stream;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Project;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Project.LMMS
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
            StreamAccessor file = new StreamAccessor(objectModel, mmp);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(output);

			file.Open(ms);
			file.Load();
			file.Close();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
