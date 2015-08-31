using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Synthesized;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Synthesized.SPC
{
	public class SPC700DataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("SNES-SPC700 sound file", new byte?[][] { new byte?[] { new byte?(83), new byte?(78), new byte?(69), new byte?(83), new byte?(45), new byte?(83), new byte?(80), new byte?(67), new byte?(55), new byte?(48), new byte?(48), new byte?(32), new byte?(83), new byte?(111), new byte?(117), new byte?(110), new byte?(100), new byte?(32), new byte?(70), new byte?(105), new byte?(108), new byte?(101), new byte?(32), new byte?(68), new byte?(97), new byte?(116), new byte?(97) } }, new string[] { "*.spc" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private byte[] mvarID666Reserved = new byte[45];
		public byte[] ID666Reserved { get { return mvarID666Reserved; } set { mvarID666Reserved = value; } }
		private byte mvarID666DefaultChannelDisables = 0;
		public byte ID666DefaultChannelDisables { get { return mvarID666DefaultChannelDisables; } set { mvarID666DefaultChannelDisables = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel au = (objectModel as SynthesizedAudioObjectModel);
			BinaryReader br = base.Stream.BinaryReader;
			string fileHeader = br.ReadFixedLengthString(33);
			byte[] flags = br.ReadBytes(2u);
			byte hasID666Value = br.ReadByte();
			bool hasID666 = hasID666Value == 26;
			byte versionMinor = br.ReadByte();
			short regPC = br.ReadInt16();
			byte regA = br.ReadByte();
			byte regX = br.ReadByte();
			byte regY = br.ReadByte();
			byte regPSW = br.ReadByte();
			byte regSP = br.ReadByte();
			short regReserved = br.ReadInt16();
			if (hasID666)
			{
				au.Information.SongTitle = br.ReadNullTerminatedString(32);
				au.Information.AlbumTitle = br.ReadNullTerminatedString(32);
				au.Information.Creator = br.ReadNullTerminatedString(16);
				au.Information.Comments = br.ReadNullTerminatedString(32);
				string id666DumpDate = br.ReadNullTerminatedString(11);
				au.Information.DateCreated = new DateTime(int.Parse(id666DumpDate.Substring(0, 4)), int.Parse(id666DumpDate.Substring(3, 2)), int.Parse(id666DumpDate.Substring(5, 2)));
				au.Information.FadeOutDelay = int.Parse(br.ReadNullTerminatedString(3));
				au.Information.FadeOutLength = int.Parse(br.ReadNullTerminatedString(5));
				au.Information.SongArtist = br.ReadNullTerminatedString(32);
				this.mvarID666DefaultChannelDisables = br.ReadByte();
				switch (br.ReadByte())
				{
					case 0:
					{
						au.Information.GeneratorTitle = string.Empty;
						break;
					}
					case 1:
					{
						au.Information.GeneratorTitle = "ZSNES";
						break;
					}
					case 2:
					{
						au.Information.GeneratorTitle = "Snes9x";
						break;
					}
				}
				byte[] id666Reserved = br.ReadBytes(45u);
				this.mvarID666Reserved = id666Reserved;
			}
			byte[] regRAM = br.ReadBytes(65536u);
			byte[] regDSP = br.ReadBytes(128u);
			byte[] regUnused2 = br.ReadBytes(64u);
			byte[] regExtra = br.ReadBytes(64u);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel au = objectModel as SynthesizedAudioObjectModel;
			BinaryWriter bw = base.Stream.BinaryWriter;
			bw.WriteFixedLengthString("SNES-SPC700 Sound File Data v0.30");
			byte[] flags = new byte[]
			{
				26, 
				26
			};
			bw.Write(flags);
			bool hasID666 = true;
			if (hasID666)
			{
				bw.Write(26);
			}
			else
			{
				bw.Write(27);
			}
			byte versionMinor = 30;
			bw.Write(versionMinor);
			short regPC = 0;
			bw.Write(regPC);
			byte regA = 0;
			bw.Write(regA);
			byte regX = 0;
			bw.Write(regX);
			byte regY = 0;
			bw.Write(regY);
			byte regPSW = 0;
			bw.Write(regPSW);
			byte regSP = 0;
			bw.Write(regSP);
			short regReserved = 0;
			bw.Write(regReserved);
			if (hasID666)
			{
				bw.WriteFixedLengthString(au.Information.SongTitle, 32);
				bw.WriteFixedLengthString(au.Information.AlbumTitle, 32);
				bw.WriteFixedLengthString(au.Information.Creator, 16);
				bw.WriteFixedLengthString(au.Information.Comments, 32);
				string id666DumpDate = au.Information.DateCreated.ToString("YYYYMMDD");
				bw.WriteFixedLengthString(id666DumpDate, 11);
				BinaryWriter arg_15E_0 = bw;
				int num = au.Information.FadeOutDelay;
				arg_15E_0.WriteFixedLengthString(num.ToString(), 3);
				BinaryWriter arg_17A_0 = bw;
				num = au.Information.FadeOutLength;
				arg_17A_0.WriteFixedLengthString(num.ToString(), 5);
				bw.WriteFixedLengthString(au.Information.SongArtist, 32);
				bw.Write(this.mvarID666DefaultChannelDisables);
				string generatorTitle = au.Information.GeneratorTitle;
				if (generatorTitle != null)
				{
					if (generatorTitle == "ZSNES")
					{
						bw.Write(1);
						goto IL_1F1;
					}
					if (generatorTitle == "Snes9x")
					{
						bw.Write(2);
						goto IL_1F1;
					}
				}
				bw.Write(0);
				IL_1F1:
				bw.WriteFixedLengthBytes(this.mvarID666Reserved, 45);
			}
			byte[] regRAM = new byte[65536];
			bw.Write(regRAM);
			byte[] regDSP = new byte[128];
			bw.Write(regDSP);
			byte[] regUnused2 = new byte[64];
			bw.Write(regUnused2);
			byte[] regExtra = new byte[64];
			bw.Write(regExtra);
			bw.Flush();
		}
	}
}
