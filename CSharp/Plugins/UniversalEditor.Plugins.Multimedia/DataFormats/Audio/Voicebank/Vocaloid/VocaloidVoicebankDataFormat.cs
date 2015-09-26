using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Voicebank;
using UniversalEditor.ObjectModels.FileSystem;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Voicebank.Vocaloid
{
	public class VocaloidVoicebankDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("VOCALOID voice bank database", new byte?[][] { new byte?[] { new byte?(70), new byte?(45), new byte?(0), new byte?(0) }, new byte?[] { new byte?(70), new byte?(82), new byte?(77), new byte?(50) } }, new string[] { "*.ddb" });
			dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.Load);
			dfr.Capabilities.Add(typeof(VoicebankObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BinaryReader br = base.Stream.BinaryReader;

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
				br.SeekUntil("SND ");

				string SND_ = br.ReadFixedLengthString(4);
				if (SND_ == "SND ")
				{
					int sign = br.ReadInt32();
					int size = br.ReadInt32();
					int freq = br.ReadInt32();
					short channels = br.ReadInt16();
					int dummy = br.ReadInt32();

					byte[] data = br.ReadBytes(size - 18);

					if (fsom != null)
					{
						fsom.Files.Add(i.ToString() + ".raw", data);
					}
					Console.WriteLine("found sound file " + i.ToString() + ":");
                    Console.WriteLine("    sign: " + sign.ToString());
                    Console.WriteLine("    size: " + size.ToString());
                    Console.WriteLine("    freq: " + freq.ToString());
                    Console.WriteLine("    channels: " + channels.ToString());
                    Console.WriteLine("    unknown_int32: " + dummy.ToString());
					i++;
				}
				else
				{
					break;
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
