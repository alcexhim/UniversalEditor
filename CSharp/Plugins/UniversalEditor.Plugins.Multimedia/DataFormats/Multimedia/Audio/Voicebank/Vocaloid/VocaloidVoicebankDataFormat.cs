using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.Accessors;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Voicebank.Vocaloid
{
	public class VocaloidVoicebankDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Filters.Add("VOCALOID voice bank database", new byte?[][] { new byte?[] { new byte?(70), new byte?(45), new byte?(0), new byte?(0) }, new byte?[] { new byte?(70), new byte?(82), new byte?(77), new byte?(50) } }, new string[] { "*.ddb" });
				_dfr.Capabilities.Add(typeof(VoicebankObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;

			VoicebankObjectModel vom = (objectModel as VoicebankObjectModel);
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			byte[] signature = br.ReadBytes(4u);
			int vocaloidVersion = 1;
			if (signature[0] == 70 && signature[1] == 45 && signature[2] == 0 && signature[3] == 0)
			{
				vocaloidVersion = 1;
			}
			else
			{
				if (signature[0] == 70 && signature[1] == 82 && signature[2] == 77 && signature[3] == 50)
				{
					vocaloidVersion = 2;
				}
			}
			if (vom != null)
			{
				vom.CreatorVersion = new Version(vocaloidVersion, 0);
			}

			int i = 0;
			while (!br.EndOfStream)
			{
				string chunkName = br.ReadFixedLengthString(4);
				uint chunkSize = br.ReadUInt32();
				uint dataSize = chunkSize - 8;
				byte[] chunkData = br.ReadBytes(dataSize);

				switch (chunkName)
				{
					case "SND ":
					{
						Reader r = new Reader(new MemoryAccessor(chunkData));
						int size = r.ReadInt32();
						int freq = r.ReadInt32();
						short channels = br.ReadInt16();
						int dummy = br.ReadInt32();

						// TODO: test this
						byte[] data = br.ReadBytes(size - 18);

						if (fsom != null)
						{
							fsom.Files.Add(i.ToString() + ".raw", data);
						}
						Console.WriteLine("found sound file " + i.ToString() + ":");
						Console.WriteLine("    size: " + size.ToString());
						Console.WriteLine("    freq: " + freq.ToString());
						Console.WriteLine("    channels: " + channels.ToString());
						Console.WriteLine("    unknown_int32: " + dummy.ToString());
						i++;
						break;
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
