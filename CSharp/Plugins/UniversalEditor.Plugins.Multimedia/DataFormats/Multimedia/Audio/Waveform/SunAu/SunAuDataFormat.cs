using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.SunAu
{
	public class SunAuDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Sun Microsystems audio file", new byte?[][] { new byte?[] { new byte?(46), new byte?(115), new byte?(110), new byte?(100) } }, new string[] { "*.au" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private SunAuEncoding Int32ToSunAuEncoding(int encoding)
		{
			SunAuEncoding result;
			switch (encoding)
			{
				case 1:
				{
					result = SunAuEncoding.G711ULaw8Bit;
					return result;
				}
				case 2:
				{
					result = SunAuEncoding.LinearPCM8Bit;
					return result;
				}
				case 3:
				{
					result = SunAuEncoding.LinearPCM16Bit;
					return result;
				}
				case 4:
				{
					result = SunAuEncoding.LinearPCM24Bit;
					return result;
				}
				case 5:
				{
					result = SunAuEncoding.LinearPCM32Bit;
					return result;
				}
				case 6:
				{
					result = SunAuEncoding.IeeeFloatingPoint32Bit;
					return result;
				}
				case 7:
				{
					result = SunAuEncoding.IeeeFloatingPoint64Bit;
					return result;
				}
				case 8:
				{
					result = SunAuEncoding.FragmentedSampleData;
					return result;
				}
				case 9:
				{
					result = SunAuEncoding.DspProgram;
					return result;
				}
				case 10:
				{
					result = SunAuEncoding.FixedPoint8Bit;
					return result;
				}
				case 11:
				{
					result = SunAuEncoding.FixedPoint16Bit;
					return result;
				}
				case 12:
				{
					result = SunAuEncoding.FixedPoint24Bit;
					return result;
				}
				case 13:
				{
					result = SunAuEncoding.FixedPoint32Bit;
					return result;
				}
				case 18:
				{
					result = SunAuEncoding.LinearWithEmphasis16Bit;
					return result;
				}
				case 19:
				{
					result = SunAuEncoding.LinearCompressed16Bit;
					return result;
				}
				case 20:
				{
					result = SunAuEncoding.LinearWithEmphasisCompressed16Bit;
					return result;
				}
				case 21:
				{
					result = SunAuEncoding.DspProgramMusicKit;
					return result;
				}
				case 23:
				{
					result = SunAuEncoding.G721Adpcm4Bit;
					return result;
				}
				case 24:
				{
					result = SunAuEncoding.G722Adpcm4Bit;
					return result;
				}
				case 25:
				{
					result = SunAuEncoding.G723Adpcm3Bit;
					return result;
				}
				case 26:
				{
					result = SunAuEncoding.G723Adpcm5Bit;
					return result;
				}
				case 27:
				{
					result = SunAuEncoding.G711ALaw8Bit;
					return result;
				}
			}
			result = SunAuEncoding.Unknown;
			return result;
		}
		private int SunAuEncodingToInt32(SunAuEncoding encoding)
		{
			int result;
			switch (encoding)
			{
				case SunAuEncoding.G711ULaw8Bit:
				{
					result = 1;
					break;
				}
				case SunAuEncoding.LinearPCM8Bit:
				{
					result = 2;
					break;
				}
				case SunAuEncoding.LinearPCM16Bit:
				{
					result = 3;
					break;
				}
				case SunAuEncoding.LinearPCM24Bit:
				{
					result = 4;
					break;
				}
				case SunAuEncoding.LinearPCM32Bit:
				{
					result = 5;
					break;
				}
				case SunAuEncoding.IeeeFloatingPoint32Bit:
				{
					result = 6;
					break;
				}
				case SunAuEncoding.IeeeFloatingPoint64Bit:
				{
					result = 7;
					break;
				}
				case SunAuEncoding.FragmentedSampleData:
				{
					result = 8;
					break;
				}
				case SunAuEncoding.DspProgram:
				{
					result = 9;
					break;
				}
				case SunAuEncoding.FixedPoint8Bit:
				{
					result = 10;
					break;
				}
				case SunAuEncoding.FixedPoint16Bit:
				{
					result = 11;
					break;
				}
				case SunAuEncoding.FixedPoint24Bit:
				{
					result = 12;
					break;
				}
				case SunAuEncoding.FixedPoint32Bit:
				{
					result = 13;
					break;
				}
				case SunAuEncoding.LinearWithEmphasis16Bit:
				{
					result = 18;
					break;
				}
				case SunAuEncoding.LinearCompressed16Bit:
				{
					result = 19;
					break;
				}
				case SunAuEncoding.LinearWithEmphasisCompressed16Bit:
				{
					result = 20;
					break;
				}
				case SunAuEncoding.DspProgramMusicKit:
				{
					result = 21;
					break;
				}
				case SunAuEncoding.G721Adpcm4Bit:
				{
					result = 23;
					break;
				}
				case SunAuEncoding.G722Adpcm4Bit:
				{
					result = 24;
					break;
				}
				case SunAuEncoding.G723Adpcm3Bit:
				{
					result = 25;
					break;
				}
				case SunAuEncoding.G723Adpcm5Bit:
				{
					result = 26;
					break;
				}
				case SunAuEncoding.G711ALaw8Bit:
				{
					result = 27;
					break;
				}
				default:
				{
					result = -1;
					break;
				}
			}
			return result;
		}

		private SunAuEncoding mvarEncoding = SunAuEncoding.Unknown;
		public SunAuEncoding Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;
			string magicNumber = br.ReadFixedLengthString(4);
			int dataOffset = br.ReadInt32();
			uint dataSize = br.ReadUInt32();
			int encoding = br.ReadInt32();
			mvarEncoding = Int32ToSunAuEncoding(encoding);
			int sampleRate = br.ReadInt32();
			int channels = br.ReadInt32();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.Endianness = Endianness.BigEndian;
			bw.WriteFixedLengthString(".snd");
			int dataOffset = 24;
			bw.WriteInt32(dataOffset);
			uint dataSize = 4294967295u;
			bw.WriteUInt32(dataSize);
			int encoding = SunAuEncodingToInt32(mvarEncoding);
			bw.WriteInt32(encoding);
			int sampleRate = 0;
			bw.WriteInt32(sampleRate);
			int channels = 2;
			bw.WriteInt32(channels);
			bw.Flush();
		}
	}
}
