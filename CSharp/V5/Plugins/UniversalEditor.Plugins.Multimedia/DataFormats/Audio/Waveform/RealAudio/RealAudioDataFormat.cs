using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Waveform;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.RealAudio
{
	public class RealAudioDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("RealNetworks Audio", new byte?[][] { new byte?[] { new byte?(46), new byte?(114), new byte?(97), new byte?(253) } }, new string[] { "*.ra" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		private short mvarVersion = 3;
		public short Version { get { return mvarVersion; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BinaryReader br = base.Stream.BinaryReader;
			br.Endianness = Endianness.BigEndian;
			byte[] signature = br.ReadBytes(4u);
			this.mvarVersion = br.ReadInt16();
			if (this.mvarVersion == 3)
			{
				short headerSize = br.ReadInt16();
				byte[] unknown = br.ReadBytes(10u);
				int dataSize = br.ReadInt32();
				byte titleStringLength = br.ReadByte();
				string titleString = br.ReadNullTerminatedString((int)titleStringLength);
				byte authorStringLength = br.ReadByte();
				string authorString = br.ReadNullTerminatedString((int)authorStringLength);
				byte copyrightStringLength = br.ReadByte();
				string copyrightString = br.ReadNullTerminatedString((int)copyrightStringLength);
				byte commentStringLength = br.ReadByte();
				string commentString = br.ReadNullTerminatedString((int)commentStringLength);
				int finalHeaderSize = (int)(15 + titleStringLength + 1 + authorStringLength + 1 + copyrightStringLength + 1 + commentStringLength);
				int finalHeaderSizeDifference = (int)headerSize - finalHeaderSize;
				string fourccString = string.Empty;
				if (finalHeaderSizeDifference >= 1)
				{
					byte unknown2 = br.ReadByte();
					if (finalHeaderSizeDifference >= 2)
					{
						byte fourccStringLength = br.ReadByte();
						if (finalHeaderSizeDifference > 2)
						{
							fourccString = br.ReadFixedLengthString(fourccStringLength);
						}
					}
				}
			}
			else
			{
				if (this.mvarVersion == 4)
				{
					short unused = br.ReadInt16();
					string ra4signature = br.ReadFixedLengthString(4);
					int dataSize = br.ReadInt32();
					dataSize -= 39;
					short version2 = br.ReadInt16();
					int headerSize2 = br.ReadInt32();
					short codecFlavor = br.ReadInt16();
					int codedFrameSize = br.ReadInt32();
					byte[] unknown3 = br.ReadBytes(12u);
					short subPacketH = br.ReadInt16();
					short frameSize = br.ReadInt16();
					short subPacketSize = br.ReadInt16();
					short unknown4 = br.ReadInt16();
					short sampleRate = br.ReadInt16();
					short unknown5 = br.ReadInt16();
					short sampleSize = br.ReadInt16();
					short channels = br.ReadInt16();
					byte interleaverIDStringLength = br.ReadByte();
					string interleaverIDString = br.ReadFixedLengthString(interleaverIDStringLength);
					byte fourccStringLength = br.ReadByte();
					string fourccString = br.ReadFixedLengthString(fourccStringLength);
					byte[] unknown6 = br.ReadBytes(3u);
					byte titleStringLength = br.ReadByte();
					string titleString = br.ReadFixedLengthString(titleStringLength);
					byte authorStringLength = br.ReadByte();
					string authorString = br.ReadFixedLengthString(authorStringLength);
					byte copyrightStringLength = br.ReadByte();
					string copyrightString = br.ReadFixedLengthString(copyrightStringLength);
					byte commentStringLength = br.ReadByte();
					string commentString = br.ReadFixedLengthString(commentStringLength);
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave != null)
			{
				BinaryWriter bw = base.Stream.BinaryWriter;
				bw.Endianness = Endianness.BigEndian;
				byte[] signature = new byte[]
				{
					46, 
					114, 
					97, 
					253
				};
				bw.Write(signature);
				bw.Write(this.mvarVersion);
				switch (this.mvarVersion)
				{
					case 3:
					{
						byte[] unknown = new byte[10];
						int dataSize = 0;
						string titleString = wave.Information.SongTitle;
						byte titleStringLength = (byte)titleString.Length;
						string authorString = wave.Information.SongArtist;
						byte authorStringLength = (byte)authorString.Length;
						string copyrightString = wave.Information.CustomProperties["Copyright"].Value.ToString();
						byte copyrightStringLength = (byte)copyrightString.Length;
						string commentString = wave.Information.Comments;
						byte commentStringLength = (byte)commentString.Length;
						short headerSize = (short)(15 + titleStringLength + 1 + authorStringLength + 1 + copyrightStringLength + 1 + commentStringLength + 6);
						bw.Write(headerSize);
						bw.Write(unknown);
						bw.Write(dataSize);
						bw.Write(titleStringLength);
						bw.WriteNullTerminatedString(titleString, (int)titleStringLength);
						bw.Write(authorStringLength);
						bw.WriteNullTerminatedString(authorString, (int)authorStringLength);
						bw.Write(copyrightStringLength);
						bw.WriteNullTerminatedString(copyrightString, (int)copyrightStringLength);
						bw.Write(commentStringLength);
						bw.WriteNullTerminatedString(commentString, (int)commentStringLength);
						bw.Write(0);
						bw.Write(4);
						bw.WriteFixedLengthString("lpcJ");
						break;
					}
					case 4:
					{
						short unused = 0;
						bw.Write(unused);
						string ra4signature = ".ra4";
						bw.WriteFixedLengthString(ra4signature);
						int dataSize = 39;
						bw.Write(dataSize);
						string titleString = wave.Information.SongTitle;
						byte titleStringLength = (byte)titleString.Length;
						string authorString = wave.Information.SongArtist;
						byte authorStringLength = (byte)authorString.Length;
						string copyrightString = wave.Information.CustomProperties["Copyright"].Value.ToString();
						byte copyrightStringLength = (byte)copyrightString.Length;
						string commentString = wave.Information.Comments;
						byte commentStringLength = (byte)commentString.Length;
						bw.Write(this.mvarVersion);
						int headerSize2 = 16;
						bw.Write(headerSize2);
						short codecFlavor = 0;
						bw.Write(codecFlavor);
						int codedFrameSize = 0;
						bw.Write(codedFrameSize);
						byte[] unknown2 = new byte[12];
						bw.Write(unknown2);
						short subPacketH = 0;
						bw.Write(subPacketH);
						short frameSize = 0;
						bw.Write(frameSize);
						short subPacketSize = 0;
						bw.Write(subPacketSize);
						short unknown3 = 0;
						bw.Write(unknown3);
						short sampleRate = 0;
						bw.Write(sampleRate);
						short unknown4 = 0;
						bw.Write(unknown4);
						short sampleSize = 0;
						bw.Write(sampleSize);
						short channels = 0;
						bw.Write(channels);
						byte interleaverIDStringLength = 4;
						bw.Write(interleaverIDStringLength);
						string interleaverIDString = "\0\0\0\0";
						bw.Write(interleaverIDString);
						byte fourccStringLength = 4;
						bw.Write(fourccStringLength);
						string fourccString = "lpcJ";
						bw.Write(fourccString);
						byte[] unknown5 = new byte[3];
						bw.Write(unknown5);
						bw.Write(titleStringLength);
						bw.Write(titleString);
						bw.Write(authorStringLength);
						bw.Write(authorString);
						bw.Write(copyrightStringLength);
						bw.Write(copyrightString);
						bw.Write(commentStringLength);
						bw.Write(commentString);
						break;
					}
				}
				bw.Flush();
			}
		}
	}
}
