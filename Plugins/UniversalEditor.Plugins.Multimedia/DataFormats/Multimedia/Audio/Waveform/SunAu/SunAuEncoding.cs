//
//  SunAuEncoding.cs - indicates the encoding of the Sun .au file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.SunAu
{
	/// <summary>
	/// Indicates the encoding of the Sun .au file.
	/// </summary>
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
