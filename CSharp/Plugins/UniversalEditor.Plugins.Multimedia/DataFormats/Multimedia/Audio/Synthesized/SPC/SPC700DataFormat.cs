using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	public class SPC700DataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("SNES-SPC700 sound file", new byte?[][] { new byte?[] { (byte)'S', (byte)'N', (byte)'E', (byte)'S', (byte)'-', (byte)'S', (byte)'P', (byte)'C', (byte)'7', (byte)'0', (byte)'0', (byte)' ', (byte)'S', (byte)'o', (byte)'u', (byte)'n', (byte)'d', (byte)' ', (byte)'F', (byte)'i', (byte)'l', (byte)'e', (byte)' ', (byte)'D', (byte)'a', (byte)'t', (byte)'a' } }, new string[] { "*.spc" });
			dfr.Capabilities.Add(typeof(SynthesizedAudioObjectModel), DataFormatCapabilities.All);
            
            dfr.ExportOptions.Add(new ExportOptionChoice("Generator", "&Generator:", true,
                new ExportOptionFieldChoice("Unknown", SPC700Emulator.Unknown, true),
                new ExportOptionFieldChoice("ZSNES", SPC700Emulator.ZSNES),
                new ExportOptionFieldChoice("Snes9x", SPC700Emulator.Snes9x)
            ));

			return dfr;
		}

		private byte[] mvarID666Reserved = new byte[45];
		public byte[] ID666Reserved { get { return mvarID666Reserved; } set { mvarID666Reserved = value; } }
		private byte mvarID666DefaultChannelDisables = 0;
		public byte ID666DefaultChannelDisables { get { return mvarID666DefaultChannelDisables; } set { mvarID666DefaultChannelDisables = value; } }

        private SPC700Emulator mvarGenerator = SPC700Emulator.Unknown;
        /// <summary>
        /// The emulator that generated this file.
        /// </summary>
        public SPC700Emulator Generator { get { return mvarGenerator; } set { mvarGenerator = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			SynthesizedAudioObjectModel au = (objectModel as SynthesizedAudioObjectModel);
			
            BinaryReader br = base.Stream.BinaryReader;
			string fileHeader = br.ReadFixedLengthString(33);
            if (!fileHeader.StartsWith("SNES-SPC700 Sound File Data")) throw new InvalidDataFormatException("File does not begin with \"SNES-SPC700 Sound File Data\"");

			byte[] flags = br.ReadBytes(2u);
			byte hasID666Value = br.ReadByte();
			bool hasID666 = (hasID666Value == 26);
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
                mvarGenerator = (SPC700Emulator)br.ReadByte();
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
                bw.Write((byte)mvarGenerator);

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
