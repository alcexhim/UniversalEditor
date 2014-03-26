using System;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.SunAu
{
	public enum SunAuEncoding
	{
		Unknown = -1,
		G711ULaw8Bit,
		LinearPCM8Bit,
		LinearPCM16Bit,
		LinearPCM24Bit,
		LinearPCM32Bit,
		IeeeFloatingPoint32Bit,
		IeeeFloatingPoint64Bit,
		FragmentedSampleData,
		DspProgram,
		FixedPoint8Bit,
		FixedPoint16Bit,
		FixedPoint24Bit,
		FixedPoint32Bit,
		LinearWithEmphasis16Bit,
		LinearCompressed16Bit,
		LinearWithEmphasisCompressed16Bit,
		DspProgramMusicKit,
		G721Adpcm4Bit,
		G722Adpcm4Bit,
		G723Adpcm3Bit,
		G723Adpcm5Bit,
		G711ALaw8Bit
	}
}
